using inaApp.Common.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static inaApp.Common.Enums.Enums;

namespace inaApp.Entities
{
    // This class represents the entity of the client  
    [Index(nameof(TipoIdentificacion), nameof(NumeroIdentificacion), IsUnique = true)]
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tipo de identificacion es obligatorio.")]
        //
        public TipoIdentificacion TipoIdentificacion { get; set; }

        [Required(ErrorMessage = "El numero de identificacion es obligatorio.")]
        [MaxLength(20, ErrorMessage = "El numero de identificacion no puede superar los 20 caracteres")]
        public string NumeroIdentificacion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre del cliente es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El primer apellido del cliente es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El primer apellido no puede superar los 50 caracteres.")]
        public string PrimerApellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "El segundo apellido del cliente es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El segundo apellido no puede superar los 50 caracteres.")]
        public string? SegundoApellido { get; set; }

        [EmailAddress]
        [MaxLength(150)]
        public string? CorreoElectronico { get; set; }

        [Phone]
        [MaxLength(20)]
        public string? Telefono { get; set; }

        public bool Estado { get; set; } = true;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

    }//end class

}//end namespace
