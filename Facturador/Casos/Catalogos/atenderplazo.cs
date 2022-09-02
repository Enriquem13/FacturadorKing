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
    public partial class atenderplazo : Form
    {
        public String sgPlazosdetalleid = "";
        public String sgFechavigencia = "";
        public String sgTipoplazo = "";
        Form1 Login;
        public atenderplazo(String sPlazosdetalleid, String sFechaVigencia, String sTipoplazo, Form1 fLogin)
        {
            sgPlazosdetalleid = sPlazosdetalleid;
            sgFechavigencia = sFechaVigencia;
            sgTipoplazo = sTipoplazo;
            Login = fLogin;
            InitializeComponent();

            tbTipodeplazo.Text = sgTipoplazo;
            tbFechavencimineto.Text = sgFechavigencia;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            { 
                if (rtObservaciones.Text != "" )//&& (radioButton3.Checked || radioButton2.Checked || radioButton1.Checked))
                {
                    DateTime sFechaactual = DateTime.Today;
                    conect con1 = new conect();
                    String sQueryallescritos = " UPDATE `plazos_detalle` SET " +
                                                //" `Atendio_Plazos_detalleid` = '" + splazos_detalleid_ult + "', " +
                                                " `Estatus_plazoid` = '4', " +
                                                " `Motivo_cancelacion_plazo` = '" + rtObservaciones.Text + "', " +
                                                " `Fecha_cancelacion_plazo` = '" + sFechaactual.ToString("yyyy/MM/dd") + "', " +
                                                " `usuario_cancelo` = '" + Login.sId + "' " +
                                                " WHERE (`Plazos_detalleid` = '" + sgPlazosdetalleid + "');";
                    MySqlDataReader resp_escritos = con1.getdatareader(sQueryallescritos);
                    if (resp_escritos.RecordsAffected > 0)
                    {
                        MessageBox.Show("Plazo atendido correctamente.");
                    }
                    resp_escritos.Close();
                    con1.Cerrarconexion();
                    
                    // Se atendera el plazo tal sgPlazoid
                    //MessageBox.Show("Plazo atendido");
                    this.Close();
                }
                else {
                    MessageBox.Show("Debe escribir observaciones y un estatus para atender el plazo");
                }
            }catch(Exception Ex){

            }
        }

        public ComboboxItem validareader(String campoText, String campoValue, MySqlDataReader mresultado)
        {
            ComboboxItem cItemresult = new ComboboxItem();
            try
            {


                if (!mresultado.IsDBNull(mresultado.GetOrdinal(campoText)))
                {
                    cItemresult.Text = mresultado.GetString(mresultado.GetOrdinal(campoText));
                }
                else
                {
                    cItemresult.Text = "";
                }

                if (!mresultado.IsDBNull(mresultado.GetOrdinal(campoValue)))
                {
                    cItemresult.Value = mresultado.GetString(mresultado.GetOrdinal(campoValue));
                }
                else
                {
                    cItemresult.Value = "";
                }
                return cItemresult;
            }
            catch (Exception e)
            {
                cItemresult.Text = "";
                cItemresult.Value = "";
                return cItemresult;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (rtObservaciones.Text != "")//&& (radioButton3.Checked || radioButton2.Checked || radioButton1.Checked))
                {
                    DateTime sFechaactual = DateTime.Today;
                    conect con1 = new conect();
                    String sQueryallescritos = " UPDATE `plazos_detalle` SET " +
                                                //" `Atendio_Plazos_detalleid` = '" + splazos_detalleid_ult + "', " +
                                                " `Estatus_plazoid` = '2', " +
                                                " `Motivo_cancelacion_plazo` = '" + rtObservaciones.Text + "', " +
                                                " `Fecha_atendio_plazo` = '" + sFechaactual.ToString("yyyy/MM/dd") + "', " +
                                                " `Usuarioid_atendio_plazo` = '" + Login.sId + "' " +
                                                " WHERE (`Plazos_detalleid` = '" + sgPlazosdetalleid + "');";
                    MySqlDataReader resp_escritos = con1.getdatareader(sQueryallescritos);
                    if (resp_escritos.RecordsAffected > 0)
                    {
                        MessageBox.Show("Plazo atendido correctamente.");
                    }
                    resp_escritos.Close();
                    con1.Cerrarconexion();

                    // Se atendera el plazo tal sgPlazoid
                    //MessageBox.Show("Plazo atendido");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Debe escribir observaciones y un estatus para atender el plazo");
                }
            }
            catch (Exception Ex)
            {

            }
        }
    }
}
