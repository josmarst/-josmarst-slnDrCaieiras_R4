using System.Web.Mvc;

namespace atribuicaoAulas.Controllers
{
    public class RelatorioController : Controller
    {
        // GET: Relatorio
        public ActionResult Index()
        {
            return View();
        }

        // GET: Relatorio/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Relatorio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Relatorio/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Relatorio/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Relatorio/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                              return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Relatorio/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Relatorio/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
               return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
