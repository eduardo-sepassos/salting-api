using Microsoft.EntityFrameworkCore;
using SaltingApi.Repository;
using SaltingHandler.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure App
builder.Services.AddSaltHandler();
builder.Services.AddDbContext<SaltingApiContext>(opt => opt.UseInMemoryDatabase("saltingDB"));
builder.Services.AddScoped<IUserCredentialsRepository, UserCredentialsRepository>();

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
