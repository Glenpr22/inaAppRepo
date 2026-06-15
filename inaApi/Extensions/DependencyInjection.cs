using inaApp.Common.interfaces;
using inaApp.Data;
using inaApp.Entities;
using inaApp.Repository;
using inaApp.Service;
using inaAppDTOs.Producto;
using Microsoft.EntityFrameworkCore;
using Pratice.DTO.Cliente;

namespace inaApi.Extensions
{
    
    public static class DependencyInjection
    {
        //lleva coleccion de TODAS las inyecciones del programa, ayuda a mantenimiento mas eficiente
        public static IServiceCollection AddAplicationServices(this IServiceCollection services, IConfiguration configuration)
        {

            //base de datos - db context
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            // NEW inyeccion de dependencia de servicios
            services.AddScoped<IGenericService <ProductoResponseDTO,ProductoCreateDTO, ProductoUpdateDTO>, ProductoService>();
           // services.AddScoped<IGenericService<Cliente>, ClienteService>();
           services.AddScoped<IGenericService<CustomerResponseDTO,CustomerCreateDTO, CustomerUpdateDTO>, ClienteService>(); 
            

            //OLD inyeccion e dependencia de repositorios
          //services.AddScoped<IGenericRepository<Producto>, ProductoRepository>();
           //services.AddScoped<IGenericRepository<Cliente>, ClienteRepository>();

            return services;
        }
    }//end 
}
