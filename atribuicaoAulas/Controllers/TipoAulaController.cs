using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace atribuicaoAulas.Controllers
{
    public class TipoAulaController : Controller
    {
         private PerfilDB_ db = new PerfilDB_();

        // GET: TipoAula
        public ActionResult Index()
        {
            return View(db.TipoAula.ToList());
        }

        // GET: TipoAula/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoAula tipoAula = db.TipoAula.Find(id);
            if (tipoAula == null)
            {
                return HttpNotFound();
            }
            return View(tipoAula);
        }

        // GET: TipoAula/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoAula/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdTipoAula,descrTipoAula")] TipoAula tipoAula)
        {
            if (ModelState.IsValid)
            {
                db.TipoAula.Add(tipoAula);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoAula);
        }

        // GET: TipoAula/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoAula tipoAula = db.TipoAula.Find(id);
            if (tipoAula == null)
            {
                return HttpNotFound();
            }
            return View(tipoAula);
        }

        // POST: TipoAula/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdTipoAula,descrTipoAula")] TipoAula tipoAula)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoAula).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoAula);
        }

        // GET: TipoAula/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoAula tipoAula = db.TipoAula.Find(id);
            if (tipoAula == null)
            {
                return HttpNotFound();
            }
            return View(tipoAula);
        }

        // POST: TipoAula/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoAula tipoAula = db.TipoAula.Find(id);
            db.TipoAula.Remove(tipoAula);
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
