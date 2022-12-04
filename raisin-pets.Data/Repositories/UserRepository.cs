namespace raisin_pets.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public UserRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<User>> GetByGoogleNameIdentifierAsync(string identifier)
        => (await _context
                .Users
                .Where(user => user.GoogleNameIdentifier == identifier)
                .FirstOrDefaultAsync())
            .ToResponse();

    public async Task<Response<User>> AddAsync(CreateUserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        user.Avatar = new Uri(AvatarHelper.GetAvatar(user.FirstName, user.LastName));

        await _context.AddAsync(user);
        await _context.SaveChangesAsync();

        return user.ToResponse();
    }
}