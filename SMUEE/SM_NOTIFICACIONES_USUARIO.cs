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
    
    public partial class SM_NOTIFICACIONES_USUARIO
    {
        public string FK_USUARIO { get; set; }
        public int FK_NOTIFICACIONES { get; set; }
        public Nullable<bool> VISTO { get; set; }
    
        public virtual SM_NOTIFICACIONES SM_NOTIFICACIONES { get; set; }
    }
}