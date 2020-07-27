namespace atribuicaoAulas.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;

	[Table("Escola")]
	public partial class Escola
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public Escola()
		{

		}

		[Key]
		public int IdEscola { get; set; }

		[Required]
		[StringLength(50)]
		[Display(Name = "Escola")]
		public virtual string descrEscola { get; set; }

		public int IdMunicipio { get; set; }

		public virtual Municipio Municipio { get; set; }

		public virtual int excluido { get; set; }
	}
}
