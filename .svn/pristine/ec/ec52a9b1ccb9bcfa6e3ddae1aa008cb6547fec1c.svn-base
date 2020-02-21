using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SVN_BackUp.rutinas
{
    class General
    {
        //==========================================================================================
        //===============Rutinas Generales==========================================================
        //==========================================================================================

        public static string formatear(string cadena, int largo)
        {
            int largo_cadena = 0;
            string cadena_formateada = "";

            largo_cadena = cadena.Length;
            cadena_formateada = cadena;


            for (int i = 0; i < largo - largo_cadena; i++)
            {
                cadena_formateada = " " + cadena_formateada;
            }
            if (largo_cadena > largo)
            {
                cadena_formateada = cadena.Substring(0, largo - 3) + "...";
            }


            return cadena_formateada;
        }

        public static void eLog(string mensaje)
        {
            DateTime fecha = DateTime.Now;
            string fecha_string = fecha.ToString("dd/MM/yyyy hh:mm:ss");
            string fecha_juliana = fecha.ToString("yyyyMMdd");
            String ArchLog = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"" + fecha_juliana + "ProcesoAutomatico.log";

            mensaje = fecha + "; " + mensaje + ";";

            try
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(ArchLog, true);
                sw.WriteLine(mensaje);
                sw.Close();
            }
            catch (Exception ex)
            {
                ArchLog = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"" + fecha_juliana + "ProcesoAutomatico_2.log";
                System.IO.StreamWriter sw = new System.IO.StreamWriter(ArchLog, true);
                sw.WriteLine(mensaje);
                sw.Close();
            }



        }
    }
}
