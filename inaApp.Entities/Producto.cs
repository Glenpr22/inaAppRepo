using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inaApp.Entities
{
    // niveles: acceso
    //
    public class Producto
    {
        //propiedades o atributos class Producto
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string Descripcion { get; set; }

        public bool Estado { get; set; }


    }//end 
}
