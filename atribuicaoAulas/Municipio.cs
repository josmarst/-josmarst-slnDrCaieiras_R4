namespace atribuicaoAulas
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Municipio")]
    public partial class Municipio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Municipio()
        {
            Escola = new HashSet<Escola>();
        }

        [Key]
        public int IdMunicipio { get; set; }

        [Required]
        [StringLength(50)]
        public string descrMunicipio { get; set; }

        public int IdEstado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Escola> Escola { get; set; }

        public virtual Estado Estado { get; set; }
    }
}
