using SportBuddy.Application;
using SportBuddy.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

// TODO: loggerConfiguration

var app = builder.Build();
app.UseInfrastructure();
app.Run();
