using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace atribuicaoAulas.Controllers
{
    public class MunicipioController : Controller
    {
        private PerfilDB_ db = new PerfilDB_();

        // GET: Municipio
        public ActionResult Index()
        {
            ViewBag.ultimoRecado = db.Recado.OrderByDescending(x => x.IdRecado).FirstOrDefault();

            var municipio = db.Municipio.Include(m => m.Estado).OrderBy(a => a.descrMunicipio);
            return View(municipio.ToList());
        }

        // GET: Municipio
        public ActionResult ListarMunicipio(int id)
        {
            var escol = (from ad in db.AULASDISPONIVEIS
                         where ad.EXCLUIDO == 0
                         where ad.atribuida == 0
                         join es in db.Escola on ad.IDESCOLA equals es.IdEscola
                         join mu in db.Municipio on es.IdMunicipio equals mu.IdMunicipio
                         where mu.IdMunicipio == id
                         select es).Distinct().ToList();

            ViewBag.descrMunicipio = db.Municipio.Where(e => e.IdMunicipio == id).Select(a => a.descrMunicipio).FirstOrDefault();
            return View(escol);
        }
        // GET: Municipio/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Municipio municipio = db.Municipio.Find(id);
            if (municipio == null)
            {
                return HttpNotFound();
            }
            return View(municipio);
        }

        // GET: Municipio/Create
        public ActionResult Create()
        {
            ViewBag.IdEstado = new SelectList(db.Estado, "IdEstado", "desricao");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Municipio municipio)
        {
            if (ModelState.IsValid)
            {
                db.Municipio.Add(municipio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdEstado = new SelectList(db.Estado, "IdEstado", "desricao", municipio.IdEstado);
            return View(municipio);
        }

        // GET: Municipio/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Municipio municipio = db.Municipio.Find(id);
            if (municipio == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEstado = new SelectList(db.Estado, "IdEstado", "desricao", municipio.IdEstado);
            return View(municipio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Municipio municipio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(municipio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdEstado = new SelectList(db.Estado, "IdEstado", "desricao", municipio.IdEstado);
            return View(municipio);
        }

        // GET: Municipio/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Municipio municipio = db.Municipio.Find(id);
            if (municipio == null)
            {
                return HttpNotFound();
            }
            return View(municipio);
        }

        // POST: Municipio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Municipio municipio = db.Municipio.Find(id);
            db.Municipio.Remove(municipio);
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
