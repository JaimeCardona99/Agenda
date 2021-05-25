using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Agenda.Models
{
    public class Acudiente
    {
        [Key]
        public int AcudienteId { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "El valor ingresado debe estar entre 6 y 15 caracteres")]
        [Index("IndexDocumento", IsUnique = true)]
        public string Documento { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El valor ingresado debe estar entre 3 y 50 caracteres")]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "El valor ingresado debe estar entre 3 y 50 caracteres")]
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Celular { get; set; }

        //Establecer la relacion con Aprendiz
        public virtual ICollection<Aprendiz> Aprendizs { get; set; }
    }
}