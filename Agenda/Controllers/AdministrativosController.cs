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
    public class AdministrativosController : Controller
    {
        //Creando un Objeto del contexto
        private AgendaContext db = new AgendaContext();
        // GET: Administrativos
        [HttpGet]
        public ActionResult Index()
        {
            var administrativos = db.Administrativos.Include(a => a.Centro);//Recupera la relacion entre Ficha y Centro
            var administrativosProfesion = db.Administrativos.Include(a => a.Profesion);
            return View(db.Administrativos.ToList()); //SELECT * FROM Fichas
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.CentroId = new SelectList(db.Centros, "CentroId", "Nombre");//Llenar el ViewBag con centros buscando por Id y mostrando por Codigo
            ViewBag.ProfesionId = new SelectList(db.Profesiones, "ProfesionId", "Nombre");
            return View();
        }
        [HttpPost]
        public ActionResult Create(Administrativo administrativo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Administrativos.Add(administrativo);//INSERT INTO
                    db.SaveChanges(); //Se genera el valor Identity para el campo AdministrativoId, Guarda la informacion 
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null &&
                        ex.InnerException.InnerException.Message.Contains("IndexDocumento"))
                    {
                        ViewBag.Error = "El documento ya se encuentra registrado";
                        ViewBag.CentroId = new SelectList(db.Centros, "CentroId", "Nombre", administrativo.CentroId);
                        ViewBag.ProfesionId = new SelectList(db.Profesiones, "ProfesionId", "Nombre", administrativo.ProfesionId);
                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                    }
                    return View(administrativo);
                }
                ViewBag.CentroId = new SelectList(db.Centros, "CentroId", "Nombre", administrativo.CentroId);
                ViewBag.ProfesionId = new SelectList(db.Profesiones, "ProfesionId", "Nombre", administrativo.ProfesionId);
                return RedirectToAction("Index");
            }
            return View(administrativo);
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrativo administrativo = db.Administrativos.Find(id); //SELECT FROM ADMINISTRATIVOS WHERE AdministrativoId = id, Find consulta por llave primaria
            if (administrativo.Equals(null))
            {
                return HttpNotFound();
            }
            return View(administrativo);
        }
        [HttpPost]
        public ActionResult Edit(Administrativo administrativo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(administrativo).State = EntityState.Modified; //UPDATE 
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null &&
                        ex.InnerException.InnerException.Message.Contains("IndexDocumento"))
                    {
                        ViewBag.Error = "El documento ya se encuentra registrado";
                        ViewBag.CentroId = new SelectList(db.Centros, "CentroId", "Nombre", administrativo.CentroId);
                        ViewBag.ProfesionId = new SelectList(db.Profesiones, "ProfesionId", "Nombre", administrativo.ProfesionId);

                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                    }
                    return View(administrativo);
                }
                ViewBag.CentroId = new SelectList(db.Centros, "CentroId", "Nombre", administrativo.CentroId);
                ViewBag.ProfesionId = new SelectList(db.Profesiones, "ProfesionId", "Nombre", administrativo.ProfesionId);
                return RedirectToAction("Index");
            }
            return View(administrativo);
        }
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrativo administrativo = db.Administrativos.Find(id); 
            if (administrativo.Equals(null))
            {
                return HttpNotFound();
            }
            return View(administrativo);
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrativo administrativo = db.Administrativos.Find(id);  //SELECT FROM FICHA WHERE FichaId = id, Find consulta por llave primaria
            if (administrativo.Equals(null))
            {
                return HttpNotFound();
            }
            return View(administrativo);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            //Ficha ficha = db.Fichas.Find(id);
            var administrativo = db.Administrativos.Find(id);
            try
            {
                db.Administrativos.Remove(administrativo); //Delete FROM Administrativo where AdministrativoId = Id
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
                return View(administrativo);
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
