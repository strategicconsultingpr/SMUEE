using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMUEE.Account
{
    public partial class ResetPasswordLogin : System.Web.UI.Page
    {
        protected string StatusMessage
        {
            get;
            private set;
        }

        protected void Reset_Click(object sender, EventArgs e)
        {
            string email = this.Request.QueryString["email"].ToString();

            if (email != null || email != string.Empty)
            {
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

                var user = manager.FindByName(Email.Text);
                if (user == null)
                {
                    ErrorMessage.Text = "No se encontró un usuario";
                    return;
                }
                // code = manager.GeneratePasswordResetToken(user.Id);
                IdentityResult result = manager.ChangePassword(user.Id, OldPassword.Text, Password.Text);

                if (result.Succeeded)
                {
                    if (!user.PasswordChanged)
                    {
                        user.PasswordChanged = true;
                        var update = manager.Update(user);
                        if (update.Succeeded)
                        {
                            Response.Redirect("~/Account/ResetPasswordConfirmation");
                            return;
                        }
                    }
                }
                ErrorMessage.Text = result.Errors.FirstOrDefault();
                return;
            }

            ErrorMessage.Text = "Un error ocurrió";
        }
    }
}