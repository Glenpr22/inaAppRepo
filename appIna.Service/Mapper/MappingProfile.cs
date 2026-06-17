using System;
using System.Collections.Generic;
using AutoMapper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using inaApp.Entities;
using inaAppDTOs.Producto;
using Pratice.DTO.Cliente;
using inaAppDTOs.Categoria;

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
            CreateMap<CategoriaCreateDTO, Categoria>();

            //DTOUpdate a Entity
            CreateMap<ProductoUpdateDTO, Producto>();
            CreateMap<CustomerUpdateDTO, Cliente>();
            CreateMap<CategoriaUpdateDTO, Categoria>();

            //Entity => DTOResponse
            //CreateMap<Producto, ProductoResponseDTO>();
            //Para llenar CategoriaNombre en el response,
            //use el Nombre de la categoria relacionada
            CreateMap<Producto, ProductoResponseDTO>()
           .ForMember(dest => dest.CategoriaNombre,
               opt => opt.MapFrom(src => src.Categoria.Nombre));

        
            CreateMap<Cliente, CustomerResponseDTO>();
            CreateMap<Categoria, CategoriaResponseDTO>();
            //DTOUpdate a Entity

        }
    }
}
