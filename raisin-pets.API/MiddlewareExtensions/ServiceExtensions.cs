namespace raisin_pets.MiddlewareExtensions;

public static class ServiceExtensions
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.DbOptions(configuration);
    }
    
    private static void DbOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DbConnection")));
    }
}