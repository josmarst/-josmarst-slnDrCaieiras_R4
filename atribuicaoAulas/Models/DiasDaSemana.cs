namespace atribuicaoAulas.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;

	[Table("DiasDaSemana")]
	public partial class DiasDaSemana
	{
		[Key]
		public int IdDiaSemana { get; set; }
		public string descrDiaSemana { get; set; }
}
}




