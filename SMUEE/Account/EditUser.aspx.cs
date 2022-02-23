using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using SMUEE.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMUEE.Account
{
    public partial class EditUser : System.Web.UI.Page
    {
        ApplicationDbContext context = new ApplicationDbContext();
        ApplicationUser Usuario = new ApplicationUser();

        protected string pk_usuario;
        protected void Page_Load(object sender, EventArgs e)
        {
            pk_usuario = this.Request.QueryString["pk_usuario"].ToString();
            Usuario = (ApplicationUser)Session["Usuario"];

            if (!this.IsPostBack)
            {
                try
                {
                    List<ListItem> roles = context.Roles.Select(r => new ListItem { Value = r.Name, Text = r.Name }).ToList();

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

                SetUserInformation(pk_usuario);
            }

            
        }

        private void SetUserInformation(string pk_usuario)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var user = userManager.FindById(pk_usuario);

            var rol = userManager.GetRoles(pk_usuario).FirstOrDefault().ToString();

            this.Email.Text = user.Email;
            this.Telefono.Text = user.PhoneNumber;
            this.NB_Primero.Text = user.NB_Primero;
            this.NB_Segundo.Text = user.NB_Segundo;
            this.AP_Primero.Text = user.AP_Primero;
            this.AP_Segundo.Text = user.AP_Segundo;
            this.ddlRol.SelectedValue = rol;

            chkModulos.Enabled = true;

            if (userManager.IsInRole(pk_usuario, "SuperAdmin") || userManager.IsInRole(pk_usuario, "Administrador"))
            {
                for (int i = 0; i < chkModulos.Items.Count; i++)
                {
                    chkModulos.Items[i].Selected = true;
                }

                chkModulos.Enabled = false;
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
                    for (int i = 0; i < chkModulos.Items.Count; i++)
                    {
                        if(chkModulos.Items[i].Value == "TEDS")
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

            if (Usuario.ProfileImgPath != null)
            {
                profileImg.ImageUrl = ConfigurationManager.AppSettings["URL_Documentos"].ToString() + "UsuarioFotosPerfil/" + user.Email + "/" + user.ProfileImgPath;
            }
            else
            {
                profileImg.ImageUrl = "~/Content/img/Avatar.png";
            }

        }

        protected void ProfileUpdate_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();



            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            string mensaje = string.Empty;

            var user = userManager.FindById(pk_usuario);
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

            var newuser = userManager.Update(user);

            if (newuser.Succeeded)
            {
                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                //string code = manager.GenerateEmailConfirmationToken(user.Id);
                //string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                //manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");

                //signInManager.SignIn( user, isPersistent: false, rememberBrowser: false);
                //IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);

                context.SaveChanges();

                var rol = userManager.GetRoles(pk_usuario).FirstOrDefault().ToString();

                if(rol != ddlRol.SelectedValue)
                {
                    var resultRemove = userManager.RemoveFromRole(user.Id, rol);
                    var resultAdd = userManager.AddToRole(user.Id, ddlRol.SelectedValue);
                }


                var TEDS_Claims = new Claim("Modulo", "TEDS");
                var MonitoreoSEPS_Claims = new Claim("Modulo", "MonitoreoSEPS");
                var MantenimientoSEPS_Claims = new Claim("Modulo", "MantenimientoSEPS");
                var ReportesInformativos_Claims = new Claim("Modulo", "ReportesInformativos");

                var cla = user.Claims.ToList();
                // var claims = userManager.GetClaimsAsync(item.PK_Usuario);

                var TEDS = cla.Where(x => x.ClaimValue == "TEDS").Count();
                var MantenimientoSEPS = cla.Where(x => x.ClaimValue == "MantenimientoSEPS").Count();
                var MonitoreoSEPS = cla.Where(x => x.ClaimValue == "MonitoreoSEPS").Count();
                var ReportesInformativos = cla.Where(x => x.ClaimValue == "ReportesInformativos").Count();

                for (int i = 0; i < chkModulos.Items.Count; i++)
                {
                    if (chkModulos.Items[i].Selected)
                    {
                        if (chkModulos.Items[i].Value == "TEDS" && TEDS < 1)
                        {
                            userManager.AddClaimAsync(user.Id, TEDS_Claims).GetAwaiter().GetResult();
                        }
                        else if (chkModulos.Items[i].Value == "MonitoreoSEPS" && MonitoreoSEPS < 1)
                        {
                            userManager.AddClaimAsync(user.Id, MonitoreoSEPS_Claims).GetAwaiter().GetResult();
                        }
                        else if (chkModulos.Items[i].Value == "MantenimientoSEPS" && MantenimientoSEPS < 1)
                        {
                            userManager.AddClaimAsync(user.Id, MantenimientoSEPS_Claims).GetAwaiter().GetResult();
                        }
                        else if (chkModulos.Items[i].Value == "ReportesInformativos" && ReportesInformativos < 1)
                        {
                            userManager.AddClaimAsync(user.Id, ReportesInformativos_Claims).GetAwaiter().GetResult();
                        }
                    }
                    else
                    {
                        if (chkModulos.Items[i].Value == "TEDS" && TEDS > 0)
                        {
                            userManager.RemoveClaimAsync(user.Id, TEDS_Claims).GetAwaiter().GetResult();
                        }
                        else if (chkModulos.Items[i].Value == "MonitoreoSEPS" && MonitoreoSEPS > 0)
                        {
                            userManager.RemoveClaimAsync(user.Id, MonitoreoSEPS_Claims).GetAwaiter().GetResult();
                        }
                        else if (chkModulos.Items[i].Value == "MantenimientoSEPS" && MantenimientoSEPS > 0)
                        {
                            userManager.RemoveClaimAsync(user.Id, MantenimientoSEPS_Claims).GetAwaiter().GetResult();
                        }
                        else if (chkModulos.Items[i].Value == "ReportesInformativos" && ReportesInformativos > 0)
                        {
                            userManager.RemoveClaimAsync(user.Id, ReportesInformativos_Claims).GetAwaiter().GetResult();
                        }

                    }
                }

                SetUserInformation(pk_usuario);

                mensaje = "La actualización del usuario fué correcto.";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Usuario Actualizado", "sweetAlert('Usuario Actualizado','" + mensaje + "','success')", true);

            }
            else
            {
                //ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }

        protected void ChangeImg_Click(object sender, EventArgs e)
        {
            if (imgUpload.PostedFile != null)
            {
                string strpath = Path.GetExtension(imgUpload.PostedFile.FileName);
                if (strpath != ".jpg" && strpath != ".png")
                {
                    lblChangeImg.Text = "Solo imagenes de tipo .jpg y .png son permitidos";
                    lblChangeImg.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblChangeImg.Text = "Su nueva imagen de perfil fue agregada!";
                    lblChangeImg.ForeColor = System.Drawing.Color.Green;
                }

                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                var user = userManager.FindById(pk_usuario);

                string fileimg = Path.GetFileName(imgUpload.PostedFile.FileName);

                if (!Directory.Exists(ConfigurationManager.AppSettings["URL_Documentos"].ToString() + "UsuarioFotosPerfil/" + user.Email))
                {
                    Directory.CreateDirectory(ConfigurationManager.AppSettings["URL_Documentos"].ToString() + "UsuarioFotosPerfil/" + user.Email + "/");
                }


                imgUpload.SaveAs(ConfigurationManager.AppSettings["URL_Documentos"].ToString() + "UsuarioFotosPerfil/" + user.Email + "/" + fileimg);


                string mensaje = string.Empty;

                

                user.ProfileImgPath = fileimg;


                var newuser = userManager.Update(user);

                if (newuser.Succeeded)
                {
                    mensaje = "Se actualizó su contraseña correctamente.";

                    context.SaveChanges();

                    profileImg.ImageUrl = ConfigurationManager.AppSettings["URL_Documentos"].ToString() + "UsuarioFotosPerfil/" + user.Email + "/" + fileimg;

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