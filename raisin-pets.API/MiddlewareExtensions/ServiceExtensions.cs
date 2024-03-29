using raisin_pets.Interfaces.IExercise;
using raisin_pets.Interfaces.IPet;
using raisin_pets.Interfaces.IReminder;
using raisin_pets.Interfaces.ITutorial;

namespace raisin_pets.MiddlewareExtensions;

public static class ServiceExtensions
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(User));

        services.AddMemoryCache();

        services.DbOptions(configuration);

        services.InjectRepositories();
        services.InjectServices();

        services.AddAuthentication("GoogleAuthorizationHandler")
            .AddScheme<AuthenticationSchemeOptions, GoogleAuthenticationHandler>("GoogleAuthorizationHandler", null);

        services.SwaggerSetup();
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
        services.AddTransient<IPetRepository, PetRepository>();
        services.AddTransient<ITutorialRepository, TutorialRepository>();
        services.AddTransient<IExerciseRepository, ExerciseRepository>();
        services.AddTransient<IReminderRepository, ReminderRepository>();
    }

    private static void InjectServices(this IServiceCollection services)
    {
        services.AddTransient<ValidTokenFilter>();
        services.AddTransient<UniqueGoogleIdentifierFilter>();

        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IPetService, PetService>();
        services.AddTransient<IPetValidationService, PetValidationService>();
        services.AddTransient<ITutorialService, TutorialService>();
        services.AddTransient<IExerciseService, ExerciseService>();
        services.AddTransient<IReminderService, ReminderService>();
    }

    private static void SwaggerSetup(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "raisin' pets API",
                Version = "v1"
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT with Bearer into field",
                Name = "Bearer",
                BearerFormat = "JWT",
                Scheme = "bearer",
                Type = SecuritySchemeType.Http
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }

    #endregion
}