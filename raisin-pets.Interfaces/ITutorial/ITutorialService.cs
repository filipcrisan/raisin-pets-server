namespace raisin_pets.Interfaces.ITutorial;

public interface ITutorialService
{
    Task<Response<List<TutorialDto>>> GetTutorialsByCategoryAsync(int userId, int petId, TutorialCategory tutorialCategory);
}