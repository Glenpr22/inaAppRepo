using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static inaApp.Common.Enums.Enums;

namespace Pratice.DTO.Cliente
{
    public class CustomerCreateDTO
    {

        [Required(ErrorMessage = "Tipo de identificacion es obligatorio.")]

        public TipoIdentificacion TipoIdentificacion { get; set; }

        [Required(ErrorMessage = "El numero de identificacion es obligatorio.")]

        public string NumeroIdentificacion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre del cliente es obligatorio.")]

        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El primer apellido del cliente es obligatorio.")]

        public string PrimerApellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "El segundo apellido del cliente es obligatorio.")]
        public string? SegundoApellido { get; set; }

        [EmailAddress]
        public string? CorreoElectronico { get; set; }

        [Phone]
        public string? Telefono { get; set; }

    }//end
}
