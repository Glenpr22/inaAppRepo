using AutoMapper;
using inaApp.Common.Exception;
using inaApp.Common.interfaces;
using inaApp.Entities;
using inaApp.ProyectoINAApp.Models.Categoria;
using inaApp.ProyectoINAApp.Models.Producto;
using inaApp.Service;
using inaAppDTOs.Categoria;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace inaApp.ProyectoINAApp.Controllers
{
    public class CategoriaController : Controller
    {

        //inyeccion de dependencia
        private readonly IGenericService<CategoriaResponseDTO, CategoriaCreateDTO, CategoriaUpdateDTO> _categoriaService;
        private readonly IMapper _mapper;
        public CategoriaController(IGenericService<CategoriaResponseDTO, CategoriaCreateDTO, CategoriaUpdateDTO> categoriaServ, IMapper mapper)
        {
            _categoriaService = categoriaServ;
            _mapper = mapper;
        }


        // GET: CategoriaController
        public async Task<ActionResult> IndexAsync()
        {
            try
            {
                //requiero ir a productos y mostrarlos en la vista 
                var lista = await _categoriaService.ObtenerTodosAsync();

                //mapear la lista de productos repossedto a una lista de viewmodels
                var listaViewModel = _mapper.Map<List<CategoriaIndexViewModel>>(lista.Data);

                //pasamos a la vista la lista de productos por el model para que pueda mostrarlos  
                return View(listaViewModel);
            }
            catch (NotFoundException)
            {
                //model: pasar datos, dto, viewmodel, entities, listados, etc
                //vieBag: pasar datos simples, string, int, bool, etc
                //ViewData: pasar datos simples, string, int, bool, etc
                ViewBag.Mensaje = "No hay categorias disponibles.";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Errort al cargar la pagina.";
                return View();
            }
        }

        
        public async Task<ActionResult> DetailsAsync(int id)
        {
            var response = await _categoriaService.ObtenerPorIdAsync(id);
            if (!response.Success)
            {
                TempData["Mensaje"] = response.Message;
                return RedirectToAction(nameof(Index));
            }

            var categoriaVM = _mapper.Map<CategoriaIndexViewModel>(response.Data);


            return View(categoriaVM);
        }

        // GET: CategoriaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(CategoriaCreateViewModel productoVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(productoVM);
                }

                //mapear el viewmodel a un dto de creacion para enviarlo al servicio
                var categoriaCreateDTO = _mapper.Map<CategoriaCreateDTO>(productoVM);

                //llamar al servicio para crear el producto 
                var response = await _categoriaService.CrearAsync(categoriaCreateDTO);

                if (!response.Success)
                {
                    ModelState.AddModelError("", response.Message);
                    return View(productoVM);
                }

                TempData["Mensaje"] = "Categoria creada correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriaController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var response = await _categoriaService.ObtenerPorIdAsync(id);
            if (!response.Success)
            {
                TempData["Mensaje"] = response.Message;
                return RedirectToAction(nameof(Index));
            }

            var categoriaVM = _mapper.Map<CategoriaEditViewModel>(response.Data);


            return View(categoriaVM);
        }

        // POST: CategoriaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(CategoriaEditViewModel categoriaEditVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(categoriaEditVM);
                }
     
                var categoriaUpdateDTO = _mapper.Map<CategoriaUpdateDTO>(categoriaEditVM);
         
                var response = await _categoriaService.ActualizarAsync(categoriaUpdateDTO);

                if (!response.Success)
                {
                    ModelState.AddModelError("", response.Message);
                    return View(categoriaUpdateDTO);
                }

                TempData["Mensaje"] = "Categoria editada correctamente";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriaController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var response = await _categoriaService.ObtenerPorIdAsync(id);
            if (!response.Success)
            {
                TempData["Mensaje"] = response.Message;
                return RedirectToAction(nameof(Index));
            }

            var categoriaVM = _mapper.Map<CategoriaIndexViewModel>(response.Data);


            return View(categoriaVM);
        }

        // POST: CategoriaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedAsync(int id)
        {
            try
            {
                var response = await _categoriaService.EliminarAsync(id);
                if (!response.Success)
                {
                    TempData["Mensaje"] = response.Message;
                    return RedirectToAction(nameof(Index));
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
