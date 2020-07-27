namespace atribuicaoAulas
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Afastamento")]
    public partial class Afastamento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Afastamento()
        {
            AULASDISPONIVEIS = new HashSet<AULASDISPONIVEIS>();
        }

        [Key]
        public int IdAfastamento { get; set; }

        [Required]
        [StringLength(50)]
        public string descrAfastamento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AULASDISPONIVEIS> AULASDISPONIVEIS { get; set; }
    }
}
