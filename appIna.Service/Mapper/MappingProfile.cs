using System;
using System.Collections.Generic;
using AutoMapper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using inaApp.Entities;
using inaAppDTOs.Producto;
using Pratice.DTO.Cliente;

//instalamos el paquete de automapper complete
//dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection

namespace inaApp.Service.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            //DTO => Entity
            CreateMap<ProductoCreateDTO, Producto>();
            CreateMap<CustomerCreateDTO, Cliente>();    

            //DTOUpdate a Entity
            CreateMap<ProductoUpdateDTO, Producto>();
            CreateMap<CustomerUpdateDTO, Cliente>();

            //Entity => DTOResponse
            CreateMap<Producto, ProductoResponseDTO>();
            CreateMap<Cliente, CustomerResponseDTO>();
            //DTOUpdate a Entity

        }
    }
}
