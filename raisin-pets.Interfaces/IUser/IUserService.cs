namespace raisin_pets.Interfaces.IUser;

public interface IUserService
{
    Task<Response<UserDto>> GetByGoogleNameIdentifierAsync(string identifier);
    Task<Response<UserDto>> LoginAsync(string token);
    Task<Response<UserDto>> SignupAsync(string token);
}