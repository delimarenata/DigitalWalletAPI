using DigitalWalletAPI.DTOs;

namespace DigitalWalletAPI.IServices
{

    public interface IApiUserService
    {
        Task<string> AuthenticateAsync(ApiUserLoginDto loginDto);
        Task RegisterAsync(ApiUserDto userDto);
    }

}