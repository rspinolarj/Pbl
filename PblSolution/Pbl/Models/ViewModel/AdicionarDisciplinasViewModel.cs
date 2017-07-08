using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pbl.Models.ViewModel
{
    public class AdicionarDisciplinasViewModel
    {
        public List<Disciplina> disciplinasCadastradas { get; set; }
        public List<Disciplina> disciplinasDisponiveis { get; set; }
        public Med med { get; set; }
    }
}