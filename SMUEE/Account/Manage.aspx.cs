using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using SMUEE.Models;

namespace SMUEE.Account
{
    public partial class Manage : System.Web.UI.Page
    {
        ApplicationDbContext context = new ApplicationDbContext();
        protected string SuccessMessage
        {
            get;
            private set;
        }

        private bool HasPassword(ApplicationUserManager manager)
        {
            return manager.HasPassword(User.Identity.GetUserId());
        }

        public bool HasPhoneNumber { get; private set; }

        public bool TwoFactorEnabled { get; private set; }

        public bool TwoFactorBrowserRemembered { get; private set; }

        public int LoginsCount { get; set; }

        ApplicationUser Usuario = new ApplicationUser();

        protected void Page_Load()
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

            HasPhoneNumber = String.IsNullOrEmpty(manager.GetPhoneNumber(User.Identity.GetUserId()));

            // Enable this after setting up two-factor authentientication
            //PhoneNumber.Text = manager.GetPhoneNumber(User.Identity.GetUserId()) ?? String.Empty;

            TwoFactorEnabled = manager.GetTwoFactorEnabled(User.Identity.GetUserId());

            LoginsCount = manager.GetLogins(User.Identity.GetUserId()).Count;

            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;


            Usuario = (ApplicationUser)Session["Usuario"];

            //ApplicationDbContext context = new ApplicationDbContext();
            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            //var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //this.NB_Primero.Text = Usuario.NB_Primero;
            //this.NB_Segundo.Text = Usuario.NB_Segundo;
            //this.AP_Primero.Text = Usuario.AP_Primero;
            //this.AP_Segundo.Text = Usuario.AP_Segundo;
            //this.lblEmail.Text = Usuario.Email;
            //this.Telefono.Text = Usuario.PhoneNumber;
            //this.lblRol.Text = userManager.GetRoles(Usuario.Id).FirstOrDefault().ToString();


            //profileImg.ImageUrl = "~/Content/img/Avatar.png";
            SetUserInformation();

            if (!IsPostBack)
            {
                // Determine the sections to render
                //if (HasPassword(manager))
                //{
                //    ChangePassword.Visible = true;
                //}
                //else
                //{
                //    CreatePassword.Visible = true;
                //    ChangePassword.Visible = false;
                //}

                // Render success message
                var message = Request.QueryString["m"];
                if (message != null)
                {
                    // Strip the query string from action
                    Form.Action = ResolveUrl("~/Account/Manage");

                    SuccessMessage =
                        message == "ChangePwdSuccess" ? "Your password has been changed."
                        : message == "SetPwdSuccess" ? "Your password has been set."
                        : message == "RemoveLoginSuccess" ? "The account was removed."
                        : message == "AddPhoneNumberSuccess" ? "Phone number has been added"
                        : message == "RemovePhoneNumberSuccess" ? "Phone number was removed"
                        : String.Empty;
                   // successMessage.Visible = !String.IsNullOrEmpty(SuccessMessage);
                }

            }
        }

