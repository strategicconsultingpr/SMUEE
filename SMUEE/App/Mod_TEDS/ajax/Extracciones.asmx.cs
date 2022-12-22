using ClosedXML.Excel;
using Microsoft.Reporting.WebForms;
using SMUEE.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using ClosedXML.Excel;
using System.IO;
using System.Data;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using Microsoft.Ajax.Utilities;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Web.UI;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace SMUEE.App.Mod_TEDS.ajax
{
    /// <summary>
    /// Summary description for Extracciones
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Extracciones : System.Web.Services.WebService
    {
        [WebMethod]
        public List<VW_HISTORIAL> GetHistory()
        {
            using (var smuee = new SMUEEEntities())
            {
                return smuee.VW_HISTORIAL.Where(x => x.FK_Modulo == "TEDS").OrderByDescending(x => x.FE_Historial).Take(50).ToList();


            }
        }

        [WebMethod(EnableSession = true)]
        public PassParameter GenerateExcel(string file, DateTime min, DateTime max, int[] programs)
        {
            var sesionSMUEE = HttpContext.Current.Session["PK_Sesion"].ToString();
            var pass = new PassParameter();
            var maxrow = Convert.ToInt32(ConfigurationManager.AppSettings["TEDSMaxRowExcel"].ToString());
            ;

            try
            {

                


                string[] headerAD = Helpers.HeaderAD;
                string[] headerDIS = Helpers.HeaderDIS;
                 

                var str = "";
                for (int i = 0; i < programs.Length; i++)
                {
                    str += programs[i].ToString() + ",";
                }


                List<TMP_SC_VW_RPT_TEDS_MH_AD> mh_ad = new List<TMP_SC_VW_RPT_TEDS_MH_AD>();
                List<TMP_SC_VW_RPT_TEDS_MH_DIS> mh_dis = new List<TMP_SC_VW_RPT_TEDS_MH_DIS>();
                List<TMP_SC_VW_RPT_TEDS_SA_AD> sa_ad = new List<TMP_SC_VW_RPT_TEDS_SA_AD>();
                List<TMP_SC_VW_RPT_TEDS_SA_DIS> sa_dis = new List<TMP_SC_VW_RPT_TEDS_SA_DIS>();


                using (var seps = new SEPSEntities())
                {
                    if (!string.IsNullOrEmpty(file))
                    {
                        switch (file)
                        {
                            case "MHAD":
                                seps.SC_VW_RPT_TEDS_MH_AD_SMUEE(min, max, str);
                                mh_ad = seps.TMP_SC_VW_RPT_TEDS_MH_AD.ToList();

                                break;
                            case "MHDIS":
                                seps.SC_VW_RPT_TEDS_MH_DIS_SMUEE(min, max, str);
                                mh_dis = seps.TMP_SC_VW_RPT_TEDS_MH_DIS.ToList();

                                break;
                            case "SAAD":
                                seps.SC_VW_RPT_TEDS_SA_AD_SMUEE(min, max, str);
                                sa_ad = seps.TMP_SC_VW_RPT_TEDS_SA_AD.ToList();

                                break;
                            case "SADIS":
                                seps.SC_VW_RPT_TEDS_SA_DIS_SMUEE(min, max, str);
                                sa_dis = seps.TMP_SC_VW_RPT_TEDS_SA_DIS.ToList();

                                break;


                            case "DDIS":
                                seps.SC_VW_RPT_TEDS_DELETE_DIS_SMUEE(min, max, str);
                                sa_dis = seps.TMP_SC_VW_RPT_TEDS_SA_DIS.ToList();
                                break;
                            case "DAD":
                                seps.SC_VW_RPT_TEDS_DELETE_AD_SMUEE(min, max, str);
                                sa_ad = seps.TMP_SC_VW_RPT_TEDS_SA_AD.ToList();


                                break;
                            case "AD":
                                seps.SC_VW_RPT_TEDS_SA_AD_SMUEE(min, max, str);
                                sa_ad = seps.TMP_SC_VW_RPT_TEDS_SA_AD.ToList();
                                seps.SC_VW_RPT_TEDS_MH_AD_SMUEE(min, max, str);
                                mh_ad = seps.TMP_SC_VW_RPT_TEDS_MH_AD.ToList();


                                break;
                            case "DIS":
                                seps.SC_VW_RPT_TEDS_SA_DIS_SMUEE(min, max, str);
                                sa_dis = seps.TMP_SC_VW_RPT_TEDS_SA_DIS.ToList();
                                seps.SC_VW_RPT_TEDS_MH_DIS_SMUEE(min, max, str);
                                mh_dis = seps.TMP_SC_VW_RPT_TEDS_MH_DIS.ToList();

                                break;
                        };
                    }
                    else
                        return null;

                }


                using (XLWorkbook wb = new XLWorkbook())
                {


                    var ws = wb.Worksheets.Add();


                    var countTrans = 2;


                    if (file == "MHAD" || file == "SAAD" || file == "DAD" || file == "AD")
                        Header(ws, headerAD);
                    else
                        Header(ws, headerDIS);


                    switch (file)
                    {
                        case "MHAD":
                            if (mh_ad.Count > maxrow)
                            {
                                pass.MaxRowReached(maxrow, mh_ad.Count);
                                return pass;
                            }
                            else
                            {
                                if (mh_ad.Count > 0)
                                {

                                    foreach (var trans in mh_ad)
                                    {
                                        {
                                            ws.Row(countTrans).SetDataType(XLDataType.Text);

                                            //SysTranType
                                            ws.Cell(countTrans, 1).SetValue(trans.System_Transaction_Type);

                                            //StateCode
                                            ws.Cell(countTrans, 2).SetValue(trans.State_Code);


                                            //ReportDate
                                            ws.Cell(countTrans, 3).SetValue(trans.Reporting_Date);

                                            //ProviderID
                                            ws.Cell(countTrans, 4).SetValue(trans.Provider_Identifier);

                                            //ClientID
                                            ws.Cell(countTrans, 5).SetValue(trans.Client_Identifier);

                                            //CoDep
                                            ws.Cell(countTrans, 6).SetValue(trans.Co_Dependant);

                                            //ClientTransType
                                            ws.Cell(countTrans, 7).SetValue(trans.Client_Transaction_Type);

                                            //DateAdmission
                                            ws.Cell(countTrans, 8).SetValue(trans.Date_of_Admision);

                                            //Services
                                            ws.Cell(countTrans, 9).SetValue(trans.Type_of_Service);

                                            //NumPriorTreat
                                            ws.Cell(countTrans, 10).SetValue(trans.Num_of_Prior_Treat_Episode);

                                            //PrinSrcRef
                                            ws.Cell(countTrans, 11).SetValue(trans.Principal_Source_of_Referral);

                                            //DateBirth
                                            ws.Cell(countTrans, 12).SetValue(trans.Date_of_Birth);

                                            //Gender
                                            ws.Cell(countTrans, 13).SetValue(trans.Sex);

                                            //Race
                                            ws.Cell(countTrans, 14).SetValue(trans.Race);

                                            //Ethnicity
                                            ws.Cell(countTrans, 15).SetValue(trans.Ethnicity);

                                            //Education
                                            ws.Cell(countTrans, 16).SetValue(trans.Education);

                                            //EmpStat
                                            ws.Cell(countTrans, 17).SetValue(trans.Employment_Status);

                                            //SubProb1
                                            ws.Cell(countTrans, 18).SetValue(trans.Substance_Pro_Code_Primary);

                                            //RteAdmin1
                                            ws.Cell(countTrans, 19).SetValue(trans.Usual_Route_of_Admin_Primary);

                                            //FreqUse1
                                            ws.Cell(countTrans, 20).SetValue(trans.Frecuency_of_Use_Primary);

                                            //AgeFirstUse1
                                            ws.Cell(countTrans, 21).SetValue(trans.Age_of_First_Use_Primary);

                                            //SubProb2
                                            ws.Cell(countTrans, 22).SetValue(trans.Substance_Pro_Code_Secondary);

                                            //RteAdmin2
                                            ws.Cell(countTrans, 23).SetValue(trans.Usual_Route_of_Admin_Secondary);

                                            //FreqUse2
                                            ws.Cell(countTrans, 24).SetValue(trans.Frecuency_of_Use_Secondary);

                                            //AgeFirstUse2
                                            ws.Cell(countTrans, 25).SetValue(trans.Age_of_First_Use_Secondary);

                                            //SubProb3
                                            ws.Cell(countTrans, 26).SetValue(trans.Substance_Pro_Code_Tertiary);

                                            //RteAdmin3
                                            ws.Cell(countTrans, 27).SetValue(trans.Usual_Route_of_Admin_Tertiary);

                                            //FreqUse3
                                            ws.Cell(countTrans, 28).SetValue(trans.Frecuency_of_Use_Tertiary);



                                            //AgeFirstUse3
                                            ws.Cell(countTrans, 29).SetValue(trans.Age_of_First_Use_Tertiary);

                                            //OpiodTherapy
                                            ws.Cell(countTrans, 30).SetValue(trans.Opioid_Replacement_Therapy);

                                            //DetailedDrug1
                                            ws.Cell(countTrans, 31).SetValue(trans.Detailed_Drug_Code_Primary);

                                            //DetailedDrug2
                                            ws.Cell(countTrans, 32).SetValue(trans.Detailed_Drug_Code_Secondary);

                                            //DetailedDrug3
                                            ws.Cell(countTrans, 33).SetValue(trans.Detailed_Drug_Code_Tertiary);

                                            //DSMIIIRCriteria
                                            ws.Cell(countTrans, 34).SetValue(trans.DSM_Diagnosis);

                                            //CoOccurringSAMH
                                            ws.Cell(countTrans, 35).SetValue(trans.Co_occuring_Substance_Abuse_and_Mental_Health_Problems);

                                            //Pregnant
                                            ws.Cell(countTrans, 36).SetValue(trans.Pregnant_at_Time_of_Admision);

                                            //Veteran
                                            ws.Cell(countTrans, 37).SetValue(trans.Veteran_Status);

                                            //LivingArrange
                                            ws.Cell(countTrans, 38).SetValue(trans.Living_Arrangements);

                                            //PrimSrcInc
                                            ws.Cell(countTrans, 39).SetValue(trans.Source_of_Income_Support);

                                            //HealthIns
                                            ws.Cell(countTrans, 40).SetValue(trans.Health_Insurance);

                                            //PrimSrcPay
                                            ws.Cell(countTrans, 41).SetValue(trans.Expected_Actual_Primary_Source_of_Payment);

                                            //DetNLF
                                            ws.Cell(countTrans, 42).SetValue(trans.Detailed_Not_In_Labor_Force);

                                            //DetCriminal
                                            ws.Cell(countTrans, 43).SetValue(trans.Detailed_Criminal_Justice_Referral);

                                            //MaritalStat
                                            ws.Cell(countTrans, 44).SetValue(trans.Maritial_Status);

                                            //DaysWaitTreat
                                            ws.Cell(countTrans, 45).SetValue(trans.Days_Waiting_to_Enter_Treatment);

                                            //Arrests
                                            ws.Cell(countTrans, 46).SetValue(trans.Number_of_Arrests_in_30_Days_Prior_Adm);

                                            //AtndSelfHelp
                                            ws.Cell(countTrans, 47).SetValue(trans.Frequency_of_Attendance);

                                            //DiagType
                                            ws.Cell(countTrans, 48).SetValue(trans.Diagnostic_Code_Set_Identifier);

                                            //SADiagnosis
                                            ws.Cell(countTrans, 49).SetValue(trans.Substance_Abuse_Diagnostic_ICD_10);

                                            //MHDiagnosis1
                                            ws.Cell(countTrans, 50).SetValue(trans.MH_Diag_Code_Primary_ICD_10);

                                            //MHDiagnosis2
                                            ws.Cell(countTrans, 51).SetValue(trans.MH_Diag_Code_Secondary_ICD_10);

                                            //MHDiagnosis3
                                            ws.Cell(countTrans, 52).SetValue(trans.MH_Diag_Code_Tertiary_ICD_10);

                                            //SMISEDStat
                                            ws.Cell(countTrans, 53).SetValue(trans.Smi_Sed_Status);

                                            //SchoolAtndStat
                                            ws.Cell(countTrans, 54).SetValue(trans.School_Attendance_Status);

                                            //LegalStat
                                            ws.Cell(countTrans, 55).SetValue(trans.Legal_Status_at_Admission_State_Hospital);

                                            //GlobalAssess
                                            ws.Cell(countTrans, 56).SetValue(trans.Escala_GAF);

                                            //Perfil
                                            ws.Cell(countTrans, 57).SetValue(trans.Perfil);

                                            //Episodio
                                            ws.Cell(countTrans, 58).SetValue(trans.Episode);

                                            countTrans++;
                                        }
                                    }
                                }
                            }
                            break;
                        case "MHDIS":
                            if (mh_dis.Count > maxrow)
                            {
                                pass.MaxRowReached(maxrow, mh_dis.Count);
                                return pass;
                            }
                            else
                            {

                                if (mh_dis.Count > 0)
                                {
                                    foreach (var trans in mh_dis)
                                    {

                                        ws.Row(countTrans).SetDataType(XLDataType.Text);

                                        //SysTransTyp
                                        ws.Cell(countTrans, 1).SetValue(trans.System_Transaction_Type);

                                        //StateCode
                                        ws.Cell(countTrans, 2).SetValue(trans.State_Code);

                                        //ReportDate
                                        ws.Cell(countTrans, 3).SetValue(trans.Reporting_Date);

                                        //ProviderID
                                        ws.Cell(countTrans, 4).SetValue(trans.Provider_Identifier);

                                        //ClientID
                                        ws.Cell(countTrans, 5).SetValue(trans.Client_Identifier);

                                        //CoDep
                                        ws.Cell(countTrans, 6).SetValue(trans.Co_Dependant);

                                        //Services
                                        ws.Cell(countTrans, 7).SetValue(trans.Type_of_Service);

                                        //DateLastContact
                                        ws.Cell(countTrans, 8).SetValue(trans.Date_Last_Contact);

                                        //DateDischarge
                                        ws.Cell(countTrans, 9).SetValue(trans.Date_of_Discharge);

                                        //RsnDischarge
                                        ws.Cell(countTrans, 10).SetValue(trans.Reason_for_Discharge);

                                        //AdmProviderID
                                        ws.Cell(countTrans, 11).SetValue(trans.Provider_ID_at_Admission);

                                        //AdmClientID
                                        ws.Cell(countTrans, 12).SetValue(trans.Client_ID_at_Admission);

                                        //AdmCoDep
                                        ws.Cell(countTrans, 13).SetValue(trans.Co_dependent_Collateral);

                                        //AdmClientTransTyp
                                        ws.Cell(countTrans, 14).SetValue(trans.Client_Transaction_Type_AD);

                                        //DateAdmission
                                        ws.Cell(countTrans, 15).SetValue(trans.Date_of_Admision);

                                        //AdmServices
                                        ws.Cell(countTrans, 16).SetValue(trans.Type_of_Service_AD);

                                        //DateBirth
                                        ws.Cell(countTrans, 17).SetValue(trans.Date_of_Birth_at_AD);

                                        //Gender
                                        ws.Cell(countTrans, 18).SetValue(trans.Sex_at_AD);

                                        //Race
                                        ws.Cell(countTrans, 19).SetValue(trans.Race_at_AD);

                                        //Ethnicity
                                        ws.Cell(countTrans, 20).SetValue(trans.Ethnicity_at_AD);

                                        //SubProb1
                                        ws.Cell(countTrans, 21).SetValue(trans.Substance_Pro_Code_Primary);

                                        //SubProb2
                                        ws.Cell(countTrans, 22).SetValue(trans.Substance_Pro_Code_Secondary);

                                        //SubProb3
                                        ws.Cell(countTrans, 23).SetValue(trans.Substance_Pro_Code_Tertiary);

                                        //FreqUse1
                                        ws.Cell(countTrans, 24).SetValue(trans.Frecuency_of_Use_Primary);

                                        //FreqUse2
                                        ws.Cell(countTrans, 25).SetValue(trans.Frecuency_of_Use_Secondary);

                                        //FreqUse3
                                        ws.Cell(countTrans, 26).SetValue(trans.Frecuency_of_Use_Tertiary);

                                        //LivingArrange
                                        ws.Cell(countTrans, 27).SetValue(trans.Living_Arrangements);

                                        //EmpStat
                                        ws.Cell(countTrans, 28).SetValue(trans.Employment_Status);

                                        //DetNLF
                                        ws.Cell(countTrans, 29).SetValue(trans.Detailed_Not_In_Labor_Force);

                                        //Arrests
                                        ws.Cell(countTrans, 30).SetValue(trans.Number_of_Arrests_in_30_Days_Prior_Adm);

                                        //AtndSelfHelp
                                        ws.Cell(countTrans, 31).SetValue(trans.Frequency_of_Attendance);

                                        //ClientTransType
                                        ws.Cell(countTrans, 32).SetValue(trans.Client_Transaction_Type);

                                        //DiagType
                                        ws.Cell(countTrans, 33).SetValue(trans.Diagnostic_Code_Set_Identifier);

                                        //MHDiagnosis1
                                        ws.Cell(countTrans, 34).SetValue(trans.MH_Diag_Code_Primary_ICD_10);

                                        //MHDiagnosis2
                                        ws.Cell(countTrans, 35).SetValue(trans.MH_Diag_Code_Secondary_ICD_10);

                                        //MHDiagnosis3
                                        ws.Cell(countTrans, 36).SetValue(trans.MH_Diag_Code_Tertiary_ICD_10);

                                        //SMISEDStat
                                        ws.Cell(countTrans, 37).SetValue(trans.Smi_Sed_Status);

                                        //SchoolAtndStat
                                        ws.Cell(countTrans, 38).SetValue(trans.School_Attendance_Status);

                                        //Education
                                        ws.Cell(countTrans, 39).SetValue(trans.Education);

                                        //GlobalAssess
                                        ws.Cell(countTrans, 40).SetValue(trans.Escala_GAF);

                                        //Perfil
                                        ws.Cell(countTrans, 41).SetValue(trans.Perfil);

                                        //Episodio
                                        ws.Cell(countTrans, 42).SetValue(trans.Episode);
                                        countTrans++;

                                    }


                                }
                            }
                            break;
                        case "SAAD":
                            if (sa_ad.Count > maxrow)
                            {
                                pass.MaxRowReached(maxrow, sa_ad.Count);
                                return pass;
                            }
                            else
                            {
                                if (sa_ad.Count > 0)
                                {

                                    foreach (var trans in sa_ad)
                                    {
                                        {
                                            ws.Row(countTrans).SetDataType(XLDataType.Text);

                                            //SysTranType
                                            ws.Cell(countTrans, 1).SetValue(trans.System_Transaction_Type);

                                            //StateCode
                                            ws.Cell(countTrans, 2).SetValue(trans.State_Code);

                                            //ReportDate
                                            ws.Cell(countTrans, 3).SetValue(trans.Reporting_Date);

                                            //ProviderID
                                            ws.Cell(countTrans, 4).SetValue(trans.Provider_Identifier);

                                            //ClientID
                                            ws.Cell(countTrans, 5).SetValue(trans.Client_Identifier);

                                            //CoDep
                                            ws.Cell(countTrans, 6).SetValue(trans.Co_Dependant);

                                            //ClientTransType
                                            ws.Cell(countTrans, 7).SetValue(trans.Client_Transaction_Type);

                                            //DateAdmission
                                            ws.Cell(countTrans, 8).SetValue(trans.Date_of_Admision);

                                            //Services
                                            ws.Cell(countTrans, 9).SetValue(trans.Type_of_Service);

                                            //NumPriorTreat
                                            ws.Cell(countTrans, 10).SetValue(trans.Num_of_Prior_Treat_Episode);

                                            //PrinSrcRef
                                            ws.Cell(countTrans, 11).SetValue(trans.Principal_Source_of_Referral);

                                            //DateBirth
                                            ws.Cell(countTrans, 12).SetValue(trans.Date_of_Birth);

                                            //Gender
                                            ws.Cell(countTrans, 13).SetValue(trans.Sex);

                                            //Race
                                            ws.Cell(countTrans, 14).SetValue(trans.Race);

                                            //Ethnicity
                                            ws.Cell(countTrans, 15).SetValue(trans.Ethnicity);

                                            //Education
                                            ws.Cell(countTrans, 16).SetValue(trans.Education);

                                            //EmpStat
                                            ws.Cell(countTrans, 17).SetValue(trans.Employment_Status);

                                            //SubProb1
                                            ws.Cell(countTrans, 18).SetValue(trans.Substance_Pro_Code_Primary);

                                            //RteAdmin1
                                            ws.Cell(countTrans, 19).SetValue(trans.Usual_Route_of_Admin_Primary);

                                            //FreqUse1
                                            ws.Cell(countTrans, 20).SetValue(trans.Frecuency_of_Use_Primary);

                                            //AgeFirstUse1
                                            ws.Cell(countTrans, 21).SetValue(trans.Age_of_First_Use_Primary);

                                            //SubProb2
                                            ws.Cell(countTrans, 22).SetValue(trans.Substance_Pro_Code_Secondary);

                                            //RteAdmin2
                                            ws.Cell(countTrans, 23).SetValue(trans.Usual_Route_of_Admin_Secondary);

                                            //FreqUse2
                                            ws.Cell(countTrans, 24).SetValue(trans.Frecuency_of_Use_Secondary);

                                            //AgeFirstUse2
                                            ws.Cell(countTrans, 25).SetValue(trans.Age_of_First_Use_Secondary);

                                            //SubProb3
                                            ws.Cell(countTrans, 26).SetValue(trans.Substance_Pro_Code_Tertiary);

                                            //RteAdmin3
                                            ws.Cell(countTrans, 27).SetValue(trans.Usual_Route_of_Admin_Tertiary);

                                            //FreqUse3
                                            ws.Cell(countTrans, 28).SetValue(trans.Frecuency_of_Use_Tertiary);

                                            //AgeFirstUse3
                                            ws.Cell(countTrans, 29).SetValue(trans.Age_of_First_Use_Tertiary);

                                            //OpiodTherapy
                                            ws.Cell(countTrans, 30).SetValue(trans.Opioid_Replacement_Therapy);

                                            //DetailedDrug1
                                            ws.Cell(countTrans, 31).SetValue(trans.Detailed_Drug_Code_Primary);

                                            //DetailedDrug2
                                            ws.Cell(countTrans, 32).SetValue(trans.Detailed_Drug_Code_Secondary);

                                            //DetailedDrug3
                                            ws.Cell(countTrans, 33).SetValue(trans.Detailed_Drug_Code_Tertiary);

                                            //DSMIIIRCriteria
                                            ws.Cell(countTrans, 34).SetValue(trans.DSM_Diagnosis);

                                            //CoOccurringSAMH
                                            ws.Cell(countTrans, 35).SetValue(trans.Co_occuring_Substance_Abuse_and_Mental_Health_Problems);

                                            //Pregnant
                                            ws.Cell(countTrans, 36).SetValue(trans.Pregnant_at_Time_of_Admision);

                                            //Veteran
                                            ws.Cell(countTrans, 37).SetValue(trans.Veteran_Status);

                                            //LivingArrange
                                            ws.Cell(countTrans, 38).SetValue(trans.Living_Arrangements);

                                            //PrimSrcInc
                                            ws.Cell(countTrans, 39).SetValue(trans.Source_of_Income_Support);

                                            //HealthIns
                                            ws.Cell(countTrans, 40).SetValue(trans.Health_Insurance);

                                            //PrimSrcPay
                                            ws.Cell(countTrans, 41).SetValue(trans.Expected_Actual_Primary_Source_of_Payment);

                                            //DetNLF
                                            ws.Cell(countTrans, 42).SetValue(trans.Detailed_Not_In_Labor_Force);

                                            //DetCriminal
                                            ws.Cell(countTrans, 43).SetValue(trans.Detailed_Criminal_Justice_Referral);

                                            //MaritalStat
                                            ws.Cell(countTrans, 44).SetValue(trans.Maritial_Status);

                                            //DaysWaitTreat
                                            ws.Cell(countTrans, 45).SetValue(trans.Days_Waiting_to_Enter_Treatment);

                                            //Arrests
                                            ws.Cell(countTrans, 46).SetValue(trans.Number_of_Arrests_in_30_Days_Prior_Adm);

                                            //AtndSelfHelp
                                            ws.Cell(countTrans, 47).SetValue(trans.Frequency_of_Attendance);

                                            //DiagType
                                            ws.Cell(countTrans, 48).SetValue(trans.Diagnostic_Code_Set_Identifier);

                                            //SADiagnosis
                                            ws.Cell(countTrans, 49).SetValue(trans.Substance_Abuse_Diagnostic_ICD_10);

                                            //MHDiagnosis1
                                            ws.Cell(countTrans, 50).SetValue(trans.MH_Diag_Code_Primary_ICD_10);

                                            //MHDiagnosis2
                                            ws.Cell(countTrans, 51).SetValue(trans.MH_Diag_Code_Secondary_ICD_10);

                                            //MHDiagnosis3
                                            ws.Cell(countTrans, 52).SetValue(trans.MH_Diag_Code_Tertiary_ICD_10);

                                            //SMISEDStat
                                            ws.Cell(countTrans, 53).SetValue(trans.Smi_Sed_Status);

                                            //SchoolAtndStat
                                            ws.Cell(countTrans, 54).SetValue(trans.School_Attendance_Status);

                                            //LegalStat
                                            ws.Cell(countTrans, 55).SetValue(trans.Legal_Status_at_Admission_State_Hospital);

                                            //GlobalAssess
                                            ws.Cell(countTrans, 56).SetValue(trans.Escala_GAF);

                                            //Perfil
                                            ws.Cell(countTrans, 57).SetValue(trans.Perfil);

                                            //Episodio
                                            ws.Cell(countTrans, 58).SetValue(trans.Episode);

                                            countTrans++;
                                        }
                                    }
                                }

                            }
                            break;
                        case "SADIS":
                            if (sa_dis.Count > maxrow)
                            {
                                pass.MaxRowReached(maxrow, sa_dis.Count);
                                return pass;
                            }
                            else
                            {
                                if (sa_dis.Count > 0)
                                {
                                    foreach (var trans in sa_dis)
                                    {
                                        ws.Row(countTrans).SetDataType(XLDataType.Text);

                                        //SysTransTyp
                                        ws.Cell(countTrans, 1).SetValue(trans.System_Transaction_Type);

                                        //StateCode
                                        ws.Cell(countTrans, 2).SetValue(trans.State_Code);

                                        //ReportDate
                                        ws.Cell(countTrans, 3).SetValue(trans.Reporting_Date);

                                        //ProviderID
                                        ws.Cell(countTrans, 4).SetValue(trans.Provider_Identifier);

                                        //ClientID
                                        ws.Cell(countTrans, 5).SetValue(trans.Client_Identifier);

                                        //CoDep
                                        ws.Cell(countTrans, 6).SetValue(trans.Co_Dependant);

                                        //Services
                                        ws.Cell(countTrans, 7).SetValue(trans.Type_of_Service);

                                        //DateLastContact
                                        ws.Cell(countTrans, 8).SetValue(trans.Date_Last_Contact);

                                        //DateDischarge
                                        ws.Cell(countTrans, 9).SetValue(trans.Date_of_Discharge);

                                        //RsnDischarge
                                        ws.Cell(countTrans, 10).SetValue(trans.Reason_for_Discharge);

                                        //AdmProviderID
                                        ws.Cell(countTrans, 11).SetValue(trans.Provider_ID_at_Admission);

                                        //AdmClientID
                                        ws.Cell(countTrans, 12).SetValue(trans.Client_ID_at_Admission);

                                        //AdmCoDep
                                        ws.Cell(countTrans, 13).SetValue(trans.Co_dependent_Collateral);

                                        //AdmClientTransTyp
                                        ws.Cell(countTrans, 14).SetValue(trans.Client_Transaction_Type_AD);

                                        //DateAdmission
                                        ws.Cell(countTrans, 15).SetValue(trans.Date_of_Admision);

                                        //AdmServices
                                        ws.Cell(countTrans, 16).SetValue(trans.Type_of_Service_AD);

                                        //DateBirth
                                        ws.Cell(countTrans, 17).SetValue(trans.Date_of_Birth_at_AD);

                                        //Gender
                                        ws.Cell(countTrans, 18).SetValue(trans.Sex_at_AD);

                                        //Race
                                        ws.Cell(countTrans, 19).SetValue(trans.Race_at_AD);

                                        //Ethnicity
                                        ws.Cell(countTrans, 20).SetValue(trans.Ethnicity_at_AD);

                                        //SubProb1
                                        ws.Cell(countTrans, 21).SetValue(trans.Substance_Pro_Code_Primary);

                                        //SubProb2
                                        ws.Cell(countTrans, 22).SetValue(trans.Substance_Pro_Code_Secondary);

                                        //SubProb3
                                        ws.Cell(countTrans, 23).SetValue(trans.Substance_Pro_Code_Tertiary);

                                        //FreqUse1
                                        ws.Cell(countTrans, 24).SetValue(trans.Frecuency_of_Use_Primary);

                                        //FreqUse2
                                        ws.Cell(countTrans, 25).SetValue(trans.Frecuency_of_Use_Secondary);

                                        //FreqUse3
                                        ws.Cell(countTrans, 26).SetValue(trans.Frecuency_of_Use_Tertiary);

                                        //LivingArrange
                                        ws.Cell(countTrans, 27).SetValue(trans.Living_Arrangements);

                                        //EmpStat
                                        ws.Cell(countTrans, 28).SetValue(trans.Employment_Status);

                                        //DetNLF
                                        ws.Cell(countTrans, 29).SetValue(trans.Detailed_Not_In_Labor_Force);

                                        //Arrests
                                        ws.Cell(countTrans, 30).SetValue(trans.Number_of_Arrests_in_30_Days_Prior_Adm);

                                        //AtndSelfHelp
                                        ws.Cell(countTrans, 31).SetValue(trans.Frequency_of_Attendance);

                                        //ClientTransType
                                        ws.Cell(countTrans, 32).SetValue(trans.Client_Transaction_Type);

                                        //DiagType
                                        ws.Cell(countTrans, 33).SetValue(trans.Diagnostic_Code_Set_Identifier);

                                        //MHDiagnosis1
                                        ws.Cell(countTrans, 34).SetValue(trans.MH_Diag_Code_Primary_ICD_10);

                                        //MHDiagnosis2
                                        ws.Cell(countTrans, 35).SetValue(trans.MH_Diag_Code_Secondary_ICD_10);

                                        //MHDiagnosis3
                                        ws.Cell(countTrans, 36).SetValue(trans.MH_Diag_Code_Tertiary_ICD_10);

                                        //SMISEDStat
                                        ws.Cell(countTrans, 37).SetValue(trans.Smi_Sed_Status);

                                        //SchoolAtndStat
                                        ws.Cell(countTrans, 38).SetValue(trans.School_Attendance_Status);

                                        //Education
                                        ws.Cell(countTrans, 39).SetValue(trans.Education);

                                        //GlobalAssess
                                        ws.Cell(countTrans, 40).SetValue(trans.Escala_GAF);

                                        //Perfil
                                        ws.Cell(countTrans, 41).SetValue(trans.Perfil);

                                        //Episodio
                                        ws.Cell(countTrans, 42).SetValue(trans.Episode);
                                        countTrans++;

                                    }


                                }

                            }
                            break;
                        case "DDIS":
                            file += $" Paso 1 Eliminar Altas "; 
                          
                            if (sa_dis.Count > maxrow)
                            {
                                pass.MaxRowReached(maxrow, sa_dis.Count);
                                return pass;
                            }
                            else
                            {
                                if (sa_dis.Count > 0)
                                {
                                    foreach (var trans in sa_dis)
                                    {
                                        ws.Row(countTrans).SetDataType(XLDataType.Text);

                                        //SysTransTyp
                                        ws.Cell(countTrans, 1).SetValue(trans.System_Transaction_Type);

                                        //StateCode
                                        ws.Cell(countTrans, 2).SetValue(trans.State_Code);

                                        //ReportDate
                                        ws.Cell(countTrans, 3).SetValue(trans.Reporting_Date);

                                        //ProviderID
                                        ws.Cell(countTrans, 4).SetValue(trans.Provider_Identifier);

                                        //ClientID
                                        ws.Cell(countTrans, 5).SetValue(trans.Client_Identifier);

                                        //CoDep
                                        ws.Cell(countTrans, 6).SetValue(trans.Co_Dependant);

                                        //Services
                                        ws.Cell(countTrans, 7).SetValue(trans.Type_of_Service);

                                        //DateLastContact
                                        ws.Cell(countTrans, 8).SetValue(trans.Date_Last_Contact);

                                        //DateDischarge
                                        ws.Cell(countTrans, 9).SetValue(trans.Date_of_Discharge);

                                        //DateAdmission
                                        ws.Cell(countTrans, 15).SetValue(trans.Date_of_Admision);
                                       
                                        //client TransType
                                        ws.Cell(countTrans, 32).SetValue(trans.Client_Transaction_Type);

                                        //Perfil
                                        ws.Cell(countTrans, 41).SetValue(trans.Perfil);

                                        //Episodio
                                        ws.Cell(countTrans, 42).SetValue(trans.Episode);
                                        countTrans++;

                                    }


                                }

                            }
                            break;
                        case "DAD":

                            file += $" Paso 2 Eliminar Admisiones ";

                            if (sa_ad.Count > maxrow)
                            {
                                pass.MaxRowReached(maxrow, sa_ad.Count);
                                return pass;
                            }
                            else
                            {
                                if (sa_ad.Count > 0)
                                {

                                    foreach (var trans in sa_ad)
                                    {
                                        {
                                            ws.Row(countTrans).SetDataType(XLDataType.Text);

                                            //SysTranType
                                            ws.Cell(countTrans, 1).SetValue(trans.System_Transaction_Type);

                                            //StateCode
                                            ws.Cell(countTrans, 2).SetValue(trans.State_Code);

                                            //ReportDate
                                            ws.Cell(countTrans, 3).SetValue(trans.Reporting_Date);

                                            //ProviderID
                                            ws.Cell(countTrans, 4).SetValue(trans.Provider_Identifier);

                                            //ClientID
                                            ws.Cell(countTrans, 5).SetValue(trans.Client_Identifier);

                                            //CoDep
                                            ws.Cell(countTrans, 6).SetValue(trans.Co_Dependant);

                                            //ClientTransType
                                            ws.Cell(countTrans, 7).SetValue(trans.Client_Transaction_Type);

                                            //DateAdmission
                                            ws.Cell(countTrans, 8).SetValue(trans.Date_of_Admision);

                                            //Services
                                            ws.Cell(countTrans, 9).SetValue(trans.Type_of_Service);


                                            //Perfil
                                            ws.Cell(countTrans, 57).SetValue(trans.Perfil);

                                            //Episodio
                                            ws.Cell(countTrans, 58).SetValue(trans.Episode);

                                            countTrans++;
                                        }
                                    }
                                }

                            }

                            break;
                        case "AD":

                            file += $" Paso 3 Agregar Admisiones ";


                            var countMH = mh_ad.Count;
                            var countSA = sa_ad.Count;

                            if (countMH + countSA > maxrow)
                            {
                                pass.MaxRowReached(maxrow, countMH + countSA);
                                return pass;
                            }
                            else
                            {

                                if (mh_ad.Count > 0)
                                {

                                    foreach (var trans in mh_ad)
                                    {
                                        {
                                            ws.Row(countTrans).SetDataType(XLDataType.Text);

                                            //SysTranType
                                            ws.Cell(countTrans, 1).SetValue(trans.System_Transaction_Type);

                                            //StateCode
                                            ws.Cell(countTrans, 2).SetValue(trans.State_Code);


                                            //ReportDate
                                            ws.Cell(countTrans, 3).SetValue(trans.Reporting_Date);

                                            //ProviderID
                                            ws.Cell(countTrans, 4).SetValue(trans.Provider_Identifier);

                                            //ClientID
                                            ws.Cell(countTrans, 5).SetValue(trans.Client_Identifier);

                                            //CoDep
                                            ws.Cell(countTrans, 6).SetValue(trans.Co_Dependant);

                                            //ClientTransType
                                            ws.Cell(countTrans, 7).SetValue(trans.Client_Transaction_Type);

                                            //DateAdmission
                                            ws.Cell(countTrans, 8).SetValue(trans.Date_of_Admision);

                                            //Services
                                            ws.Cell(countTrans, 9).SetValue(trans.Type_of_Service);

                                            //NumPriorTreat
                                            ws.Cell(countTrans, 10).SetValue(trans.Num_of_Prior_Treat_Episode);

                                            //PrinSrcRef
                                            ws.Cell(countTrans, 11).SetValue(trans.Principal_Source_of_Referral);

                                            //DateBirth
                                            ws.Cell(countTrans, 12).SetValue(trans.Date_of_Birth);

                                            //Gender
                                            ws.Cell(countTrans, 13).SetValue(trans.Sex);

                                            //Race
                                            ws.Cell(countTrans, 14).SetValue(trans.Race);

                                            //Ethnicity
                                            ws.Cell(countTrans, 15).SetValue(trans.Ethnicity);

                                            //Education
                                            ws.Cell(countTrans, 16).SetValue(trans.Education);

                                            //EmpStat
                                            ws.Cell(countTrans, 17).SetValue(trans.Employment_Status);

                                            //SubProb1
                                            ws.Cell(countTrans, 18).SetValue(trans.Substance_Pro_Code_Primary);

                                            //RteAdmin1
                                            ws.Cell(countTrans, 19).SetValue(trans.Usual_Route_of_Admin_Primary);

                                            //FreqUse1
                                            ws.Cell(countTrans, 20).SetValue(trans.Frecuency_of_Use_Primary);

                                            //AgeFirstUse1
                                            ws.Cell(countTrans, 21).SetValue(trans.Age_of_First_Use_Primary);

                                            //SubProb2
                                            ws.Cell(countTrans, 22).SetValue(trans.Substance_Pro_Code_Secondary);

                                            //RteAdmin2
                                            ws.Cell(countTrans, 23).SetValue(trans.Usual_Route_of_Admin_Secondary);

                                            //FreqUse2
                                            ws.Cell(countTrans, 24).SetValue(trans.Frecuency_of_Use_Secondary);

                                            //AgeFirstUse2
                                            ws.Cell(countTrans, 25).SetValue(trans.Age_of_First_Use_Secondary);

                                            //SubProb3
                                            ws.Cell(countTrans, 26).SetValue(trans.Substance_Pro_Code_Tertiary);

                                            //RteAdmin3
                                            ws.Cell(countTrans, 27).SetValue(trans.Usual_Route_of_Admin_Tertiary);

                                            //FreqUse3
                                            ws.Cell(countTrans, 28).SetValue(trans.Frecuency_of_Use_Tertiary);



                                            //AgeFirstUse3
                                            ws.Cell(countTrans, 29).SetValue(trans.Age_of_First_Use_Tertiary);

                                            //OpiodTherapy
                                            ws.Cell(countTrans, 30).SetValue(trans.Opioid_Replacement_Therapy);

                                            //DetailedDrug1
                                            ws.Cell(countTrans, 31).SetValue(trans.Detailed_Drug_Code_Primary);

                                            //DetailedDrug2
                                            ws.Cell(countTrans, 32).SetValue(trans.Detailed_Drug_Code_Secondary);

                                            //DetailedDrug3
                                            ws.Cell(countTrans, 33).SetValue(trans.Detailed_Drug_Code_Tertiary);

                                            //DSMIIIRCriteria
                                            ws.Cell(countTrans, 34).SetValue(trans.DSM_Diagnosis);

                                            //CoOccurringSAMH
                                            ws.Cell(countTrans, 35).SetValue(trans.Co_occuring_Substance_Abuse_and_Mental_Health_Problems);

                                            //Pregnant
                                            ws.Cell(countTrans, 36).SetValue(trans.Pregnant_at_Time_of_Admision);

                                            //Veteran
                                            ws.Cell(countTrans, 37).SetValue(trans.Veteran_Status);

                                            //LivingArrange
                                            ws.Cell(countTrans, 38).SetValue(trans.Living_Arrangements);

                                            //PrimSrcInc
                                            ws.Cell(countTrans, 39).SetValue(trans.Source_of_Income_Support);

                                            //HealthIns
                                            ws.Cell(countTrans, 40).SetValue(trans.Health_Insurance);

                                            //PrimSrcPay
                                            ws.Cell(countTrans, 41).SetValue(trans.Expected_Actual_Primary_Source_of_Payment);

                                            //DetNLF
                                            ws.Cell(countTrans, 42).SetValue(trans.Detailed_Not_In_Labor_Force);

                                            //DetCriminal
                                            ws.Cell(countTrans, 43).SetValue(trans.Detailed_Criminal_Justice_Referral);

                                            //MaritalStat
                                            ws.Cell(countTrans, 44).SetValue(trans.Maritial_Status);

                                            //DaysWaitTreat
                                            ws.Cell(countTrans, 45).SetValue(trans.Days_Waiting_to_Enter_Treatment);

                                            //Arrests
                                            ws.Cell(countTrans, 46).SetValue(trans.Number_of_Arrests_in_30_Days_Prior_Adm);

                                            //AtndSelfHelp
                                            ws.Cell(countTrans, 47).SetValue(trans.Frequency_of_Attendance);

                                            //DiagType
                                            ws.Cell(countTrans, 48).SetValue(trans.Diagnostic_Code_Set_Identifier);

                                            //SADiagnosis
                                            ws.Cell(countTrans, 49).SetValue(trans.Substance_Abuse_Diagnostic_ICD_10);

                                            //MHDiagnosis1
                                            ws.Cell(countTrans, 50).SetValue(trans.MH_Diag_Code_Primary_ICD_10);

                                            //MHDiagnosis2
                                            ws.Cell(countTrans, 51).SetValue(trans.MH_Diag_Code_Secondary_ICD_10);

                                            //MHDiagnosis3
                                            ws.Cell(countTrans, 52).SetValue(trans.MH_Diag_Code_Tertiary_ICD_10);

                                            //SMISEDStat
                                            ws.Cell(countTrans, 53).SetValue(trans.Smi_Sed_Status);

                                            //SchoolAtndStat
                                            ws.Cell(countTrans, 54).SetValue(trans.School_Attendance_Status);

                                            //LegalStat
                                            ws.Cell(countTrans, 55).SetValue(trans.Legal_Status_at_Admission_State_Hospital);

                                            //GlobalAssess
                                            ws.Cell(countTrans, 56).SetValue(trans.Escala_GAF);

                                            //Perfil
                                            ws.Cell(countTrans, 57).SetValue(trans.Perfil);

                                            //Episodio
                                            ws.Cell(countTrans, 58).SetValue(trans.Episode);

                                            countTrans++;
                                        }
                                    }
                                }
                                if (sa_ad.Count > 0)
                                {

                                    foreach (var trans in sa_ad)
                                    {
                                        {
                                            ws.Row(countTrans).SetDataType(XLDataType.Text);

                                            //SysTranType
                                            ws.Cell(countTrans, 1).SetValue(trans.System_Transaction_Type);

                                            //StateCode
                                            ws.Cell(countTrans, 2).SetValue(trans.State_Code);

                                            //ReportDate
                                            ws.Cell(countTrans, 3).SetValue(trans.Reporting_Date);

                                            //ProviderID
                                            ws.Cell(countTrans, 4).SetValue(trans.Provider_Identifier);

                                            //ClientID
                                            ws.Cell(countTrans, 5).SetValue(trans.Client_Identifier);

                                            //CoDep
                                            ws.Cell(countTrans, 6).SetValue(trans.Co_Dependant);

                                            //ClientTransType
                                            ws.Cell(countTrans, 7).SetValue(trans.Client_Transaction_Type);

                                            //DateAdmission
                                            ws.Cell(countTrans, 8).SetValue(trans.Date_of_Admision);

                                            //Services
                                            ws.Cell(countTrans, 9).SetValue(trans.Type_of_Service);

                                            //NumPriorTreat
                                            ws.Cell(countTrans, 10).SetValue(trans.Num_of_Prior_Treat_Episode);

                                            //PrinSrcRef
                                            ws.Cell(countTrans, 11).SetValue(trans.Principal_Source_of_Referral);

                                            //DateBirth
                                            ws.Cell(countTrans, 12).SetValue(trans.Date_of_Birth);

                                            //Gender
                                            ws.Cell(countTrans, 13).SetValue(trans.Sex);

                                            //Race
                                            ws.Cell(countTrans, 14).SetValue(trans.Race);

                                            //Ethnicity
                                            ws.Cell(countTrans, 15).SetValue(trans.Ethnicity);

                                            //Education
                                            ws.Cell(countTrans, 16).SetValue(trans.Education);

                                            //EmpStat
                                            ws.Cell(countTrans, 17).SetValue(trans.Employment_Status);

                                            //SubProb1
                                            ws.Cell(countTrans, 18).SetValue(trans.Substance_Pro_Code_Primary);

                                            //RteAdmin1
                                            ws.Cell(countTrans, 19).SetValue(trans.Usual_Route_of_Admin_Primary);

                                            //FreqUse1
                                            ws.Cell(countTrans, 20).SetValue(trans.Frecuency_of_Use_Primary);

                                            //AgeFirstUse1
                                            ws.Cell(countTrans, 21).SetValue(trans.Age_of_First_Use_Primary);

                                            //SubProb2
                                            ws.Cell(countTrans, 22).SetValue(trans.Substance_Pro_Code_Secondary);

                                            //RteAdmin2
                                            ws.Cell(countTrans, 23).SetValue(trans.Usual_Route_of_Admin_Secondary);

                                            //FreqUse2
                                            ws.Cell(countTrans, 24).SetValue(trans.Frecuency_of_Use_Secondary);

                                            //AgeFirstUse2
                                            ws.Cell(countTrans, 25).SetValue(trans.Age_of_First_Use_Secondary);

                                            //SubProb3
                                            ws.Cell(countTrans, 26).SetValue(trans.Substance_Pro_Code_Tertiary);

                                            //RteAdmin3
                                            ws.Cell(countTrans, 27).SetValue(trans.Usual_Route_of_Admin_Tertiary);

                                            //FreqUse3
                                            ws.Cell(countTrans, 28).SetValue(trans.Frecuency_of_Use_Tertiary);

                                            //AgeFirstUse3
                                            ws.Cell(countTrans, 29).SetValue(trans.Age_of_First_Use_Tertiary);

                                            //OpiodTherapy
                                            ws.Cell(countTrans, 30).SetValue(trans.Opioid_Replacement_Therapy);

                                            //DetailedDrug1
                                            ws.Cell(countTrans, 31).SetValue(trans.Detailed_Drug_Code_Primary);

                                            //DetailedDrug2
                                            ws.Cell(countTrans, 32).SetValue(trans.Detailed_Drug_Code_Secondary);

                                            //DetailedDrug3
                                            ws.Cell(countTrans, 33).SetValue(trans.Detailed_Drug_Code_Tertiary);

                                            //DSMIIIRCriteria
                                            ws.Cell(countTrans, 34).SetValue(trans.DSM_Diagnosis);

                                            //CoOccurringSAMH
                                            ws.Cell(countTrans, 35).SetValue(trans.Co_occuring_Substance_Abuse_and_Mental_Health_Problems);

                                            //Pregnant
                                            ws.Cell(countTrans, 36).SetValue(trans.Pregnant_at_Time_of_Admision);

                                            //Veteran
                                            ws.Cell(countTrans, 37).SetValue(trans.Veteran_Status);

                                            //LivingArrange
                                            ws.Cell(countTrans, 38).SetValue(trans.Living_Arrangements);

                                            //PrimSrcInc
                                            ws.Cell(countTrans, 39).SetValue(trans.Source_of_Income_Support);

                                            //HealthIns
                                            ws.Cell(countTrans, 40).SetValue(trans.Health_Insurance);

                                            //PrimSrcPay
                                            ws.Cell(countTrans, 41).SetValue(trans.Expected_Actual_Primary_Source_of_Payment);

                                            //DetNLF
                                            ws.Cell(countTrans, 42).SetValue(trans.Detailed_Not_In_Labor_Force);

                                            //DetCriminal
                                            ws.Cell(countTrans, 43).SetValue(trans.Detailed_Criminal_Justice_Referral);

                                            //MaritalStat
                                            ws.Cell(countTrans, 44).SetValue(trans.Maritial_Status);

                                            //DaysWaitTreat
                                            ws.Cell(countTrans, 45).SetValue(trans.Days_Waiting_to_Enter_Treatment);

                                            //Arrests
                                            ws.Cell(countTrans, 46).SetValue(trans.Number_of_Arrests_in_30_Days_Prior_Adm);

                                            //AtndSelfHelp
                                            ws.Cell(countTrans, 47).SetValue(trans.Frequency_of_Attendance);

                                            //DiagType
                                            ws.Cell(countTrans, 48).SetValue(trans.Diagnostic_Code_Set_Identifier);

                                            //SADiagnosis
                                            ws.Cell(countTrans, 49).SetValue(trans.Substance_Abuse_Diagnostic_ICD_10);

                                            //MHDiagnosis1
                                            ws.Cell(countTrans, 50).SetValue(trans.MH_Diag_Code_Primary_ICD_10);

                                            //MHDiagnosis2
                                            ws.Cell(countTrans, 51).SetValue(trans.MH_Diag_Code_Secondary_ICD_10);

                                            //MHDiagnosis3
                                            ws.Cell(countTrans, 52).SetValue(trans.MH_Diag_Code_Tertiary_ICD_10);

                                            //SMISEDStat
                                            ws.Cell(countTrans, 53).SetValue(trans.Smi_Sed_Status);

                                            //SchoolAtndStat
                                            ws.Cell(countTrans, 54).SetValue(trans.School_Attendance_Status);

                                            //LegalStat
                                            ws.Cell(countTrans, 55).SetValue(trans.Legal_Status_at_Admission_State_Hospital);

                                            //GlobalAssess
                                            ws.Cell(countTrans, 56).SetValue(trans.Escala_GAF);

                                            //Perfil
                                            ws.Cell(countTrans, 57).SetValue(trans.Perfil);

                                            //Episodio
                                            ws.Cell(countTrans, 58).SetValue(trans.Episode);

                                            countTrans++;
                                        }
                                    }
                                }
                            }

                            break;
                        case "DIS":

                            file += $" Paso 4 Agregar Altas ";


                            var countMH2 = mh_dis.Count;
                            var countSA2 = sa_dis.Count;

                            if (countMH2 + countSA2 > maxrow)
                            {
                                pass.MaxRowReached(maxrow, countMH2 + countSA2);
                                return pass; ;
                            }
                            else
                            {
                                if (mh_dis.Count > 0)
                                {
                                    foreach (var trans in mh_dis)
                                    {

                                        ws.Row(countTrans).SetDataType(XLDataType.Text);

                                        //SysTransTyp
                                        ws.Cell(countTrans, 1).SetValue(trans.System_Transaction_Type);

                                        //StateCode
                                        ws.Cell(countTrans, 2).SetValue(trans.State_Code);

                                        //ReportDate
                                        ws.Cell(countTrans, 3).SetValue(trans.Reporting_Date);

                                        //ProviderID
                                        ws.Cell(countTrans, 4).SetValue(trans.Provider_Identifier);

                                        //ClientID
                                        ws.Cell(countTrans, 5).SetValue(trans.Client_Identifier);

                                        //CoDep
                                        ws.Cell(countTrans, 6).SetValue(trans.Co_Dependant);

                                        //Services
                                        ws.Cell(countTrans, 7).SetValue(trans.Type_of_Service);

                                        //DateLastContact
                                        ws.Cell(countTrans, 8).SetValue(trans.Date_Last_Contact);

                                        //DateDischarge
                                        ws.Cell(countTrans, 9).SetValue(trans.Date_of_Discharge);

                                        //RsnDischarge
                                        ws.Cell(countTrans, 10).SetValue(trans.Reason_for_Discharge);

                                        //AdmProviderID
                                        ws.Cell(countTrans, 11).SetValue(trans.Provider_ID_at_Admission);

                                        //AdmClientID
                                        ws.Cell(countTrans, 12).SetValue(trans.Client_ID_at_Admission);

                                        //AdmCoDep
                                        ws.Cell(countTrans, 13).SetValue(trans.Co_dependent_Collateral);

                                        //AdmClientTransTyp
                                        ws.Cell(countTrans, 14).SetValue(trans.Client_Transaction_Type_AD);

                                        //DateAdmission
                                        ws.Cell(countTrans, 15).SetValue(trans.Date_of_Admision);

                                        //AdmServices
                                        ws.Cell(countTrans, 16).SetValue(trans.Type_of_Service_AD);

                                        //DateBirth
                                        ws.Cell(countTrans, 17).SetValue(trans.Date_of_Birth_at_AD);

                                        //Gender
                                        ws.Cell(countTrans, 18).SetValue(trans.Sex_at_AD);

                                        //Race
                                        ws.Cell(countTrans, 19).SetValue(trans.Race_at_AD);

                                        //Ethnicity
                                        ws.Cell(countTrans, 20).SetValue(trans.Ethnicity_at_AD);

                                        //SubProb1
                                        ws.Cell(countTrans, 21).SetValue(trans.Substance_Pro_Code_Primary);

                                        //SubProb2
                                        ws.Cell(countTrans, 22).SetValue(trans.Substance_Pro_Code_Secondary);

                                        //SubProb3
                                        ws.Cell(countTrans, 23).SetValue(trans.Substance_Pro_Code_Tertiary);

                                        //FreqUse1
                                        ws.Cell(countTrans, 24).SetValue(trans.Frecuency_of_Use_Primary);

                                        //FreqUse2
                                        ws.Cell(countTrans, 25).SetValue(trans.Frecuency_of_Use_Secondary);

                                        //FreqUse3
                                        ws.Cell(countTrans, 26).SetValue(trans.Frecuency_of_Use_Tertiary);

                                        //LivingArrange
                                        ws.Cell(countTrans, 27).SetValue(trans.Living_Arrangements);

                                        //EmpStat
                                        ws.Cell(countTrans, 28).SetValue(trans.Employment_Status);

                                        //DetNLF
                                        ws.Cell(countTrans, 29).SetValue(trans.Detailed_Not_In_Labor_Force);

                                        //Arrests
                                        ws.Cell(countTrans, 30).SetValue(trans.Number_of_Arrests_in_30_Days_Prior_Adm);

                                        //AtndSelfHelp
                                        ws.Cell(countTrans, 31).SetValue(trans.Frequency_of_Attendance);

                                        //ClientTransType
                                        ws.Cell(countTrans, 32).SetValue(trans.Client_Transaction_Type);

                                        //DiagType
                                        ws.Cell(countTrans, 33).SetValue(trans.Diagnostic_Code_Set_Identifier);

                                        //MHDiagnosis1
                                        ws.Cell(countTrans, 34).SetValue(trans.MH_Diag_Code_Primary_ICD_10);

                                        //MHDiagnosis2
                                        ws.Cell(countTrans, 35).SetValue(trans.MH_Diag_Code_Secondary_ICD_10);

                                        //MHDiagnosis3
                                        ws.Cell(countTrans, 36).SetValue(trans.MH_Diag_Code_Tertiary_ICD_10);

                                        //SMISEDStat
                                        ws.Cell(countTrans, 37).SetValue(trans.Smi_Sed_Status);

                                        //SchoolAtndStat
                                        ws.Cell(countTrans, 38).SetValue(trans.School_Attendance_Status);

                                        //Education
                                        ws.Cell(countTrans, 39).SetValue(trans.Education);

                                        //GlobalAssess
                                        ws.Cell(countTrans, 40).SetValue(trans.Escala_GAF);

                                        //Perfil
                                        ws.Cell(countTrans, 41).SetValue(trans.Perfil);

                                        //Episodio
                                        ws.Cell(countTrans, 42).SetValue(trans.Episode);
                                        countTrans++;

                                    }


                                }
                                if (sa_dis.Count > 0)
                                {
                                    foreach (var trans in sa_dis)
                                    {
                                        ws.Row(countTrans).SetDataType(XLDataType.Text);

                                        //SysTransTyp
                                        ws.Cell(countTrans, 1).SetValue(trans.System_Transaction_Type);

                                        //StateCode
                                        ws.Cell(countTrans, 2).SetValue(trans.State_Code);

                                        //ReportDate
                                        ws.Cell(countTrans, 3).SetValue(trans.Reporting_Date);

                                        //ProviderID
                                        ws.Cell(countTrans, 4).SetValue(trans.Provider_Identifier);

                                        //ClientID
                                        ws.Cell(countTrans, 5).SetValue(trans.Client_Identifier);

                                        //CoDep
                                        ws.Cell(countTrans, 6).SetValue(trans.Co_Dependant);

                                        //Services
                                        ws.Cell(countTrans, 7).SetValue(trans.Type_of_Service);

                                        //DateLastContact
                                        ws.Cell(countTrans, 8).SetValue(trans.Date_Last_Contact);

                                        //DateDischarge
                                        ws.Cell(countTrans, 9).SetValue(trans.Date_of_Discharge);

                                        //RsnDischarge
                                        ws.Cell(countTrans, 10).SetValue(trans.Reason_for_Discharge);

                                        //AdmProviderID
                                        ws.Cell(countTrans, 11).SetValue(trans.Provider_ID_at_Admission);

                                        //AdmClientID
                                        ws.Cell(countTrans, 12).SetValue(trans.Client_ID_at_Admission);

                                        //AdmCoDep
                                        ws.Cell(countTrans, 13).SetValue(trans.Co_dependent_Collateral);

                                        //AdmClientTransTyp
                                        ws.Cell(countTrans, 14).SetValue(trans.Client_Transaction_Type_AD);

                                        //DateAdmission
                                        ws.Cell(countTrans, 15).SetValue(trans.Date_of_Admision);

                                        //AdmServices
                                        ws.Cell(countTrans, 16).SetValue(trans.Type_of_Service_AD);

                                        //DateBirth
                                        ws.Cell(countTrans, 17).SetValue(trans.Date_of_Birth_at_AD);

                                        //Gender
                                        ws.Cell(countTrans, 18).SetValue(trans.Sex_at_AD);

                                        //Race
                                        ws.Cell(countTrans, 19).SetValue(trans.Race_at_AD);

                                        //Ethnicity
                                        ws.Cell(countTrans, 20).SetValue(trans.Ethnicity_at_AD);

                                        //SubProb1
                                        ws.Cell(countTrans, 21).SetValue(trans.Substance_Pro_Code_Primary);

                                        //SubProb2
                                        ws.Cell(countTrans, 22).SetValue(trans.Substance_Pro_Code_Secondary);

                                        //SubProb3
                                        ws.Cell(countTrans, 23).SetValue(trans.Substance_Pro_Code_Tertiary);

                                        //FreqUse1
                                        ws.Cell(countTrans, 24).SetValue(trans.Frecuency_of_Use_Primary);

                                        //FreqUse2
                                        ws.Cell(countTrans, 25).SetValue(trans.Frecuency_of_Use_Secondary);

                                        //FreqUse3
                                        ws.Cell(countTrans, 26).SetValue(trans.Frecuency_of_Use_Tertiary);

                                        //LivingArrange
                                        ws.Cell(countTrans, 27).SetValue(trans.Living_Arrangements);

                                        //EmpStat
                                        ws.Cell(countTrans, 28).SetValue(trans.Employment_Status);

                                        //DetNLF
                                        ws.Cell(countTrans, 29).SetValue(trans.Detailed_Not_In_Labor_Force);

                                        //Arrests
                                        ws.Cell(countTrans, 30).SetValue(trans.Number_of_Arrests_in_30_Days_Prior_Adm);

                                        //AtndSelfHelp
                                        ws.Cell(countTrans, 31).SetValue(trans.Frequency_of_Attendance);

                                        //ClientTransType
                                        ws.Cell(countTrans, 32).SetValue(trans.Client_Transaction_Type);

                                        //DiagType
                                        ws.Cell(countTrans, 33).SetValue(trans.Diagnostic_Code_Set_Identifier);

                                        //MHDiagnosis1
                                        ws.Cell(countTrans, 34).SetValue(trans.MH_Diag_Code_Primary_ICD_10);

                                        //MHDiagnosis2
                                        ws.Cell(countTrans, 35).SetValue(trans.MH_Diag_Code_Secondary_ICD_10);

                                        //MHDiagnosis3
                                        ws.Cell(countTrans, 36).SetValue(trans.MH_Diag_Code_Tertiary_ICD_10);

                                        //SMISEDStat
                                        ws.Cell(countTrans, 37).SetValue(trans.Smi_Sed_Status);

                                        //SchoolAtndStat
                                        ws.Cell(countTrans, 38).SetValue(trans.School_Attendance_Status);

                                        //Education
                                        ws.Cell(countTrans, 39).SetValue(trans.Education);

                                        //GlobalAssess
                                        ws.Cell(countTrans, 40).SetValue(trans.Escala_GAF);

                                        //Perfil
                                        ws.Cell(countTrans, 41).SetValue(trans.Perfil);

                                        //Episodio
                                        ws.Cell(countTrans, 42).SetValue(trans.Episode);
                                        countTrans++;

                                    }


                                }
                            }
                            break;




                    }
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);


                        var rep = stream.ToArray();



                        if (rep != null)
                        {
                            var h = new SM_HISTORIAL() { TI_ACCION = 0, DE_Historial = $"Realizo una extracción de {file} {min.ToString("yyyy-MM-dd")} al {max.ToString("yyyy-MM-dd")}", FE_Historial = DateTime.Now, FK_Sesion = sesionSMUEE, FK_Modulo = "TEDS" };
                            Logs.Add(h);

                            pass.Success($"{file} {min.ToString("yyyy-MM-dd")} al {max.ToString("yyyy-MM-dd")}.xlsx", rep);
                            return pass;
                        }
                    }
                }




                return null;

            }
            catch (Exception ee)
            {
                pass.Error();
                return pass;
            }
        }

        private static void Header(IXLWorksheet ws, string[] arr)
        {
            var countHeader = 1;
            foreach (var header in arr)
            {

                ws.Cell(1, countHeader).SetValue(header);
                ws.Column(countHeader).SetDataType(XLDataType.Text);
                countHeader++;
            }
        }





        [WebMethod]
        public List<List<Models.DdlProgramExtracciones>> GetProgramasTEDS()
        {
            var lstMh = new List<Models.DdlProgramExtracciones>();
            var lstSa = new List<Models.DdlProgramExtracciones>();
            var lstBoth = new List<Models.DdlProgramExtracciones>();


            using (var seps = new SEPSEntities())
            {

                List<List<Models.DdlProgramExtracciones>> lst = new List<List<Models.DdlProgramExtracciones>>();


                var lista = seps.SA_PROGRAMA.Where(x => x.REP_TEDS == true || x.REP_TEDS_MH == true).ToList();

                var mh = lista.Where(x => x.REP_TEDS == false && x.REP_TEDS_MH == true).OrderBy(x => x.NB_Programa).ToList();
                var sa = lista.Where(x => x.REP_TEDS == true && x.REP_TEDS_MH == false).OrderBy(x => x.NB_Programa).ToList();
                var both = lista.Where(x => x.REP_TEDS == true && x.REP_TEDS_MH == true).OrderBy(x => x.NB_Programa).ToList();


                if (mh.Count > 0)
                {

                    foreach (var programa in mh)
                    {

                        var label = $"{programa.CO_Programa} - {programa.NB_Programa} {((programa.IN_Inactivo == true) ? "(Inactivo)" : "")}";
                        lstMh.Add(new Models.DdlProgramExtracciones() { label = label, alias = label, value = programa.PK_Programa.ToString() });
                    }

                }

                if (sa.Count > 0)
                {

                    foreach (var programa in sa)
                    {

                        var label = $"{programa.CO_Programa} - {programa.NB_Programa} {((programa.IN_Inactivo == true) ? "(Inactivo)" : "")}";
                        lstSa.Add(new Models.DdlProgramExtracciones() { label = label, alias = label, value = programa.PK_Programa.ToString() });
                    }

                }


                if (both.Count > 0)
                {

                    foreach (var programa in both)
                    {

                        var label = $"{programa.CO_Programa} - {programa.NB_Programa} {((programa.IN_Inactivo == true) ? "(Inactivo)" : "")}";
                        lstBoth.Add(new Models.DdlProgramExtracciones() { label = label, alias = label, value = programa.PK_Programa.ToString() });
                    }

                }

                lst.Add(lstMh);
                lst.Add(lstSa);
                lst.Add(lstBoth);


                return lst;



            }


        }



        public class PassParameter
        {

            public int Type { get; set; }
            public string Icon { get; set; }
            public string Message { get; set; }
            public string FileName { get; set; }
            public string Title { get; set; }
            public byte[] File { get; set; }


            public void MaxRowReached(int min ,int total)
            {
                Type = 1;
                Title = "Máximo de transacciones alcanzadas";
                Message = $"Ha alcanzado el máximo de transacciones permitidas por el sistema de TEDS. Favor de elegir un rango de fecha más pequeño para poder realizar la extracción ({total}/{min}).";
                FileName = "";
                Icon = "warning";
                File = null;
            }

            public void Error()
            {
                Type = 2;
                Title = "Error";
                Message = "No se pudo descargar su archivo, inténtelo nuevamente.";
                FileName = "";
                Icon = "error";
                File = null;
            }

            public void Success(string filename, byte[] file)
            {
                Type = 3;
                Title = "Descarga Completada";
                Message = $"Su archivo se ha descargado como {filename}";
                FileName = filename;
                Icon = "success";
                File = file;
            }

        }

    }





}
