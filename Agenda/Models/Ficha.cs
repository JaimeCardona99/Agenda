using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Agenda.Models
{
    public class Ficha
    {
        [Key]
        public int FichaId { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(12, MinimumLength = 3, ErrorMessage = "El valor ingresado debe estar entre {1} y {0} caracteres")]
        [Index("IndexCodigo", IsUnique = true)]
        public string Codigo { get; set; }
        public string Especialidad { get; set; }
        public int CentroId { get; set; } //Llave foranea de Centro

        public virtual Centro Centro { get; set; } //Representacion virtual de la clase Centro

        //Establecer la relacion con Aprendiz
        public virtual ICollection<Aprendiz> Aprendizs { get; set; }

    }
}