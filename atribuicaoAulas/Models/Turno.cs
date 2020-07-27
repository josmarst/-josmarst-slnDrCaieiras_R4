namespace atribuicaoAulas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Turno")]
    public partial class Turno
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Turno()
        {
            aulasDisponiveis = new HashSet<aulasDisponiveis>();
        }

        [Key]
        public int IdTurno { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Turno")]
        public virtual string descrTurno { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aulasDisponiveis> aulasDisponiveis { get; set; }
    }
}
