using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inaApp.Entities
{
    [Table(name: "tbCliente")]
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public  DateOnly FechaNacimiento { get; set; }
        private bool Estado { get; set; }

    }//end Cliente
}
