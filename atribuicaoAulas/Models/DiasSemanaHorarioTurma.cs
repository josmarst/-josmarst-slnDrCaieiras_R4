using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace atribuicaoAulas.Models
{
	public class DiasSemanaHorarioTurma
	{
		[Key]
		public int idDiasSemanaHorarioTurma { get; set; }

		public virtual int idDisciplina { get; set; }
		public virtual string descrDisciplina { get; set; }

		public virtual int idTurno { get; set; }
		public virtual string descrTurno { get; set; }

		public virtual List<int> Horario { get; set; }
		public virtual List<string> descrHorario { get; set; }

		public virtual List<int> DiaDaSemana { get; set; }
		public virtual List<string> descrDiaDaSemana { get; set; }
		                    
		public virtual string turma { get; set; }
		
		public virtual string professor { get; set; }
	}
}

