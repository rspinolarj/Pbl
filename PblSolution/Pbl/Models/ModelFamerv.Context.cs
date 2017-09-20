﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Pbl.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class FamervEntities : DbContext
    {
        public FamervEntities()
            : base("name=FamervEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Aluno> Aluno { get; set; }
        public virtual DbSet<Ano> Ano { get; set; }
        public virtual DbSet<Aula> Aula { get; set; }
        public virtual DbSet<AvaliacaoTutoria> AvaliacaoTutoria { get; set; }
        public virtual DbSet<ControleNotas> ControleNotas { get; set; }
        public virtual DbSet<ControleNotasXAula> ControleNotasXAula { get; set; }
        public virtual DbSet<ControleNotasXProva> ControleNotasXProva { get; set; }
        public virtual DbSet<Disciplina> Disciplina { get; set; }
        public virtual DbSet<Grupo> Grupo { get; set; }
        public virtual DbSet<InscricaoTurma> InscricaoTurma { get; set; }
        public virtual DbSet<Med> Med { get; set; }
        public virtual DbSet<Modulo> Modulo { get; set; }
        public virtual DbSet<PerguntaXFicha> PerguntaXFicha { get; set; }
        public virtual DbSet<Problema> Problema { get; set; }
        public virtual DbSet<ProblemaXMed> ProblemaXMed { get; set; }
        public virtual DbSet<Professor> Professor { get; set; }
        public virtual DbSet<Prova> Prova { get; set; }
        public virtual DbSet<Semestre> Semestre { get; set; }
        public virtual DbSet<TipoDisciplina> TipoDisciplina { get; set; }
        public virtual DbSet<TipoProva> TipoProva { get; set; }
        public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }
        public virtual DbSet<Turma> Turma { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<FichaAvaliacao> FichaAvaliacao { get; set; }
        public virtual DbSet<Pergunta> Pergunta { get; set; }
    
        public virtual int SpCrudAluno(Nullable<int> id, string nome, string cpf, string senha, string matricula, string statementType)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var nomeParameter = nome != null ?
                new ObjectParameter("nome", nome) :
                new ObjectParameter("nome", typeof(string));
    
            var cpfParameter = cpf != null ?
                new ObjectParameter("cpf", cpf) :
                new ObjectParameter("cpf", typeof(string));
    
            var senhaParameter = senha != null ?
                new ObjectParameter("senha", senha) :
                new ObjectParameter("senha", typeof(string));
    
            var matriculaParameter = matricula != null ?
                new ObjectParameter("matricula", matricula) :
                new ObjectParameter("matricula", typeof(string));
    
            var statementTypeParameter = statementType != null ?
                new ObjectParameter("StatementType", statementType) :
                new ObjectParameter("StatementType", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SpCrudAluno", idParameter, nomeParameter, cpfParameter, senhaParameter, matriculaParameter, statementTypeParameter);
        }
    
        public virtual int SpCrudAno(Nullable<int> id, string ano, string statementType)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var anoParameter = ano != null ?
                new ObjectParameter("ano", ano) :
                new ObjectParameter("ano", typeof(string));
    
            var statementTypeParameter = statementType != null ?
                new ObjectParameter("StatementType", statementType) :
                new ObjectParameter("StatementType", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SpCrudAno", idParameter, anoParameter, statementTypeParameter);
        }
    
        public virtual int SpCrudDisciplina(Nullable<int> id, Nullable<int> idTipoDisciplina, string descricao, string statementType)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var idTipoDisciplinaParameter = idTipoDisciplina.HasValue ?
                new ObjectParameter("idTipoDisciplina", idTipoDisciplina) :
                new ObjectParameter("idTipoDisciplina", typeof(int));
    
            var descricaoParameter = descricao != null ?
                new ObjectParameter("descricao", descricao) :
                new ObjectParameter("descricao", typeof(string));
    
            var statementTypeParameter = statementType != null ?
                new ObjectParameter("StatementType", statementType) :
                new ObjectParameter("StatementType", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SpCrudDisciplina", idParameter, idTipoDisciplinaParameter, descricaoParameter, statementTypeParameter);
        }
    
        public virtual int SpCrudGrupo(Nullable<int> id, Nullable<int> idProfessor, string descricao, Nullable<int> idMed, string statementType)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var idProfessorParameter = idProfessor.HasValue ?
                new ObjectParameter("idProfessor", idProfessor) :
                new ObjectParameter("idProfessor", typeof(int));
    
            var descricaoParameter = descricao != null ?
                new ObjectParameter("descricao", descricao) :
                new ObjectParameter("descricao", typeof(string));
    
            var idMedParameter = idMed.HasValue ?
                new ObjectParameter("idMed", idMed) :
                new ObjectParameter("idMed", typeof(int));
    
            var statementTypeParameter = statementType != null ?
                new ObjectParameter("StatementType", statementType) :
                new ObjectParameter("StatementType", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SpCrudGrupo", idParameter, idProfessorParameter, descricaoParameter, idMedParameter, statementTypeParameter);
        }
    
        public virtual int SpCrudMed(Nullable<int> id, string descricao, Nullable<int> idSemestre, string statementType)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var descricaoParameter = descricao != null ?
                new ObjectParameter("descricao", descricao) :
                new ObjectParameter("descricao", typeof(string));
    
            var idSemestreParameter = idSemestre.HasValue ?
                new ObjectParameter("idSemestre", idSemestre) :
                new ObjectParameter("idSemestre", typeof(int));
    
            var statementTypeParameter = statementType != null ?
                new ObjectParameter("StatementType", statementType) :
                new ObjectParameter("StatementType", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SpCrudMed", idParameter, descricaoParameter, idSemestreParameter, statementTypeParameter);
        }
    
        public virtual int SpCrudModulo(Nullable<int> id, string descricao, Nullable<System.DateTime> dtInicio, Nullable<System.DateTime> dtFim, Nullable<int> idSemestre, string statementType)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var descricaoParameter = descricao != null ?
                new ObjectParameter("descricao", descricao) :
                new ObjectParameter("descricao", typeof(string));
    
            var dtInicioParameter = dtInicio.HasValue ?
                new ObjectParameter("dtInicio", dtInicio) :
                new ObjectParameter("dtInicio", typeof(System.DateTime));
    
            var dtFimParameter = dtFim.HasValue ?
                new ObjectParameter("dtFim", dtFim) :
                new ObjectParameter("dtFim", typeof(System.DateTime));
    
            var idSemestreParameter = idSemestre.HasValue ?
                new ObjectParameter("idSemestre", idSemestre) :
                new ObjectParameter("idSemestre", typeof(int));
    
            var statementTypeParameter = statementType != null ?
                new ObjectParameter("StatementType", statementType) :
                new ObjectParameter("StatementType", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SpCrudModulo", idParameter, descricaoParameter, dtInicioParameter, dtFimParameter, idSemestreParameter, statementTypeParameter);
        }
    
        public virtual int SpCrudProblema(Nullable<int> id, string titulo, string descricao, string statementType)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var tituloParameter = titulo != null ?
                new ObjectParameter("titulo", titulo) :
                new ObjectParameter("titulo", typeof(string));
    
            var descricaoParameter = descricao != null ?
                new ObjectParameter("descricao", descricao) :
                new ObjectParameter("descricao", typeof(string));
    
            var statementTypeParameter = statementType != null ?
                new ObjectParameter("StatementType", statementType) :
                new ObjectParameter("StatementType", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SpCrudProblema", idParameter, tituloParameter, descricaoParameter, statementTypeParameter);
        }
    
        public virtual int SpCrudProfessor(Nullable<int> id, string nome, string cpf, string senha, string statementType)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var nomeParameter = nome != null ?
                new ObjectParameter("nome", nome) :
                new ObjectParameter("nome", typeof(string));
    
            var cpfParameter = cpf != null ?
                new ObjectParameter("cpf", cpf) :
                new ObjectParameter("cpf", typeof(string));
    
            var senhaParameter = senha != null ?
                new ObjectParameter("senha", senha) :
                new ObjectParameter("senha", typeof(string));
    
            var statementTypeParameter = statementType != null ?
                new ObjectParameter("StatementType", statementType) :
                new ObjectParameter("StatementType", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SpCrudProfessor", idParameter, nomeParameter, cpfParameter, senhaParameter, statementTypeParameter);
        }
    
        public virtual int SpCrudProva(Nullable<int> id, Nullable<int> idTipoProva, Nullable<decimal> valorQuestao, string statementType)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var idTipoProvaParameter = idTipoProva.HasValue ?
                new ObjectParameter("idTipoProva", idTipoProva) :
                new ObjectParameter("idTipoProva", typeof(int));
    
            var valorQuestaoParameter = valorQuestao.HasValue ?
                new ObjectParameter("valorQuestao", valorQuestao) :
                new ObjectParameter("valorQuestao", typeof(decimal));
    
            var statementTypeParameter = statementType != null ?
                new ObjectParameter("StatementType", statementType) :
                new ObjectParameter("StatementType", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SpCrudProva", idParameter, idTipoProvaParameter, valorQuestaoParameter, statementTypeParameter);
        }
    
        public virtual int SpCrudSemestre(Nullable<int> id, Nullable<System.DateTime> dtIni1, Nullable<System.DateTime> dtIni2, Nullable<System.DateTime> dtIni3, Nullable<System.DateTime> dtFim1, Nullable<System.DateTime> dtFim2, Nullable<System.DateTime> dtFim3, string statementType)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var dtIni1Parameter = dtIni1.HasValue ?
                new ObjectParameter("dtIni1", dtIni1) :
                new ObjectParameter("dtIni1", typeof(System.DateTime));
    
            var dtIni2Parameter = dtIni2.HasValue ?
                new ObjectParameter("dtIni2", dtIni2) :
                new ObjectParameter("dtIni2", typeof(System.DateTime));
    
            var dtIni3Parameter = dtIni3.HasValue ?
                new ObjectParameter("dtIni3", dtIni3) :
                new ObjectParameter("dtIni3", typeof(System.DateTime));
    
            var dtFim1Parameter = dtFim1.HasValue ?
                new ObjectParameter("dtFim1", dtFim1) :
                new ObjectParameter("dtFim1", typeof(System.DateTime));
    
            var dtFim2Parameter = dtFim2.HasValue ?
                new ObjectParameter("dtFim2", dtFim2) :
                new ObjectParameter("dtFim2", typeof(System.DateTime));
    
            var dtFim3Parameter = dtFim3.HasValue ?
                new ObjectParameter("dtFim3", dtFim3) :
                new ObjectParameter("dtFim3", typeof(System.DateTime));
    
            var statementTypeParameter = statementType != null ?
                new ObjectParameter("StatementType", statementType) :
                new ObjectParameter("StatementType", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SpCrudSemestre", idParameter, dtIni1Parameter, dtIni2Parameter, dtIni3Parameter, dtFim1Parameter, dtFim2Parameter, dtFim3Parameter, statementTypeParameter);
        }
    
        public virtual int SpCrudTipoDisciplina(Nullable<int> id, string descricao, string statementType)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var descricaoParameter = descricao != null ?
                new ObjectParameter("descricao", descricao) :
                new ObjectParameter("descricao", typeof(string));
    
            var statementTypeParameter = statementType != null ?
                new ObjectParameter("StatementType", statementType) :
                new ObjectParameter("StatementType", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SpCrudTipoDisciplina", idParameter, descricaoParameter, statementTypeParameter);
        }
    
        public virtual int SpCrudTipoProva(Nullable<int> id, string descricao, string statementType)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var descricaoParameter = descricao != null ?
                new ObjectParameter("descricao", descricao) :
                new ObjectParameter("descricao", typeof(string));
    
            var statementTypeParameter = statementType != null ?
                new ObjectParameter("StatementType", statementType) :
                new ObjectParameter("StatementType", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SpCrudTipoProva", idParameter, descricaoParameter, statementTypeParameter);
        }
    
        public virtual int SpVerificaSemestreAtivo()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SpVerificaSemestreAtivo");
        }
    }
}
