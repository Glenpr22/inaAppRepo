using inaAppDTOs.Producto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inaAppDTOs.Categoria
{
    public class CategoriaResponseDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public bool Estado { get; set; }

        public ICollection<ProductoResponseDTO> Productos { get; set; } = new List<ProductoResponseDTO>();
    }
}
