
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
    public partial class Conceptos : Form
    {

        public captura capturaform;
        public Form1 Formlogin;
        public String[,] astringdates;
        public String[,] sIdvalues;
        public String [,] aaValues;
        public String[] aComboBoxGrupo;
        public Conceptos(captura cap, Form1 login)
        {
            InitializeComponent();
            this.listvRelacioneconcept.FullRowSelect = true;
            capturaform = cap;
            Formlogin = login;
            conect con = new conect();
            String query = "SELECT ConceptoCargoId, ConceptoDescripcionEsp" +
                            " FROM conceptocargo  ORDER BY `conceptocargo`.`ConceptoDescripcionEsp` ASC";
            MySqlDataReader respuestastring = con.getdatareader(query);
            

            while (respuestastring.Read())
            {
                conbobServicios.Items.Add(validareader("ConceptoDescripcionEsp", "ConceptoCargoId", respuestastring));
            }

            conect con2 = new conect();
            String query2 = "SELECT grupo.GrupoId, grupo.GrupoDescrip, grupo.GrupoDescripcion, conceptocargoestatus.ConceptoCargoId, "+
                             "conceptocargo.ConceptoDescripcionEsp,  estatuscaso.EstatusCasoDescrip, estatuscaso.EstatusCasoId " +
                             "FROM conceptocargoestatus, conceptocargo, estatuscaso, grupo " +
                             "where estatuscaso.EstatusCasoId = conceptocargoestatus.EstatusID and " +
                             "conceptocargo.ConceptoCargoId = conceptocargoestatus.conceptocargoID and " +
                             "grupo.GrupoId = conceptocargoestatus.GrupoID " +
                             ";";
            MySqlDataReader respuestastring2count = con2.getdatareader(query2);
            MySqlDataReader respuestastring2 = con2.getdatareader(query2);
            String siniGrupoId = "";
            String siniGrupoDescrip = "";
            String siniGrupoDescripcion = "";
            String siniConceptoCargoId = "";
            String siniConceptoDescripcionEsp = "";
            String siniEstatusCasoDescrip = "";
            String sEstatusCasoId = "";
            //0 es grupoid
            //1 es gurpodescripcion
            //2 es concepto cargo id
            //3 es conceptocargodesc
            //4 es EstatusCasoId
            //5 es EstatusCasoDescrip

            int iCount = 0;
            int countIntrows = 0;
            while (respuestastring2count.Read())
            {
                countIntrows++;
            }
            
            aaValues = new String[7, countIntrows];
            while (respuestastring2.Read())
            {
                if (!respuestastring2.IsDBNull(respuestastring2.GetOrdinal("GrupoId")))
                {
                    siniGrupoId = respuestastring2.GetString(respuestastring2.GetOrdinal("GrupoId"));
                    aaValues[0, iCount] = siniGrupoId;
                }
                else
                {
                    siniGrupoId = "";
                    aaValues[0, iCount] = "";
                }
                //if (!respuestastring2.IsDBNull(respuestastring2.GetOrdinal("GrupoDescrip")))
                //{
                //    siniGrupoDescrip = respuestastring2.GetString(respuestastring2.GetOrdinal("GrupoDescrip"));
                //    aaValues[1, iCount] = siniGrupoDescrip;
                //}
                //else
                //{
                //    siniGrupoDescrip = "";
                //    aaValues[1, iCount] = "";
                //}
                if (!respuestastring2.IsDBNull(respuestastring2.GetOrdinal("GrupoDescripcion")))
                {
                    siniGrupoDescripcion = respuestastring2.GetString(respuestastring2.GetOrdinal("GrupoDescripcion"));
                    aaValues[1, iCount] = siniGrupoDescripcion;
                }
                else
                {
                    aaValues[1, iCount] = siniGrupoDescripcion;
                }
                if (!respuestastring2.IsDBNull(respuestastring2.GetOrdinal("ConceptoCargoId")))
                {
                    siniConceptoCargoId = respuestastring2.GetString(respuestastring2.GetOrdinal("ConceptoCargoId"));
                    aaValues[2, iCount] = siniConceptoCargoId;
                }
                else
                {
                    siniConceptoCargoId = "";
                    aaValues[2, iCount] = "";
                }
                if (!respuestastring2.IsDBNull(respuestastring2.GetOrdinal("ConceptoDescripcionEsp")))
                {
                    siniConceptoDescripcionEsp = respuestastring2.GetString(respuestastring2.GetOrdinal("ConceptoDescripcionEsp"));
                    aaValues[3, iCount] = siniConceptoDescripcionEsp;
                }
                else
                {
                    siniConceptoDescripcionEsp = "";
                    aaValues[3, iCount] = "";
                }
                if (!respuestastring2.IsDBNull(respuestastring2.GetOrdinal("EstatusCasoId")))
                {
                    sEstatusCasoId = respuestastring2.GetString(respuestastring2.GetOrdinal("EstatusCasoId"));
                    aaValues[4, iCount] = sEstatusCasoId;
                }
                else
                {
                    sEstatusCasoId = "";
                    aaValues[4, iCount] = "";
                }
                if (!respuestastring2.IsDBNull(respuestastring2.GetOrdinal("EstatusCasoDescrip")))
                {
                    siniEstatusCasoDescrip = respuestastring2.GetString(respuestastring2.GetOrdinal("EstatusCasoDescrip"));
                    aaValues[5, iCount] = siniEstatusCasoDescrip;
                }
                else
                {
                    siniEstatusCasoDescrip = "";
                    aaValues[5, iCount] = "";
                }


                ListViewItem lCampoestatus = new ListViewItem(siniGrupoDescripcion);
                lCampoestatus.SubItems.Add(siniConceptoDescripcionEsp);
                lCampoestatus.SubItems.Add(siniEstatusCasoDescrip);
                listvRelacioneconcept.Items.Add(lCampoestatus);
                iCount++;

            }

            conect con3 = new conect();
            
            String query3 = "SELECT *" +
                            " FROM grupo;";
            MySqlDataReader respuestastring3 = con3.getdatareader(query3);
            MySqlDataReader respuestastring4 = con3.getdatareader(query3);
            int iTamanoarraygrupo = 0;
            while (respuestastring4.Read())
            {
                iTamanoarraygrupo++;
            }
            //String sGrupoid = "";
            //String sGrupodesc = "";
            int iContgurpo = 0;
            aComboBoxGrupo = new String[iTamanoarraygrupo];
            while (respuestastring3.Read())
            {
                //if (!respuestastring3.IsDBNull(respuestastring3.GetOrdinal("GrupoId")))
                //{
                //    sGrupoid = respuestastring3.GetString(respuestastring3.GetOrdinal("GrupoId"));
                //}
                //else
                //{
                //    sGrupoid = "";
                //}
                //if (!respuestastring3.IsDBNull(respuestastring3.GetOrdinal("GrupoDescripcion")))
                //{
                //    sGrupodesc = respuestastring3.GetString(respuestastring3.GetOrdinal("GrupoDescripcion"));
                //}
                //else
                //{
                //    sGrupodesc = "";
                //}
                aComboBoxGrupo[iContgurpo] = validareader("GrupoDescripcion", "GrupoId", respuestastring3).Value.ToString();
                combGrupos.Items.Add(validareader("GrupoDescripcion", "GrupoId", respuestastring3));
                iContgurpo++;
            }
        }

        private void bAgregartarifa_Click(object sender, EventArgs e)
        {
           
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            capturaform.Close();
            Formlogin.Close();
            this.Close();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //String sCliente = lCliente.Text;
            //if (!sCliente.Equals(""))
            //{
            //    ListViewItem listaagregar = new ListViewItem(sCliente);
            //    listaagregar.SubItems.Add(lConpecto.Text);
            //    listaagregar.SubItems.Add(tDerechos.Text);
            //    listaagregar.SubItems.Add(tCpeso.Text);
            //    listaagregar.SubItems.Add(tCdolar.Text);
            //    listaagregar.SubItems.Add(tCeuro.Text);
            //    if (rAct.Checked)
            //    {
            //        listaagregar.SubItems.Add("Activo");
            //    }
            //    else
            //    {
            //        listaagregar.SubItems.Add("Inactivo");
            //    }

            //    listviewtarifas.Items.Add(listaagregar);

            //}
            //else
            //{
            //    MessageBox.Show("Debe seleccionar Cliente, concepto y llenar los campos");
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            capturaform.Show();
            
        }

        private void combGrupos_SelectedIndexChanged(object sender, EventArgs e)
        {
            //String []grupoSeleccionado = combGrupos.Text.Split('-');
            comboEstatus.Items.Clear();
            comboEstatus.Text = "";
            conect con3 = new conect();
            String query3 = "select estatuscaso.EstatusCasoId, estatuscaso.EstatusCasoDescrip "+
                            "from estatuscaso, grupoestatuscaso " +
                            "where grupoestatuscaso.GrupoId = " + (combGrupos.SelectedItem as ComboboxItem).Value.ToString() + " and " +
                            "grupoestatuscaso.EstatusCasoId = estatuscaso.EstatusCasoId;";
            
            MySqlDataReader respuestastring3 = con3.getdatareader(query3);
            //String sEstatusCasoId = "";
            //String sEstatusCasoDescrip = "";

            while (respuestastring3.Read())
            {
                //if (!respuestastring3.IsDBNull(respuestastring3.GetOrdinal("EstatusCasoId")))
                //{
                //    sEstatusCasoId = respuestastring3.GetString(respuestastring3.GetOrdinal("EstatusCasoId"));
                //}
                //else
                //{
                //    sEstatusCasoId = "";
                //}
                //if (!respuestastring3.IsDBNull(respuestastring3.GetOrdinal("EstatusCasoDescrip")))
                //{
                //    sEstatusCasoDescrip = respuestastring3.GetString(respuestastring3.GetOrdinal("EstatusCasoDescrip"));
                //}
                //else
                //{
                //    sEstatusCasoDescrip = "";
                //}

                comboEstatus.Items.Add(validareader("EstatusCasoDescrip", "EstatusCasoId", respuestastring3));

            }
        }

        
        //agregar la relacion de grupo estatus y concepto de cargo
        private void button4_Click(object sender, EventArgs e)
        {
            if (!combGrupos.Text.Equals("") && !comboEstatus.Text.Equals("") && !conbobServicios.Text.Equals(""))
            {
                (combGrupos.SelectedItem as ComboboxItem).Value.ToString();
                (comboEstatus.SelectedItem as ComboboxItem).Value.ToString();
                (conbobServicios.SelectedItem as ComboboxItem).Value.ToString();

                try { 
                    //Agregamos al listview la lista que generamos al seleccionar los combobox
                    conect coninsert = new conect();
                    String queryinsert = "INSERT INTO `conceptocargoestatus` " +
                                    "(`conceptocargoestatus_id`, `GrupoID`, `EstatusID`, `conceptocargoID`) "+
                                    "VALUES (NULL, '" + (combGrupos.SelectedItem as ComboboxItem).Value.ToString() + "', '" + 
                                    (comboEstatus.SelectedItem as ComboboxItem).Value.ToString() + "', '" + 
                                    (conbobServicios.SelectedItem as ComboboxItem).Value.ToString() + "');";

                    MySqlDataReader respuestastringinsert = coninsert.getdatareader(queryinsert);

                    if (respuestastringinsert == null)
                    {
                        MessageBox.Show("La relación ya existe");
                    }
                    else {
                        ListViewItem listaagregar = new ListViewItem((combGrupos.SelectedItem as ComboboxItem).Text);
                        listaagregar.SubItems.Add((comboEstatus.SelectedItem as ComboboxItem).Text);
                        listaagregar.SubItems.Add((conbobServicios.SelectedItem as ComboboxItem).Text);
                        
                        listvRelacioneconcept.Items.Add(listaagregar);
                        Array.Resize(ref aComboBoxGrupo, aComboBoxGrupo.Length + 1);
                        aComboBoxGrupo[aComboBoxGrupo.Length-1] = (combGrupos.SelectedItem as ComboboxItem).Value.ToString();
                    }
                }catch(Exception E){
                    //escribimos en log
                    MessageBox.Show("Verifique la conexión a la red, o que la base de datos esté disponible");
                }
                

                //while (respuestastringinsert.Read())
                //{
                //    if (!respuestastringinsert.IsDBNull(respuestastringinsert.GetInt32(0)))
                //    {
                //        sRespuestainsert = respuestastringinsert.GetString(respuestastringinsert.GetInt32(0));
                //    }
                //    else
                //    {
                //        sRespuestainsert = "";
                //    }
                //}
            }
            else {
                MessageBox.Show("Debe seleccionar los campos Grupo, estatus y concepto cargo");
            }
            //listaagregar.SubItems.Add(tCpeso.Text);
        }

        private void listvRelacioneconcept_SelectedIndexChanged(object sender, EventArgs e)
        {
            //String namn = this.listvRelacioneconcept.SelectedItems[0].Text;
            //MessageBox.Show("El renglo solicitado es:"+namn);
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try { 
                String grupo = listvRelacioneconcept.SelectedItems[0].SubItems[0].Text;
                String estatus = listvRelacioneconcept.SelectedItems[0].SubItems[1].Text;
                String conceptocargo = listvRelacioneconcept.SelectedItems[0].SubItems[2].Text;
                //0 es grupoid
                //1 es gurpodescripcion
                //2 es concepto cargo id
                //3 es conceptocargodesc
                //4 es EstatusCasoId
                //5 es EstatusCasoDescrip
                String uy = aaValues[0, 5];
                ComboboxItem cItemresult = new ComboboxItem();
                if (listvRelacioneconcept.SelectedItems.Count > 0)
                {
                    int num = listvRelacioneconcept.Items.IndexOf(listvRelacioneconcept.SelectedItems[0]);
                    //MessageBox.Show(num.ToString());
                    cItemresult.Text = grupo;
                    cItemresult.Value = aaValues[0,num];
                    //combGrupos.Text = cItemresult.Text;
                     //aaValues[0, num];
                    //combGrupos.Items.Add(cItemresult);
                    //combGrupos.SelectedItem = grupo;

                    int indicegrupo = Array.IndexOf<String>(aComboBoxGrupo, aaValues[0, num]);
                    combGrupos.SelectedIndex = indicegrupo; 


                }else{
                    MessageBox.Show("Debe seleccionar un registro de la lista para modificar");
                }



                

                //combGrupos.Items.Add(cItemresult);
                //combGrupos.Text = id_grupo + " - " + descrip_grupo;
                //comboEstatus.Text = id_grupo + " - " + descrip_grupo;
                //conbobServicios.Text = id_grupo + " - " + descrip_grupo;
                //combGrupos.Tag = "valor tag";
                //comboEstatus.Text = estatus_caso;
                //combGrupos.SelectedValue = "valor del grupo puede ser el id";
                //conbobServicios.Text = concepto_cargo;
                //MessageBox.Show("value de estatus:" + combGrupos.SelectedValue);

                //listvRelacioneconcept.SelectedItems[0].Remove();
            }catch(Exception E){
                MessageBox.Show("Debe seleccionar un registro de la lista para poder modificar "+E);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //combGrupos.Text = 
            //comboEstatus.Text = 
            //conbobServicios.Text = 

            //conbobServicios.SelectedIndex = 0;
            MessageBox.Show((conbobServicios.SelectedItem as ComboboxItem).Value.ToString());
            MessageBox.Show("Se guardaron los cambios");
        }

        public ComboboxItem validareader(String campoText,String campoValue, MySqlDataReader mresultado){
            
            ComboboxItem cItemresult =  new ComboboxItem();

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

        public ListboxItemss validareaderlist(String campoText, String campoValue, MySqlDataReader mresultado)
        {

            ListboxItemss cItemresult = new ListboxItemss();

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

    }
    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
    public class ListboxItemss
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
