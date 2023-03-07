namespace raisin_pets.Common.ViewModels.Tutorial;

public class TutorialViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public TutorialCategory Category { get; set; }
    public Species Species { get; set; }
    public Size Size { get; set; }
    public int MinAgeInYears { get; set; }
    public int MaxAgeInYears { get; set; }
    public string Frequency { get; set; }
    public string Content { get; set; }
}