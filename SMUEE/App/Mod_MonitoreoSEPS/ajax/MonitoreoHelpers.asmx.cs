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

        public List<SP_GET_EPISODIOS_CERRADOS_SIN_ALTAS_Result> GetCloseEpisodeWithoutDischarge()
        {
            using(var seps = new SEPSEntities())
            {
                return seps.SP_GET_EPISODIOS_CERRADOS_SIN_ALTAS().ToList();
            }
        }

        [WebMethod]
        public List<SP_GET_EPISODIOS_CERRADOS_SIN_ALTAS_Result> GetCloseEpisodeWithoutDischargeByProgram(int pk_programa)
        {
            return GetCloseEpisodeWithoutDischarge().Where(x => x.FK_Programa == pk_programa).OrderByDescending(x => x.FE_Episodio).ToList();
        }

        [WebMethod]
        public List<SP_GET_EPISODIOS_CERRADOS_SIN_ALTAS_Result> GetCloseEpisodeWithoutDischargeByIUP(int iup)
        {
            return GetCloseEpisodeWithoutDischarge().Where(x => x.FK_Persona == iup).OrderByDescending(x => x.FE_Episodio).ToList();
        }

        [WebMethod]
        public List<SA_PROGRAMA> GetProgramas()
        {
            using (var seps = new SEPSEntities())
            {
                return seps.SA_PROGRAMA.ToList();
            }
        }

        [WebMethod]
        public List<SA_PROGRAMA> GetProgramasNotCloseEpisodeInAdm()
        {
            using (var seps = new SEPSEntities())
            {
                return seps.SA_PROGRAMA.Where(x=>x.CERRAR_EPISODIO_ADMISION == false).OrderBy(x=>x.NB_Programa).ToList();
            }
        }







    }
}

