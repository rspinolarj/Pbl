using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pbl.Models.ViewModel
{
    public class NotasProblemaViewModel
    {
        public int idAvaliacaoTutoria { get; set; }
        public string problema { get; set; }
        public decimal nota { get; set; }
        public bool emAberto { get; set; }
    }
}