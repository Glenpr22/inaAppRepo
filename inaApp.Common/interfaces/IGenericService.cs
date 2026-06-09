using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inaApp.Common.interfaces
{
    /// <E> parametriza la entidad para manejar todos esos metodos service en uno solo
    
    public interface IGenericService<E>
    {
        Task<List<E>> ObtenerTodosAsync();

        Task<E> ObtenerPorIdAsync(int id);

        Task<E> CrearAsync(E entity);

        Task<E> ActualizarAsync(E entity);

        Task<bool> EliminarAsync(int id);
    }//end 
}
