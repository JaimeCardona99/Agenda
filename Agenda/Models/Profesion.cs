using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Agenda.Models
{
    public class Profesion
    {
        [Key]
        public int ProfesionId { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El valor ingresado debe estar entre 3 y 50 caracteres")]
        [Index("IndexNombre", IsUnique = true)]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        //Establecer la relacion con Instructor
        public virtual ICollection<Instructor> Instructores { get; set; }

        //Establecer la relacion con Administrativo
        public virtual ICollection<Administrativo> Administrativos { get; set; }
    }
}