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
    
    public partial class Med
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Med()
        {
            this.Grupo = new HashSet<Grupo>();
            this.ProblemaXMed = new HashSet<ProblemaXMed>();
            this.Prova = new HashSet<Prova>();
            this.Turma = new HashSet<Turma>();
            this.Disciplina = new HashSet<Disciplina>();
        }
    
        public int idMed { get; set; }
        public string descMed { get; set; }
        public bool ativo { get; set; }
        public Nullable<int> idSemestre { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Grupo> Grupo { get; set; }
        public virtual Semestre Semestre { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProblemaXMed> ProblemaXMed { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prova> Prova { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Turma> Turma { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Disciplina> Disciplina { get; set; }
    }
}
