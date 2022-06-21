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

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }


        [WebMethod]
        public bool EliminarPersonas(int iup)
        {
            var user = ConfigurationManager.AppSettings["SEPS_USER"].ToString();
            var password = ConfigurationManager.AppSettings["SEPS_PASSWORD"].ToString();

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
                                }

                            }

                            seps.Entry(participante).State = System.Data.Entity.EntityState.Deleted;

                            if (seps.SaveChanges() > 0)
                            {
                                Logs.Add();
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
