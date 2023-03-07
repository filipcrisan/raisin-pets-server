namespace raisin_pets.Services;

public class PetService : IPetService
{
    private readonly IPetRepository _petRepository;
    private readonly IMapper _mapper;
    private readonly IPetValidationService _petValidationService;

    public PetService(IPetRepository petRepository, IMapper mapper, IPetValidationService petValidationService)
    {
        _petRepository = petRepository;
        _mapper = mapper;
        _petValidationService = petValidationService;
    }

    public async Task<Response<List<PetDto>>> GetAllAsync(int userId)
    {
        var response = await _petRepository.GetAllAsync(userId);

        return _mapper.Map<Response<List<PetDto>>>(response);
    }

    public async Task<Response<PetDto>> AddAsync(CreatePetDto petDto)
    {
        var response = await _petRepository.AddAsync(petDto);

        return _mapper.Map<Response<PetDto>>(response);
    }

    public async Task<Response<PetDto>> EditAsync(EditPetDto petDto)
    {
        if (!await _petValidationService.IsUserOwnerOfPetAsync(petDto.UserId, petDto.Id))
        {
            return new Response<PetDto>().Failed;
        }
        
        var response = await _petRepository.EditAsync(petDto);
        
        return _mapper.Map<Response<PetDto>>(response);
    }

    public async Task<Response<PetDto>> DeleteAsync(int id, int userId)
    {
        if (!await _petValidationService.IsUserOwnerOfPetAsync(userId, id))
        {
            return new Response<PetDto>().Failed;
        }
        
        var response = await _petRepository.DeleteAsync(id);
        
        return _mapper.Map<Response<PetDto>>(response);
    }
}