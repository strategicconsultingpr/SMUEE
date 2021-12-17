using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using SMUEE.Models;

namespace SMUEE.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //RegisterHyperLink.NavigateUrl = "Register";
            // Enable this once you have account confirmation enabled for password reset functionality
            //ForgotPasswordHyperLink.NavigateUrl = "Forgot";
            //OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];
            //var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            //if (!String.IsNullOrEmpty(returnUrl))
            //{
            //    RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            //}
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Validate the user password
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

                ApplicationDbContext context = new ApplicationDbContext();
                // This doen't count login failures towards account lockout
                // To enable password failures to trigger lockout, change to shouldLockout: true


                var user = manager.FindByEmail(Email.Text);


                if (user != null)
                {
                    if (user.Active == false)
                    {
                        FailureText.Text = "Su cuenta se encuentra desactivada. Favor de comunicarse con equipo de informática.";
                        ErrorMessage.Visible = true;
                    }
                    else if (!user.EmailConfirmed)
                    {
                        FailureText.Text = "Intento fallido!. Ustede debe confirmar su cuenta antes de utilizar este sistema.";
                        ErrorMessage.Visible = true;
                        ResendConfirm.Visible = true;
                    }
                    else
                    {
                        var result = signinManager.PasswordSignIn(Email.Text, Password.Text, RememberMe.Checked, shouldLockout: false);

                        switch (result)
                        {
                            case SignInStatus.Success:
                                //IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                                try
                                {
                                    ApplicationUser Usuario = context.Users.Where(u => u.Email.Equals(Email.Text, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                                    //using (CARAEntities dsCARA = new CARAEntities())
                                    //{
                                    //    System.Data.Entity.Core.Objects.ObjectParameter pk_Sesion_Output = new System.Data.Entity.Core.Objects.ObjectParameter("PK_Sesion", typeof(string));

                                    //    var spc_sesion = dsCARA.SPC_SESION(Usuario.Id, pk_Sesion_Output);

                                    //    Session["PK_Sesion"] = pk_Sesion_Output.Value.ToString();
                                    //}

                                    Session["Usuario"] = Usuario;

                                    if (Usuario.PasswordChanged)
                                    {
                                        Response.Redirect("~/App/Entrada", false);
                                    }
                                    else
                                    {
                                        //string code = manager.GeneratePasswordResetToken(Usuario.Id);
                                        //string a = IdentityHelper.GetResetPasswordRedirectUrl();
                                        //string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                                        Session["Usuario"] = null;
                                        // Session["PK_Sesion"] = null;
                                        Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                                        

                                        Response.Redirect("~/Account/ResetPasswordLogin?email="+ Usuario.Email);
                                    }

                                }
                                catch (Exception)
                                {
                                    //Exception err = Server.GetLastError();
                                    //Session.Add("LastError", err);
                                    //Response.Redirect("~/App/Errores/OtrosError");
                                    throw;
                                }

                                break;
                            case SignInStatus.LockedOut:
                                Response.Redirect("/Account/Lockout");
                                break;
                            case SignInStatus.RequiresVerification:
                                Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn?ReturnUrl={0}&RememberMe={1}",
                                                                Request.QueryString["ReturnUrl"],
                                                                RememberMe.Checked),
                                                  true);
                                break;
                            case SignInStatus.Failure:
                            default:
                                FailureText.Text = "Intento de ingreso fallído.";
                                ErrorMessage.Visible = true;
                                break;
                        }
                    }
                }
                else
                {
                    FailureText.Text = "No existe una cuenta con el email ingresado. Favor utilizar email de cuenta existente.";
                    ErrorMessage.Visible = true;
                }

            }

        }

        protected void SendEmailConfirmationToken(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = manager.FindByName(Email.Text);
            if (user != null)
            {
                if (!user.EmailConfirmed)
                {
                    string code = manager.GenerateEmailConfirmationToken(user.Id);
                    string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);

                    string body = CreateBody(callbackUrl, user.NB_Primero, user.AP_Primero);
                    manager.SendEmail(user.Id, "Confirmacion de su cuenta", body);

                    // manager.SendEmail(user.Id, "Confirm your account", callbackUrl);

                    FailureText.Text = "El email de confirmación fué enviado. Verifique su email y confirme su cuenta.";
                    ErrorMessage.Visible = true;
                    //ResendConfirm.Visible = false;
                }
            }
        }

        private string CreateBody(string Code, string FirstName, string LastName)
        {
            string body = string.Empty;
            string code = "<a href =\"" + Code + "\" class=\"es-button\" target=\"_blank\" style=\"mso-style-priority:100 !important;text-decoration:none;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;font-size:18px;color:#4A7EB0;border-style:solid;border-color:#EFEFEF;border-width:10px 25px;display:inline-block;background:#EFEFEF;border-radius:0px;font-weight:normal;font-style:normal;line-height:22px;width:auto;text-align:center;\">Confirmar Cuenta</a>";
            using (StreamReader reader = new StreamReader(Server.MapPath("~/App/EmailsHTML/confirmacionCuenta.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{NombreCompleto}", FirstName + " " + LastName);
            body = body.Replace("{email}", Email.Text);
            body = body.Replace("{password}", Password.Text);
            body = body.Replace("{botonConfirmar}", code);

            return body;

        }

    }
}
