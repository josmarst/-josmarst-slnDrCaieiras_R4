using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace atribuicaoAulas.Models
{
    public class RegisterViewModel
    {
        [StringLength(11)]
        [Required]
        [RegularExpression("[0-9]{3}.?[0-9]{3}.?[0-9]{3}-?[0-9]{2}", ErrorMessage = "CPF inválido.")]
        public string CPF { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0} deve ter mais de {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [StringLength(100, ErrorMessage = "A {0} deve ter mais de {2} caracteres.", MinimumLength = 6)]
        [Compare("Password", ErrorMessage = "As senhas não conferem.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Nome")]
        [Required]
        public string Nome { get; set; }

        [Display(Name = "Perfil")]
        [Required]
        public int idperfil { get; set; }

        [Required(ErrorMessage = "Selecione um Município.")]
        public int? idMunicipio { get; set; }

        [Required(ErrorMessage = "Selecione uma Escola.")]
        public int? idEscola { get; set; }

        [Display(Name = "E-mail")]
        [Required]
        [StringLength(100)]
        //  [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        //   [RegularExpression(@"b[A-Z0-9._%-]+@[A-Z0-9.-]+.[A-Z]{2,4}b", ErrorMessage = "E-mail em formato inválido.")]
        public string email { get; set; }

    }
}
