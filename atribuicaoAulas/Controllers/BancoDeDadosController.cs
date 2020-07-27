using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using atribuicaoAulas;

namespace atribuicaoAulas.Controllers
{
    public class BancoDeDadosController : Controller
    {
        private PerfilDB_ db = new PerfilDB_();

        // GET: BancoDeDados
        public ActionResult Index()
        {
            return View(db.BancoDeDados.ToList());
        }

        // GET: BancoDeDados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BancoDeDados bancoDeDados = db.BancoDeDados.Find(id);
            if (bancoDeDados == null)
            {
                return HttpNotFound();
            }
            return View(bancoDeDados);
        }

        // GET: BancoDeDados/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BancoDeDados/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Select,Update,Delete,Insert")] BancoDeDados bancoDeDados)
        {
            if (ModelState.IsValid)
            {
                db.BancoDeDados.Add(bancoDeDados);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bancoDeDados);
        }

        // GET: BancoDeDados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BancoDeDados bancoDeDados = db.BancoDeDados.Find(id);
            if (bancoDeDados == null)
            {
                return HttpNotFound();
            }
            return View(bancoDeDados);
        }

        // POST: BancoDeDados/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Select,Update,Delete,Insert")] BancoDeDados bancoDeDados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bancoDeDados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bancoDeDados);
        }

        // GET: BancoDeDados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BancoDeDados bancoDeDados = db.BancoDeDados.Find(id);
            if (bancoDeDados == null)
            {
                return HttpNotFound();
            }
            return View(bancoDeDados);
        }

        // POST: BancoDeDados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BancoDeDados bancoDeDados = db.BancoDeDados.Find(id);
            db.BancoDeDados.Remove(bancoDeDados);
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
