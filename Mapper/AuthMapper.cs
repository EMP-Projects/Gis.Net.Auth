using AutoMapper;
using Gis.Net.Auth.DTO;
using Gis.Net.Auth.Entities;
using Gis.Net.Core.Mapper;

namespace Gis.Net.Auth.Mapper;

/// <summary>
/// The AuthMapper class is an abstract mapper profile that maps between DTOs and models for authentication purposes.
/// </summary>
/// <typeparam name="TDto">The DTO type.</typeparam>
/// <typeparam name="TModel">The model type.</typeparam>
/// <typeparam name="TRequest">The request DTO type.</typeparam>
public abstract class AuthMapper<TDto, TModel, TRequest> : AbstractMapperProfile<TModel, TDto, TRequest>
    where TDto: LoginDto, IReducedUser, IFullUser
    where TModel: Login, IReducedUser, IFullUser
    where TRequest: LoginRequestDto, IReducedUser, IFullUser
{
    /// <summary>
    /// Represents a mapper for converting DTOs to the <see cref="AuthUserDto"/> object.
    /// </summary>
    /// <typeparam name="TDto">The type of the DTO to be mapped.</typeparam>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    protected IMappingExpression<TDto, AuthUserDto> DtoToAuthDtoMapper;

    /// <summary>
    /// Represents a mapper for converting login request DTOs to login DTOs.
    /// </summary>
    protected IMappingExpression<LoginRequestDto, LoginDto> LoginMapper;

    /// <summary>
    /// Represents a mapper for authentication entities and DTOs.
    /// </summary>
    /// <typeparam name="TDto">The type of the DTO.</typeparam>
    /// <typeparam name="TModel">The type of the entity model.</typeparam>
    /// <typeparam name="TRequest">The type of the request DTO.</typeparam>
    protected AuthMapper()
    {
        DtoToModelMapper
            .ForMember(dest => dest.Username, opt => opt.Ignore())
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForMember(dest => dest.ApiKeyToken, opt => opt.Ignore());
        
        DtoToModelMapper
            .ForAllMembers(x => x.Condition((source, dest, inMember) => source != null));
        
        DtoToAuthDtoMapper = CreateMap<TDto, AuthUserDto>();
        
        // mapping per il login di un utente
        LoginMapper = CreateMap<LoginRequestDto, LoginDto>();
    }
}