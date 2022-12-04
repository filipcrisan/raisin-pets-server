namespace raisin_pets.Handlers;

/// <summary>
/// Use AuthenticationHandler since it's executed before middlewares and filters.
/// </summary>
public class GoogleAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _memoryCache;

    public GoogleAuthenticationHandler(
        IUserService userService,
        IMapper mapper,
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IMemoryCache memoryCache)
        : base(options, logger, encoder, clock)
    {
        _userService = userService;
        _mapper = mapper;
        _memoryCache = memoryCache;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // check if [AllowAnonymous] is present
        var endpoint = Context.GetEndpoint();
        if (endpoint?.Metadata.GetMetadata<IAllowAnonymous>() is not null)
        {
            return AuthenticateResult.NoResult();
        }

        if (!Request.Headers.ContainsKey("Authorization"))
        {
            const string errorMessage = "Missing authorization header in request";
            return AuthenticateResult.Fail(errorMessage);
        }

        var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
        var cacheResponse = _memoryCache.TryGetValue<List<string>>("blacklistedJwts", out var blacklistedJwts);
        if (cacheResponse && blacklistedJwts is not null && blacklistedJwts.Contains(authHeader.Parameter))
        {
            const string errorMessage = "Blacklisted JWT";
            return AuthenticateResult.Fail(errorMessage);
        }

        var user = _mapper.Map<Response<UserDto>>(await _userService.LoginAsync(authHeader.Parameter)).Payload;

        if (user is null)
        {
            const string errorMessage = "Invalid user data";
            return AuthenticateResult.Fail(errorMessage);
        }

        var claims = new[]
        {
            // add necessary claims for the request to pass
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.GoogleNameIdentifier)
        };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }
}