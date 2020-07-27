using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using atribuicaoAulas.Models;
using System;
using System.Collections.Generic;
using ClosedXML.Excel;
using System.Data;
using System.IO;
using System.Globalization;
using System.Text;

namespace atribuicaoAulas.Controllers
{
	public class aulasDisponiveisController : Controller
	{
		private PerfilDB_ db = new PerfilDB_();

		#region variaveis auxiliares Inicialização
		bool diaUm = false;
		bool diaDois = false;
		bool diaTres = false;
		bool diaQuatro = false;
		bool diaCinco = false;

		bool horarioUm = false;
		bool horarioDois = false;
		bool horarioTres = false;
		bool horarioQuatro = false;
		bool horarioCinco = false;
		bool horarioSeis = false;
		#endregion
		public ActionResult Index()
		{
			ViewBag.ultimoRecado = db.Recado.OrderByDescending(x => x.IdRecado).FirstOrDefault();
			if (db.BloqueioCadastro.OrderByDescending(x => x.IdBloqueioCadastro).Count() == 0) ViewBag.BloqueioCadastroAula = "Desbloqueado";  //DESBLOQUEADO COMO STATUS INICIAL
			else ViewBag.BloqueioCadastroAula = db.BloqueioCadastro.OrderByDescending(x => x.IdBloqueioCadastro).FirstOrDefault().bloqueado == "S" ? "Bloqueado" : "Desbloqueado";

			ViewBag.IdEscola = new SelectList(db.Escola.OrderBy(x => x.descrEscola).ToList(), "IdEscola", "descrEscola");
			ViewBag.IdDisciplina = new SelectList(db.Disciplina.OrderBy(x => x.descrDisciplina).ToList(), "IdDisciplina", "descrDisciplina");
			ViewBag.IdTipoAula = new SelectList(db.TipoAula.ToList(), "IdTipoAula", "descrTipoAula", ViewBag.descrTipoAula);
			ViewBag.MotivoExclusao = new SelectList(db.MotivoExclusao.OrderBy(x => x.descrMotivoExclusao).ToList(), "IdMotivoExclusao", "descrMotivoExclusao");


			if (Session["usuariologado"] != null)
			{
				if (((atribuicaoAulas.ACESSO)(Session["usuarioLogado"])).IDPERFIL == 1 || ((atribuicaoAulas.ACESSO)(Session["usuarioLogado"])).IDPERFIL == 3)  // ADMIN OU SUPERVISOR
				{
					var aulasDisponiveis = db.AULASDISPONIVEIS.Where(a => a.EXCLUIDO == 0).Where(x => x.atribuida == 0).AsNoTracking().ToList();

					ViewBag.NomeEscola = (from ad in aulasDisponiveis
										  join es in db.Escola on ad.IDESCOLA equals es.IdEscola
										  where ad.EXCLUIDO == 0
										  select es.descrEscola).ToList();
					ViewBag.descrTurno = (from ad in aulasDisponiveis
										  join tu in db.Turno on ad.IDTURNO equals tu.IdTurno
										  where ad.EXCLUIDO == 0
										  select tu.descrTurno).ToList();
					ViewBag.descrTipoAula = (from ad in aulasDisponiveis
											 join ta in db.TipoAula on ad.IDTIPOAULA equals ta.IdTipoAula
											 where ad.EXCLUIDO == 0
											 select ta.descrTipoAula).ToList();
					ViewBag.descrDisciplina = (from ad in aulasDisponiveis
											   join dd in db.Disciplina on ad.IDDISCIPLINA equals dd.IdDisciplina
											   where ad.EXCLUIDO == 0
											   select dd.descrDisciplina).ToList();
					ViewBag.descrAfastamento = (from ad in aulasDisponiveis
												join af in db.Afastamento on ad.IDAFASTAMENTO equals af.IdAfastamento
												where ad.EXCLUIDO == 0
												select af.descrAfastamento).ToList();
					ViewBag.qtdAulas = (from ad in aulasDisponiveis
										where ad.EXCLUIDO == 0
										select ad.QTDEAULAS).ToList();
					ViewBag.PeriodoAfastamento = (from ad in aulasDisponiveis
												  where ad.EXCLUIDO == 0
												  select ad.PERIODOAFASTAMENTO).ToList();
					ViewBag.Turma = (from ad in aulasDisponiveis
									 where ad.EXCLUIDO == 0
									 select ad.TURMA).ToList();

					return View(aulasDisponiveis);
				}
				else
				{
					var ac = db.ACESSO.Find(((atribuicaoAulas.ACESSO)(Session["usuariologado"])).CPF);
					var idEsc = ((atribuicaoAulas.ACESSO)(Session["usuariologado"])).IDESCOLA;

					var aulasDisponiveisList = db.AULASDISPONIVEIS.Where(x => x.IDESCOLA == idEsc).Where(a => a.EXCLUIDO == 0).Where(x => x.atribuida == 0).AsNoTracking().ToList();

					ViewBag.NomeEscola = (from ad in aulasDisponiveisList
										  join es in db.Escola on ad.IDESCOLA equals es.IdEscola
										  select es.descrEscola).ToList();
					ViewBag.descrTurno = (from ad in aulasDisponiveisList
										  join tu in db.Turno on ad.IDTURNO equals tu.IdTurno
										  select tu.descrTurno).ToList();
					ViewBag.descrTipoAula = (from ad in aulasDisponiveisList
											 join ta in db.TipoAula on ad.IDTIPOAULA equals ta.IdTipoAula
											 select ta.descrTipoAula).ToList();
					ViewBag.descrDisciplina = (from ad in aulasDisponiveisList
											   join dd in db.Disciplina on ad.IDDISCIPLINA equals dd.IdDisciplina
											   select dd.descrDisciplina).ToList();
					ViewBag.descrAfastamento = (from ad in aulasDisponiveisList
												join af in db.Afastamento on ad.IDAFASTAMENTO equals af.IdAfastamento
												select af.descrAfastamento).ToList();
					ViewBag.qtdAulas = (from ad in aulasDisponiveisList
										select ad.QTDEAULAS).ToList();
					ViewBag.PeriodoAfastamento = (from ad in aulasDisponiveisList
												  select ad.PERIODOAFASTAMENTO).ToList();
					ViewBag.Turma = (from ad in aulasDisponiveisList
									 select ad.TURMA).ToList();

					return View(aulasDisponiveisList);
				}
			}
			return View(db.AULASDISPONIVEIS.ToList());
		}

		public ActionResult aulasDisponiveisAgrupadas()
		{
			ViewBag.ultimoRecado = db.Recado.OrderByDescending(x => x.IdRecado).FirstOrDefault();
			var varAulasDisponiveis = db.AULASDISPONIVEIS
									.Where(x => x.EXCLUIDO == 0)
									.Where(x => x.atribuida == 0)
									.GroupBy(a => a.IDDISCIPLINA)
									.Select(a => new { qtdeAulas = a.Sum(b => b.QTDEAULAS), IdDisciplina = a.Key })
									.OrderByDescending(a => a.qtdeAulas)
									.AsNoTracking().ToList();

			List<aulasDisponiveisAgrupadas> aulasDisp = new List<aulasDisponiveisAgrupadas>();

			foreach (var item in varAulasDisponiveis)
			{
				aulasDisponiveisAgrupadas ad = new aulasDisponiveisAgrupadas();
				ad.id = item.IdDisciplina;
				ad.qtdeAulas = item.qtdeAulas;
				// ad.profTitular = item.IdDisciplina.PROFTITULAR;
				aulasDisp.Add(ad);
			}

			ViewBag.descrDisciplina = db.Disciplina.AsNoTracking().ToList();

			return View(aulasDisp);

		}

