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
    public partial class subtipodocumentoestatus : Form
    {
        public subtipodocumentoestatus()
        {
            InitializeComponent();
            //Cargamos El combobox con la información de la tabla grupo
            conect conect = new conect();
            String sQgrupos = "select * from grupo;";
            MySqlDataReader resp_grupos = conect.getdatareader(sQgrupos);
            while (resp_grupos.Read())
            {
                cbGrupo.Items.Add(validareader("GrupoDescripcion", "GrupoId", resp_grupos));
                //cbGrupo.Text = validareader("GrupoDescripcion", "GrupoId", resp_grupos).Text;
            }
            cbGrupo.Text = "";
            resp_grupos.Close();
            conect.Cerrarconexion();

            //cargamos el combobox de todos los Estatus existentes de la tabla estatuscaso
            conect conect2 = new conect();
            String sQEstatus = "select * from estatuscaso;";
            MySqlDataReader resp_tEstatus = conect2.getdatareader(sQEstatus);
            while (resp_tEstatus.Read())
            {
                cbEstatus.Items.Add(validareader("estatuscasodescrip", "estatuscasoid", resp_tEstatus));
                cbEstatus.Text = "";
                //cbGrupo.Text = validareader("GrupoDescripcion", "GrupoId", resp_grupos).Text;
            }
            resp_tEstatus.Close();
            conect2.Cerrarconexion();

            //cargamos el combobox de todos los tipos de documentos existentes de la tabla Tipodocumento
            conect conect3 = new conect();
            String sQTipodocumento = "select * from tipodocumento;";
            MySqlDataReader resp_tDoc = conect3.getdatareader(sQTipodocumento);
            while (resp_tDoc.Read())
            {
                cbTipodocumento.Items.Add(validareader("TipoDocumentoDescrip", "TipoDocumentoId", resp_tDoc));
                cbTipodocumento.Text = "";
                //cbGrupo.Text = validareader("GrupoDescripcion", "GrupoId", resp_grupos).Text;
            }
            resp_tDoc.Close();
            conect3.Cerrarconexion();

            
        }
        public ComboboxItem validareader(String campoText, String campoValue, MySqlDataReader mresultado)
        {
            ComboboxItem cItemresult = new ComboboxItem();

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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            //Agregamos la información de los estauscaso disponibles y activos

            //buscamos los estatos que se tienen para este grupo en la tabla estatuscasosubtipodocumento
            conect conect2 = new conect();
            String sQestatuscaso = "SELECT * FROM estatuscasosubtipodocumento, estatuscaso where GrupoId = " + (cbGrupo.SelectedItem as ComboboxItem).Value.ToString() + " and estatuscasosubtipodocumento.estatuscasoid = estatuscaso.estatuscasoid and  estatuscaso.estautscasoindseg =1 group by estatuscaso.estatuscasoid";
            MySqlDataReader resp_estatus = conect2.getdatareader(sQestatuscaso);
            cbEstatus.Items.Clear();
            while (resp_estatus.Read())
            {
                cbEstatus.Items.Add(validareader("EstatusCasoDescrip", "EstatusCasoId", resp_estatus));
                cbEstatus.Text = "";
                //cbEstatus.Text = validareader("EstatusCasoDescrip", "EstatusCasoId", resp_estatus).Text;
            }
            resp_estatus.Close();
            conect2.Cerrarconexion();

            //conect conect = new conect();
            //String sQgrupos = "select * from grupo;";
            //MySqlDataReader resp_grupos = conect.getdatareader(sQgrupos);
            //while (resp_grupos.Read())
            //{
            //    cbGrupo.Items.Add(validareader("GrupoDescripcion", "GrupoId", resp_grupos));
            //    cbGrupo.Text = validareader("GrupoDescripcion", "GrupoId", resp_grupos).Text;
            //}
            //resp_grupos.Close();
            //conect.Cerrarconexion();
            //comboboxSubtipo.Items.Clear();
            //comboboxSubtipo.Text = "";
            //comboboxSubtipo2.Items.Clear();
            //comboboxSubtipo2.Text = "";
            //conect con = new conect();
            //String query = "select SubTipoSolicitudId, SubTipoSolicitudDescripcion from subtiposolicitud where tiposolicitudID =" + (cbGrupo.SelectedItem as ComboboxItem).Value.ToString();
            //MySqlDataReader respuestastring = con.getdatareader(query);
            //while (respuestastring.Read())
            //{
            //    comboboxSubtipo.Items.Add(validareader("SubTipoSolicitudDescripcion", "SubTipoSolicitudId", respuestastring));
            //}
        }
    }
}
