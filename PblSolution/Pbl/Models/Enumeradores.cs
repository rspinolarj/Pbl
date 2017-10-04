using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pbl.Models
{
    public class Enumeradores
    {

        public enum TipoUsuario
        {
            Diretor = 1,
            Professor = 2,
            Aluno = 3
        }

        public enum TipoProva
        {
            Morfofuncional = 1,
            Tutoria = 2
        }

        public enum TipoDisciplina
        {
            Morfofuncional = 1,
            Pratica = 2
        }

    }
}