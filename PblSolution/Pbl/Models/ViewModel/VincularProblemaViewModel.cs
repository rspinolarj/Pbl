using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pbl.Models.ViewModel
{
    public class VincularProblemaViewModel
    {
        public List<Problema> ListProblemaDisponiveis { get; set; }
        public List<ProblemaXMed> ListProblemasCadastrados { get; set; }
        public Med MedAtual { get; set; }
    }
}