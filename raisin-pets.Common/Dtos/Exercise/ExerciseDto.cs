namespace raisin_pets.Common.Dtos.Exercise;

public class ExerciseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int PetId { get; set; }
    public double TotalDistance { get; set; }
    public double AverageSpeed { get; set; }
    public List<CheckpointDto> Checkpoints { get; set; }
}