namespace raisin_pets.Common.ViewModels.Exercise;

public class ExerciseViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int PetId { get; set; }
    public double TotalDistance { get; set; }
    public double AverageSpeed { get; set; }
    public List<CheckpointViewModel> Checkpoints { get; set; }
}