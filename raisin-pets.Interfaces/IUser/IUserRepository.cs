namespace raisin_pets.Interfaces.IUser;

public interface IUserRepository
{
    Task<Response<User>> GetByGoogleNameIdentifierAsync(string identifier);
    Task<Response<User>> AddAsync(CreateUserDto userDto);
}