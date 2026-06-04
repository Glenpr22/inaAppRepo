using inaApp.Common.interfaces;
using inaApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inaApp.Repository
{
    public class ProductoRepository : IProductoRepository
    {
        

        public Task<Producto> ActualizarAsync(Producto producto)
        {
            throw new NotImplementedException();
        }

        public Task<Producto> CrearAsync(Producto producto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EliminarAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Producto> ObtenerPorIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Producto>> ObtenerTodosAsync()
        {
            throw new NotImplementedException();
        }
    }//end
}
