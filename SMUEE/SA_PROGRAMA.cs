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
    
    public partial class SA_PROGRAMA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SA_PROGRAMA()
        {
            this.SA_EPISODIO = new HashSet<SA_EPISODIO>();
            this.SA_PERSONA_PROGRAMA = new HashSet<SA_PERSONA_PROGRAMA>();
        }
    
        public short PK_Programa { get; set; }
        public byte FK_Administracion { get; set; }
        public Nullable<short> CO_Tipo { get; set; }
        public string CO_Programa { get; set; }
        public string CO_Prov_ID { get; set; }
        public string NB_Programa { get; set; }
        public string DE_Programa { get; set; }
        public string NB_ProgramaIngles { get; set; }
        public string CO_Hacienda { get; set; }
        public bool IN_Inactivo { get; set; }
        public bool IN_Creado_Fed { get; set; }
        public bool IN_TEDS { get; set; }
        public Nullable<bool> REP_TEDS { get; set; }
        public Nullable<bool> PREOW { get; set; }
        public Nullable<bool> IN_SUST { get; set; }
        public Nullable<bool> IN_INF_ANUAL { get; set; }
        public Nullable<bool> REP_TEDS_MH { get; set; }
        public Nullable<bool> IN_SELECCIONADO_REP { get; set; }
        public Nullable<bool> IN_CONVENIO { get; set; }
        public Nullable<bool> REP_URS_MH { get; set; }
        public Nullable<int> FK_CATEGORIA_PROGRAMA { get; set; }
        public Nullable<bool> CERRAR_EPISODIO_ADMISION { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SA_EPISODIO> SA_EPISODIO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SA_PERSONA_PROGRAMA> SA_PERSONA_PROGRAMA { get; set; }
        public virtual SA_PROGRAMA SA_PROGRAMA1 { get; set; }
        public virtual SA_PROGRAMA SA_PROGRAMA2 { get; set; }
    }
}