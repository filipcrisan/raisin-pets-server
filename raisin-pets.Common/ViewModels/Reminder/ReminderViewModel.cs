namespace raisin_pets.Common.ViewModels.Reminder;

public class ReminderViewModel
{
    public int Id { get; set; }
    public int PetId { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public int Hours { get; set; }
    public int Minutes { get; set; }
    public bool Enabled { get; set; }
}