﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class SEPSEntities : DbContext
    {
        public SEPSEntities()
            : base("name=SEPSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<VW_PERFIL> VW_PERFIL { get; set; }
        public virtual DbSet<VW_PERSONA> VW_PERSONA { get; set; }
        public virtual DbSet<VW_PERSONAS> VW_PERSONAS { get; set; }
        public virtual DbSet<VW_PROGRAMAS> VW_PROGRAMAS { get; set; }
        public virtual DbSet<VW_EPISODIO> VW_EPISODIO { get; set; }
        public virtual DbSet<SA_EPISODIO> SA_EPISODIO { get; set; }
        public virtual DbSet<SA_PERFIL> SA_PERFIL { get; set; }
        public virtual DbSet<SA_PERSONA> SA_PERSONA { get; set; }
        public virtual DbSet<SA_PERSONA_PROGRAMA> SA_PERSONA_PROGRAMA { get; set; }
    
        public virtual ObjectResult<SPC_SESION_Result> SPC_SESION(string nB_Login, string pASSWORD, ObjectParameter pK_Sesion)
        {
            var nB_LoginParameter = nB_Login != null ?
                new ObjectParameter("NB_Login", nB_Login) :
                new ObjectParameter("NB_Login", typeof(string));
    
            var pASSWORDParameter = pASSWORD != null ?
                new ObjectParameter("PASSWORD", pASSWORD) :
                new ObjectParameter("PASSWORD", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPC_SESION_Result>("SPC_SESION", nB_LoginParameter, pASSWORDParameter, pK_Sesion);
        }
    }
}
