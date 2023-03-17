namespace raisin_pets.Common.ViewModels.Exercise;

public class CreateExerciseViewModel
{
    public string Name { get; set; }
    public int PetId { get; set; }
    public List<CreateCheckpointViewModel> Checkpoints { get; set; }
}