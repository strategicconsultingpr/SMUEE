using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMUEE.Models
{
    public class Logs
    {
     

        public static void Add(SM_HISTORIAL historial)
        {
            try
            {
                using(var smuee = new SMUEEEntities())
                {
                    smuee.Entry(historial).State = System.Data.Entity.EntityState.Added;
                    smuee.SaveChanges();
                }
            }
            catch (Exception ee)
            {

            }

        }
    }

    
}