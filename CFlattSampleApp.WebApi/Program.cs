using CFlattSampleApp.Domain;
using CFlattSampleApp.Data;
using Microsoft.EntityFrameworkCore;
using MediatR;

var builder = WebApplication.CreateBuilder(args);
// Local developer config, ignored by Git
builder.Configuration.AddJsonFile("appsettings.Override.json", optional: true);
IConfiguration configuration = builder.Configuration;

// Add services to the container.
//builder.Services.AddTransient<ILogger>();
builder.Services.AddDbContext<CFlattSampleAppDbContext>(options => options
    .UseSqlServer(configuration.GetConnectionString("CFlattSampleAppDb")));

builder.Services.AddMediatR(config => config
    .RegisterServicesFromAssembly(typeof(CFlattSampleApp.Data.Models.User).Assembly)
    .RegisterServicesFromAssembly(typeof(CFlattSampleApp.Domain.Models.User).Assembly));

builder.Services.AddControllers();
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

// Allows using TestHost for integration tests
public partial class Program { }
