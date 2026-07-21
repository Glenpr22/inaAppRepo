using System.Threading.Tasks;
using System.Threading.Tasks;
using AutoMapper;
using inaApp.Common.Exception;
using inaApp.Common.interfaces;
using inaApp.Entities;
using inaApp.ProyectoINAApp.Models.Producto;
using inaApp.Service;
using inaAppDTOs.Categoria;
using inaAppDTOs.Producto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace inaApp.ProyectoINAApp.Controllers
{
    public class ProductoController : Controller
    {

        //dependencia de servicio generico para productos y categorias

        private readonly IGenericService<ProductoResponseDTO, ProductoCreateDTO, ProductoUpdateDTO> _productoService;
        private readonly IGenericService<CategoriaResponseDTO, CategoriaCreateDTO, CategoriaUpdateDTO> _categoriaService;
        private readonly IMapper _mapper;

        //constructor
        public ProductoController(IGenericService<ProductoResponseDTO, ProductoCreateDTO, ProductoUpdateDTO> productoServ,
            IGenericService<CategoriaResponseDTO, CategoriaCreateDTO, CategoriaUpdateDTO> categoriaServ
            , IMapper mapper)
        {
            _productoService = productoServ;
            _categoriaService = categoriaServ;
            _mapper = mapper;
        }

        // GET: ProductoController
        public async Task<ActionResult> Index()
        {
            try
            {
       
                var lista = await _productoService.ObtenerTodosAsync();

                //mapear la lista 
                var listaViewModel = _mapper.Map<List<ProductoIndexViewModel>>(lista.Data);

                //pasamos the view la lista de productos por el model
                return View(listaViewModel);
            }
            catch (NotFoundException)
            {
                //model: pasar datos, dto, viewmodel, entities, listados, etc
                //vieBag: pasar datos simples, string, int, bool, etc
                //ViewData: pasar datos simples, string, int, bool, etc
                ViewBag.Mensaje = "No hay productos disponibles.";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Errort al cargar la pagina.";
                return View();
            }
        }

        // GET: ProductoController/Details/5
        public async Task<ActionResult> DetailsAsync(int id)
        {
            var response = await _productoService.ObtenerPorIdAsync(id);
            if (!response.Success)
            {
                TempData["Mensaje"] = response.Message;
                return RedirectToAction(nameof(Index));
            }

            var productoVM = _mapper.Map<ProductoIndexViewModel>(response.Data);


            return View(productoVM);
        }


        // GET: ProductoController/Create
        public async Task<ActionResult> CreateAsync()
        {
            var categoriasResponse = await _categoriaService.ObtenerTodosAsync();
            var categoriasActivas = categoriasResponse.Data
                .Where(c => c.Estado)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Nombre
                });

            var productoVM = new ProductoCreateViewModel
            {
                Categorias = categoriasActivas
            };

            return View(productoVM);
        }

        // POST: ProductoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(ProductoCreateViewModel productoVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    // Recargar las categorías activas si hay errores de validación recorriendo el servicio de categorias para obtener
                    // las categorias activas y pasarlas al viewmodel para que se muestren en el dropdownlist(El combobox) de categorias
                    var categoriasResponse = await _categoriaService.ObtenerTodosAsync();
                    productoVM.Categorias = categoriasResponse.Data
                        .Where(c => c.Estado)
                        .Select(c => new SelectListItem
                        {
                            Value = c.Id.ToString(),
                            Text = c.Nombre
                        });

                    return View(productoVM);
                }

                var productoCreateDTO = _mapper.Map<ProductoCreateDTO>(productoVM);
                var response = await _productoService.CrearAsync(productoCreateDTO);

                if (!response.Success)
                {
                    ModelState.AddModelError("", response.Message);
                    return View(productoVM);
                }

                TempData["Mensaje"] = "Producto creado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductoController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var response = await _productoService.ObtenerPorIdAsync(id);
            if (!response.Success)
            {
                TempData["Mensaje"] = response.Message;
                return RedirectToAction(nameof(Index));
            }

            var productoVM = _mapper.Map<ProductoEditViewModel>(response.Data);

            // Cargar las categorías activas
            var categoriasResponse = await _categoriaService.ObtenerTodosAsync();
            productoVM.Categorias = categoriasResponse.Data
                .Where(c => c.Estado)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Nombre
                });

            return View(productoVM);
        }

        // POST: ProductoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(ProductoEditViewModel productoEditVM)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    // Recargar las categorías activas si hay errores de validación recorriendo el servicio de categorias para obtener
                    // las categorias activas y pasarlas al viewmodel para que se muestren en el dropdownlist(El combobox) de categorias
                    var categoriasResponse = await _categoriaService.ObtenerTodosAsync();
                    productoEditVM.Categorias = categoriasResponse.Data
                        .Where(c => c.Estado)
                        .Select(c => new SelectListItem
                        {
                            Value = c.Id.ToString(),
                            Text = c.Nombre
                        });

                    return View(productoEditVM);
                }

                var productoUpdateDTO = _mapper.Map<ProductoUpdateDTO>(productoEditVM);
                var response = await _productoService.ActualizarAsync(productoUpdateDTO);

                if (!response.Success)
                {
                    ModelState.AddModelError("", response.Message);
                    return View(productoEditVM);
                }

                TempData["Mensaje"] = "Producto editado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }

        }

        // GET: ProductoController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var response = await _productoService.ObtenerPorIdAsync(id);
            if (!response.Success)
            {
                TempData["Mensaje"] = response.Message;
                return RedirectToAction(nameof(Index));
            }

            var productoVM = _mapper.Map<ProductoIndexViewModel>(response.Data);


            return View(productoVM);
        }

        // POST: ProductoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {

                var response = await _productoService.EliminarAsync(id);
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

