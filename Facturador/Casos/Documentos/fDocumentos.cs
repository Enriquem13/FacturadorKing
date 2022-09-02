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

namespace Facturador.Casos.Documentos
{
    public partial class fDocumentos : Form
    {
        funcionesdicss funcionesgenerales = new funcionesdicss();
        public fDocumentos()
        {
            InitializeComponent();
            cargartiposplazos();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //agregar tipoplazo
            try
            {
                if (tbDescripcion.Text.Trim() != ""
                    && !(cbGrupoplazo.SelectedItem is null)
                    && !(cbHabilitado.SelectedItem is null)
                    && !(cbGrupo.SelectedItem is null))
                {
                    //cargamos los tipoplazos existentes
                    conect con_plazos = new conect();
                    String squery_plazos = " INSERT INTO `tipoplazo` " +
                                            " ( " +
                                            " `TipoPlazoDescrip`, " +
                                            " `GrupoPlazoId`, " +
                                            " `TipoPlazoIndAct`, " +
                                            " `Grupoid`) " +
                                            " VALUES " +
                                            " ( " +
                                            " '" + tbDescripcion.Text + "', " +
                                            " '" + (cbGrupoplazo.SelectedItem as ComboboxItem).Value + "', " +
                                            " '" + (cbHabilitado.SelectedItem as ComboboxItem).Value + "', " +
                                            " '" + (cbGrupo.SelectedItem as ComboboxItem).Value + "' " +
                                            " ); ";
                    MySqlDataReader resp_plazos = con_plazos.getdatareader(squery_plazos);
                    if (resp_plazos.RecordsAffected > 0)
                    {
                        MessageBox.Show("Tipo plazo agregado correctamente.");
                        tbDescripcion.Text = "";
                        cbGrupoplazo.Text = "";
                        cbHabilitado.Text = "";
                        cbGrupo.Text = "";
                        cargartiposplazos();

                    }
                    else
                    {
                        MessageBox.Show("Verifica la información.");
                    }

                    resp_plazos.Close();
                    con_plazos.Cerrarconexion();
                }
                else
                {
                    MessageBox.Show("Debe llenar todos los datos");
                }
            }
            catch (Exception exs)
            {
                MessageBox.Show("Debe llenar todos los datos");
            }
        }

