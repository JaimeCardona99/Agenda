using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Agenda.Models
{
    public class Aprendiz
    {
        [Key]
        public int AprendizId { get; set; }
        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        [StringLength(15,MinimumLength =6, ErrorMessage ="El valor ingresado debe estar entre 6 y 15 caracteres")]
        [Index("IndexDocumento", IsUnique = true)]
        public string Documento { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(35, MinimumLength = 3, ErrorMessage = "El campo {0}  debe tener entre {2} y {1} caracteres")]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(35, MinimumLength = 3, ErrorMessage = "El campo {0}  debe tener entre {2} y {1} caracteres")]
        public string Apellidos { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Correo { get; set; }
        public string Celular { get; set; }
        public int FichaId { get; set; } //Llave foranea de Ficha
        
        public virtual Ficha Ficha { get; set; } //Representacion virtual de la clase ficha

        //Relacion con acudiente
        public int AcudienteId { get; set; } //Llave foranea de Acudiente

        public virtual Acudiente Acudiente { get; set; } //Representacion virtual de la clase Acudiente

    }
}