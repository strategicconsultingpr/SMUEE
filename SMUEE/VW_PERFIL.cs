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
    
    public partial class VW_PERFIL
    {
        public int PK_NR_Perfil { get; set; }
        public int FK_Episodio { get; set; }
        public System.DateTime FE_Perfil { get; set; }
        public string IN_TI_Perfil { get; set; }
        public string DE_DSMV_TrastornosClinicos1 { get; set; }
        public string DE_DSMV_TrastornosClinicos2 { get; set; }
        public string DE_DSMV_TrastornosClinicos3 { get; set; }
        public string CODE_DSMV_TrastornosClinicos1 { get; set; }
        public string CODE_DSMV_TrastornosClinicos2 { get; set; }
        public string CODE_DSMV_TrastornosClinicos3 { get; set; }
        public Nullable<int> FK_DSMV_TrastornosClinicos1 { get; set; }
        public Nullable<int> FK_DSMV_TrastornosClinicos2 { get; set; }
        public Nullable<int> FK_DSMV_TrastornosClinicos3 { get; set; }
        public string DE_DSMV_TrastornosPersonalidadRM1 { get; set; }
        public string DE_DSMV_TrastornosPersonalidadRM2 { get; set; }
        public string DE_DSMV_TrastornosPersonalidadRM3 { get; set; }
        public string CODE_DSMV_TrastornosPersonalidadRM1 { get; set; }
        public string CODE_DSMV_TrastornosPersonalidadRM2 { get; set; }
        public string CODE_DSMV_TrastornosPersonalidadRM3 { get; set; }
        public Nullable<int> FK_DSMV_TrastornosPersonalidadRM1 { get; set; }
        public Nullable<int> FK_DSMV_TrastornosPersonalidadRM2 { get; set; }
        public Nullable<int> FK_DSMV_TrastornosPersonalidadRM3 { get; set; }
        public string DE_DSMV_ProblemasPsicosocialesAmbientales1 { get; set; }
        public string DE_DSMV_ProblemasPsicosocialesAmbientales2 { get; set; }
        public string DE_DSMV_ProblemasPsicosocialesAmbientales3 { get; set; }
        public Nullable<int> FK_DSMV_ProblemasPsicosocialesAmbientales1 { get; set; }
        public Nullable<int> FK_DSMV_ProblemasPsicosocialesAmbientales2 { get; set; }
        public Nullable<int> FK_DSMV_ProblemasPsicosocialesAmbientales3 { get; set; }
        public Nullable<int> IN_DSMV_DiagnosticoDual { get; set; }
        public string NR_DSMV_FuncionamientoGlobal { get; set; }
        public string DE_DSMV_Comentarios { get; set; }
        public string DE_DSMV_OtrasObservaciones { get; set; }
        public string DE_DSMV_DiagnosticoDual { get; set; }
        public string DE_TI_Perfil { get; set; }
        public Nullable<byte> IN_Emancipado { get; set; }
        public string DE_Emancipado { get; set; }
        public byte FK_EstadoMarital { get; set; }
        public string DE_EstadoMarital { get; set; }
        public byte FK_CondicionLaboral { get; set; }
        public string DE_CondLaboral { get; set; }
        public byte FK_ActividadNoLaboral { get; set; }
        public string DE_NoFuerzaLaboral { get; set; }
        public Nullable<byte> IN_EmbarazosTratamiento { get; set; }
        public string DE_EmbarazosTratamiento { get; set; }
        public byte NR_Hijos { get; set; }
        public byte FK_Escolaridad { get; set; }
        public string DE_Grado { get; set; }
        public Nullable<System.DateTime> FE_UltimoInformeEscolar { get; set; }
        public Nullable<decimal> NR_PromedioAcademico { get; set; }
        public Nullable<short> NR_AusenciasAcad { get; set; }
        public Nullable<byte> IN_EducEspecial { get; set; }
        public string DE_EducEspecial { get; set; }
        public Nullable<byte> IN_DesertorEscolar { get; set; }
        public string DE_DesertorEscolar { get; set; }
        public byte FK_Familia { get; set; }
        public string DE_Familiar { get; set; }
        public byte NR_Familiar { get; set; }
        public byte FK_Residencia { get; set; }
        public string DE_Residencia { get; set; }
        public Nullable<byte> IN_ParticReunGrupos { get; set; }
        public string DE_ParticReunGrupos { get; set; }
        public Nullable<byte> FK_FreqAutoAyuda { get; set; }
        public string DE_FreqAutoAyuda { get; set; }
        public Nullable<byte> IN_Arrestado30dias { get; set; }
        public string DE_Arrestado30dias { get; set; }
        public Nullable<byte> NR_Arrestos30dias { get; set; }
        public Nullable<byte> IN_Arrestado365dias { get; set; }
        public string DE_Arrestado365dias { get; set; }
        public Nullable<byte> FK_CondicionPrimaria { get; set; }
        public string DE_DiagnosticoPrimario { get; set; }
        public Nullable<byte> FK_CondicionSecundaria { get; set; }
        public string DE_DiagnosticoSecundario { get; set; }
        public Nullable<byte> FK_CondicionTerciaria { get; set; }
        public string DE_DiagnosticoTerciaria { get; set; }
        public Nullable<short> FK_TrastornosClinicosPrimario { get; set; }
        public string DE_TipoAdmision { get; set; }
        public Nullable<int> FK_TipoAdmision { get; set; }
        public string DE_CategoriaCentroPrivado { get; set; }
        public Nullable<int> FK_CategoriaCentroPrivado { get; set; }
        public string DE_DSMIV_TCP { get; set; }
        public Nullable<short> FK_TrastornosClinicosSecundario { get; set; }
        public string DE_DSMIV_TCS { get; set; }
        public Nullable<short> FK_TrastornosClinicosTerciario { get; set; }
        public string DE_DSMIV_TCT { get; set; }
        public Nullable<short> FK_TrastornosPersonalidadPrimario { get; set; }
        public string DE_DSMIV_TPP { get; set; }
        public Nullable<short> FK_TrastornosPersonalidadSecundario { get; set; }
        public string DE_DSMIV_TPS { get; set; }
        public Nullable<short> FK_TrastornosPersonalidadTerciario { get; set; }
        public string DE_DSMIV_TPT { get; set; }
        public string CO_CondicionesMedicasPrimario { get; set; }
        public string CO_CondicionesMedicasSecundario { get; set; }
        public string CO_CondicionesMedicasTerciario { get; set; }
        public Nullable<byte> FK_ProblemasPsicosocialesPrimario { get; set; }
        public string DE_DSMIV_IV_P { get; set; }
        public Nullable<byte> FK_ProblemasPsicosocialesSecundario { get; set; }
        public string DE_DSMIV_IV_S { get; set; }
        public Nullable<byte> FK_ProblemasPsicosocialesTerciario { get; set; }
        public string DE_DSMIV_IV_T { get; set; }
        public Nullable<int> NR_EscalaGAF { get; set; }
        public Nullable<byte> FK_ReferidosGeneradosTX { get; set; }
        public string DE_ReferidosTX { get; set; }
        public Nullable<byte> FK_DisposicionFinalReferido { get; set; }
        public string DE_DisposicionFinal { get; set; }
        public byte FK_DrogaPrimario { get; set; }
        public string DE_Droga_P { get; set; }
        public byte FK_DrogaSecundario { get; set; }
        public string DE_Droga_S { get; set; }
        public byte FK_DrogaTerciario { get; set; }
        public string DE_Droga_T { get; set; }
        public byte FK_ViaPrimario { get; set; }
        public string DE_Via_P { get; set; }
        public byte FK_ViaSecundario { get; set; }
        public string DE_Via_S { get; set; }
        public byte FK_ViaTerciario { get; set; }
        public string DE_Via_T { get; set; }
        public byte FK_FrecuenciaPrimario { get; set; }
        public string DE_Frecuencia_P { get; set; }
        public byte FK_FrecuenciaSecundario { get; set; }
        public string DE_Frecuencia_S { get; set; }
        public byte FK_FrecuenciaTerciario { get; set; }
        public string DE_Frecuencia_T { get; set; }
        public Nullable<byte> IN_EdadInicioPrimario { get; set; }
        public Nullable<byte> IN_EdadInicioSecundario { get; set; }
        public Nullable<byte> IN_EdadInicioTerciario { get; set; }
        public Nullable<byte> NR_CantidadPrimario { get; set; }
        public Nullable<byte> NR_CantidadSecundario { get; set; }
        public Nullable<byte> NR_CantidadTerciario { get; set; }
        public Nullable<byte> FK_MedidaPrimario { get; set; }
        public string DE_Medida_P { get; set; }
        public Nullable<int> FK_SituacionEscolar { get; set; }
        public string DE_SituacionEscolar { get; set; }
        public Nullable<byte> FK_MedidaSecundario { get; set; }
        public string DE_Medida_S { get; set; }
        public Nullable<byte> FK_MedidaTerciario { get; set; }
        public string DE_Medida_T { get; set; }
        public Nullable<decimal> NR_GastoPrimario { get; set; }
        public Nullable<decimal> NR_GastoSecundario { get; set; }
        public Nullable<decimal> NR_GastoTerciario { get; set; }
        public Nullable<decimal> NR_PromedioVisitas { get; set; }
        public Nullable<byte> FK_Alta { get; set; }
        public string DE_Alta { get; set; }
        public Nullable<short> FK_CentroTraslado { get; set; }
        public string NB_Programa { get; set; }
        public string DE_Comentario { get; set; }
        public string TI_Transaccion { get; set; }
        public string DE_Transaccion { get; set; }
        public System.Guid FK_Sesion { get; set; }
        public string TI_Edicion { get; set; }
        public string DE_Edicion { get; set; }
        public System.DateTime FE_Edicion { get; set; }
        public Nullable<bool> ES_Perfil { get; set; }
        public string DE_SaludMental { get; set; }
        public string DE_AbusoSustancias { get; set; }
        public string NB_ProgramaActual { get; set; }
        public string NB_AdministracionActual { get; set; }
        public Nullable<byte> IN_EstLeg { get; set; }
        public string DE_EstLeg { get; set; }
        public Nullable<System.DateTime> FE_Contacto { get; set; }
        public int FK_Persona { get; set; }
        public string NR_CelularPrimario { get; set; }
        public string NR_CelularContacto { get; set; }
        public string DE_EmailPrimario { get; set; }
        public string DE_EmailSecundario { get; set; }
        public Nullable<byte> FK_CatRecuperacionRes { get; set; }
        public string DE_CarRecuperacionRes { get; set; }
        public string HogarRecuperacionRes { get; set; }
        public Nullable<short> FK_DSMV_Sustancias1 { get; set; }
        public string DE_DSMV_Sustancias1 { get; set; }
        public Nullable<short> FK_DSMV_Sustancias2 { get; set; }
        public string DE_DSMV_Sustancias2 { get; set; }
        public Nullable<short> FK_DSMV_Sustancias3 { get; set; }
        public string DE_DSMV_Sustancias3 { get; set; }
        public Nullable<byte> IN_Fumado { get; set; }
        public string DE_FrecuenciaFumado { get; set; }
        public Nullable<int> NR_CigarrosXDias { get; set; }
        public string DE_DrogaNueva1 { get; set; }
        public string DE_DrogaNueva2 { get; set; }
        public string DE_DrogaNueva3 { get; set; }
        public Nullable<byte> IN_Toxicologia1 { get; set; }
        public Nullable<byte> IN_Toxicologia2 { get; set; }
        public Nullable<byte> IN_Toxicologia3 { get; set; }
        public Nullable<int> FK_IDENTIDAD_GENERO { get; set; }
        public string DE_IDENTIDAD_GENERO { get; set; }
        public string CAT_DSMV_TrastornosClinicos1 { get; set; }
        public string CAT_DSMV_TrastornosClinicos2 { get; set; }
        public string CAT_DSMV_TrastornosClinicos3 { get; set; }
        public string CAT_DSMV_Sustancias1 { get; set; }
        public string CAT_DSMV_Sustancias2 { get; set; }
        public string CAT_DSMV_Sustancias3 { get; set; }
    }
}