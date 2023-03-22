using raisin_pets.Common.Dtos.Reminder;

namespace raisin_pets.Interfaces.IReminder;

public interface IReminderRepository
{
    Task<Response<List<Reminder>>> GetAllAsync(int petId);
    Task<Response<Reminder>> AddAsync(CreateReminderDto reminderDto);
    Task<Response<Reminder>> DeleteAsync(int id);
}