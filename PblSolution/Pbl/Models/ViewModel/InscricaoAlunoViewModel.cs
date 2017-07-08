using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pbl.Models.ViewModel
{
    public class InscricaoAlunoViewModel
    {
        public List<Aluno> alunosDisponiveis { get; set; }
        public List<Aluno> alunosCadastrados { get; set; }
    }
}