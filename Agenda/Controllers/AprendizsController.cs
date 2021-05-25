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
    public class AprendizsController : Controller
    {
        //Creando un Objeto del contexto
        private AgendaContext db = new AgendaContext();
        // GET: Aprendizs
        [HttpGet]   
        public ActionResult Index()
        {
            var aprendizs = db.Aprendizs.Include(a => a.Ficha);//Recupera la relacion entre aprenfiz y ficha
            var aprendizsAcu = db.Aprendizs.Include(a => a.Acudiente);
            return View(db.Aprendizs.ToList()); //SELECT * FROM Aprendizs
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.FichaId = new SelectList(db.Fichas, "FichaId", "Codigo");//Llenar el ViewBag con fichas buscando por Id y mostrando por Codigo
            ViewBag.AcudienteId = new SelectList(db.Acudientes, "AcudienteId", "Nombres");
            return View();
        }
        /*[HttpPost]
        public ActionResult Create(Aprendiz aprendiz)
        {
            if (ModelState.IsValid)
            {
                //bool existe = ValidarDocumento(aprendiz);//Preguntar si el documento ya existe
                bool existe = ValidarCedula(aprendiz);
                if (existe)
                {
                    ViewBag.Error = "El documento ya se encuentra registrado";
                    ViewBag.FichaId = new SelectList(db.Fichas, "FichaId", "Codigo", aprendiz.FichaId);
                    return View(aprendiz);

                }
                db.Aprendizs.Add(aprendiz);//INSERT INTO
                db.SaveChanges(); //Se genera el valor Identity para el campo AprendizId, Guarda la informacion      
            }
            else
            {
                return View(aprendiz);
            }
            ViewBag.FichaId = new SelectList(db.Fichas, "FichaId", "Codigo", aprendiz.FichaId);
            return RedirectToAction("Index");

        }*/
        [HttpPost]
        public ActionResult Create(Aprendiz aprendiz)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Aprendizs.Add(aprendiz);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null &&
                        ex.InnerException.InnerException.Message.Contains("IndexDocumento"))
                    {
                        ViewBag.Error = "El documento ya se encuentra registrado";
                        ViewBag.FichaId = new SelectList(db.Fichas, "FichaId", "Codigo", aprendiz.FichaId);
                        ViewBag.AcudienteId = new SelectList(db.Acudientes, "AcudienteId", "Nombres", aprendiz.AcudienteId);
                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                    }
                    return View(aprendiz);
                }
                ViewBag.FichaId = new SelectList(db.Fichas, "FichaId", "Codigo", aprendiz.FichaId);
                ViewBag.AcudienteId = new SelectList(db.Acudientes, "AcudienteId", "Nombres", aprendiz.AcudienteId);
                return RedirectToAction("Index");
            }

            return View(aprendiz);

        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if(id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aprendiz aprendiz = db.Aprendizs.Find(id); //SELECT FROM APRENDIZ WHERE AprendizId = id
            if(aprendiz.Equals(null))
            {
                return HttpNotFound();
            }
            ViewBag.FichaId = new SelectList(db.Fichas, "FichaId", "Codigo", aprendiz.FichaId);
            return View(aprendiz);
        }
        [HttpPost]
        public ActionResult Edit(Aprendiz aprendiz)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    db.Entry(aprendiz).State = EntityState.Modified; //UPDATE 
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null &&
                        ex.InnerException.InnerException.Message.Contains("IndexDocumento"))
                    {
                        ViewBag.Error = "El documento ya se encuentra registrado";
                        ViewBag.FichaId = new SelectList(db.Fichas, "FichaId", "Codigo", aprendiz.FichaId);
                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                    }
                    return View(aprendiz);
                }
                ViewBag.FichaId = new SelectList(db.Fichas, "FichaId", "Codigo", aprendiz.FichaId);
                ViewBag.AcudienteId = new SelectList(db.Acudientes, "AcudienteId", "Nombres", aprendiz.AcudienteId);
                return RedirectToAction("Index");
            }

            return View(aprendiz);

        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aprendiz aprendiz = db.Aprendizs.Find(id); //SELECT FROM APRENDIZ WHERE AprendizId = id
            if (aprendiz.Equals(null))
            {
                return HttpNotFound();
            }
            return View(aprendiz);
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aprendiz aprendiz = db.Aprendizs.Find(id); //SELECT FROM APRENDIZ WHERE AprendizId = id
            if (aprendiz.Equals(null))
            {
                return HttpNotFound();
            }
            return View(aprendiz);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            //Aprendiz aprendiz = db.Aprendizs.Find(id);
            var aprendiz = db.Aprendizs.Find(id);
            if(aprendiz.Equals(null))
            {
                return HttpNotFound();
            }
            else
            {
                db.Aprendizs.Remove(aprendiz); //Delete FROM Aprendizs where AprendizId = Id
                db.SaveChanges();      
            }
            return RedirectToAction("Index");
        }
        //Metodo para impedir duplicados en el campo Documento
        /*public bool ValidarDocumento(Aprendiz objAprendiz)
        {
            bool ex = false;
            List<Aprendiz> lista = db.Aprendizs.ToList(); // almacena en la variable lista todos los aprendice que hay en la base de datos
            foreach (Aprendiz objApr in lista)
            {
                if (objApr.Documento == objAprendiz.Documento)
                {
                    ex = true;
                    break;
                }
            }

            return ex;
        }*/
        public bool ValidarCedula(Aprendiz objAprendiz)
        {
            bool existe = false;
            var apr = db.Aprendizs.Where(ap => ap.Documento == objAprendiz.Documento).FirstOrDefault();
            if (apr != null)
            {
                existe = true;
            }
            return existe;
        }
    

        //Metodo o Accion para cerrar la cadena de conexión con la base de datos
        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}