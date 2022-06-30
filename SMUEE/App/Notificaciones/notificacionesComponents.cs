using Microsoft.AspNet.SignalR;
using SMUEE.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SMUEE.App.Notificaciones
{
    public class notificacionesComponents
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public void RegisterNotification()
        {
            //try
            //{
            //    using (SMUEEEntities dsSMUEE = new SMUEEEntities())
            //    {
            //        var notificaciones = dsSMUEE.SM_NOTIFICACIONES.Where(a => a.VISTO ==false).ToList();

            //        SqlDependency sqlDep = new SqlDependency(notificaciones);
            //    }
            //}
            //catch (Exception)
            //{

            //    throw;
            //}

            string conStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string sqlCommand = @"SELECT [PK_NOTIFICACIONES],[DE_NOTIFICACIONES],[FK_Usuario],[COLOR_ICONO],[NB_ICONO],[FE_ENVIADO],[VISTO] FROM [SMUEE].[dbo].[SM_NOTIFICACIONES] WHERE [VISTO] = 0";

            using(SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand(sqlCommand, con);
                if(con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                cmd.Notification = null;
                SqlDependency sqlDep = new SqlDependency(cmd);
                sqlDep.OnChange += sqlDep_OnChange;

                cmd.ExecuteReader();
            }
        }

        private void sqlDep_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if(e.Type == SqlNotificationType.Change)
            {
                SqlDependency sqlDep = sender as SqlDependency;
                sqlDep.OnChange += sqlDep_OnChange;

                var notificationHub = GlobalHost.ConnectionManager.GetHubContext<notificacionesHub>();
                notificationHub.Clients.All.notify("added");

                RegisterNotification();

            }
        }

        public List<VW_NOTIFICACIONES_USUARIO> GetNotifications(string pk_usuario)
        {
            try
            {

                using (SMUEEEntities dsSMUEE = new SMUEEEntities())
                {
                    //return dsSMUEE.SM_NOTIFICACIONES.Where(b => b.FK_Usuario.Equals(pk_usuario)).Where(a => a.VISTO.Equals(false)).ToList();
                    return dsSMUEE.VW_NOTIFICACIONES_USUARIO.Where(b => b.FK_USUARIO ==pk_usuario && b.VISTO == false).Take(10).ToList();

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}