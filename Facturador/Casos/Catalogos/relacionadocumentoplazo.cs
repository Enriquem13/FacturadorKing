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
    public partial class relacionadocumentoplazo : Form
    {
        funcionesdicss objfuncionesdicss = new funcionesdicss();
        String sSubtipodocumentoid = "";
        public relacionadocumentoplazo()
        {
            InitializeComponent();
            //Agregamos el tipo documento
            conect con_tipodoc = new conect();
            String squery_tipodoc = "SELECT * FROM tipodocumento;";
            MySqlDataReader resp_ripodoc = con_tipodoc.getdatareader(squery_tipodoc);
            cbTipodocumento.Items.Clear();
            while (resp_ripodoc.Read())
            {
                cbTipodocumento.Items.Add(objfuncionesdicss.validareader("TipoDocumentoDescrip", "TipoDocumentoId", resp_ripodoc));
            }
            resp_ripodoc.Close();
            con_tipodoc.Cerrarconexion();


            //Agregamos el grupo plazo
            conect con_grupoplazo = new conect();
            String squeryareaimpi = "select * from grupoplazo;";
            MySqlDataReader resp_grupoplazo = con_grupoplazo.getdatareader(squeryareaimpi);
            cbGrupoPlazo.Items.Clear();
            while (resp_grupoplazo.Read())
            {
                cbGrupoPlazo.Items.Add(objfuncionesdicss.validareader("GrupoPlazoDescripcion", "GrupoPlazoId", resp_grupoplazo));
            }
            resp_grupoplazo.Close();
            con_grupoplazo.Cerrarconexion();

        }

        private void btn_Salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbTipodocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            //buscamos según el tipo de documento seleccionado
            //Agregamos el tipo documento
            try {
                Object oTipodocumentoid = (cbTipodocumento.SelectedItem as ComboboxItem).Value;
                conect con_tipodoc = new conect();
                String squery_tipodoc = "select * from subtipodocumento where tipodocumentoid = " + oTipodocumentoid;
                MySqlDataReader resp_ripodoc = con_tipodoc.getdatareader(squery_tipodoc);
                cbSubtipodocumento.Text = "";
                cbSubtipodocumento.Items.Clear();
                while (resp_ripodoc.Read())
                {
                    cbSubtipodocumento.Items.Add(objfuncionesdicss.validareader("SubTipoDocumentoDescrip", "SubTipoDocumentoId", resp_ripodoc));
                    
                }
                resp_ripodoc.Close();
                con_tipodoc.Cerrarconexion();
            }catch(Exception Ex){
            
            }
            
        }

        private void cbGrupoPlazo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Agregamos el grupo plazo
            try { 
            
                Object oGrupoplazo = (cbGrupoPlazo.SelectedItem as ComboboxItem).Value;
                conect con_grupoplazo = new conect();
                String squeryareaimpi = "select * from tipoplazo where grupoplazoid = " + oGrupoplazo+" ;";
                MySqlDataReader resp_grupoplazo = con_grupoplazo.getdatareader(squeryareaimpi);
                cbTipoplazo.Items.Clear();
                cbTipoplazo.Text = "";
                while (resp_grupoplazo.Read())
                {
                    cbTipoplazo.Items.Add(objfuncionesdicss.validareader("TipoPlazoDescrip", "tipoplazoid", resp_grupoplazo));
                }
                resp_grupoplazo.Close();
                con_grupoplazo.Cerrarconexion();
            }catch(Exception Ex){
            }
        }

        private void btn_consultar_Click(object sender, EventArgs e)
        {
            try {
                //Consultamos los plazos relacionados a éste documento, los que GENERAN éstos documentos
                Object oSubtipodocumentoid = (cbSubtipodocumento.SelectedItem as ComboboxItem).Value;
                String sSubtipodocumentoid = (cbSubtipodocumento.SelectedItem as ComboboxItem).Text;
                conect con_grupoplazo = new conect();

                String squeryareaimpi = " SELECT " +
                                        " tipoplazo.tipoplazoid, " +
                                        " tipoplazo.TipoPlazoDescrip" +
                                        " FROM " +
                                        " relacion_plazo_subtipodocumento, " +
                                        " tipoplazo" +
                                        " where subtipodocumentoid = " + oSubtipodocumentoid +
                                        " AND tipoplazo.TipoPlazoId = relacion_plazo_subtipodocumento.tipoplazoid;";
                MySqlDataReader resp_grupoplazo = con_grupoplazo.getdatareader(squeryareaimpi);
                dgTipoplazosgenra.Rows.Clear();
                while (resp_grupoplazo.Read())
                {
                    dgTipoplazosgenra.Rows.Add(objfuncionesdicss.validareader("TipoPlazoDescrip", "tipoplazoid", resp_grupoplazo).Value, objfuncionesdicss.validareader("TipoPlazoDescrip", "tipoplazoid", resp_grupoplazo).Text);
                    //cbGrupoPlazo.Items.Add(objfuncionesdicss.validareader("TipoPlazoDescrip", "tipoplazoid", resp_grupoplazo));
                }
                tbDocumento.Text = sSubtipodocumentoid;
                resp_grupoplazo.Close();
                con_grupoplazo.Cerrarconexion();
            }catch(Exception Ex){
                MessageBox.Show("Debe seleccionar un documento.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try {
                Object oGrupoplazo = (cbGrupoPlazo.SelectedItem as ComboboxItem).Value;
                conect con_grupoplazo = new conect();
                String squeryareaimpi = " INSERT INTO `relacion_plazo_subtipodocumento` " +
                                        " (`relacion_plazo_subtipodocumento`, " +
                                        " `tipoplazoid`, " +
                                        " `subtipodocumentoid`, " +
                                        " `activo`) " +
                                        " VALUES " +
                                        " ('' , " +
                                        " '' , " +
                                        " '' , " +
                                        " '' ); ";
                MySqlDataReader resp_grupoplazo = con_grupoplazo.getdatareader(squeryareaimpi);
                dgTipoplazosgenra.Rows.Clear();
            }catch(Exception Ex){
            }
        }
    }
}
