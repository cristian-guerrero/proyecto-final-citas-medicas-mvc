using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CitasMedicasMVC;

namespace CitasMedicasMVC.Controllers
{
  public class citasController : Controller
  {
    private citas_medicasEntities2 db = new citas_medicasEntities2();

    // GET: citas
    public ActionResult Index()
    {
      if (notSession())
        return RedirectToAction("Index", "Login");


      //Session["perfil"] = "medico";
      IQueryable<citas> citas;
      int userId = (int)Session["_user_id"];
      string perfil = Session["user_profile"].ToString();
      if (perfil == "medico")
      {
        citas = db.citas
          .Where(s => s.medico == userId)
           .Where(s =>  s.estado == "activo")
           .Include(c => c.usuarios).Include(c => c.usuarios1);
      }
      else if (perfil == "paciente")
      {
        citas = db.citas.Where(s => s.paciente == userId)
           .Include(c => c.usuarios).Include(c => c.usuarios1);
      }
      else
      {
        citas = db.citas;
      }


      return View(citas.ToList());
    }

    // GET: citas/Details/5
    public ActionResult Details(int? id)
    {
      if (notSession())
        return RedirectToAction("Index", "Login");


      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      citas citas = db.citas.Find(id);
      if (citas == null)
      {
        return HttpNotFound();
      }
      return View(citas);
    }

    // GET: citas/Create
    public ActionResult Create()
    {
      if (notSession())
        return RedirectToAction("Index", "Login");

      //  ViewBag.paciente = new SelectList(db.usuarios, "id", "nombres");

      var usuarios = db.usuarios.Where(s => s.perfil == "medico");
      ViewBag.medico = new SelectList(usuarios,
   "id", "nombres");

      return View();
    }

    // POST: citas/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "id,medico,paciente,fecha,estado")] citas citas)
    {
      var id = (int)Session["_user_id"];
      citas.paciente = id ;
      if (ModelState.IsValid)
      {
        db.citas.Add(citas);
        db.SaveChanges();
        return RedirectToAction("Index");
      }

      ViewBag.paciente = new SelectList(db.usuarios, "id", "nombres", citas.paciente);
      ViewBag.medico = new SelectList(db.usuarios, "id", "nombres", citas.medico);
      return View(citas);
    }

    // GET: citas/Edit/5
    public ActionResult Edit(int? id)
    {
      if (notSession())
        return RedirectToAction("Index", "Login");

      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      citas citas = db.citas.Find(id);
      if (citas == null)
      {
        return HttpNotFound();
      }
      ViewBag.paciente = new SelectList(db.usuarios, "id", "nombres", citas.paciente);
      ViewBag.medico = new SelectList(db.usuarios, "id", "nombres", citas.medico);
      return View(citas);
    }

    // POST: citas/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "id,medico,paciente,fecha,estado,detalles")] citas citas)
    {
      var id = (int)Session["_user_id"];
      citas.paciente = id;
      if (ModelState.IsValid)
      {
        db.Entry(citas).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      ViewBag.paciente = new SelectList(db.usuarios, "id", "nombres", citas.paciente);
      ViewBag.medico = new SelectList(db.usuarios, "id", "nombres", citas.medico);
      return View(citas);
    }

    // GET: citas/Delete/5
    public ActionResult Delete(int? id)
    {
      if (notSession())
        return RedirectToAction("Index", "Login");

      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      citas citas = db.citas.Find(id);
      if (citas == null)
      {
        return HttpNotFound();
      }
      return View(citas);
    }

    // POST: citas/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      citas citas = db.citas.Find(id);
      string errorm = "No se pudo cancelar la cita, ";

      if (citas != null && citas.fecha.HasValue)
      {
        var now = DateTime.Now;
        var fecha = citas.fecha ?? now;

        if (fecha.Subtract(now) >= TimeSpan.FromMinutes(120))
        {

          citas.estado = "cancelado";
          db.Entry(citas).State = EntityState.Modified;
          db.SaveChanges();
          db.SaveChanges();
          return RedirectToAction("Index");
        }
        errorm += "solo se puede cancelar 2 horas antes";
      }

      ViewBag.DateError = errorm;
      return View(citas);

     
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        db.Dispose();
      }
      base.Dispose(disposing);
    }

    private bool notSession()
    {
      return Session["user_name"] == null && Session["user_profile"] == null;


    }
  }
}