		public ActionResult DetailsDisciplina(int? id)
		{
			if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

			var varAulasDisponiveis = db.AULASDISPONIVEIS
										   .Where(x => x.EXCLUIDO == 0)
										   .Where(x => x.atribuida == 0)
										   .Where(x => x.IDDISCIPLINA == id)
										   .GroupBy(y => new { y.IDESCOLA, y.PROFTITULAR })
										   // .GroupBy(a => a.IDESCOLA)
										   .Select(a => new { qtdeAulas = a.Sum(b => b.QTDEAULAS), idEscola = a.Key })
										   // .Select(a => new   { qtdeAulas = a.Sum(b => b.QTDEAULAS), idEscola = a.Key, profTitular = a.profTitular})
										   .OrderByDescending(a => a.qtdeAulas)
										   .AsNoTracking().ToList();
			List<aulasDisponiveisAgrupadas> aulasDisp = new List<aulasDisponiveisAgrupadas>();

			foreach (var item in varAulasDisponiveis)
			{
				aulasDisponiveisAgrupadas ad = new aulasDisponiveisAgrupadas();
				ad.id = item.idEscola.IDESCOLA;  // PASSANDO O IDESCOLA E NÃO O IDDISCIPLINA
				ad.qtdeAulas = item.qtdeAulas;
				ad.profTitular = item.idEscola.PROFTITULAR;
				aulasDisp.Add(ad);
			}
			ViewBag.descrEscola = db.Escola.Where(x => x.Excluido == 0).AsNoTracking().ToList();
			ViewBag.idDisciplina = id;

			return View(aulasDisp);
		}

		public ActionResult DetailsEscola(int? idEscola, int idDisciplina, string profTitular)
		{
			List<AULASDISPONIVEIS> lstaulasDisponiveis = new List<AULASDISPONIVEIS>();
			lstaulasDisponiveis = db.AULASDISPONIVEIS.Where(x => x.IDESCOLA == idEscola).Where(x => x.IDDISCIPLINA == idDisciplina).Where(a => a.PROFTITULAR == profTitular).Where(a => a.EXCLUIDO == 0).Where(a => a.atribuida == 0).AsNoTracking().ToList();

			var tipoAula = lstaulasDisponiveis[0].IDTIPOAULA;
			var tipoAfastamento = lstaulasDisponiveis[0].IDAFASTAMENTO;

			ViewBag.descrEscola = db.Escola.Where(x => x.IdEscola == idEscola).Select(x => x.descrEscola).FirstOrDefault();
			ViewBag.Professor = lstaulasDisponiveis[0].PROFTITULAR;
			ViewBag.PeriodoAfastamento = lstaulasDisponiveis[0].PERIODOAFASTAMENTO;
			ViewBag.Disciplina = new SelectList(db.Disciplina.Where(x => x.IdDisciplina == idDisciplina).ToList(), "IdDisciplina", "descrDisciplina");
			ViewBag.IdTipoAula = new SelectList(db.TipoAula.Where(x => x.IdTipoAula == tipoAula).ToList(), "IdTipoAula", "descrTipoAula");
			ViewBag.IdAfastamento = new SelectList(db.Afastamento.Where(x => x.IdAfastamento == tipoAfastamento).ToList(), "IdAfastamento", "descrAfastamento");
			ViewBag.IdEscola = new SelectList(db.Escola.Where(x => x.IdEscola == idEscola).ToList(), "IdEscola", "descrEscola");

			return View("_AulasDisponiveisDescricao", lstaulasDisponiveis);
		}

		public ActionResult Details(int? id)
		{
			if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

			AULASDISPONIVEIS aulasDisponiveis = db.AULASDISPONIVEIS.Find(id);
			if (aulasDisponiveis == null) return HttpNotFound();

			return View(aulasDisponiveis);
		}

		public ActionResult Create()
		{
			int idmunicipio = Convert.ToInt32(Session["model.idMunicipio"]);

			if (db.BloqueioCadastro.OrderByDescending(x => x.IdBloqueioCadastro).Count() == 0) ViewBag.BloqueioCadastroAula = "Desbloqueado";  //DESBLOQUEADO COMO STATUS INICIAL
			else ViewBag.BloqueioCadastroAula = db.BloqueioCadastro.OrderByDescending(x => x.IdBloqueioCadastro).FirstOrDefault().bloqueado == "S" ? "Bloqueado" : "Desbloqueado";

			ViewBag.IdTipoAula = null;
			ViewBag.IdTurno = null;
			ViewBag.IdDisciplina = null;
			ViewBag.IdAfastamento = null;
			ViewBag.IdEstado = null;
			ViewBag.IdHorario = null;
			ViewBag.IdDiasSemana = null;
			ViewBag.idMunicipio = null;
			ViewBag.IdEscola = null;

			ViewBag.IdTipoAula = new SelectList(db.TipoAula.OrderBy(x => x.descrTipoAula).ToList(), "IdTipoAula", "descrTipoAula");
			ViewBag.IdTurno = new SelectList(db.Turno.OrderBy(x => x.descrTurno).ToList(), "IdTurno", "descrTurno");
			ViewBag.IdDisciplina = new SelectList(db.Disciplina.OrderBy(x => x.descrDisciplina).ToList(), "IdDisciplina", "descrDisciplina");
			ViewBag.IdAfastamento = new SelectList(db.Afastamento.OrderBy(x => x.descrAfastamento).ToList(), "IdAfastamento", "descrAfastamento");
			ViewBag.IdEstado = new SelectList(db.Estado.OrderBy(x => x.desricao).ToList(), "IdEstado", "descrEstado");

			var listHorario = new[]     {
						new SelectListItem { Value = "1", Text = "1º Aula" },
						new SelectListItem { Value = "2", Text = "2º Aula" },
						new SelectListItem { Value = "3", Text = "3º Aula" },
						new SelectListItem { Value = "4", Text = "4º Aula" },
						new SelectListItem { Value = "5", Text = "5º Aula" },
						new SelectListItem { Value = "6", Text = "6º Aula" },
						new SelectListItem { Value = "7", Text = "7º Aula" }
					};
			ViewBag.IdHorario = new SelectList(listHorario, "Value", "Text");
			var listDiasSemana = new[]     {
						new SelectListItem { Value = "1", Text = "Segunda" },
						new SelectListItem { Value = "2", Text = "Terça" },
						new SelectListItem { Value = "3", Text = "Quarta" },
						new SelectListItem { Value = "4", Text = "Quinta" },
						new SelectListItem { Value = "5", Text = "Sexta" }
					};
			ViewBag.IdDiasSemana = new SelectList(listDiasSemana, "Value", "Text");

			if (((atribuicaoAulas.ACESSO)(Session["usuarioLogado"])).IDPERFIL == 1 || ((atribuicaoAulas.ACESSO)(Session["usuarioLogado"])).IDPERFIL == 3) //ADMIN OU SUPERVISOR
			{
				ViewBag.idMunicipio = "Admin";
				ViewBag.IdEscola = new SelectList(db.Escola.ToList(), "IdEscola", "descrEscola");
			}
			else
			{
				var idEsc = ((atribuicaoAulas.ACESSO)(Session["usuariologado"])).IDESCOLA;
				ViewBag.IdEscola = new SelectList(db.Escola.Where(x => x.IdEscola == idEsc).ToList(), "IdEscola", "descrEscola");   //USUARIO
			}

			return View();
		}

