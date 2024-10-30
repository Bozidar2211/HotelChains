using DataAcess;
using DataAcess.Repositories;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Service.Abstractions;
using Services;

var builder = WebApplication.CreateBuilder(args);

//register the repositories 
builder.Services.AddScoped<IHotelRepository, HotelRepository>();

// Add services to the container.
builder.Services.AddDbContext<RepositoryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IHotelService, HotelService>();

// Add other services
builder.Services.AddControllers();
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
