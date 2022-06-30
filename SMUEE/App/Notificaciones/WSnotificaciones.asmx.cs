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
        public List<VW_NOTIFICACIONES_USUARIO> getListaNotificaciones()
        {
            notificacionesComponents NC = new notificacionesComponents();
            var Usuario = (ApplicationUser)Session["Usuario"];

            return NC.GetNotifications(Usuario.Id);


        }


        [WebMethod(EnableSession = true)]
        public bool CheckNotificaciones(int id)
        {
            var Usuario = (ApplicationUser)Session["Usuario"];


            using(var smuee = new SMUEEEntities())
            {
                var ref_notfication = smuee.SM_NOTIFICACIONES_USUARIO.FirstOrDefault(x=>x.FK_NOTIFICACIONES == id && x.FK_USUARIO == Usuario.Id);

                if(ref_notfication != null)
                {
                    ref_notfication.VISTO = true;
                    smuee.Entry(ref_notfication).State = System.Data.Entity.EntityState.Modified;

                    if (smuee.SaveChanges() > 0)
                        return true;

                }
            }

            return false;

        }






    }
}
