using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using atribuicaoAulas.Models;
using System;

namespace atribuicaoAulas.Controllers
{
	public class ACESSOController : Controller
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
		bool horarioSete = false;
		#endregion

		// GET: ACESSO
		public ActionResult Index()
		{
			return View(db.ACESSO.ToList());
		}

		// GET: ACESSO/Details/5
		public ActionResult Details(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			ACESSO aCESSO = db.ACESSO.Find(id);
			if (aCESSO == null)
			{
				return HttpNotFound();
			}
			return View(aCESSO);
		}

		// GET: ACESSO/Create
		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(ACESSO aCESSO)
		{
			if (ModelState.IsValid)
			{
				db.ACESSO.Add(aCESSO);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(aCESSO);
		}

		// GET: ACESSO/Edit/5
		public ActionResult Edit(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			ACESSO aCESSO = db.ACESSO.Find(id);
			if (aCESSO == null)
			{
				return HttpNotFound();
			}
			return View(aCESSO);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "CPF,SENHA,ATIVO,IdPERFIL,NOME,idEscola")] ACESSO aCESSO)
		{
			if (ModelState.IsValid)
			{
				db.Entry(aCESSO).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(aCESSO);
		}

		[HttpGet]
		public PartialViewResult Delete(int Id)
		{
			List<DiasSemanaHorarioTurma> lstDs = (List<DiasSemanaHorarioTurma>)Session["SessionDiasSemanaHorarioTurma"];
			DiasSemanaHorarioTurma lstRemove = new DiasSemanaHorarioTurma();

			lstRemove = lstDs.Find(x => x.idDiasSemanaHorarioTurma == Id);
			lstDs.Remove(lstRemove);

			return PartialView("_AtualizarGrid", lstDs);
		}

		// POST: ACESSO/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(string id)
		{
			ACESSO aCESSO = db.ACESSO.Find(id);
			db.ACESSO.Remove(aCESSO);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		[HttpPost]
		public ActionResult gravarDados(string ProfTitular, int escola, int? disciplinaManha, int? disciplinaTarde, int? disciplinaNoite, int tipoAula, int tipoAfastamento, string periodoAfastamento, string[] arrManha, string[] arrTarde, string[] arrNoite, string atpcManha, string atpcTarde, string atpcNoite)
		{
			if (db.BloqueioCadastro.OrderByDescending(x => x.IdBloqueioCadastro).Count() == 0) ViewBag.BloqueioCadastroAula = "Desbloqueado";  //DESBLOQUEADO COMO STATUS INICIAL
			else ViewBag.BloqueioCadastroAula = db.BloqueioCadastro.OrderByDescending(x => x.IdBloqueioCadastro).FirstOrDefault().bloqueado == "S" ? "Bloqueado" : "Desbloqueado";

			AULASDISPONIVEIS aula = new AULASDISPONIVEIS();

			aula.DIADASEMANA = string.Empty;
			aula.HORARIO = string.Empty;
			aula.QTDEAULAS = 0;

			aula.IDESCOLA = escola;
			aula.PROFTITULAR = ProfTitular;
			aula.IDTIPOAULA = tipoAula;
			aula.PERIODOAFASTAMENTO = periodoAfastamento;
			aula.IDAFASTAMENTO = tipoAfastamento;

			try
			{
				#region Cadastro Período Manhã
				if (arrManha != null)
				{
					for (int i = 0; i < arrManha.Length; i++)
					{
						arrManha[i] = arrManha[i].ToUpper();
					}

					aula.ATPC = atpcManha;
					var arrManhaDistinct = arrManha.Distinct().Where(x => x != "").ToArray();

					for (int j = 0; j < arrManhaDistinct.Length; j++)
					{
						for (int i = 0; i < arrManha.Length; i++)
						{
							if (arrManha[i] != "")
							{
								if ((arrManha[i] == arrManhaDistinct[j]))
								{
									aula.IDDISCIPLINA = (int)disciplinaManha;
									aula.TURMA = arrManha[i];
									aula.IDTURNO = 1;
									aula.QTDEAULAS++;
									if (i < 5)
									{
										horarioUm = true; verificaDia(i);
									}
									if (i >= 5 && i < 10)
									{
										horarioDois = true; verificaDia(i);
									}
									if (i >= 10 && i < 15)
									{
										horarioTres = true; verificaDia(i);
									}
									if (i >= 15 && i < 20)
									{
										horarioQuatro = true; verificaDia(i);
									}
									if (i >= 20 && i < 25)
									{
										horarioCinco = true; verificaDia(i);
									}
									if (i >= 25 && i < 30)
									{
										horarioSeis = true; verificaDia(i);
									}
									if (i >= 30)
									{
										horarioSete = true; verificaDia(i);
									}

									if (diaUm) aula.DIADASEMANA = aula.DIADASEMANA + "1,";
									if (diaDois) aula.DIADASEMANA = aula.DIADASEMANA + "2,";
									if (diaTres) aula.DIADASEMANA = aula.DIADASEMANA + "3,";
									if (diaQuatro) aula.DIADASEMANA = aula.DIADASEMANA + "4,";
									if (diaCinco) aula.DIADASEMANA = aula.DIADASEMANA + "5,";

									if (horarioUm) aula.HORARIO = aula.HORARIO + "1,";
									if (horarioDois) aula.HORARIO = aula.HORARIO + "2,";
									if (horarioTres) aula.HORARIO = aula.HORARIO + "3,";
									if (horarioQuatro) aula.HORARIO = aula.HORARIO + "4,";
									if (horarioCinco) aula.HORARIO = aula.HORARIO + "5,";
									if (horarioSeis) aula.HORARIO = aula.HORARIO + "6,";
									if (horarioSete) aula.HORARIO = aula.HORARIO + "7,";

									resetaVariaveis();
								}
							}
						}

						if (aula.DIADASEMANA != "")
						{
							aula.idDiaSemana = 1;
							aula.idHorario = 1;
							aula.CPFCADASTRO = (((atribuicaoAulas.ACESSO)(Session["usuariologado"])).CPF);
							aula.DTCADASTRO = DateTime.Now;
							db.AULASDISPONIVEIS.Add(aula);
							db.SaveChanges();
						}

						aula.DIADASEMANA = string.Empty;
						aula.HORARIO = string.Empty;
						aula.QTDEAULAS = 0;

						resetaVariaveis();
					}
				}
			}
			catch (Exception ex)
			{
				string strErro = ex.Message.ToString();
				Response.Write("<script language=javascript>alert('" + strErro + "');</script>");

				//Console.WriteLine(ex.StackTrace.ToString());
			}
			#endregion

			#region Cadastro Período Tarde
			if (arrTarde != null)
			{
				for (int i = 0; i < arrTarde.Length; i++)
				{
					arrTarde[i] = arrTarde[i].ToUpper();
				}

				aula.ATPC = atpcTarde;
				var arrTardeDistinct = arrTarde.Distinct().Where(x => x != "").ToArray();

				for (int j = 0; j < arrTardeDistinct.Length; j++)
				{
					for (int i = 0; i < arrTarde.Length; i++)
					{
						if (arrTarde[i] != "")
						{
							if ((arrTarde[i] == arrTardeDistinct[j]))
							{
								aula.IDDISCIPLINA = (int)disciplinaTarde;
								aula.TURMA = arrTarde[i];
								aula.IDTURNO = 2;
								aula.QTDEAULAS++;
								if (i < 5)
								{
									horarioUm = true; verificaDia(i);
								}
								if (i >= 5 && i < 10)
								{
									horarioDois = true; verificaDia(i);
								}
								if (i >= 10 && i < 15)
								{
									horarioTres = true; verificaDia(i);
								}
								if (i >= 15 && i < 20)
								{
									horarioQuatro = true; verificaDia(i);
								}
								if (i >= 20 && i < 25)
								{
									horarioCinco = true; verificaDia(i);
								}
								if (i >= 25 && i < 30)
								{
									horarioSeis = true; verificaDia(i);
								}
								if (i >= 30)
								{
									horarioSete = true; verificaDia(i);
								}

								if (diaUm) aula.DIADASEMANA = aula.DIADASEMANA + "1,";
								if (diaDois) aula.DIADASEMANA = aula.DIADASEMANA + "2,";
								if (diaTres) aula.DIADASEMANA = aula.DIADASEMANA + "3,";
								if (diaQuatro) aula.DIADASEMANA = aula.DIADASEMANA + "4,";
								if (diaCinco) aula.DIADASEMANA = aula.DIADASEMANA + "5,";

								if (horarioUm) aula.HORARIO = aula.HORARIO + "1,";
								if (horarioDois) aula.HORARIO = aula.HORARIO + "2,";
								if (horarioTres) aula.HORARIO = aula.HORARIO + "3,";
								if (horarioQuatro) aula.HORARIO = aula.HORARIO + "4,";
								if (horarioCinco) aula.HORARIO = aula.HORARIO + "5,";
								if (horarioSeis) aula.HORARIO = aula.HORARIO + "6,";
								if (horarioSete) aula.HORARIO = aula.HORARIO + "7,";

								resetaVariaveis();
							}
						}
					}
					if (aula.DIADASEMANA != "")
					{
						aula.idDiaSemana = 1;
						aula.idHorario = 1;
						aula.CPFCADASTRO = (((atribuicaoAulas.ACESSO)(Session["usuariologado"])).CPF);
						aula.DTCADASTRO = DateTime.Now;
						db.AULASDISPONIVEIS.Add(aula);
						db.SaveChanges();
					}

					aula.DIADASEMANA = string.Empty;
					aula.HORARIO = string.Empty;
					aula.QTDEAULAS = 0;

					resetaVariaveis();
				}
			}
			#endregion

			#region Cadastro Período Noite
			if (arrNoite != null)
			{
				for (int i = 0; i < arrNoite.Length; i++)
				{
					arrNoite[i] = arrNoite[i].ToUpper();
				}

				aula.ATPC = atpcNoite;
				var arrNoiteDistinct = arrNoite.Distinct().Where(x => x != "").ToArray();
				for (int j = 0; j < arrNoiteDistinct.Length; j++)
				{
					for (int i = 0; i < arrNoite.Length; i++)
					{
						if (arrNoite[i] != "")
						{
							if ((arrNoite[i] == arrNoiteDistinct[j]))
							{
								aula.IDDISCIPLINA = (int)disciplinaNoite;
								aula.TURMA = arrNoite[i];
								aula.IDTURNO = 3;
								aula.QTDEAULAS++;
								if (i < 5)
								{
									horarioUm = true; verificaDia(i);
								}
								if (i >= 5 && i < 10)
								{
									horarioDois = true; verificaDia(i);
								}
								if (i >= 10 && i < 15)
								{
									horarioTres = true; verificaDia(i);
								}
								if (i >= 15 && i < 20)
								{
									horarioQuatro = true; verificaDia(i);
								}
								if (i >= 20 && i < 25)
								{
									horarioCinco = true; verificaDia(i);
								}
								if (i >= 25 && i < 30)
								{
									horarioSeis = true; verificaDia(i);
								}
								if (i >= 30)
								{
									horarioSete = true; verificaDia(i);
								}

								if (diaUm) aula.DIADASEMANA = aula.DIADASEMANA + "1,";
								if (diaDois) aula.DIADASEMANA = aula.DIADASEMANA + "2,";
								if (diaTres) aula.DIADASEMANA = aula.DIADASEMANA + "3,";
								if (diaQuatro) aula.DIADASEMANA = aula.DIADASEMANA + "4,";
								if (diaCinco) aula.DIADASEMANA = aula.DIADASEMANA + "5,";

								if (horarioUm) aula.HORARIO = aula.HORARIO + "1,";
								if (horarioDois) aula.HORARIO = aula.HORARIO + "2,";
								if (horarioTres) aula.HORARIO = aula.HORARIO + "3,";
								if (horarioQuatro) aula.HORARIO = aula.HORARIO + "4,";
								if (horarioCinco) aula.HORARIO = aula.HORARIO + "5,";
								if (horarioSeis) aula.HORARIO = aula.HORARIO + "6,";
								if (horarioSete) aula.HORARIO = aula.HORARIO + "7,";

								resetaVariaveis();
							}
						}
					}
					if (aula.DIADASEMANA != "")
					{
						aula.idDiaSemana = 1;
						aula.idHorario = 1;
						aula.CPFCADASTRO = (((atribuicaoAulas.ACESSO)(Session["usuariologado"])).CPF);
						aula.DTCADASTRO = DateTime.Now;
						db.AULASDISPONIVEIS.Add(aula);
						db.SaveChanges();
					}

					aula.DIADASEMANA = string.Empty;
					aula.HORARIO = string.Empty;
					aula.QTDEAULAS = 0;

					resetaVariaveis();
				}
			}
			#endregion

			return RedirectToAction("Create", "aulasDisponiveis", aula);
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
				case 30: diaUm = true; break;
				case 31: diaDois = true; break;
				case 32: diaTres = true; break;
				case 33: diaQuatro = true; break;
				case 34: diaCinco = true; break;
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
			horarioSete = false;
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
