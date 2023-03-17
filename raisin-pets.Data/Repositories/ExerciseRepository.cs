using raisin_pets.Common.Dtos.Exercise;

namespace raisin_pets.Data.Repositories;

public class ExerciseRepository : IExerciseRepository
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;

    public ExerciseRepository(DataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public async Task<Response<List<Exercise>>> GetAllAsync(int petId) =>
        (await _dataContext.Exercises
            .Where(x => x.PetId == petId)
            .Include(x => x.Checkpoints)
            .ToListAsync()
        ).ToResponse();

    public async Task<Response<Exercise>> AddAsync(CreateExerciseDto exerciseDto)
    {
        var exercise = _mapper.Map<Exercise>(exerciseDto);

        await _dataContext.AddAsync(exercise);
        await _dataContext.SaveChangesAsync();

        return exercise.ToResponse();
    }
}