namespace raisin_pets.Common.Models;

public class Response<T>
{
    public ResponseStatus Status { get; set; }
    public T Payload { get; set; }

    public Response<T> Failed => new()
    {
        Status = ResponseStatus.Failed,
        Payload = default
    };
}