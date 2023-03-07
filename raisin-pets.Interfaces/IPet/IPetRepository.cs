namespace raisin_pets.Interfaces.IPet;

public interface IPetRepository
{
    Task<Response<List<Pet>>> GetAllAsync(int userId);
    Task<Response<Pet>> GetByIdAsync(int id);
    Task<Response<Pet>> AddAsync(CreatePetDto petDto);
    Task<Response<Pet>> EditAsync(EditPetDto petDto);
    Task<Response<Pet>> DeleteAsync(int id);
}