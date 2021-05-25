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
    public class ProfesionesController : Controller
    {
        //Creando un Objeto del contexto
        private AgendaContext db = new AgendaContext();
        // GET: Profesion
        [HttpGet]
        public ActionResult Index()
        {
            return View(db.Profesiones.ToList()); //SELECT * FROM Profesion
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Profesion profesion)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Profesiones.Add(profesion);//INSERT INTO
                    db.SaveChanges(); //Se genera el valor Identity para el campo ProfesionId, Guarda la informacion 
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null &&
                        ex.InnerException.InnerException.Message.Contains("IndexNombre"))
                    {
                        ViewBag.Error = "La profesion ya se encuentra registrada";
                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                    }
                    return View(profesion);
                }
                return RedirectToAction("Index");
            }
            return View(profesion);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profesion profesion = db.Profesiones.Find(id); //SELECT FROM PROFESION WHERE ProfesionId = id, Find consulta por llave primaria
            if (profesion.Equals(null))
            {
                return HttpNotFound();
            }
            return View(profesion);
        }
        [HttpPost]
        public ActionResult Edit(Profesion profesion)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(profesion).State = EntityState.Modified; //UPDATE 
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null &&
                        ex.InnerException.InnerException.Message.Contains("IndexNombre"))
                    {
                        ViewBag.Error = "La profesion ya se encuentra registrada";
                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                    }
                    return View(profesion);
                }
                return RedirectToAction("Index");
            }
            return View(profesion);
        }
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profesion profesion = db.Profesiones.Find(id); //SELECT FROM PROFESION WHERE ProfesionId = id, Find consulta por llave primaria
            if (profesion.Equals(null))
            {
                return HttpNotFound();
            }
            return View(profesion);
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profesion profesion = db.Profesiones.Find(id); //SELECT FROM CENTROS WHERE CentroId = id, Find consulta por llave primaria
            if (profesion.Equals(null))
            {
                return HttpNotFound();
            }
            return View(profesion);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            //Ficha ficha = db.Fichas.Find(id);
            var profesion = db.Profesiones.Find(id);
            try
            {
                db.Profesiones.Remove(profesion); //Delete FROM Profesion where ProfesionId = Id
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
                return View(profesion);
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