namespace raisin_pets.Interfaces.IExercise;

public interface IExerciseService
{
    Task<Response<List<ExerciseDto>>> GetAllAsync(int userId, int petId);
    Task<Response<ExerciseDto>> AddAsync(int userId, CreateExerciseDto exerciseDto);
}