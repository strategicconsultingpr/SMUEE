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
        public virtual DbSet<SA_EPISODIO> SA_EPISODIO { get; set; }
        public virtual DbSet<SA_PERSONA> SA_PERSONA { get; set; }
        public virtual DbSet<SA_PERSONA_PROGRAMA> SA_PERSONA_PROGRAMA { get; set; }
        public virtual DbSet<VW_EXPEDIENTE_PROGRAMA> VW_EXPEDIENTE_PROGRAMA { get; set; }
        public virtual DbSet<SA_PROGRAMA> SA_PROGRAMA { get; set; }
        public virtual DbSet<VW_REF_ABUSO_SUSTANCIA> VW_REF_ABUSO_SUSTANCIA { get; set; }
        public virtual DbSet<VW_REF_SALUD_MENTAL> VW_REF_SALUD_MENTAL { get; set; }
        public virtual DbSet<VW_EPISODIOS_EXPEDIENTE> VW_EPISODIOS_EXPEDIENTE { get; set; }
        public virtual DbSet<VW_SAEP> VW_SAEP { get; set; }
        public virtual DbSet<SA_LKP_ALTA> SA_LKP_ALTA { get; set; }
        public virtual DbSet<TMP_SC_VW_RPT_TEDS_MH_AD> TMP_SC_VW_RPT_TEDS_MH_AD { get; set; }
        public virtual DbSet<TMP_SC_VW_RPT_TEDS_MH_DIS> TMP_SC_VW_RPT_TEDS_MH_DIS { get; set; }
        public virtual DbSet<TMP_SC_VW_RPT_TEDS_SA_AD> TMP_SC_VW_RPT_TEDS_SA_AD { get; set; }
        public virtual DbSet<TMP_SC_VW_RPT_TEDS_SA_DIS> TMP_SC_VW_RPT_TEDS_SA_DIS { get; set; }
        public virtual DbSet<SA_PERFIL> SA_PERFIL { get; set; }
        public virtual DbSet<SA_PERFIL_ELIMINADO> SA_PERFIL_ELIMINADO { get; set; }
    
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
    
        public virtual ObjectResult<SP_GET_EPISODIOS_CERRADOS_SIN_ALTAS_Result> SP_GET_EPISODIOS_CERRADOS_SIN_ALTAS()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GET_EPISODIOS_CERRADOS_SIN_ALTAS_Result>("SP_GET_EPISODIOS_CERRADOS_SIN_ALTAS");
        }
    
        public virtual int SPD_SESION(Nullable<System.Guid> pK_Sesion)
        {
            var pK_SesionParameter = pK_Sesion.HasValue ?
                new ObjectParameter("PK_Sesion", pK_Sesion) :
                new ObjectParameter("PK_Sesion", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SPD_SESION", pK_SesionParameter);
        }
    
        public virtual ObjectResult<SPR_PERFIL_Result> SPR_PERFIL(Nullable<int> pK_Perfil)
        {
            var pK_PerfilParameter = pK_Perfil.HasValue ?
                new ObjectParameter("PK_Perfil", pK_Perfil) :
                new ObjectParameter("PK_Perfil", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPR_PERFIL_Result>("SPR_PERFIL", pK_PerfilParameter);
        }
    
        public virtual int SPC_PERFIL(Nullable<int> fK_Episodio, Nullable<int> nR_Expediente, Nullable<System.DateTime> fE_Perfil, Nullable<System.DateTime> fE_Contacto, string iN_TI_Perfil, Nullable<byte> fK_EstadoMarital, Nullable<byte> fK_CondicionLaboral, Nullable<byte> fK_ActividadNoLaboral, Nullable<byte> nR_Hijos, Nullable<byte> fK_Escolaridad, Nullable<byte> iN_EducEspecial, Nullable<byte> iN_DesertorEscolar, Nullable<byte> fK_Familia, Nullable<byte> nR_Familiar, Nullable<byte> fK_Residencia, Nullable<byte> iN_ParticReunGrupos, Nullable<byte> fK_FreqAutoAyuda, Nullable<byte> iN_Arrestado30dias, Nullable<byte> nR_Arrestos30dias, Nullable<short> fK_DSMV_TrastornosClinicos1, Nullable<short> fK_DSMV_TrastornosClinicos2, Nullable<short> fK_DSMV_TrastornosClinicos3, Nullable<short> fK_DSMV_TrastornosPersonalidadRM1, Nullable<short> fK_DSMV_TrastornosPersonalidadRM2, Nullable<short> fK_DSMV_TrastornosPersonalidadRM3, Nullable<byte> fK_DSMV_ProblemasPsicosocialesAmbientales1, Nullable<byte> fK_DSMV_ProblemasPsicosocialesAmbientales2, Nullable<byte> fK_DSMV_ProblemasPsicosocialesAmbientales3, string nR_DSMV_FuncionamientoGlobal, string dE_DSMV_OtrasObservaciones, string dE_DSMV_Comentarios, Nullable<short> iN_DSMV_DiagnosticoDual, Nullable<byte> fK_DisposicionFinalReferido, Nullable<byte> fK_DrogaPrimario, Nullable<byte> fK_ViaPrimario, Nullable<byte> fK_FrecuenciaPrimario, Nullable<byte> iN_EdadInicioPrimario, Nullable<byte> fK_DrogaSecundario, Nullable<byte> fK_ViaSecundario, Nullable<byte> fK_FrecuenciaSecundario, Nullable<byte> iN_EdadInicioSecundario, Nullable<byte> fK_DrogaTerciario, Nullable<byte> fK_ViaTerciario, Nullable<byte> fK_FrecuenciaTerciario, Nullable<byte> iN_EdadInicioTerciario, Nullable<decimal> nR_PromedioVisitas, Nullable<byte> fK_Alta, Nullable<short> fK_CentroTraslado, string dE_Comentario, Nullable<System.Guid> fK_Sesion, Nullable<int> fK_SituacionEscolar, Nullable<int> fK_TipoAdmision, Nullable<int> fK_CategoriaCentroPrivado, string nR_CelularPrimario, string nR_CelularContacto, string dE_EmailPrimario, string dE_EmailSecundario, Nullable<byte> fK_CatRecuperacionRes, string hogarRecuperacionRes, Nullable<int> fK_DSMV_Sustancias1, Nullable<int> fK_DSMV_Sustancias2, Nullable<int> fK_DSMV_Sustancias3, Nullable<byte> iN_Fumado, string dE_FrecuenciaFumado, Nullable<int> nR_CigarrosXDias, string dE_DrogaNueva1, string dE_DrogaNueva2, string dE_DrogaNueva3, Nullable<byte> iN_Toxicologia1, Nullable<byte> iN_Toxicologia2, Nullable<byte> iN_Toxicologia3, Nullable<int> fK_IDENTIDAD_GENERO, ObjectParameter pK_Perfil)
        {
            var fK_EpisodioParameter = fK_Episodio.HasValue ?
                new ObjectParameter("FK_Episodio", fK_Episodio) :
                new ObjectParameter("FK_Episodio", typeof(int));
    
            var nR_ExpedienteParameter = nR_Expediente.HasValue ?
                new ObjectParameter("NR_Expediente", nR_Expediente) :
                new ObjectParameter("NR_Expediente", typeof(int));
    
            var fE_PerfilParameter = fE_Perfil.HasValue ?
                new ObjectParameter("FE_Perfil", fE_Perfil) :
                new ObjectParameter("FE_Perfil", typeof(System.DateTime));
    
            var fE_ContactoParameter = fE_Contacto.HasValue ?
                new ObjectParameter("FE_Contacto", fE_Contacto) :
                new ObjectParameter("FE_Contacto", typeof(System.DateTime));
    
            var iN_TI_PerfilParameter = iN_TI_Perfil != null ?
                new ObjectParameter("IN_TI_Perfil", iN_TI_Perfil) :
                new ObjectParameter("IN_TI_Perfil", typeof(string));
    
            var fK_EstadoMaritalParameter = fK_EstadoMarital.HasValue ?
                new ObjectParameter("FK_EstadoMarital", fK_EstadoMarital) :
                new ObjectParameter("FK_EstadoMarital", typeof(byte));
    
            var fK_CondicionLaboralParameter = fK_CondicionLaboral.HasValue ?
                new ObjectParameter("FK_CondicionLaboral", fK_CondicionLaboral) :
                new ObjectParameter("FK_CondicionLaboral", typeof(byte));
    
            var fK_ActividadNoLaboralParameter = fK_ActividadNoLaboral.HasValue ?
                new ObjectParameter("FK_ActividadNoLaboral", fK_ActividadNoLaboral) :
                new ObjectParameter("FK_ActividadNoLaboral", typeof(byte));
    
            var nR_HijosParameter = nR_Hijos.HasValue ?
                new ObjectParameter("NR_Hijos", nR_Hijos) :
                new ObjectParameter("NR_Hijos", typeof(byte));
    
            var fK_EscolaridadParameter = fK_Escolaridad.HasValue ?
                new ObjectParameter("FK_Escolaridad", fK_Escolaridad) :
                new ObjectParameter("FK_Escolaridad", typeof(byte));
    
            var iN_EducEspecialParameter = iN_EducEspecial.HasValue ?
                new ObjectParameter("IN_EducEspecial", iN_EducEspecial) :
                new ObjectParameter("IN_EducEspecial", typeof(byte));
    
            var iN_DesertorEscolarParameter = iN_DesertorEscolar.HasValue ?
                new ObjectParameter("IN_DesertorEscolar", iN_DesertorEscolar) :
                new ObjectParameter("IN_DesertorEscolar", typeof(byte));
    
            var fK_FamiliaParameter = fK_Familia.HasValue ?
                new ObjectParameter("FK_Familia", fK_Familia) :
                new ObjectParameter("FK_Familia", typeof(byte));
    
            var nR_FamiliarParameter = nR_Familiar.HasValue ?
                new ObjectParameter("NR_Familiar", nR_Familiar) :
                new ObjectParameter("NR_Familiar", typeof(byte));
    
            var fK_ResidenciaParameter = fK_Residencia.HasValue ?
                new ObjectParameter("FK_Residencia", fK_Residencia) :
                new ObjectParameter("FK_Residencia", typeof(byte));
    
            var iN_ParticReunGruposParameter = iN_ParticReunGrupos.HasValue ?
                new ObjectParameter("IN_ParticReunGrupos", iN_ParticReunGrupos) :
                new ObjectParameter("IN_ParticReunGrupos", typeof(byte));
    
            var fK_FreqAutoAyudaParameter = fK_FreqAutoAyuda.HasValue ?
                new ObjectParameter("FK_FreqAutoAyuda", fK_FreqAutoAyuda) :
                new ObjectParameter("FK_FreqAutoAyuda", typeof(byte));
    
            var iN_Arrestado30diasParameter = iN_Arrestado30dias.HasValue ?
                new ObjectParameter("IN_Arrestado30dias", iN_Arrestado30dias) :
                new ObjectParameter("IN_Arrestado30dias", typeof(byte));
    
            var nR_Arrestos30diasParameter = nR_Arrestos30dias.HasValue ?
                new ObjectParameter("NR_Arrestos30dias", nR_Arrestos30dias) :
                new ObjectParameter("NR_Arrestos30dias", typeof(byte));
    
            var fK_DSMV_TrastornosClinicos1Parameter = fK_DSMV_TrastornosClinicos1.HasValue ?
                new ObjectParameter("FK_DSMV_TrastornosClinicos1", fK_DSMV_TrastornosClinicos1) :
                new ObjectParameter("FK_DSMV_TrastornosClinicos1", typeof(short));
    
            var fK_DSMV_TrastornosClinicos2Parameter = fK_DSMV_TrastornosClinicos2.HasValue ?
                new ObjectParameter("FK_DSMV_TrastornosClinicos2", fK_DSMV_TrastornosClinicos2) :
                new ObjectParameter("FK_DSMV_TrastornosClinicos2", typeof(short));
    
            var fK_DSMV_TrastornosClinicos3Parameter = fK_DSMV_TrastornosClinicos3.HasValue ?
                new ObjectParameter("FK_DSMV_TrastornosClinicos3", fK_DSMV_TrastornosClinicos3) :
                new ObjectParameter("FK_DSMV_TrastornosClinicos3", typeof(short));
    
            var fK_DSMV_TrastornosPersonalidadRM1Parameter = fK_DSMV_TrastornosPersonalidadRM1.HasValue ?
                new ObjectParameter("FK_DSMV_TrastornosPersonalidadRM1", fK_DSMV_TrastornosPersonalidadRM1) :
                new ObjectParameter("FK_DSMV_TrastornosPersonalidadRM1", typeof(short));
    
            var fK_DSMV_TrastornosPersonalidadRM2Parameter = fK_DSMV_TrastornosPersonalidadRM2.HasValue ?
                new ObjectParameter("FK_DSMV_TrastornosPersonalidadRM2", fK_DSMV_TrastornosPersonalidadRM2) :
                new ObjectParameter("FK_DSMV_TrastornosPersonalidadRM2", typeof(short));
    
            var fK_DSMV_TrastornosPersonalidadRM3Parameter = fK_DSMV_TrastornosPersonalidadRM3.HasValue ?
                new ObjectParameter("FK_DSMV_TrastornosPersonalidadRM3", fK_DSMV_TrastornosPersonalidadRM3) :
                new ObjectParameter("FK_DSMV_TrastornosPersonalidadRM3", typeof(short));
    
            var fK_DSMV_ProblemasPsicosocialesAmbientales1Parameter = fK_DSMV_ProblemasPsicosocialesAmbientales1.HasValue ?
                new ObjectParameter("FK_DSMV_ProblemasPsicosocialesAmbientales1", fK_DSMV_ProblemasPsicosocialesAmbientales1) :
                new ObjectParameter("FK_DSMV_ProblemasPsicosocialesAmbientales1", typeof(byte));
    
            var fK_DSMV_ProblemasPsicosocialesAmbientales2Parameter = fK_DSMV_ProblemasPsicosocialesAmbientales2.HasValue ?
                new ObjectParameter("FK_DSMV_ProblemasPsicosocialesAmbientales2", fK_DSMV_ProblemasPsicosocialesAmbientales2) :
                new ObjectParameter("FK_DSMV_ProblemasPsicosocialesAmbientales2", typeof(byte));
    
            var fK_DSMV_ProblemasPsicosocialesAmbientales3Parameter = fK_DSMV_ProblemasPsicosocialesAmbientales3.HasValue ?
                new ObjectParameter("FK_DSMV_ProblemasPsicosocialesAmbientales3", fK_DSMV_ProblemasPsicosocialesAmbientales3) :
                new ObjectParameter("FK_DSMV_ProblemasPsicosocialesAmbientales3", typeof(byte));
    
            var nR_DSMV_FuncionamientoGlobalParameter = nR_DSMV_FuncionamientoGlobal != null ?
                new ObjectParameter("NR_DSMV_FuncionamientoGlobal", nR_DSMV_FuncionamientoGlobal) :
                new ObjectParameter("NR_DSMV_FuncionamientoGlobal", typeof(string));
    
            var dE_DSMV_OtrasObservacionesParameter = dE_DSMV_OtrasObservaciones != null ?
                new ObjectParameter("DE_DSMV_OtrasObservaciones", dE_DSMV_OtrasObservaciones) :
                new ObjectParameter("DE_DSMV_OtrasObservaciones", typeof(string));
    
            var dE_DSMV_ComentariosParameter = dE_DSMV_Comentarios != null ?
                new ObjectParameter("DE_DSMV_Comentarios", dE_DSMV_Comentarios) :
                new ObjectParameter("DE_DSMV_Comentarios", typeof(string));
    
            var iN_DSMV_DiagnosticoDualParameter = iN_DSMV_DiagnosticoDual.HasValue ?
                new ObjectParameter("IN_DSMV_DiagnosticoDual", iN_DSMV_DiagnosticoDual) :
                new ObjectParameter("IN_DSMV_DiagnosticoDual", typeof(short));
    
            var fK_DisposicionFinalReferidoParameter = fK_DisposicionFinalReferido.HasValue ?
                new ObjectParameter("FK_DisposicionFinalReferido", fK_DisposicionFinalReferido) :
                new ObjectParameter("FK_DisposicionFinalReferido", typeof(byte));
    
            var fK_DrogaPrimarioParameter = fK_DrogaPrimario.HasValue ?
                new ObjectParameter("FK_DrogaPrimario", fK_DrogaPrimario) :
                new ObjectParameter("FK_DrogaPrimario", typeof(byte));
    
            var fK_ViaPrimarioParameter = fK_ViaPrimario.HasValue ?
                new ObjectParameter("FK_ViaPrimario", fK_ViaPrimario) :
                new ObjectParameter("FK_ViaPrimario", typeof(byte));
    
            var fK_FrecuenciaPrimarioParameter = fK_FrecuenciaPrimario.HasValue ?
                new ObjectParameter("FK_FrecuenciaPrimario", fK_FrecuenciaPrimario) :
                new ObjectParameter("FK_FrecuenciaPrimario", typeof(byte));
    
            var iN_EdadInicioPrimarioParameter = iN_EdadInicioPrimario.HasValue ?
                new ObjectParameter("IN_EdadInicioPrimario", iN_EdadInicioPrimario) :
                new ObjectParameter("IN_EdadInicioPrimario", typeof(byte));
    
            var fK_DrogaSecundarioParameter = fK_DrogaSecundario.HasValue ?
                new ObjectParameter("FK_DrogaSecundario", fK_DrogaSecundario) :
                new ObjectParameter("FK_DrogaSecundario", typeof(byte));
    
            var fK_ViaSecundarioParameter = fK_ViaSecundario.HasValue ?
                new ObjectParameter("FK_ViaSecundario", fK_ViaSecundario) :
                new ObjectParameter("FK_ViaSecundario", typeof(byte));
    
            var fK_FrecuenciaSecundarioParameter = fK_FrecuenciaSecundario.HasValue ?
                new ObjectParameter("FK_FrecuenciaSecundario", fK_FrecuenciaSecundario) :
                new ObjectParameter("FK_FrecuenciaSecundario", typeof(byte));
    
            var iN_EdadInicioSecundarioParameter = iN_EdadInicioSecundario.HasValue ?
                new ObjectParameter("IN_EdadInicioSecundario", iN_EdadInicioSecundario) :
                new ObjectParameter("IN_EdadInicioSecundario", typeof(byte));
    
            var fK_DrogaTerciarioParameter = fK_DrogaTerciario.HasValue ?
                new ObjectParameter("FK_DrogaTerciario", fK_DrogaTerciario) :
                new ObjectParameter("FK_DrogaTerciario", typeof(byte));
    
            var fK_ViaTerciarioParameter = fK_ViaTerciario.HasValue ?
                new ObjectParameter("FK_ViaTerciario", fK_ViaTerciario) :
                new ObjectParameter("FK_ViaTerciario", typeof(byte));
    
            var fK_FrecuenciaTerciarioParameter = fK_FrecuenciaTerciario.HasValue ?
                new ObjectParameter("FK_FrecuenciaTerciario", fK_FrecuenciaTerciario) :
                new ObjectParameter("FK_FrecuenciaTerciario", typeof(byte));
    
            var iN_EdadInicioTerciarioParameter = iN_EdadInicioTerciario.HasValue ?
                new ObjectParameter("IN_EdadInicioTerciario", iN_EdadInicioTerciario) :
                new ObjectParameter("IN_EdadInicioTerciario", typeof(byte));
    
            var nR_PromedioVisitasParameter = nR_PromedioVisitas.HasValue ?
                new ObjectParameter("NR_PromedioVisitas", nR_PromedioVisitas) :
                new ObjectParameter("NR_PromedioVisitas", typeof(decimal));
    
            var fK_AltaParameter = fK_Alta.HasValue ?
                new ObjectParameter("FK_Alta", fK_Alta) :
                new ObjectParameter("FK_Alta", typeof(byte));
    
            var fK_CentroTrasladoParameter = fK_CentroTraslado.HasValue ?
                new ObjectParameter("FK_CentroTraslado", fK_CentroTraslado) :
                new ObjectParameter("FK_CentroTraslado", typeof(short));
    
            var dE_ComentarioParameter = dE_Comentario != null ?
                new ObjectParameter("DE_Comentario", dE_Comentario) :
                new ObjectParameter("DE_Comentario", typeof(string));
    
            var fK_SesionParameter = fK_Sesion.HasValue ?
                new ObjectParameter("FK_Sesion", fK_Sesion) :
                new ObjectParameter("FK_Sesion", typeof(System.Guid));
    
            var fK_SituacionEscolarParameter = fK_SituacionEscolar.HasValue ?
                new ObjectParameter("FK_SituacionEscolar", fK_SituacionEscolar) :
                new ObjectParameter("FK_SituacionEscolar", typeof(int));
    
            var fK_TipoAdmisionParameter = fK_TipoAdmision.HasValue ?
                new ObjectParameter("FK_TipoAdmision", fK_TipoAdmision) :
                new ObjectParameter("FK_TipoAdmision", typeof(int));
    
            var fK_CategoriaCentroPrivadoParameter = fK_CategoriaCentroPrivado.HasValue ?
                new ObjectParameter("FK_CategoriaCentroPrivado", fK_CategoriaCentroPrivado) :
                new ObjectParameter("FK_CategoriaCentroPrivado", typeof(int));
    
            var nR_CelularPrimarioParameter = nR_CelularPrimario != null ?
                new ObjectParameter("NR_CelularPrimario", nR_CelularPrimario) :
                new ObjectParameter("NR_CelularPrimario", typeof(string));
    
            var nR_CelularContactoParameter = nR_CelularContacto != null ?
                new ObjectParameter("NR_CelularContacto", nR_CelularContacto) :
                new ObjectParameter("NR_CelularContacto", typeof(string));
    
            var dE_EmailPrimarioParameter = dE_EmailPrimario != null ?
                new ObjectParameter("DE_EmailPrimario", dE_EmailPrimario) :
                new ObjectParameter("DE_EmailPrimario", typeof(string));
    
            var dE_EmailSecundarioParameter = dE_EmailSecundario != null ?
                new ObjectParameter("DE_EmailSecundario", dE_EmailSecundario) :
                new ObjectParameter("DE_EmailSecundario", typeof(string));
    
            var fK_CatRecuperacionResParameter = fK_CatRecuperacionRes.HasValue ?
                new ObjectParameter("FK_CatRecuperacionRes", fK_CatRecuperacionRes) :
                new ObjectParameter("FK_CatRecuperacionRes", typeof(byte));
    
            var hogarRecuperacionResParameter = hogarRecuperacionRes != null ?
                new ObjectParameter("HogarRecuperacionRes", hogarRecuperacionRes) :
                new ObjectParameter("HogarRecuperacionRes", typeof(string));
    
            var fK_DSMV_Sustancias1Parameter = fK_DSMV_Sustancias1.HasValue ?
                new ObjectParameter("FK_DSMV_Sustancias1", fK_DSMV_Sustancias1) :
                new ObjectParameter("FK_DSMV_Sustancias1", typeof(int));
    
            var fK_DSMV_Sustancias2Parameter = fK_DSMV_Sustancias2.HasValue ?
                new ObjectParameter("FK_DSMV_Sustancias2", fK_DSMV_Sustancias2) :
                new ObjectParameter("FK_DSMV_Sustancias2", typeof(int));
    
            var fK_DSMV_Sustancias3Parameter = fK_DSMV_Sustancias3.HasValue ?
                new ObjectParameter("FK_DSMV_Sustancias3", fK_DSMV_Sustancias3) :
                new ObjectParameter("FK_DSMV_Sustancias3", typeof(int));
    
            var iN_FumadoParameter = iN_Fumado.HasValue ?
                new ObjectParameter("IN_Fumado", iN_Fumado) :
                new ObjectParameter("IN_Fumado", typeof(byte));
    
            var dE_FrecuenciaFumadoParameter = dE_FrecuenciaFumado != null ?
                new ObjectParameter("DE_FrecuenciaFumado", dE_FrecuenciaFumado) :
                new ObjectParameter("DE_FrecuenciaFumado", typeof(string));
    
            var nR_CigarrosXDiasParameter = nR_CigarrosXDias.HasValue ?
                new ObjectParameter("NR_CigarrosXDias", nR_CigarrosXDias) :
                new ObjectParameter("NR_CigarrosXDias", typeof(int));
    
            var dE_DrogaNueva1Parameter = dE_DrogaNueva1 != null ?
                new ObjectParameter("DE_DrogaNueva1", dE_DrogaNueva1) :
                new ObjectParameter("DE_DrogaNueva1", typeof(string));
    
            var dE_DrogaNueva2Parameter = dE_DrogaNueva2 != null ?
                new ObjectParameter("DE_DrogaNueva2", dE_DrogaNueva2) :
                new ObjectParameter("DE_DrogaNueva2", typeof(string));
    
            var dE_DrogaNueva3Parameter = dE_DrogaNueva3 != null ?
                new ObjectParameter("DE_DrogaNueva3", dE_DrogaNueva3) :
                new ObjectParameter("DE_DrogaNueva3", typeof(string));
    
            var iN_Toxicologia1Parameter = iN_Toxicologia1.HasValue ?
                new ObjectParameter("IN_Toxicologia1", iN_Toxicologia1) :
                new ObjectParameter("IN_Toxicologia1", typeof(byte));
    
            var iN_Toxicologia2Parameter = iN_Toxicologia2.HasValue ?
                new ObjectParameter("IN_Toxicologia2", iN_Toxicologia2) :
                new ObjectParameter("IN_Toxicologia2", typeof(byte));
    
            var iN_Toxicologia3Parameter = iN_Toxicologia3.HasValue ?
                new ObjectParameter("IN_Toxicologia3", iN_Toxicologia3) :
                new ObjectParameter("IN_Toxicologia3", typeof(byte));
    
            var fK_IDENTIDAD_GENEROParameter = fK_IDENTIDAD_GENERO.HasValue ?
                new ObjectParameter("FK_IDENTIDAD_GENERO", fK_IDENTIDAD_GENERO) :
                new ObjectParameter("FK_IDENTIDAD_GENERO", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SPC_PERFIL", fK_EpisodioParameter, nR_ExpedienteParameter, fE_PerfilParameter, fE_ContactoParameter, iN_TI_PerfilParameter, fK_EstadoMaritalParameter, fK_CondicionLaboralParameter, fK_ActividadNoLaboralParameter, nR_HijosParameter, fK_EscolaridadParameter, iN_EducEspecialParameter, iN_DesertorEscolarParameter, fK_FamiliaParameter, nR_FamiliarParameter, fK_ResidenciaParameter, iN_ParticReunGruposParameter, fK_FreqAutoAyudaParameter, iN_Arrestado30diasParameter, nR_Arrestos30diasParameter, fK_DSMV_TrastornosClinicos1Parameter, fK_DSMV_TrastornosClinicos2Parameter, fK_DSMV_TrastornosClinicos3Parameter, fK_DSMV_TrastornosPersonalidadRM1Parameter, fK_DSMV_TrastornosPersonalidadRM2Parameter, fK_DSMV_TrastornosPersonalidadRM3Parameter, fK_DSMV_ProblemasPsicosocialesAmbientales1Parameter, fK_DSMV_ProblemasPsicosocialesAmbientales2Parameter, fK_DSMV_ProblemasPsicosocialesAmbientales3Parameter, nR_DSMV_FuncionamientoGlobalParameter, dE_DSMV_OtrasObservacionesParameter, dE_DSMV_ComentariosParameter, iN_DSMV_DiagnosticoDualParameter, fK_DisposicionFinalReferidoParameter, fK_DrogaPrimarioParameter, fK_ViaPrimarioParameter, fK_FrecuenciaPrimarioParameter, iN_EdadInicioPrimarioParameter, fK_DrogaSecundarioParameter, fK_ViaSecundarioParameter, fK_FrecuenciaSecundarioParameter, iN_EdadInicioSecundarioParameter, fK_DrogaTerciarioParameter, fK_ViaTerciarioParameter, fK_FrecuenciaTerciarioParameter, iN_EdadInicioTerciarioParameter, nR_PromedioVisitasParameter, fK_AltaParameter, fK_CentroTrasladoParameter, dE_ComentarioParameter, fK_SesionParameter, fK_SituacionEscolarParameter, fK_TipoAdmisionParameter, fK_CategoriaCentroPrivadoParameter, nR_CelularPrimarioParameter, nR_CelularContactoParameter, dE_EmailPrimarioParameter, dE_EmailSecundarioParameter, fK_CatRecuperacionResParameter, hogarRecuperacionResParameter, fK_DSMV_Sustancias1Parameter, fK_DSMV_Sustancias2Parameter, fK_DSMV_Sustancias3Parameter, iN_FumadoParameter, dE_FrecuenciaFumadoParameter, nR_CigarrosXDiasParameter, dE_DrogaNueva1Parameter, dE_DrogaNueva2Parameter, dE_DrogaNueva3Parameter, iN_Toxicologia1Parameter, iN_Toxicologia2Parameter, iN_Toxicologia3Parameter, fK_IDENTIDAD_GENEROParameter, pK_Perfil);
        }
    
        public virtual int SPC_ALTA_ADMINISTRATIVA(Nullable<int> fK_Episodio, Nullable<System.DateTime> fE_Perfil, Nullable<int> fK_Alta, Nullable<System.Guid> fK_Sesion, ObjectParameter pK_Perfil)
        {
            var fK_EpisodioParameter = fK_Episodio.HasValue ?
                new ObjectParameter("FK_Episodio", fK_Episodio) :
                new ObjectParameter("FK_Episodio", typeof(int));
    
            var fE_PerfilParameter = fE_Perfil.HasValue ?
                new ObjectParameter("FE_Perfil", fE_Perfil) :
                new ObjectParameter("FE_Perfil", typeof(System.DateTime));
    
            var fK_AltaParameter = fK_Alta.HasValue ?
                new ObjectParameter("FK_Alta", fK_Alta) :
                new ObjectParameter("FK_Alta", typeof(int));
    
            var fK_SesionParameter = fK_Sesion.HasValue ?
                new ObjectParameter("FK_Sesion", fK_Sesion) :
                new ObjectParameter("FK_Sesion", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SPC_ALTA_ADMINISTRATIVA", fK_EpisodioParameter, fE_PerfilParameter, fK_AltaParameter, fK_SesionParameter, pK_Perfil);
        }
    
        public virtual int SC_VW_RPT_TEDS_DELETE_AD_SMUEE(Nullable<System.DateTime> start, Nullable<System.DateTime> end, string program)
        {
            var startParameter = start.HasValue ?
                new ObjectParameter("start", start) :
                new ObjectParameter("start", typeof(System.DateTime));
    
            var endParameter = end.HasValue ?
                new ObjectParameter("end", end) :
                new ObjectParameter("end", typeof(System.DateTime));
    
            var programParameter = program != null ?
                new ObjectParameter("program", program) :
                new ObjectParameter("program", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SC_VW_RPT_TEDS_DELETE_AD_SMUEE", startParameter, endParameter, programParameter);
        }
    
        public virtual int SC_VW_RPT_TEDS_DELETE_DIS_SMUEE(Nullable<System.DateTime> start, Nullable<System.DateTime> end, string program)
        {
            var startParameter = start.HasValue ?
                new ObjectParameter("start", start) :
                new ObjectParameter("start", typeof(System.DateTime));
    
            var endParameter = end.HasValue ?
                new ObjectParameter("end", end) :
                new ObjectParameter("end", typeof(System.DateTime));
    
            var programParameter = program != null ?
                new ObjectParameter("program", program) :
                new ObjectParameter("program", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SC_VW_RPT_TEDS_DELETE_DIS_SMUEE", startParameter, endParameter, programParameter);
        }
    
        public virtual int SC_VW_RPT_TEDS_MH_AD_SMUEE(Nullable<System.DateTime> start, Nullable<System.DateTime> end, string program)
        {
            var startParameter = start.HasValue ?
                new ObjectParameter("start", start) :
                new ObjectParameter("start", typeof(System.DateTime));
    
            var endParameter = end.HasValue ?
                new ObjectParameter("end", end) :
                new ObjectParameter("end", typeof(System.DateTime));
    
            var programParameter = program != null ?
                new ObjectParameter("program", program) :
                new ObjectParameter("program", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SC_VW_RPT_TEDS_MH_AD_SMUEE", startParameter, endParameter, programParameter);
        }
    
        public virtual int SC_VW_RPT_TEDS_MH_DIS_SMUEE(Nullable<System.DateTime> start, Nullable<System.DateTime> end, string program)
        {
            var startParameter = start.HasValue ?
                new ObjectParameter("start", start) :
                new ObjectParameter("start", typeof(System.DateTime));
    
            var endParameter = end.HasValue ?
                new ObjectParameter("end", end) :
                new ObjectParameter("end", typeof(System.DateTime));
    
            var programParameter = program != null ?
                new ObjectParameter("program", program) :
                new ObjectParameter("program", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SC_VW_RPT_TEDS_MH_DIS_SMUEE", startParameter, endParameter, programParameter);
        }
    
        public virtual int SC_VW_RPT_TEDS_SA_AD_SMUEE(Nullable<System.DateTime> start, Nullable<System.DateTime> end, string program)
        {
            var startParameter = start.HasValue ?
                new ObjectParameter("start", start) :
                new ObjectParameter("start", typeof(System.DateTime));
    
            var endParameter = end.HasValue ?
                new ObjectParameter("end", end) :
                new ObjectParameter("end", typeof(System.DateTime));
    
            var programParameter = program != null ?
                new ObjectParameter("program", program) :
                new ObjectParameter("program", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SC_VW_RPT_TEDS_SA_AD_SMUEE", startParameter, endParameter, programParameter);
        }
    
        public virtual int SC_VW_RPT_TEDS_SA_DIS_SMUEE(Nullable<System.DateTime> start, Nullable<System.DateTime> end, string program)
        {
            var startParameter = start.HasValue ?
                new ObjectParameter("start", start) :
                new ObjectParameter("start", typeof(System.DateTime));
    
            var endParameter = end.HasValue ?
                new ObjectParameter("end", end) :
                new ObjectParameter("end", typeof(System.DateTime));
    
            var programParameter = program != null ?
                new ObjectParameter("program", program) :
                new ObjectParameter("program", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SC_VW_RPT_TEDS_SA_DIS_SMUEE", startParameter, endParameter, programParameter);
        }
    
        public virtual int SPC_PERFILES_ELIMINADOS_POR_EPISODIO(Nullable<int> episodio, Nullable<System.Guid> pK_Sesion, Nullable<int> estatus_TEDS)
        {
            var episodioParameter = episodio.HasValue ?
                new ObjectParameter("episodio", episodio) :
                new ObjectParameter("episodio", typeof(int));
    
            var pK_SesionParameter = pK_Sesion.HasValue ?
                new ObjectParameter("PK_Sesion", pK_Sesion) :
                new ObjectParameter("PK_Sesion", typeof(System.Guid));
    
            var estatus_TEDSParameter = estatus_TEDS.HasValue ?
                new ObjectParameter("Estatus_TEDS", estatus_TEDS) :
                new ObjectParameter("Estatus_TEDS", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SPC_PERFILES_ELIMINADOS_POR_EPISODIO", episodioParameter, pK_SesionParameter, estatus_TEDSParameter);
        }
    
        public virtual int SPC_PERFILES_ELIMINADOS_POR_PERFIL(Nullable<int> perfil, Nullable<System.Guid> pK_Sesion, Nullable<int> estatus_TEDS)
        {
            var perfilParameter = perfil.HasValue ?
                new ObjectParameter("perfil", perfil) :
                new ObjectParameter("perfil", typeof(int));
    
            var pK_SesionParameter = pK_Sesion.HasValue ?
                new ObjectParameter("PK_Sesion", pK_Sesion) :
                new ObjectParameter("PK_Sesion", typeof(System.Guid));
    
            var estatus_TEDSParameter = estatus_TEDS.HasValue ?
                new ObjectParameter("Estatus_TEDS", estatus_TEDS) :
                new ObjectParameter("Estatus_TEDS", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SPC_PERFILES_ELIMINADOS_POR_PERFIL", perfilParameter, pK_SesionParameter, estatus_TEDSParameter);
        }
    }
}
