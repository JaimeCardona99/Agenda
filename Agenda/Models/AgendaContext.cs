using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Agenda.Models
{
    public class AgendaContext:DbContext
    {
        //Constructor por Defecto
        public AgendaContext()
            :base("DefaultConnection")//Llamado al Contructor de la base DbContext
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        //Definir los atributos y/o propiedades
        public DbSet<Aprendiz> Aprendizs { get; set; }

        public DbSet<Ficha> Fichas { get; set; }

        public DbSet<Acudiente> Acudientes { get; set; }

        public DbSet<Centro> Centros { get; set; }

        public DbSet<Administrativo> Administrativos { get; set; }

        public DbSet<Profesion> Profesiones { get; set; }

        public DbSet<Instructor> Instructores { get; set; }
    }
}