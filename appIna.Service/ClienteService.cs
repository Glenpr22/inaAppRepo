using inaApp.Common.interfaces;
using inaApp.Entities;
using inaApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inaApp.Service
{
    public class ClienteService : IGenericService<Cliente>
    {
        // inyeccion de dependencias
        public readonly IGenericRepository <Cliente> _clienteRepository;

        public ClienteService(IGenericRepository <Cliente> clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public Task<Cliente> ActualizarAsync(Cliente entity)
        {
            throw new NotImplementedException();
        }

        public Task<Cliente> CrearAsync(Cliente entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EliminarAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Cliente> ObtenerPorIdAsync(int id)
        {
            //obtener db  repository
            return _clienteRepository.ObtenerPorIdAsync(id);
            throw new NotImplementedException();
        }

        public Task<List<Cliente>> ObtenerTodosAsync()
        {
            _clienteRepository.ObtenerTodosAsync();
            throw new NotImplementedException();
        }
    }//end class ClienteService
}
