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
    
    public partial class SA_PERFIL
    {
        public int PK_NR_Perfil { get; set; }
        public int FK_Episodio { get; set; }
        public System.DateTime FE_Perfil { get; set; }
        public Nullable<System.DateTime> FE_Contacto { get; set; }
        public string IN_TI_Perfil { get; set; }
        public Nullable<byte> IN_Emancipado { get; set; }
        public byte FK_EstadoMarital { get; set; }
        public byte FK_CondicionLaboral { get; set; }
        public byte FK_ActividadNoLaboral { get; set; }
        public Nullable<byte> IN_EmbarazosTratamiento { get; set; }
        public byte NR_Hijos { get; set; }
        public byte FK_Escolaridad { get; set; }
        public Nullable<System.DateTime> FE_UltimoInformeEscolar { get; set; }
        public Nullable<decimal> NR_PromedioAcademico { get; set; }
        public Nullable<short> NR_AusenciasAcad { get; set; }
        public Nullable<byte> IN_EducEspecial { get; set; }
        public Nullable<byte> IN_DesertorEscolar { get; set; }
        public byte FK_Familia { get; set; }
        public byte NR_Familiar { get; set; }
        public byte FK_Residencia { get; set; }
        public Nullable<byte> IN_ParticReunGrupos { get; set; }
        public Nullable<byte> FK_FreqAutoAyuda { get; set; }
        public Nullable<byte> IN_Arrestado30dias { get; set; }
        public Nullable<byte> NR_Arrestos30dias { get; set; }
        public Nullable<byte> IN_Arrestado365dias { get; set; }
        public Nullable<byte> IN_EstLeg { get; set; }
        public Nullable<byte> FK_CondicionPrimaria { get; set; }
        public Nullable<byte> FK_CondicionSecundaria { get; set; }
        public Nullable<byte> FK_CondicionTerciaria { get; set; }
        public Nullable<short> FK_TrastornosClinicosPrimario { get; set; }
        public Nullable<short> FK_TrastornosClinicosSecundario { get; set; }
        public Nullable<short> FK_TrastornosClinicosTerciario { get; set; }
        public Nullable<short> FK_TrastornosPersonalidadPrimario { get; set; }
        public Nullable<short> FK_TrastornosPersonalidadSecundario { get; set; }
        public Nullable<short> FK_TrastornosPersonalidadTerciario { get; set; }
        public string CO_CondicionesMedicasPrimario { get; set; }
        public string CO_CondicionesMedicasSecundario { get; set; }
        public string CO_CondicionesMedicasTerciario { get; set; }
        public Nullable<byte> FK_ProblemasPsicosocialesPrimario { get; set; }
        public Nullable<byte> FK_ProblemasPsicosocialesSecundario { get; set; }
        public Nullable<byte> FK_ProblemasPsicosocialesTerciario { get; set; }
        public Nullable<int> NR_EscalaGAF { get; set; }
        public Nullable<byte> FK_ReferidosGeneradosTX { get; set; }
        public Nullable<byte> FK_DisposicionFinalReferido { get; set; }
        public byte FK_DrogaPrimario { get; set; }
        public byte FK_ViaPrimario { get; set; }
        public byte FK_FrecuenciaPrimario { get; set; }
        public Nullable<byte> IN_EdadInicioPrimario { get; set; }
        public Nullable<byte> NR_CantidadPrimario { get; set; }
        public Nullable<byte> FK_MedidaPrimario { get; set; }
        public Nullable<decimal> NR_GastoPrimario { get; set; }
        public byte FK_DrogaSecundario { get; set; }
        public byte FK_ViaSecundario { get; set; }
        public byte FK_FrecuenciaSecundario { get; set; }
        public Nullable<byte> IN_EdadInicioSecundario { get; set; }
        public Nullable<byte> NR_CantidadSecundario { get; set; }
        public Nullable<byte> FK_MedidaSecundario { get; set; }
        public Nullable<decimal> NR_GastoSecundario { get; set; }
        public byte FK_DrogaTerciario { get; set; }
        public byte FK_ViaTerciario { get; set; }
        public byte FK_FrecuenciaTerciario { get; set; }
        public Nullable<byte> IN_EdadInicioTerciario { get; set; }
        public Nullable<byte> NR_CantidadTerciario { get; set; }
        public Nullable<byte> FK_MedidaTerciario { get; set; }
        public Nullable<decimal> NR_GastoTerciario { get; set; }
        public Nullable<decimal> NR_PromedioVisitas { get; set; }
        public Nullable<byte> FK_Alta { get; set; }
        public Nullable<short> FK_CentroTraslado { get; set; }
        public string DE_Comentario { get; set; }
        public string TI_Transaccion { get; set; }
        public System.Guid FK_Sesion { get; set; }
        public string TI_Edicion { get; set; }
        public System.DateTime FE_Edicion { get; set; }
        public Nullable<bool> ES_Perfil { get; set; }
        public string PK_PerfilID_Legacy { get; set; }
        public Nullable<int> FK_Old { get; set; }
        public Nullable<bool> CHK_ENVIADO { get; set; }
        public Nullable<System.DateTime> FE_ENVIADO { get; set; }
        public Nullable<bool> IN_NOENVIADO { get; set; }
        public Nullable<bool> CHK_ENVIADO_SM { get; set; }
        public Nullable<System.DateTime> FE_ENVIADO_SM { get; set; }
        public Nullable<int> FK_CategoriaCentroPrivado { get; set; }
        public Nullable<int> FK_DSMV_TrastornosClinicos1 { get; set; }
        public Nullable<int> FK_DSMV_TrastornosClinicos2 { get; set; }
        public Nullable<int> FK_DSMV_TrastornosClinicos3 { get; set; }
        public Nullable<int> FK_DSMV_TrastornosPersonalidadRM1 { get; set; }
        public Nullable<int> FK_DSMV_TrastornosPersonalidadRM2 { get; set; }
        public Nullable<int> FK_DSMV_TrastornosPersonalidadRM3 { get; set; }
        public Nullable<int> FK_DSMV_ProblemasPsicosocialesAmbientales1 { get; set; }
        public Nullable<int> FK_DSMV_ProblemasPsicosocialesAmbientales2 { get; set; }
        public Nullable<int> FK_DSMV_ProblemasPsicosocialesAmbientales3 { get; set; }
        public string NR_DSMV_FuncionamientoGlobal { get; set; }
        public string DE_DSMV_OtrasObservaciones { get; set; }
        public string DE_DSMV_Comentarios { get; set; }
        public Nullable<int> IN_DSMV_DiagnosticoDual { get; set; }
        public Nullable<int> FK_TipoAdmision { get; set; }
        public Nullable<int> FK_SituacionEscolar { get; set; }
        public Nullable<bool> IN_ESCOGIDO { get; set; }
        public string FileNameTEDS { get; set; }
        public Nullable<System.DateTime> ProcessDate { get; set; }
        public Nullable<System.DateTime> FE_Registro { get; set; }
        public string NR_CelularPrimario { get; set; }
        public string NR_CelularContacto { get; set; }
        public string DE_EmailPrimario { get; set; }
        public string DE_EmailSecundario { get; set; }
        public Nullable<byte> FK_CatRecuperacionRes { get; set; }
        public string HogarRecuperacionRes { get; set; }
        public Nullable<int> FK_DSMV_Sustancias1 { get; set; }
        public Nullable<int> FK_DSMV_Sustancias2 { get; set; }
        public Nullable<int> FK_DSMV_Sustancias3 { get; set; }
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
        public Nullable<bool> AGREGADO_POR_SMUEE { get; set; }
        public Nullable<System.DateTime> FE_AGREGADO_POR_SMUEE { get; set; }
        public Nullable<int> FK_ESTATUS_PERFIL_TEDS { get; set; }
        public Nullable<System.DateTime> FE_SUBIDO_ACEPTADO_POR_TEDS { get; set; }
        public string TEDS_ID { get; set; }
    
        public virtual SA_EPISODIO SA_EPISODIO { get; set; }
    }
}
