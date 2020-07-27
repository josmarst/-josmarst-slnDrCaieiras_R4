namespace atribuicaoAulas
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("BloqueioCadastro")]
    public partial class BloqueioCadastro
    {
        [Key]
        public int IdBloqueioCadastro { get; set; }

        [Required]
        [StringLength(1)]
        public string bloqueado { get; set; }

        public DateTime dataBloqueio { get; set; }

        [Required]
        [StringLength(11)]
        public string CPFBloqueio { get; set; }
    }
}
