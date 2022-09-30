using Microsoft.Reporting.WebForms;
using SMUEE.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace SMUEE.App.Mod_TEDS.ajax
{
    /// <summary>
    /// Summary description for Extracciones
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class Extracciones : System.Web.Services.WebService
    {
        [WebMethod]
        public List<VW_HISTORIAL> GetHistory()
        {
            using (var smuee = new SMUEEEntities())
            {
                return smuee.VW_HISTORIAL.Where(x => x.FK_Modulo == "TEDS").OrderByDescending(x => x.FE_Historial).ToList();


            }
        }

        [WebMethod(EnableSession = true)]
        public byte[] GenerateExcel(string file,DateTime min,DateTime max,int[] programs)
        {
            var sesionSMUEE = HttpContext.Current.Session["PK_Sesion"].ToString();

            try
            {
                Warning[] warnings;
                string[] streamIds;
                string contentType;
                string encoding;
                string extension;
                ReportViewerForSSRS rv = new ReportViewerForSSRS();

                rv.RvSiteMapping.ServerReport.ReportPath = $"/Informes de Portal Extracciones/{file}_TEDS"; //Passing the Report Path
                var startParameter = new ReportParameter("start", min.ToString("yyyy-MM-dd"));
                var endParameter = new ReportParameter("end", max.ToString("yyyy-MM-dd"));

                var str = "";
                for(int i = 0; i < programs.Length; i++)
                {
                    str += programs[i].ToString()+",";                    
                }

                var programsParameter = new ReportParameter("program", str);

                rv.RvSiteMapping.ServerReport.SetParameters(programsParameter);
                rv.RvSiteMapping.ServerReport.SetParameters(startParameter);
                rv.RvSiteMapping.ServerReport.SetParameters(endParameter);

                //Export the RDLC Report to Byte Array.
                var rep =  rv.RvSiteMapping.ServerReport.Render("EXCEL", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                if (rep != null)
                {
                    var h = new SM_HISTORIAL() { TI_ACCION = 0, DE_Historial = $"Realizo una extracción de {file} {min.ToString("yyyy-MM-dd")} al {max.ToString("yyyy-MM-dd")}", FE_Historial = DateTime.Now, FK_Sesion = sesionSMUEE, FK_Modulo = "TEDS" };
                    Logs.Add(h);
                }

                return rep;
            }
            catch (Exception ee)
            {
                return null;
            }
        }

        [WebMethod]
        public List<List<Models.DdlProgramExtracciones>> GetProgramasTEDS()
        {
            var lstMh = new List<Models.DdlProgramExtracciones>();
            var lstSa = new List<Models.DdlProgramExtracciones>();
            var lstBoth = new List<Models.DdlProgramExtracciones>();


            using (var seps = new SEPSEntities())
            {

                List<List<Models.DdlProgramExtracciones>> lst = new List<List<Models.DdlProgramExtracciones>>();


                var lista =   seps.SA_PROGRAMA.Where(x => x.REP_TEDS == true || x.REP_TEDS_MH == true).ToList();

                var mh = lista.Where(x => x.REP_TEDS == false && x.REP_TEDS_MH == true).OrderBy(x => x.NB_Programa).ToList();
                var sa = lista.Where(x => x.REP_TEDS == true && x.REP_TEDS_MH == false).OrderBy(x => x.NB_Programa).ToList();
                var both = lista.Where(x => x.REP_TEDS == true && x.REP_TEDS_MH == true).OrderBy(x => x.NB_Programa).ToList();


                if (mh.Count > 0)
                {
                   
                    foreach (var programa in mh)
                    {

                        var label = $"{programa.CO_Programa} - {programa.NB_Programa} {((programa.IN_Inactivo == true)? "(Inactivo)":"")}";
                        lstMh.Add(new Models.DdlProgramExtracciones() { label = label, alias = label, value = programa.PK_Programa.ToString()  });
                    }

                }

                if (sa.Count > 0)
                {

                    foreach (var programa in sa)
                    {

                        var label = $"{programa.CO_Programa} - {programa.NB_Programa} {((programa.IN_Inactivo == true) ? "(Inactivo)" : "")}";
                        lstSa.Add(new Models.DdlProgramExtracciones() { label = label, alias = label, value = programa.PK_Programa.ToString() });
                    }

                }


                if (both.Count > 0)
                {

                    foreach (var programa in both)
                    {

                        var label = $"{programa.CO_Programa} - {programa.NB_Programa} {((programa.IN_Inactivo == true) ? "(Inactivo)" : "")}";
                        lstBoth.Add(new Models.DdlProgramExtracciones() { label = label, alias = label, value = programa.PK_Programa.ToString() });
                    }

                }

                lst.Add(lstMh);
                lst.Add(lstSa);
                lst.Add(lstBoth);


                return lst;



            }


        }

       


    }

 
}
