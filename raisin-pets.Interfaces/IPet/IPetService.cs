namespace raisin_pets.Interfaces.IPet;

public interface IPetService
{
    Task<Response<List<PetDto>>> GetAllAsync(int userId);
    Task<Response<PetDto>> AddAsync(CreatePetDto petDto);
    Task<Response<PetDto>> EditAsync(EditPetDto petDto);
    Task<Response<PetDto>> DeleteAsync(int id, int userId);
}