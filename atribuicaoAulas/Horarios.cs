namespace atribuicaoAulas
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Horarios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Horarios()
        {
            AULASDISPONIVEIS = new HashSet<AULASDISPONIVEIS>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdHorario { get; set; }

        [Required]
        [StringLength(15)]
        public string descrHorario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AULASDISPONIVEIS> AULASDISPONIVEIS { get; set; }
    }
}
