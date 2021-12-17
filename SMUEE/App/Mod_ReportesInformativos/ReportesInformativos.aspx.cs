using SMUEE.DsReportesTableAdapters;
using SMUEE.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMUEE.App.Mod_ReportesInformativos
{
    public partial class ReportesInformativos : System.Web.UI.Page
    {
        public SM_REPORTESTableAdapter VW_REPORTES = new SM_REPORTESTableAdapter();
        public SM_CATEGORIA_REPORTETableAdapter VW_CATEGORIA_REPORTES = new SM_CATEGORIA_REPORTETableAdapter();

        ApplicationDbContext context = new ApplicationDbContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getReportsList();
            }
        }

        private void getReportsList()
        {
            try
            {
                using (SMUEEEntities dsSMUEE = new SMUEEEntities())
                {
                    var reportes = dsSMUEE.VW_REPORTES.Where(a => a.MODULO_REPORTE.Equals("ReportesInformativos")).ToList();

                    gvReportsList.DataSource = reportes;

                    gvReportsList.DataBind();

                    gvReportsList.UseAccessibleHeader = true;
                    gvReportsList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    gvReportsList.FooterRow.TableSection = TableRowSection.TableFooter;

                    //var categorias = dsSMUEE.VW_CATEGORIA_REPORTE.Where(a => a.MODULO_REPORTE.Equals("ReportesInformativos")).ToList();
                    //var reportes = dsSMUEE.VW_REPORTES.Where(a => a.MODULO_REPORTE.Equals("ReportesInformativos")).ToList();

                    //VW_CATEGORIA_REPORTE = ConvertCategoriaToDataTable(categorias);
                    //VW_REPORTES = ConvertReportesToDataTable(reportes);

                }
            }
            catch (Exception)
            {

                throw;
            }


        }

        static DataTable ConvertCategoriaToDataTable(List<VW_CATEGORIA_REPORTE> list)
        {
            // New table.
            DataTable table = new DataTable();

            // Get max columns.
            int columns = 4;
            //foreach (var array in list)
            //{
            //    if (array. > columns)
            //    {
            //        columns = array.Length;
            //    }
            //}

            // Add columns.
            for (int i = 0; i < columns; i++)
            {
                table.Columns.Add();
            }

            // Add rows.
            foreach (var array in list)
            {
                table.Rows.Add(array);
            }

            return table;
        }

        static DataTable ConvertReportesToDataTable(List<VW_REPORTES> list)
        {
            // New table.
            DataTable table = new DataTable();

            // Get max columns.
            int columns = 7;
            //foreach (var array in list)
            //{
            //    if (array. > columns)
            //    {
            //        columns = array.Length;
            //    }
            //}

            // Add columns.
            for (int i = 0; i < columns; i++)
            {
                table.Columns.Add();
            }

            // Add rows.
            foreach (var array in list)
            {
                table.Rows.Add(array);
            }

            return table;
        }
    }
}