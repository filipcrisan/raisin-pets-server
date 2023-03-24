namespace raisin_pets.Interfaces.IExercise;

public interface IExerciseService
{
    Task<Response<List<ExerciseDto>>> GetAllAsync(int userId, int petId);
    Task<Response<ExerciseDto>> AddAsync(int userId, CreateExerciseDto exerciseDto);
    Task<Response<ExerciseDto>> DeleteAsync(int userId, int petId, int exerciseId);
}