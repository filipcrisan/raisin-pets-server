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
        CreateMap<CreatePetViewModel, CreatePetDto>()
            // this is necessary because DateOnly is not yet fully supported by the binder
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(x => DateOnly.FromDateTime(x.DateOfBirth)));
        CreateMap<CreatePetDto, Pet>();
        CreateMap<Response<Pet>, Response<PetDto>>();
        CreateMap<EditPetViewModel, EditPetDto>()
            // this is necessary because DateOnly is not yet fully supported by the binder
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(x => DateOnly.FromDateTime(x.DateOfBirth)));
        CreateMap<Pet, TutorialListPetDto>();
    }
}