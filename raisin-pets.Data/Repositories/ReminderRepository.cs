using raisin_pets.Interfaces.IReminder;

namespace raisin_pets.Data.Repositories;

public class ReminderRepository : IReminderRepository
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;

    public ReminderRepository(DataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }
    
    public async Task<Response<List<Reminder>>> GetAllAsync(int petId) =>
        (await _dataContext.Reminders
            .Where(x => x.PetId == petId)
            .ToListAsync()
        ).ToResponse();
}