using inaApp.Common.Exception;
using inaApp.Common.interfaces;
using inaApp.Entities;
using inaAppDTOs.Producto;
using System;
using System.Collections.Generic;
using System.Data;
using AutoMapper;
using inaApp.Common.Response;

namespace inaApp.Service
{
    public class ProductoService : IGenericService<ProductoResponseDTO, 
        ProductoCreateDTO, ProductoUpdateDTO>
    {
        //inserto mapper
        private readonly IGenericRepository <Producto> _productoRepository;
        private readonly IMapper _mapper;

        public ProductoService(IGenericRepository <Producto> productoRepos, IMapper mapper)
        {
            _productoRepository = productoRepos;
            _mapper = mapper;
        }

        public async Task<Response<ProductoResponseDTO>> ActualizarAsync(ProductoUpdateDTO entity)
        {
            // validaciones de negocio
            if (entity.Precio <= 0)
                throw new InvalidPriceException("El precio debe ser mayor a 0");

            if (entity.Stock <= 0)
                throw new InvalidStockException("El stock debe ser mayor a 0");

            // busca si el producto existe en la base de datos
            var productoExistente = await _productoRepository.ObtenerPorIdAsync(entity.Id);

            if (productoExistente == null)
                throw new NotFoundException("Producto no encontrado");

            // evita que existan dos productos con el mismo nombre
            var productos = await _productoRepository.ObtenerTodosAsync();

            if (productos.Any(p => p.Nombre.ToLower() == entity.Nombre.ToLower() && p.Id != entity.Id))
                throw new DuplicateNameException($"El nombre {entity.Nombre} ya existe");

            // mapeo: pasa los cambios del dto al producto existente
            _mapper.Map(entity, productoExistente);

            // guarda los cambios en la base de datos
            var productoActualizado = await _productoRepository.ActualizarAsync(productoExistente);

            // mapeo: convierte el producto guardado al dto de respuesta
            return new Response<ProductoResponseDTO>
            {
                Data = _mapper.Map<ProductoResponseDTO>(productoActualizado),
                Message = "Producto actualizado correctamente",
                Success = true
            };
        }

        public async Task <Response<ProductoResponseDTO>> CrearAsync(ProductoCreateDTO entity)
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

            //Convertir el dto a entity y guardarlo en la bd 
            //Producto product = new Producto
            //{
            //    Nombre = entity.Nombre,
            //    Descripcion = entity.Descripcion,
            //    Precio = entity.Precio,
            //    Stock = entity.Stock,
            //    Estado = true
            //};

            Producto producto = _mapper.Map<Producto>(entity);

            producto = await _productoRepository.CrearAsync(producto);

            
            //convierte a DTOResponse para return the customer

            return new Response<ProductoResponseDTO>
            {
                Data = _mapper.Map<ProductoResponseDTO>(producto),
                Message = "Producto creado correctamente",
                Success = true
            };

        }//ent crearasync

        public async Task<Response<bool>> EliminarAsync(int id)
        {
            return new Response<bool>
            {
                //reglas de negocio, id!null, estate
                Data = await _productoRepository.EliminarAsync(id),
                Message = "Producto eliminado correctamente",
                Success = true
            };       
        }

        public async Task<Response<ProductoResponseDTO>> ObtenerPorIdAsync(int id)
        {
            //regla de negocio del producto exista
            var pro = await _productoRepository.ObtenerPorIdAsync(id);

            if (pro is null)
                //string template = "El producto con id {0} no existe";
                throw new NotFoundException($"El producto con id {id} no existe");

            //convierte a DTOResponse para return the customer
            return new Response<ProductoResponseDTO>
            {
                Data = _mapper.Map<ProductoResponseDTO>(pro),
                Message = "Producto obtenido correctamente",
                Success = true
            };

           // var productoResponse = _mapper.Map<ProductoResponseDTO>(pro);   
           // return productoResponse;
        }

        public async Task<Response<List<ProductoResponseDTO>>> ObtenerTodosAsync()
        {
            var listaProductos = await _productoRepository.ObtenerTodosAsync();

            if (listaProductos == null || !listaProductos.Any())
                throw new NotFoundException("No se encontraron productos.");   

            //convierte a DTOResponse para return the producto
            return new Response<List<ProductoResponseDTO>>
            {
                Data = _mapper.Map<List<ProductoResponseDTO>>(listaProductos),
                Message = "Productos obtenidos exitosamente",
                Success = true
            };
        }

     
    }//end class
}
