namespace raisin_pets.Common.Dtos.User;

public class CreateUserDto
{
    public string GoogleNameIdentifier { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}