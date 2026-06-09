using inaApp.Common.interfaces;
using inaApp.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public readonly IGenericService <Cliente> _clienteService;

        public ClienteController(IGenericService<Cliente> clienteService)
        {
            _clienteService = clienteService;   
        }
        // GET: ClienteController

        //obtener todos
        [HttpGet]
        public ActionResult Index()
        {
           _clienteService.ObtenerTodosAsync();

            return Ok("Correcta ruta");
        }

        // GET: ClienteController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ClienteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClienteController/Create
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

        // GET: ClienteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: ClienteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ClienteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
