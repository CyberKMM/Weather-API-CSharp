using Microsoft.Extensions.Options;
using WeatherAPI.Configuration;
using WeatherAPI.Interfaces;
using WeatherAPI.Repositories;
using WeatherAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.Configure<OpenMeteoSettings>(builder.Configuration.GetSection("OpenMeteo"));

builder.Services.AddScoped<IWeatherRepository, WeatherRepository>();

builder.Services.AddHttpClient<IOpenMeteoClient, OpenMeteoClient>((serviceProvider, client) =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<OpenMeteoSettings>>().Value;
    client.BaseAddress = new Uri(settings.BaseUrl);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();