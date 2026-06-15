using inaApp.Common.Exception;
using inaApp.Common.interfaces;
using inaApp.Entities;
using inaAppDTOs.Producto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inaApp.Service
{
    public class ProductoService : IGenericService<ProductoResponseDTO, 
        ProductoCreateDTO, ProductoUpdateDTO>
    {
        private readonly IGenericRepository <Producto> _productoRepository;

        public ProductoService(IGenericRepository <Producto> productoRepos)
        {
            _productoRepository = productoRepos;
        }


        public async Task<ProductoResponseDTO> ActualizarAsync(ProductoUpdateDTO entity)
        {
            //precio sea mayor a 0 - InvalidPriceException
            if (entity.Precio <= 0)
            {
                throw new InvalidPriceException("El precio debe ser mayor a 0");
            }

            //nombre repetido -- DuplicateProductNameException
            var productos = await _productoRepository.ObtenerTodosAsync();
            if (productos.Any(p => p.Nombre.ToLower() == entity.Nombre.ToLower() && p.Id != entity.Id))
            {
                throw new DuplicateNameException($"El nombre {entity.Nombre} ya existe");
            }

            //stock negativo o 0 --InvalidStockException
            if (entity.Stock <= 0)
            {
                throw new InvalidStockException("El stock debe ser mayor a 0");
            }

            var producto = await _productoRepository.ActualizarAsync(new Producto());

            return new ProductoResponseDTO();
        }

        public async Task<ProductoResponseDTO> CrearAsync(Producto entity)
        {

            /// precio sea mayor a cero = InvalidPriceException
            /// nombre repetido no se puede crear = DuplicateProductNameException
            /// stock negativo no se puede crear = InvalidStockException

            if (entity.Precio <= 0)
                throw new InvalidPriceException("El precio debe ser mayor a cero.");

            if (entity.Stock <= 0)
                throw new InvalidStockException("El stock no puede ser negativo y debe de ser mayor a cero.");


            var productos = await _productoRepository.ObtenerTodosAsync();
            if (productos.Any(p => p.Nombre.ToLower() == entity.Nombre.ToLower()))
                throw new DuplicateProductNameException("El nombre del producto ya existe.");

            //debemos impolementar reglas de negocio    
            return new ProductoResponseDTO();

            /*
             opcion 2
            var productos = await _productoRepository.ObtenerTodosAsync();
            if (productos.Any(p => p.Nombre.ToLower() == entity.Nombre.ToLower()))
                throw new DuplicateProductNameException("El nombre del producto ya existe.")
             */


        }// end method crear async

        public Task<ProductoResponseDTO> CrearAsync(ProductoCreateDTO entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> EliminarAsync(int id)
        {
            //reglas de negocio, id!null, estate
            return await _productoRepository.EliminarAsync(id);
        }

        public async Task<ProductoResponseDTO> ObtenerPorIdAsync(int id)
        {
            //regla de negocio del producto exista
            var pro = await _productoRepository.ObtenerPorIdAsync(id);

            if (pro is null)
            {
                //string template = "El producto con id {0} no existe";

                throw new NotFoundExceptionD($"El producto con id {id} no existe");

            }

            return new ProductoResponseDTO();
        }

        public async Task<List<Producto>> ObtenerTodosAsync()
        {
            //retornar capa repositorio
           return await _productoRepository.ObtenerTodosAsync();    
            throw new NotImplementedException();
        }

        Task<List<ProductoResponseDTO>> IGenericService<ProductoResponseDTO, ProductoCreateDTO, ProductoUpdateDTO>.ObtenerTodosAsync()
        {
            throw new NotImplementedException();
        }
    }//end class
}
