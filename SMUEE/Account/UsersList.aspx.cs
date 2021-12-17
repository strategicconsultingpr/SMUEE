using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SMUEE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMUEE.Account
{
    public partial class UsersList : System.Web.UI.Page
    {
        ApplicationDbContext context = new ApplicationDbContext();
        ApplicationUser Usuario = new ApplicationUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                getUsersList();
            }
        }

        private void getUsersList()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            Usuario = (ApplicationUser)Session["Usuario"];

            try
            {
                using(SMUEEEntities dsSMUEE = new SMUEEEntities())
                {
                    var usersList = dsSMUEE.VW_UsersList.Where(a => a.PK_Usuario != Usuario.Id).ToList();

                    if (userManager.IsInRole(Usuario.Id, "Administrador"))
                    {
                        usersList = usersList.Where(a => a.Rol != "SuperAdmin").ToList();
                    }

                    foreach (VW_UsersList item in usersList)
                    {
                        ApplicationUser user = context.Users.Find(item.PK_Usuario);

                        if(userManager.IsInRole(user.Id, "SuperAdmin") || userManager.IsInRole(user.Id, "Administrador"))
                        {
                            item.Modulos += "<span class='badge bg-warning text-white text-wrap' style='width: 6rem;'>Todos los Modulos</span>";
                        }
                        else
                        {
                            var cla = user.Claims.ToList();
                            // var claims = userManager.GetClaimsAsync(item.PK_Usuario);

                            var TEDS = cla.Where(x => x.ClaimValue == "TEDS").Count();
                            var MantenimientoSEPS = cla.Where(x => x.ClaimValue == "MantenimientoSEPS").Count();
                            var MonitoreoSEPS = cla.Where(x => x.ClaimValue == "MonitoreoSEPS").Count();
                            var ReportesInformativos = cla.Where(x => x.ClaimValue == "ReportesInformativos").Count();

                            if (TEDS > 0)
                            {
                                item.Modulos += "<h5><span class='badge bg-primary text-white text-wrap' style='width: 6rem;'>TEDS</span></h5>&nbsp";
                            }
                            if (MantenimientoSEPS > 0)
                            {
                                item.Modulos += "<span class='badge bg-success text-white text-wrap' style='width: 6rem;'>Mantenimiento SEPS</span>&nbsp";
                            }
                            if (MonitoreoSEPS > 0)
                            {
                                item.Modulos += "<span class='badge bg-info text-white text-wrap' style='width: 6rem;'>Monitoreo SEPS</span>&nbsp";
                            }
                            if (ReportesInformativos > 0)
                            {
                                item.Modulos += "<span class='badge bg-dark text-white text-wrap' style='width: 6rem;'>Reportes Info</span>&nbsp";
                            }
                        }
                        

                    }

                    gvUsersList.DataSource = usersList;

                    gvUsersList.DataBind();

                    gvUsersList.UseAccessibleHeader = true;
                    gvUsersList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    gvUsersList.FooterRow.TableSection = TableRowSection.TableFooter;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}