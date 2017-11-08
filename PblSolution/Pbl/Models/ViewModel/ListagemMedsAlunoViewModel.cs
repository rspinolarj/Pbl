using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pbl.Models.ViewModel
{
    public class ListagemMedsAlunoViewModel
    {
        public int idMed { get; set; }
        public string descMed { get; set; }
        public string descSemestre { get; set; }
        public double[] notas { get; set; }
        public int[] idControleNotas { get; set; }
    }
}