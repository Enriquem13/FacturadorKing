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
    public partial class CatEstatus : Form
    {
        funcionesdicss obj_dicss = new funcionesdicss();
        public CatEstatus()
        {
            InitializeComponent();
            iniciacomponentes();
        }

        public void iniciacomponentes() {
            try {
                cb_seguimiento.Items.Clear();
                //iniciamos los componentes del combobox
                ComboboxItem cambouno = new ComboboxItem();
                cambouno.Text = "Habilitado";
                cambouno.Value= 1;
                ComboboxItem cambodos = new ComboboxItem();
                cambodos.Text = "Deshablitado";
                cambodos.Value = 0;
                cb_seguimiento.Items.Add(cambouno);
                cb_seguimiento.Items.Add(cambodos);

                //iniciamos los componentes del datagridview 
                //iniciamos la consultra de los estatus que son generados por los subtipodocumentos
                conect con = new conect();
                String sQuerytipoSol = "select * from estatuscaso;";

                MySqlDataReader respuestastringtoiposl = con.getdatareader(sQuerytipoSol);
                dgv_estatuscaso.Rows.Clear();
                while (respuestastringtoiposl.Read())
                {
                    //cb_estatusid  SubTipoDocumentoIndTipo
                    //SubTipoDocumentoDescrip
                    //cbtipodedocumento.Items.Add(obj_dicss.validareader("SubTipoDocumentoDescrip", "SubTipoDocumentoId", respuestastringtoiposl));
                    String sEstatusCasoDescrip = obj_dicss.validareader("EstatusCasoDescrip", "EstatusCasoId", respuestastringtoiposl).Text;
                    String sEstatusCasoDescripIngles = obj_dicss.validareader("EstatusCasoDescripIngles", "EstatusCasoId", respuestastringtoiposl).Text;
                    String sestautscasoindseg = obj_dicss.validareader("estautscasoindseg", "EstatusCasoId", respuestastringtoiposl).Text;
                    String sEstatusCasoId = obj_dicss.validareader("EstatusCasoId", "EstatusCasoId", respuestastringtoiposl).Text;
                    if (sestautscasoindseg == "1")
                    {
                        sestautscasoindseg = "Habilitado";
                    }else{
                        sestautscasoindseg = "Deshabilitado";
                    }

                    dgv_estatuscaso.Rows.Add(sEstatusCasoId, sEstatusCasoDescrip, sEstatusCasoDescripIngles, sestautscasoindseg);

                }
                respuestastringtoiposl.Close();
                con.Cerrarconexion();

            }
            catch (Exception Ex)
            {
                new filelog("catálogo de estatus caso ", "Error " + Ex);
            }
        }

        private void btn_salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try {
                if (tb_estatusespano.Text=="")
                {
                    MessageBox.Show("La descripción en español no puede estar vacía");
                    return;
                }
                if (tb_estatusingles.Text == "")
                {
                    MessageBox.Show("La descripción en Inglés no puede estar vacía");
                    return;
                }

                if(cb_seguimiento.Text == "")
                {
                    MessageBox.Show("Debe seleccionar una opción de seguimiento.");
                    return;
                }

                //tb_estatusespano
                //tb_estatusingles
                //cb_seguimiento
                conect con = new conect();
                String sQueryaddecaso= " INSERT INTO `estatuscaso` " +
                                        " (`EstatusCasoId`, " +
                                        " `EstatusCasoDescrip`, " +
                                        " `EstatusCasoDescripIngles`, " +
                                        " `estautscasoindseg`) " +
                                        " VALUES " +
                                        " (NULL, " +
                                        " '" + tb_estatusespano.Text+ "', " +
                                        " '" + tb_estatusingles.Text+ "', " +
                                        " '" + (cb_seguimiento.SelectedItem as ComboboxItem).Value + "'); ";
                MySqlDataReader respuestastringtoiposl = con.getdatareader(sQueryaddecaso);
                if (respuestastringtoiposl.RecordsAffected == 1)
                {
                    tb_estatusespano.Text = "";
                    tb_estatusingles.Text = "";
                    cb_seguimiento.Text = "";
                    iniciacomponentes();
                }
                else {
                    MessageBox.Show("Hubo un error al intentar agregar el estatus");
                    new filelog("Agregar estatus CatEstatus.cs 93", "Hubo un error al intentar agregar el estatus");
                }
                respuestastringtoiposl.Read();
                respuestastringtoiposl.Close();
                con.Cerrarconexion();
            }catch(Exception Ex){
                new filelog("Agregar estatus CatEstatus.cs", "Error"+Ex);
            }
        }

        private void dgv_estatuscaso_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try {
                String sEstatusCasoId = dgv_estatuscaso.SelectedRows[0].Cells[0].Value.ToString();
                String sEstatusCasoDescrip = dgv_estatuscaso.SelectedRows[0].Cells[1].Value.ToString();
                String sEstatusCasoDescripIngles = dgv_estatuscaso.SelectedRows[0].Cells[2].Value.ToString();
                String vasestautscasoindseglor = dgv_estatuscaso.SelectedRows[0].Cells[3].Value.ToString();
                //String valor = dgv_estatuscaso.SelectedRows[0].Cells[5].Value.ToString();
                tb_estatusespano.Text = sEstatusCasoDescrip;
                tb_estatusingles.Text = sEstatusCasoDescripIngles;

                button1.Enabled = false;
                button2.Enabled = true;
                button3.Enabled = true;

                cb_seguimiento.Text = vasestautscasoindseglor;
                dgv_estatuscaso.Enabled = false;
            }catch(Exception Ex){
                new filelog("139 CatEstatus.cs", "Error: "+Ex);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try {
                if(cb_seguimiento.Text==""){
                    MessageBox.Show("Debe seleccionar si él estatus está habilitado.");
                    return;
                }
                String sEstatusCasoId = dgv_estatuscaso.SelectedRows[0].Cells[0].Value.ToString();
                String estatuespanol = tb_estatusespano.Text;
                String Estatusingles = tb_estatusingles.Text;
                String sSeguimiento = (cb_seguimiento.SelectedItem as ComboboxItem).Value.ToString();
                conect con = new conect();
                String sQueryupdate = " UPDATE `estatuscaso` " +
                                        " SET " +
                                        " `EstatusCasoDescrip` = '" + estatuespanol + "', " +
                                        " `EstatusCasoDescripIngles` = '" + Estatusingles + "', " +
                                        " `estautscasoindseg` = '" + sSeguimiento + "' " +
                                        " WHERE (`EstatusCasoId` = " + sEstatusCasoId +");";
                MySqlDataReader respuestastringtoiposl = con.getdatareader(sQueryupdate);
                if (respuestastringtoiposl.RecordsAffected == 1)
                {
                    //si la modificación al estatus fue correcta debemos de limpiar los campos para capturar y habilitar el botón de agregar
                    //y el datagridview
                    tb_estatusespano.Text = "";
                    tb_estatusingles.Text = "";
                    cb_seguimiento.Text = "";
                    button1.Enabled = true;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    dgv_estatuscaso.Enabled = true;
                    iniciacomponentes();
                }
                else
                {
                    MessageBox.Show("Hubo un error al intentar agregar el estatus");
                    new filelog("Agregar estatus CatEstatus.cs 93", "Hubo un error al intentar agregar el estatus");
                }
                respuestastringtoiposl.Read();
                respuestastringtoiposl.Close();
            }catch(Exception Ex){
                new filelog("Update guardar cambios", "Error:"+Ex);
            }
            
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            try {
                tb_estatusespano.Text = "";
                tb_estatusingles.Text = "";
                cb_seguimiento.Text = "";
                button1.Enabled = true;
                button2.Enabled = false;
                button3.Enabled = false;
                dgv_estatuscaso.Enabled = true;
            }catch(Exception Ex){
                new filelog("", "Error: "+Ex.Message);
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try {
                String sEstatusCasoId = dgv_estatuscaso.SelectedRows[0].Cells[0].Value.ToString();
                if (sEstatusCasoId!="")
                {
                    conect con = new conect();
                    String sQuerydelete = "DELETE FROM `estatuscaso` WHERE (`EstatusCasoId` = '" + sEstatusCasoId + "');";
                    MySqlDataReader respuestastringtoiposl = con.getdatareader(sQuerydelete);
                    if (respuestastringtoiposl.RecordsAffected == 1)
                    {
                        MessageBox.Show("el registro se eliminó correctamente");
                        iniciacomponentes();
                        tb_estatusespano.Text = "";
                        tb_estatusingles.Text = "";
                        cb_seguimiento.Text = "";
                        button1.Enabled = true;
                        button2.Enabled = false;
                        button3.Enabled = false;
                        dgv_estatuscaso.Enabled = true;
                        dgv_estatuscaso.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Error al intentar eliminar el registro");
                    }
                }
            }catch(Exception Ex){
                new filelog("eliminar estatus", "Error: "+Ex.Message);
            }
                

        }

    }
}

