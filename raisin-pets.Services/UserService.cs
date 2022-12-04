namespace raisin_pets.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _memoryCache;

    public UserService(IUserRepository userRepository, IMapper mapper, IMemoryCache memoryCache)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _memoryCache = memoryCache;
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

    public void Logout(string token)
    {
        var response = _memoryCache.TryGetValue<List<string>>("blacklistedJwts", out var blacklistedJwts);

        if (!response || blacklistedJwts is null || !blacklistedJwts.Any())
        {
            _memoryCache.Set("blacklistedJwts", new List<string> { token }, TimeSpan.FromHours(1));
            return;
        }
        
        blacklistedJwts.Add(token);
        _memoryCache.Set("blacklistedJwts", blacklistedJwts, TimeSpan.FromHours(1));
    }
}