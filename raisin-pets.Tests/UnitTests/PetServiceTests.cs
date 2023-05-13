namespace raisin_pets.Tests.UnitTests;

public class PetServiceTests
{
    private readonly IMapper _mapper;

    public PetServiceTests()
    {
        var mappingConfig = new MapperConfiguration(x =>
        {
            x.AddProfile(new PetProfile());
        });
        _mapper = mappingConfig.CreateMapper();
    }

    [Fact]
    public async Task GetAllPets()
    {
        // Arrange
        var petRepositoryMock = new Mock<IPetRepository>();
        petRepositoryMock
            .Setup(x => x.GetAllAsync(1))
            .ReturnsAsync(new Response<List<Pet>>
            {
                Payload = new List<Pet> { new() { Name = "Pet 1", UserId = 1 }, new() { Name = "Pet 2", UserId = 1 } },
                Status = ResponseStatus.Success
            });
        var petValidationService = new PetValidationService(petRepositoryMock.Object);
        var petService = new PetService(petRepositoryMock.Object, _mapper, petValidationService);

        // Act
        var response = await petService.GetAllAsync(1);

        // Assert
        petRepositoryMock.Verify(x => x.GetAllAsync(It.IsAny<int>()), Times.Once);
        Assert.Equal(ResponseStatus.Success, response.Status);
        Assert.Equal("Pet 1", response.Payload[0].Name);
        Assert.Equal(1, response.Payload[1].UserId);
    }

    [Fact]
    public async Task AddPet()
    {
        // Arrange
        var createPetDto = new CreatePetDto { Name = "Pet", UserId = 1 };
        
        var petRepositoryMock = new Mock<IPetRepository>();
        petRepositoryMock
            .Setup(x => x.AddAsync(createPetDto))
            .ReturnsAsync(new Response<Pet>
            {
                Payload = new Pet { Name = "Pet", UserId = 1 }, Status = ResponseStatus.Success
            });
        var petValidationService = new PetValidationService(petRepositoryMock.Object);
        var petService = new PetService(petRepositoryMock.Object, _mapper, petValidationService);

        // Act
        var response = await petService.AddAsync(createPetDto);

        // Assert
        petRepositoryMock.Verify(x => x.AddAsync(It.IsAny<CreatePetDto>()), Times.Once);
        Assert.Equal(ResponseStatus.Success, response.Status);
        Assert.Equal("Pet", response.Payload.Name);
        Assert.Equal(1, response.Payload.UserId);
    }
    
    [Fact]
    public async Task EditPet()
    {
        // Arrange
        var editPetDto = new EditPetDto { Id = 1, Name = "Pet with edited name", UserId = 1 };
        
        var petRepositoryMock = new Mock<IPetRepository>();
        petRepositoryMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(new Response<Pet>
            {
                Payload = new Pet { Id = 1, Name = "Pet", UserId = 1 }, Status = ResponseStatus.Success
            });
        petRepositoryMock
            .Setup(x => x.EditAsync(editPetDto))
            .ReturnsAsync(new Response<Pet>
            {
                Payload = new Pet { Id = 1, Name = "Pet with edited name", UserId = 1 }, Status = ResponseStatus.Success
            });
        var petValidationService = new PetValidationService(petRepositoryMock.Object);
        var petService = new PetService(petRepositoryMock.Object, _mapper, petValidationService);

        // Act
        var response = await petService.EditAsync(editPetDto);

        // Assert
        petRepositoryMock.Verify(x => x.EditAsync(It.IsAny<EditPetDto>()), Times.Once);
        Assert.Equal(ResponseStatus.Success, response.Status);
        Assert.Equal("Pet with edited name", response.Payload.Name);
        Assert.Equal(1, response.Payload.UserId);
    }
    
    [Fact]
    public async Task EditPet_NotOwned()
    {
        // Arrange
        var editPetDto = new EditPetDto { Id = 1, Name = "Pet with edited name", UserId = 1 };
        
        var petRepositoryMock = new Mock<IPetRepository>();
        petRepositoryMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(new Response<Pet>
            {
                Payload = new Pet { Id = 1, Name = "Pet", UserId = 2 }, Status = ResponseStatus.Success
            });
        petRepositoryMock
            .Setup(x => x.EditAsync(editPetDto))
            .ReturnsAsync(new Response<Pet>
            {
                Payload = new Pet { Id = 1, Name = "Pet with edited name", UserId = 1 }, Status = ResponseStatus.Success
            });
        var petValidationService = new PetValidationService(petRepositoryMock.Object);
        var petService = new PetService(petRepositoryMock.Object, _mapper, petValidationService);

        // Act
        var response = await petService.EditAsync(editPetDto);

        // Assert
        petRepositoryMock.Verify(x => x.EditAsync(It.IsAny<EditPetDto>()), Times.Never);
        Assert.Equal(ResponseStatus.Failed, response.Status);
    }
}