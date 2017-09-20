//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Pbl.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AvaliacaoTutoria
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AvaliacaoTutoria()
        {
            this.FichaAvaliacao = new HashSet<FichaAvaliacao>();
        }
    
        public int idAvaliacaoTutoria { get; set; }
        public Nullable<int> idProblemaxMed { get; set; }
        public int idGrupo { get; set; }
        public Nullable<decimal> notaProfessor { get; set; }
        public Nullable<int> idControleNotas { get; set; }
        public System.DateTime dtInicio { get; set; }
        public System.DateTime dtFim { get; set; }
    
        public virtual Grupo Grupo { get; set; }
        public virtual ControleNotas ControleNotas { get; set; }
        public virtual ProblemaXMed ProblemaXMed { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FichaAvaliacao> FichaAvaliacao { get; set; }
    }
}
