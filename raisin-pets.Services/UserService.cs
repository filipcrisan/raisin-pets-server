namespace raisin_pets.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Response<UserDto>> GetByGoogleNameIdentifierAsync(string identifier)
    {
        var response = await _userRepository.GetByGoogleNameIdentifierAsync(identifier);

        return _mapper.Map<Response<UserDto>>(response);
    }

    public async Task<Response<UserDto>> LoginAsync(string token)
    {
        var tokenHandler = await GoogleJsonWebSignature.ValidateAsync(token);

        return await GetByGoogleNameIdentifierAsync(tokenHandler.Subject);
    }

    public async Task<Response<UserDto>> SignupAsync(string token)
    {
        var tokenHandler = await GoogleJsonWebSignature.ValidateAsync(token);

        var response = await _userRepository.AddAsync(new CreateUserDto
        {
            Email = tokenHandler.Email,
            FirstName = tokenHandler.GivenName,
            LastName = tokenHandler.FamilyName,
            GoogleNameIdentifier = tokenHandler.Subject,
        });

        return _mapper.Map<Response<UserDto>>(response);
    }
}