using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMUEE.Models
{
    public class Helpers
    {
        public static string[] HeaderAD = {"SysTranType","StateCode","ReportDate","ProviderID","ClientID","CoDep","ClientTransType","DateAdmission","Services","NumPriorTreat","PrinSrcRef","DateBirth",
                        "Gender","Race","Ethnicity","Education","EmpStat","SubProb1","RteAdmin1","FreqUse1","AgeFirstUse1","SubProb2","RteAdmin2","FreqUse2","AgeFirstUse2","SubProb3","RteAdmin3","FreqUse3",
                        "AgeFirstUse3","OpiodTherapy","DetailedDrug1","DetailedDrug2","DetailedDrug3","DSMIIIRCriteria","CoOccurringSAMH","Pregnant","Veteran","LivingArrange","PrimSrcInc","HealthIns","PrimSrcPay","DetNLF","DetCriminal","MaritalStat","DaysWaitTreat","Arrests","AtndSelfHelp",
                        "DiagType","SADiagnosis","MHDiagnosis1","MHDiagnosis2","MHDiagnosis3","SMISEDStat","SchoolAtndStat","LegalStat","GlobalAssess","Perfil","Episodio"};
       public static string[] HeaderDIS = { "SysTransTyp", "StateCode", "ReportDate", "ProviderID", "ClientID", "CoDep", "Services", "DateLastContact", "DateDischarge", "RsnDischarge", "AdmProviderID", "AdmClientID", "AdmCoDep", "AdmClientTransTyp", "DateAdmission", "AdmServices", "DateBirth", "Gender", "Race", "Ethnicity", "SubProb1", "SubProb2"
                    , "SubProb3", "FreqUse1", "FreqUse2", "FreqUse3", "LivingArrange", "EmpStat", "DetNLF", "Arrests", "AtndSelfHelp", "ClientTransType", "DiagType", "MHDiagnosis1", "MHDiagnosis2", "MHDiagnosis3", "SMISEDStat", "SchoolAtndStat", "Education", "GlobalAssess", "Perfil", "Episodio"};


    }
}