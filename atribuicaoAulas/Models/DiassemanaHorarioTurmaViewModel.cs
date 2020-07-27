using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace atribuicaoAulas.Models
{
	[DataContract]
	public class DiassemanaHorarioTurmaViewModel
	{
		[DataMember]
		public int idDiasSemanaHorarioTurma { get; set; }

		[DataMember]
		public virtual int idDisciplina { get; set; }

		[DataMember]
		public virtual int idTurno { get; set; }

		[DataMember]
		public virtual int[] Horario { get; set; }

		[DataMember]
		public virtual int[] DiaDaSemana { get; set; }

		[DataMember]
		public virtual string turma { get; set; }
	}
}