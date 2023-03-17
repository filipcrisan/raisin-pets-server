namespace raisin_pets.Common.Entities;

public class Exercise
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int PetId { get; set; }
    public Pet Pet { get; set; }
    public List<Checkpoint> Checkpoints { get; set; }
}