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
    
        public virtual DbSet<VW_UsersList> VW_UsersList { get; set; }
        public virtual DbSet<VW_CATEGORIA_REPORTE> VW_CATEGORIA_REPORTE { get; set; }
        public virtual DbSet<VW_REPORTES> VW_REPORTES { get; set; }
    }
}
