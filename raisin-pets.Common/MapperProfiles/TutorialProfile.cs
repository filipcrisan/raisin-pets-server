using raisin_pets.Common.Dtos.Tutorial;
using raisin_pets.Common.ViewModels.Tutorial;

namespace raisin_pets.Common.MapperProfiles;

public class TutorialProfile : Profile
{
    public TutorialProfile()
    {
        CreateMap<Tutorial, TutorialDto>();
        CreateMap<TutorialDto, TutorialViewModel>();
        CreateMap<Response<List<Tutorial>>, Response<List<TutorialDto>>>();
        CreateMap<Response<List<TutorialDto>>, Response<List<TutorialViewModel>>>();
    }
}