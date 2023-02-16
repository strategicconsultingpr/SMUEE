using SMUEE.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace SMUEE.App.Mod_MonitoreoSEPS.ajax
{
    /// <summary>
    /// Summary description for TransferEpisodePerson
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class TransferEpisodePerson : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        public bool TransferEpisode(int episode, int iup,string expedienteOption,string numberExpediente)
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
                        var perfiles = seps.SA_PERFIL.Where(x => x.FK_Episodio == e.PK_Episodio).ToList();

                        if (perfiles.Count > 0)
                        {
                            var oldPerson = e.FK_Persona;
                            e.FK_Persona = iup;
                            listLogs.Add(new SM_HISTORIAL() { TI_ACCION = 1, DE_Historial = $"Transfirió episodio {e.PK_Episodio} perteneciente a IUP-{oldPerson} a  IUP-{e.FK_Persona}" });



                            if (expedienteOption == "rdExpediente2")
                            {

                                var expediente = seps.SA_PERSONA_PROGRAMA.FirstOrDefault(x => x.FK_Persona == iup && x.FK_Programa == e.FK_Programa);
                                if (expediente == null)
                                {
                                    var newExpediente = new SA_PERSONA_PROGRAMA()
                                    {
                                        FE_Edicion = DateTime.Now,
                                        FK_Persona = iup,
                                        FK_Programa = e.FK_Programa,
                                        NR_Expediente = numberExpediente,
                                        TI_Edicion = "C",
                                        FK_Sesion = Guid.Parse(sesion.Value.ToString())


                                    };

                                    seps.SA_PERSONA_PROGRAMA.Add(newExpediente);
                                    listLogs.Add(new SM_HISTORIAL() { TI_ACCION = 0, DE_Historial = $"Agrego expediente #{numberExpediente} para IUP:{e.FK_Persona} en Programa #{e.FK_Programa}" });

                                }
                                else
                                    return false;

                            }

                            seps.Entry(e).State = EntityState.Modified;

                            foreach (var perfil in perfiles)
                            {
                                perfil.TI_Transaccion = "A";
                                //Volver a enviar a TEDS
                                perfil.FK_ESTATUS_PERFIL_TEDS = 5;
                                seps.Entry(perfil).State = EntityState.Modified;

                            }
seps.SPC_PERFILES_ELIMINADOS_POR_EPISODIO(e.PK_Episodio, Guid.Parse(sesion.Value.ToString()),5);
                       

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
            }

            return false;
        }
    }
}
