namespace atribuicaoAulas
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Recado")]
    public partial class Recado
    {
        [Key]
        public int IdRecado { get; set; }

        [StringLength(100)]
        public string dataRecado { get; set; }

        [StringLength(100)]
        public string EnderecoRecado { get; set; }
        
        [StringLength(600)]
        public string descrRecado { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? dtPublicacao { get; set; }

        [Required]
        [StringLength(11)]
        public string CPFCriacao { get; set; }
    }
}
