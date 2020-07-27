namespace atribuicaoAulas
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("perfil")]
    public partial class perfil
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public perfil()
        {
            ACESSO = new HashSet<ACESSO>();
        }

        [Key]
        public int IdPERFIL { get; set; }

        [Required]
        [StringLength(30)]
        public string DescrPerfil { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACESSO> ACESSO { get; set; }
    }
}
