using inaApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inaApp.Common.interfaces
{
    public interface IClienteRepository
    {
        //cualquier peticion a recurso externo debe ser asincronico
        Task<List<Cliente>> ObtenerTodosAsync();
        Task<Cliente> ObtenerPorIdAsync();
        Task<Cliente> CrearAsync(Cliente cliente);
        Task<Producto> ActualizarAsync(Cliente cliente);
        Task<bool> EliminarAsync(int id);
    }//end
}
