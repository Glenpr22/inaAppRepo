using inaApp.Common.interfaces;
using inaApp.Repository;
using inaApp.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//
builder.Services.AddControllers();

// add dependency 
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();




// add teacher
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
