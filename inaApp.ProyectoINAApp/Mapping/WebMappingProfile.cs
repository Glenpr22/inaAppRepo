using AutoMapper;
using inaApp.ProyectoINAApp.Models.Producto;
//using inaApp.ProyectoINAApp.Models.Categoria;
using inaAppDTOs.Producto;
using inaAppDTOs.Categoria;
using inaApp.ProyectoINAApp.Models.Categoria;

namespace inaApp.ProyectoINAApp.Mapping
{
    public class WebMappingProfile : Profile
    {
        public WebMappingProfile()
        {

            //Mapeo Productos

            //DTO Response -> ViewModel
            CreateMap<ProductoResponseDTO, ProductoIndexViewModel>();
            CreateMap<ProductoResponseDTO, ProductoEditViewModel>();

            //ViewModel -> DTO Response
            CreateMap<ProductoIndexViewModel, ProductoResponseDTO>();
            CreateMap<ProductoCreateViewModel, ProductoCreateDTO>();
            CreateMap<ProductoEditViewModel, ProductoUpdateDTO>();


            //Mapp categorias

            //DTO Response -> ViewModel          
             CreateMap<CategoriaResponseDTO, CategoriaIndexViewModel>();
             CreateMap<CategoriaResponseDTO, CategoriaEditViewModel>();

            //ViewModel -> DTO Response

             CreateMap<CategoriaIndexViewModel, CategoriaResponseDTO>();
             CreateMap<CategoriaCreateViewModel, CategoriaCreateDTO>();
             CreateMap<CategoriaEditViewModel, CategoriaUpdateDTO>();


        }

    }

}
