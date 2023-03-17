using raisin_pets.Common.Dtos.Exercise;
using raisin_pets.Common.ViewModels.Exercise;

namespace raisin_pets.Common.MapperProfiles;

public class ExerciseProfile : Profile
{
    public ExerciseProfile()
    {
        CreateMap<Exercise, ExerciseDto>();
        CreateMap<ExerciseDto, ExerciseViewModel>();
        CreateMap<Response<List<Exercise>>, Response<List<ExerciseDto>>>();
        CreateMap<Response<List<ExerciseDto>>, Response<List<ExerciseViewModel>>>();
        CreateMap<CreateExerciseViewModel, CreateExerciseDto>();
        CreateMap<CreateExerciseDto, Exercise>();
        CreateMap<Response<Exercise>, Response<ExerciseDto>>();
        
        CreateMap<Checkpoint, CheckpointDto>();
        CreateMap<CheckpointDto, CheckpointViewModel>();
        CreateMap<CreateCheckpointViewModel, CreateCheckpointDto>();
        CreateMap<CreateCheckpointDto, Checkpoint>();
    }
}