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
    public class CentrosController : Controller
    {
        //Creando un Objeto del contexto
        private AgendaContext db = new AgendaContext();
        // GET: Centros
        [HttpGet]
        public ActionResult Index()
        {
            return View(db.Centros.ToList()); //SELECT * FROM Centros
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Centro centro)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Centros.Add(centro);//INSERT INTO
                    db.SaveChanges(); //Se genera el valor Identity para el campo FichaId, Guarda la informacion 
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null &&
                        ex.InnerException.InnerException.Message.Contains("IndexNombre"))
                    {
                        ViewBag.Error = "El centro ya se encuentra registrado";
                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                    }
                    return View(centro);
                }
                return RedirectToAction("Index");
            }
            return View(centro);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Centro centro = db.Centros.Find(id); //SELECT FROM CENTROS WHERE CentroId = id, Find consulta por llave primaria
            if (centro.Equals(null))
            {
                return HttpNotFound();
            }
            return View(centro);
        }
        [HttpPost]
        public ActionResult Edit(Centro centro)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(centro).State = EntityState.Modified; //UPDATE 
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null &&
                        ex.InnerException.InnerException.Message.Contains("IndexNombre"))
                    {
                        ViewBag.Error = "El centro ya se encuentra registrado";
                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                    }
                    return View(centro);
                }
                return RedirectToAction("Index");
            }
            return View(centro);
        }
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Centro centro = db.Centros.Find(id); //SELECT FROM CENTROS WHERE CentroId = id, Find consulta por llave primaria
            if (centro.Equals(null))
            {
                return HttpNotFound();
            }
            return View(centro);
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Centro centro = db.Centros.Find(id); //SELECT FROM CENTROS WHERE CentroId = id, Find consulta por llave primaria
            if (centro.Equals(null))
            {
                return HttpNotFound();
            }
            return View(centro);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            //Ficha ficha = db.Fichas.Find(id);
            var centro = db.Centros.Find(id);
            try
            {
                db.Centros.Remove(centro); //Delete FROM Centros where CentroId = Id
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
                return View(centro);
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