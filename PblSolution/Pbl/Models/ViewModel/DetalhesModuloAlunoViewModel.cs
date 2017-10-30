using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pbl.Models.ViewModel
{
    public class DetalhesModuloAlunoViewModel
    {
        public string descModulo { get; set; }
        public double notaSimuladoMorfofuncional { get; set; }
        public double notaSimuladoTutoria { get; set; }
        public List<DetalhesProblemaAlunoViewModel> problemas { get; set; }
        public List<DetalhesDisciplinaAlunoViewModel> disciplinas { get; set; }
    }
}