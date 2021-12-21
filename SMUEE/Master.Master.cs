using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SMUEE.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMUEE
{
    public partial class Master : System.Web.UI.MasterPage
    {
        ApplicationUser Usuario = new ApplicationUser();
        ApplicationDbContext context = new ApplicationDbContext();

        public string AntiXsrfTokenKey { get; private set; }
        public string AntiXsrfUserNameKey { get; private set; }

        private string _antiXsrfTokenValue;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx", false);
                return;
            }
            else if (Session["Usuario"] == null)
            {
                //Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                var userId = Context.User.Identity.GetUserId();
                Usuario = context.Users.Where(a => a.Id.Equals(userId)).FirstOrDefault();
                Session["Usuario"] = Usuario;
                setUserInformation();
                //Response.Redirect("~/Account/Login.aspx", false);
                return;
            }

            setActiveNav();

            if (!Page.IsPostBack)
            {
                setUserInformation();
            }
        }

        private void setUserInformation()
        {
            Usuario = (ApplicationUser)Session["Usuario"];

            
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            this.lblNombre.Text = Usuario.NB_Primero + " " + Usuario.AP_Primero;

            if(Usuario.ProfileImgPath != null)
            {
                profileImg.ImageUrl = ConfigurationManager.AppSettings["URL_Documentos"].ToString() + "UsuarioFotosPerfil/" + Usuario.Email + "/" + Usuario.ProfileImgPath;
            }
            else
            {
                profileImg.ImageUrl = "~/Content/img/Avatar.png";
            }

            

            if(userManager.IsInRole(Usuario.Id, "SuperAdmin") || userManager.IsInRole(Usuario.Id, "Administrador"))
            {
                moduloTEDS.Visible = true;
                moduloMantenimientoSEPS.Visible = true;
                moduloMonitoreoSEPS.Visible = true;
                moduloReportesInformativos.Visible = true;
                divAdministracion.Visible = true;
            }
            else
            {
                var cla = Usuario.Claims.ToList();

                var TEDS = cla.Where(x => x.ClaimValue == "TEDS").Count();
                var MantenimientoSEPS = cla.Where(x => x.ClaimValue == "MantenimientoSEPS").Count();
                var MonitoreoSEPS = cla.Where(x => x.ClaimValue == "MonitoreoSEPS").Count();
                var ReportesInformativos = cla.Where(x => x.ClaimValue == "ReportesInformativos").Count();

                if (TEDS > 0)
                {
                    moduloTEDS.Visible = true;
                }
                if (MantenimientoSEPS > 0)
                {
                    moduloMantenimientoSEPS.Visible = true;
                }
                if (MonitoreoSEPS > 0)
                {
                    moduloMonitoreoSEPS.Visible = true;
                }
                if (ReportesInformativos > 0)
                {
                    moduloReportesInformativos.Visible = true;
                }
            }
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            //try
            //{
            //    using (CARAEntities dsCARA = new CARAEntities())
            //    {
            //        var spd_sesion = dsCARA.SPD_SESION(Session["PK_Sesion"].ToString());
            //    }
            //}
            //catch (Exception)
            //{

            //    throw;
            //}

            Session["Usuario"] = null;
            Session["PK_Sesion"] = null;
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

        }

        protected void setActiveNav()
        {
            string currentFolder = Path.GetFileName( Path.GetDirectoryName(this.Page.Request.FilePath));
            string currentPage = Path.GetFileName(this.Page.Request.FilePath);

            if (currentFolder == "Mod_TEDS")
            {
                this.moduloTEDS.Attributes.Add("class", "nav-item active");

                this.tableroPrincipal.Attributes.Add("class", "nav-item");
                this.moduloMonitoreoSEPS.Attributes.Add("class", "nav-item");
                this.moduloMantenimientoSEPS.Attributes.Add("class", "nav-item");
                this.moduloReportesInformativos.Attributes.Add("class", "nav-item");
                this.manejoUsuarios.Attributes.Add("class", "nav-item");
            }
            else if (currentFolder == "Mod_MonitoreoSEPS")
            {
                this.moduloMonitoreoSEPS.Attributes.Add("class", "nav-item active");

                this.tableroPrincipal.Attributes.Add("class", "nav-item");
                this.moduloTEDS.Attributes.Add("class", "nav-item");
                this.moduloMantenimientoSEPS.Attributes.Add("class", "nav-item");
                this.moduloReportesInformativos.Attributes.Add("class", "nav-item");
                this.manejoUsuarios.Attributes.Add("class", "nav-item");
            }
            else if (currentFolder == "Mod_MantenimientoSEPS")
            {
                this.moduloMantenimientoSEPS.Attributes.Add("class", "nav-item active");

                this.tableroPrincipal.Attributes.Add("class", "nav-item");
                this.moduloTEDS.Attributes.Add("class", "nav-item");
                this.moduloMonitoreoSEPS.Attributes.Add("class", "nav-item");
                this.moduloReportesInformativos.Attributes.Add("class", "nav-item");
                this.manejoUsuarios.Attributes.Add("class", "nav-item");
            }
            else if (currentFolder == "Mod_ReportesInformativos")
            {
                this.moduloReportesInformativos.Attributes.Add("class", "nav-item active");

                this.tableroPrincipal.Attributes.Add("class", "nav-item");
                this.moduloTEDS.Attributes.Add("class", "nav-item");
                this.moduloMonitoreoSEPS.Attributes.Add("class", "nav-item");
                this.moduloMantenimientoSEPS.Attributes.Add("class", "nav-item");
                this.manejoUsuarios.Attributes.Add("class", "nav-item");
            }
            else if (currentFolder == "Account")
            {
                this.moduloReportesInformativos.Attributes.Add("class", "nav-item");
                this.tableroPrincipal.Attributes.Add("class", "nav-item");
                this.moduloTEDS.Attributes.Add("class", "nav-item");
                this.moduloMonitoreoSEPS.Attributes.Add("class", "nav-item");
                this.moduloMantenimientoSEPS.Attributes.Add("class", "nav-item");
                this.manejoUsuarios.Attributes.Add("class", "nav-item");

                if(currentPage == "UsersList" || currentPage == "EditUser" || currentPage == "Register")
                {
                    this.manejoUsuarios.Attributes.Add("class", "nav-item active");
                }
            }
            else if (currentFolder == "App")
            {
                this.moduloReportesInformativos.Attributes.Add("class", "nav-item");
                this.tableroPrincipal.Attributes.Add("class", "nav-item");
                this.moduloTEDS.Attributes.Add("class", "nav-item");
                this.moduloMonitoreoSEPS.Attributes.Add("class", "nav-item");
                this.moduloMantenimientoSEPS.Attributes.Add("class", "nav-item");
                this.manejoUsuarios.Attributes.Add("class", "nav-item");

                if (currentPage == "Entrada")
                {
                    this.tableroPrincipal.Attributes.Add("class", "nav-item active");
                }
            }

        }
    }
}