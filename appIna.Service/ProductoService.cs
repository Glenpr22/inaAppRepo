using inaApp.Common.interfaces;
using inaApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inaApp.Service
{
    public class ProductoService : IGenericService<Producto>
    {
        private readonly IGenericRepository <Producto> _productoRepository;

        public ProductoService(IGenericRepository <Producto> productoRepos)
        {
            _productoRepository = productoRepos;
        }

        public Task<Producto> ActualizarAsync(Producto entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Producto> CrearAsync(Producto entity)
        {
            //reglas de negocio
            return await _productoRepository.CrearAsync(entity);
           // throw new NotImplementedException();
        }

        public async Task<bool> EliminarAsync(int id)
        {
            //reglas de negocio, id!null, estate
            return await _productoRepository.EliminarAsync(id);
        }

        public Task<Producto> ObtenerPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Producto>> ObtenerTodosAsync()
        {
            //retornar capa repositorio
           return await _productoRepository.ObtenerTodosAsync();    
            throw new NotImplementedException();
        }
    }//end class
}
