using raisin_pets.Common.Dtos.User;
using raisin_pets.Common.Models;

namespace raisin_pets.Services;

public class UserService : IUserService
{
    public Task<Response<UserDto>> GetByGoogleNameIdentifierAsync(string identifier)
    {
        throw new NotImplementedException();
    }

    public Task<Response<UserDto>> LoginAsync(string token)
    {
        throw new NotImplementedException();
    }

    public Task<Response<UserDto>> SignupAsync(string token)
    {
        throw new NotImplementedException();
    }

    public Task BlacklistJwtAsync(string jwt)
    {
        throw new NotImplementedException();
    }
}