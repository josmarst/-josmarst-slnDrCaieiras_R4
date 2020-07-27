namespace atribuicaoAulas
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TipoAula")]
    public partial class TipoAula
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoAula()
        {
            AULASDISPONIVEIS = new HashSet<AULASDISPONIVEIS>();
        }

        [Key]
        public int IdTipoAula { get; set; }

        [Required]
        [StringLength(50)]
        public string descrTipoAula { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AULASDISPONIVEIS> AULASDISPONIVEIS { get; set; }
    }
}
