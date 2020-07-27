using System;
using System.Web.Mvc;

namespace atribuicaoAulas.Controllers
{
    public class AccountChangePasswordController : Controller
    {
        private PerfilDB_ db = new PerfilDB_();

        // GET: AccountChangePassword
        public ActionResult Index(ACESSO acesso)
        {
            return View(acesso);
        }

        public ActionResult Register(ACESSO model)
        {
            ViewBag.ViewBagMunicipio = new SelectList(db.Municipio, "IdMunicipio", "descrMunicipio");
            if (ModelState.IsValid)
            {
                try
                {
                    var delAcesso = db.ACESSO.Find(model.CPF);
                    model.Email = delAcesso.Email;

                    db.ACESSO.Remove(delAcesso);
                    db.ACESSO.Add(model);
                    db.SaveChanges();

                    Session["usuarioLogado"] = model;

                    ModelState.AddModelError("Sucesso", "Senha alterada com sucesso.");
                    return View(model);
                }
                catch (Exception ex)
                {
                    return View(model);
                }
            }
            return View(model);
        }
    }
}