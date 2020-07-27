namespace atribuicaoAulas
{
    using System.Data.Entity;

    public partial class PerfilDB_ : DbContext
    {
        public PerfilDB_()
            : base("name=PerfilDB_")
        {        }

        public virtual DbSet<ACESSO> ACESSO { get; set; }
        public virtual DbSet<Afastamento> Afastamento { get; set; }
        public virtual DbSet<AulasAtribuidas> AulasAtribuidas { get; set; }
        public virtual DbSet<AULASDISPONIVEIS> AULASDISPONIVEIS { get; set; }
        public virtual DbSet<BloqueioCadastro> BloqueioCadastro { get; set; }
        public virtual DbSet<DiasDaSemana> DiasDaSemana { get; set; }
        public virtual DbSet<Disciplina> Disciplina { get; set; }
        public virtual DbSet<Escola> Escola { get; set; }
        public virtual DbSet<Estado> Estado { get; set; }
        public virtual DbSet<Horarios> Horarios { get; set; }
        public virtual DbSet<Municipio> Municipio { get; set; }
        public virtual DbSet<perfil> perfil { get; set; }
        public virtual DbSet<Recado> Recado { get; set; }
        public virtual DbSet<TipoAula> TipoAula { get; set; }
        public virtual DbSet<Turno> Turno { get; set; }
        public virtual DbSet<MotivoExclusao> MotivoExclusao { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ACESSO>()
                .Property(e => e.CPF)
                .IsUnicode(false);

            modelBuilder.Entity<ACESSO>()
                .Property(e => e.SENHA)
                .IsUnicode(false);

            modelBuilder.Entity<ACESSO>()
                .Property(e => e.ATIVO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ACESSO>()
                .Property(e => e.NOME)
                .IsUnicode(false);

            modelBuilder.Entity<ACESSO>()
                .HasMany(e => e.AULASDISPONIVEIS)
                .WithRequired(e => e.ACESSO)
                .HasForeignKey(e => e.CPFCADASTRO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Afastamento>()
                .HasMany(e => e.AULASDISPONIVEIS)
                .WithRequired(e => e.Afastamento)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AulasAtribuidas>()
             .Property(e => e.NomeProfessor)
             .IsUnicode(false);

            modelBuilder.Entity<AulasAtribuidas>()
                .Property(e => e.CPF)
                .IsUnicode(false);

            modelBuilder.Entity<AulasAtribuidas>()
                .Property(e => e.TELEFONE)
                .IsUnicode(false);
            modelBuilder.Entity<AULASDISPONIVEIS>()
                .Property(e => e.CPFCADASTRO)
                .IsUnicode(false);

            modelBuilder.Entity<AULASDISPONIVEIS>()
                .Property(e => e.CPFALTERACAO)
                .IsUnicode(false);

            modelBuilder.Entity<AULASDISPONIVEIS>()
                .Property(e => e.HORARIO)
                .IsUnicode(false);

            modelBuilder.Entity<AULASDISPONIVEIS>()
                .Property(e => e.TURMA)
                .IsUnicode(false);

            modelBuilder.Entity<AULASDISPONIVEIS>()
                .Property(e => e.DIADASEMANA)
                .IsFixedLength();

            modelBuilder.Entity<BloqueioCadastro>()
                .Property(e => e.bloqueado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BloqueioCadastro>()
                .Property(e => e.CPFBloqueio)
                .IsFixedLength();

            modelBuilder.Entity<DiasDaSemana>()
                .Property(e => e.descrDiaSemana)
                .IsFixedLength();

            modelBuilder.Entity<DiasDaSemana>()
                .HasMany(e => e.AULASDISPONIVEIS)
                .WithRequired(e => e.DiasDaSemana)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Disciplina>()
                .HasMany(e => e.AULASDISPONIVEIS)
                .WithRequired(e => e.Disciplina)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Escola>()
                .HasMany(e => e.ACESSO)
                .WithRequired(e => e.Escola)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Escola>()
                .HasMany(e => e.AULASDISPONIVEIS)
                .WithRequired(e => e.Escola)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Estado>()
                .Property(e => e.UF)
                .IsUnicode(false);

            modelBuilder.Entity<Estado>()
                .HasMany(e => e.Municipio)
                .WithRequired(e => e.Estado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Horarios>()
                .Property(e => e.descrHorario)
                .IsFixedLength();

            modelBuilder.Entity<Horarios>()
                .HasMany(e => e.AULASDISPONIVEIS)
                .WithRequired(e => e.Horarios)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Municipio>()
                .HasMany(e => e.Escola)
                .WithRequired(e => e.Municipio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<perfil>()
                .Property(e => e.DescrPerfil)
                .IsUnicode(false);

            modelBuilder.Entity<perfil>()
                .HasMany(e => e.ACESSO)
                .WithRequired(e => e.perfil)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Recado>()
                .Property(e => e.CPFCriacao)
                .IsUnicode(false);

            modelBuilder.Entity<TipoAula>()
                .HasMany(e => e.AULASDISPONIVEIS)
                .WithRequired(e => e.TipoAula)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Turno>()
                .HasMany(e => e.AULASDISPONIVEIS)
                .WithRequired(e => e.Turno)
                .WillCascadeOnDelete(false);
          
        }

    }
}
