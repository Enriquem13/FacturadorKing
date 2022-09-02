using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturador
{
    class configuracionfiles
    {
        
        public String sServer { get; set;}
        public String sDatabase { get; set; }
        public String sUid { get; set; }
        public String sPwd { get; set; }
        public String sFileupload { get; set; }
        
        public String[] lineas;
        public void configuracionfilesinicio() {
            try
            {
                String ruta_log = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string fichero = ruta_log+"\\casosking\\confacturador.prop";
                string contenido = String.Empty;
                if (File.Exists(fichero))
                {
                    contenido = File.ReadAllText(fichero);
                    lineas = contenido.Split('\n');
                }
                sServer = lineas[0];
                sDatabase = lineas[1];
                sUid = lineas[2];
                sPwd = lineas[3];
                sFileupload = lineas[4];
                String conexion = "server=" + lineas[0] + ";database=" + lineas[1] + ";Uid=" + lineas[2] + ";pwd=" + lineas[3] + ";";
            }catch(Exception E){
                String ruta_log = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + "\\casosking\\";
                if (!Directory.Exists(ruta_log))
                {
                    System.IO.Directory.CreateDirectory(ruta_log);
                }
                String fechalog = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                StringBuilder sb_log = new StringBuilder();
                sb_log.Append(fechalog + ":configuracionfilesinicio:" + " Error:" + E + "\n");
                System.IO.File.AppendAllText(ruta_log + "sistema_casosking.log", sb_log.ToString());
                sb_log.Clear();
            }
        }
    }
}
