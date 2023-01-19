namespace raisin_pets.Common.Entities;

public class User
{
    public int Id { get; set; }
    public string GoogleNameIdentifier { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public Uri Avatar { get; set; }
    public List<Pet> Pets { get; set; } = new();
}