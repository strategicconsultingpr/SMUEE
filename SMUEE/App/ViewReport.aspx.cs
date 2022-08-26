using Microsoft.Reporting.WebForms;
using SMUEE.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMUEE.App
{
    public partial class ViewReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (this.Request.QueryString["reportSSRS"] != null && this.Request.QueryString["reportName"] != null)
                {
                    lblReporte.Text = "Ver Reporte: " + this.Request.QueryString["reportName"];
                    ShowSSRSReport(this.Request.QueryString["reportSSRS"]);

                }



            }
        }

        private void ShowSSRSReport(string nameReport)
        {

            ReportViewerForSSRS rv = new ReportViewerForSSRS(rvSiteMapping);
            rv.RvSiteMapping.Height = Unit.Pixel(800 - 58);
            rv.RvSiteMapping.ServerReport.ReportPath = $"/Informes de Portal Extracciones/{nameReport}"; //Passing the Report Path   
            rv.RvSiteMapping.ServerReport.Refresh();





        }
    }

}
