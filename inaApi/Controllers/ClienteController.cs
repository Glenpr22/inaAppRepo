using inaApp.Common.interfaces;
using inaApp.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using inaApp.Common.Exception;
using Pratice.DTO.Cliente;

namespace inaApi.Controllers
{
    //access 
    [ApiController]
    //forma 1
     [Route("api/cliente")]
    //forma 2, se puede usar el nombre del controlador, se recomienda esta forma
    // [Route("api/[controller]")]//ruta personalizada, se puede usar el nombre del controlador

    public class ClienteController : Controller
    {
        //inject of dependency the interface of service
        public readonly IGenericService<CustomerResponseDTO, CustomerCreateDTO, CustomerUpdateDTO> _clienteService;

        //it goes through for parameter the interface of service 
        public ClienteController(IGenericService<CustomerResponseDTO, CustomerCreateDTO, CustomerUpdateDTO> clienteService)
        {
            _clienteService = clienteService;
        }//end method constructor

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            try
            {
                var list = await _clienteService.ObtenerTodosAsync();
                return Ok(list);
            }
            catch (NotFoundExceptionD ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = $"Error al obtener los clientes: {ex.Message}" });
            }
        }//end class 

        [HttpGet("{id}")]
        public async Task<ActionResult> Details(int id)
        {

            try
            {

                var client = await _clienteService.ObtenerPorIdAsync(id);
                return Ok(client);

            }
            catch (NotFoundExceptionD ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidIdException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener el cliente: {ex.Message}");
            }

        }//end method getById

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] CustomerCreateDTO cliente)
        {
            try
            {

                var client = await _clienteService.CrearAsync(cliente);
                return Ok(client);

            }
            catch (InvalidExceptionData ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear los cliente: {ex.Message}");
            }
        }//end method create


        [HttpPut]
        public async Task<ActionResult> Edit([FromBody] CustomerUpdateDTO cliente)
        {
            try
            {

                var client = await _clienteService.ActualizarAsync(cliente);
                return Ok(client);

            }
            catch (InvalidExceptionData ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el cliente: {ex.Message}");
            }
        }//end method edit post


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {

                var client = await _clienteService.EliminarAsync(id);
                return Ok(client);

            }
            catch (NotFoundExceptionD ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidIdException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al eliminar al cliente: {ex.Message}");
            }
        }//end method delete


    }//end class
}
