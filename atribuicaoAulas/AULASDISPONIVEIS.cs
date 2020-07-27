namespace atribuicaoAulas
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	public partial class AULASDISPONIVEIS
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public AULASDISPONIVEIS()
		{
			AulasAtribuidas = new HashSet<AulasAtribuidas>();
		}

		[Key]
		public int IDAULASDISPONIVEIS { get; set; }

		public int IDDISCIPLINA { get; set; }

		public int IDTURNO { get; set; }

		public int IDTIPOAULA { get; set; }

		public int IDAFASTAMENTO { get; set; }

		[Required]
		[StringLength(50)]
		public string PERIODOAFASTAMENTO { get; set; }

		public int QTDEAULAS { get; set; }

		public int IDESCOLA { get; set; }

		public DateTime DTCADASTRO { get; set; }

		[Required]
		[StringLength(11)]
		public string CPFCADASTRO { get; set; }

		public int EXCLUIDO { get; set; }

		[StringLength(11)]
		public string CPFALTERACAO { get; set; }

		public DateTime? DTALTERACAO { get; set; }

		[Required]
		[StringLength(100)]
		public string HORARIO { get; set; }

		[Required]
		[StringLength(20)]
		public string TURMA { get; set; }

		[StringLength(50)]
		public string PROFTITULAR { get; set; }

		[StringLength(100)]
		public string DIADASEMANA { get; set; }

		public int idDiaSemana { get; set; }

		public int idHorario { get; set; }

		public int? idMotivoExclusao { get; set; }

		[StringLength(400)]  
		public string ATPC { get; set; }

		public int atribuida { get; set; }

		public virtual ACESSO ACESSO { get; set; }

		public virtual Afastamento Afastamento { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<AulasAtribuidas> AulasAtribuidas { get; set; }

		public virtual Escola Escola { get; set; }

		public virtual DiasDaSemana DiasDaSemana { get; set; }

		public virtual Disciplina Disciplina { get; set; }

		public virtual Horarios Horarios { get; set; }

		public virtual TipoAula TipoAula { get; set; }

		public virtual Turno Turno { get; set; }

		public static explicit operator AULASDISPONIVEIS(List<object> v)
		{
			throw new NotImplementedException();
		}
	}
}
