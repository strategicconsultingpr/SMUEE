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
        [WebMethod]
        public bool TransferEpisode(int episode, int program, int nvlMh, int nvlAs, string expedienteNumber, string expedienteOption, bool deleteExpediente)
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
                        var oldProgram = e.FK_Programa;
                        e.FK_Programa = (short)program;
                        e.FK_NivelCuidadoMental = (byte?)nvlMh;
                        e.FK_NivelCuidadoSustancias = (byte?)nvlAs;
                        var expediente = seps.SA_PERSONA_PROGRAMA.FirstOrDefault(x => x.FK_Persona == e.FK_Persona && x.FK_Programa == e.FK_Programa);
                        var expedienteOriginal = seps.SA_PERSONA_PROGRAMA.FirstOrDefault(x => x.FK_Persona == e.FK_Persona && x.FK_Programa == oldProgram);

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

                            }

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
