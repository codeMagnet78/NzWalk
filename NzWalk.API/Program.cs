using Microsoft.EntityFrameworkCore;
using NzWalk.API.Data;
using NzWalk.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DI for connection string
builder.Services.AddDbContext<NzWalkDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionString")));

//Injecting the Repository pattern
//AddScoped: Scoped lifetime services are created once per request.
// Scoped repositories are created once per HTTP request, and then disposed of at the end of the request
builder.Services.AddScoped<IRegionRepository, SqlRegionRepository>();


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
