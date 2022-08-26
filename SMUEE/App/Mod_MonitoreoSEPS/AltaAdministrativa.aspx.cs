using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMUEE.App.Mod_MonitoreoSEPS
{
    public partial class AltaAdministrativa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (var seps = new SEPSEntities())
                {

                    ddlPrograma.DataSource = seps.SA_PROGRAMA.OrderBy(x => x.NB_Programa).ToList();
                    ddlPrograma.DataBind();
                    var defaultItem = new ListItem() { Value = "-1", Text = "Elegir", Selected = true };
                    ddlPrograma.Items.Insert(0, defaultItem);




                }
            }
        }
    }
}