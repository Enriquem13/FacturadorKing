using MySql.Data.MySqlClient;
//using MySql.Data.MySqlClient;
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
    public partial class tarifas : Form
    {
        Form loginobj;
        captura capturaform;

        public tarifas(Form login, captura cap)
        {
            loginobj = login;
            capturaform = cap;
            InitializeComponent();

            conect con3 = new conect();
            String query3 = "SELECT *" +
                            " FROM grupo;";
            MySqlDataReader respuestastring3 = con3.getdatareader(query3);
            String sGrupoid = "";
            String sGrupodesc = "";

            while (respuestastring3.Read())
            {
                if (!respuestastring3.IsDBNull(respuestastring3.GetOrdinal("GrupoId")))
                {
                    sGrupoid = respuestastring3.GetString(respuestastring3.GetOrdinal("GrupoId"));
                }
                else
                {
                    sGrupoid = "";
                }
                if (!respuestastring3.IsDBNull(respuestastring3.GetOrdinal("GrupoDescripcion")))
                {
                    sGrupodesc = respuestastring3.GetString(respuestastring3.GetOrdinal("GrupoDescripcion"));
                }
                else
                {
                    sGrupodesc = "";
                }
                comboGrupo.Items.Add(sGrupoid + " - " + sGrupodesc);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            capturaform.Show();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //capturaform.Close();
            capturaform.Close();
            loginobj.Close();
            this.Close();
        }

        private void comboGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            String[] grupoSeleccionado = comboGrupo.Text.Split('-');
            comboEstatus.Items.Clear();
            comboEstatus.Text = "";
            conect con3 = new conect();
            String query3 = "select estatuscaso.EstatusCasoId, estatuscaso.EstatusCasoDescrip " +
                            "from estatuscaso, grupoestatuscaso " +
                            "where grupoestatuscaso.GrupoId = " + grupoSeleccionado[0] + " and " +
                            "grupoestatuscaso.EstatusCasoId = estatuscaso.EstatusCasoId;";
            MySqlDataReader respuestastring3 = con3.getdatareader(query3);
            String sEstatusCasoId = "";
            String sEstatusCasoDescrip = "";

            while (respuestastring3.Read())
            {
                if (!respuestastring3.IsDBNull(respuestastring3.GetOrdinal("EstatusCasoId")))
                {
                    sEstatusCasoId = respuestastring3.GetString(respuestastring3.GetOrdinal("EstatusCasoId"));
                }
                else
                {
                    sEstatusCasoId = "";
                }
                if (!respuestastring3.IsDBNull(respuestastring3.GetOrdinal("EstatusCasoDescrip")))
                {
                    sEstatusCasoDescrip = respuestastring3.GetString(respuestastring3.GetOrdinal("EstatusCasoDescrip"));
                }
                else
                {
                    sEstatusCasoDescrip = "";
                }
                comboEstatus.Items.Add(sEstatusCasoId + " - " + sEstatusCasoDescrip);

            }
        }

        private void comboEstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            String[] grupoSeleccionado = comboEstatus.Text.Split('-');
            comboEstatus.Items.Clear();
            comboEstatus.Text = "";
            conect con3 = new conect();
            String query3 = "select estatuscaso.EstatusCasoId, estatuscaso.EstatusCasoDescrip " +
                            "from estatuscaso, grupoestatuscaso " +
                            "where grupoestatuscaso.GrupoId = " + grupoSeleccionado[0] + " and " +
                            "grupoestatuscaso.EstatusCasoId = estatuscaso.EstatusCasoId;";
            MySqlDataReader respuestastring3 = con3.getdatareader(query3);
            String sEstatusCasoId = "";
            String sEstatusCasoDescrip = "";

            while (respuestastring3.Read())
            {
                if (!respuestastring3.IsDBNull(respuestastring3.GetOrdinal("EstatusCasoId")))
                {
                    sEstatusCasoId = respuestastring3.GetString(respuestastring3.GetOrdinal("EstatusCasoId"));
                }
                else
                {
                    sEstatusCasoId = "";
                }
                if (!respuestastring3.IsDBNull(respuestastring3.GetOrdinal("EstatusCasoDescrip")))
                {
                    sEstatusCasoDescrip = respuestastring3.GetString(respuestastring3.GetOrdinal("EstatusCasoDescrip"));
                }
                else
                {
                    sEstatusCasoDescrip = "";
                }
                comboConceptocargo.Items.Add(sEstatusCasoId + " - " + sEstatusCasoDescrip);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }   

        
    }
}
