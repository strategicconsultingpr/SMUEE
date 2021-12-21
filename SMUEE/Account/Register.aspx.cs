using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using SMUEE.Models;

namespace SMUEE.Account
{
    public partial class Register : Page
    {
        ApplicationDbContext context = new ApplicationDbContext();
        ApplicationUser Usuario = new ApplicationUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                try
                {
                    var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                    Usuario = (ApplicationUser)Session["Usuario"];

                    List<ListItem> roles = context.Roles.Select(r => new ListItem { Value = r.Name, Text = r.Name }).ToList();

                    if (userManager.IsInRole(Usuario.Id, "Administrador"))
                    {
                        roles = roles.Where(a => a.Value != "SuperAdmin").ToList();
                    }

                    ddlRol.DataValueField = "Value";
                    ddlRol.DataTextField = "Text";
                    ddlRol.DataSource = roles;
                    ddlRol.DataBind();
                    ddlRol.Items.Insert(0, new ListItem("", "0"));

                    
                }
                catch (Exception ex)
                {

                    string mensaje;

                    if (ex.InnerException == null)
                    {
                        mensaje = ex.Message;
                    }
                    else
                    {
                        mensaje = ex.InnerException.Message;
                    }

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error ", "sweetAlert('Error','" + mensaje + "','error')", true);
                }
            }
        }
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();

            

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            string password = GeneratePassword();

            string mensaje = string.Empty;

            var user = new ApplicationUser();
            user.UserName = Email.Text;
            user.Email = Email.Text;
            user.NB_Primero = NB_Primero.Text;
            user.NB_Segundo = NB_Segundo.Text;
            user.AP_Primero = AP_Primero.Text;
            user.AP_Segundo = AP_Segundo.Text;
            user.PhoneNumber = Telefono.Text;
            user.PasswordChanged = true;
            user.Active = true;
            user.EmailConfirmed = true;
            string pwd = "Admin@2021";

            var newuser = userManager.Create(user, password);

            if (newuser.Succeeded)
            {
                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                //string code = manager.GenerateEmailConfirmationToken(user.Id);
                //string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                //manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");

                //signInManager.SignIn( user, isPersistent: false, rememberBrowser: false);
                //IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);

                var result = userManager.AddToRole(user.Id, ddlRol.SelectedValue);


                var TEDS_Claims = new Claim("Modulo", "TEDS");
                var MonitoreoSEPS_Claims = new Claim("Modulo", "MonitoreoSEPS");
                var MantenimientoSEPS_Claims = new Claim("Modulo", "MantenimientoSEPS");
                var ReportesInformativos_Claims = new Claim("Modulo", "ReportesInformativos");

                for (int i = 0; i < chkModulos.Items.Count; i++)
                {
                    if(chkModulos.Items[i].Selected)
                    {
                        if(chkModulos.Items[i].Value == "TEDS")
                        {
                            userManager.AddClaimAsync(user.Id, TEDS_Claims).GetAwaiter().GetResult();
                        }
                        else if (chkModulos.Items[i].Value == "MonitoreoSEPS")
                        {
                            userManager.AddClaimAsync(user.Id, MonitoreoSEPS_Claims).GetAwaiter().GetResult();
                        }
                        else if (chkModulos.Items[i].Value == "MantenimientoSEPS")
                        {
                            userManager.AddClaimAsync(user.Id, MantenimientoSEPS_Claims).GetAwaiter().GetResult();
                        }
                        else if (chkModulos.Items[i].Value == "ReportesInformativos")
                        {
                            userManager.AddClaimAsync(user.Id, ReportesInformativos_Claims).GetAwaiter().GetResult();
                        }

                    }
                }

                mensaje = "El registro del usuario fué correcto. Se envió un email de confirmación al nuevo usuario.";

                try
                {
                    string code = manager.GenerateEmailConfirmationToken(user.Id);
                    string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                    string body = CreateBody(callbackUrl, password);
                    manager.SendEmail(user.Id, "Confirmacion de su cuenta", body);
                }
                catch (Exception)
                {

                    throw;
                }
                

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Usuario Registrado", "sweetAlert('Usuario Registrado','" + mensaje + "','success')", true);

            }
            else 
            {
                //ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }

        public string GeneratePassword()
        {


            int length = 8;

            bool nonAlphanumeric = true;
            bool digit = true;
            bool lowercase = true;
            bool uppercase = true;

            StringBuilder password = new StringBuilder();
            Random random = new Random();

            while (password.Length < length)
            {
                char c = (char)random.Next(32, 126);

                password.Append(c);

                if (char.IsDigit(c))
                    digit = false;
                else if (char.IsLower(c))
                    lowercase = false;
                else if (char.IsUpper(c))
                    uppercase = false;
                else if (!char.IsLetterOrDigit(c))
                    nonAlphanumeric = false;
            }

            if (nonAlphanumeric)
                password.Append((char)random.Next(33, 48));
            if (digit)
                password.Append((char)random.Next(48, 58));
            if (lowercase)
                password.Append((char)random.Next(97, 123));
            if (uppercase)
                password.Append((char)random.Next(65, 91));

            return password.ToString();
        }

        private string CreateBody(string Code, string password)
        {
            string body = string.Empty;
            string code = "<a href =\"" + Code + "\" class=\"es-button\" target=\"_blank\" style=\"mso-style-priority:100 !important;text-decoration:none;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;font-size:18px;color:#4A7EB0;border-style:solid;border-color:#EFEFEF;border-width:10px 25px;display:inline-block;background:#EFEFEF;border-radius:0px;font-weight:normal;font-style:normal;line-height:22px;width:auto;text-align:center;\">Confirmar Cuenta</a>";
            using (StreamReader reader = new StreamReader(Server.MapPath("~/App/EmailsHTML/confirmacionCuenta.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{NombreCompleto}", NB_Primero.Text + " " + AP_Primero.Text);
            body = body.Replace("{email}", Email.Text);
            body = body.Replace("{password}", password);
            body = body.Replace("{botonConfirmar}", code);

            return body;

        }
    }
}