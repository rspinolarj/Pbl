using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pbl.Models.ViewModel
{
    public class GerenciarNotasViewModel
    {
        public List<ControleNotasViewModel> controleNotas { get; set; }
        public InscricaoTurma aluno { get; set; }
    }
}