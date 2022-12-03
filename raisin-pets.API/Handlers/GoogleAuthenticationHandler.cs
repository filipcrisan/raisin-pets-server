namespace raisin_pets.Handlers;

/// <summary>
/// We use a handler for authorization since handlers are called before the request makes it into
/// a controller, whereas filters execute after the request passed the controller stage.
/// </summary>
public class GoogleAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public GoogleAuthenticationHandler(
        IUserService userService,
        IMapper mapper,
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock)
        : base(options, logger, encoder, clock)
    {
        _userService = userService;
        _mapper = mapper;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // check if the endpoint is decorated with an annotation that would allow anonymous usage [AllowAnonymous]  
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
        // TODO
        // https://learn.microsoft.com/en-us/aspnet/core/performance/caching/memory?view=aspnetcore-7.0
        // if ((await _cacheContext.Users.GetBlacklistedJwt(authHeader.Parameter)).Status == ResponseStatus.CacheHit)
        // {
        //     const string errorMessage = "Blacklisted JWT";
        //     return AuthenticateResult.Fail(errorMessage);
        // }

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