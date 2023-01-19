using raisin_pets.Common.Dtos.Pet;

namespace raisin_pets.Interfaces.IPet;

public interface IPetService
{
    Task<Response<List<PetDto>>> GetAllAsync(int userId);
    Task<Response<PetDto>> AddAsync(CreatePetDto petDto);
}