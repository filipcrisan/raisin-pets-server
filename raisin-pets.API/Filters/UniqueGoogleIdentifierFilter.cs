namespace raisin_pets.Filters;

public class UniqueGoogleIdentifierFilter : IAsyncActionFilter
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UniqueGoogleIdentifierFilter(
        IUserService userService,
        IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var authHeader = AuthenticationHeaderValue.Parse(context.ActionArguments["token"]!.ToString()).Parameter;
        var tokenHandler = await GoogleJsonWebSignature.ValidateAsync(authHeader);

        var user = _mapper.Map<Response<UserDto>>(
            await _userService.GetByGoogleNameIdentifierAsync(tokenHandler.Subject));
        if (user.Status == ResponseStatus.Success)
        {
            context.Result = new BadRequestObjectResult("User already signed up.");
            return;
        }

        await next();
    }
}