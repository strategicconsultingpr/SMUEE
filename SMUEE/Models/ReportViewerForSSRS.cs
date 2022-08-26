using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMUEE.Models
{
    public  class ReportViewerForSSRS
    {
        public ReportViewer RvSiteMapping { get; set; }

        public  ReportViewerForSSRS()
        {
            RvSiteMapping = new ReportViewer();
            ConfigReportViewer();
        }


        public ReportViewerForSSRS(ReportViewer rv)
        {
            RvSiteMapping = rv;
            ConfigReportViewer();
        }




        void ConfigReportViewer()
        {
            RvSiteMapping.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            //IReportServerCredentials irsc = new CustomReportCredentials(ConfigurationManager.AppSettings["SSRS_Username"].ToString(), ConfigurationManager.AppSettings["SSRS_Password"].ToString(), "assmca.local"); // e.g.: ("demo-001", "123456789", "ifc")
            IReportServerCredentials irsc = new CustomReportCredentials("JOSE A RAMOS", "e966c0e48b", ""); 
            RvSiteMapping.ServerReport.ReportServerCredentials = irsc;

            //rvSiteMapping.ServerReport.ReportServerUrl = new Uri("http://192.168.100.24//ReportServer"); //Prod Server - Add the Reporting Server URL 
            RvSiteMapping.ServerReport.ReportServerUrl = new Uri("http://10.0.0.117//ReportServer"); //Prod Server - Add the Reporting Server URL 

        }
    }
}