        public void cargartiposplazos()
        {
            try
            {
                //cargamos los tipoplazos existentes
                conect con_plazos = new conect();
                String squery_plazos = " select documento.*, tipodocumento.TipoDocumentoDescrip, subtipodocumento.SubTipoDocumentoDescrip  from documento  " +
                                        " left join subtipodocumento on documento.SubTipoDocumentoId = subtipodocumento.subtipodocumentoid " +
                                        " left join tipodocumento on subtipodocumento.TipoDocumentoId = tipodocumento.TipoDocumentoId order by DocumentoId desc limit 10 ";
                MySqlDataReader resp_plazos = con_plazos.getdatareader(squery_plazos);
                dgvTioplazos.Rows.Clear();
                while (resp_plazos.Read())
                {
                    //String sPlazohabilitado = "Inhabilitado";
                    //if (funcionesgenerales.validareader("TipoPlazoIndAct", "TipoPlazoIndAct", resp_plazos).Text == "1")
                    //    sPlazohabilitado = "habilitado";
                    dgvTioplazos.Rows.Add(int.Parse(funcionesgenerales.validareader("DocumentoId", "DocumentoId", resp_plazos).Text),
                                                    funcionesgenerales.validareader("DocumentoCodigoBarras", "DocumentoCodigoBarras", resp_plazos).Text,
                                                    funcionesgenerales.validareader("SubTipoDocumentoId", "SubTipoDocumentoId", resp_plazos).Text,
                                                    funcionesgenerales.validareader("TipoDocumentoDescrip", "TipoDocumentoDescrip", resp_plazos).Text,
                                                    funcionesgenerales.validareader("SubTipoDocumentoDescrip", "SubTipoDocumentoDescrip", resp_plazos).Text,
                                                    funcionesgenerales.validareader("DocumentoFecha", "DocumentoFecha", resp_plazos).Text,
                                                    funcionesgenerales.validareader("DocumentoFolio", "DocumentoFolio", resp_plazos).Text,
                                                    funcionesgenerales.validareader("DocumentoFechaRecepcion", "DocumentoFechaRecepcion", resp_plazos).Text,
                                                    funcionesgenerales.validareader("DocumentoFechaVencimiento", "DocumentoFechaVencimiento", resp_plazos).Text,
                                                    funcionesgenerales.validareader("DocumentoFechaCaptura", "DocumentoFechaCaptura", resp_plazos).Text,
                                                    funcionesgenerales.validareader("DocumentoFechaEscaneo", "DocumentoFechaEscaneo", resp_plazos).Text,
                                                    funcionesgenerales.validareader("DocumentoObservacion", "DocumentoObservacion", resp_plazos).Text,
                                                    funcionesgenerales.validareader("DocumentoIdRef", "DocumentoIdRef", resp_plazos).Text,
                                                    funcionesgenerales.validareader("UsuarioId", "UsuarioId", resp_plazos).Text,
                                                    funcionesgenerales.validareader("CompaniaMensajeriaId", "CompaniaMensajeriaId", resp_plazos).Text,
                                                    funcionesgenerales.validareader("DocumentoFechaEnvio", "DocumentoFechaEnvio", resp_plazos).Text,
                                                    funcionesgenerales.validareader("DocumentoNumeroGuia", "DocumentoNumeroGuia", resp_plazos).Text,
                                                    funcionesgenerales.validareader("DocumentoFechaEntrega", "DocumentoFechaEntrega", resp_plazos).Text,
                                                    funcionesgenerales.validareader("foliodocumentocontesto", "foliodocumentocontesto", resp_plazos).Text,
                                                    funcionesgenerales.validareader("codigobarrasdoccontesto", "codigobarrasdoccontesto", resp_plazos).Text,
                                                    funcionesgenerales.validareader("usuarioIdPreparo", "usuarioIdPreparo", resp_plazos).Text,
                                                    funcionesgenerales.validareader("casoid", "casoid", resp_plazos).Text,
                                                    funcionesgenerales.validareader("TipoSolicitudId", "TipoSolicitudId", resp_plazos).Text,
                                                    funcionesgenerales.validareader("RelacionDocumentoLink", "RelacionDocumentoLink", resp_plazos).Text

                );
                }
                resp_plazos.Close();
                con_plazos.Cerrarconexion();
            }
            catch (Exception ecs)
            {
                MessageBox.Show("Warning:"+ ecs.Message);
            }

        }

        public void cargardatos()
        {
            try
            {
                cargartiposplazos();
                //cargamos en el combobox los grupos existentes de los tipoplazos

                //cargamos los grupos existentes para los grupos de casos
                conect con_grupo = new conect();
                String squery_grupo = "select * from grupo;";
                MySqlDataReader resp_grupo = con_grupo.getdatareader(squery_grupo);
                while (resp_grupo.Read())
                {
                    cbGrupo.Items.Add(funcionesgenerales.validareader("GrupoDescripcion", "GrupoId", resp_grupo));
                }
                resp_grupo.Close();
                con_grupo.Cerrarconexion();


                conect con_grupo_plazos = new conect();
                String sQuerygrupoplazos = "select * from grupoplazo;";
                MySqlDataReader resp_grupo_plazos = con_grupo_plazos.getdatareader(sQuerygrupoplazos);
                while (resp_grupo_plazos.Read())
                {
                    ComboboxItem cbItme = new ComboboxItem();
                    cbItme.Value = funcionesgenerales.validareader("GrupoPlazoId", "GrupoPlazoId", resp_grupo_plazos).ToString();
                    cbItme.Text = funcionesgenerales.validareader("GrupoPlazoDescripcion", "GrupoPlazoDescripcion", resp_grupo_plazos).ToString();
                    cbGrupoplazo.Items.Add(cbItme);
                }
                resp_grupo_plazos.Close();
                con_grupo_plazos.Cerrarconexion();

                //cargamos en el combobox habilitdo

                ComboboxItem cbItme1 = new ComboboxItem();
                cbItme1.Value = 1;
                cbItme1.Text = "Habilitado";
                ComboboxItem cbItme2 = new ComboboxItem();
                cbItme2.Value = 0;
                cbItme2.Text = "Inhabilitado";

                cbHabilitado.Items.Add(cbItme1);
                cbHabilitado.Items.Add(cbItme2);

            }
            catch (Exception exs)
            {
                new filelog("Agregar tipoplazo exception ", " error: " + exs.ToString());
            }
        }

        private void catTipoplazos_Load(object sender, EventArgs e)
        {

        }

