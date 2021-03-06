using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Agenda.Models
{
    public class Administrativo
    {
        [Key]
        public int AdministrativoId { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "El valor ingresado debe estar entre 6 y 15 caracteres")]
        [Index("IndexDocumento", IsUnique = true)]
        public string Documento { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El valor ingresado debe estar entre 3 y 50 caracteres")]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El valor ingresado debe estar entre 3 y 50 caracteres")]
        public string Apellidos { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }

        public int CentroId { get; set; } //Llave foranea de Centro

        public virtual Centro Centro { get; set; } //Representacion virtual de la clase Centro

        public int ProfesionId { get; set; } //Llave foranea de Profesion

        public virtual Profesion Profesion { get; set; } //Representacion virtual de la clase Profesion
    }
}