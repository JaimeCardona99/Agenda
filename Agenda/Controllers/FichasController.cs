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
    [Authorize]
    public class FichasController : Controller
    {
        //Creando un Objeto del contexto
        private AgendaContext db = new AgendaContext();
        // GET: Fichas
        [HttpGet]
        public ActionResult Index()
        {
            var fichas = db.Fichas.Include(a => a.Centro);//Recupera la relacion entre Ficha y Centro
            return View(db.Fichas.ToList()); //SELECT * FROM Fichas
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.CentroId = new SelectList(db.Centros, "CentroId", "Nombre");//Llenar el ViewBag con centros buscando por Id y mostrando por Codigo
            return View();
        }
        [HttpPost]
        public ActionResult Create(Ficha ficha)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Fichas.Add(ficha);//INSERT INTO
                    db.SaveChanges(); //Se genera el valor Identity para el campo FichaId, Guarda la informacion 
                }
                catch(Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null &&
                        ex.InnerException.InnerException.Message.Contains("IndexCodigo"))
                    {
                        ViewBag.Error = "El codigo de Ficha ya se encuentra registrado";
                        ViewBag.CentroId = new SelectList(db.Centros, "CentroId", "Nombre", ficha.CentroId);
                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                    }
                    return View(ficha);
                }
                ViewBag.CentroId = new SelectList(db.Centros, "CentroId", "Nombre", ficha.CentroId);
                return RedirectToAction("Index");
            }
            return View(ficha);
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ficha ficha = db.Fichas.Find(id); //SELECT FROM FICHA WHERE FichaId = id, Find consulta por llave primaria
            if (ficha.Equals(null))
            {
                return HttpNotFound();
            }
            return View(ficha);
        }
        [HttpPost]
        public ActionResult Edit(Ficha ficha)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(ficha).State = EntityState.Modified; //UPDATE 
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null &&
                        ex.InnerException.InnerException.Message.Contains("IndexCodigo"))
                    {
                        ViewBag.Error = "El centro ya se encuentra registrado";
                        ViewBag.CentroId = new SelectList(db.Centros, "CentroId", "Nombre", ficha.CentroId);
                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                    }
                    return View(ficha);
                }
                ViewBag.CentroId = new SelectList(db.Centros, "CentroId", "Nombre", ficha.CentroId);
                return RedirectToAction("Index");
            }
            return View(ficha);
        }
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ficha ficha = db.Fichas.Find(id); //SELECT FROM FICHA WHERE FichaId = id, Find consulta por llave primaria
            if (ficha.Equals(null))
            {
                return HttpNotFound();
            }
            return View(ficha);
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ficha ficha = db.Fichas.Find(id); //SELECT FROM FICHA WHERE FichaId = id, Find consulta por llave primaria
            if (ficha.Equals(null))
            {
                return HttpNotFound();
            }
            return View(ficha);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            //Ficha ficha = db.Fichas.Find(id);
            var ficha = db.Fichas.Find(id);
            try
            {
                db.Fichas.Remove(ficha); //Delete FROM Fichas where FichaId = Id
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex.InnerException!=null && ex.InnerException.InnerException!= null &&
                    ex.InnerException.InnerException.Message.Contains("REFERENCE"))
                {
                    ViewBag.Error = "No se pueden eliminar elementos con integridad referencial";
                }
                else
                {
                    ViewBag.Error = ex.Message;
                }
                return View(ficha);
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
