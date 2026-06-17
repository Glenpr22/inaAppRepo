using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using inaApp.Common.Exception;
using inaApp.Common.interfaces;
using inaApp.Common.Response;
using inaApp.Entities;
using inaApp.Repository;
using inaAppDTOs.Categoria;

namespace inaApp.Service
{
    public class CategoriaService : IGenericService<CategoriaResponseDTO,
        CategoriaCreateDTO, CategoriaUpdateDTO>
     {
        private readonly IGenericRepository<Categoria> _categoriaRepository;
        private readonly IMapper _mapper;

        public CategoriaService(IGenericRepository<Categoria> categoriaRepository, IMapper mapper)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        public async Task<Response<CategoriaResponseDTO>> ObtenerPorIdAsync(int id)
        {
            var categoria = await _categoriaRepository.ObtenerPorIdAsync(id);

            if (categoria == null)
                throw new NotFoundException($"La categoria con id {id} no existe");

            return new Response<CategoriaResponseDTO>
            {
                Data = _mapper.Map<CategoriaResponseDTO>(categoria),
                Message = "Categoria obtenida correctamente",
                Success = true
            };
        }

        public async Task<Response<List<CategoriaResponseDTO>>> ObtenerTodosAsync()
        {
            var listaCategorias = await _categoriaRepository.ObtenerTodosAsync();

            if (listaCategorias == null || !listaCategorias.Any())
                throw new NotFoundException("No se encontraron categorias");

            return new Response<List<CategoriaResponseDTO>>
            {
                Data = _mapper.Map<List<CategoriaResponseDTO>>(listaCategorias),
                Message = "Categorias obtenidas correctamente",
                Success = true
            };
        }

        //method createCategory
        public async Task<Response<CategoriaResponseDTO>> CrearAsync(CategoriaCreateDTO entity)
        {
            var categorias = await _categoriaRepository.ObtenerTodosAsync();

            if (categorias.Any(c => c.Nombre.ToLower() == entity.Nombre.ToLower()))
                throw new DuplicateNameException($"El nombre {entity.Nombre} ya existe");

            Categoria categoria = _mapper.Map<Categoria>(entity);

            categoria = await _categoriaRepository.CrearAsync(categoria);

            return new Response<CategoriaResponseDTO>
            {
                Data = _mapper.Map<CategoriaResponseDTO>(categoria),
                Message = "Categoria creada correctamente",
                Success = true
            };
        }

        public async Task<Response<CategoriaResponseDTO>> ActualizarAsync(CategoriaUpdateDTO entity)
        {
            var categoriaExistente = await _categoriaRepository.ObtenerPorIdAsync(entity.Id);

            if (categoriaExistente == null)
                throw new NotFoundException("Categoria no encontrada");

            var categorias = await _categoriaRepository.ObtenerTodosAsync();

            if (categorias.Any(c => c.Nombre.ToLower() == entity.Nombre.ToLower() && c.Id != entity.Id))
                throw new DuplicateNameException($"El nombre {entity.Nombre} ya existe");

            _mapper.Map(entity, categoriaExistente);

            var categoriaActualizada = await _categoriaRepository.ActualizarAsync(categoriaExistente);

            return new Response<CategoriaResponseDTO>
            {
                Data = _mapper.Map<CategoriaResponseDTO>(categoriaActualizada),
                Message = "Categoria actualizada correctamente",
                Success = true
            };
        }

        public async Task<Response<bool>> EliminarAsync(int id)
        {
            var categoria = await _categoriaRepository.ObtenerPorIdAsync(id);

            if (categoria == null)
                throw new NotFoundException("Categoria no encontrada");

            return new Response<bool>
            {
                Data = await _categoriaRepository.EliminarAsync(id),
                Message = "Categoria eliminada correctamente",
                Success = true
            };
        }

    }//end class serviceCategoria

}//end namespace

