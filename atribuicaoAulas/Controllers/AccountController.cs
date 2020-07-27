using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using atribuicaoAulas.Models;
using System.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace atribuicaoAulas.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private PerfilDB_ db = new PerfilDB_();

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            ViewBag.ultimoRecado = db.Recado.OrderByDescending(x => x.IdRecado).FirstOrDefault();
            return View();
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(ACESSO acesso)
        {
            if (acesso.CPF == null)
            {
                ModelState.AddModelError("", "Insira um CPF.");
                return View("ResetPassword");
            }
            if (acesso.SENHA != acesso.ConfirmaSENHA)
            {
                ModelState.AddModelError("", "Senhas não conferem.");
                return View();
            }
            try
            {
                ACESSO ac = db.ACESSO.Where(x => x.CPF == acesso.CPF).Where(x => x.SENHA == acesso.senhaAntiga).Where(x => x.ATIVO == "S").FirstOrDefault();
                ac.SENHA = acesso.SENHA;
                ac.ConfirmaSENHA = acesso.ConfirmaSENHA;
				
		//		TryUpdateModel(ac);
                db.SaveChanges();
                ModelState.AddModelError("", "Senha alterada com sucesso.");
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "A Senha não foi alterada. Verifique o preenchimento dos campos.");
                return View("ResetPassword");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(ACESSO model)
        {
            ViewBag.ultimoRecado = db.Recado.OrderByDescending(x => x.IdRecado).FirstOrDefault();

            var ACESSO = db.ACESSO.Where(i => i.SENHA == model.SENHA).Where(a => a.CPF == model.CPF.Trim()).Where(x => x.ATIVO == "S").FirstOrDefault();
            if (ACESSO == null)
            {
                ModelState.AddModelError("", "Login inválido.");
                return View(model);
            }
            if (model.SENHA.ToLower() == "123abc")
            {
                model.SENHA = null;
                ACESSO.SENHA = null;
                return RedirectToAction("Index", "AccountChangePassword", ACESSO);
            }

            if (Session["usuarioLogado"] != null)
            {
                ModelState.AddModelError("", "Já existe um usuário logado.");
                return View(model);
            }
            model.ATIVO = ACESSO.ATIVO;
            model.CPF = ACESSO.CPF;
            model.IDPERFIL = ACESSO.IDPERFIL;
            model.NOME = ACESSO.NOME;
            model.SENHA = ACESSO.SENHA;
            model.IDESCOLA = ACESSO.IDESCOLA;
            Session["usuarioLogado"] = model;

            ModelState.AddModelError("", "Logon efetuado com sucesso.");
            return View();
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            ViewBag.ultimoRecado = db.Recado.OrderByDescending(x => x.IdRecado).FirstOrDefault();
            ViewBag.ViewBagMunicipios = new SelectList(db.Municipio.ToList().OrderBy(x => x.descrMunicipio), "IdMunicipio", "descrMunicipio");
            ViewBag.ViewBagEscolas = new SelectList(db.Escola.Where(x => x.Excluido == 0).OrderBy(x => x.descrEscola).ToList(), "IdEscola", "descrEscola");
            ViewBag.ViewBagPerfil = new SelectList(db.perfil.ToList(), "IdPERFIL", "descrPerfil");

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            ViewBag.ultimoRecado = db.Recado.OrderByDescending(x => x.IdRecado).FirstOrDefault();
            ViewBag.ViewBagMunicipios = new SelectList(db.Municipio.ToList(), "IdMunicipio", "descrMunicipio");
            ViewBag.ViewBagEscolas = new SelectList(db.Escola.Where(x => x.Excluido == 0).ToList(), "IdEscola", "descrEscola");
            ViewBag.ViewBagPerfil = new SelectList(db.perfil.ToList(), "IdPERFIL", "descrPerfil");

            if (ModelState.IsValid)
            {
                try
                {
                    ACESSO acesso = new ACESSO();
                    acesso.CPF = model.CPF.Trim();
                    acesso.SENHA = model.Password;
                    acesso.ConfirmaSENHA = model.ConfirmPassword;
                    acesso.ATIVO = "S";
                    acesso.IDPERFIL = model.idperfil;
                    acesso.NOME = model.Nome;
                    acesso.IDESCOLA = (int)model.idEscola;
                    acesso.IDPERFIL = model.idperfil;
                    acesso.Email = model.email;

                    db.ACESSO.Add(acesso);
                    db.SaveChanges();

                    ModelState.AddModelError("Sucesso", "Usuário cadastrado com sucesso.");

                    TempData["msg"] = "<script>alert('Operação realizada com sucesso');</script>";
                    return View(model);
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.InnerException.Message.Contains("The duplicate key value"))
                    {
                        TempData["msg"] = "<script>alert('Erro! O CPF informado já existe.');</script>";
                        return View(model);
                    }
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Pesquisar(RegisterViewModel model)
        {
            ViewBag.ultimoRecado = db.Recado.OrderByDescending(x => x.IdRecado).FirstOrDefault();
            ViewBag.ViewBagEscolas = new SelectList(db.Escola.Where(x => x.Excluido == 0).ToList(), "IdEscola", "descrEscola");
            ViewBag.ViewBagPerfil = new SelectList(db.perfil.ToList(), "IdPERFIL", "descrPerfil");

            List<ACESSO> lst = new List<ACESSO>();
            lst = db.ACESSO.OrderBy(x => x.NOME).AsNoTracking().ToList();
            return View(lst);
        }

        [AllowAnonymous]
        public ActionResult AlteraAcesso(string id)
        {
            ACESSO aCESSO = db.ACESSO.Find(id);
            if (aCESSO.ATIVO == "S") { aCESSO.ATIVO = "N"; } else { aCESSO.ATIVO = "S"; }

            aCESSO.ConfirmaSENHA = aCESSO.SENHA;
            TryUpdateModel(aCESSO);

            db.SaveChanges();

            return RedirectToAction("Pesquisar", "Account");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Excluir(string id)
        {
            ACESSO aCESSO = db.ACESSO.Find(id);

            db.ACESSO.Remove(aCESSO);
            db.SaveChanges();

            return RedirectToAction("Pesquisar", "Account");
        }

        [AllowAnonymous]
        public ActionResult Sair()
        {
            Session["usuarioLogado"] = null;
           // Session.Remove("SessionDiasSemanaHorarioTurma");
           // return RedirectToAction("Index", "Escola");
            return RedirectToAction("Index", "Home");

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}