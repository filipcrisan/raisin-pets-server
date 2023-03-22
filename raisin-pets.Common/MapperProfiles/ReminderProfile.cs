using raisin_pets.Common.Dtos.Reminder;
using raisin_pets.Common.ViewModels.Reminder;

namespace raisin_pets.Common.MapperProfiles;

public class ReminderProfile : Profile
{
    public ReminderProfile()
    {
        CreateMap<Reminder, ReminderDto>();
        CreateMap<ReminderDto, ReminderViewModel>();
        CreateMap<Response<List<Reminder>>, Response<List<ReminderDto>>>();
        CreateMap<Response<List<ReminderDto>>, Response<List<ReminderViewModel>>>();
        CreateMap<CreateReminderViewModel, CreateReminderDto>();
        CreateMap<CreateReminderDto, Reminder>();
        CreateMap<Response<Reminder>, Response<ReminderDto>>();
    }
}