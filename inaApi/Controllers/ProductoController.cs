using inaApp.Common.interfaces;
using inaApp.Entities;
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
        private readonly IGenericService<Producto> _productoService;

        public ProductoController(IGenericService<Producto> productoServ)
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
                var lista = await _productoService.ObtenerTodosAsync();
                // _productoService.ObtenerTodosAsync();

                if (lista.Count == 0)
                {
                    return NotFound("No existen datos");
                }
                return Ok(lista);
            }
            catch (Exception)
            {

                return StatusCode(500, "Error al eliminar el producto: Contacte administrador");

            }



            //return Ok("Correcto");
        }

        // GET: ProductoController/Details/5 por id

        //ocupa pasar parametro que se llame igual al parametro del atributo
        [HttpGet("getById/{id}")]
        public ActionResult Details(int id)
        {
            return View();
        }

        //Tarea2 Obtener por id
        [HttpGet("getById/{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
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
        public async Task<ActionResult> Create([FromBody] Producto producto)
        {
         
            try
            {
                var result = await _productoService.CrearAsync(producto);

                return Created("Creado producto", result);   

            }
            catch (Exception)
            {
                return StatusCode(500, "Error al eliminar el producto: Contacte administrador");
            }

            
        }

        // POST: ProductoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody] Producto producto)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Id incorrecto");

                if (producto == null)
                    return BadRequest("Datos incorrectos");

                producto.Id = id;

                var result = await _productoService.ActualizarAsync(producto);

                if (result == null)
                    return BadRequest("Error al actualizar el producto");

                return Ok(new
                {
                    mensaje = "Producto actualizado correctamente",
                    producto = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al actualizar el producto: Contacte administrador " + ex.Message);
            }
        }//end update

        // GET: ProductoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductoController/Delete/Ç
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {

            try
            {
                if (id <= 0)
                    return BadRequest("Error al eliminar, id Incorrecto");

                var result = await _productoService.EliminarAsync(id);

                return result ? Ok("Producto eliminado") : BadRequest("Error al eliminar el producto");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al eliminar el producto: Contacte administrador" + ex.Message); 
            }
  
        }//End delete 

    }
}
