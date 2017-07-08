using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pbl.Models.ViewModel
{
    public class AlunosTurmaViewModel
    {
        public Turma turmaAtual { get; set; }
        public List<Aluno> AlunosDisponiveis { get; set; }
        public List<Aluno> AlunosCadastrados { get; set; }
    }
}