using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace inaApp.ProyectoINAApp.Models.Producto
{
    public class ProductoEditViewModel
    {
        [Required(ErrorMessage = "El Id del producto es obligatorio")]
        public int Id { get; set; }

        [Display(Name = "Nombre del producto")]
        [Required(ErrorMessage = "El nombre del producto es obligatorio")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Display(Name = "Precio del producto")]
        [Required(ErrorMessage = "El precio del producto es obligatorio")]
        [Range(0.1, 10000.00, ErrorMessage = "El nombre debe tener entre 0.01 y 10000.00.")]
        public decimal Precio { get; set; } = 0;

        [Display(Name = "Stock del producto")]
        [Required(ErrorMessage = "El stock del producto es obligatorio")]
        [Range(0, 1000, ErrorMessage = "El nombre debe tener entre 0 y 1000")]
        public int Stock { get; set; }

        [Display(Name = "Descripcion del producto")]
        [StringLength(500, ErrorMessage = "No se puede crear una descripcion de mas de 500 caracteres")]
        public string? Descripcion { get; set; }


        [Display(Name = "Categoría")]
        [Required(ErrorMessage = "Debe seleccionar una categoría.")]
        public int CategoriaId { get; set; }
      
        public IEnumerable<SelectListItem>? Categorias { get; set; }
    }
}
