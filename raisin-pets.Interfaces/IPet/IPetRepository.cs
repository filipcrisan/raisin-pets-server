namespace raisin_pets.Interfaces.IPet;

public interface IPetRepository
{
    Task<Response<List<Pet>>> GetAllAsync(int userId);
}