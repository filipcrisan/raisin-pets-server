namespace raisin_pets.Common.Dtos.Exercise;

public class CheckpointDto
{
    public int Id { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double Speed { get; set; }
    public DateTime Timestamp { get; set; }
}