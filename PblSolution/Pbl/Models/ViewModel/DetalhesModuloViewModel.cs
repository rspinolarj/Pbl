using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pbl.Models.ViewModel
{
    public class DetalhesModuloViewModel
    {
        public decimal nota { get; set; }
        public List<NotasProblemaViewModel> avaliacoesTutoria { get; set; }
        public List<ControleNotasXAula> avaliacoesAula { get; set; }
        public List<ControleNotasXProva> avaliacoesProva { get; set; }
    }
}