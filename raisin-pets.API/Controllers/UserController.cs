namespace raisin_pets.Controllers;

[ApiController]
[Route("api/users")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public UserController(IMapper mapper, IUserService userService)
    {
        _mapper = mapper;
        _userService = userService;
    }

    [HttpPut]
    [Route("login")]
    [AllowAnonymous]
    [ServiceFilter(typeof(ValidTokenFilter))]
    [Produces("application/json")]
    [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> LoginAsync([FromQuery] string token)
    {
        var authHeader = AuthenticationHeaderValue.Parse(token).Parameter;
        var loginResponse = await _userService.LoginAsync(authHeader);
        var response = loginResponse.Status != ResponseStatus.Failed
            ? loginResponse
            : await _userService.SignupAsync(authHeader);
        if (response.Status == ResponseStatus.Failed)
            return BadRequest();

        return Ok(_mapper.Map<UserViewModel>(response.Payload));
    }

    [HttpPut]
    [Route("logout")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult LogoutAsync()
    {
        _userService.Logout(
            AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]).Parameter);
        return Ok();
    }
}