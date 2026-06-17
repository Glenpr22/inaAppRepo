using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using inaApp.Common.Exception;
using inaApp.Common.interfaces;
using inaAppDTOs.Categoria;
using System.Data;


namespace inaApi.Controllers
{
    //indica como comportarse el controller
    //ruta acceso end point personalizada
    [ApiController]
    [Route("api/categoria")]
    public class CategoriaController : Controller
    {
        //save dependency injection
        private readonly IGenericService<CategoriaResponseDTO, CategoriaCreateDTO, CategoriaUpdateDTO> _categoriaService;

        public CategoriaController(IGenericService<CategoriaResponseDTO, CategoriaCreateDTO, CategoriaUpdateDTO> categoriaServ)
        {
            //inyecta el service en el controlador
            _categoriaService = categoriaServ;
        }

        //get all
        [HttpGet]
        public async Task<ActionResult> IndexAsync()
        {
            try
            {
                var response = await _categoriaService.ObtenerTodosAsync();
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        //get by id
        [HttpGet("{id}")]
        public async Task<ActionResult> ObtenerPorIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Id incorrecto");

                var categoria = await _categoriaService.ObtenerPorIdAsync(id);

                if (categoria == null)
                    return NotFound("Categoria no encontrada");

                return Ok(categoria);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al obtener la categoria: Contacte administrador " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CategoriaCreateDTO categoria)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var response = await _categoriaService.CrearAsync(categoria);

                return Created("Categoria creada correctamente", response);
            }
            catch (DuplicateNameException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidExceptionData ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear la categoria: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody] CategoriaUpdateDTO categoriaUpdate)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Id incorrecto");

                if (categoriaUpdate == null)
                    return BadRequest("Datos incorrectos");

                categoriaUpdate.Id = id;

                var response = await _categoriaService.ActualizarAsync(categoriaUpdate);

                if (response == null)
                    return BadRequest("Error al actualizar la categoria");

                return Ok(new
                {
                    mensaje = "Categoria actualizada correctamente",
                    categoria = response
                });
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (DuplicateNameException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al actualizar la categoria: Contacte administrador " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Error al eliminar, id incorrecto");

                var result = await _categoriaService.EliminarAsync(id);

                return result.Data == true ? Ok("Categoria eliminada")
                : BadRequest("Error al eliminar la categoria");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al eliminar la categoria: Contacte administrador " + ex.Message);
            }
        }
    }
}
