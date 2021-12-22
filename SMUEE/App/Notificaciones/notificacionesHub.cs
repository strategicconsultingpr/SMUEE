using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SMUEE.App.Notificaciones
{
    public class notificacionesHub : Hub
    {
        public void Hello()
        {
            //Clients.All.hello();
        }
    }
}