using InsightLogs.Infrastructure.Extensions;
using InsightLogs.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Agregar Infraestructura
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                      ?? "Data Source=insightlogs.db";
builder.Services.AddInfrastructure(connectionString);

builder.Services.AddControllers()
    .AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<InsightLogsDbContext>();
    db.Database.Migrate(); // SOLO migrate, nunca EnsureCreated aquí
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();