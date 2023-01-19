namespace raisin_pets.Common.Dtos.Pet;

public class PetDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string AvatarUrl { get; set; }
    public Species Species { get; set; }
    public Size Size { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public int UserId { get; set; }
}