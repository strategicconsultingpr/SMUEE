using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMUEE.App.Notificaciones
{
    public partial class Notificaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                getNotificationList();
            }
        }

        private void getNotificationList()
        {
            try
            {
                using (SMUEEEntities dsSMUEE = new SMUEEEntities())
                {
                    var listNotificaciones = dsSMUEE.VW_NOTIFICACIONES.OrderByDescending(x=>x.FE_ENVIADO).ToList();

                    gvNotificationList.DataSource = listNotificaciones;

                    gvNotificationList.DataBind();

 
                }
            }
            catch (Exception)
            {

                throw;
            }


        }

    }
}