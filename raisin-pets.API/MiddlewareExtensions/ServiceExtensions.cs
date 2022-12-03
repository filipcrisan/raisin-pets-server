namespace raisin_pets.MiddlewareExtensions;

public static class ServiceExtensions
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(User));

        services.DbOptions(configuration);

        services.InjectRepositories();
        services.InjectServices();
    }

    #region Private methods

    private static void DbOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DbConnection")));
    }

    private static void InjectRepositories(this IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
    }

    private static void InjectServices(this IServiceCollection services)
    {
        services.AddTransient<IUserService, UserService>();
    }

    #endregion
}