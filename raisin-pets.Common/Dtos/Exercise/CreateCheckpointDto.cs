namespace raisin_pets.Common.Dtos.Exercise;

public class CreateCheckpointDto
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double Speed { get; set; }
    public DateTime Timestamp { get; set; }
}