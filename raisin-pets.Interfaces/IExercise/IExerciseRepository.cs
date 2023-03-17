namespace raisin_pets.Interfaces.IExercise;

public interface IExerciseRepository
{
    Task<Response<List<Exercise>>> GetAllAsync(int petId);
    Task<Response<Exercise>> AddAsync(CreateExerciseDto exerciseDto);
}