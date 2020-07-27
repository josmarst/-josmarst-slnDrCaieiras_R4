namespace atribuicaoAulas.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ACESSO")]
	public partial class ACESSO
	{
		[Key]
		[StringLength(11)]
		[Required]
		[RegularExpression("[0-9]{3}.?[0-9]{3}.?[0-9]{3}-?[0-9]{2}", ErrorMessage = "CPF inválido.")]
		public string CPF { get; set; }

        [StringLength(100, ErrorMessage = "A {0} deve ter mais de {2} caracteres.", MinimumLength = 6)]
		public string SENHA { get; set; }

		[NotMapped]
		[DataType(DataType.Password)]
		[Display(Name = "Confirmar senha")]
		[StringLength(100, ErrorMessage = "A {0} deve ter mais de {2} caracteres.", MinimumLength = 6)]
		[Compare("SENHA", ErrorMessage = "As senhas não conferem.")]
		public string ConfirmaSENHA { get; set; }

		[StringLength(1)]
		public string ATIVO { get; set; }

		public int? IdPERFIL { get; set; }

		[StringLength(50)]
		public string NOME { get; set; }

		public int? idEscola { get; set; }
	}
}
