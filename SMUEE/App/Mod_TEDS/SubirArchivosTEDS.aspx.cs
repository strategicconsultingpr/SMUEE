using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using SMUEE.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMUEE.App.Mod_TEDS
{
    public partial class SubirArchivosTEDS : System.Web.UI.Page
    {
        private string downloadPath = @"\\JOSE-RAMOS\Users\Jose A Ramos\Desktop\shfolder\";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
           Step2Div.Visible = false;
            }
          
        }

        protected async  void btnSubmitFile_Click(object sender, EventArgs e)
        {

            //Get the name of the File.
            string fileName = Path.GetFileName(btnfileInput.PostedFile.FileName);
            var typeFile = ddlTypeFile.SelectedValue;
            var typeFileS = ddlTypeFile.SelectedItem.Text;
            List<IXLRow> lstNoEncontrados = new List<IXLRow>();
            btnNoEncontrados.Visible = false;

            //Get the content type of the File.
            string contentType = btnfileInput.PostedFile.ContentType;

           
  
            using (var excelWorkbook = new XLWorkbook(btnfileInput.PostedFile.InputStream))
            {
                if (excelWorkbook != null)
                {
                    var ws = excelWorkbook.Worksheets.FirstOrDefault();
                    if (ws != null)
                    {
                        var countRows = ws.Rows().Where(x=>x.IsEmpty() == false).Count();
                       
                            //Admision
                            if (typeFile == "1")
                            {

                            var headerRow = ws.Row(1);
                            lstNoEncontrados.Add(headerRow);

                                if (headerRow.Cell(1).Value.ToString() == Helpers.HeaderAD[0] && headerRow.Cell(2).Value.ToString() == Helpers.HeaderAD[1] && headerRow.Cell(3).Value.ToString() == Helpers.HeaderAD[2] && headerRow.Cell(4).Value.ToString() == Helpers.HeaderAD[3]
                                    && headerRow.Cell(5).Value.ToString() == Helpers.HeaderAD[4] && headerRow.Cell(6).Value.ToString() == Helpers.HeaderAD[5] && headerRow.Cell(7).Value.ToString() == Helpers.HeaderAD[6] && headerRow.Cell(8).Value.ToString() == Helpers.HeaderAD[7] &&
                                    headerRow.Cell(9).Value.ToString() == Helpers.HeaderAD[8])
                                {
                                    using (var seps = new SEPSEntities())
                                    {

                                    var totalTransaciones = 0;
                                    var totalAceptados = 0;



                                    foreach (var row in ws.Rows(2,countRows))
                                    {
                                        if (row.IsEmpty())
                                            break;

                                        totalTransaciones++;
                                        var SysTranType = row.Cell(1).Value.ToString().Trim();
                                        var StateCode = row.Cell(2).Value.ToString().Trim();
                                        var ReportDate = row.Cell(3).Value.ToString().Trim();
                                        var ProviderID = row.Cell(4).Value.ToString().Trim();
                                        var ClientID = row.Cell(5).Value.ToString().Trim();
                                        var CoDep = row.Cell(6).Value.ToString().Trim();
                                        var ClientTransType = row.Cell(7).Value.ToString().Trim();
                                        var DateAdmission = row.Cell(8).Value.ToString().Trim();
                                        var Services = row.Cell(9).Value.ToString().Trim();

                                        var teds_id = $"{StateCode}{ReportDate}{ProviderID}{ClientID}{CoDep}{ClientTransType}{DateAdmission}{Services}";
                                        var flag = await FindPerfil(seps, SysTranType, teds_id);

                                        if(flag)
                                            totalAceptados++;
                                        else
                                        {
                                            lstNoEncontrados.Add(row);

                                        }


                                    }

       

                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Usuario Actualizado", $"sweetAlert('Completado','Se han encontrado {totalAceptados} de {totalTransaciones} transacciones aceptadas','success')", true);

                                    await seps.SaveChangesAsync();

                                    if(totalAceptados != totalTransaciones)
                                    {
                                        GenerateExcelNoEncontrados(lstNoEncontrados);
                                    }

                                    Step1Div.Visible = false;
                                    txtTotal.InnerText= totalTransaciones.ToString();
                                    txtAcceptadas.InnerText = totalAceptados.ToString() ;
                                    txtNoEncontradas.InnerText = (totalTransaciones - totalAceptados).ToString();
                                    Step2Div.Visible = true;
                             


                                }
                            }
                                else
                                {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Usuario Actualizado", "sweetAlert('Formato Incorrecto','Este archivo no cuenta con un formato de " + typeFileS.ToLower() + "','error')", true);
                            }


                        }
                            //Altas
                            else if (typeFile == "2")
                            {

                            var headerRow = ws.Row(1);
                            lstNoEncontrados.Add(headerRow);


                            if (headerRow.Cell(1).Value.ToString() == Helpers.HeaderDIS[0] && headerRow.Cell(2).Value.ToString() == Helpers.HeaderDIS[1] && headerRow.Cell(3).Value.ToString() == Helpers.HeaderDIS[2] && headerRow.Cell(4).Value.ToString() == Helpers.HeaderDIS[3]
                                && headerRow.Cell(5).Value.ToString() == Helpers.HeaderDIS[4] && headerRow.Cell(6).Value.ToString() == Helpers.HeaderDIS[5] && headerRow.Cell(7).Value.ToString() == Helpers.HeaderDIS[6] && headerRow.Cell(8).Value.ToString() == Helpers.HeaderDIS[7] &&
                                headerRow.Cell(9).Value.ToString() == Helpers.HeaderDIS[8] && headerRow.Cell(14).Value.ToString() == Helpers.HeaderDIS[13] && headerRow.Cell(15).Value.ToString() == Helpers.HeaderDIS[14] && headerRow.Cell(32).Value.ToString() == Helpers.HeaderDIS[31])
                            {
                                using (var seps = new SEPSEntities())
                                {

                                    var totalTransaciones = 0;
                                    var totalAceptados = 0;


                                    foreach (var row in ws.Rows(2, countRows))
                                    {
                                        if (row.IsEmpty())
                                            break;

                                        totalTransaciones++;

                                        var SysTranType = row.Cell(1).Value.ToString().Trim();
                                        var StateCode = row.Cell(2).Value.ToString().Trim();
                                        var ReportDate = row.Cell(3).Value.ToString().Trim();
                                        var ProviderID = row.Cell(4).Value.ToString().Trim();
                                        var ClientID = row.Cell(5).Value.ToString().Trim();
                                        var CoDep = row.Cell(6).Value.ToString().Trim();
                                        var Services = row.Cell(7).Value.ToString().Trim();
                                        var DateLastContact = row.Cell(8).Value.ToString().Trim();
                                        var DateDischarge = row.Cell(9).Value.ToString().Trim();
                                        var DateAdmission = row.Cell(15).Value.ToString().Trim();
                                        var ClientTransType = row.Cell(32).Value.ToString().Trim();

                                        var teds_id =  $"{StateCode}{ReportDate}{ProviderID}{ClientID}{CoDep}{Services}{DateLastContact}{DateDischarge}{DateAdmission}{ClientTransType}";

                                        var flag = await FindPerfil(seps, SysTranType, teds_id);

                                        if (flag)
                                            totalAceptados++;
                                        else
                                        {
                                            lstNoEncontrados.Add(row);
                                        }


                                    }


                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Usuario Actualizado", $"sweetAlert('Completado','Se han encontrado {totalAceptados} de {totalTransaciones} transacciones aceptadas','success')", true);

                                    await seps.SaveChangesAsync();

                                    if (totalAceptados != totalTransaciones)
                                    {
                                        GenerateExcelNoEncontrados(lstNoEncontrados);

                                    }

                                    Step1Div.Visible = false;
                                    txtTotal.InnerText = totalTransaciones.ToString();
                                    txtAcceptadas.InnerText = totalAceptados.ToString();
                                    txtNoEncontradas.InnerText = (totalTransaciones - totalAceptados).ToString();
                                    Step2Div.Visible = true;

                                }
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Usuario Actualizado", "sweetAlert('Formato Incorrecto','Este archivo no cuenta con un formato de " + typeFileS.ToLower() + "','error')", true);
                            }
                        }
                        
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Usuario Actualizado", $"sweetAlert('Error','Ha ocurrido un error, por favor inténtelo de nuevo más tarde','error')", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Usuario Actualizado", $"sweetAlert('Error','Ha ocurrido un error, por favor inténtelo de nuevo más tarde','error')", true);
                }


            }
        }

        private void GenerateExcelNoEncontrados(List<IXLRow> lstNoEncontrados)
        {
            var excelWorkbook2 = new XLWorkbook();
            var w2 = excelWorkbook2.Worksheets.Add("No Encontrados");
            var cRow = 1;
            var totalCell = 0;

            if (ddlTypeFile.SelectedValue == "1")
                totalCell = Helpers.HeaderAD.Length - 2;
            else
                totalCell = Helpers.HeaderDIS.Length - 2;


            foreach (var row in lstNoEncontrados)
                {
                    for (int i = 1; i <= totalCell; i++)
                        w2.Row(cRow).Cell(i).SetValue(row.Cell(i).Value.ToString());

                    cRow++;
                }
            var myUniqueFileName = $@"{DateTime.Now.Ticks}.xlsx";
            btnNoEncontrados.Visible = true;
            btnNoEncontrados.CommandArgument = myUniqueFileName;

            excelWorkbook2.SaveAs(downloadPath + myUniqueFileName);
        }




        private static async Task<bool> FindPerfil(SEPSEntities seps, string SysTranType, string teds_id)
        {
            if (SysTranType != "D")
            {
                var transaction = await seps.SA_PERFIL.FirstOrDefaultAsync(x => x.TEDS_ID == teds_id);

                if (transaction != null)
                {
                    transaction.FK_ESTATUS_PERFIL_TEDS = 4;
                    transaction.FE_SUBIDO_ACEPTADO_POR_TEDS = DateTime.Now;
                    seps.Entry(transaction).State = EntityState.Modified;
                    return true;
                }
            }
            else
            {
                var transaction = await seps.SA_PERFIL_ELIMINADO.FirstOrDefaultAsync(x => x.TEDS_ID == teds_id);

                if (transaction != null)
                {
                    
                    transaction.FK_ESTATUS_PERFIL_TEDS = 4;
                    transaction.FE_SUBIDO_ACEPTADO_POR_TEDS = DateTime.Now;
                    seps.Entry(transaction).State = EntityState.Modified;
                    return true;

                }
            }
           
            return false;
        }

        protected void btnNoEncontrados2_Click(object sender, EventArgs e)
        {
            if (btnNoEncontrados.CommandArgument != null)
            {
                var filename = btnNoEncontrados.CommandArgument.ToString();
                //Path of the File to be downloaded.
                string filePath = downloadPath+ filename;

                //Content Type and Header.
                Response.ContentType = "application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);

                //Writing the File to Response Stream.
                Response.WriteFile(filePath);

                //Flushing the Response.
                Response.Flush();
                Response.End();
            }

        }

  
    }
}