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

namespace Facturador.Modulo_facturacion
{
    public partial class relaciona_subtipodoocumento_concepto_tarifa : Form
    {
        funcionesdicss fun_dicss = new funcionesdicss();
        public relaciona_subtipodoocumento_concepto_tarifa()
        {
            InitializeComponent();
            conect con_2 = new conect();
            String sGetescritos = "select * from subtipodocumento where subtipodocumento.TipoDocumentoId = 2 order by SubTipoDocumentoDescrip Asc;";
            MySqlDataReader resp_get_Escritos = con_2.getdatareader(sGetescritos);
            while (resp_get_Escritos.Read())
            {
                cbEscritos_subtipodocumento.Items.Add(fun_dicss.validareader("SubTipoDocumentoDescrip", "SubTipoDocumentoId", resp_get_Escritos));
            }
            resp_get_Escritos.Close();
            con_2.Cerrarconexion();

            conect con_3 = new conect();
            String sGetconceptotarifas = "select * from tarifa_conceptos_king order by concepto;";
            MySqlDataReader resp_get_tarifas = con_3.getdatareader(sGetconceptotarifas);
            while (resp_get_tarifas.Read())
            {
                cb_conceptoratifa.Items.Add(fun_dicss.validareader("concepto", "tarifa_conceptos_kingid", resp_get_tarifas));
            }
            resp_get_tarifas.Close();
            con_3.Cerrarconexion();

            /*cargamos las relaciones existentes actuales*/
            conect con_4 = new conect();
            String squeryrelacion = " SELECT  " +
                                    "     * " +
                                    " FROM " +
                                    "     relacion_subtipodocumento_tarifa, " +
                                    "     subtipodocumento, " +
                                    "     tarifa_conceptos_king " +
                                    " WHERE " +
                                    "     relacion_subtipodocumento_tarifa.id_subtipodocumentoid = subtipodocumento.SubTipoDocumentoId " +
                                    "         AND relacion_subtipodocumento_tarifa.id_tarifa_concepto = tarifa_conceptos_king.id_concepto; ";
            MySqlDataReader resp_get_relacion = con_4.getdatareader(squeryrelacion);
            while (resp_get_relacion.Read())
            {
                String relacion_subtipodocumento_tarifaid = fun_dicss.validareader("relacion_subtipodocumento_tarifaid", "relacion_subtipodocumento_tarifaid", resp_get_relacion).Text;
                String SubTipoDocumentoId = fun_dicss.validareader("SubTipoDocumentoId", "relacion_subtipodocumento_tarifaid", resp_get_relacion).Text;
                String id_concepto = fun_dicss.validareader("id_concepto", "relacion_subtipodocumento_tarifaid", resp_get_relacion).Text;
                String SubTipoDocumentoDescrip = fun_dicss.validareader("SubTipoDocumentoDescrip", "relacion_subtipodocumento_tarifaid", resp_get_relacion).Text;
                String concepto = fun_dicss.validareader("concepto", "relacion_subtipodocumento_tarifaid", resp_get_relacion).Text;
                dgv_relacionsubtipoconceptotarifa.Rows.Add(relacion_subtipodocumento_tarifaid, SubTipoDocumentoId, id_concepto, SubTipoDocumentoDescrip, concepto);
                
            }
            resp_get_relacion.Close();
            con_4.Cerrarconexion();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try {
                String sSubtipodocumentoid = (cbEscritos_subtipodocumento.SelectedItem as ComboboxItem).Value.ToString();
                String sConceptoid = (cb_conceptoratifa.SelectedItem as ComboboxItem).Value.ToString();
                String sQueryinsert = " INSERT INTO `relacion_subtipodocumento_tarifa`" +
                                        " (`relacion_subtipodocumento_tarifaid`," +
                                        " `id_subtipodocumentoid`," +
                                        " `id_tarifa_concepto`)" +
                                        " VALUES" +
                                        " (''," +
                                        " '" + sSubtipodocumentoid + "'," +
                                        " '" + sConceptoid + "');";
                conect con_insert = new conect();
                MySqlDataReader resp_get_insert = con_insert.getdatareader(sQueryinsert);
                if (resp_get_insert.RecordsAffected == 1)
                {
                    String sSubtipodocumentoid_text = (cbEscritos_subtipodocumento.SelectedItem as ComboboxItem).Text;
                    String sConceptoid_text = (cb_conceptoratifa.SelectedItem as ComboboxItem).Text;
                    String sidinsertado = "";
                    conect con_3 = new conect();
                    String sGetconceptotarifas = "select * from relacion_subtipodocumento_tarifa order by relacion_subtipodocumento_tarifaid desc limit 1;";
                    MySqlDataReader resp_get_tarifas = con_3.getdatareader(sGetconceptotarifas);
                    while (resp_get_tarifas.Read())
                    {
                        sidinsertado = fun_dicss.validareader("relacion_subtipodocumento_tarifaid", "relacion_subtipodocumento_tarifaid", resp_get_tarifas).Text;
                    }
                    resp_get_tarifas.Close();
                    con_3.Cerrarconexion();
                    dgv_relacionsubtipoconceptotarifa.Rows.Add(sidinsertado, sSubtipodocumentoid, sConceptoid, sSubtipodocumentoid_text, sConceptoid_text);

                }
                else
                {
                    MessageBox.Show("Relación agregada correctamente.");
                }

                resp_get_insert.Close();
                con_insert.Cerrarconexion();
            }catch(Exception E){
                MessageBox.Show("Error al intentar agregar la relación "+E.Message);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void carga_relaciones(){
            /*cargamos las relaciones existentes actuales*/
            conect con_4 = new conect();
            String squeryrelacion = " SELECT  " +
                                    "     * " +
                                    " FROM " +
                                    "     relacion_subtipodocumento_tarifa, " +
                                    "     subtipodocumento, " +
                                    "     tarifa_conceptos_king " +
                                    " WHERE " +
                                    "     relacion_subtipodocumento_tarifa.id_subtipodocumentoid = subtipodocumento.SubTipoDocumentoId " +
                                    "         AND relacion_subtipodocumento_tarifa.id_tarifa_concepto = tarifa_conceptos_king.id_concepto; ";
            MySqlDataReader resp_get_relacion = con_4.getdatareader(squeryrelacion);
            dgv_relacionsubtipoconceptotarifa.Rows.Clear();
            while (resp_get_relacion.Read())
            {
                String relacion_subtipodocumento_tarifaid = fun_dicss.validareader("relacion_subtipodocumento_tarifaid", "relacion_subtipodocumento_tarifaid", resp_get_relacion).Text;
                String SubTipoDocumentoId = fun_dicss.validareader("SubTipoDocumentoId", "relacion_subtipodocumento_tarifaid", resp_get_relacion).Text;
                String id_concepto = fun_dicss.validareader("id_concepto", "relacion_subtipodocumento_tarifaid", resp_get_relacion).Text;
                String SubTipoDocumentoDescrip = fun_dicss.validareader("SubTipoDocumentoDescrip", "relacion_subtipodocumento_tarifaid", resp_get_relacion).Text;
                String concepto = fun_dicss.validareader("concepto", "relacion_subtipodocumento_tarifaid", resp_get_relacion).Text;
                dgv_relacionsubtipoconceptotarifa.Rows.Add(relacion_subtipodocumento_tarifaid, SubTipoDocumentoId, id_concepto, SubTipoDocumentoDescrip, concepto);

            }
            resp_get_relacion.Close();
            con_4.Cerrarconexion();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String ids = "";
            var confirmResult = MessageBox.Show("¿Seguro que desea " + dgv_relacionsubtipoconceptotarifa.SelectedRows.Count + " eliminar relaciones seleccionadas? ", "Confirmación de eliminar relación", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                for (int x = 0; x < dgv_relacionsubtipoconceptotarifa.SelectedRows.Count; x++)
                {

                    ids += dgv_relacionsubtipoconceptotarifa.SelectedRows[x].Cells[0].Value.ToString()+",";
                }
                //MessageBox.Show(ids);
                if (ids != "")
                {
                    ids = ids.Substring(0, ids.Length - 1);
                    conect con_3 = new conect();
                    String sGetconceptotarifas = "delete from relacion_subtipodocumento_tarifa where relacion_subtipodocumento_tarifaid in(" + ids + ");";
                    MySqlDataReader resp_get_tarifas = con_3.getdatareader(sGetconceptotarifas);
                    
                    if (resp_get_tarifas.RecordsAffected>0)
                    {
                        MessageBox.Show(resp_get_tarifas.RecordsAffected+" Registro(s) eliminado(s) correctamente.");
                        //solo falta remover el grid
                        carga_relaciones();
                    }
                    resp_get_tarifas.Close();
                    con_3.Cerrarconexion();
                }
                else {
                    MessageBox.Show("Debe seleccionar por lo menos un registro para poder eliminar.");
                }
                
            }
            else
            {
                // If 'No', do something here.
            }
            
        }
    }
}
