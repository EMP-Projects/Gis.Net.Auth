using AutoMapper;
using Gis.Net.Auth.DTO;
using Gis.Net.Auth.Entities;
using Gis.Net.Core.Mapper;

namespace Gis.Net.Auth.Mapper;

public abstract class AuthMapper<TDto, TModel, TRequest> : AbstractMapperProfile<TModel, TDto, TRequest>
    where TDto: LoginDto, IReducedUser, IFullUser
    where TModel: Login, IReducedUser, IFullUser
    where TRequest: LoginRequestDto, IReducedUser, IFullUser
{
    protected IMappingExpression<TDto, AuthUserDto> DtoToAuthDtoMapper;
    protected IMappingExpression<LoginRequestDto, LoginDto> LoginMapper;
    
    /// <inheritdoc />
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