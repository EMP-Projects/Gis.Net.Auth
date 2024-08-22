using System.Security.Authentication;
using AutoMapper;
using Gis.Net.Auth.DTO;
using Gis.Net.Core.DTO;
using Gis.Net.Core.Entities;
using Gis.Net.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Gis.Net.Auth.Repositories;

/// <inheritdoc cref="IAuthRepository" />
public abstract class AuthRepository<TModel, TDto, TQuery, TContext> : 
    RepositoryCore<TModel, TDto, TQuery, TContext>,
    IAuthRepository
    where TModel : ModelBase, ILogin
    where TDto : DtoBase, ILoginDto
    where TQuery : AuthQuery, new()
    where TContext: DbContext
{
    /// <inheritdoc />
    protected AuthRepository(
        ILogger logger,
        TContext context, 
        IMapper mapper
    ) : base(logger, context, mapper)
    {
        
    }

    /// <inheritdoc />
    protected override IQueryable<TModel> ParseQueryParams(IQueryable<TModel> query, TQuery? queryByParams)
    {
        if (queryByParams?.Ids is not null)
            query = query.Where(f => queryByParams.Ids.Contains(f.Id));

        if (queryByParams?.Username is not null)
            query = query.Where(f => queryByParams.Username.ToLower().Equals(f.Username.ToLower()));

        if (queryByParams?.ApiKeyToken is not null)
            query = query.Where(f => string.IsNullOrEmpty(f.ApiKeyToken) == false);
        
        return base.ParseQueryParams(query, queryByParams);   
    }

    /// <inheritdoc />
    public virtual async Task<AuthUserDto> SignIn(LoginRequestDto login)
    {
        var users = await GetRows(new ListOptions<TModel, TDto, TQuery>(new TQuery()));
        var user = login.ApiKeyToken is not null
            ? users.FirstOrDefault(u => PasswordHasher.VerifyHashedPasswordV3(u.ApiKeyToken!, login.ApiKeyToken))
            : users.Where(x => x.Username != null && x.Username.Equals(login.Username))
                .FirstOrDefault(x => x.Password != null && PasswordHasher.VerifyHashedPasswordV3(x.Password, login.Password));
        
        if (user is null)
            throw new Exception("Login failed");

        return Mapper.Map<AuthUserDto>(user);
    }

    /// <inheritdoc />
    public virtual async Task<AuthUserDto> SignUp(ILoginDto signUpDto)
    {
        var user = Mapper.Map<TDto>(signUpDto);
        var result = await InsertAsync(
            user,
            new InsertOptions<TModel, TDto, TQuery>
            {
                OnExtraMapping = (dto, model) =>
                {
                    model.Password = PasswordHasher.HashPasswordV3(signUpDto.Password!);
                    if (string.IsNullOrEmpty(signUpDto.ApiKeyToken) == false)
                        model.ApiKeyToken = PasswordHasher.HashPasswordV3(signUpDto.ApiKeyToken);
                }
            }
        );

        await SaveChanges();
        Logger.LogInformation("Users {Username} saved successfully", signUpDto.Username);
        return Mapper.Map<AuthUserDto>(result);
    }

    /// <inheritdoc />
    public virtual async Task<AuthUserDto> GetAuthUserById(long id)
    {
        var result = await GetRowByFirst(
            new ListOptions<TModel, TDto, TQuery>(new TQuery
                {
                    Id = id
                }
        ));

        return Mapper.Map<AuthUserDto>(result);
    }

    /// <inheritdoc />
    public async Task SetCredentials(long id, string username, string password)
    {
        var model = await FindAsync(id);
        var ctx = GetDbContext();
        model.Username = username.Trim();
        model.Password = PasswordHasher.HashPasswordV3(password);
        await ctx.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task ResetCredentials(long id)
    {
        var model = await FindAsync(id);
        var ctx = GetDbContext();
        model.Username = string.Empty;
        model.Password = string.Empty;
        await ctx.SaveChangesAsync();
    }

    /// <inheritdoc />
    public virtual async Task<ILogin> ChangePassword(ChangePasswordDto changePwdDto)
    {
        var userModel = await FindAsync((long)changePwdDto.Id!);
        if (userModel is null)
            throw new Exception("Unauthorized");

        if (!PasswordHasher.VerifyHashedPasswordV3(userModel.Password, changePwdDto.OldPassword!))
            throw new AuthenticationException("The password to be changed is invalid");

        var userDto = Mapper.Map<TDto>(userModel);

        return await Update(
            userDto,
            new UpdateOptions<TModel, TDto, TQuery>
            {
                OnExtraMapping = (dto, model) =>
                {
                    if (model is not ILogin modelLogin)
                        throw new Exception("User model is not ILogin");
                    modelLogin.Password = PasswordHasher.HashPasswordV3(changePwdDto.NewPassword!);
                }
            }
        );
    }

    /// <inheritdoc />
    public virtual async Task<ILogin> ChangeApiKey(ChangeApiKeyDto changeKeyDto)
    {
        if (string.IsNullOrEmpty(changeKeyDto.NewApiKey) || changeKeyDto.Id is null)
            throw new Exception("Missing parameters NewApiKey/Id");

        var userModel = await FindAsync(changeKeyDto.Id.Value);

        if (userModel.ApiKeyToken is null)
            throw new Exception("Call to method ChangeApiKey on a user without an ApiKey");

        if (userModel.ApiKeyToken.Equals(changeKeyDto.OldApiKey))
            throw new AuthenticationException("The ApiKey to be changed is invalid");

        var userDto = Mapper.Map<TDto>(userModel);

        return await Update(
            userDto,
            new UpdateOptions<TModel, TDto, TQuery>
            {
                OnExtraMapping = (dto, model) =>
                {
                    model.ApiKeyToken = PasswordHasher.HashPasswordV3(changeKeyDto.NewApiKey);
                }
            }
        );
    }
}
