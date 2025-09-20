using CrudOpsMongoDB.API.Models;
using CrudOpsMongoDB.API.Service;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.Configure<MoviesDBSettings>(builder.Configuration.GetSection("MongoDBConnectionStrings"));
builder.Services.AddSingleton<IMoviesDBSettings>(sp => sp.GetRequiredService<IOptions<MoviesDBSettings>>().Value);
builder.Services.AddSingleton<IMongoClient>(s => new MongoClient(builder.Configuration.GetValue<string>("MongoDBConnectionStrings:ConnectionString")));
builder.Services.AddScoped<IMovieService, MovieService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle		//again
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
