using Store.Application.Extensions;
using Store.Auth.Extensions;
using Store.Persistence.Data;
using Store.Persistence.Extensions;
using Store.WebApi.Extensions;
using Store.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Configuration
ConfigureAppSettings(builder);

// Services Registration
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

// Application Pipeline Configuration
ConfigureApplicationPipeline(app);

app.Run();

void ConfigureAppSettings(WebApplicationBuilder builder)
{
    // Load configuration from environment variables and other sources
    builder.Configuration.AddEnvironmentVariables();
}

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    // Add framework services
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGenConfiguration();
    services.AddCorsConfiguration();

    // Add application services
    services.AddPersistence(configuration);
    services.AddRepositories();
    services.AddTransactionService();
    services.AddMapper();
    services.AddMediatr();
    services.AddValidatorBehavior();
    services.AddLoggerBehavior();

    // Add authentication and authorization services
    services.AddJwtBearerConfiguration(configuration);
    services.AddJwtBearerAuthentication(configuration);
    services.AddAuthService();
    services.AddAuthContext();
    services.AddAuthorizationPolicy();

    // Add validators and user roles services
    services.AddValidators();
    services.AddUserRoleService();
}

void ConfigureApplicationPipeline(WebApplication app)
{
    // Seed initial data
    app.SeedInitialDataAsync().Wait();

    // Global error handling
    app.UseMiddleware<ErrorHandlerMiddleware>();

    // Swagger configuration
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // Middleware pipeline configuration
    app.UseCors();
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
}
