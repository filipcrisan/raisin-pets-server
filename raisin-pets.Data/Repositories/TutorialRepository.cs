using raisin_pets.Common.Dtos.Pet;
using raisin_pets.Common.Enums;
using raisin_pets.Interfaces.ITutorial;

namespace raisin_pets.Data.Repositories;

public class TutorialRepository : ITutorialRepository
{
    private readonly DataContext _dataContext;

    public TutorialRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Response<List<Tutorial>>> GetTutorialsByCategoryAsync(TutorialListPetDto petDto, TutorialCategory category)
        => (await _dataContext.Tutorials
            .Where(x => x.Category == category && x.Size == petDto.Size && x.Species == petDto.Species &&
                        DateTime.Now.AddYears(-x.MinAgeInYears).CompareTo(petDto.DateOfBirth.ToDateTime(TimeOnly.MinValue)) >= 0 &&
                        DateTime.Now.AddYears(-x.MaxAgeInYears).CompareTo(petDto.DateOfBirth.ToDateTime(TimeOnly.MinValue)) <= 0)
            .ToListAsync()).ToResponse();
}