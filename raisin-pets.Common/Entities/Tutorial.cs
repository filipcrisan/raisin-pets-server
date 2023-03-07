namespace raisin_pets.Common.Entities;

public class Tutorial
{
    public int Id { get; set; }
    public string Name { get; set; }
    public TutorialCategory Category { get; set; }
    public Species Species { get; set; }
    public Size Size { get; set; }
    public int MinAgeInYears { get; set; }
    public int MaxAgeInYears { get; set; }
    public string Frequency { get; set; }
}