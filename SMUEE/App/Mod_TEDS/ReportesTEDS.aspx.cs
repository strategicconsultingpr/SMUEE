using SMUEE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMUEE.App.Mod_TEDS
{
    public partial class ReportesTEDS : System.Web.UI.Page
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
                    var reportes = dsSMUEE.VW_REPORTES.Where(a => a.MODULO_REPORTE.Equals("TEDS")).ToList();

                    gvReportsList.DataSource = reportes;

                    gvReportsList.DataBind();

                    gvReportsList.UseAccessibleHeader = true;
                    gvReportsList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    gvReportsList.FooterRow.TableSection = TableRowSection.TableFooter;
                }
            }
            catch (Exception)
            {

                throw;
            }

            
        }
    }
}