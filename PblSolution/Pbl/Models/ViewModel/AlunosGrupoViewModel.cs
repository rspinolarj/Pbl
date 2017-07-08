using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pbl.Models.ViewModel
{
    public class AlunosGrupoViewModel
    {
        public List<InscricaoTurma> AlunosInscritos { get; set; }
        public List<InscricaoTurma> AlunosDisponiveis { get; set; }
        public Grupo grupo { get; set; }
    }
}