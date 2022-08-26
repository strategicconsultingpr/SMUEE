using Microsoft.Reporting.WebForms;
using SMUEE.Models;
using System;
using System.Collections.Generic;
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

        string folder = "prueba";

        [WebMethod(EnableSession = true)]
        public byte[] GenerateExcel(string file,DateTime min,DateTime max)
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

                rv.RvSiteMapping.ServerReport.ReportPath = $"/{folder}/{file}_TEDS"; //Passing the Report Path
                var startParameter = new ReportParameter("start", min.ToString("yyyy-MM-dd"));
                var endParameter = new ReportParameter("end", max.ToString("yyyy-MM-dd"));

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
    }
}
