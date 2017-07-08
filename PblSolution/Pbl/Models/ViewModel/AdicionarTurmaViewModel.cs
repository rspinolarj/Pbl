using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pbl.Models.ViewModel
{
    public class AdicionarTurmaViewModel
    {
        public Turma turma { get; set; }
        public List<Aula> aulasMinistradas { get; set; }
    }
}