using Microsoft.EntityFrameworkCore;
using TrackPro.Application.Contracts.Persistence;
using TrackPro.Domain.Entities;
using TrackPro.Infrastructure.Persistence.DbContexts;
using TrackPro.Infrastructure.Persistence.Repositories;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TrackProDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("TrackProConnectionString"))
);

builder.Services.AddScoped<IStationRepository, StationRepository>();
builder.Services.AddScoped<IPartRepository, PartRepository>();
builder.Services.AddScoped<IMovementRepository, MovementRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TrackProDbContext>();
    if (!await dbContext.Stations.AnyAsync())
    {
        await dbContext.Stations.AddRangeAsync(new List<Station>
        {
            new Station { Name = "Recebimento", Order = 1 },
            new Station { Name = "Montagem", Order = 2 },
            new Station { Name = "Inspeção Final", Order = 3 }
        });
        await dbContext.SaveChangesAsync();
    }
}


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();