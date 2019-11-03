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
        public ActionResult Index(){
      Session["perfil"] = "medico";
            var citas = db.citas.Include(c => c.usuarios).Include(c => c.usuarios1);
            return View(citas.ToList());
        }

        // GET: citas/Details/5
        public ActionResult Details(int? id)
        {
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
            ViewBag.paciente = new SelectList(db.usuarios, "id", "nombres");
            ViewBag.medico = new SelectList(db.usuarios, "id", "nombres");
            return View();
        }

        // POST: citas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,medico,paciente,fecha,estado")] citas citas)
        {
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
        public ActionResult Edit([Bind(Include = "id,medico,paciente,fecha,estado")] citas citas)
        {
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
            db.citas.Remove(citas);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
