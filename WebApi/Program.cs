using FluentValidation;
using WebApi.Context;
using WebApi.Middlewares;
using WebApi.Models;
using WebApi.Repositories;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
services.AddControllers();
services.AddDbContext<AppDbContext>();
services.AddAutoMapper(typeof(SubdivisionRequestMapProfile), typeof(SubdivisionResponseMapProfile));
services.AddScoped<ISubdivisionRepository, SubdivisionRepository>();
services.AddScoped<ISubdivisionService, SubdivisionService>();
services.AddScoped<IValidator<SubdivisionRequest>, SubdivisionRequestValidator>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionsMiddleware>();

app.MapControllers();

app.Run();
