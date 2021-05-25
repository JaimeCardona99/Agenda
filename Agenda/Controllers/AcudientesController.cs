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
    public class AcudientesController : Controller
    {
        //Creando un Objeto del contexto
        private AgendaContext db = new AgendaContext();
        // GET: Acudiente
        [HttpGet]
        public ActionResult Index()
        {
            return View(db.Acudientes.ToList()); //SELECT * FROM Acudiente
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Acudiente acudiente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Acudientes.Add(acudiente);//INSERT INTO
                    db.SaveChanges(); //Se genera el valor Identity para el campo FichaId, Guarda la informacion  
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null &&
                        ex.InnerException.InnerException.Message.Contains("IndexDocumento"))
                    {
                        ViewBag.Error = "El documento ya se encuentra registrado";
                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(acudiente);
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Acudiente acudiente = db.Acudientes.Find(id); //SELECT FROM ACUDIENTES WHERE AcudienteId = id, Find consulta por llave primaria
            if (acudiente.Equals(null))
            {
                return HttpNotFound();
            }
            return View(acudiente);
        }
        [HttpPost]
        public ActionResult Edit(Acudiente acudiente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(acudiente).State = EntityState.Modified; //UPDATE 
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null &&
                        ex.InnerException.InnerException.Message.Contains("IndexDocumento"))
                    {
                        ViewBag.Error = "El documento ya se encuentra registrado";
                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                    }
                    return View(acudiente);
                }
                return RedirectToAction("Index");
            }

            return View(acudiente);
        }
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Acudiente acudiente = db.Acudientes.Find(id); //SELECT FROM ACUDIENTES WHERE AcudienteId = id, Find consulta por llave primaria
            if (acudiente.Equals(null))
            {
                return HttpNotFound();
            }
            return View(acudiente);
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Acudiente acudiente = db.Acudientes.Find(id);
            if (acudiente.Equals(null))
            {
                return HttpNotFound();
            }
            return View(acudiente);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            //Ficha ficha = db.Fichas.Find(id);
            var acudiente = db.Acudientes.Find(id);
            try
            {
                db.Acudientes.Remove(acudiente); //Delete FROM ACUDIENTES where AcudienteId = Id
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
                return View(acudiente);
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
