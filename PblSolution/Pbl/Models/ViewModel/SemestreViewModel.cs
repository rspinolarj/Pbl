using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pbl.Models.ViewModel
{
    public class SemestreViewModel
    {
        public int idSemestre { get; set; }
        public DateTime Ano { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInicioModulo1 { get; set; }
        public DateTime DataInicioModulo2 { get; set; }
        public DateTime DataInicioModulo3 { get; set; }
        public DateTime DataFinalModulo1 { get; set; }
        public DateTime DataFinalModulo2 { get; set; }
        public DateTime DataFinalModulo3 { get; set; }

    }
}