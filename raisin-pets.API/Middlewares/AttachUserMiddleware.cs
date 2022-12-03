namespace raisin_pets.Middlewares;

public class AttachUserMiddleware
{
    private readonly RequestDelegate _next;

    public AttachUserMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IUserService userService, IMapper mapper)
    {
        context.Request.Query.TryGetValue("token", out var token);

        var jwtToken = context.Request
            .Headers["Authorization"]
            .FirstOrDefault()?
            .Split(" ")
            .Last();

        if (jwtToken is null && token == StringValues.Empty)
        {
            var exception = new UnauthorizedAccessException("No JWT token found.");
            throw exception;
        }

        await AttachUserAsync(context, userService, mapper,
            jwtToken ?? token.ToString().Split(" ").LastOrDefault());

        await _next(context);
    }

    #region Private methods

    private static async Task AttachUserAsync(HttpContext context, IUserService userService,
        IMapperBase mapper, string jwtToken)
    {
        var handler = new JwtSecurityTokenHandler();
        var processedToken = (JwtSecurityToken)handler.ReadToken(jwtToken);
        var userDto = await userService.GetByGoogleNameIdentifierAsync(processedToken.Subject);

        if (userDto.Status != ResponseStatus.Success)
            return;
        context.Items["User"] = mapper.Map<UserDto>(userDto.Payload);
    }

    #endregion
}