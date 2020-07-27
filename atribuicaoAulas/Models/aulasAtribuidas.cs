namespace atribuicaoAulas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    //[Table("AulasAtribuidas")]
    public partial class aulasAtribuidas
    {
        [Key]
        public int IdAulaAtribuida { get; set; }

        public virtual int IDAULASDISPONIVEIS { get; set; }

        public virtual string NomeProfessor { get; set; }

        public virtual string CPF { get; set; }

        public virtual string TELEFONE { get; set; }

    }
}
