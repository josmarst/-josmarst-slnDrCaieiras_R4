using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace atribuicaoAulas.Controllers
{
    public class AfastamentoController : Controller
    {
        private PerfilDB_ db = new PerfilDB_();

        // GET: Afastamento
        public ActionResult Index()
        {
            return View(db.Afastamento.ToList());
        }

        // GET: Afastamento/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Afastamento afastamento = db.Afastamento.Find(id);
            if (afastamento == null)
            {
                return HttpNotFound();
            }
            return View(afastamento);
        }

        // GET: Afastamento/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAfastamento,descrAfastamento")] Afastamento afastamento)
        {
            if (ModelState.IsValid)
            {
                db.Afastamento.Add(afastamento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(afastamento);
        }

        // GET: Afastamento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Afastamento afastamento = db.Afastamento.Find(id);
            if (afastamento == null)
            {
                return HttpNotFound();
            }
            return View(afastamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAfastamento,descrAfastamento")] Afastamento afastamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(afastamento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(afastamento);
        }

        // GET: Afastamento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Afastamento afastamento = db.Afastamento.Find(id);
            if (afastamento == null)
            {
                return HttpNotFound();
            }
            return View(afastamento);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Afastamento afastamento = db.Afastamento.Find(id);
            db.Afastamento.Remove(afastamento);
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
