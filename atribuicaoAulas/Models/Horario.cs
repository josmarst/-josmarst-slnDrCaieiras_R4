using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace atribuicaoAulas.Models
{
	public partial class Horario
	{
		[Key]
		public int IdHorario { get; set; }
		
		[Required]
		[StringLength(15)]
		[Display(Name = "Horário")]
		public string descrHorario { get; set; }
	}
}





