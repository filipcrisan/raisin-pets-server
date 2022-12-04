namespace raisin_pets.Common.MapperProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserDto, User>();
        CreateMap<UserDto, UserViewModel>();
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<Response<User>, Response<UserDto>>();
    }
}