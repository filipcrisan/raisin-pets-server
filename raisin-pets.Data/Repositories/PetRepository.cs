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

    public async Task<Response<Pet>> GetByIdAsync(int id) =>
        (await _dataContext.Pets.FirstOrDefaultAsync(x => x.Id == id)).ToResponse();

    public async Task<Response<Pet>> AddAsync(CreatePetDto petDto)
    {
        var pet = _mapper.Map<Pet>(petDto);

        await _dataContext.AddAsync(pet);
        await _dataContext.SaveChangesAsync();

        return pet.ToResponse();
    }

    public async Task<Response<Pet>> EditAsync(EditPetDto petDto)
    {
        var pet = await _dataContext.Pets.FirstOrDefaultAsync(x => x.Id == petDto.Id);
        if (pet is null)
        {
            return new Response<Pet>().Failed;
        }
        
        _dataContext.Entry(pet).CurrentValues.SetValues(petDto);
        await _dataContext.SaveChangesAsync();

        return pet.ToResponse();
    }

    public async Task<Response<Pet>> DeleteAsync(int id)
    {
        var pet = await _dataContext.Pets.FirstOrDefaultAsync(x => x.Id == id);
        if (pet is null)
        {
            return new Response<Pet>().Failed;
        }

        _dataContext.Remove(pet);
        await _dataContext.SaveChangesAsync();

        return pet.ToResponse();
    }
}