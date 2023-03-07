namespace raisin_pets.Interfaces.IPet;

public interface IPetValidationService
{
    Task<bool> IsUserOwnerOfPetAsync(int userId, int petId);
}