namespace raisin_pets.Common.Entities;

public class Pet
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string AvatarInBase64 { get; set; }
    public Species Species { get; set; }
    public Size Size { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public List<Exercise> Exercises { get; set; }
}