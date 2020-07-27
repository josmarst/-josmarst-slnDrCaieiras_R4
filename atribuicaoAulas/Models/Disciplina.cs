namespace atribuicaoAulas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Disciplina")]
    public partial class Disciplina
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Disciplina()
        {
            aulasDisponiveis = new HashSet<aulasDisponiveis>();
       
        }

        [Key]
        public int IdDisciplina { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Disciplina")]
        public virtual string descrDisciplina { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aulasDisponiveis> aulasDisponiveis { get; set; }
    
    }
}
