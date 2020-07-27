using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace atribuicaoAulas.Controllers
{
       public class HomeController : Controller
    {
        private PerfilDB_ db = new PerfilDB_();
        public ActionResult Index()
        {
            //ViewBag.ultimoRecado = db.Recado.OrderByDescending(x => x.IdRecado).FirstOrDefault();
            ViewBag.ultimoRecado = null;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}