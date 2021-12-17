using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using SMUEE.Models;
using System.Security.Claims;

[assembly: OwinStartupAttribute(typeof(SMUEE.Startup))]
namespace SMUEE
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
            CreateUsersAndRoles();
        }

        public void CreateUsersAndRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("SuperAdmin"))
            {

                var role = new IdentityRole("SuperAdmin");
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "admin@assmca.pr.gov";
                user.Email = "admin@assmca.pr.gov";
                user.NB_Primero = "Super";
                user.NB_Segundo = "Admin";
                user.AP_Primero = "Uee";
                user.AP_Segundo = "Assmca";
                user.PhoneNumber = "7875555555";
                user.PasswordChanged = true;
                user.Active = true;
                user.EmailConfirmed = true;
                string pwd = "Admin@2021";
                var newuser = userManager.Create(user, pwd);
                if (newuser.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, "SuperAdmin");
                }
            }

            if (!roleManager.RoleExists("Administrador"))
            {
                var role = new IdentityRole("Administrador");
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Usuario"))
            {
                var role = new IdentityRole("Usuario");
                roleManager.Create(role);
            }

        }

        public void CreateClaims()
        {
            var TEDS_Claims = new Claim("Modulo", "TEDS");
            var MonitoreoSEPS_Claims = new Claim("Modulo", "MonitoreoSEPS");
            var MantenimientoSEPS_Claims = new Claim("Modulo", "MantenimientoSEPS");
            var ReportesInformativos_Claims = new Claim("Modulo", "ReportesInformativos");
        }
    }
}
