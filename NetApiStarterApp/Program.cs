using Microsoft.EntityFrameworkCore;
using NetApiStarterApp.Data;
using NetApiStarterApp.Models.Mappings;
using NetApiStarterApp.Repository.Vehicle;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Database service
builder.Services.AddDbContext<DataConnection>(con => con.UseSqlServer(builder.Configuration.GetConnectionString("DataConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//register interfaces and services
builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddHttpClient();
builder.Services.AddScoped<IVehicleService, VehicleService>();

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
