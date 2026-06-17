using inaApp.Common.Exception;
using inaApp.Common.interfaces;
using inaApp.Entities;
using inaAppDTOs.Producto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace inaApi.Controllers
{
    //indica como comportarse el controller
    //ruta acceso end point personalizada
    [ApiController]
    [Route("api/producto")]
    public class ProductoController : Controller
    {
        //guarda inyeccion dependencias 
        private readonly IGenericService<ProductoResponseDTO,ProductoCreateDTO, ProductoUpdateDTO> _productoService;

        public ProductoController(IGenericService<ProductoResponseDTO, ProductoCreateDTO, ProductoUpdateDTO> productoServ)
        {
            //inyecta el service en el controlador
            _productoService = productoServ;
        }


        // GET: ProductoController

        //obtener todos
        [HttpGet]
        public async Task <ActionResult> IndexAsync()
        {

            try
            {
                var response = await _productoService.ObtenerTodosAsync();
                // _productoService.ObtenerTodosAsync();
                return Ok(response);
            }
            catch (NotFoundException ex)    
            {
                return NotFound(ex.Message);    
            }
            catch (Exception ex)
            {

              //  return StatusCode(500, "Error al obtener productos: Contacte administrador");    
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }

            //return Ok("Correcto");
        }

        // GET: ProductoController/Details/5 por id

        //ocupa pasar parametro que se llame igual al parametro del atributo
      
        //Tarea2 Obtener por id
        [HttpGet("{id}")]
        public async Task<ActionResult> ObtenerPorIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Id incorrecto");

                var producto = await _productoService.ObtenerPorIdAsync(id);

                if (producto == null)
                    return NotFound("Producto no encontrado");

                return Ok(producto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al obtener el producto: Contacte administrador " + ex.Message);
            }
        }

        // GET: ProductoController/Create
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ProductoCreateDTO producto)
        {

            try
            {
                //producto.Estado = true; // por default el producto se crea activo

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);      

                var response = await _productoService.CrearAsync(producto);
                return Created("Producto creado correctamente ",response);
            }
            catch (DuplicateProductNameException ex)
            {
                return BadRequest(ex.Message);

            }
            catch (InvalidPriceException ex)
            {
                return BadRequest(ex.Message);

            }
            catch (InvalidStockException ex)
            {

                return BadRequest(ex.Message);

            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el producto: {ex.Message}");
            }

        }//end method create
       
        // GET: ProductoController/Edit/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody] ProductoUpdateDTO productoUpdate)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Id incorrecto");

                if (productoUpdate == null)
                    return BadRequest("Datos incorrectos");

                productoUpdate.Id = id;

                var response = await _productoService.ActualizarAsync(productoUpdate);

                if (response == null)
                    return BadRequest("Error al actualizar el producto");

                return Ok(new
                {
                    mensaje = "Producto actualizado correctamente",
                    producto = response
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al actualizar el producto: Contacte administrador " + ex.Message);
            }
        }//end update

        // GET: ProductoController/Delete/
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {

            try
            {
                if (id <= 0)
                    return BadRequest("Error al eliminar, id Incorrecto");
                //
                var result = await _productoService.EliminarAsync(id);

                return result.Data == true? Ok("Producto eliminado")
                : BadRequest("Error al eliminar el producto");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al eliminar el producto: Contacte administrador" + ex.Message); 
            }
  
        }//End delete 

    }
}
