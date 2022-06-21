using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace SMUEE.App.Mod_MonitoreoSEPS.ajax
{
    /// <summary>
    /// Summary description for MonitoreoHelpers
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class MonitoreoHelpers : System.Web.Services.WebService
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
        public List<VW_EXPEDIENTE_PROGRAMA> GetExpedienteById(int iup)
        {
            using (var seps = new SEPSEntities())
            {
                return seps.VW_EXPEDIENTE_PROGRAMA.Where(x => x.FK_Persona == iup).ToList();
            }
        }

        [WebMethod]
        public VW_PERSONAS GetExpedienteByNRExpediente(string nr_expediente, int programa)
        {
            using (var seps = new SEPSEntities())
            {
                return seps.VW_PERSONAS.FirstOrDefault(x => nr_expediente == x.NR_Expediente && x.FK_Programa == programa);
            }
        }

    }
}
