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
    /// Summary description for EpisodiosCerrados
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class EpisodiosCerrados : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        public int OpenEpisode(int episode)
        {
            var user = ConfigurationManager.AppSettings["SEPS_USER"].ToString();
            var password = ConfigurationManager.AppSettings["SEPS_PASSWORD"].ToString();
            var sesionSMUEE = HttpContext.Current.Session["PK_Sesion"].ToString();

            using (var seps = new SEPSEntities())
            {
                ObjectParameter sesion = new ObjectParameter("PK_Sesion", typeof(Guid));

                var list = seps.SPC_SESION(user, password, sesion).ToList();



                if (sesion.Value != null)
                {
                   
                    var e = seps.SA_EPISODIO.FirstOrDefault(x => x.PK_Episodio == episode);

                    //Validar si existe un episodio abierto bajo el mismo programa.
                    //No se le puede estar dando más de un servicio a la vez en un mismo programa
                    var episodios = seps.SA_EPISODIO.FirstOrDefault(x => x.PK_Episodio != e.PK_Episodio && x.FK_Persona == e.FK_Persona && x.FK_Programa == e.FK_Programa && x.ES_Episodio == false);
                    if (episodios == null)
                    {
                        if (e != null)
                        {
                            e.FE_Alta = null;
                            e.ES_Episodio = false;

                            seps.Entry(e).State = EntityState.Modified;

                            if (seps.SaveChanges() > 0)
                            {
                                Logs.Add(new SM_HISTORIAL() {FK_Sesion = sesionSMUEE,FE_Historial = DateTime.Now,
                                FK_Modulo = "MonitoreoSEPS", TI_ACCION = 1, DE_Historial = $"Reabrió episodio {e.PK_Episodio}" });

                                return 1;
                            }
                        }
                    }
                    else
                        return 2;

                    seps.SPD_SESION(Guid.Parse(sesion.Value.ToString()));

                }
            }
            //No se pudo reabir
            return 0;
        }
    }

}

