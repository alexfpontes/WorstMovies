using WorstMovie.Services;
using WorstMovie.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using WorstMovie.Infra;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProducerRangeAwardsService, ProducerRangeAwardsService>();

builder.Services.AddDbContext<WorstMovieDbContext>(options =>
    options.UseSqlite(builder.Configuration["ConnectionStrings:DefaultConnection"]));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<WorstMovieDbContext>();
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
    context.EnsurePopulated();
    
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Ocorreu um erro ao criar o banco de dados");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }