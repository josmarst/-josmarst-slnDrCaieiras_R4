namespace atribuicaoAulas
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Estado")]
    public partial class Estado
    {
       // GIT Estado Segundo passo
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Estado()
        {
            Municipio = new HashSet<Municipio>();
        }

        [Key]
        public int IdEstado { get; set; }

        [Required]
        [StringLength(50)]
        public string desricao { get; set; }

        [StringLength(2)]
        public string UF { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Municipio> Municipio { get; set; }
    }
}
