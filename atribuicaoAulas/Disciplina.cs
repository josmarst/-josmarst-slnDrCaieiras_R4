namespace atribuicaoAulas
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Disciplina")]
    public partial class Disciplina
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Disciplina()
        {
            AULASDISPONIVEIS = new HashSet<AULASDISPONIVEIS>();
        }

        [Key]
        public int IdDisciplina { get; set; }

        [Required]
        [StringLength(50)]
        public string descrDisciplina { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AULASDISPONIVEIS> AULASDISPONIVEIS { get; set; }
    }
}
