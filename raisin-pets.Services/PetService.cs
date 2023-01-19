using raisin_pets.Common.Dtos.Pet;
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
}