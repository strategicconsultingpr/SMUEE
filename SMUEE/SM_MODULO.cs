//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SMUEE
{
    using System;
    using System.Collections.Generic;
    
    public partial class SM_MODULO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SM_MODULO()
        {
            this.SM_HISTORIAL = new HashSet<SM_HISTORIAL>();
        }
    
        public string PK_Modulo { get; set; }
        public string DE_Modulo { get; set; }
        public Nullable<bool> Activo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SM_HISTORIAL> SM_HISTORIAL { get; set; }
    }
}
