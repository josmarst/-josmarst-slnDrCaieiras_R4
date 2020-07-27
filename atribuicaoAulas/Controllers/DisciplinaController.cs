using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace atribuicaoAulas.Controllers
{
    public class DisciplinaController : Controller
    {
        private PerfilDB_ db = new PerfilDB_();

        // GET: Disciplina
        public ActionResult Index()
        {
            ViewBag.ultimoRecado = db.Recado.OrderByDescending(x => x.IdRecado).FirstOrDefault();
            return View(db.Disciplina.ToList().OrderBy(a => a.descrDisciplina));
        }

        // GET: Disciplina/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disciplina disciplina = db.Disciplina.Find(id);
            if (disciplina == null)
            {
                return HttpNotFound();
            }
            return View(disciplina);
        }

        // GET: Disciplina/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Disciplina disciplina)
        {
            if (ModelState.IsValid)
            {
                db.Disciplina.Add(disciplina);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(disciplina);
        }

        // GET: Disciplina/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<AULASDISPONIVEIS> LstAulasDisponiveis = new List<AULASDISPONIVEIS>();
            List<Escola> LstEscola = new List<Escola>();

            LstAulasDisponiveis = db.AULASDISPONIVEIS.Where(x => x.IDDISCIPLINA == id).Where(a => a.EXCLUIDO == 0).AsNoTracking().ToList();

            foreach (var item in LstAulasDisponiveis)
            {
                Escola escola = db.Escola.Find(item.IDESCOLA);
                LstEscola.Add(escola);
            }

            return View(LstEscola.OrderBy(a => a.Municipio.descrMunicipio));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDisciplina,descrDisciplina")] Disciplina disciplina)
        {
            if (ModelState.IsValid)
            {
                db.Entry(disciplina).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(disciplina);
        }

        // GET: Disciplina/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disciplina disciplina = db.Disciplina.Find(id);
            if (disciplina == null)
            {
                return HttpNotFound();
            }
            return View(disciplina);
        }

        public JsonResult DeleteConfirmed(int id)
        {
            bool result = false;
            Disciplina disciplina = db.Disciplina.Find(id);
            try
            {
                db.Disciplina.Remove(disciplina);
                db.SaveChanges();
                result = true;

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
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
