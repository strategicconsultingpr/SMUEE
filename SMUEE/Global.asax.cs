using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace SMUEE
{
    public class Global : HttpApplication
    {
        string con = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            SqlDependency.Start(con);
        }

        void Session_Start(object sender, EventArgs e)
        {

        }
        void Application_End()
        {
            SqlDependency.Stop(con);
        }
    }
}