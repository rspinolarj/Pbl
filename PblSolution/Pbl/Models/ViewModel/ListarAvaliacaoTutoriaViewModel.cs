using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pbl.Models.ViewModel
{
    public class ListarAvaliacaoTutoriaViewModel
    {
        public List<AvaliacaoTutoria> alunosAvaliados { get; set; }
        public List<AvaliacaoTutoria> alunosNaoAvaliados { get; set; }
    }
}