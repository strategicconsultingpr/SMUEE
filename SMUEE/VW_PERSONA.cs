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
    
    public partial class VW_PERSONA
    {
        public int PK_Persona { get; set; }
        public string NR_SeguroSocial { get; set; }
        public byte FK_Sexo { get; set; }
        public string DE_Sexo { get; set; }
        public string AP_Primero { get; set; }
        public string AP_Segundo { get; set; }
        public string NB_Primero { get; set; }
        public string NB_Segundo { get; set; }
        public System.DateTime FE_Nacimiento { get; set; }
        public Nullable<int> NR_Edad { get; set; }
        public byte FK_Veterano { get; set; }
        public string DE_Veterano { get; set; }
        public byte FK_GrupoEtnico { get; set; }
        public string DE_GrupoEtnico { get; set; }
        public System.Guid FK_Sesion { get; set; }
        public string TI_Edicion { get; set; }
        public System.DateTime FE_Edicion { get; set; }
    }
}
