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
    
    public partial class SA_EPISODIO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SA_EPISODIO()
        {
            this.SA_PERFIL = new HashSet<SA_PERFIL>();
        }
    
        public int PK_Episodio { get; set; }
        public int FK_Persona { get; set; }
        public short FK_Programa { get; set; }
        public System.DateTime FE_Episodio { get; set; }
        public byte FK_SeguroSalud { get; set; }
        public byte FK_FuentePago { get; set; }
        public byte FK_FeminaHijos { get; set; }
        public Nullable<byte> IN_VaronHijos { get; set; }
        public byte FK_FuenteIngreso { get; set; }
        public Nullable<byte> FK_IngresoIndividual { get; set; }
        public Nullable<byte> FK_IngresoFamiliar { get; set; }
        public byte FK_TiempoResidencia { get; set; }
        public short FK_Municipio { get; set; }
        public byte IN_Zona { get; set; }
        public string NR_ZipCode { get; set; }
        public string DE_Barrio { get; set; }
        public byte FK_EtapaServicio { get; set; }
        public Nullable<byte> FK_NivelCuidadoSustancias { get; set; }
        public Nullable<byte> FK_NivelCuidadoMental { get; set; }
        public byte IN_Metadona { get; set; }
        public byte IN_CodDependiente { get; set; }
        public Nullable<byte> IN_DiagnosticoDual { get; set; }
        public byte FK_FuenteReferido { get; set; }
        public byte FK_EstadoLegal { get; set; }
        public byte IN_ArrestadoAnteriormente { get; set; }
        public Nullable<byte> NR_TotalArrestosPasado { get; set; }
        public byte FK_Justicia { get; set; }
        public Nullable<short> NR_DiasEsperaSustancias { get; set; }
        public byte FK_EpisodiosSustancias { get; set; }
        public byte FK_DuracionSustancias { get; set; }
        public Nullable<short> NR_DiasUltimaAltaSustancias { get; set; }
        public Nullable<short> NR_MesesUltimaAltaSustancias { get; set; }
        public byte FK_NivelCuidadoSustanciasAnterior { get; set; }
        public Nullable<byte> IN_SaludMental { get; set; }
        public Nullable<short> NR_DiasEsperaMental { get; set; }
        public byte FK_EpisodiosMental { get; set; }
        public byte FK_DuracionMental { get; set; }
        public Nullable<short> NR_DiasUltimaAltaMental { get; set; }
        public Nullable<short> NR_MesesUltimaAltaMental { get; set; }
        public byte FK_NivelCuidadoMentalAnterior { get; set; }
        public Nullable<byte> IN_AbusoSustancias { get; set; }
        public byte IN_ViolenciaDomestica { get; set; }
        public byte IN_Maltrato { get; set; }
        public byte IN_TI_Maltrato { get; set; }
        public Nullable<byte> IN_IdeaSuicida { get; set; }
        public byte IN_Suicida { get; set; }
        public Nullable<byte> IN_TratadoMental { get; set; }
        public Nullable<byte> IN_Ambulatorio { get; set; }
        public Nullable<byte> IN_HospitalizadoMental { get; set; }
        public Nullable<byte> IN_TI_Hospital { get; set; }
        public Nullable<byte> IN_TratamientoResidencial { get; set; }
        public Nullable<bool> ES_Episodio { get; set; }
        public Nullable<System.DateTime> FE_Alta { get; set; }
        public string PK_EpiID_Legacy { get; set; }
        public Nullable<bool> CHK_ENVIADO { get; set; }
        public Nullable<int> PK_Old { get; set; }
        public Nullable<System.DateTime> FE_FechaConvenio { get; set; }
    
        public virtual SA_PERSONA SA_PERSONA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SA_PERFIL> SA_PERFIL { get; set; }
    }
}
