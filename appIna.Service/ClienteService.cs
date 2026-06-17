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
using AutoMapper;
using inaAppDTOs.Producto;
using inaApp.Common.Response;


namespace inaApp.Service
{
    public class ClienteService : IGenericService<CustomerResponseDTO,CustomerCreateDTO,CustomerUpdateDTO>
    {
        //inyeccion para el repository
        public readonly IGenericRepository<Cliente> _clienteRepository;
        //inyeccion mapper
        private readonly IMapper _mapper;

        //now we can access the repository through the interface  
        public ClienteService(IGenericRepository<Cliente> clienteRepository,IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }//end method constructor

        //We contacted the repository and then checked if there data in the database.   
        public async Task<Response<List<CustomerResponseDTO>>> ObtenerTodosAsync()
        {
            var customerList = await _clienteRepository.ObtenerTodosAsync();

            if (customerList == null || !customerList.Any())
                throw new NotFoundException("No se encontraron clientes.");

            return new Response<List<CustomerResponseDTO>>
            {
                Data = _mapper.Map<List<CustomerResponseDTO>>(customerList),
                Message = "Clientes obtenidos exitosamente",
                Success = true
            };
        }

        //
        public async Task<Response<CustomerResponseDTO>> ObtenerPorIdAsync(int id)
        {
            if (id <= 0)
                throw new InvalidIdException("El ID debe ser un número positivo.");

            var customer = await _clienteRepository.ObtenerPorIdAsync(id);

            if (customer is null)
                throw new NotFoundException($"No se encontró el cliente con ID {id}.");

            return new Response<CustomerResponseDTO>
            {
                Data = _mapper.Map<CustomerResponseDTO>(customer),
                Message = "Cliente obtenido correctamente",
                Success = true
            };
        }//end method getById

        //
        public async Task<Response<CustomerResponseDTO>> CrearAsync(CustomerCreateDTO cliente)
        {
            if (!Enum.IsDefined(cliente.TipoIdentificacion))
                throw new InvalidExceptionData("El tipo de identificación no es válido. Solo se permite: " +
                                                "\n1.Cedula." +
                                                "\n2.Dimex." +
                                                "\n3.Pasaporte.");

            if (string.IsNullOrWhiteSpace(cliente.NumeroIdentificacion))
                throw new InvalidExceptionData("El número de identificación es obligatorio.");

            if (!cliente.NumeroIdentificacion.All(char.IsDigit))
                throw new InvalidExceptionData("El número de identificación debe contener únicamente números.");

            var clientes = await _clienteRepository.ObtenerTodosAsync();

            if (clientes.Any(c => c.NumeroIdentificacion == cliente.NumeroIdentificacion))
                throw new InvalidExceptionData("Ya existe un cliente con el mismo número de identificación.");

            Cliente clienteEntity = _mapper.Map<Cliente>(cliente);

            clienteEntity = await _clienteRepository.CrearAsync(clienteEntity);

            return new Response<CustomerResponseDTO>
            {
                Data = _mapper.Map<CustomerResponseDTO>(clienteEntity),
                Message = "Cliente creado correctamente",
                Success = true
            };
        }//end method create 

        public async Task<Response<CustomerResponseDTO>> ActualizarAsync(CustomerUpdateDTO cliente)
        {
            if (cliente.Id <= 0)
                throw new InvalidIdException("El ID debe ser un número positivo.");

            if (!Enum.IsDefined(cliente.TipoIdentificacion))
                throw new InvalidExceptionData("El tipo de identificación no es válido. Solo se permite: " +
                                                "\n1.Cedula." +
                                                "\n2.Dimex." +
                                                "\n3.Pasaporte.");

            if (string.IsNullOrWhiteSpace(cliente.NumeroIdentificacion))
                throw new InvalidExceptionData("El número de identificación es obligatorio.");

            if (!cliente.NumeroIdentificacion.All(char.IsDigit))
                throw new InvalidExceptionData("El número de identificación debe contener únicamente números.");

            var clienteExistente = await _clienteRepository.ObtenerPorIdAsync(cliente.Id);

            if (clienteExistente is null)
                throw new NotFoundException($"No se encontró el cliente con ID {cliente.Id}.");

            var clientes = await _clienteRepository.ObtenerTodosAsync();

            if (clientes.Any(c => c.NumeroIdentificacion == cliente.NumeroIdentificacion && c.Id != cliente.Id))
                throw new InvalidExceptionData("Ya existe otro cliente con el mismo número de identificación.");

            _mapper.Map(cliente, clienteExistente);

            var clienteActualizado = await _clienteRepository.ActualizarAsync(clienteExistente);

            return new Response<CustomerResponseDTO>
            {
                Data = _mapper.Map<CustomerResponseDTO>(clienteActualizado),
                Message = "Cliente actualizado correctamente",
                Success = true
            };
        }//end method customerUpdate service

        //
        public async Task<Response<bool>> EliminarAsync(int id)
        {

            if (id <= 0)
                throw new InvalidIdException("El ID debe ser un número positivo.");

            var client = await _clienteRepository.ObtenerPorIdAsync(id);
            if (client is null)
                throw new NotFoundException($"No se encontró el cliente con ID {id}.");

            bool result = await _clienteRepository.EliminarAsync(id);

            //
            return new Response<bool>
            {
                Data = result,
                Message = "Cliente eliminado correctamente",
                Success = true
            };

        }//end method delete

    }//end class
}
