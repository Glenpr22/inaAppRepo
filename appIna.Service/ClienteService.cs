using inaApp.Common.interfaces;
using inaApp.Entities;
using inaApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using inaApp.Common.Exception;
using static inaApp.Common.Enums.Enums;
using Pratice.DTO.Cliente;


namespace inaApp.Service
{
    public class ClienteService : IGenericService<CustomerResponseDTO,CustomerCreateDTO,CustomerUpdateDTO>
    {
        //inyeccion para el repository
        public readonly IGenericRepository<Cliente> _clienteRepository;


        //now we can access the repository through the interface  
        public ClienteService(IGenericRepository<Cliente> clienteRepository)
        {
            _clienteRepository = clienteRepository;

        }//end method constructor

        //We contacted the repository and then checked if there data in the database.
        public async Task<List<CustomerResponseDTO>> ObtenerTodosAsync()
        {

            var client = await _clienteRepository.ObtenerTodosAsync();

            if (client == null || !client.Any())
                throw new NotFoundExceptionD("No se encontraron clientes.");

            return new List<CustomerResponseDTO>();

        }//end method getAll


        //
        public async Task<CustomerResponseDTO> ObtenerPorIdAsync(int id)
        {

            var client = await _clienteRepository.ObtenerPorIdAsync(id);

            if (client is null)
                throw new NotFoundExceptionD($"No se encontró el cliente con ID {id}.");

            if (id <= 0)
                throw new InvalidIdException("El ID debe ser un número positivo.");

            return new CustomerResponseDTO ();

        }//end method getById

        //
        public async Task<CustomerResponseDTO> CrearAsync(CustomerCreateDTO cliente)
        {

            if (!Enum.IsDefined(cliente.TipoIdentificacion))
                throw new InvalidExceptionData("El tipo de identificación no es válido. Solo se permite: " +
                                                "\n1.Cedula." +
                                                "\n2.Dimex" +
                                                "\n3.Pasaporte");

            if (!string.IsNullOrWhiteSpace(cliente.NumeroIdentificacion) &&
                !cliente.NumeroIdentificacion.All(char.IsDigit))
                throw new InvalidExceptionData("El número de identificación debe contener únicamente números.");


            var client = await _clienteRepository.ObtenerTodosAsync();

            if (!client.Any(c => c.NumeroIdentificacion == cliente.NumeroIdentificacion))
                return new CustomerResponseDTO();
            else
                throw new InvalidExceptionData("Ya existe un cliente con el mismo numero de identificacion.");

        }//end method create

        //
        public async Task<CustomerResponseDTO> ActualizarAsync(CustomerUpdateDTO cliente)
        {

            if (!Enum.IsDefined(cliente.TipoIdentificacion))
                throw new InvalidExceptionData("El tipo de identificación no es válido. Solo se permite: " +
                                                "\n1.Cedula." +
                                                "\n2.Dimex" +
                                                "\n3.Pasaporte");

            if (!string.IsNullOrWhiteSpace(cliente.NumeroIdentificacion) &&
                !cliente.NumeroIdentificacion.All(char.IsDigit))
                throw new InvalidExceptionData("El número de identificación debe contener únicamente números.");

            if (cliente.Id <= 0)
                throw new InvalidIdException("El ID debe ser un número positivo");

            var clienteExistente = await _clienteRepository.ObtenerPorIdAsync(cliente.Id);
            if (clienteExistente is null)
                throw new NotFoundExceptionD($"No se encontró el cliente con ID {cliente.Id}.");

            //var client = await _clienteRepository.ActualizarAsync(cliente);
            return new CustomerResponseDTO();

        }//end method update


        //
        public async Task<bool> EliminarAsync(int id)
        {

            if (id <= 0)
                throw new InvalidIdException("El ID debe ser un número positivo.");

            var client = await _clienteRepository.ObtenerPorIdAsync(id);
            if (client is null)
                throw new NotFoundExceptionD($"No se encontró el cliente con ID {id}.");

            bool result = await _clienteRepository.EliminarAsync(id);
            return result;

        }//end method delete


    }//end class
}
