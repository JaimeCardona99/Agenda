using Agenda.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Agenda.Controllers
{
    public class InstructoresController : Controller
    {
        //Creando un Objeto del contexto
        private AgendaContext db = new AgendaContext();
        // GET: Administrativos
        [HttpGet]
        public ActionResult Index()
        {
            var instructorProfesion = db.Instructores.Include(a => a.Profesion);//Recupera la relacion entre Ficha y Centro
            return View(db.Instructores.ToList()); //SELECT * FROM Fichas
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ProfesionId = new SelectList(db.Profesiones, "ProfesionId", "Nombre");
            return View();
        }
        [HttpPost]
        public ActionResult Create(Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Instructores.Add(instructor);//INSERT INTO
                    db.SaveChanges(); //Se genera el valor Identity para el campo AdministrativoId, Guarda la informacion 
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null &&
                        ex.InnerException.InnerException.Message.Contains("IndexDocumento"))
                    {
                        ViewBag.Error = "El documento ya se encuentra registrado";
                        ViewBag.ProfesionId = new SelectList(db.Profesiones, "ProfesionId", "Nombre", instructor.ProfesionId);
                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                    }
                    return View(instructor);
                }
                ViewBag.ProfesionId = new SelectList(db.Profesiones, "ProfesionId", "Nombre", instructor.ProfesionId);
                return RedirectToAction("Index");
            }
            return View(instructor);
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = db.Instructores.Find(id); //SELECT FROM INSTRUCTOR WHERE InstructorId = id, Find consulta por llave primaria
            if (instructor.Equals(null))
            {
                return HttpNotFound();
            }
            return View(instructor);
        }
        [HttpPost]
        public ActionResult Edit(Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(instructor).State = EntityState.Modified; //UPDATE 
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null &&
                        ex.InnerException.InnerException.Message.Contains("IndexDocumento"))
                    {
                        ViewBag.Error = "El documento ya se encuentra registrado";
                        ViewBag.ProfesionId = new SelectList(db.Profesiones, "ProfesionId", "Nombre", instructor.ProfesionId);

                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                    }
                    return View(instructor);
                }
                ViewBag.ProfesionId = new SelectList(db.Profesiones, "ProfesionId", "Nombre", instructor.ProfesionId);
                return RedirectToAction("Index");
            }
            return View(instructor);
        }
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = db.Instructores.Find(id);
            if (instructor.Equals(null))
            {
                return HttpNotFound();
            }
            return View(instructor);
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = db.Instructores.Find(id);  //SELECT FROM FICHA WHERE FichaId = id, Find consulta por llave primaria
            if (instructor.Equals(null))
            {
                return HttpNotFound();
            }
            return View(instructor);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            //Ficha ficha = db.Fichas.Find(id);
            var instructor = db.Instructores.Find(id);
            try
            {
                db.Instructores.Remove(instructor); //Delete FROM Instructor where InstructorId = Id
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.InnerException != null &&
                    ex.InnerException.InnerException.Message.Contains("REFERENCE"))
                {
                    ViewBag.Error = "No se pueden eliminar elementos con integridad referencial";
                }
                else
                {
                    ViewBag.Error = ex.Message;
                }
                return View(instructor);
            }
            return RedirectToAction("Index");
        }


        //Metodo o Accion para cerrar la cadena de conexión con la base de datos
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}