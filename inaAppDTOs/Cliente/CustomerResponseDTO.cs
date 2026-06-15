using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static inaApp.Common.Enums.Enums;

namespace Pratice.DTO.Cliente
{
    public class CustomerResponseDTO
    {

        public int Id { get; set; }

        public TipoIdentificacion TipoIdentificacion { get; set; }

        public string NumeroIdentificacion { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;

        public string PrimerApellido { get; set; } = string.Empty;

        public string? SegundoApellido { get; set; }

        public string? CorreoElectronico { get; set; }
        public string? Telefono { get; set; }

    }//END CLASS
}
