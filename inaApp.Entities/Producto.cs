using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inaApp.Entities
{
    // niveles: acceso
    //
    [Table(name:"tbProducto")]
    public class Producto
    {
        //propiedades o atributos class Producto


        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string Descripcion { get; set; }

        public bool Estado { get; set; }


    }//end 
}
