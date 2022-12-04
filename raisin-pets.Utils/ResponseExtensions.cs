namespace raisin_pets.Utils;

public static class ResponseExtensions
{
    public static Response<T> ToResponse<T>(this T entity)
    {
        return new Response<T>
        {
            Status = entity != null ? ResponseStatus.Success : ResponseStatus.Failed,
            Payload = entity
        };
    }
}