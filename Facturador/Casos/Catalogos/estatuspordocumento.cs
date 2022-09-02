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
    public partial class estatuspordocumento : Form
    {
        funcionesdicss obj_dicss = new funcionesdicss();
        public estatuspordocumento()
        {
            InitializeComponent();
            iniciacomponentes();
        }

        public void iniciacomponentes() {
            try {
                //iniciamos la consultra de los estatus que son generados por los subtipodocumentos
                conect con = new conect();
                String sQuerytipoSol = "SELECT  " +
                                        "    sbes.SubTipoDocumentoEstatusCasoId, " +
                                        "    subt.SubTipoDocumentoId, " +
                                        "    subt.SubTipoDocumentoDescrip, " +
                                        "    esta.EstatusCasoId, " +
                                        "    esta.EstatusCasoDescrip, " +
                                        "    grupo.GrupoDescripcion " +
                                        "FROM " +
                                        "    subtipodocumentoestatuscaso sbes, " +
                                        "    subtipodocumento subt, " +
                                        "    estatuscaso esta, " +
                                        "    grupo " +
                                        "WHERE " +
                                        "    sbes.SubTipoDocumentoIndTipo = subt.SubTipoDocumentoIndTipo " +
                                        "        AND sbes.EstatusCasoId = esta.EstatusCasoId " +
                                        "        AND sbes.grupoid = grupo.grupoid; ";

                MySqlDataReader respuestastringtoiposl = con.getdatareader(sQuerytipoSol);
                dgv_estatuscasodocument.Rows.Clear();
                while (respuestastringtoiposl.Read())
                {
                    //cb_estatusid  SubTipoDocumentoIndTipo
                    //SubTipoDocumentoDescrip
                    //cbtipodedocumento.Items.Add(obj_dicss.validareader("SubTipoDocumentoDescrip", "SubTipoDocumentoId", respuestastringtoiposl));
                    String sSubTipoDocumentoEstatusCasoId = obj_dicss.validareader("SubTipoDocumentoEstatusCasoId", "SubTipoDocumentoId", respuestastringtoiposl).Text;
                    String sSubTipoDocumentoId = obj_dicss.validareader("SubTipoDocumentoId", "SubTipoDocumentoId", respuestastringtoiposl).Text;
                    String sSubTipoDocumentoDescrip = obj_dicss.validareader("SubTipoDocumentoDescrip", "SubTipoDocumentoId", respuestastringtoiposl).Text;
                    String sEstatusCasoId = obj_dicss.validareader("EstatusCasoId", "SubTipoDocumentoId", respuestastringtoiposl).Text;
                    String sEstatusCasoDescrip = obj_dicss.validareader("EstatusCasoDescrip", "SubTipoDocumentoId", respuestastringtoiposl).Text;
                    String sGrupoDescripcion = obj_dicss.validareader("GrupoDescripcion", "SubTipoDocumentoId", respuestastringtoiposl).Text;
                    dgv_estatuscasodocument.Rows.Add(sSubTipoDocumentoEstatusCasoId, sEstatusCasoId, sSubTipoDocumentoId, sSubTipoDocumentoDescrip, sEstatusCasoDescrip, sGrupoDescripcion);
                    
                }
                respuestastringtoiposl.Close();
                con.Cerrarconexion();

                //seleccionamos las opciones de grupos
                conect con_grupo = new conect();
                String squery_grupo = "select * from grupo;";
                MySqlDataReader resp_grupo = con_grupo.getdatareader(squery_grupo);
                while (resp_grupo.Read())
                {
                    cb_grupo.Items.Add(obj_dicss.validareader("GrupoDescripcion", "GrupoId", resp_grupo));
                }
                resp_grupo.Close();
                con_grupo.Cerrarconexion();

                //cargamos tipo de documento
                conect con_tarea = new conect();
                String squery_tarea = "select * from tipodocumento;";
                MySqlDataReader resp_tarea = con_tarea.getdatareader(squery_tarea);
                cb_tipodocumento.Items.Clear();
                while (resp_tarea.Read())
                {
                    cb_tipodocumento.Items.Add(obj_dicss.validareader("TipoDocumentoDescrip", "TipoDocumentoId", resp_tarea));
                }
                resp_tarea.Close();
                con_tarea.Cerrarconexion();
                

                //seleccionamos las opciones de subtipodocumento
                conect con_estatus = new conect();
                String squery_estatus = "select * from estatuscaso;";
                MySqlDataReader resp_estatus = con_estatus.getdatareader(squery_estatus);
                cb_estatusid.Items.Clear();
                while (resp_estatus.Read())
                {
                    cb_estatusid.Items.Add(obj_dicss.validareader("EstatusCasoDescrip", "EstatusCasoId", resp_estatus));
                }
                resp_estatus.Close();
                con_estatus.Cerrarconexion();
                //FIN seleccionamos las opciones de subtipodocumento
                
            }catch(Exception Ex){
                new filelog("estatusdocuemnto", Ex.Message);
            }
        }

        private void btn_agregarreñlacion_Click(object sender, EventArgs e)
        {

        }

        private void btn_salir_estatusdoc_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cb_tipodocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            try {

                object sidtipodoc = (cb_tipodocumento.SelectedItem as ComboboxItem).Value;
                //seleccionamos las opciones de subtipodocumento
                conect con_tarea = new conect();
                String squery_tarea = "select * from subtipodocumento where TipoDocumentoId = " + sidtipodoc;
                MySqlDataReader resp_tarea = con_tarea.getdatareader(squery_tarea);
                cb_subtipodocumentoid.Items.Clear();
                while (resp_tarea.Read())
                {
                    cb_subtipodocumentoid.Items.Add(obj_dicss.validareader("SubTipoDocumentoDescrip", "SubTipoDocumentoId", resp_tarea));
                }
                resp_tarea.Close();
                con_tarea.Cerrarconexion();
                //FIN seleccionamos las opciones de subtipodocumento
            }catch(Exception Ex){
                new filelog("estatusdocuemnto", Ex.Message);
            }
            
        }

        private void btn_modificar_Click(object sender, EventArgs e)
        {

        }

    }
}
