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
    
    public partial class ProblemaXMed
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProblemaXMed()
        {
            this.AvaliacaoTutoria = new HashSet<AvaliacaoTutoria>();
        }
    
        public int idProblemaxMed { get; set; }
        public int idProblema { get; set; }
        public int idMed { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AvaliacaoTutoria> AvaliacaoTutoria { get; set; }
        public virtual Med Med { get; set; }
        public virtual Problema Problema { get; set; }
    }
}
