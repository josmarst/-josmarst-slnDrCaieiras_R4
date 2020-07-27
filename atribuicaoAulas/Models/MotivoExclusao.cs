namespace atribuicaoAulas
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MotivoExclusao")]
    public partial class MotivoExclusao
    {
        [Key]
        public int IdMotivoExclusao { get; set; }

        [Required]
        [StringLength(50)]
        public string descrMotivoExclusao { get; set; }

        public int excluido { get; set; }
    }
}
