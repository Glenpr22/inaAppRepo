using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inaApp.Common.interfaces
{
    /// <E> parametriza la entidad para manejar todos esos metodos service en uno solo
    
    public interface IGenericService<TResponse, TCreate, TUpdate>
    {
        Task<List<TResponse>> ObtenerTodosAsync();

        Task<TResponse> ObtenerPorIdAsync(int id);

        Task<TResponse> CrearAsync(TCreate entity);

        Task<TResponse> ActualizarAsync(TUpdate entity);

        Task<bool> EliminarAsync(int id);
    }//end 
}
