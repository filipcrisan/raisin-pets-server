namespace raisin_pets.Services;

public class TutorialService : ITutorialService
{
    private readonly IPetRepository _petRepository;
    private readonly IPetValidationService _petValidationService;
    private readonly IMapper _mapper;
    private readonly ITutorialRepository _tutorialRepository;

    public TutorialService(
        IPetRepository petRepository,
        IPetValidationService petValidationService,
        IMapper mapper,
        ITutorialRepository tutorialRepository)
    {
        _petRepository = petRepository;
        _petValidationService = petValidationService;
        _mapper = mapper;
        _tutorialRepository = tutorialRepository;
    }

    public async Task<Response<List<TutorialDto>>> GetTutorialsByCategoryAsync(int userId, int petId, TutorialCategory tutorialCategory)
    {
        if (!await _petValidationService.IsUserOwnerOfPetAsync(userId, petId))
        {
            return new Response<List<TutorialDto>>().Failed;
        }

        var pet = await _petRepository.GetByIdAsync(petId);
        var petDto = _mapper.Map<TutorialListPetDto>(pet.Payload);

        var response = await _tutorialRepository.GetTutorialsByCategoryAsync(petDto, tutorialCategory);

        return _mapper.Map<Response<List<TutorialDto>>>(response);
    }
}