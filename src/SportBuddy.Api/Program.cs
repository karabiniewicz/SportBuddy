using SportBuddy.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddInfrastructure()
    .AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();
