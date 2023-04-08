using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inject DB Context and connection string
builder.Services.AddDbContext<NZWalkDbContext>(options =>
options.UseMySQL(builder.Configuration.GetConnectionString("LocalConnection")));

// builder.Services.AddScoped<IRegionRepository, MySqlRegionRepository>();
builder.Services.AddScoped<IRegionRepository, InMemoryRegionRepository>();

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