        private void dgvTioplazos_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                //obtenemos los valores del front para poder modificarlos
                if (dgvTioplazos.SelectedRows.Count > 0)
                {
                    String stipoplazoid = "", sDescripcion = "", sGrupoplazos = "", sGrupo = "", sHabilitado = "";
                    stipoplazoid = dgvTioplazos.SelectedRows[0].Cells[0].Value.ToString();
                    sDescripcion = dgvTioplazos.SelectedRows[0].Cells[1].Value.ToString();
                    sGrupoplazos = dgvTioplazos.SelectedRows[0].Cells[2].Value.ToString();
                    sGrupo = dgvTioplazos.SelectedRows[0].Cells[3].Value.ToString();
                    sHabilitado = dgvTioplazos.SelectedRows[0].Cells[4].Value.ToString();

                    tbTipoplazoid.Text = stipoplazoid;
                    tbDescripcion.Text = sDescripcion;
                    cbGrupoplazo.Text = sGrupoplazos;
                    cbGrupo.Text = sGrupo;
                    cbHabilitado.Text = sHabilitado;

                    //despues de captar los datos debemos hacer el update habilitar botones
                    btnModificar.Enabled = true;
                    btnAgregar.Enabled = false;
                }


            }
            catch (Exception exs)
            {

            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            //una vez seleccionado el registro del datagridview se habailitara este boton para que podamos hacer el update con el dato del id
            try
            {
                if (tbDescripcion.Text.Trim() != ""
                    && !(cbGrupoplazo.SelectedItem is null)
                    && !(cbHabilitado.SelectedItem is null)
                    && !(cbGrupo.SelectedItem is null))
                {
                    //cargamos los tipoplazos existentes
                    conect con_plazos = new conect();
                    String squery_plazos = " UPDATE `tipoplazo`" +
                                            " SET" +
                                            " `TipoPlazoDescrip` = '" + tbDescripcion.Text + "'," +
                                            " `TipoPlazoIndAct` = '" + (cbHabilitado.SelectedItem as ComboboxItem).Value + "'," +
                                            " `GrupoPlazoId` = '" + (cbGrupoplazo.SelectedItem as ComboboxItem).Value + "'," +
                                            " `Grupoid` = '" + (cbGrupo.SelectedItem as ComboboxItem).Value + "'" +
                                            " WHERE `TipoPlazoId` = '" + tbTipoplazoid.Text + "';";

                    MySqlDataReader resp_plazos = con_plazos.getdatareader(squery_plazos);
                    if (resp_plazos.RecordsAffected > 0)
                    {
                        MessageBox.Show("Tipo plazo modificado correctamente.");
                        tbTipoplazoid.Text = "";
                        tbDescripcion.Text = "";
                        cbGrupoplazo.Text = "";
                        cbHabilitado.Text = "";
                        cbGrupo.Text = "";
                        cargartiposplazos();
                        btnAgregar.Enabled = true;
                        btnModificar.Enabled = false;

                    }
                    else
                    {
                        MessageBox.Show("Verifica la información.");
                    }
                    resp_plazos.Close();
                    con_plazos.Cerrarconexion();
                }
                else
                {
                    MessageBox.Show("Debe seleccionar todos los campos para poder modificar.");
                }
            }
            catch (Exception exs)
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //obtenemos los valores del front para poder modificarlos
                if (dgvTioplazos.SelectedRows.Count > 0)
                {
                    DialogResult result = MessageBox.Show("Seguro que desea eliminar el tipo plazo con descripción: \n\n" +
                                                        dgvTioplazos.SelectedRows[0].Cells[1].Value.ToString() +
                                                        "?", "Eliminar", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (result.Equals(DialogResult.OK))
                    {
                        conect con_plazos = new conect();
                        String squery_plazos = " delete from `tipoplazo`" +
                                                " WHERE `TipoPlazoId` = '" + dgvTioplazos.SelectedRows[0].Cells[0].Value.ToString() + "';";

                        MySqlDataReader resp_plazos = con_plazos.getdatareader(squery_plazos);
                        if (resp_plazos.RecordsAffected > 0)
                        {
                            MessageBox.Show("Tipo plazo modificado correctamente.");
                            cargartiposplazos();
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un registro de la lista.");
                }
            }
            catch (Exception exs)
            {
                new filelog("", "" + exs.Message);
            }
        }
    }
}
