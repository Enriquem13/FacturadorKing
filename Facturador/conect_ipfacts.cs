using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturador
{
    class conect_ipfacts
    {
        public string[] lineas;
        public MySqlConnection con;
        MySqlCommand mEjecutaquery;
        MySqlDataReader respuesta;

        public MySqlConnection conecto()
        {
            String conexion = "";
            try
            {
                conexion = "server=192.168.1.160;database=king;Uid=root;pwd=Alejandra5m;;SslMode=none";
                con = new MySqlConnection(conexion);

                con.Open();
                return con;
            }
            catch (Exception E)
            {
                new filelog("conexion productiva king", E.ToString());
                return null;
            }

        }
        public void Cerrarconexion()
        {
            //con.EnlistTransaction(new System.Transactions.Transaction MySqlTransaction iwudeh);
            //con.ClearAllPoolsAsync();
            //con.CloseAsync();
            try
            {
                con.Close();
            }
            catch (Exception E)
            {

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
