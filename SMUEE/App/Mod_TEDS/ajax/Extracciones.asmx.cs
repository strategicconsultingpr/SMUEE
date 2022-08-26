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

        [WebMethod]
        public byte[] GenerateExcel(string file,DateTime min,DateTime max)
        {
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
                return rv.RvSiteMapping.ServerReport.Render("EXCEL", null, out contentType, out encoding, out extension, out streamIds, out warnings);
            }
            catch (Exception ee)
            {
               var lol = ee.Message;
                return null;
            }
        }
    }
}
