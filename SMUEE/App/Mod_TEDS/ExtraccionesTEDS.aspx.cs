using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMUEE.App.Mod_TEDS
{
    public partial class ExtraccionesTEDS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
               
            }

        }

        protected void Generar_Click(object sender, EventArgs e)
        {

            Thread.Sleep(2000);

            string start = startDate.Text;
            string end = endDate.Text;

            this.fechaLabel.Text = start + " al " + end;
            saveJobParams("start", string.Format("{0:MM/dd/yyyy}", start));
            saveJobParams("end", string.Format("{0:MM/dd/yyyy}", end));

            SQLExecuteTEDS();
            SQLjob2();

            do
            {

            }
            while (!statusETL());

            //int i = 0;
            //while(i < 50)
            //{
            //    i++;
            //}

            divDescargar.Visible = true;
        }

        public static void saveJobParams(String Parameter, String Value)
        {
            string cmdText = "UPDATE [dbo].[SSIS_Parameters] SET [Value] = '"
                + Value + "' WHERE [Parameter] = '"
                + Parameter
                + "'";

            SqlConnection connection =
                new SqlConnection(ConfigurationManager.ConnectionStrings[2].ConnectionString);

            connection.Open();

            SqlCommand cmd = new SqlCommand(cmdText, connection);

            cmd.ExecuteNonQuery();

            connection.Close();
        }

        public void SQLjob2()
        {

            string myjob = ConfigurationManager.AppSettings["ExecJob1"];
            string cmdText = "exec StartAgentJobAndWait '" + myjob + "',3600 ";

            SqlConnection connection =
                new SqlConnection(ConfigurationManager.ConnectionStrings[2].ConnectionString);

            connection.Open();

            SqlCommand cmd = new SqlCommand(cmdText, connection);
            cmd.CommandTimeout = 0;

            cmd.ExecuteNonQuery();

            connection.Close();
        }

        public void SQLExecuteTEDS()
        {


            using (SqlConnection connection =
                new SqlConnection(ConfigurationManager.ConnectionStrings[2].ConnectionString))
            {
                String MH_ADresult = "", MH_DISresult = "", SA_ADresult = "", SA_DISresult = "";
                String MH_AD = "", MH_DIS = "", SA_AD = "", SA_DIS = "";
                String sqlMH_AD = null, sqlMH_DIS = null, sqlSA_AD = null, sqlSA_DIS = null;
                sqlMH_AD = "SELECT [Value] FROM [dbo].[SSIS_Parameters] WHERE[Parameter] = 'TEDS_MH_AD.txt'";
                sqlSA_AD = "SELECT [Value] FROM [dbo].[SSIS_Parameters] WHERE[Parameter] = 'TEDS_SA_AD.txt'";
                sqlMH_DIS = "SELECT [Value] FROM [dbo].[SSIS_Parameters] WHERE[Parameter] = 'TEDS_MH_DIS.txt'";
                sqlSA_DIS = "SELECT [Value] FROM [dbo].[SSIS_Parameters] WHERE[Parameter] = 'TEDS_SA_DIS.txt'";


                MH_AD = "execute [dbo].[SC_VW_RPT_TEDS_MH_AD] ";
                SA_AD = "execute [dbo].[SC_VW_RPT_TEDS_SA_AD] ";
                MH_DIS = "execute [dbo].[SC_VW_RPT_TEDS_MH_DIS] ";
                SA_DIS = "execute [dbo].[SC_VW_RPT_TEDS_SA_DIS] ";

                try
                {
                    connection.Open();


                    SqlCommand cmdMH_AD = new SqlCommand(MH_AD, connection);
                    cmdMH_AD.CommandTimeout = 0;

                    SqlCommand cmdSA_AD = new SqlCommand(SA_AD, connection);
                    cmdSA_AD.CommandTimeout = 0;

                    SqlCommand cmdMH_DIS = new SqlCommand(MH_DIS, connection);
                    cmdMH_DIS.CommandTimeout = 0;

                    SqlCommand cmdSA_DIS = new SqlCommand(SA_DIS, connection);
                    cmdSA_DIS.CommandTimeout = 0;

                    //cmdMH_AD.CommandType = System.Data.CommandType.StoredProcedure;

                    cmdMH_AD.ExecuteNonQuery();

                    SqlCommand MH_ADcmd = new SqlCommand(sqlMH_AD, connection);
                    //cmd.CommandTimeout = 0;            

                    do
                    {
                        SqlDataReader sqlReaderFile = MH_ADcmd.ExecuteReader();
                        while (sqlReaderFile.Read())
                        {
                            MH_ADresult = sqlReaderFile.GetValue(0).ToString();
                        }
                        sqlReaderFile.Close();
                    } while (MH_ADresult != "1");


                    cmdSA_AD.ExecuteNonQuery();

                    SqlCommand SA_ADcmd = new SqlCommand(sqlSA_AD, connection);
                    //cmd.CommandTimeout = 0;            

                    do
                    {
                        SqlDataReader sqlReaderFileSA_AD = SA_ADcmd.ExecuteReader();
                        while (sqlReaderFileSA_AD.Read())
                        {
                            SA_ADresult = sqlReaderFileSA_AD.GetValue(0).ToString();
                        }
                        sqlReaderFileSA_AD.Close();
                    } while (SA_ADresult != "1");

                    cmdMH_DIS.ExecuteNonQuery();

                    SqlCommand MH_DIScmd = new SqlCommand(sqlMH_DIS, connection);
                    //cmd.CommandTimeout = 0;            

                    do
                    {
                        SqlDataReader sqlReaderFileMH_DIS = MH_DIScmd.ExecuteReader();
                        while (sqlReaderFileMH_DIS.Read())
                        {
                            MH_DISresult = sqlReaderFileMH_DIS.GetValue(0).ToString();
                        }
                        sqlReaderFileMH_DIS.Close();
                    } while (MH_DISresult != "1");

                    cmdSA_DIS.ExecuteNonQuery();

                    SqlCommand SA_DIScmd = new SqlCommand(sqlSA_DIS, connection);
                    //cmd.CommandTimeout = 0;            

                    do
                    {
                        SqlDataReader sqlReaderFileSA_DIS = SA_DIScmd.ExecuteReader();
                        while (sqlReaderFileSA_DIS.Read())
                        {
                            SA_DISresult = sqlReaderFileSA_DIS.GetValue(0).ToString();
                        }
                        sqlReaderFileSA_DIS.Close();
                    } while (SA_DISresult != "1");


                    connection.Close();
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }




            //return text;
        }

        public static Boolean statusETL()
        {
            Boolean value = false;

            SqlConnection sqlCnn;
            SqlCommand sqlCmd;
            String sql = null;
            String result = "";

            sql = "SELECT [Value] FROM [dbo].[SSIS_Parameters] WHERE[Parameter] = 'TEDS_SA_DIS.txt'";

            sqlCnn = new SqlConnection(ConfigurationManager.ConnectionStrings[2].ConnectionString);

            sqlCnn.Open();
            sqlCmd = new SqlCommand(sql, sqlCnn);
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            while (sqlReader.Read())
            {
                result = sqlReader.GetValue(0).ToString();
            }
            sqlReader.Close();
            sqlCmd.Dispose();
            sqlCnn.Close();

            if (result.Equals("1")) { value = true; }

            return value;
        }

        protected void File(object sender, EventArgs e)
        {
            Button btn = (Button)(sender);
            String OldFile = btn.CommandArgument;
            String TedsFile = string.Empty;

            if(OldFile == "TEDS_MH_AD.txt")
            {
                TedsFile = "MH_AD";
            }
            else if (OldFile == "TEDS_SA_AD.txt")
            {
                TedsFile = "SA_AD";
            }
            else if (OldFile == "TEDS_MH_DIS.txt")
            {
                TedsFile = "MH_DIS";
            }
            else if (OldFile == "TEDS_SA_DIS.txt")
            {
                TedsFile = "SA_DIS";
            }

            if (GetFileStatus(OldFile).Equals("1"))
            {

                String fileName = null;
                DirectoryInfo dir = new DirectoryInfo(ConfigurationManager.AppSettings["Job3FolderPath"]);
                FileInfo[] find = dir.GetFiles();

                foreach (FileInfo item in find)
                {
                    if (item.Name.Contains(TedsFile))
                        fileName = item.FullName;
                }

                String sFilePath = fileName;
                //  System.IO.FileInfo Dfile = new System.IO.FileInfo(HttpContext.Current.Server.MapPath(sFilePath));

                string filePath = sFilePath;
                FileInfo file = new FileInfo(filePath);

                byte[] Content = System.IO.File.ReadAllBytes(sFilePath);
                Response.ContentType = "text/csv";
                Response.AddHeader("content-disposition", "attachment; filename=" + file.Name);
                Response.BufferOutput = true;
                Response.OutputStream.Write(Content, 0, Content.Length);
                Response.End();


            }
            else { }

            //   Label2.Text = "Please Wait. Still processing.";
            //   StatusUpdatePanel.Update();
            //    return View("Teds");

        }

        public static string GetFileStatus(String file)
        {
            SqlConnection sqlCnn;
            SqlCommand sqlCmd;
            String sql = null;
            String result = "";

            sql = "SELECT [Value] FROM [dbo].[SSIS_Parameters] WHERE[Parameter] = '" + file + "'";

            sqlCnn = new SqlConnection(ConfigurationManager.ConnectionStrings[2].ConnectionString);

            sqlCnn.Open();
            sqlCmd = new SqlCommand(sql, sqlCnn);
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            while (sqlReader.Read())
            {
                result = sqlReader.GetValue(0).ToString();
            }
            sqlReader.Close();
            sqlCmd.Dispose();
            sqlCnn.Close();

            return result;
        }
    }
}