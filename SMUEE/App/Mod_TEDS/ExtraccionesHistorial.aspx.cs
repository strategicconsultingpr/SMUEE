using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMUEE.App.Mod_TEDS
{
    public partial class ExtraccionesHistorial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                using (var smuee = new SMUEEEntities())
                {
                    gvHistory.DataSource = smuee.VW_HISTORIAL.Where(x => x.FK_Modulo == "TEDS").OrderByDescending(x=>x.FE_Historial).ToList();
                    gvHistory.DataBind();

                    gvHistory.UseAccessibleHeader = true;
                    gvHistory.HeaderRow.TableSection = TableRowSection.TableHeader;
                    gvHistory.FooterRow.TableSection = TableRowSection.TableFooter;
                }
            }
        }
    }
}