using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inaApp.Entities
{
     public class Clliente
    {
        private int Id { get; }
        private string Nombre { get; set; }
        private string Apellido1 { get; set; }
        private string Apellido2 { get; set; }
        private DateTime FechaNac { get; set; }
        private bool Estado { get; set; }

    }//end Cliente
}
