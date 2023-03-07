namespace raisin_pets.Services;

public class PetValidationService : IPetValidationService
{
    private readonly IPetRepository _petRepository;

    public PetValidationService(IPetRepository petRepository)
    {
        _petRepository = petRepository;
    }

    public async Task<bool> IsUserOwnerOfPetAsync(int userId, int petId)
    {
        var existingPetResponse = await TryGetPetByIdAsync(petId);
        if (existingPetResponse.Status == ResponseStatus.Failed)
        {
            return false;
        }
        
        return existingPetResponse.Payload.UserId == userId;
    }
    
    #region Private methods

    private async Task<Response<Pet>> TryGetPetByIdAsync(int id) => await _petRepository.GetByIdAsync(id);

    #endregion
}