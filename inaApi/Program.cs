using inaApi.Extensions;
using inaApp.Common.interfaces;
using inaApp.Repository;
using inaApp.Service;

var builder = WebApplication.CreateBuilder(args);

//registro contenerdor de inyeccion de dependencias total del aplicativo
builder.Services.AddAplicationServices(builder.Configuration);



// Add services to the container.
//
builder.Services.AddControllers();
//pr
// add dependency 



// add teacher
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
