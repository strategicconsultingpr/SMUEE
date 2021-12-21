using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMUEE
{
    public partial class Entrada : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime hoy = DateTime.Today;
            this.lblDia.Text = hoy.ToString("dddd", CultureInfo.CreateSpecificCulture("es-US"));
            this.lblFecha.Text = " · " + hoy.ToString("MMMM dd", CultureInfo.CreateSpecificCulture("es-US"));
        }
    }
}