		public ActionResult Edit(int? id)
		{
			if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

			AULASDISPONIVEIS aulasDisponiveis = db.AULASDISPONIVEIS.Find(id);
			if (aulasDisponiveis == null) return HttpNotFound();

			int idmunicipio = Convert.ToInt32(Session["model.idMunicipio"]);
			ViewBag.NomeEscola = (from ad in db.AULASDISPONIVEIS
								  join es in db.Escola on ad.IDESCOLA equals es.IdEscola
								  where ad.IDAULASDISPONIVEIS == id
								  select es.descrEscola).FirstOrDefault();
			ViewBag.descrTipoAula = (from ad in db.AULASDISPONIVEIS
									 join ta in db.TipoAula on ad.IDTIPOAULA equals ta.IdTipoAula
									 where ad.IDTIPOAULA == aulasDisponiveis.IDTIPOAULA
									 select ta.descrTipoAula).FirstOrDefault();
			ViewBag.descrTurno = (from ad in db.AULASDISPONIVEIS
								  join tu in db.Turno on ad.IDTURNO equals tu.IdTurno
								  where ad.IDTURNO == aulasDisponiveis.IDTURNO
								  select tu.descrTurno).FirstOrDefault();
			ViewBag.descrDisciplina = (from ad in db.AULASDISPONIVEIS
									   join dd in db.Disciplina on ad.IDDISCIPLINA equals dd.IdDisciplina
									   where ad.IDDISCIPLINA == aulasDisponiveis.IDDISCIPLINA
									   select dd.descrDisciplina).FirstOrDefault();
			ViewBag.descrAfastamento = (from ad in db.AULASDISPONIVEIS
										join af in db.Afastamento on ad.IDAFASTAMENTO equals af.IdAfastamento
										where af.IdAfastamento == aulasDisponiveis.IDAFASTAMENTO
										select af.descrAfastamento).FirstOrDefault();

			ViewBag.IdTipoAula = new SelectList(db.TipoAula.ToList(), "IdTipoAula", "descrTipoAula", ViewBag.descrTipoAula);
			ViewBag.IdTurno = new SelectList(db.Turno.ToList(), "IdTurno", "descrTurno", ViewBag.descrTurno);
			ViewBag.IdDisciplina = new SelectList(db.Disciplina.ToList(), "IdDisciplina", "descrDisciplina", ViewBag.descrDisciplina);
			ViewBag.IdAfastamento = new SelectList(db.Afastamento.ToList(), "IdAfastamento", "descrAfastamento", ViewBag.descrAfastamento);

			return View(aulasDisponiveis);
		}

