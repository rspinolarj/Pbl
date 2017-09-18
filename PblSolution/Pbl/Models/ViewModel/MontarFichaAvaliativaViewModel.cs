using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pbl.Models.ViewModel
{
    public class MontarFichaAvaliativaViewModel
    {
        public AvaliacaoTutoria avaliacaoTutoria { get; set; }
        public List<Modulo> modulos { get; set; }
    }
}