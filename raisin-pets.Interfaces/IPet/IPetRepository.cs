using raisin_pets.Common.Dtos.Pet;

namespace raisin_pets.Interfaces.IPet;

public interface IPetRepository
{
    Task<Response<List<Pet>>> GetAllAsync(int userId);
    Task<Response<Pet>> AddAsync(CreatePetDto petDto);
}