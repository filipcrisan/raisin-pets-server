using System.Globalization;
using raisin_pets.Common.Dtos.Exercise;

namespace raisin_pets.Services;

public class ExerciseService : IExerciseService
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IPetValidationService _petValidationService;
    private readonly IMapper _mapper;

    public ExerciseService(
        IExerciseRepository exerciseRepository,
        IPetValidationService petValidationService,
        IMapper mapper)
    {
        _exerciseRepository = exerciseRepository;
        _petValidationService = petValidationService;
        _mapper = mapper;
    }

    public async Task<Response<List<ExerciseDto>>> GetAllAsync(int userId, int petId)
    {
        if (!await _petValidationService.IsUserOwnerOfPetAsync(userId, petId))
        {
            return new Response<List<ExerciseDto>>().Failed;
        }

        var response = await _exerciseRepository.GetAllAsync(petId);

        return _mapper.Map<Response<List<ExerciseDto>>>(response);
    }

    public async Task<Response<ExerciseDto>> AddAsync(int userId, CreateExerciseDto exerciseDto)
    {
        if (!await _petValidationService.IsUserOwnerOfPetAsync(userId, exerciseDto.PetId))
        {
            return new Response<ExerciseDto>().Failed;
        }
        
        exerciseDto.Name = DateOnly.FromDateTime(DateTime.UtcNow).ToString();
        
        var response = await _exerciseRepository.AddAsync(exerciseDto);

        return _mapper.Map<Response<ExerciseDto>>(response);
    }
}