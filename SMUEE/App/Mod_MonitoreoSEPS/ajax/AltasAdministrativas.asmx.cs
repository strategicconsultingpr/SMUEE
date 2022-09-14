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

        [WebMethod(EnableSession = true)]

        public List<CreatedDischargeBySystem> DichargeBySystem(List<Models.AltasAdministrativas> lst)
        {
            var user = ConfigurationManager.AppSettings["SEPS_USER"].ToString();
            var password = ConfigurationManager.AppSettings["SEPS_PASSWORD"].ToString();
            var sesionSMUEE = HttpContext.Current.Session["PK_Sesion"].ToString();
            var listLogs = new List<SM_HISTORIAL>();
            var listAltasCreadas = new List<CreatedDischargeBySystem>();

            using (var seps = new SEPSEntities())
            {
                ObjectParameter sesion = new ObjectParameter("PK_Sesion", typeof(Guid));

                var list = seps.SPC_SESION(user, password, sesion).ToList();



                if (sesion.Value != null)
                {

                    if (lst.Count > 0)
                    {


                        foreach (var episodio in lst)
                        {
                            ObjectParameter pk = new ObjectParameter("PK_Perfil", typeof(int));

                            seps.SPC_ALTA_ADMINISTRATIVA(episodio.Episodio, episodio.Fecha, episodio.Razon, Guid.Parse(sesion.Value.ToString()), pk);

                            if (pk != null)
                            {
                                listLogs.Add(new SM_HISTORIAL() { TI_ACCION = 0, DE_Historial = $"Creo perfil de alta por sistema #Perfil {pk.Value.ToString()}" });

                            }

                            int pk_parse;
                            int.TryParse(pk.Value.ToString(), out pk_parse);
                            listAltasCreadas.Add(new CreatedDischargeBySystem() { Episodio = episodio.Episodio, Perfil = pk_parse });

                        }

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



                    }
                }

            }

            return listAltasCreadas;

        }


        public class CreatedDischargeBySystem
        {
            public int Episodio { get; set; }
            public int? Perfil { get; set; }
        }
    }


}
