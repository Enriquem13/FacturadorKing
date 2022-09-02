using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Facturador
{
    public partial class FSelectdireccionint : Form
    {
        String sgInteresadid = "";
        String sgCasoid = "";
        String sgCasonumero = "";
        String sgRelacion = "";
        String sgDomicilio = "";
        String sgTiposolicitud = "";
        funcionesdicss objfuncionesdicss = new funcionesdicss();

        public FSelectdireccionint(String sInteresadosid, String sCasoid, String sCasonumero, String  sRelacion, String sDomicilio,  String sTiposolicitud)
        {
            sgInteresadid = sInteresadosid;
            sgCasoid = sCasoid;
            sgCasonumero = sCasonumero;
            sgRelacion = sRelacion;
            sgDomicilio = sDomicilio;
            sgTiposolicitud = sTiposolicitud;
            InitializeComponent();


            conect conectdeleteinteresado = new conect();
            String sCondiciondeinteresado = "";
            if (sTiposolicitud == "1" || sTiposolicitud == "2" || sTiposolicitud == "3" || sTiposolicitud == "4" || sTiposolicitud == "5" || sTiposolicitud == "19")
            {
                sCondiciondeinteresado = "1, 2, 3, 5";
            }
            else { //ya buscaremos para marcas y otros casos
                sCondiciondeinteresado = "1, 5";
            }
            String query_tiporelacion = "select * from tiporelacion where TipoRelacionId in(" + sCondiciondeinteresado + ");";
            MySqlDataReader respuesta_tiporelaciones = conectdeleteinteresado.getdatareader(query_tiporelacion);
            while (respuesta_tiporelaciones.Read())
            {
                cbRelacioncasointeresado.Items.Add(objfuncionesdicss.validareader("TipoRelacionDescrip", "TipoRelacionId", respuesta_tiporelaciones));
            }
            respuesta_tiporelaciones.Close();
            conectdeleteinteresado.Cerrarconexion();

            cbRelacioncasointeresado.Text = sRelacion;
            tbInteresado.Text = sInteresadosid;
            tbCasonumero.Text = sCasonumero;
            tbDomicilio.Text = sDomicilio;
            cargarinteresados();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tbDomicilio.Text != "")
            {
                this.Close();
            }
            else {
                MessageBox.Show("Debe seleccionar dando doble click en una dirección antes de Salir");
            }
            
        }

        public void cargarinteresados()
        {
            conect conectdireccioninteresado = new conect();
            String kwerydireccioninteresado = "SELECT  direccion.DireccionCalle,  " +
                                                     " direccion.DireccionNumExt, " +
                                                     " direccion.DireccionNumInt, " +
                                                     " direccion.DireccionColonia, " +
                                                     " direccion.DireccionPoblacion, " +
                                                     " direccion.DireccionEstado, " +
                                                     " direccion.DireccionCP, " +
                                                     " direccion.DireccionID, " +
                                                     " DameNombrePais(direccion.PaisId) AS NOMBREPAIS, " +
                                                     " direccion.PaisId AS PaisId, " +
                                                     " direccion.TipoDireccionId AS TipoDireccionId, " +
                                                     " Dametipodirecccion (direccion.TipoDireccionId) AS TIPODIRECCION " +
                                                     " FROM direccion " +
                                                     " WHERE direccion.InteresadoId =  " + sgInteresadid + ";";
            MySqlDataReader respuestastringdireccioninteresado = conectdireccioninteresado.getdatareader(kwerydireccioninteresado);
            dataGridView1.Rows.Clear();
            int count = 0;
            while (respuestastringdireccioninteresado.Read())
            {
                string sDireccionID = objfuncionesdicss.validareader("DireccionID", "DireccionID", respuestastringdireccioninteresado).Text;
                string sDireccionCalle = objfuncionesdicss.validareader("DireccionCalle", "DireccionID", respuestastringdireccioninteresado).Text;
                string sDireccionNumExt = objfuncionesdicss.validareader("DireccionNumExt", "DireccionID", respuestastringdireccioninteresado).Text;
                string sDireccionNumInt = objfuncionesdicss.validareader("DireccionNumInt", "DireccionID", respuestastringdireccioninteresado).Text;
                string sDireccionColonia = objfuncionesdicss.validareader("DireccionColonia", "DireccionID", respuestastringdireccioninteresado).Text;
                string sDireccionPoblacion = objfuncionesdicss.validareader("DireccionPoblacion", "DireccionID", respuestastringdireccioninteresado).Text;
                string sDireccionEstado = objfuncionesdicss.validareader("DireccionEstado", "DireccionID", respuestastringdireccioninteresado).Text;
                string sDireccionCP = objfuncionesdicss.validareader("DireccionCP", "DireccionID", respuestastringdireccioninteresado).Text;
                string sNOMBREPAIS = objfuncionesdicss.validareader("NOMBREPAIS", "DireccionID", respuestastringdireccioninteresado).Text;
                string sPaisId = objfuncionesdicss.validareader("PaisId", "DireccionID", respuestastringdireccioninteresado).Text;
                string sTIPODIRECCION = objfuncionesdicss.validareader("TIPODIRECCION", "DireccionID", respuestastringdireccioninteresado).Text;
                string sTipoDireccionId = objfuncionesdicss.validareader("TipoDireccionId", "DireccionID", respuestastringdireccioninteresado).Text;

                dataGridView1.Rows.Add(sDireccionID, sDireccionCalle, sDireccionNumExt, sDireccionNumInt, sDireccionColonia, sDireccionPoblacion, sDireccionEstado, sDireccionCP, sNOMBREPAIS, sPaisId, sTipoDireccionId, sTIPODIRECCION);
                count++;
            }
            respuestastringdireccioninteresado.Close();
            conectdireccioninteresado.Cerrarconexion();

            if(count == 1){//si es un registro
                dataGridView1.Rows[0].Selected = true;
                bool bMuestramnensaje = false;
                seleccionaladireccon(bMuestramnensaje);
  
            }
            
        }

        public void seleccionaladireccon(bool bMuestramnensaje)
        {
            String kwerydireccioninteresado_update = "";
            try
            {
                String sDireccionid = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();//Direccionid seleccionado
                string sDireccionCalle = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                string sDireccionNumExt = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                string sDireccionNumInt = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                string sDireccionColonia = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                string sDireccionPoblacion = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                string sDireccionEstado = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
                string sDireccionCP = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
                string sNOMBREPAIS = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
                if (sDireccionid == "" || sgInteresadid == "" || sgCasoid == "" || sgTiposolicitud == "")
                {
                    MessageBox.Show("Ocurrió un error, verifique la seleccióm del iteresado");
                    return;
                }
                conect conectdireccioninteresado_update = new conect();
                kwerydireccioninteresado_update = "UPDATE `casointeresado` SET `DireccionId` = '" + sDireccionid + "' WHERE InteresadoId = " + sgInteresadid + " and  CasoId = " + sgCasoid + " and TipoSolicitudId= '" + sgTiposolicitud + "';";
                // and TipoRelacionId = '" + (cbRelacioncasointeresado.SelectedItem as ComboboxItem).Value + "'; ";
                MySqlDataReader respuestastringdireccioninteresado_update = conectdireccioninteresado_update.getdatareader(kwerydireccioninteresado_update);
                //MessageBox.Show(respuestastringdireccioninteresado_update.RecordsAffected + " dirección(es) actualizada(s).");
                respuestastringdireccioninteresado_update.Close();
                conectdireccioninteresado_update.Cerrarconexion();
                //hacemos update de direccionid a a tabla 
                if (respuestastringdireccioninteresado_update.RecordsAffected > 0)
                {
                    tbDomicilio.Text = sDireccionid + " " + sDireccionCalle + " " + sDireccionNumExt + " " + sDireccionNumInt + " " + sDireccionColonia + " " + sDireccionPoblacion + " " + sDireccionEstado + " " + sDireccionCP + " " + sNOMBREPAIS;
                    if (bMuestramnensaje)
                    {
                        MessageBox.Show("Se actualizó la dirección del titular para la solicitud.");
                    }
                    //this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar la relación de la dirección: " + kwerydireccioninteresado_update);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("No se pudo actualizar la relación de la dirección: " + kwerydireccioninteresado_update);
            }
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            seleccionaladireccon(true);//debe mostrar el mensaje porque es más de una dirección
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            captura Formcap = new captura(form);

            FInteresadoDetalle detalleinteresado = new FInteresadoDetalle(sgInteresadid, form, Formcap);
            detalleinteresado.ShowDialog();
            cargarinteresados();
            Formcap.Close();
            form.Close();
        }

        private void cbRelacioncasointeresado_SelectedIndexChanged(object sender, EventArgs e)
        {
            try {
                //hacemos el update del interesado seleccionado 
                conect conectrelacion_update = new conect();
                String squeryupdate = "UPDATE `casointeresado` SET `TipoRelacionId` = '" + (cbRelacioncasointeresado.SelectedItem as ComboboxItem).Value + "' WHERE InteresadoId = " + sgInteresadid +
                                " and  CasoId = " + sgCasoid + " and TipoSolicitudId= '" + sgTiposolicitud + "'; ";
                MySqlDataReader respuestastringdireccioninteresado_update = conectrelacion_update.getdatareader(squeryupdate);
                //MessageBox.Show(respuestastringdireccioninteresado_update.RecordsAffected + " Relación actualizada.");
                respuestastringdireccioninteresado_update.Close();
                conectrelacion_update.Cerrarconexion();
            }
            catch (Exception exs) {
                new filelog(" error", ""+exs.StackTrace);
            }
            


        }
    }
}
