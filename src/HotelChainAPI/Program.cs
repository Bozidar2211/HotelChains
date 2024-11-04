using DataAcess;
using DataAcess.Repositories;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using MyProject.Middlewares;
using Service.Abstractions;
using Services;
using Shared.Mappings;

var builder = WebApplication.CreateBuilder(args);
// Register AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

//register the repositories 
builder.Services.AddScoped<IHotelRepository, HotelRepository>();

// Add services to the container.
builder.Services.AddDbContext<RepositoryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IHotelService, HotelService>();

builder.Services.AddScoped<IEmployeeService, EmployeeService>();    //Registrovanje Employee servisa
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>(); // Registering EmployeeRepository

// Add other services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ErrorHandlerMiddleware>(); // Global error handling middleware

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
