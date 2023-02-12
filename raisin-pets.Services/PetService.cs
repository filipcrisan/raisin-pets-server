using raisin_pets.Common.Dtos.Pet;
using raisin_pets.Common.Entities;
using raisin_pets.Common.Enums;
using raisin_pets.Interfaces.IPet;

namespace raisin_pets.Services;

public class PetService : IPetService
{
    private readonly IPetRepository _petRepository;
    private readonly IMapper _mapper;

    public PetService(IPetRepository petRepository, IMapper mapper)
    {
        _petRepository = petRepository;
        _mapper = mapper;
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
        #region Validation

        var existingPetResponse = await TryGetPetByIdAsync(petDto.Id);
        if (existingPetResponse.Status == ResponseStatus.Failed)
        {
            return new Response<PetDto>().Failed;
        }

        var existingPet = existingPetResponse.Payload;
        if (existingPet.UserId != petDto.UserId)
        {
            return new Response<PetDto>().Failed;
        }

        #endregion
        
        var response = await _petRepository.EditAsync(petDto);
        
        return _mapper.Map<Response<PetDto>>(response);
    }

    public async Task<Response<PetDto>> DeleteAsync(int id, int userId)
    {
        #region Validation

        var existingPetResponse = await TryGetPetByIdAsync(id);
        if (existingPetResponse.Status == ResponseStatus.Failed)
        {
            return new Response<PetDto>().Failed;
        }

        var existingPet = existingPetResponse.Payload;
        if (existingPet.UserId != userId)
        {
            return new Response<PetDto>().Failed;
        }

        #endregion
        
        var response = await _petRepository.DeleteAsync(id);
        
        return _mapper.Map<Response<PetDto>>(response);
    }

    #region Private methods

    private async Task<Response<Pet>> TryGetPetByIdAsync(int id) => await _petRepository.GetByIdAsync(id);

    #endregion
}