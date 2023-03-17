using raisin_pets.Common.Dtos.Exercise;
using raisin_pets.Common.ViewModels.Exercise;
using raisin_pets.Interfaces.IExercise;

namespace raisin_pets.Controllers;

[ApiController]
[Route("api/pets/{petId:int}/exercises")]
[Authorize]
public class ExerciseController : ControllerBase
{
    private readonly IExerciseService _exerciseService;
    private readonly IMapper _mapper;

    public ExerciseController(IMapper mapper, IExerciseService exerciseService)
    {
        _mapper = mapper;
        _exerciseService = exerciseService;
    }

    [HttpGet]
    [Route("list")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<ExerciseViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllAsync([FromRoute] int petId)
    {
        var userId = ((UserDto)HttpContext.Items["User"])?.Id;
        if (!userId.HasValue)
        {
            return Unauthorized();
        }
        
        var response = await _exerciseService.GetAllAsync(userId.Value, petId);
        if (response.Status == ResponseStatus.Failed)
        {
            return BadRequest();
        }

        return Ok(_mapper.Map<List<ExerciseViewModel>>(response.Payload));
    }
    
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ExerciseViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddAsync([FromRoute] int petId, [FromBody] CreateExerciseViewModel exerciseViewModel)
    {
        var userId = ((UserDto)HttpContext.Items["User"])?.Id;
        if (!userId.HasValue)
        {
            return Unauthorized();
        }

        if (petId != exerciseViewModel.PetId)
        {
            return BadRequest();
        }

        var exerciseDto = _mapper.Map<CreateExerciseDto>(exerciseViewModel);
        
        var response = await _exerciseService.AddAsync(userId.Value, exerciseDto);
        if (response.Status == ResponseStatus.Failed)
        {
            return BadRequest();
        }

        return Ok(_mapper.Map<ExerciseViewModel>(response.Payload));
    }
}