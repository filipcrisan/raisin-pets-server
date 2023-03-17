namespace raisin_pets.Common.Dtos.Exercise;

public class CreateExerciseDto
{
    public string Name { get; set; }
    public int PetId { get; set; }
    public List<CreateCheckpointDto> Checkpoints { get; set; }
}