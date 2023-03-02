namespace raisin_pets.Common.ViewModels.Pet;

public class CreatePetViewModel
{
    public string Name { get; set; }
    public string AvatarInBase64 { get; set; }
    public Species Species { get; set; }
    public Size Size { get; set; }
    public DateTime DateOfBirth { get; set; }
}