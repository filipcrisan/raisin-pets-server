namespace raisin_pets.Interfaces.User;

public interface IUserService
{
    Task<Response<UserDto>> GetByGoogleNameIdentifierAsync(string identifier);
    Task<Response<UserDto>> LoginAsync(string token);
    Task<Response<UserDto>> SignupAsync(string token);
    Task BlacklistJwtAsync(string jwt);
}