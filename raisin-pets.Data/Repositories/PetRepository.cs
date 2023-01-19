using raisin_pets.Common.Dtos.Pet;
using raisin_pets.Interfaces.IPet;

namespace raisin_pets.Data.Repositories;

public class PetRepository : IPetRepository
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;

    public PetRepository(DataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public async Task<Response<List<Pet>>> GetAllAsync(int userId)
        => (await _dataContext.Pets.Where(x => x.UserId == userId).ToListAsync()).ToResponse();

    public async Task<Response<Pet>> AddAsync(CreatePetDto petDto)
    {
        var pet = _mapper.Map<Pet>(petDto);

        await _dataContext.AddAsync(pet);
        await _dataContext.SaveChangesAsync();

        return pet.ToResponse();
    }
}