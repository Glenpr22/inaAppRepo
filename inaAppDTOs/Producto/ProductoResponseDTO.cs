using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inaAppDTOs.Producto
{
    public class ProductoResponseDTO
    {
        //devolucion de datos the customer
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string? Descripcion { get; set; }
        public int CategoriaId { get; set; }
        public string CategoriaNombre { get; set; }

    }//end 
}
