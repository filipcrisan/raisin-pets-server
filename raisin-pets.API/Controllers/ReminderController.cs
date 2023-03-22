using raisin_pets.Common.Dtos.Reminder;
using raisin_pets.Common.ViewModels.Reminder;
using raisin_pets.Interfaces.IReminder;

namespace raisin_pets.Controllers;

[ApiController]
[Route("api/pets/{petId:int}/reminders")]
[Authorize]
public class ReminderController : ControllerBase
{
    private readonly IReminderService _reminderService;
    private readonly IMapper _mapper;

    public ReminderController(IMapper mapper, IReminderService reminderService)
    {
        _mapper = mapper;
        _reminderService = reminderService;
    }

    [HttpGet]
    [Route("list")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<ReminderViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllAsync([FromRoute] int petId)
    {
        var userId = ((UserDto)HttpContext.Items["User"])?.Id;
        if (!userId.HasValue)
        {
            return Unauthorized();
        }
        
        var response = await _reminderService.GetAllAsync(userId.Value, petId);
        if (response.Status == ResponseStatus.Failed)
        {
            return BadRequest();
        }

        return Ok(_mapper.Map<List<ReminderViewModel>>(response.Payload));
    }
    
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ReminderViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddAsync([FromRoute] int petId, [FromBody] CreateReminderViewModel reminderViewModel)
    {
        var userId = ((UserDto)HttpContext.Items["User"])?.Id;
        if (!userId.HasValue)
        {
            return Unauthorized();
        }

        if (petId != reminderViewModel.PetId)
        {
            return BadRequest();
        }

        var reminderDto = _mapper.Map<CreateReminderDto>(reminderViewModel);
        
        var response = await _reminderService.AddAsync(userId.Value, reminderDto);
        if (response.Status == ResponseStatus.Failed)
        {
            return BadRequest();
        }

        return Ok(_mapper.Map<ReminderViewModel>(response.Payload));
    }
}