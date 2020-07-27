namespace atribuicaoAulas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("perfil")]
    public partial class perfil
    {
        [Key]
        public int IdPERFIL { get; set; }

        [Required]
        [StringLength(30)]
        public string DescrPerfil { get; set; }
    }
}
