using System.ComponentModel.DataAnnotations;

namespace atribuicaoAulas
{
    public class aulasDisponiveisAgrupadas
    {
        [Key]
        public int id { get; set; }
        public int qtdeAulas { get; set; }
        public string profTitular { get; set; }

    }
}