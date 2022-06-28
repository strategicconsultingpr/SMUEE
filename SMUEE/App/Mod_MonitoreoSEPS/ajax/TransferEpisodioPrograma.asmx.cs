using SMUEE.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace SMUEE.App.Mod_MonitoreoSEPS.ajax
{
    /// <summary>
    /// Summary description for TransferEpisodioPrograma
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class TransferEpisodioPrograma : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        public bool TransferEpisode(int episode, int program, int nvlMh, int nvlAs, string expedienteNumber, string expedienteOption, bool deleteExpediente)
        {
            var user = ConfigurationManager.AppSettings["SEPS_USER"].ToString();
            var password = ConfigurationManager.AppSettings["SEPS_PASSWORD"].ToString();
            var sesionSMUEE = HttpContext.Current.Session["PK_Sesion"].ToString();
            var listLogs = new List<SM_HISTORIAL>();

            using (var seps = new SEPSEntities())
            {
                ObjectParameter sesion = new ObjectParameter("PK_Sesion", typeof(Guid));

                var list = seps.SPC_SESION(user, password, sesion).ToList();



                if (sesion.Value != null)
                {
                    var e = seps.SA_EPISODIO.FirstOrDefault(x => x.PK_Episodio == episode);

                    if (e != null)
                    {
                        var oldProgram = e.FK_Programa;
                        e.FK_Programa = (short)program;
                        e.FK_NivelCuidadoMental = (byte?)nvlMh;
                        e.FK_NivelCuidadoSustancias = (byte?)nvlAs;
                        var expediente = seps.SA_PERSONA_PROGRAMA.FirstOrDefault(x => x.FK_Persona == e.FK_Persona && x.FK_Programa == e.FK_Programa);
                        var expedienteOriginal = seps.SA_PERSONA_PROGRAMA.FirstOrDefault(x => x.FK_Persona == e.FK_Persona && x.FK_Programa == oldProgram);
                        listLogs.Add(new SM_HISTORIAL() { TI_ACCION = 1, DE_Historial = $"Transfirió episodio {e.PK_Episodio} en programa #{oldProgram} a programa #{e.FK_Programa}" });


                        if (expedienteOption == "rdExpediente2")
                        {

                            if (expediente == null)
                            {
                                var newExpediente = new SA_PERSONA_PROGRAMA()
                                {
                                    FE_Edicion = DateTime.Now,
                                    FK_Persona = e.FK_Persona,
                                    FK_Programa = (short)program,
                                    NR_Expediente = expedienteNumber,
                                    TI_Edicion = "C",
                                    FK_Sesion = Guid.Parse(sesion.Value.ToString())


                                };

                                seps.SA_PERSONA_PROGRAMA.Add(newExpediente);
                                listLogs.Add(new SM_HISTORIAL() { TI_ACCION = 0, DE_Historial = $"Agrego expediente #{expedienteNumber} para IUP:{e.FK_Persona} en Programa #{program}" });

                            }
                            else
                                return false;

                        }
                        else if (expedienteOption == "rdExpediente3")
                        {
                            
                            if (expedienteOriginal != null && expediente == null)
                            {
                                var newExpediente = new SA_PERSONA_PROGRAMA()
                                {
                                    FE_Edicion = DateTime.Now,
                                    FK_Persona = e.FK_Persona,
                                    FK_Programa = (short)program,
                                    NR_Expediente = expedienteOriginal.NR_Expediente,
                                    TI_Edicion = "C",
                                    FK_Sesion = Guid.Parse(sesion.Value.ToString())


                                };

                                seps.SA_PERSONA_PROGRAMA.Add(newExpediente);
                                listLogs.Add(new SM_HISTORIAL() {TI_ACCION = 0, DE_Historial = $"Agrego expediente #{expedienteOriginal.NR_Expediente} para IUP:{e.FK_Persona} en Programa #{program}" });

                            }
                            else
                                return false;
                        }


                        if (deleteExpediente && expedienteOriginal != null)
                        {
                            var eps = seps.SA_EPISODIO.Where(x => x.FK_Persona == expedienteOriginal.FK_Persona && x.FK_Programa == expedienteOriginal.FK_Programa).ToList();
                            if (eps.Count <=1)
                            {
                                seps.Entry(expedienteOriginal).State = EntityState.Deleted;
                                listLogs.Add(new SM_HISTORIAL() { TI_ACCION = 2, DE_Historial = $"Elimino expediente #{expedienteOriginal.NR_Expediente} para IUP:{expedienteOriginal.FK_Persona} en programa #{expedienteOriginal.FK_Programa}" });

                            }

                        }

                            seps.Entry(e).State = EntityState.Modified;

                        if (seps.SaveChanges() > 0)
                        {
                            if(listLogs.Count >0)
                            {
                                foreach(var historial in listLogs)
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

                    seps.SPD_SESION(Guid.Parse(sesion.Value.ToString()));

                }

            }

            return false;
        }


    }
}
