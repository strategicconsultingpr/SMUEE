using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SMUEE.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
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

                //HtmlGenericControl tableroPrincipal = Page.Master.FindControl("tableroPrincipal") as HtmlGenericControl;
                //HtmlGenericControl moduloTEDS = Page.Master.FindControl("moduloTEDS") as HtmlGenericControl;
                //HtmlGenericControl moduloMonitoreoSEPS = Page.Master.FindControl("moduloMonitoreoSEPS") as HtmlGenericControl;
                //HtmlGenericControl moduloMantenimientoSEPS = Page.Master.FindControl("moduloMantenimientoSEPS") as HtmlGenericControl;
                //HtmlGenericControl moduloReportesInformativos = Page.Master.FindControl("moduloReportesInformativos") as HtmlGenericControl;
                //HtmlGenericControl manejoUsuarios = Page.Master.FindControl("manejoUsuarios") as HtmlGenericControl;

                //tableroPrincipal.Attributes.Add("class", "nav-item");
                //moduloTEDS.Attributes.Add("class", "nav-item");
                //moduloMonitoreoSEPS.Attributes.Add("class", "nav-item");
                //moduloMantenimientoSEPS.Attributes.Add("class", "nav-item");
                //moduloReportesInformativos.Attributes.Add("class", "nav-item");
                //manejoUsuarios.Attributes.Add("class", "nav-item active");
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

                        if (item.ImgPerfil != null)
                        {
                            item.ImgPerfil = "<asp:Image ID=\"profileImg\" ImageUrl=\"" + ConfigurationManager.AppSettings["URL_Documentos"].ToString() + "UsuarioFotosPerfil/" + item.Email + "/" + item.ImgPerfil + "\" runat=\"server\" CssClass=\"img-profile rounded-circle\" />"; 
                        }
                        else
                        {
                            item.ImgPerfil = "~/Content/img/Avatar.png";
                        }

                        if (userManager.IsInRole(user.Id, "SuperAdmin") || userManager.IsInRole(user.Id, "Administrador"))
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

                        if (item.Confirmado == "SI")
                        {
                            item.Confirmado = "<span class='badge bg-success text-white text-wrap' style='width: 6rem;'>SI</span>";
                        }
                        else
                        {
                            item.Confirmado = "<span class='badge bg-danger text-white text-wrap' style='width: 6rem;'>NO</span>";
                        }

                        if (item.Estatus == "Activo")
                        {
                            item.Estatus = "<span class='badge bg-success text-white text-wrap' style='width: 6rem;'>Activo</span>";
                        }
                        else
                        {
                            item.Estatus = "<span class='badge bg-danger text-white text-wrap' style='width: 6rem;'>Inactivo</span>";
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