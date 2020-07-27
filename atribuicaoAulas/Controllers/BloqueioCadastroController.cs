using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace atribuicaoAulas.Controllers
{
    public class BloqueioCadastroController : Controller
    {
        private PerfilDB_ db = new PerfilDB_();

        // GET: BloqueioCadastro
        public ActionResult Index()
        {
            ViewBag.ultimoRecado = db.Recado.OrderByDescending(x => x.IdRecado).FirstOrDefault();
            return View(db.BloqueioCadastro.ToList());
        }

        // GET: BloqueioCadastro/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BloqueioCadastro bloqueioCadastro = db.BloqueioCadastro.Find(id);
            if (bloqueioCadastro == null)
            {
                return HttpNotFound();
            }
            return View(bloqueioCadastro);
        }

        // GET: BloqueioCadastro/Create
        public ActionResult Create()
        {
            ViewBag.ultimoRecado = db.Recado.OrderByDescending(x => x.IdRecado).FirstOrDefault();
            BloqueioCadastro bc = new BloqueioCadastro();
            bc.CPFBloqueio = (((atribuicaoAulas.ACESSO)(Session["usuariologado"])).CPF);
            bc.dataBloqueio = DateTime.Now;

            var listOpt = new[]     {
                         new SelectListItem { Value = "S", Text = "Bloquear" },
                        new SelectListItem { Value = "N", Text = "Desbloquear" } };

            ViewBag.lstOptions = new SelectList(listOpt, "Value", "Text");
            if (db.BloqueioCadastro.Count() > 0)
            {
                ViewBag.bloqueado = db.BloqueioCadastro.OrderByDescending(x => x.IdBloqueioCadastro).FirstOrDefault().bloqueado == "S" ? "Bloqueado" : "Desbloqueado";
                ViewBag.lstBloqueio = db.BloqueioCadastro.OrderByDescending(x => x.dataBloqueio).AsNoTracking().ToList();
                return View(bc);
            }
            else
            {
                return View();
            }
        }

        // GET: BloqueioCadastro/Edit/5
        [HttpGet]
        public ActionResult Salvar(BloqueioCadastro bloqueioCadastro)
        {
            db.BloqueioCadastro.Add(bloqueioCadastro);
            db.SaveChanges();
            return RedirectToAction("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BloqueioCadastro bloqueioCadastro)
        {
            ViewBag.ultimoRecado = db.Recado.OrderByDescending(x => x.IdRecado).FirstOrDefault();
            if (ModelState.IsValid)
            {
                db.BloqueioCadastro.Add(bloqueioCadastro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bloqueioCadastro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idBloqueioCadastro,bloqueado,databloqueio,CPFBloqueio")] BloqueioCadastro bloqueioCadastro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bloqueioCadastro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bloqueioCadastro);
        }

        // GET: BloqueioCadastro/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BloqueioCadastro bloqueioCadastro = db.BloqueioCadastro.Find(id);
            if (bloqueioCadastro == null)
            {
                return HttpNotFound();
            }
            return View(bloqueioCadastro);
        }

        public ActionResult DeleteConfirmed(int id)
        {
            BloqueioCadastro bloqueioCadastro = db.BloqueioCadastro.Find(id);
            db.BloqueioCadastro.Remove(bloqueioCadastro);
            db.SaveChanges();
            return RedirectToAction("Create");
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
