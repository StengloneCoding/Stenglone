global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using System.Threading;
using Microsoft.AspNetCore.Diagnostics;
using StenglonesApi.Conventions;
using StenglonesApi.Data;
using StenglonesApi.Interface;
using StenglonesApi.Interfaces;
using StenglonesApi.Services;
using System.Reflection;

const string API_PREFIX = "api/v1";

var builder = WebApplication.CreateBuilder(args);

// Add SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=metrics.db"));

// Add services to the container.
builder.Services.AddScoped<IMachineMetricsService, MachineMetricsService>();
builder.Services.AddScoped<IFizzBuzzService, FizzBuzzService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "StenglonesApi",
        Version = API_PREFIX
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);


    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new RoutePrefixConvention(API_PREFIX));
});

var app = builder.Build();

app.UseExceptionHandler(options =>
{
    options.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        int statusCode = exception switch
        {
            KeyNotFoundException => StatusCodes.Status404NotFound,
            ArgumentException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/problem+json";

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = statusCode switch
            {
                StatusCodes.Status404NotFound => "Resource not found",
                StatusCodes.Status400BadRequest => "Bad request",
                StatusCodes.Status500InternalServerError => "Internal server error",
                _ => "Error"
            },
            Detail = exception?.Message,
            Instance = context.Request.Path
        };

        await context.Response.WriteAsJsonAsync(problemDetails);
    });
});



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
        c.SwaggerEndpoint("/swagger/v1/swagger.json", $"StenglonesApi {API_PREFIX}");
        c.RoutePrefix = string.Empty;
    });
}

app.MapControllers();

app.Run();