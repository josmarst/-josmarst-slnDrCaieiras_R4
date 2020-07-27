using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace atribuicaoAulas.Models
{
    public class aulasDisponiveisAgrupadas
    {
        [Key]
        public int id { get; set; }
        public int qtdeAulas { get; set; }

    }
}