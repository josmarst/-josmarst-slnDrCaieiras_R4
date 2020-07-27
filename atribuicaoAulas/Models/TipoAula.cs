namespace atribuicaoAulas.Models
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
            aulasDisponiveis = new HashSet<aulasDisponiveis>();
        }

        [Key]
        public int IdTipoAula { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Tipo de Aula")]
        public virtual string descrTipoAula { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aulasDisponiveis> aulasDisponiveis { get; set; }
    }
}
