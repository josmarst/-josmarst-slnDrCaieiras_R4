namespace atribuicaoAulas
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ACESSO")]
    public partial class ACESSO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ACESSO()
        {
            AULASDISPONIVEIS = new HashSet<AULASDISPONIVEIS>();
        }

        [Key]
        [StringLength(11)]
        public string CPF { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        public string senhaAntiga { get; set; }

        [Required]
        [StringLength(100)]
        public string SENHA { get; set; }


        [NotMapped]
        public string ConfirmaSENHA { get; set; }

        [Required]
        [StringLength(1)]
        public string ATIVO { get; set; }

        public int IDPERFIL { get; set; }

        //[Required]
        [StringLength(50)]
        public string NOME { get; set; }

        public int IDESCOLA { get; set; }

        public virtual Escola Escola { get; set; }

        public virtual perfil perfil { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AULASDISPONIVEIS> AULASDISPONIVEIS { get; set; }
    }
}
