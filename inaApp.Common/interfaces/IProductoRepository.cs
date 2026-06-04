using inaApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inaApp.Common.interfaces
{
    public interface IProductoRepository
    {
        //cualquier peticion a recurso externo debe ser asincronico
        Task<List<Producto>> ObtenerTodosAsync();
        Task<Producto> ObtenerPorIdAsync();
        Task<Producto> CrearAsync(Producto producto);
        Task<Producto> ActualizarAsync(Producto producto);
        Task<bool> EliminarAsync(int id);


    }
}
