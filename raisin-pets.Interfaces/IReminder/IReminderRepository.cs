namespace raisin_pets.Interfaces.IReminder;

public interface IReminderRepository
{
    Task<Response<List<Reminder>>> GetAllAsync(int petId);
}