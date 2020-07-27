using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace atribuicaoAulas.Controllers
{
	public class AccountChangePassword
	{
		[StringLength(11)]
		[Required]
		[RegularExpression("[0-9]{3}.?[0-9]{3}.?[0-9]{3}-?[0-9]{2}", ErrorMessage = "CPF inválido.")]
		public string CPF { get; set; }

		[NotMapped]
		[Required]
		public string senhaAntiga { get; set; }

		[StringLength(100, ErrorMessage = "A {0} deve ter mais de {2} caracteres.", MinimumLength = 6)]
		public string SENHA { get; set; }

		[NotMapped]
		[DataType(DataType.Password)]
		[Display(Name = "Confirmar senha")]
		[Compare("SENHA", ErrorMessage = "As senhas não conferem.")]
		public string ConfirmaSENHA { get; set; }

		public string Nome { get; set; }

	}
}