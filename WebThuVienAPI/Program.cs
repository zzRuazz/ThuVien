using Common;
using Microsoft.EntityFrameworkCore;
using WebThuVienAPI.Infrastructure;
using WebThuVienAPI.Models.Context;
using WebThuVienAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterProviders();
builder.Services.RegisterInfrastructures();
builder.Services.RegisterServices();

builder.Services.AddControllers();

// Add Entity Framework DBContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
