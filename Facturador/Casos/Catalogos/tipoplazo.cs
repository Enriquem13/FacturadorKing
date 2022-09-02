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
    public partial class tipoplazo : Form
    {
        funcionesdicss funcionesgenerales = new funcionesdicss();
        public tipoplazo()
        {
            InitializeComponent();
            iniciacomponentes();
        }

        public void iniciacomponentes() {
            //seleccionamos las opciones de grupos
            conect con_grupo = new conect();
            String squery_grupo = "select * from grupo;";
            MySqlDataReader resp_grupo = con_grupo.getdatareader(squery_grupo);
            while (resp_grupo.Read())
            {
                cbGrupo.Items.Add(funcionesgenerales.validareader("GrupoDescripcion", "GrupoId", resp_grupo));
            }
            resp_grupo.Close();
            con_grupo.Cerrarconexion();
            //FIN seleccionamos las opciones de grupos

            //seleccionamos las opciones de grupos
            conect con_grupo_plazo = new conect();
            String squery_grupo_plazo = "SELECT * FROM grupoplazo;";
            MySqlDataReader resp_grupo_plazo = con_grupo_plazo.getdatareader(squery_grupo_plazo);
            while (resp_grupo_plazo.Read())
            {
                cbgrupo_plazo.Items.Add(funcionesgenerales.validareader("GrupoPlazoDescripcion", "GrupoPlazoId", resp_grupo_plazo));
            }
            resp_grupo_plazo.Close();
            con_grupo_plazo.Cerrarconexion();
            //FIN seleccionamos las opciones de grupos


            //seleccionamos las opciones de tareas
            conect con_tarea = new conect();
            String squery_tarea = "select * from tipotarea;";
            MySqlDataReader resp_tarea = con_tarea.getdatareader(squery_tarea);
            while (resp_tarea.Read())
            {
                cbTarea.Items.Add(funcionesgenerales.validareader("TipoTareaDescrip", "TipoTareaId", resp_tarea));
            }
            resp_tarea.Close();
            con_tarea.Cerrarconexion();
            //FIN seleccionamos las opciones de tareas


            //cargamos los tipo de plazos existentes
            String sConsultaplazos = " select * from  " +
                                        " tipoplazo;  ";
                                        //" tipotarea,  " +
                                        //" grupo,  " +
                                        //" grupoplazo  " +
                                        //" where  " +
                                        //" tipoplazo.Grupoid = grupo.grupoid " +
                                        //" AND tipoplazo.TipoTareaId = tipotarea.TipoTareaId " +
                                        //" AND tipoplazo.GrupoPlazoId = grupoplazo.GrupoPlazoId; ";
            conect conin_plazos = new conect();
            MySqlDataReader respuestastrig_plazos = conin_plazos.getdatareader(sConsultaplazos);
            int iRows = 0;
            dataGridView1.Rows.Clear();
            while (respuestastrig_plazos.Read())
            {
                String sTipoPlazoId = funcionesgenerales.validareader("TipoPlazoId", "TipoPlazoId", respuestastrig_plazos).Text;
                String sTipoPlazoDescrip = funcionesgenerales.validareader("TipoPlazoDescrip", "TipoPlazoId", respuestastrig_plazos).Text;
                String sTipoTareaId = funcionesgenerales.validareader("TipoTareaId", "TipoPlazoId", respuestastrig_plazos).Text;
                String sGrupoPlazoId = funcionesgenerales.validareader("GrupoPlazoId", "TipoPlazoId", respuestastrig_plazos).Text;
                String sGrupoId = funcionesgenerales.validareader("Grupoid", "TipoPlazoId", respuestastrig_plazos).Text;

                String sGrupoPlazoDescripcion = funcionesgenerales.getitembycomobobox(cbgrupo_plazo, sGrupoPlazoId).Text;
                String sGrupoDescripcion = funcionesgenerales.getitembycomobobox(cbGrupo, sGrupoId).Text;
                String sTareadescrip = funcionesgenerales.getitembycomobobox(cbTarea, sTipoTareaId).Text;

                //Tipo plazo, tarea, Grupo plazo, Grupo
                DataGridViewRow RowItem = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                RowItem.Cells[0].Value = sTipoPlazoId;//Tipo plazo
                RowItem.Cells[1].Value = sTipoPlazoDescrip;//Tipo plazo
                RowItem.Cells[2].Value = sTareadescrip;//tarea
                RowItem.Cells[3].Value = sGrupoPlazoDescripcion;//Grupo plazo
                RowItem.Cells[4].Value = sGrupoDescripcion;//Grupo
                dataGridView1.Rows.Add(RowItem);
                iRows++;
            }
            iRowscount.Text = iRows + "";
            respuestastrig_plazos.Close();
            conin_plazos.Cerrarconexion();
            //FIN cargamos los tipo de plazos existentes
            //ocultamos el id para que no sea visible
            dataGridView1.Columns["tipoplazoid"].Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try {
                String sTipoplazoid = dataGridView1.SelectedCells[0].Value.ToString();//tipo plazo
                String sTipoplazodescrip = dataGridView1.SelectedCells[1].Value.ToString();//tipo plazo
                String sDescriptarea = dataGridView1.SelectedCells[2].Value.ToString();//tarea
                String sDescripgrupoplazo = dataGridView1.SelectedCells[3].Value.ToString();//grupo plazo
                String sDescripGrupo = dataGridView1.SelectedCells[4].Value.ToString();//grupo

                //aqui agreamos el id 
                tbDescripcionplazo.Text = sTipoplazodescrip;
                cbgrupo_plazo.Text = sDescripgrupoplazo;
                cbGrupo.Text= sDescripGrupo;
                cbTarea.Text = sDescriptarea;

                btnGuardarcambios.Enabled = true;
                btnCancelar.Enabled = true;
                btnEliminar.Enabled = true;
                btn_agregar.Enabled = false;
                dataGridView1.Enabled = false;
            }catch(Exception E){
                new filelog("Tipoplazo", E.ToString());
            }
        }

        private void btnGuardarcambios_Click(object sender, EventArgs e)
        {
            try {
                String sTipoplazoid = dataGridView1.SelectedCells[0].Value.ToString();//tipo plazo
                String plazo_descripcion = tbDescripcionplazo.Text;
                ComboboxItem oTareaid = (cbTarea.SelectedItem as ComboboxItem);
                ComboboxItem oGrupoplazoid = (cbgrupo_plazo.SelectedItem as ComboboxItem);
                ComboboxItem oGropid = (cbGrupo.SelectedItem as ComboboxItem);
                if (tbDescripcionplazo.Text != "" && oTareaid != null)
                {
                    //hacemos el update 
                    //MessageBox.Show("se modificarán" + tbDescripcionplazo.Text );
                    //UPDATE `casos_king_v1`.`tipoplazo` SET `Grupoid` = '1' WHERE (`TipoPlazoId` = '5');
                    conect con_update = new conect();
                    //validamos todos los campos del update
                    String  sNuevadescipciontipoplazo = tbDescripcionplazo.Text;//nuevo texto de descripcion
                    ComboboxItem oTareid = (cbTarea.SelectedItem as ComboboxItem);//tareaid modificada
                    ComboboxItem oGrupo_plazoid = (cbgrupo_plazo.SelectedItem as ComboboxItem);
                    ComboboxItem oGrupoid = (cbGrupo.SelectedItem as ComboboxItem);
                    String sSetquery = "";
                    if (sNuevadescipciontipoplazo != "") { sSetquery += " TipoPlazoDescrip = '" + sNuevadescipciontipoplazo + "' ,"; }
                    if (oTareid != null) { sSetquery += " TipoTareaId = " + oTareid.Value + " ,"; }
                    if (oGrupo_plazoid != null) { sSetquery += " GrupoPlazoId = " + oGrupo_plazoid.Value + " ,"; }
                    if (oGrupoid != null) { sSetquery += " Grupoid = " + oGrupoid.Value + " ,"; }

                    if (sSetquery != "" && sTipoplazoid != "")
                    {
                        sSetquery = sSetquery.Substring(0, sSetquery.Length - 1); 
                        String squery_update = "UPDATE `tipoplazo` SET " + sSetquery + " WHERE (`TipoPlazoId` = '" + sTipoplazoid + "');";
                        MySqlDataReader resp_update = con_update.getdatareader(squery_update);
                        resp_update.Read();
                        resp_update.Close();
                        con_update.Cerrarconexion();
                        //terminamos el update
                    }else{
                        MessageBox.Show("No se encontraron cambios");
                        return;
                    }
                    

                    //habilitamos y deshabilitamos los botones correspondientes
                    btnGuardarcambios.Enabled = false;
                    btnCancelar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btn_agregar.Enabled = true;
                    dataGridView1.Enabled = true;
                    //FIN habilitamos y deshabilitamos los botones correspondientes

                    //vaciamos los combobox para iniciar de nuevo
                    cbTarea.Items.Clear();
                    cbGrupo.Items.Clear();
                    cbgrupo_plazo.Items.Clear();
                    tbDescripcionplazo.Text = "";
                    cbTarea.Text = "";
                    cbGrupo.Text = "";
                    cbgrupo_plazo.Text = "";

                    //FIN vaciamos los combobox para iniciar de nuevo

                    //reiniciamos los componentes
                    iniciacomponentes();
                    //FIN reiniciamos los componentes
                }
                else 
                {
                    MessageBox.Show("Debe seleccionar Descripcion y tarea para agregar un plazo");
                }
            }catch(Exception E){
                new filelog("Tipoplazo", E.ToString());
            }
            
        }

        private void button1_Click(object sender, EventArgs e)//Boton de cancelar
        {
            //habilitamos y deshabilitamos los botones correspondientes
            btnGuardarcambios.Enabled = false;
            btnCancelar.Enabled = false;
            btnEliminar.Enabled = false;
            btn_agregar.Enabled = true;
            dataGridView1.Enabled = true;
            //habilitamos y deshabilitamos los botones correspondientes

            //ponemos los valores en vacio ya que cancelamos la modificacion
            tbDescripcionplazo.Text = "";
            cbTarea.Text = "";
            cbGrupo.Text = "";
            cbgrupo_plazo.Text = "";
            //terminamos 
        }
        
        private void btn_agregar_Click(object sender, EventArgs e)
        {
            //insertamos un nuevo tipo de plazo
            String sNuevadescipciontipoplazo = tbDescripcionplazo.Text;//nuevo texto de descripcion
            ComboboxItem oTareid = (cbTarea.SelectedItem as ComboboxItem);//tareaid modificada
            ComboboxItem oGrupo_plazoid = (cbgrupo_plazo.SelectedItem as ComboboxItem);
            ComboboxItem oGrupoid = (cbGrupo.SelectedItem as ComboboxItem);
            String sSetquery = "";

            if (sNuevadescipciontipoplazo != "")
            {
                if (oTareid == null) {
                    oTareid = new ComboboxItem();
                    oTareid.Value = "0"; 
                    oTareid.Text = ""; 
                }
                if (oGrupo_plazoid == null) { 
                    oGrupo_plazoid = new ComboboxItem();
                    oGrupo_plazoid.Value = "0"; 
                    oGrupo_plazoid.Text = ""; 
                }
                if (oGrupoid == null) { 
                    oGrupoid = new ComboboxItem();
                    oGrupoid.Value = "0"; 
                    oGrupoid.Text = ""; 
                }

                conect con_insert = new conect();
                String squery_insert = "INSERT INTO `tipoplazo` (`TipoPlazoDescrip`, `TipoTareaId`, `GrupoPlazoId`, `Grupoid`) "+
                                        "VALUES" +
                                        " ('" + sNuevadescipciontipoplazo + "', '" + oTareid.Value + "', '" + oGrupo_plazoid.Value + "', '" + oGrupoid.Value + "');";
                MySqlDataReader resp_insert = con_insert.getdatareader(squery_insert);
                resp_insert.Read();
                resp_insert.Close();
                con_insert.Cerrarconexion();

                //vaciamos los combobox para iniciar de nuevo
                cbTarea.Items.Clear();
                cbGrupo.Items.Clear();
                cbgrupo_plazo.Items.Clear();
                tbDescripcionplazo.Text = "";
                cbTarea.Text = "";
                cbGrupo.Text = "";
                cbgrupo_plazo.Text = "";

                //FIN vaciamos los combobox para iniciar de nuevo

                //reiniciamos los componentes
                iniciacomponentes();

            }
            else {

                MessageBox.Show("Debe seleccionar por lo menos la descripción para agregar un tipo de plazo.");    
            }
            //INSERT INTO `casos_king_v1`.`tipoplazo` (`TipoPlazoDescrip`, `TipoTareaId`, `GrupoPlazoId`, `Grupoid`) VALUES ('descripcion', '1', '2', '3');
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            String sTipoplazoid = dataGridView1.SelectedCells[0].Value.ToString();//tipo plazo
            String sTipoplazoDescripcion = dataGridView1.SelectedCells[1].Value.ToString();//tipo plazo
            var confirmResult = MessageBox.Show("¿Seguro que desea borrar el TipoPlazo: " + sTipoplazoDescripcion + " ?","Eliminar Tipo plazo", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                conect con_delete = new conect();
                String squery_delete = "Delete  from tipoplazo where tipoplazoid = " + sTipoplazoid + " ;";
                MySqlDataReader resp_delete = con_delete.getdatareader(squery_delete);
                //resp_delete.Read();
                resp_delete.Close();
                con_delete.Cerrarconexion();

                //vaciamos los combobox para iniciar de nuevo
                cbTarea.Items.Clear();
                cbGrupo.Items.Clear();
                cbgrupo_plazo.Items.Clear();
                tbDescripcionplazo.Text = "";
                cbTarea.Text = "";
                cbGrupo.Text = "";
                cbgrupo_plazo.Text = "";

                //FIN vaciamos los combobox para iniciar de nuevo

                //reiniciamos los componentes
                iniciacomponentes();

                //habilitamos y deshabilitamos los botones correspondientes
                btnGuardarcambios.Enabled = false;
                btnCancelar.Enabled = false;
                btnEliminar.Enabled = false;
                btn_agregar.Enabled = true;
                dataGridView1.Enabled = true;
            }
        }
       
    }
}

