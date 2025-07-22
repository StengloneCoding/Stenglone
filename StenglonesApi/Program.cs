using Microsoft.EntityFrameworkCore;
using StenglonesApi.Data;
using StenglonesApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=metrics.db"));

// Add services to the container.
builder.Services.AddScoped<MachineMetricsService>();
builder.Services.AddScoped<FizzBuzzService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register service
builder.Services.AddSingleton<FizzBuzzService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FizzBuzz API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.MapControllers();

app.Run();