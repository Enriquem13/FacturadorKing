using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emailking
{
    class conect_robot
    {
        public string[] lineas;
        public MySqlConnection con;
        MySqlCommand mEjecutaquery;
        MySqlDataReader respuesta;
        String strRutaArchivo = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public MySqlConnection conecto()
        {
            string fichero = strRutaArchivo + "\\mailking\\conemailking.properties";
            string contenido = String.Empty;
            if (File.Exists(fichero))
            {
                contenido = File.ReadAllText(fichero);
                lineas = contenido.Split('\n');
            }
            else
            {
            }
            String conexion = "server=" + lineas[0] + ";database=" + lineas[1] + ";Uid=" + lineas[2] + ";pwd=" + lineas[3] + ";SslMode=none";
            //String conexion = "server=192.168.1.160;database=king;Uid=root;pwd=Alejandra5m;" + "SslMode=none";
            con = new MySqlConnection(conexion);
            con.Open();
            return con;
        }
        public string getdatavalues(){
            string fichero = strRutaArchivo + "\\mailking\\conemailking.properties";
            string contenido = String.Empty;
            if (File.Exists(fichero))
            {
                contenido = File.ReadAllText(fichero);
                lineas = contenido.Split('\n');
            }
            else
            {

            }
            //String conexion = "server=" + lineas[0] + ";database=" + lineas[1] + ";Uid=" + lineas[2] + ";pwd=" + lineas[3] + ";";
            String conexion = "user id=" + lineas[2] + ";database=jobconfig" + ";host=" + lineas[0] + ";password=" + lineas[3] + ";SslMode=none";
            return conexion;
        }
        
        public void Cerrarconexion()
        {
            //con.EnlistTransaction(new System.Transactions.Transaction MySqlTransaction iwudeh);
            try {
                con.ClearAllPoolsAsync();
                con.CloseAsync();
                //agregar referencia
                con.Close();
            }catch(Exception ex){
                
            }

            
            //con.ClearPoolAsync(,);
        }

        public String setquery(String query)
        {
            MySqlConnection con = conecto();
            mEjecutaquery = new MySqlCommand(String.Format(query), con);
            respuesta = mEjecutaquery.ExecuteReader();
            String respuestastring = "";
            while (respuesta.Read())
            {
                respuestastring += "\n " + respuesta.GetInt32(0);
            }
            return respuestastring;
        }

        public MySqlDataReader getdatareader(String query)
        {
            MySqlDataReader respuesta = null;
            try
            {
                MySqlConnection con = conecto();
                MySqlCommand mEjecutaquery = new MySqlCommand(String.Format(query), con);
                respuesta = mEjecutaquery.ExecuteReader();
            }
            catch (Exception E)
            {

            }

            return respuesta;
        }
    }
}