        private void SetUserInformation()
        {
            Usuario = (ApplicationUser)Session["Usuario"];

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            var rol = userManager.GetRoles(Usuario.Id).FirstOrDefault().ToString();

            this.NB_Primero.Text = Usuario.NB_Primero;
            this.NB_Segundo.Text = Usuario.NB_Segundo;
            this.AP_Primero.Text = Usuario.AP_Primero;
            this.AP_Segundo.Text = Usuario.AP_Segundo;
            this.lblEmail.Text = Usuario.Email;
            this.Telefono.Text = Usuario.PhoneNumber;
            this.lblRol.Text = userManager.GetRoles(Usuario.Id).FirstOrDefault().ToString();

            chkModulos.Enabled = true;

            if (userManager.IsInRole(Usuario.Id, "SuperAdmin") || userManager.IsInRole(Usuario.Id, "Administrador"))
            {
                for (int i = 0; i < chkModulos.Items.Count; i++)
                {
                    chkModulos.Items[i].Selected = true;
                }
            }
            else
            {
                var cla = Usuario.Claims.ToList();
                // var claims = userManager.GetClaimsAsync(item.PK_Usuario);

                var TEDS = cla.Where(x => x.ClaimValue == "TEDS").Count();
                var MantenimientoSEPS = cla.Where(x => x.ClaimValue == "MantenimientoSEPS").Count();
                var MonitoreoSEPS = cla.Where(x => x.ClaimValue == "MonitoreoSEPS").Count();
                var ReportesInformativos = cla.Where(x => x.ClaimValue == "ReportesInformativos").Count();

                if (TEDS > 0)
                {
                    for (int i = 0; i < chkModulos.Items.Count; i++)
                    {
                        if (chkModulos.Items[i].Value == "TEDS")
                        {
                            chkModulos.Items[i].Selected = true;
                        }

                    }
                }
                if (MantenimientoSEPS > 0)
                {
                    for (int i = 0; i < chkModulos.Items.Count; i++)
                    {
                        if (chkModulos.Items[i].Value == "MantenimientoSEPS")
                        {
                            chkModulos.Items[i].Selected = true;
                        }

                    }
                }
                if (MonitoreoSEPS > 0)
                {
                    for (int i = 0; i < chkModulos.Items.Count; i++)
                    {
                        if (chkModulos.Items[i].Value == "MonitoreoSEPS")
                        {
                            chkModulos.Items[i].Selected = true;
                        }

                    }
                }
                if (ReportesInformativos > 0)
                {
                    for (int i = 0; i < chkModulos.Items.Count; i++)
                    {
                        if (chkModulos.Items[i].Value == "ReportesInformativos")
                        {
                            chkModulos.Items[i].Selected = true;
                        }

                    }
                }
            }

            chkModulos.Enabled = false;

            if (Usuario.ProfileImgPath != null)
            {
                profileImg.ImageUrl = ConfigurationManager.AppSettings["URL_Documentos"].ToString() + "UsuarioFotosPerfil/" + Usuario.Email + "/" + Usuario.ProfileImgPath;
            }
            else
            {
                profileImg.ImageUrl = "~/Content/img/Avatar.png";
            }

        }


        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        // Remove phonenumber from user
        protected void RemovePhone_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var result = manager.SetPhoneNumber(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return;
            }
            var user = manager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                Response.Redirect("/Account/Manage?m=RemovePhoneNumberSuccess");
            }
        }

        // DisableTwoFactorAuthentication
        protected void TwoFactorDisable_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            manager.SetTwoFactorEnabled(User.Identity.GetUserId(), false);

            Response.Redirect("/Account/Manage");
        }

        //EnableTwoFactorAuthentication 
        protected void TwoFactorEnable_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            manager.SetTwoFactorEnabled(User.Identity.GetUserId(), true);

            Response.Redirect("/Account/Manage");
        }

        protected void ChangePassword_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            if (IsValid)
            {
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
                IdentityResult result = manager.ChangePassword(User.Identity.GetUserId(), CurrentPassword.Text, NewPassword.Text);
                if (result.Succeeded)
                {
                    var user = manager.FindById(User.Identity.GetUserId());
                    signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);

                    mensaje = "Se actualizó su contraseña correctamente.";

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Contraseña Actualizada", "sweetAlert('Contraseña Actualizada','" + mensaje + "','success')", true);
                    //Response.Redirect("~/Account/Manage?m=ChangePwdSuccess");
                }
                else
                {
                    //AddErrors(result);
                    foreach (var error in result.Errors)
                    {
                        //ModelState.AddModelError("", error);

                        mensaje += error + "\n";
                    }

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error ", "sweetAlert('Error','" + mensaje + "','error')", true);
                    //ClientScript.RegisterStartupScript(this.GetType(), "Error ", "sweetAlertRef('Error','" + mensaje + "','error','Account/Manage.aspx');", true);

                }
            }
        }

        protected void ProfileUpdate_Click(object sender, EventArgs e)
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            Usuario = (ApplicationUser)Session["Usuario"];

            string mensaje = string.Empty;

            //var user = new ApplicationUser();

            var user = userManager.FindById(Usuario.Id);

            //user.UserName = Usuario.Email;
            //user.Email = Usuario.Email;
            user.NB_Primero = NB_Primero.Text;
            user.NB_Segundo = NB_Segundo.Text;
            user.AP_Primero = AP_Primero.Text;
            user.AP_Segundo = AP_Segundo.Text;
            user.PhoneNumber = Telefono.Text;
            user.PasswordChanged = true;
            user.Active = true;
            user.EmailConfirmed = true;
            var newuser = userManager.Update(user);

            if (newuser.Succeeded)
            {
                mensaje = "Se actualizó su contraseña correctamente.";

                context.SaveChanges();

                SetUserInformation();

                //Session["Usuario"] = user;

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Cuenta Actualizada", "sweetAlert('Cuenta Actualizada','" + mensaje + "','success')", true);
            }

            else
            {

            }
        }

        protected void ChangeImg_Click(object sender, EventArgs e)
        {
            if(imgUpload.PostedFile != null)
            {
                string strpath = Path.GetExtension(imgUpload.PostedFile.FileName);
                if(strpath != ".jpg" && strpath != ".png")
                {
                    lblChangeImg.Text = "Solo imagenes de tipo .jpg y .png son permitidos";
                    lblChangeImg.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblChangeImg.Text = "Su nueva imagen de perfil fue agregada!";
                    lblChangeImg.ForeColor = System.Drawing.Color.Green;
                }

                Usuario = (ApplicationUser)Session["Usuario"];

                string fileimg = Path.GetFileName(imgUpload.PostedFile.FileName);

                if (!Directory.Exists(ConfigurationManager.AppSettings["URL_Documentos"].ToString() + "UsuarioFotosPerfil/" + Usuario.Email))
                {
                    Directory.CreateDirectory(ConfigurationManager.AppSettings["URL_Documentos"].ToString() + "UsuarioFotosPerfil/" + Usuario.Email + "/");
                }


                imgUpload.SaveAs(ConfigurationManager.AppSettings["URL_Documentos"].ToString() + "UsuarioFotosPerfil/" + Usuario.Email + "/" + fileimg);

                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


                string mensaje = string.Empty;

                var user = userManager.FindById(Usuario.Id);

                user.ProfileImgPath = fileimg;


                var newuser = userManager.Update(user);

                if (newuser.Succeeded)
                {
                    mensaje = "Se actualizó su contraseña correctamente.";

                    context.SaveChanges();

                    profileImg.ImageUrl = ConfigurationManager.AppSettings["URL_Documentos"].ToString() + "UsuarioFotosPerfil/" + Usuario.Email + "/" + fileimg;

                    //Session["Usuario"] = user;

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Cuenta Actualizada", "sweetAlert('Cuenta Actualizada','" + mensaje + "','success')", true);
                }

                else
                {

                }
            }
        }

    }
}