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
    
    public partial class Prova
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Prova()
        {
            this.ControleNotasXProva = new HashSet<ControleNotasXProva>();
        }
    
        public int idProva { get; set; }
        public Nullable<int> idTipoProva { get; set; }
        public Nullable<decimal> valorQuestao { get; set; }
        public int idMed { get; set; }
        public int idModulo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ControleNotasXProva> ControleNotasXProva { get; set; }
        public virtual TipoProva TipoProva { get; set; }
        public virtual Med Med { get; set; }
        public virtual Modulo Modulo { get; set; }
    }
}
