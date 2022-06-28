using SMUEE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace SMUEE.App.Notificaciones
{

    /// <summary>
    /// Summary description for WSnotificaciones
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class WSnotificaciones : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        public List<SM_NOTIFICACIONES> getListaNotificaciones()
        {
            notificacionesComponents NC = new notificacionesComponents();
            var Usuario = (ApplicationUser)Session["Usuario"];

            return NC.GetNotifications(Usuario.Id);


        }
    }
}
