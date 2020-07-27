namespace atribuicaoAulas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Afastamento")]
    public partial class Afastamento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Afastamento()
        {
            aulasDisponiveis = new HashSet<aulasDisponiveis>();
        }

        [Key]
        public int IdAfastamento { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Descrição do Afastamento")]
        public virtual string descrAfastamento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aulasDisponiveis> aulasDisponiveis { get; set; }
    }
}
