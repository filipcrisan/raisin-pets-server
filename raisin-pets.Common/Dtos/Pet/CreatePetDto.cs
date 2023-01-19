namespace raisin_pets.Common.Dtos.Pet;

public class CreatePetDto
{
    public string Name { get; set; }
    public string AvatarUrl { get; set; }
    public Species Species { get; set; }
    public Size Size { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public int UserId { get; set; }
}