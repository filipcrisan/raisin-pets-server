using raisin_pets.Interfaces.IPet;

namespace raisin_pets.Data.Repositories;

public class PetRepository : IPetRepository
{
    private readonly DataContext _dataContext;

    public PetRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Response<List<Pet>>> GetAllAsync(int userId)
        => (await _dataContext.Pets.Where(x => x.UserId == userId).ToListAsync()).ToResponse();
}