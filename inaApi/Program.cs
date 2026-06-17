using inaApi.Extensions;
using inaApp.Common.interfaces;
using inaApp.Repository;
using inaApp.Service;

var builder = WebApplication.CreateBuilder(args);

//registro contenerdor de inyeccion de dependencias total del aplicativo
builder.Services.AddAplicationServices(builder.Configuration);
// swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();


//swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
