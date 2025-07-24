using Microsoft.EntityFrameworkCore;
using TrackPro.Application.Contracts.Persistence;
using TrackPro.Domain.Entities;
using TrackPro.Infrastructure.Persistence.DbContexts;
using TrackPro.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);

var app = builder.Build();

ConfigurePipeline(app);

app.Run();



void ConfigureServices(IServiceCollection services)
{
    var connectionString = builder.Configuration.GetConnectionString("TrackProConnectionString");
    
    services.AddDbContext<TrackProDbContext>(options =>
        options.UseSqlite(connectionString)
    );

    services.AddScoped<IStationRepository, StationRepository>();
    services.AddScoped<IPartRepository, PartRepository>();
    services.AddScoped<IMovementRepository, MovementRepository>();
    
    services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IStationRepository).Assembly));
    
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
}

void ConfigurePipeline(WebApplication webApp)
{
    SeedDatabase(webApp);

    if (webApp.Environment.IsDevelopment())
    {
        webApp.UseSwagger();
        webApp.UseSwaggerUI();
    }
    
    webApp.UseHttpsRedirection();
    webApp.UseAuthorization();
    webApp.MapControllers();
}

void SeedDatabase(WebApplication webApp)
{
    using var scope = webApp.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<TrackProDbContext>();
    
    
    if (!dbContext.Stations.AnyAsync().Result)
    {
        dbContext.Stations.AddRange(
            new Station { Name = "Recebimento", Order = 1 },
            new Station { Name = "Montagem", Order = 2 },
            new Station { Name = "Inspeção Final", Order = 3 }
        );
        dbContext.SaveChanges();
    }
}