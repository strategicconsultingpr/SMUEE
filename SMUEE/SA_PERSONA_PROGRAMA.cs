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
    
    public partial class SA_PERSONA_PROGRAMA
    {
        public int FK_Persona { get; set; }
        public short FK_Programa { get; set; }
        public string NR_Expediente { get; set; }
        public System.Guid FK_Sesion { get; set; }
        public string TI_Edicion { get; set; }
        public System.DateTime FE_Edicion { get; set; }
        public Nullable<int> PK_Old { get; set; }
    
        public virtual SA_PERSONA SA_PERSONA { get; set; }
    }
}
