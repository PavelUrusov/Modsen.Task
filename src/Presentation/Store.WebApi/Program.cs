using Store.Application.Extensions;
using Store.Auth.Extensions;
using Store.Persistence.Data;
using Store.Persistence.Extensions;
using Store.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenConfiguration();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddTransactionService();
builder.Services.AddMapper();
builder.Services.AddMediatr();
builder.Services.AddValidatorBehavior();
builder.Services.AddLoggerBehavior();
builder.Services.AddJwtBearerConfiguration(builder.Configuration);
builder.Services.AddJwtBearerAuthentication(builder.Configuration);
builder.Services.AddCorsConfiguration();
builder.Services.AddAuthService();
builder.Services.AddAuthContext();
builder.Services.AddAuthorizationPolicy();
builder.Services.AddValidators();
builder.Services.AddUserRoleService();

var app = builder.Build();

await app.SeedInitialDataAsync();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();