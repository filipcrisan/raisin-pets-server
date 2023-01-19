using raisin_pets.Common.Dtos.Pet;
using raisin_pets.Common.ViewModels.Pet;
using raisin_pets.Interfaces.IPet;

namespace raisin_pets.Controllers;

[ApiController]
[Route("api/pets")]
[Authorize]
public class PetController : ControllerBase
{
    private readonly IPetService _petService;
    private readonly IMapper _mapper;

    public PetController(IPetService petService, IMapper mapper)
    {
        _petService = petService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("list")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<PetViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllAsync()
    {
        var userId = ((UserDto)HttpContext.Items["User"])?.Id;
        if (!userId.HasValue)
            return Unauthorized();

        var response = await _petService.GetAllAsync(userId.Value);

        return Ok(_mapper.Map<List<PetViewModel>>(response.Payload));
    }
    
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PetViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddAsync([FromBody] CreatePetViewModel petViewModel)
    {
        var userId = ((UserDto)HttpContext.Items["User"])?.Id;
        if (!userId.HasValue)
            return Unauthorized();

        var petDto = _mapper.Map<CreatePetDto>(petViewModel);
        petDto.UserId = userId.Value;
        
        var response = await _petService.AddAsync(petDto);

        return Ok(_mapper.Map<PetViewModel>(response.Payload));
    }
}