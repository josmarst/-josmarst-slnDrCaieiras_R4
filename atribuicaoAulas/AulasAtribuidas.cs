namespace atribuicaoAulas
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class AulasAtribuidas
    {
        [Key]
        public int IdAulaAtribuida { get; set; }

        public int IDAULASDISPONIVEIS { get; set; }

        [Required]
        [StringLength(100)]
        public string NomeProfessor { get; set; }

        [Required]
        [StringLength(14)]
        public string CPF { get; set; }

        [StringLength(14)]
        public string TELEFONE { get; set; }
			
		public DateTime DTATRIBUICAO { get; set; }

		[StringLength(11)]
		public string CPFATRIBUICAO { get; set; }

		public virtual AULASDISPONIVEIS AULASDISPONIVEIS { get; set; }
    }
}
