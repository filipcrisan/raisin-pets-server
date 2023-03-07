namespace raisin_pets.Interfaces.ITutorial;

public interface ITutorialRepository
{
    Task<Response<List<Tutorial>>> GetTutorialsByCategoryAsync(TutorialListPetDto petDto, TutorialCategory category);
}