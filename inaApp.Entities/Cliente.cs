using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inaApp.Entities
{
     public class Cliente
    {
        public int Id { get; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public  DateOnly FechaNacimiento { get; set; }
        private bool Estado { get; set; }

    }//end Cliente
}
