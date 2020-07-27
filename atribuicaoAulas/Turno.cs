namespace atribuicaoAulas
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
            AULASDISPONIVEIS = new HashSet<AULASDISPONIVEIS>();
        }

        [Key]
        public int IdTurno { get; set; }

        [Required]
        [StringLength(50)]
        public string descrTurno { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AULASDISPONIVEIS> AULASDISPONIVEIS { get; set; }
    }
}
