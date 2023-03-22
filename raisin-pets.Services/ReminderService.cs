using raisin_pets.Common.Dtos.Reminder;
using raisin_pets.Interfaces.IReminder;

namespace raisin_pets.Services;

public class ReminderService : IReminderService
{
    private readonly IReminderRepository _reminderRepository;
    private readonly IPetValidationService _petValidationService;
    private readonly IMapper _mapper;

    public ReminderService(
        IReminderRepository reminderRepository,
        IPetValidationService petValidationService,
        IMapper mapper)
    {
        _reminderRepository = reminderRepository;
        _petValidationService = petValidationService;
        _mapper = mapper;
    }
    
    public async Task<Response<List<ReminderDto>>> GetAllAsync(int userId, int petId)
    {
        if (!await _petValidationService.IsUserOwnerOfPetAsync(userId, petId))
        {
            return new Response<List<ReminderDto>>().Failed;
        }

        var response = await _reminderRepository.GetAllAsync(petId);

        return _mapper.Map<Response<List<ReminderDto>>>(response);
    }
}