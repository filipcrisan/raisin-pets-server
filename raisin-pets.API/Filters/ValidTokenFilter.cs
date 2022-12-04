namespace raisin_pets.Filters;

public class ValidTokenFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        context.ActionArguments.TryGetValue("token", out var token);

        if (token is null)
        {
            context.Result = new BadRequestObjectResult($"Empty {nameof(token)}");
            return;
        }

        var authHeader = AuthenticationHeaderValue.Parse(token.ToString()).Parameter;
        if (authHeader is null)
        {
            context.Result = new BadRequestObjectResult($"Incorrect {nameof(authHeader)}");
            return;
        }

        await next();
    }
}