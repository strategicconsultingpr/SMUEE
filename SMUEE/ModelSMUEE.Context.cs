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
    
    public partial class SMUEEEntities : DbContext
    {
        public SMUEEEntities()
            : base("name=SMUEEEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<VW_CATEGORIA_REPORTE> VW_CATEGORIA_REPORTE { get; set; }
        public virtual DbSet<VW_REPORTES> VW_REPORTES { get; set; }
        public virtual DbSet<VW_UsersList> VW_UsersList { get; set; }
        public virtual DbSet<SM_HISTORIAL> SM_HISTORIAL { get; set; }
        public virtual DbSet<SM_MODULO> SM_MODULO { get; set; }
        public virtual DbSet<SM_LKP_ICONO_NOTIFICACIONES> SM_LKP_ICONO_NOTIFICACIONES { get; set; }
        public virtual DbSet<SM_NOTIFICACIONES> SM_NOTIFICACIONES { get; set; }
        public virtual DbSet<SM_NOTIFICACIONES_USUARIO> SM_NOTIFICACIONES_USUARIO { get; set; }
        public virtual DbSet<VW_NOTIFICACIONES_USUARIO> VW_NOTIFICACIONES_USUARIO { get; set; }
        public virtual DbSet<VW_NOTIFICACIONES> VW_NOTIFICACIONES { get; set; }
        public virtual DbSet<VW_HISTORIAL> VW_HISTORIAL { get; set; }
        public virtual DbSet<VW_DOCUMENTOS> VW_DOCUMENTOS { get; set; }
    
        public virtual int SPC_SESION(string fK_Usuario, ObjectParameter pK_Sesion)
        {
            var fK_UsuarioParameter = fK_Usuario != null ?
                new ObjectParameter("FK_Usuario", fK_Usuario) :
                new ObjectParameter("FK_Usuario", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SPC_SESION", fK_UsuarioParameter, pK_Sesion);
        }
    
        public virtual int SPD_SESION(string pK_Sesion)
        {
            var pK_SesionParameter = pK_Sesion != null ?
                new ObjectParameter("PK_Sesion", pK_Sesion) :
                new ObjectParameter("PK_Sesion", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SPD_SESION", pK_SesionParameter);
        }
    }
}
