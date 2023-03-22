using raisin_pets.Common.Dtos.Reminder;

namespace raisin_pets.Interfaces.IReminder;

public interface IReminderService
{
    Task<Response<List<ReminderDto>>> GetAllAsync(int userId, int petId);
}