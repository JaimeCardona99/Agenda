using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Agenda.Models
{
    public class Centro
    {
        [Key]
        public int CentroId { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(50, MinimumLength = 15, ErrorMessage = "El valor ingresado debe estar entre {1} y {0} caracteres")]
        [Index("IndexNombre", IsUnique = true)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Subdirector { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Coordinador { get; set; }

        //Establecer la relacion con Ficha
        public virtual ICollection<Ficha> Fichas { get; set; }

        //Establecer la relacion con Administrativo
        public virtual ICollection<Administrativo> Administrativos { get; set; }
    }
}