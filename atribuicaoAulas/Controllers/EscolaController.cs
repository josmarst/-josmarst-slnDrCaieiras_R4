using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace atribuicaoAulas.Controllers
{
    public class EscolaController : Controller
    {
        private PerfilDB_ db = new PerfilDB_();

        public ActionResult Index(int? id)
        {
            List<Escola> lstEscola = new List<Escola>();

            ViewBag.ultimoRecado = db.Recado.OrderByDescending(x => x.IdRecado).FirstOrDefault();

            if (Session["usuarioLogado"] != null) // IdPERFIL 1 = admin; IdPERFIL 2 = user
            {
                if (((atribuicaoAulas.ACESSO)(Session["usuarioLogado"])).IDPERFIL != 1)  // Não é Admin
                {
                    lstEscola = new List<Escola>();
                    // Só as escolas do município que têm aulas disponíveis
                    var ad = db.AULASDISPONIVEIS.Where(a => a.EXCLUIDO == 0).Where(a => a.atribuida == 0).Select(a => a.IDESCOLA).Distinct().ToList();
                    var es = db.Escola.Where(x => ad.Contains(x.IdEscola)).Where(z => z.Excluido == 0).ToList().OrderBy(a => a.Municipio.descrMunicipio).ThenBy(z => z.descrEscola);

                    foreach (var item in es)
                    {
                        lstEscola.Add(item);
                    }
                    //Escolas do município que nõ têm aulas disponiveis
                    ViewBag.aulasNaoDisponiveis = db.Escola.Where(x => !ad.Contains(x.IdEscola)).ToList().OrderBy(a => a.Municipio.descrMunicipio).ThenBy(z => z.descrEscola);

                    return View(lstEscola);
                }
            }
            // Usuário ADMIN -- Só as escolas que têm aulas disponíveis
            var ad2 = db.AULASDISPONIVEIS.Where(a => a.EXCLUIDO == 0).Where(a => a.atribuida == 0).Select(a => a.IDESCOLA).ToList();
            var escola = db.Escola.Where(x => ad2.Contains(x.IdEscola)).Where(z => z.Excluido == 0).ToList().OrderBy(a => a.Municipio.descrMunicipio).ThenBy(z => z.descrEscola);

            foreach (var item in escola)
            {
                lstEscola.Add(item);
            }
            //Escolas que nõ têm aulas disponiveis
            ViewBag.aulasNaoDisponiveis = db.Escola.Where(x => !ad2.Contains(x.IdEscola)).Where(z => z.Excluido == 0).ToList().OrderBy(a => a.Municipio.descrMunicipio).ThenBy(z => z.descrEscola); ;

            return View(lstEscola);
        }

        public ActionResult EditEscola(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Escola escola = db.Escola.Find(id);
            if (escola == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdMunicipio = new SelectList(db.Municipio.ToList(), "IdMunicipio", "descrMunicipio", escola.IdMunicipio);

            return View(escola);
        }

        public ActionResult Create()
        {
            ViewBag.IdMunicipio = new SelectList(db.Municipio, "IdMunicipio", "descrMunicipio");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Escola escola)
        {
            if (ModelState.IsValid)
            {
                db.Escola.Add(escola);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdMunicipio = new SelectList(db.Municipio, "IdMunicipio", "descrMunicipio", escola.IdMunicipio);
            return View(escola);
        }

        public ActionResult EditAula(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Escola escola = db.Escola.Find(id);
            if (escola == null) return HttpNotFound();

            List<AULASDISPONIVEIS> lstaulasDisponiveis = new List<AULASDISPONIVEIS>();
            lstaulasDisponiveis = db.AULASDISPONIVEIS.Where(x => x.IDESCOLA == id).Where(a => a.EXCLUIDO == 0).Where(a => a.atribuida == 0).AsNoTracking().ToList();

            var lstaulas = db.AULASDISPONIVEIS.Where(x => x.IDESCOLA == id)
                                            .Where(x => x.EXCLUIDO == 0)
                                            .Where(x => x.atribuida == 0)
                                            .GroupBy(x => new { x.IDDISCIPLINA, x.IDTURNO, x.IDTIPOAULA, x.IDAFASTAMENTO, x.PROFTITULAR })
                                            .Select(x => new { IdDisciplina = x.Key, qtdeAulas = x.Sum(b => b.QTDEAULAS) })
                                            .OrderByDescending(a => a.qtdeAulas)
                                            .AsNoTracking().ToList();

            List<AulasDisponiveisDescricao> lstaulasdescr = new List<AulasDisponiveisDescricao>();

            foreach (var item in lstaulas)
            {
                lstaulasdescr.Add(
                new AulasDisponiveisDescricao
                {
                    Turno = db.Turno.Where(x => x.IdTurno == item.IdDisciplina.IDTURNO).Select(a => a.descrTurno).FirstOrDefault().ToString(),
                    TipoAula = db.TipoAula.Where(x => x.IdTipoAula == item.IdDisciplina.IDTIPOAULA).Select(a => a.descrTipoAula).FirstOrDefault().ToString(),
                    Afastamento = db.Afastamento.Where(x => x.IdAfastamento == item.IdDisciplina.IDAFASTAMENTO).Select(a => a.descrAfastamento).FirstOrDefault().ToString(),
                    qtdeAulas = item.qtdeAulas,
                    Disciplina = db.Disciplina.Where(x => x.IdDisciplina == item.IdDisciplina.IDDISCIPLINA).Select(a => a.descrDisciplina).FirstOrDefault().ToString(),
                    idDisciplina = item.IdDisciplina.IDDISCIPLINA,
                    profTitular = item.IdDisciplina.PROFTITULAR
                }
                );
            }

            ViewBag.Escola = escola;
            return View("_AulasDisponiveisDetalhes", lstaulasdescr);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Escola escola)
        {
            try
            {
                Escola CPescola = db.Escola.Find(escola.IdEscola);

                CPescola.descrEscola = escola.descrEscola;
                CPescola.IdMunicipio = escola.IdMunicipio;

                UpdateModel(CPescola);
                db.SaveChanges();
                ViewBag.IdMunicipio = new SelectList(db.Municipio, "IdMunicipio", "descrMunicipio", escola.IdMunicipio);
                return RedirectToAction("EditEscola/" + escola.IdEscola + "");
            }
            catch (Exception ex)
            {
                ViewBag.IdMunicipio = new SelectList(db.Municipio, "IdMunicipio", "descrMunicipio", escola.IdMunicipio);
                return RedirectToAction("EditEscola/" + escola.IdEscola + "");
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Escola escola = db.Escola.Find(id);
            if (escola == null)
            {
                return HttpNotFound();
            }
            return View(escola);
        }

        public JsonResult DeleteConfirmed(int id)
        {
            bool result = false;
            Escola escola = db.Escola.Find(id);
            AULASDISPONIVEIS aulasDisp = new AULASDISPONIVEIS();

            try
            {
                aulasDisp = db.AULASDISPONIVEIS.Where(m => m.IDESCOLA == id).Where(q => q.EXCLUIDO == 0).FirstOrDefault();
                if (aulasDisp == null)
                {
                    escola.Excluido = 1;
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    result = false;
                }

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult RetornarEscolas(int ID)
        {
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(new SelectList(db.Escola.Where(a => a.Excluido == 0).Where(a => a.IdMunicipio == ID).OrderBy(x => x.descrEscola).ToList(), "IdEscola", "descrEscola"));

            return Json(json, JsonRequestBehavior.AllowGet);
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