		public PartialViewResult _DisciplinaView()
		{
			return PartialView(new SelectList(db.Disciplina.ToList(), "IdDisciplina", "descrDisciplina"));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(AULASDISPONIVEIS aulasDisponiveis)
		{
			try
			{
				AULASDISPONIVEIS ad = db.AULASDISPONIVEIS.Find(aulasDisponiveis.IDAULASDISPONIVEIS);

				if (aulasDisponiveis.IDAFASTAMENTO == 0) aulasDisponiveis.IDAFASTAMENTO = ad.IDAFASTAMENTO;
				if (aulasDisponiveis.IDTIPOAULA == 0) aulasDisponiveis.IDTIPOAULA = ad.IDTIPOAULA;
				if (aulasDisponiveis.IDTURNO == 0) aulasDisponiveis.IDTURNO = ad.IDTURNO;
				if (aulasDisponiveis.IDDISCIPLINA == 0) aulasDisponiveis.IDDISCIPLINA = ad.IDDISCIPLINA;
				aulasDisponiveis.PROFTITULAR = ad.PROFTITULAR;
				aulasDisponiveis.CPFALTERACAO = ((atribuicaoAulas.ACESSO)(Session["usuariologado"])).CPF;
				aulasDisponiveis.DTALTERACAO = DateTime.Now;
				aulasDisponiveis.CPFALTERACAO = (((atribuicaoAulas.ACESSO)(Session["usuariologado"])).CPF);
				aulasDisponiveis.DIADASEMANA = string.Empty;
				aulasDisponiveis.QTDEAULAS = 0;
				aulasDisponiveis.idDiaSemana = 1;
				aulasDisponiveis.idHorario = 1;

				var arrHorario = aulasDisponiveis.HORARIO.Split(',');
				aulasDisponiveis.HORARIO = string.Empty;

				for (int i = 0; i < arrHorario.Count(); i++)
				{
					for (int j = 0; j < 30; j++)
					{
						if (arrHorario[i] == j.ToString())
						{
							aulasDisponiveis.QTDEAULAS++;
							if (j < 5)
							{
								horarioUm = true; verificaDia(j);
							}
							if (j >= 5 && j < 10)
							{
								horarioDois = true; verificaDia(j);
							}
							if (j >= 10 && j < 15)
							{
								horarioTres = true; verificaDia(j);
							}
							if (j >= 15 && j < 20)
							{
								horarioQuatro = true; verificaDia(j);
							}
							if (j >= 20 && j < 25)
							{
								horarioCinco = true; verificaDia(j);
							}
							if (j >= 25)
							{
								horarioSeis = true; verificaDia(j);
							}
							if (diaUm) aulasDisponiveis.DIADASEMANA = aulasDisponiveis.DIADASEMANA + "1,";
							if (diaDois) aulasDisponiveis.DIADASEMANA = aulasDisponiveis.DIADASEMANA + "2,";
							if (diaTres) aulasDisponiveis.DIADASEMANA = aulasDisponiveis.DIADASEMANA + "3,";
							if (diaQuatro) aulasDisponiveis.DIADASEMANA = aulasDisponiveis.DIADASEMANA + "4,";
							if (diaCinco) aulasDisponiveis.DIADASEMANA = aulasDisponiveis.DIADASEMANA + "5,";

							if (horarioUm) aulasDisponiveis.HORARIO = aulasDisponiveis.HORARIO + "1,";
							if (horarioDois) aulasDisponiveis.HORARIO = aulasDisponiveis.HORARIO + "2,";
							if (horarioTres) aulasDisponiveis.HORARIO = aulasDisponiveis.HORARIO + "3,";
							if (horarioQuatro) aulasDisponiveis.HORARIO = aulasDisponiveis.HORARIO + "4,";
							if (horarioCinco) aulasDisponiveis.HORARIO = aulasDisponiveis.HORARIO + "5,";
							if (horarioSeis) aulasDisponiveis.HORARIO = aulasDisponiveis.HORARIO + "6,";

							resetaVariaveis();
						}
					}
				}

				ViewBag.IdTipoAula = new SelectList(db.TipoAula.ToList(), "IdTipoAula", "descrTipoAula", ViewBag.descrTipoAula);
				ViewBag.IdTurno = new SelectList(db.Turno.ToList(), "IdTurno", "descrTurno", ViewBag.descrTurno);
				ViewBag.IdDisciplina = new SelectList(db.Disciplina.ToList(), "IdDisciplina", "descrDisciplina", ViewBag.descrDisciplina);
				ViewBag.IdAfastamento = new SelectList(db.Afastamento.ToList(), "IdAfastamento", "descrAfastamento", ViewBag.descrAfastamento);

				db.Entry(ad).State = EntityState.Deleted;
				db.Entry(aulasDisponiveis).State = EntityState.Added;

				db.SaveChanges();

				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				return View(aulasDisponiveis);
			}
		}

		public ActionResult Delete(int? id)
		{
			if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

			AULASDISPONIVEIS aulasDisponiveis = db.AULASDISPONIVEIS.Find(id);
			if (aulasDisponiveis == null) return HttpNotFound();

			return View(aulasDisponiveis);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		public void verificaDia(int i)
		{
			switch (i)
			{
				case 0: diaUm = true; break;
				case 1: diaDois = true; break;
				case 2: diaTres = true; break;
				case 3: diaQuatro = true; break;
				case 4: diaCinco = true; break;
				case 5: diaUm = true; break;
				case 6: diaDois = true; break;
				case 7: diaTres = true; break;
				case 8: diaQuatro = true; break;
				case 9: diaCinco = true; break;
				case 10: diaUm = true; break;
				case 11: diaDois = true; break;
				case 12: diaTres = true; break;
				case 13: diaQuatro = true; break;
				case 14: diaCinco = true; break;
				case 15: diaUm = true; break;
				case 16: diaDois = true; break;
				case 17: diaTres = true; break;
				case 18: diaQuatro = true; break;
				case 19: diaCinco = true; break;
				case 20: diaUm = true; break;
				case 21: diaDois = true; break;
				case 22: diaTres = true; break;
				case 23: diaQuatro = true; break;
				case 24: diaCinco = true; break;
				case 25: diaUm = true; break;
				case 26: diaDois = true; break;
				case 27: diaTres = true; break;
				case 28: diaQuatro = true; break;
				case 29: diaCinco = true; break;
			}
		}
		public void resetaVariaveis()
		{
			diaUm = false;
			diaDois = false;
			diaTres = false;
			diaQuatro = false;
			diaCinco = false;

			horarioUm = false;
			horarioDois = false;
			horarioTres = false;
			horarioQuatro = false;
			horarioCinco = false;
			horarioSeis = false;
		}

		public JsonResult Salvar(string Nome, string CPF, string Telefone, string[] arrAulas)
		{
			bool result = false;
			try
			{
				AulasAtribuidas aa = new AulasAtribuidas();

				for (int i = 0; i < arrAulas.Length; i++)
				{
					aa.IDAULASDISPONIVEIS = Convert.ToInt32(arrAulas[i]);
					aa.NomeProfessor = Nome;
					aa.CPF = CPF;
					aa.TELEFONE = Telefone;
					aa.CPFATRIBUICAO = (((atribuicaoAulas.ACESSO)(Session["usuariologado"])).CPF);
					aa.DTATRIBUICAO = DateTime.Now;

					db.AulasAtribuidas.Add(aa);
					db.SaveChanges();

					SetaAtribuido(Convert.ToInt32(arrAulas[i]));
				}
				result = true;
				return Json(result, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				var data = ex.InnerException;
				return Json(result, JsonRequestBehavior.AllowGet);
			}
		}

		[HttpPost]
		public PartialViewResult Filtrar(string Escola, string CPF, string Disciplina, string TipoAula, string dtCadastroInicio, string dtCadastroFim)
		{
			if (db.BloqueioCadastro.OrderByDescending(x => x.IdBloqueioCadastro).Count() == 0) ViewBag.BloqueioCadastroAula = "Desbloqueado";  //DESBLOQUEADO COMO STATUS INICIAL
			else ViewBag.BloqueioCadastroAula = db.BloqueioCadastro.OrderByDescending(x => x.IdBloqueioCadastro).FirstOrDefault().bloqueado == "S" ? "Bloqueado" : "Desbloqueado";
			ViewBag.MotivoExclusao = new SelectList(db.MotivoExclusao.OrderBy(x => x.descrMotivoExclusao).ToList(), "IdMotivoExclusao", "descrMotivoExclusao");

			List<AULASDISPONIVEIS> lstAulasDusponiveis = new List<AULASDISPONIVEIS>();
			try
			{
				if (Escola == "") Escola = "NULL";
				if (CPF == "") CPF = "NULL";
				if (Disciplina == "") Disciplina = "NULL";
				if (TipoAula == "") TipoAula = "NULL";

				StringBuilder sql = new StringBuilder(@"SELECT * FROM AULASDISPONIVEIS AA WHERE((" + CPF + " IS NULL) OR (AA.CPFCADASTRO =" + CPF + ")) AND ((" + Disciplina + " IS NULL) OR (AA.IDDISCIPLINA = " + Disciplina + ")) AND ((" + Escola + " IS NULL) OR (AA.IDESCOLA = " + Escola + ")) AND ((" + TipoAula + " IS NULL) OR (AA.IDTIPOAULA = " + TipoAula + ")) AND AA.EXCLUIDO = 0 AND ATRIBUIDA = 0");

				if (dtCadastroInicio != "" && dtCadastroFim == "")
				{
					sql.Append(" AND CONVERT(DATE, AA.DTCADASTRO,103) >= CONVERT(DATE, '" + dtCadastroInicio + "', 103)");
				}
				if (dtCadastroInicio == "" && dtCadastroFim != "")
				{
					sql.Append(" AND (CONVERT(DATE,AA.DTCADASTRO,103) =  CONVERT(DATE,'" + dtCadastroFim + "',103)");
				}
				if (dtCadastroInicio != "" && dtCadastroFim != "")
				{
					sql.Append(" AND (CONVERT(DATE,AA.DTCADASTRO,103) BETWEEN CONVERT(DATE,'" + dtCadastroInicio + "',103) AND CONVERT(DATE,'" + dtCadastroFim + "',103))");
				}

				var AulasDusponiveis = db.AULASDISPONIVEIS.SqlQuery(sql.ToString()).AsNoTracking().ToList();

				foreach (var aula in AulasDusponiveis)
				{
					AULASDISPONIVEIS ad = new AULASDISPONIVEIS();
					ad = db.AULASDISPONIVEIS.Find(aula.IDAULASDISPONIVEIS);
					lstAulasDusponiveis.Add(ad);
				}
				ViewBag.NomeEscola = (from ad in AulasDusponiveis
									  join es in db.Escola on ad.IDESCOLA equals es.IdEscola
									  select es.descrEscola).ToList();
				ViewBag.descrTurno = (from ad in AulasDusponiveis
									  join tu in db.Turno on ad.IDTURNO equals tu.IdTurno
									  select tu.descrTurno).ToList();
				ViewBag.descrTipoAula = (from ad in AulasDusponiveis
										 join ta in db.TipoAula on ad.IDTIPOAULA equals ta.IdTipoAula
										 select ta.descrTipoAula).ToList();
				ViewBag.descrDisciplina = (from ad in AulasDusponiveis
										   join dd in db.Disciplina on ad.IDDISCIPLINA equals dd.IdDisciplina
										   select dd.descrDisciplina).ToList();
				ViewBag.descrAfastamento = (from ad in AulasDusponiveis
											join af in db.Afastamento on ad.IDAFASTAMENTO equals af.IdAfastamento
											select af.descrAfastamento).ToList();

				return PartialView(lstAulasDusponiveis);

			}
			catch (Exception ex)
			{
				return PartialView();
			}
		}

		public void SetaAtribuido(int id)
		{

			AULASDISPONIVEIS ad = db.AULASDISPONIVEIS.Find(id);
			ad.atribuida = 1;
			TryUpdateModel(ad);
			db.SaveChanges();

			ModelState.AddModelError("", "Aulas atribuídas com sucesso.");
		}

		public JsonResult ImprimirComprovante(string Nome, string CPF, string Telefone, string[] arrAulas)
		{
			List<AULASDISPONIVEIS> lstAulasDisp = new List<AULASDISPONIVEIS>();

			for (int i = 0; i < arrAulas.Length; i++)
			{
				AULASDISPONIVEIS aula = new AULASDISPONIVEIS();
				aula = db.AULASDISPONIVEIS.Find(Convert.ToInt32(arrAulas[i]));
				lstAulasDisp.Add(aula);
			}

			var result = new { Nome = Nome, CPF = CPF, Telefone = Telefone, arrAulas = arrAulas };

			return Json(result, JsonRequestBehavior.AllowGet);
		}

		public ActionResult ImprimirFolhaComprovante(string dados)
		{
			if (dados != null)
			{
				var arrDados = dados.Split(';');
				var arrCPF = arrDados[1];
				List<AULASDISPONIVEIS> lstAulasDisponiveis = new List<AULASDISPONIVEIS>();
				List<string> lsEscolas = new List<string>();
				List<string> lsDisciplinas = new List<string>();
				List<string> lsTipoAulas = new List<string>();

				List<AulasAtribuidas> lstAulasAtribuidasTotal = db.AulasAtribuidas.Where(a => a.CPF == arrCPF).AsNoTracking().ToList();
				List<AulasAtribuidas> lstAulasAtribuidas = new List<AulasAtribuidas>();

				var nw = DateTime.Now.Date.ToString();
				for (int i = 0; i < lstAulasAtribuidasTotal.Count(); i++)
				{
					if (lstAulasAtribuidasTotal[i].DTATRIBUICAO.Date.ToString() == nw)
					{
						lstAulasAtribuidas.Add(lstAulasAtribuidasTotal[i]);
					}
				}
				//var dt = lstAulasAtribuidas[1].DTATRIBUICAO.Date.ToString();
				//Response.Write("<script>alert('"+ dt + "')</script>");
				//Response.Write("<script>alert('" + nw + "')</script>");

				foreach (var aula in lstAulasAtribuidas)
				{
					AULASDISPONIVEIS ad = new AULASDISPONIVEIS();
					ad = db.AULASDISPONIVEIS.Find(Convert.ToInt32(aula.IDAULASDISPONIVEIS));
					lstAulasDisponiveis.Add(ad);
					var Escola = db.Escola.Where(x => x.IdEscola == ad.IDESCOLA).Select(x => x.descrEscola).FirstOrDefault();
					lsEscolas.Add(Escola);
					var disciplina = db.Disciplina.Where(x => x.IdDisciplina == ad.IDDISCIPLINA).Select(x => x.descrDisciplina).FirstOrDefault();
					lsDisciplinas.Add(disciplina);
					var tipoAula = db.TipoAula.Where(x => x.IdTipoAula == ad.IDTIPOAULA).Select(x => x.descrTipoAula).FirstOrDefault();
					lsTipoAulas.Add(tipoAula);
				}

				ViewBag.lsEscolas = lsEscolas;
				ViewBag.lsDisciplinas = lsDisciplinas;
				ViewBag.lsTipoAulas = lsTipoAulas;
				ViewBag.lstAulasDisponiveis = lstAulasDisponiveis;

				return View(lstAulasAtribuidas);
			}

			return RedirectToAction("Index", "aulasDisponiveis");
		}

		
			public ActionResult ImprimirFolhaComprovanteDados(string dados)
		{
			if (dados != null)
			{
				var arrDados = dados.Split(';');
				var arrCPF = arrDados[1];
				List<AULASDISPONIVEIS> lstAulasDisponiveis = new List<AULASDISPONIVEIS>();
				List<string> lsEscolas = new List<string>();
				List<string> lsDisciplinas = new List<string>();
				List<string> lsTipoAulas = new List<string>();

				List<AulasAtribuidas> lstAulasAtribuidas = db.AulasAtribuidas.Where(a => a.CPF == arrCPF).AsNoTracking().ToList();
			
				foreach (var aula in lstAulasAtribuidas)
				{
					AULASDISPONIVEIS ad = new AULASDISPONIVEIS();
					ad = db.AULASDISPONIVEIS.Find(Convert.ToInt32(aula.IDAULASDISPONIVEIS));
					lstAulasDisponiveis.Add(ad);
					var Escola = db.Escola.Where(x => x.IdEscola == ad.IDESCOLA).Select(x => x.descrEscola).FirstOrDefault();
					lsEscolas.Add(Escola);
					var disciplina = db.Disciplina.Where(x => x.IdDisciplina == ad.IDDISCIPLINA).Select(x => x.descrDisciplina).FirstOrDefault();
					lsDisciplinas.Add(disciplina);
					var tipoAula = db.TipoAula.Where(x => x.IdTipoAula == ad.IDTIPOAULA).Select(x => x.descrTipoAula).FirstOrDefault();
					lsTipoAulas.Add(tipoAula);
				}

				ViewBag.lsEscolas = lsEscolas;
				ViewBag.lsDisciplinas = lsDisciplinas;
				ViewBag.lsTipoAulas = lsTipoAulas;
				ViewBag.lstAulasDisponiveis = lstAulasDisponiveis;

				return View(lstAulasAtribuidas);
			}

			return RedirectToAction("Index", "aulasDisponiveis");
		}

		[HttpPost]
		public PartialViewResult PesquisaAulasExcluidas(string CPF, string IDDISCIPLINA, string IDESCOLA, string MOTIVOEXCLUSAO)
		{
			List<AULASDISPONIVEIS> lstAulasDisponiveis = new List<AULASDISPONIVEIS>();
			List<string> lsEscolas = new List<string>();
			List<string> lsDisciplinas = new List<string>();
			List<string> lsTipoAulas = new List<string>();
			//List<string> lsDataCadastro = new List<string>();
			List<string> lsDescrMotivo = new List<string>();

			ViewBag.CPF = CPF; ViewBag.IDDISCIPLINA = IDDISCIPLINA; ViewBag.IDESCOLA = IDESCOLA; ViewBag.MOTIVOEXCLUSAO = MOTIVOEXCLUSAO;

			if (((atribuicaoAulas.ACESSO)(Session["usuarioLogado"])).IDPERFIL == 2 && CPF == "NULL")  // USUARIO
			{
				TempData["msg"] = "<script>alert('Por favor, preencha o campo CPF.');</script>";
				return PartialView();
			}

			try
			{
				StringBuilder sql = new StringBuilder("SELECT * FROM AULASDISPONIVEIS AD WHERE AD.EXCLUIDO = 1");

				if (CPF != string.Empty) sql.Append(" AND AD.CPFCADASTRO = '" + CPF + "'");
				if (IDDISCIPLINA != string.Empty) sql.Append(" AND AD.IDDISCIPLINA = " + IDDISCIPLINA.ToString() + "");
				if (IDESCOLA != string.Empty) sql.Append(" AND AD.IDESCOLA = " + IDESCOLA.ToString() + "");
				if (MOTIVOEXCLUSAO != string.Empty) sql.Append(" AND AD.IDMOTIVOEXCLUSAO = " + MOTIVOEXCLUSAO.ToString() + "");

				var result = db.AULASDISPONIVEIS.SqlQuery(sql.ToString()).AsNoTracking().ToList();

				foreach (var aula in result)
				{
					AULASDISPONIVEIS ad = new AULASDISPONIVEIS();
					ad = db.AULASDISPONIVEIS.Find(Convert.ToInt32(aula.IDAULASDISPONIVEIS));
					lstAulasDisponiveis.Add(ad);
					var Escola = db.Escola.Where(x => x.IdEscola == ad.IDESCOLA).Select(x => x.descrEscola).FirstOrDefault();
					lsEscolas.Add(Escola);
					var disciplina = db.Disciplina.Where(x => x.IdDisciplina == ad.IDDISCIPLINA).Select(x => x.descrDisciplina).FirstOrDefault();
					lsDisciplinas.Add(disciplina);
					var tipoAula = db.TipoAula.Where(x => x.IdTipoAula == ad.IDTIPOAULA).Select(x => x.descrTipoAula).FirstOrDefault();
					lsTipoAulas.Add(tipoAula);
					var desrMotivo = db.MotivoExclusao.Where(a => a.IdMotivoExclusao == ad.idMotivoExclusao).Select(x => x.descrMotivoExclusao).FirstOrDefault();
					lsDescrMotivo.Add(desrMotivo);

				}

				ViewBag.lsEscolas = lsEscolas;
				ViewBag.lsDisciplinas = lsDisciplinas;
				ViewBag.lsTipoAulas = lsTipoAulas;
				ViewBag.lstAulasDisponiveis = lstAulasDisponiveis;
				ViewBag.lsDescrMotivo = lsDescrMotivo;

				return PartialView(result);
			}
			catch (Exception ex)
			{
				return PartialView();
			}
		}

		[HttpPost]
		public PartialViewResult PesquisaAulasAtribuidas(string CPF, string IDDISCIPLINA, string IDESCOLA)
		{
			List<AULASDISPONIVEIS> lstAulasDisponiveis = new List<AULASDISPONIVEIS>();
			List<string> lsEscolas = new List<string>();
			List<string> lsDisciplinas = new List<string>();
			List<string> lsTipoAulas = new List<string>();
			List<string> lsDataCadastro = new List<string>();

			if (CPF == string.Empty) CPF = "NULL";
			if (IDDISCIPLINA == string.Empty) IDDISCIPLINA = "NULL";
			if (IDESCOLA == string.Empty) IDESCOLA = "NULL";
			ViewBag.CPF = CPF; ViewBag.IDDISCIPLINA = IDDISCIPLINA; ViewBag.IDESCOLA = IDESCOLA;

			if (((atribuicaoAulas.ACESSO)(Session["usuarioLogado"])).IDPERFIL == 2 && CPF == "NULL")  // USUARIO
			{
				TempData["msg"] = "<script>alert('Por favor, preencha o campo CPF.');</script>";
				return PartialView();
			}

			try
			{
				string sql = @"	DECLARE @CPF AS VARCHAR(50) = NULL, @IDDISCIPLINA varchar (10) = NULL, @IDESCOLA AS varchar (10) = NULL SET @CPF = " + CPF + " set @IDDISCIPLINA = " + IDDISCIPLINA + "  set @IDESCOLA = " + IDESCOLA + " SELECT * FROM AULASATRIBUIDAS AA INNER JOIN AULASDISPONIVEIS AD ON AD.IDAULASDISPONIVEIS = AA.IDAULASDISPONIVEIS WHERE ((@CPF IS NULL)  OR (AA.CPF = @CPF)) AND ((@IDDISCIPLINA IS NULL) OR (AD.IDDISCIPLINA = @IDDISCIPLINA)) AND ((@IDESCOLA IS NULL) OR (AD.IDESCOLA = @IDESCOLA))";
				var result = db.AulasAtribuidas.SqlQuery(sql).AsNoTracking().ToList();

				foreach (var aula in result)
				{
					AULASDISPONIVEIS ad = new AULASDISPONIVEIS();
					ad = db.AULASDISPONIVEIS.Find(Convert.ToInt32(aula.IDAULASDISPONIVEIS));
					lstAulasDisponiveis.Add(ad);
					var Escola = db.Escola.Where(x => x.IdEscola == ad.IDESCOLA).Select(x => x.descrEscola).FirstOrDefault();
					lsEscolas.Add(Escola);
					var disciplina = db.Disciplina.Where(x => x.IdDisciplina == ad.IDDISCIPLINA).Select(x => x.descrDisciplina).FirstOrDefault();
					lsDisciplinas.Add(disciplina);
					var tipoAula = db.TipoAula.Where(x => x.IdTipoAula == ad.IDTIPOAULA).Select(x => x.descrTipoAula).FirstOrDefault();
					lsTipoAulas.Add(tipoAula);
					lsDataCadastro.Add(ad.DTCADASTRO.ToString());
				}

				ViewBag.lsEscolas = lsEscolas;
				ViewBag.lsDisciplinas = lsDisciplinas;
				ViewBag.lsTipoAulas = lsTipoAulas;
				ViewBag.lstAulasDisponiveis = lstAulasDisponiveis;
				ViewBag.lsDataCadastro = lsDataCadastro;

				return PartialView(result);
			}
			catch (Exception ex)
			{
				return PartialView();
			}
		}

		public ActionResult relAulasAtribuidas()
		{
			ViewBag.ultimoRecado = db.Recado.OrderByDescending(x => x.IdRecado).FirstOrDefault();

			List<AULASDISPONIVEIS> lstAulasDisponiveis = new List<AULASDISPONIVEIS>();
			List<string> lsEscolas = new List<string>();
			List<string> lsDisciplinas = new List<string>();
			List<string> lsTipoAulas = new List<string>();
			List<string> lsDataCadastro = new List<string>();

			if (((atribuicaoAulas.ACESSO)(Session["usuarioLogado"])).IDPERFIL == 1 || ((atribuicaoAulas.ACESSO)(Session["usuarioLogado"])).IDPERFIL == 3)  // Não é Admin
			{
				ViewBag.IdEscola = new SelectList(db.Escola.OrderBy(x => x.descrEscola).ToList(), "IdEscola", "descrEscola");
			}
			else
			{
				var idEsc = ((atribuicaoAulas.ACESSO)(Session["usuariologado"])).IDESCOLA;
				ViewBag.IdEscola = new SelectList(db.Escola.Where(x => x.IdEscola == idEsc).OrderBy(x => x.descrEscola).ToList(), "IdEscola", "descrEscola");
			}

			ViewBag.IdDisciplina = new SelectList(db.Disciplina.OrderBy(x => x.descrDisciplina).ToList(), "IdDisciplina", "descrDisciplina");

			List<AulasAtribuidas> aat = db.AulasAtribuidas.AsNoTracking().ToList();

			foreach (var aula in aat)
			{
				AULASDISPONIVEIS ad = new AULASDISPONIVEIS();
				ad = db.AULASDISPONIVEIS.Find(Convert.ToInt32(aula.IDAULASDISPONIVEIS));
				lstAulasDisponiveis.Add(ad);
				var Escola = db.Escola.Where(x => x.IdEscola == ad.IDESCOLA).Select(x => x.descrEscola).FirstOrDefault();
				lsEscolas.Add(Escola);
				var disciplina = db.Disciplina.Where(x => x.IdDisciplina == ad.IDDISCIPLINA).Select(x => x.descrDisciplina).FirstOrDefault();
				lsDisciplinas.Add(disciplina);
				var tipoAula = db.TipoAula.Where(x => x.IdTipoAula == ad.IDTIPOAULA).Select(x => x.descrTipoAula).FirstOrDefault();
				lsTipoAulas.Add(tipoAula);
				lsDataCadastro.Add(ad.DTCADASTRO.ToString());
			}

			ViewBag.lsEscolas = lsEscolas;
			ViewBag.lsDisciplinas = lsDisciplinas;
			ViewBag.lsTipoAulas = lsTipoAulas;
			ViewBag.lstAulasDisponiveis = lstAulasDisponiveis;
			ViewBag.lsDataCadastro = lsDataCadastro;

			return View(aat);
		}

		public ActionResult relAulasExcluidas()
		{
			ViewBag.ultimoRecado = db.Recado.OrderByDescending(x => x.IdRecado).FirstOrDefault();

			List<AULASDISPONIVEIS> lstAulasDisponiveis = new List<AULASDISPONIVEIS>();
			List<string> lsEscolas = new List<string>();
			List<string> lsDisciplinas = new List<string>();
			List<string> lsTipoAulas = new List<string>();
			//List<string> lsDataCadastro = new List<string>();
			List<string> lsDescrMotivo = new List<string>();

			if (((atribuicaoAulas.ACESSO)(Session["usuarioLogado"])).IDPERFIL == 1 || ((atribuicaoAulas.ACESSO)(Session["usuarioLogado"])).IDPERFIL == 3)  // Não é Admin
			{
				ViewBag.IdEscola = new SelectList(db.Escola.OrderBy(x => x.descrEscola).ToList(), "IdEscola", "descrEscola");
			}
			else
			{
				var idEsc = ((atribuicaoAulas.ACESSO)(Session["usuariologado"])).IDESCOLA;
				ViewBag.IdEscola = new SelectList(db.Escola.Where(x => x.IdEscola == idEsc).OrderBy(x => x.descrEscola).ToList(), "IdEscola", "descrEscola");
			}

			ViewBag.IdDisciplina = new SelectList(db.Disciplina.OrderBy(x => x.descrDisciplina).ToList(), "IdDisciplina", "descrDisciplina");
			ViewBag.MotivoExclusao = new SelectList(db.MotivoExclusao.OrderBy(x => x.descrMotivoExclusao).ToList(), "IdMotivoExclusao", "descrMotivoExclusao");

			List<AULASDISPONIVEIS> aat = db.AULASDISPONIVEIS.Where(x => x.EXCLUIDO == 1).AsNoTracking().ToList();

			foreach (var aula in aat)
			{
				AULASDISPONIVEIS ad = new AULASDISPONIVEIS();
				ad = db.AULASDISPONIVEIS.Find(Convert.ToInt32(aula.IDAULASDISPONIVEIS));
				lstAulasDisponiveis.Add(ad);
				var Escola = db.Escola.Where(x => x.IdEscola == ad.IDESCOLA).Select(x => x.descrEscola).FirstOrDefault();
				lsEscolas.Add(Escola);
				var disciplina = db.Disciplina.Where(x => x.IdDisciplina == ad.IDDISCIPLINA).Select(x => x.descrDisciplina).FirstOrDefault();
				lsDisciplinas.Add(disciplina);
				var tipoAula = db.TipoAula.Where(x => x.IdTipoAula == ad.IDTIPOAULA).Select(x => x.descrTipoAula).FirstOrDefault();
				lsTipoAulas.Add(tipoAula);
				var desrMotivo = db.MotivoExclusao.Where(a => a.IdMotivoExclusao == ad.idMotivoExclusao).Select(x => x.descrMotivoExclusao).FirstOrDefault();
				lsDescrMotivo.Add(desrMotivo);
			}

			ViewBag.lsEscolas = lsEscolas;
			ViewBag.lsDisciplinas = lsDisciplinas;
			ViewBag.lsTipoAulas = lsTipoAulas;
			ViewBag.lstAulasDisponiveis = lstAulasDisponiveis;
			ViewBag.lsDescrMotivo = lsDescrMotivo;

			return View(aat);
		}

		public FileResult ExportExcelAulasDisponiveis()
		{
			if (((atribuicaoAulas.ACESSO)(Session["usuarioLogado"])).IDPERFIL != 1)  // Não é Admin
			{
				var idEsc = ((atribuicaoAulas.ACESSO)(Session["usuariologado"])).IDESCOLA;

				DataTable dt = new DataTable("AulasDisponiveis");
				dt.Columns.AddRange(new DataColumn[8] { new DataColumn("Escola"),
											new DataColumn("Disciplina"),
											new DataColumn("Turno"),
											new DataColumn("Tipo de Aula") ,
											new DataColumn("Afastamento"),
											new DataColumn("Período Afastamento"),
											new DataColumn("Qtde de Aulas"),
											new DataColumn("Turma"),
										 });
				var aulasDisponiveis = db.AULASDISPONIVEIS.Where(x => x.IDESCOLA == idEsc).Where(a => a.EXCLUIDO == 0).Where(x => x.atribuida == 0).AsNoTracking().ToList();

				var NomeEscola = (from ad in aulasDisponiveis
								  join es in db.Escola on ad.IDESCOLA equals es.IdEscola
								  where ad.EXCLUIDO == 0
								  select es.descrEscola).ToList();
				var descrTurno = (from ad in aulasDisponiveis
								  join tu in db.Turno on ad.IDTURNO equals tu.IdTurno
								  where ad.EXCLUIDO == 0
								  select tu.descrTurno).ToList();
				var descrTipoAula = (from ad in aulasDisponiveis
									 join ta in db.TipoAula on ad.IDTIPOAULA equals ta.IdTipoAula
									 where ad.EXCLUIDO == 0
									 select ta.descrTipoAula).ToList();
				var descrDisciplina = (from ad in aulasDisponiveis
									   join dd in db.Disciplina on ad.IDDISCIPLINA equals dd.IdDisciplina
									   where ad.EXCLUIDO == 0
									   select dd.descrDisciplina).ToList();
				var descrAfastamento = (from ad in aulasDisponiveis
										join af in db.Afastamento on ad.IDAFASTAMENTO equals af.IdAfastamento
										where ad.EXCLUIDO == 0
										select af.descrAfastamento).ToList();

				for (int i = 0; i < aulasDisponiveis.Count; i++)
				{
					dt.Rows.Add(NomeEscola[i], descrDisciplina[i], descrTurno[i], descrTipoAula[i], descrAfastamento[i], aulasDisponiveis[i].PERIODOAFASTAMENTO, aulasDisponiveis[i].QTDEAULAS, aulasDisponiveis[i].TURMA);
				}


				using (XLWorkbook wb = new XLWorkbook())
				{
					wb.Worksheets.Add(dt);
					using (MemoryStream stream = new MemoryStream())
					{
						wb.SaveAs(stream);
						return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "aulasDisponiveis_" + DateTime.Now + ".xlsx");
					}
				}
			}
			else
			{
				DataTable dt = new DataTable("AulasDisponiveis");
				dt.Columns.AddRange(new DataColumn[10] { new DataColumn("Escola"),
											new DataColumn("Disciplina"),
											new DataColumn("Turno"),
											new DataColumn("Tipo de Aula") ,
											new DataColumn("Afastamento"),
											new DataColumn("Período Afastamento"),
											new DataColumn("Qtde de Aulas"),
											new DataColumn("Turma"),
											new DataColumn("Data Cadastro"),
											new DataColumn("CPF Cadastro")});
				var aulasDisponiveis = db.AULASDISPONIVEIS.Where(a => a.EXCLUIDO == 0).Where(x => x.atribuida == 0).AsNoTracking().ToList();

				var NomeEscola = (from ad in aulasDisponiveis
								  join es in db.Escola on ad.IDESCOLA equals es.IdEscola
								  where ad.EXCLUIDO == 0
								  select es.descrEscola).ToList();
				var descrTurno = (from ad in aulasDisponiveis
								  join tu in db.Turno on ad.IDTURNO equals tu.IdTurno
								  where ad.EXCLUIDO == 0
								  select tu.descrTurno).ToList();
				var descrTipoAula = (from ad in aulasDisponiveis
									 join ta in db.TipoAula on ad.IDTIPOAULA equals ta.IdTipoAula
									 where ad.EXCLUIDO == 0
									 select ta.descrTipoAula).ToList();
				var descrDisciplina = (from ad in aulasDisponiveis
									   join dd in db.Disciplina on ad.IDDISCIPLINA equals dd.IdDisciplina
									   where ad.EXCLUIDO == 0
									   select dd.descrDisciplina).ToList();
				var descrAfastamento = (from ad in aulasDisponiveis
										join af in db.Afastamento on ad.IDAFASTAMENTO equals af.IdAfastamento
										where ad.EXCLUIDO == 0
										select af.descrAfastamento).ToList();

				for (int i = 0; i < aulasDisponiveis.Count; i++)
				{
					dt.Rows.Add(NomeEscola[i], descrDisciplina[i], descrTurno[i], descrTipoAula[i], descrAfastamento[i], aulasDisponiveis[i].PERIODOAFASTAMENTO, aulasDisponiveis[i].QTDEAULAS, aulasDisponiveis[i].TURMA, aulasDisponiveis[i].DTCADASTRO, aulasDisponiveis[i].CPFCADASTRO);
				}

				using (XLWorkbook wb = new XLWorkbook())
				{
					wb.Worksheets.Add(dt);
					using (MemoryStream stream = new MemoryStream())
					{
						wb.SaveAs(stream);
						return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "aulasDisponiveis_" + DateTime.Now + ".xlsx");
					}
				}
			}
		}

		public FileResult ExportExcelAulasAtribuidas(string CPF, string IDDISCIPLINA, string IDESCOLA)
		{
			DataTable dt = new DataTable("AulasAtribuidas");
			dt.Columns.AddRange(new DataColumn[10] { new DataColumn("Nome"),
											new DataColumn("CPF"),
											new DataColumn("Telefone"),
											new DataColumn("Escola") ,
											new DataColumn("Disciplina"),
											new DataColumn("Turno"),
											new DataColumn("Tipo de Aula"),
											new DataColumn("Qtde de Aulas"),
											new DataColumn("Turma"),
											new DataColumn("Data Cadastro")});

			var result = (from aa in db.AulasAtribuidas
						  join ad in db.AULASDISPONIVEIS on aa.IDAULASDISPONIVEIS equals ad.IDAULASDISPONIVEIS
						  join esc in db.Escola on ad.IDESCOLA equals esc.IdEscola
						  join disc in db.Disciplina on ad.IDDISCIPLINA equals disc.IdDisciplina
						  join tur in db.Turno on ad.IDTURNO equals tur.IdTurno
						  join tp in db.TipoAula on ad.IDTIPOAULA equals tp.IdTipoAula
						  where ((CPF == "NULL") || (aa.CPF == CPF)) && ((IDDISCIPLINA == "NULL") || (ad.IDDISCIPLINA.ToString() == IDDISCIPLINA)) && ((IDESCOLA == "NULL") || (ad.IDESCOLA.ToString() == IDESCOLA))
						  select new
						  {
							  Nome = aa.NomeProfessor,
							  CPF = aa.CPF,
							  Telefone = aa.TELEFONE,
							  Escola = esc.descrEscola,
							  Disciplina = disc.descrDisciplina,
							  Turno = tur.descrTurno,
							  TipoAula = tp.descrTipoAula,
							  qtdeAulas = ad.QTDEAULAS,
							  turma = ad.TURMA,
							  dtcadastro = ad.DTCADASTRO
						  }).AsNoTracking().ToList();

			for (int i = 0; i < result.Count; i++)
			{
				dt.Rows.Add(result[i].Nome, result[i].CPF, result[i].Telefone, result[i].Escola, result[i].Disciplina, result[i].turma, result[i].TipoAula, result[i].qtdeAulas, result[i].turma, result[i].dtcadastro);
			}

			using (XLWorkbook wb = new XLWorkbook())
			{
				wb.Worksheets.Add(dt);
				using (MemoryStream stream = new MemoryStream())
				{
					wb.SaveAs(stream);
					return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AulasAtribuidas_" + DateTime.Now + ".xlsx");
				}
			}
		}

		public JsonResult ExcluirSelecionados(string[] arrAulas, string idMotivoExclusao)
		{
			bool result = false;
			List<AULASDISPONIVEIS> lstAulasDisponiveis = new List<AULASDISPONIVEIS>();

			for (int i = 0; i < arrAulas.Count(); i++)
			{
				AULASDISPONIVEIS ad = new AULASDISPONIVEIS();
				ad = db.AULASDISPONIVEIS.Find(Convert.ToInt32(arrAulas[i]));
				lstAulasDisponiveis.Add(ad);
			}

			lstAulasDisponiveis.ForEach(x => { x.EXCLUIDO = 1; x.idMotivoExclusao = Convert.ToInt32(idMotivoExclusao); x.CPFALTERACAO = ((atribuicaoAulas.ACESSO)(Session["usuariologado"])).CPF; x.DTALTERACAO = DateTime.Now; });

			db.SaveChanges();

			return Json(result, JsonRequestBehavior.AllowGet);
		}

		public JsonResult CancelarAtribuicao(string[] arrAtribuidas)
		{
			bool result = false;
			List<AULASDISPONIVEIS> lstAulasDisponiveis = new List<AULASDISPONIVEIS>();
			List<AulasAtribuidas> lstAulasAtribuidas = new List<AulasAtribuidas>();
			for (int i = 0; i < arrAtribuidas.Count(); i++)
			{
				AulasAtribuidas aat = new AulasAtribuidas();
				aat = db.AulasAtribuidas.Find(Convert.ToInt32(arrAtribuidas[i]));
				lstAulasAtribuidas.Add(aat);

				AULASDISPONIVEIS ad = new AULASDISPONIVEIS();
				ad = db.AULASDISPONIVEIS.Find(aat.IDAULASDISPONIVEIS);
				lstAulasDisponiveis.Add(ad);
				db.AulasAtribuidas.Remove(aat);
			}

			lstAulasDisponiveis.ForEach(x => { x.atribuida = 0; });

			db.SaveChanges();

			return Json(result, JsonRequestBehavior.AllowGet);

		}

		public JsonResult ExcluirDefinitivamente(string[] arrExcluidas)
		{
			bool result = false;
			List<AULASDISPONIVEIS> lstAulasDisponiveis = new List<AULASDISPONIVEIS>();

			for (int i = 0; i < arrExcluidas.Count(); i++)
			{
				AULASDISPONIVEIS ad = new AULASDISPONIVEIS();
				ad = db.AULASDISPONIVEIS.Find(Convert.ToInt32(arrExcluidas[i]));
				lstAulasDisponiveis.Add(ad);
				db.AULASDISPONIVEIS.Remove(ad);
				db.SaveChanges();

				result = true;
			}

			return Json(result, JsonRequestBehavior.AllowGet);

		}

	}
}