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
    
    public partial class Aula
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Aula()
        {
            this.ControleNotasXAula = new HashSet<ControleNotasXAula>();
        }
    
        public int idAula { get; set; }
        public int idDisciplina { get; set; }
        public Nullable<int> idTurma { get; set; }
        public int idProfessor { get; set; }
        public bool ativo { get; set; }
    
        public virtual Disciplina Disciplina { get; set; }
        public virtual Professor Professor { get; set; }
        public virtual Turma Turma { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ControleNotasXAula> ControleNotasXAula { get; set; }
    }
}
