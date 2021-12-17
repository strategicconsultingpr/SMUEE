using SMUEE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMUEE.App.Mod_MonitoreoSEPS
{
    public partial class ReportesMonitoreoSEPS : System.Web.UI.Page
    {
        ApplicationDbContext context = new ApplicationDbContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getReportsList();
            }
        }

        private void getReportsList()
        {
            try
            {
                using (SMUEEEntities dsSMUEE = new SMUEEEntities())
                {
                    var reportes = dsSMUEE.VW_REPORTES.Where(a => a.MODULO_REPORTE.Equals("MonitoreoSEPS")).ToList();

                    gvReportsList.DataSource = reportes;

                    gvReportsList.DataBind();

                    gvReportsList.UseAccessibleHeader = true;
                    gvReportsList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    gvReportsList.FooterRow.TableSection = TableRowSection.TableFooter;

                    //var categorias = dsSMUEE.VW_CATEGORIA_REPORTE.Where(a => a.MODULO_REPORTE.Equals("ReportesInformativos")).ToList();
                    //var reportes = dsSMUEE.VW_REPORTES.Where(a => a.MODULO_REPORTE.Equals("ReportesInformativos")).ToList();

                    //VW_CATEGORIA_REPORTE = ConvertCategoriaToDataTable(categorias);
                    //VW_REPORTES = ConvertReportesToDataTable(reportes);

                }
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}