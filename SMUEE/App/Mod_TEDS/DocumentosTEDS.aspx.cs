using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMUEE.App.Mod_TEDS
{
    public partial class DocumentosTEDS : System.Web.UI.Page
    {
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
                    var reportes = dsSMUEE.VW_DOCUMENTOS.Where(a => a.FK_MODULO.Equals("TEDS")).ToList();

                    gvDocumentList.DataSource = reportes;

                    gvDocumentList.DataBind();

                    gvDocumentList.UseAccessibleHeader = true;
                    gvDocumentList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    gvDocumentList.FooterRow.TableSection = TableRowSection.TableFooter;
                }
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}
