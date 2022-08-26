using SMUEE.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace SMUEE.App.Mod_MonitoreoSEPS.ajax
{
    /// <summary>
    /// Summary description for AltasAdministrativas
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AltasAdministrativas : System.Web.Services.WebService
    {

        [WebMethod]
        public bool GetEpisodesSAEPByEpisode(int[] arr)
        {
            var user = ConfigurationManager.AppSettings["SEPS_USER"].ToString();
            var password = ConfigurationManager.AppSettings["SEPS_PASSWORD"].ToString();
            var sesionSMUEE = HttpContext.Current.Session["PK_Sesion"].ToString();
            var listLogs = new List<SM_HISTORIAL>();
            var listAltasCreadas = new List<SA_PERFIL>();

            using (var seps = new SEPSEntities())
            {
                ObjectParameter sesion = new ObjectParameter("PK_Sesion", typeof(Guid));

                var list = seps.SPC_SESION(user, password, sesion).ToList();



                if (sesion.Value != null)
                {
                    var lista = seps.VW_SAEP.Where(x => arr.Contains(x.Número_de_Episodio)).ToList();



                    if (lista.Count > 0)
                    {


                        foreach (var episodio in lista)
                        {
                            //Ultimo perfil 
                            var perfil = seps.SA_PERFIL.FirstOrDefault(x => x.PK_NR_Perfil == episodio.PK_NR_Perfil);

                            if (perfil != null)
                            {
                                //Campos en Perfil de alta para TEDS

                                /*
                                 * System Transaction Type
                                 * State Code 
                                 * Reporting Date
                                 * State Provider Identifier
                                 * Client Identifier
                                 * Codependent/collateral
                                 * Type of Treatment Service/Setting
                                 * Date of Last Contact or Data Update
                                 * Date of Discharge
                                 * Reason for Discharge, Transfer, or Discontinuance of Treatment
                                 * Substance Use (Primary, Secondary, and Tertiary) (SU NOM)
                                 * Frequency of Use at Discharge (Primary, secondary and tertiary) (SU NOM)
                                 * Living Arrangements at Discharge (SU/MH NOM)
                                 * Employment Status at Discharge (SU/MH NOM)
                                 * Detailed Not in Labor Force at Discharge (SU/MH NOM)
                                 * Arrests in Past 30 Days -Discharge (SU/MH NOM)
                                 * Attendance at SU Self-Help Groups in Past 30 Days -Discharge (SU NOM)
                                 * Client Transaction Type
                                 * Diagnostic Code Set Identifier
                                 * MH Diagnosis - One
                                 * MH Diagnosis - Two MH Diagnostic Code
                                 * MH Diagnosis - Three
                                 * SMI/SED Status
                                 * School Attendance Status (MH NOM)
                                 * Education (MH NOM)
                                 * CGAS/GAF Score
                                 * 
                                 * 
                                 * 
                                 * SEPS
                                 * Orientación Sexual:
                                 * Identidad de Género:
                                 * Números de teléfono 1
                                 * Números de teléfono 2
                                 * Dirección de correo electrónico 1
                                 * Dirección de correo electrónico 2
                                 * Fecha de alta:
                                 * Fecha de último contacto:
                                 * Estado marital:
                                 * Condición laboral (US-SM-NOM):
                                 * Si no participa en la fuerza laboral (US-SM-NOM):
                                 * Número de hijos:
                                 * Educación (SM-NOM):
                                 * Situación escolar al momento de evaluación (SM-NOM): 6 por default para el alta
                                 * ¿Ha recibido o está recibiendo educación especial?:
                                 * Es desertor escolar:
                                 * ¿Con quién vive la persona? (Disponibles)
                                 * Tamaño familia:
                                 * Residencia (US-SMNOM):
                                 *  ¿Cuántas veces ha participado de reuniones de grupos de auto-ayuda durante los pasados 30 días como apoyo a su proceso de recuperación? (SU-NOM, incluye alcohólicos anónimos, narcóticos anónimos, programas pares etc.) 
                                 *  ¿Ha sido arrestado durante los pasados 30 días? (US-SMNOM)
                                 *  Número de arrestos en los pasados 30 días o durante tratamiento si duró menos de 30 días (US-SM-NOM):
                                 *  Condiciones (Disponibles)
                                 *  Salud Mental [TEDS]
                                 *  Sustancias [TEDS]
                                 *  Comentarios
                                 *  Medida de Funcionamiento Global [TEDS, opcional]
                                 *  Otras observaciones
                                 *  Trastornos concurrentes de salud mental y uso de sustancias [TEDS]
                                 *  ¿Ha fumado al menos 100 cigarrillos en toda su vida?:
                                 *  Si contesto si, ¿con que frecuencia fuma cigarrillos actualmente?:
                                 *  ¿Si fuma todos o algunos días, cuantos cigarrillos en promedio usted fuma en un día?:
                                 *  Droga de uso 1,2,3
                                 *  Vía de utilización 1,2,3
                                 *  Frecuencia de uso 1,2,3
                                 *  Edad de inicio 1,2,3
                                 *  Confirmado por toxicología 1,2,3
                                 *  Razón de alta: 8 - Alta administrativa
                                 *  Comentarios

                                 */




                                var alta = new SA_PERFIL()
                                {
                                    TI_Edicion = "C",
                                    IN_TI_Perfil = "AL",
                                    FK_Sesion = Guid.Parse(sesion.Value.ToString()),
                                    FE_Perfil = DateTime.Now,
                                    FK_Episodio = episodio.Número_de_Episodio,
                                    FK_EstadoMarital = perfil.FK_EstadoMarital,
                                    FK_CondicionLaboral = perfil.FK_CondicionLaboral,
                                    FK_ActividadNoLaboral = perfil.FK_ActividadNoLaboral,
                                    NR_Hijos = perfil.NR_Hijos,
                                    FK_Escolaridad = perfil.FK_Escolaridad,
                                    IN_EducEspecial = perfil.IN_EducEspecial,
                                    IN_DesertorEscolar = perfil.IN_DesertorEscolar,
                                    FK_Familia = perfil.FK_Familia,
                                    NR_Familiar = perfil.NR_Familiar,
                                    FK_Residencia = perfil.FK_Residencia,
                                    IN_ParticReunGrupos = perfil.IN_ParticReunGrupos,
                                    FK_FreqAutoAyuda = perfil.FK_FreqAutoAyuda,
                                    IN_Arrestado30dias = perfil.IN_Arrestado30dias,
                                    NR_Arrestos30dias = perfil.NR_Arrestos30dias,
                                    FK_DSMV_TrastornosClinicos1 = perfil.FK_DSMV_TrastornosClinicos1,
                                    FK_DSMV_TrastornosClinicos2 = perfil.FK_DSMV_TrastornosClinicos2,
                                    FK_DSMV_TrastornosClinicos3 = perfil.FK_DSMV_TrastornosClinicos3,
                                    FK_DSMV_TrastornosPersonalidadRM1 = perfil.FK_DSMV_TrastornosPersonalidadRM1,
                                    FK_DSMV_TrastornosPersonalidadRM2 = perfil.FK_DSMV_TrastornosPersonalidadRM2,
                                    FK_DSMV_TrastornosPersonalidadRM3 = perfil.FK_DSMV_TrastornosPersonalidadRM3,
                                    FK_DSMV_ProblemasPsicosocialesAmbientales1 = perfil.FK_DSMV_ProblemasPsicosocialesAmbientales1,
                                    FK_DSMV_ProblemasPsicosocialesAmbientales2 = perfil.FK_DSMV_ProblemasPsicosocialesAmbientales2,
                                    FK_DSMV_ProblemasPsicosocialesAmbientales3 = perfil.FK_DSMV_ProblemasPsicosocialesAmbientales3,
                                    NR_DSMV_FuncionamientoGlobal = perfil.NR_DSMV_FuncionamientoGlobal,
                                    DE_DSMV_OtrasObservaciones = perfil.DE_DSMV_OtrasObservaciones,
                                    DE_DSMV_Comentarios = perfil.DE_DSMV_Comentarios,
                                    IN_DSMV_DiagnosticoDual = perfil.IN_DSMV_DiagnosticoDual,
                                    FK_DisposicionFinalReferido = perfil.FK_DisposicionFinalReferido,
                                    FK_DrogaPrimario = perfil.FK_DrogaPrimario,
                                    FK_ViaPrimario = perfil.FK_ViaPrimario,
                                    FK_FrecuenciaPrimario = perfil.FK_FrecuenciaPrimario,
                                    IN_EdadInicioPrimario = perfil.IN_EdadInicioPrimario,
                                    FK_DrogaSecundario = perfil.FK_DrogaSecundario,
                                    FK_ViaSecundario = perfil.FK_ViaSecundario,
                                    FK_FrecuenciaSecundario = perfil.FK_FrecuenciaSecundario,
                                    IN_EdadInicioSecundario = perfil.IN_EdadInicioSecundario,
                                    FK_DrogaTerciario = perfil.FK_DrogaTerciario,
                                    FK_ViaTerciario = perfil.FK_ViaTerciario,
                                    FK_FrecuenciaTerciario = perfil.FK_FrecuenciaTerciario,
                                    IN_EdadInicioTerciario = perfil.IN_EdadInicioTerciario,
                                    NR_PromedioVisitas = perfil.NR_PromedioVisitas,
                                    //Alta Administrativa
                                    FK_Alta = 6,
                                    FK_CentroTraslado = perfil.FK_CentroTraslado,
                                    DE_Comentario = perfil.DE_Comentario,
                                    FK_SituacionEscolar = perfil.FK_SituacionEscolar,
                                    FK_TipoAdmision = perfil.FK_TipoAdmision,
                                    FK_CategoriaCentroPrivado = perfil.FK_CategoriaCentroPrivado,
                                    NR_CelularPrimario = perfil.NR_CelularPrimario,
                                    NR_CelularContacto = perfil.NR_CelularContacto,
                                    DE_EmailPrimario = perfil.DE_EmailPrimario,
                                    DE_EmailSecundario = perfil.DE_EmailSecundario,
                                    FK_CatRecuperacionRes = perfil.FK_CatRecuperacionRes,
                                    HogarRecuperacionRes = perfil.HogarRecuperacionRes,
                                    FK_DSMV_Sustancias1 = perfil.FK_DSMV_Sustancias1,
                                    FK_DSMV_Sustancias2 = perfil.FK_DSMV_Sustancias2,
                                    FK_DSMV_Sustancias3 = perfil.FK_DSMV_Sustancias3,
                                    IN_Fumado = perfil.IN_Fumado,
                                    DE_FrecuenciaFumado = perfil.DE_FrecuenciaFumado,
                                    NR_CigarrosXDias = perfil.NR_CigarrosXDias,
                                    DE_DrogaNueva1 = perfil.DE_DrogaNueva1,
                                    DE_DrogaNueva2 = perfil.DE_DrogaNueva2,
                                    DE_DrogaNueva3 = perfil.DE_DrogaNueva3,
                                    IN_Toxicologia1 = perfil.IN_Toxicologia1,
                                    IN_Toxicologia2 = perfil.IN_Toxicologia2,
                                    IN_Toxicologia3 = perfil.IN_Toxicologia3,
                                    FK_IDENTIDAD_GENERO = perfil.FK_IDENTIDAD_GENERO,
                                    TI_Transaccion = "A",

                                };

                                ObjectParameter pk_perfil = new ObjectParameter("pK_Perfil", typeof(Guid));
                                seps.SPC_PERFIL(alta.FK_Episodio, null, alta.FE_Perfil, alta.FE_Contacto,
      alta.IN_TI_Perfil,
      alta.FK_EstadoMarital,
      alta.FK_CondicionLaboral,
      alta.FK_ActividadNoLaboral,
      alta.NR_Hijos,
      alta.FK_Escolaridad,
      alta.IN_EducEspecial,
      alta.IN_DesertorEscolar,
      alta.FK_Familia,
      alta.NR_Familiar,
      alta.FK_Residencia,
      alta.IN_ParticReunGrupos,
      alta.FK_FreqAutoAyuda,
      alta.IN_Arrestado30dias,
      alta.NR_Arrestos30dias,
     (short?)alta.FK_DSMV_TrastornosClinicos1,
     (short?)alta.FK_DSMV_TrastornosClinicos2,
     (short?)alta.FK_DSMV_TrastornosClinicos3,
     (short?)alta.FK_DSMV_TrastornosPersonalidadRM1,
     (short?)alta.FK_DSMV_TrastornosPersonalidadRM2,
     (short?)alta.FK_DSMV_TrastornosPersonalidadRM3,
     (byte?)alta.FK_DSMV_ProblemasPsicosocialesAmbientales1,
     (byte?)alta.FK_DSMV_ProblemasPsicosocialesAmbientales2,
     (byte?)alta.FK_DSMV_ProblemasPsicosocialesAmbientales3,
     alta.NR_DSMV_FuncionamientoGlobal,
     alta.DE_DSMV_OtrasObservaciones,
     alta.DE_DSMV_Comentarios,
     (short?)alta.IN_DSMV_DiagnosticoDual,
      alta.FK_DisposicionFinalReferido,
      alta.FK_DrogaPrimario,
      alta.FK_ViaPrimario,
      alta.FK_FrecuenciaPrimario,
      alta.IN_EdadInicioPrimario,
      alta.FK_DrogaSecundario,
      alta.FK_ViaSecundario,
      alta.FK_FrecuenciaSecundario,
      alta.IN_EdadInicioSecundario,
      alta.FK_DrogaTerciario,
      alta.FK_ViaTerciario,
      alta.FK_FrecuenciaTerciario,
      alta.IN_EdadInicioTerciario,
      alta.NR_PromedioVisitas,
      alta.FK_Alta,
      alta.FK_CentroTraslado,
      alta.DE_Comentario,
      alta.FK_Sesion,
      alta.FK_SituacionEscolar,
      alta.FK_TipoAdmision,
      alta.FK_CategoriaCentroPrivado,
      alta.NR_CelularPrimario,
      alta.NR_CelularContacto,
      alta.DE_EmailPrimario,
      alta.DE_EmailSecundario,
      alta.FK_CatRecuperacionRes,
      alta.HogarRecuperacionRes,
      alta.FK_DSMV_Sustancias1,
      alta.FK_DSMV_Sustancias2,
      alta.FK_DSMV_Sustancias3,
      alta.IN_Fumado,
      alta.DE_FrecuenciaFumado,
      alta.NR_CigarrosXDias,
      alta.DE_DrogaNueva1,
      alta.DE_DrogaNueva2,
      alta.DE_DrogaNueva3,
      alta.IN_Toxicologia1,
      alta.IN_Toxicologia2,
      alta.IN_Toxicologia3,
      alta.FK_IDENTIDAD_GENERO,
     pk_perfil);
                                if (pk_perfil != null)
                                {
                                    listLogs.Add(new SM_HISTORIAL() { TI_ACCION = 1, DE_Historial = $"Registró alta administrativa a episodio {episodio.Número_de_Episodio}" });
                                    alta.PK_NR_Perfil = (int)pk_perfil.Value;
                                    listAltasCreadas.Add(alta);
                                }
                                else
                                {
                                    listAltasCreadas.Add(alta);
                                }

                            }


                            if (seps.SaveChanges() > 0)
                            {
                                if (listLogs.Count > 0)
                                {
                                    foreach (var historial in listLogs)
                                    {
                                        historial.FK_Sesion = sesionSMUEE;
                                        historial.FE_Historial = DateTime.Now;
                                        historial.FK_Modulo = "MonitoreoSEPS";
                                        Logs.Add(historial);
                                    }
                                }
                                return true;
                            }
                        }
                    }
                }

                return false;
            }
        }
    }

}
