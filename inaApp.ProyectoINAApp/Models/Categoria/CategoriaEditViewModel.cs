namespace inaApp.ProyectoINAApp.Models.Categoria;
using System.ComponentModel.DataAnnotations;

public class CategoriaEditViewModel
{
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre de la categoría es obligatorio.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    [Display(Name = "Estado")]
    public bool Estado { get; set; }
}
