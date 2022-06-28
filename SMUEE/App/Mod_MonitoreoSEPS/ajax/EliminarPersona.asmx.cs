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
    /// Summary description for EliminarPersona
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class EliminarPersona : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        public bool EliminarPersonas(int iup)
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
                    var participante = seps.SA_PERSONA.FirstOrDefault(x => x.PK_Persona == iup);

                    if (participante != null)
                    {
                        if (seps.SA_EPISODIO.Where(x => x.FK_Persona == iup).ToList().Count == 0)
                        {
                            var expedientes = seps.SA_PERSONA_PROGRAMA.Where(x => x.FK_Persona == iup).ToList();

                            if (expedientes.Count > 0)
                            {
                                foreach (var expediente in expedientes)
                                {
                                    seps.Entry(expediente).State = System.Data.Entity.EntityState.Deleted;
                                    listLogs.Add(new SM_HISTORIAL() { TI_ACCION = 2, DE_Historial = $"Elimino expediente #{expediente.NR_Expediente} para IUP:{expediente.FK_Persona} en programa #{expediente.FK_Programa}" });

                                }

                            }

                            seps.Entry(participante).State = System.Data.Entity.EntityState.Deleted;
                            listLogs.Add(new SM_HISTORIAL() { TI_ACCION = 2, DE_Historial = $"Elimino participante para IUP:{participante.PK_Persona}" });


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
                    seps.SPD_SESION(Guid.Parse(sesion.Value.ToString()));

                }
            }
            return false;
        }
    }
}
