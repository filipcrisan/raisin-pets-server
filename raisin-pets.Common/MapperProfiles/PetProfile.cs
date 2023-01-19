using raisin_pets.Common.Dtos.Pet;
using raisin_pets.Common.ViewModels.Pet;

namespace raisin_pets.Common.MapperProfiles;

public class PetProfile : Profile
{
    public PetProfile()
    {
        CreateMap<Pet, PetDto>();
        CreateMap<PetDto, PetViewModel>();
        CreateMap<Response<List<Pet>>, Response<List<PetDto>>>();
        CreateMap<Response<List<PetDto>>, Response<List<PetViewModel>>>();
    }
}