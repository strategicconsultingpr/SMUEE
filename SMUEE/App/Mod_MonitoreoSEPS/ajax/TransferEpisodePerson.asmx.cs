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


        [WebMethod]
        public VW_PERSONA SearchIUP(int iup)
        {
            using (var seps = new SEPSEntities())
            {
                return seps.VW_PERSONA.FirstOrDefault(x => x.PK_Persona == iup);
            }

        }

        [WebMethod]
        public List<VW_EPISODIO> GetEpisodes(int iup)
        {
            using (var seps = new SEPSEntities())
            {
                return seps.VW_EPISODIO.Where(x => x.FK_Persona == iup).ToList();
            }
        }


        [WebMethod]
        public VW_PERSONAS GetExpediente(int iup, int programa)
        {
            using (var seps = new SEPSEntities())
            {
                return seps.VW_PERSONAS.FirstOrDefault(x => x.PK_Persona == iup && x.FK_Programa == programa);
            }
        }


        [WebMethod]
        public VW_PERSONAS GetExpedienteByNRExpediente(string nr_expediente,int programa)
        {
            using (var seps = new SEPSEntities())
            {
                return seps.VW_PERSONAS.FirstOrDefault(x => nr_expediente == x.NR_Expediente && x.FK_Programa == programa);
            }
        }


        [WebMethod]
        public bool TransferEpisode(int episode, int iup,string expedienteOption,string numberExpediente)
        {
            var user = ConfigurationManager.AppSettings["SEPS_USER"].ToString();
            var password = ConfigurationManager.AppSettings["SEPS_PASSWORD"].ToString();

            using (var seps = new SEPSEntities())
            {
                ObjectParameter sesion = new ObjectParameter("PK_Sesion", typeof(Guid));
               
                var list = seps.SPC_SESION(user, password, sesion).ToList();
              
             

                if (sesion.Value != null)
                {
                    var e = seps.SA_EPISODIO.FirstOrDefault(x => x.PK_Episodio == episode);

                    if (e != null)
                    {
                        e.FK_Persona = iup;
                        

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
                            }
                            else
                                return false;

                        }

                        seps.Entry(e).State = EntityState.Modified;

                        if (seps.SaveChanges() > 0)
                        {
                            Logs.Add();
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
