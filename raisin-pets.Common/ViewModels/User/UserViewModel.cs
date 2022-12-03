namespace raisin_pets.Common.ViewModels.User;

public class UserViewModel
{
    public int Id { get; set; }
    public string GoogleNameIdentifier { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public Uri Avatar { get; set; }
}