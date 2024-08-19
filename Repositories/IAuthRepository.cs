using Gis.Net.Auth.DTO;

namespace Gis.Net.Auth.Repositories;

public interface IAuthRepository 
{
    Task<AuthUserDto> SignIn(LoginRequestDto login);
    
    Task<AuthUserDto> SignUp(ILoginDto signUpDto);
    
    Task<AuthUserDto> GetAuthUserById(long id);
    
    Task<ILogin> ChangePassword(ChangePasswordDto changePwdDto);
    
    Task<ILogin> ChangeApiKey(ChangeApiKeyDto changeKeyDto);

    Task SetCredentials(long id, string username, string password);

    Task ResetCredentials(long id);
}