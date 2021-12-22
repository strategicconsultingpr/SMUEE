using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace SMUEE.App.Notificaciones
{
    public class listaNotificaciones
    {
        public int PK_NOTIFICACIONES;
        public string DE_NOTIFICACIONES;
        public string FK_Usuario;
        public string COLOR_ICONO;
        public string NB_ICONO;
        public DateTime FE_ENVIADO;
        public bool VISTO;
    }
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

        [WebMethod]
        public List<SM_NOTIFICACIONES> getListaNotificaciones()
        {
            listaNotificaciones lista = new listaNotificaciones();
            notificacionesComponents NC = new notificacionesComponents();

            string pk_usuario = "a3f1ae28-44e8-4a5e-b789-5e8817a2c388";

            return NC.GetNotifications(pk_usuario);


        }
    }
}
