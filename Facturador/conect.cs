using MySql.Data.MySqlClient;
using System.IO;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturador
{
    class conect
    {
        public string[] lineas;
        public MySqlConnection con;
        MySqlCommand mEjecutaquery;
        MySqlDataReader respuesta;
        funcionesdicss funcionesgenerales = new funcionesdicss();

        public MySqlConnection conecto()
        {
            String conexion = "";
            try {
                String strRutaArchivo = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string fichero =strRutaArchivo+ "\\casosking\\confacturador.prop";
                string contenido = String.Empty;
                if (File.Exists(fichero))
                {
                    contenido = File.ReadAllText(fichero);
                    lineas = contenido.Split('\n');
                }
                else
                {

                }
                conexion = "server=" + lineas[0] + ";database=" + lineas[1] + ";Uid=" + lineas[2] + ";port=" + lineas[5] + ";pwd=" + lineas[3] + ";";
                //conexion = "server=" + lineas[0] + ";database=" + lineas[1] + ";Uid=" + lineas[2] + ";port=3307;pwd=" + lineas[3] + ";";//para pruebas ambiente dicss
                con = new MySqlConnection(conexion);
                
                con.Open();
                return con;
            }catch(Exception E){
                String ruta_log = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + "\\casosking";
                if (!Directory.Exists(ruta_log))
                {
                    System.IO.Directory.CreateDirectory(ruta_log);
                }
                String fechalog = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                StringBuilder sb_log = new StringBuilder();
                sb_log.Append(fechalog + ":Class:conecto" + " Error:" + E + "valores conexión: " + conexion + "\n");
                System.IO.File.AppendAllText(ruta_log + "casosking.log", sb_log.ToString());
                sb_log.Clear();
                return null;
            }
            
        }

        
        public void Cerrarconexion() {
            //con.EnlistTransaction(new System.Transactions.Transaction MySqlTransaction iwudeh);
            //con.ClearAllPoolsAsync();
            //con.CloseAsync();
            try {
                con.Close();
            }catch(Exception Exes){
                new filelog("", ""+Exes.Message);
            }
            
            //con.ClearPoolAsync(,);
        }

        public String setquery(String query)
        {
            MySqlConnection con= conecto();
            mEjecutaquery = new MySqlCommand(String.Format(query), con);
            respuesta = mEjecutaquery.ExecuteReader();
            String respuestastring = "";
            while (respuesta.Read())
            {
                respuestastring += "\n "+respuesta.GetInt32(0);
            }
            return respuestastring;
        }

        public MySqlDataReader getdatareader(String query)
        {
            MySqlDataReader respuesta=null;
            try {
                MySqlConnection con = conecto();
                
                MySqlCommand mEjecutaquery = new MySqlCommand(String.Format(query), con);
               
                respuesta = mEjecutaquery.ExecuteReader();
            }catch(Exception Ex){
                new filelog("", "" + Ex.Message);
            }
            
            return respuesta;
        }
    }
}