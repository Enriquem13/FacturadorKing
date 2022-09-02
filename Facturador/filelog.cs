using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturador
{
    class filelog
    {
        public filelog(String sIdusuario, String sError) {
            try { 
                    String ruta = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + "\\casosking\\";
                    if (!Directory.Exists(ruta))
                    {
                        System.IO.Directory.CreateDirectory(ruta);
                    }
                    String fechalog = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                    StringBuilder sb = new StringBuilder();
                    sb.Append(fechalog + ":userid:" + sIdusuario + " Error:" + sError + "\n");
                    System.IO.File.AppendAllText(ruta + "sistema_casosking.log", sb.ToString());
                    sb.Clear();        
                }catch(Exception E){
                
                }
        }
    }
}
