namespace atribuicaoAulas
{
    using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public partial class aulasDisponiveis
	{
		[Key]
		public int IdAulasDisponiveis { get; set; }

		[NotMapped]
		[Display(Name = "Dia da Semana")]
		public virtual List<int> DiasSemana { get; set; }

		[NotMapped]
		[Display(Name = "Horário")]
		public virtual List<int> Horarios { get; set; }

		[Required(ErrorMessage = "Selecione Tipo de Aula")]
		[Display(Name = "Tipo Aula")]
		public virtual int idTipoAula { get; set; }

		[Required(ErrorMessage = "Selecione um Afastamento")]
		[Display(Name = "Tipo afastamanto")]
		public virtual int idAfastamento { get; set; }

		[Required(ErrorMessage = "Informe o Professor")]
		[Display(Name = "Profº Titular")]
		public virtual string ProfTitular { get; set; }

		[Required(ErrorMessage = "Selecione um Período")]
		[StringLength(50)]
		[Display(Name = "Período Afastamanto")]
		public string PeriodoAfastamento { get; set; }

		[Required(ErrorMessage = "Informe qtde de Aulas")]
		[Display(Name = "Qtde. Aulas")]
		public int qtdeAulas { get; set; }

		[Required(ErrorMessage = "Selecione uma Escola")]
		[Display(Name = "Escola")]
		public virtual int idEscola { get; set; }

		[Display(Name = "Data Cadastro")]
		public virtual DateTime dtcadastro { get; set; }

		[Display(Name = "CPF Cadastro")]
		public virtual string cpfcadastro { get; set; }

		[Display(Name = "Data Alteração")]
		public virtual DateTime? dtalteracao { get; set; }

		[Display(Name = "CPF Alteração")]
		public virtual string cpfalteracao { get; set; }

		public virtual int excluido { get; set; }


	//	public virtual List<DiasSemanaHorarioTurma> lstDiasSemanaHorarioTurma { get;set;}

		[Required(ErrorMessage = "Selecione uma Disciplina")]
		[Display(Name = "Disciplina")]
		public virtual int idDisciplina { get; set; }

		[Required(ErrorMessage = "Selecione um Turno")]
		[Display(Name = "Turno")]
		public virtual int idTurno { get; set; }

		public virtual string DiaDaSemana { get; set; }

		public virtual string Horario { get; set; }

		[Display(Name = "Turma")]
		[Required(ErrorMessage = "Informe uma Turma")]
		public virtual string turma { get; set; }

		//public virtual int idDiaSemana { get; set; }
		
		//public virtual int idHorario { get; set; }



	}
}
