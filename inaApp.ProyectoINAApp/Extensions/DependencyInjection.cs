using inaApp.Common.interfaces;
using inaApp.Data;
using inaApp.Entities;
using inaApp.ProyectoINAApp.Mapping;
using inaApp.Repository;
using inaApp.Service;
using inaApp.Service.Mapper;
using inaAppDTOs.Categoria;
using inaAppDTOs.Producto;
using Microsoft.EntityFrameworkCore;
using Pratice.DTO.Cliente;

namespace inaApp.ProyectoINAApp.Extensions
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddAplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            //base de datos- db context
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"));
            });


            //  services.AddAutoMapper(typeof(MappingProfile));

             services.AddAutoMapper(fg => { }, typeof(MappingProfile), typeof(WebMappingProfile));

            //inyecciones de dependencia de servicios
            services.AddScoped<IGenericService<ProductoResponseDTO, ProductoCreateDTO, ProductoUpdateDTO>, ProductoService>();
            services.AddScoped<IGenericService<CategoriaResponseDTO, CategoriaCreateDTO, CategoriaUpdateDTO>, CategoriaService>();
            services.AddScoped<IGenericService<CustomerResponseDTO, CustomerCreateDTO, CustomerUpdateDTO>, ClienteService>();


            //inyecciones de  dependencia de repositorios
            services.AddScoped<IGenericRepository<Producto>, ProductoRepository>();
            services.AddScoped<IGenericRepository<Categoria>, CategoriaRepository>();
            services.AddScoped<IGenericRepository<Cliente>, ClienteRepository>();

            return services;
        }
    }
}
