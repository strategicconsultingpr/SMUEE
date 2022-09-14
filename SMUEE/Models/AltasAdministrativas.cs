using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMUEE.Models
{
    public class AltasAdministrativas
    {
        public int Episodio { get; set; }
        public string FechaStr {get;set;}


        public DateTime Fecha => FormatFecha();
        public int Razon { get; set; }


        public DateTime FormatFecha()
        {
            DateTime fecha;

            if (DateTime.TryParse(FechaStr, out fecha))
            {
                return fecha;
            }
                return DateTime.Now;

        }

    }
}