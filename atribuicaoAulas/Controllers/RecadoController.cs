using System;
using System.Linq;
using System.Web.Mvc;

namespace atribuicaoAulas.Controllers
{
    public class RecadoController : Controller
    {
        private PerfilDB_ db = new PerfilDB_();

        // GET: Recado
        public ActionResult Index()
        {
            return View(db.Recado.ToList());
        }

        // GET: Recado/Create
        public ActionResult Create()
		{
			ViewBag.ultimoRecado = db.Recado.OrderByDescending(x => x.IdRecado).FirstOrDefault();
			ViewBag.ListaRecados = db.Recado.OrderByDescending(x => x.dtPublicacao).ToList();
            return View();
        }

        // POST: Recado/Create
        [HttpPost]
        public ActionResult Create(Recado recado)
        {
            try
            {
                recado.dtPublicacao = System.DateTime.Now;
                db.Recado.Add(recado);
                db.SaveChanges();

                ViewBag.ListaRecados = db.Recado.OrderByDescending(x => x.dtPublicacao).ToList();
                ViewBag.ultimoRecado = db.Recado.OrderByDescending(x => x.IdRecado).FirstOrDefault();

                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Recado/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        public JsonResult DeleteConfirmed(int id)
        {
            bool result = false;
            Recado recado = db.Recado.Find(id);
            try
            {
                db.Recado.Remove(recado);
                db.SaveChanges();
                result = true;

                ViewBag.ultimoRecado = db.Recado.OrderByDescending(x => x.IdRecado).FirstOrDefault();

                if (ViewBag.ultimoRecado != null)
                {
                    ViewBag.ultimoRecado = ViewBag.ultimoRecado.Substring(0, ViewBag.ultimoRecado.Length - 1);
                    ViewBag.ultimoRecado = ViewBag.ultimoRecado.Substring(0, ViewBag.ultimoRecado.Length - 1); ;
                }
                else
                {
                    ViewBag.ultimoRecado = null;
                }

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
