using raisin_pets.Common.Dtos.Reminder;
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

    public async Task<Response<Reminder>> AddAsync(CreateReminderDto reminderDto)
    {
        var reminder = _mapper.Map<Reminder>(reminderDto);

        await _dataContext.AddAsync(reminder);
        await _dataContext.SaveChangesAsync();

        return reminder.ToResponse();
    }

    public async Task<Response<Reminder>> DeleteAsync(int id)
    {
        var reminder = await _dataContext.Reminders.FirstOrDefaultAsync(x => x.Id == id);
        if (reminder is null)
        {
            return new Response<Reminder>().Failed;
        }

        _dataContext.Remove(reminder);
        await _dataContext.SaveChangesAsync();

        return reminder.ToResponse();
    }
}