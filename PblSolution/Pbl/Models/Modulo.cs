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
    
    public partial class Modulo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Modulo()
        {
            this.ControleNotas = new HashSet<ControleNotas>();
            this.Prova = new HashSet<Prova>();
        }
    
        public int idModulo { get; set; }
        public string descModulo { get; set; }
        public Nullable<System.DateTime> dtInicio { get; set; }
        public Nullable<System.DateTime> dtFim { get; set; }
        public int idSemestre { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ControleNotas> ControleNotas { get; set; }
        public virtual Semestre Semestre { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prova> Prova { get; set; }
    }
}
