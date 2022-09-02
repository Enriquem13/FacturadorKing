using MySql.Data.MySqlClient;
using SpreadsheetLight;
using SpreadsheetLight.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Facturador
{
    public partial class bMarcas : Form
    {
        public captura captura;
        public Form1 fLoguin;
        public String[] paises = new String[250];
        public String[] paisesclave = new String[250];
        public String sGTipocaso;
        public Image obj = null;
        public MySqlDataReader respuestastring3_2;
        funcionesdicss objfuncionesdicss = new funcionesdicss();
        public bMarcas(int iGrupo, captura capturaform, Form1 loguin)
        {
            try
            {
                fLoguin = loguin;
                captura = capturaform;
                InitializeComponent();

                objfuncionesdicss.activaaviso(tbAvisoprueba);

                this.BackColor = Color.FromArgb(255, 255, 192);
                conect contipo = new conect();
                String sQuerytipoSol = "select * from tiposolicitud where TipoSolicitudGrupo= " + iGrupo;
                sGTipocaso = iGrupo + "";
                MySqlDataReader respuestastringtoiposl = contipo.getdatareader(sQuerytipoSol);
                while (respuestastringtoiposl.Read())
                {
                    cbTiposolicitud.Items.Add(validareader("TipoSolicitudDescrip", "TipoSolicitudId", respuestastringtoiposl));
                }
                respuestastringtoiposl.Close();
                contipo.Cerrarconexion();


                try {
                    conect con_clases = new conect();
                    String sQueryclases = "select * from clasificadornizavigente order by CasoProductosClase;";
                    MySqlDataReader respuesta_clases = con_clases.getdatareader(sQueryclases);
                    while (respuesta_clases.Read())
                    {
                        cbClase.Items.Add(validareader("CasoProductosClase", "CasoProductosClase", respuesta_clases));
                    }
                    cbClase.Text = "";
                    respuesta_clases.Close();
                    con_clases.Cerrarconexion();
                }
                catch (Exception exs) {
                    new filelog(" linea 60: ", " Error: "+exs.StackTrace);
                }

                //String sQuerypais = "select * from pais  order by PaisNombre;";
                //MySqlDataReader respuestastringpais = con_dos.getdatareader(sQuerypais);
                //while (respuestastringpais.Read())
                //{
                //    ComboboxItem combopias = new ComboboxItem();
                //    combopias.Text = validareader("PaisClave", "PaisId", respuestastringpais).Text + " - " + validareader("PaisNombre", "PaisId", respuestastringpais).Text;
                //    combopias.Value = validareader("PaisClave", "PaisId", respuestastringpais).Value;

                //    //cbPais.Items.Add(validareader("PaisNombre", "PaisId", respuestastringpais));
                //    cbPais.Items.Add(combopias);

                //}
                //respuestastringpais.Close();
                //con_dos.Cerrarconexion();

                //cbHolder
                conect con_holder = new conect();
                String query_holder = "select * from holder order by HolderNombre;";
                MySqlDataReader respuestastringo_holder = con_holder.getdatareader(query_holder);
                while (respuestastringo_holder.Read())
                {
                    ComboboxItem combopias_hold = new ComboboxItem();
                    combopias_hold.Text = validareader("HolderId", "HolderId", respuestastringo_holder).Text + " - " + validareader("HolderNombre", "HolderNombre", respuestastringo_holder).Text;
                    combopias_hold.Value = validareader("HolderNombre", "HolderNombre", respuestastringo_holder).Value;
                    cbHolder.Items.Add(combopias_hold);
                }
                respuestastringo_holder.Close();
                con_holder.Cerrarconexion();
                //FIN HOLDER

                conect con_dos = new conect();
                String query5 = "select PaisNombre, PaisId, PaisClave from pais order by PaisNombre;";
                MySqlDataReader respuestastringopais = con_dos.getdatareader(query5);
                //int paisint = 0;
                while (respuestastringopais.Read())
                {
                    ComboboxItem combopias = new ComboboxItem();
                    combopias.Text = validareader("PaisClave", "PaisId", respuestastringopais).Text + " - " + validareader("PaisNombre", "PaisId", respuestastringopais).Text;
                    combopias.Value = validareader("PaisClave", "PaisId", respuestastringopais).Value;

                    //cbPais.Items.Add(validareader("PaisNombre", "PaisId", respuestastringopais));
                    cbPais.Items.Add(combopias);
                    paises[Convert.ToInt32(validareader("PaisClave", "PaisId", respuestastringopais).Value.ToString())] = validareader("PaisClave", "PaisId", respuestastringopais).Text;
                    paisesclave[Convert.ToInt32(validareader("PaisClave", "PaisId", respuestastringopais).Value.ToString())] = validareader("PaisNombre", "PaisId", respuestastringopais).Text;
                    //paisint++;
                }
                respuestastringopais.Close();
                con_dos.Cerrarconexion();


                //20220427FSV Cargamos Estatus del caso para Filtrar
                conect con_estatus = new conect();
                String sGetids = "SELECT " +
                                    "    estatuscaso.*, " +
                                    " grupoid " +
                                    " FROM " +
                                    "    grupoestatuscaso, " +
                                    "    estatuscaso " +
                                    " WHERE " +
                                    "    grupoestatuscaso.EstatusCasoId = estatuscaso.EstatusCasoId " +
                                    "        AND grupoid = " + sGTipocaso +
                                    " GROUP BY estatuscaso.EstatusCasoId" +
                                    " order by estatuscaso.EstatusCasoDescrip;";

                MySqlDataReader resp_getestatus = con_estatus.getdatareader(sGetids);
                while (resp_getestatus.Read())
                {
                    cbFiltroestatus.Items.Add(validareader("EstatusCasoDescrip", "estatuscasoid", resp_getestatus));
                }
                resp_getestatus.Close();
                con_estatus.Cerrarconexion();

                //20220427FSV



            }
            catch (Exception E)
            {
                new filelog(fLoguin.sId, E.ToString());
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
            }
            catch (Exception Ex) {
                cItemresult.Text = "";
                cItemresult.Value = "";

            }
            
            return cItemresult;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            if (validaversion(fLoguin.sVersion))
            {
                return;
            }
            //borramos el listview 

            //listViewCasos.Items.Clear();
            dgvBuscamarca.Rows.Clear();
            int rowcolor = 0;

            try
            {
                String sQuerywhere = "";
                String sQuerywherecaso = "";
                String stablaconsulta = "";

                //if (cbTiposolicitud.SelectedItem != null)
                //{
                //    sQuerywhere += " AND caso_marcas.tiposolicitudid = " + (cbTiposolicitud.SelectedItem as ComboboxItem).Value;
                //    sQuerywhere += " AND Tiposolicitud.tiposolicitudid = caso_marcas.tiposolicitudid";
                //    stablaconsulta += ",Tiposolicitud";
                //}
                ///TipoSolicitudDescrip
                if (cbTiposolicitud.SelectedItem != null)
                {
                    sQuerywhere += " AND TipoSolicitudDescrip = '" + (cbTiposolicitud.SelectedItem as ComboboxItem).Text+"'";
                }


                if (!string.IsNullOrEmpty(tbexpediente.Text))
                {
                    sQuerywhere += " AND CasoNumeroExpedienteLargo like '%" + tbexpediente.Text + "%'";
                }


                if (!string.IsNullOrEmpty(tbDenominacion.Text))
                {
                    sQuerywhere += " AND ( CasoTituloingles like '%" + tbDenominacion.Text + "%' OR CasoTituloespanol LIKE '%" + tbDenominacion.Text + "%')";
                }


                if (!string.IsNullOrEmpty(tbregistro.Text))
                {
                    sQuerywhere += " AND CasoNumConcedida like '%" + tbregistro.Text + "%'";
                }


                if (!string.IsNullOrEmpty(tbCasoid.Text))
                {
                    sQuerywhere += " AND CasoId =" + tbCasoid.Text;
                }


                if (!string.IsNullOrEmpty(textBoxCasonumero.Text))
                {
                    sQuerywhere += " AND CasoNumero like '%" + textBoxCasonumero.Text + "%'";
                }


                if (cbPais.SelectedItem != null)
                {
                    sQuerywhere += " AND PaisId = '" + (cbPais.SelectedItem as ComboboxItem).Value + "'";
                    //sQuerywhere += " AND Pais.PaisId = caso_marcas.PaisId";
                    //stablaconsulta += ",Pais";
                }

                if (cbHolder.SelectedItem != null){
                    sQuerywhere += " AND HolderName like '%" + (cbHolder.SelectedItem as ComboboxItem).Value + "%'";
                }

                if (cbClase.SelectedItem != null)
                {
                    sQuerywhere += " AND CasoProductosClase like '%" + (cbClase.SelectedItem as ComboboxItem).Value + "%'";
                }

                if (tbCliente.Text !=""){
                    sQuerywhere += " AND NombreUtilClient like '%" + tbCliente.Text + "%'";
                }

                if (tbreferencia.Text !="") {
                    sQuerywhere += " AND referencia like '%" + tbreferencia.Text + "%'";
                }

                if (tbInteresado.Text != "")
                {
                    sQuerywhere += " AND InteresadoNombre like '%" + tbInteresado.Text + "%'";
                }

                if (tbPrioridad.Text != "")
                {
                    sQuerywhere += " AND PrioridadNumero like '%" + tbPrioridad.Text + "%'";
                }
                //
                

                if (sQuerywhere != "" || sQuerywherecaso != "")
                {
                    /*Validamos si está el check de la imagen de la marca*/
                    if (cbLogo.Checked)
                    {
                        sQuerywhere += " AND consulta_casosmarcas.Nombre_logo != '' ";
                        dgvBuscamarca.Columns[17].Visible = true;
                    }
                    else
                    {
                        dgvBuscamarca.Rows[0].Height = 22;
                        dgvBuscamarca.Columns[17].Visible = false;
                    }
                    conect con_marcas = new conect();
                    sQuerywhere = sQuerywhere.Substring(4, sQuerywhere.Length - 4);
                    String sQuerybusqueda = "";
                    sQuerybusqueda = " SELECT * " +
                                        " FROM " +
                                        " consulta_casosmarcas" +
                                        stablaconsulta +
                                        " WHERE " +
                                        sQuerywhere;
                    MySqlDataReader respuestastring3 = con_marcas.getdatareader(sQuerybusqueda);
                    while (respuestastring3.Read())
                    {
                        String sCasoidconsulta = validareader("CasoId", "CasoId", respuestastring3).Text;
                        String sPrioridades = validareader("PrioridadNumero", "CasoId", respuestastring3).Text;
                        String stiposolicitud = validareader("TipoSolicitudId", "CasoId", respuestastring3).Text;

                        String sQueryintere = validareader("InteresadoNombre", "CasoId", respuestastring3).Text;
                        int residuo = rowcolor % 2;
                        //buscamos clases en productos
                        String sMarcasdescription = validareader("CasoProductosClase", "CasoId", respuestastring3).Text;//CasoProductosClase
                        String sClientesdatos = validareader("NombreUtilClient", "CasoId", respuestastring3).Text;
                        String sPaisclave = validareader("PaisClave", "CasoId", respuestastring3).Text;
                        String sTiposolicitudDescrip = validareader("TipoSolicitudDescrip", "CasoId", respuestastring3).Text;
                        String sReferenciacaso = validareader("referencia", "CasoId", respuestastring3).Text;
                        String sTitulodenomin = validareader("CasoTituloingles", "CasoId", respuestastring3).Text;
                        String TipoMarcaDescrip = validareader("TipoMarcaDescrip", "CasoId", respuestastring3).Text;
                        String sCasoFechaVigencia = validareader("CasoFechaVigencia", "CasoId", respuestastring3).Text;
                        String sCasoFechaDeclaUso = validareader("CasoFechaDeclaUso", "CasoId", respuestastring3).Text;
                        String sCasoFechaRenova = validareader("CasoFechaRenova", "CasoId", respuestastring3).Text;
                        String sLeydescript = validareader("Leydescript", "CasoId", respuestastring3).Text;


                        DateTime dFechavigencia = DateTime.MinValue;
                        DateTime dCasoFechaRenova = DateTime.MinValue;
                        DateTime dCasoFechaDeclaUso = DateTime.MinValue; //Convert.ToDateTime(sCasoFechaDeclaUso);


                        if (sCasoFechaDeclaUso != "")
                        {
                            dCasoFechaDeclaUso = Convert.ToDateTime(sCasoFechaDeclaUso);
                        }

                        if (sCasoFechaRenova != "")
                        {
                            dCasoFechaRenova = Convert.ToDateTime(sCasoFechaRenova);
                        }

                        if (sCasoFechaVigencia != "")
                        {
                            dFechavigencia = Convert.ToDateTime(sCasoFechaVigencia);
                        }
                        DateTime dFechapresentacion = DateTime.MinValue;
                        try
                        {
                            if (validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text != "00-00-0000")
                            {
                                String fecha = validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text;
                                dFechapresentacion = Convert.ToDateTime(fecha);
                            }
                        }
                        catch (Exception exdate)
                        {
                            new filelog("CasoFechaRecepcion", exdate.Message);
                        }


                        DateTime dFechaconcesion = DateTime.MinValue;
                        String sFechaconcesion = validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text;
                        try
                        {
                            
                            if (sFechaconcesion != "00-00-0000" && sFechaconcesion!="")
                            {
                                dFechaconcesion = Convert.ToDateTime(sFechaconcesion);
                            }
                        }
                        catch (Exception exdates)
                        {
                            new filelog("CasoFechaConcesion", exdates.Message);
                        }

                        /*Validamos el checkbox de logos de marcas*///Nombre_logo
                        string simageFilename = validareader("Nombre_logo", "CasoId", respuestastring3).Text;////@"C:\\facturador_lalo\\029875.gif";
                        Image animatedImage = null;
                        String simagedescripction = "";
                        if (simageFilename != "")
                        {
                            MemoryStream mArrayimg = new MemoryStream(File.ReadAllBytes(simageFilename));
                            animatedImage = Image.FromStream(mArrayimg);
                            mArrayimg.Dispose();
                            mArrayimg.Close();
                            simagedescripction = simageFilename;
                        }
                        /*Fin Validamos el checkbox de logos de marcas*/
                        if (!cbLogo.Checked)
                        {
                            dgvBuscamarca.Rows[0].Height = 22;
                        }

                        DataGridViewRow dRows = (DataGridViewRow)dgvBuscamarca.Rows[0].Clone();
                        DataGridViewImageCell cellimage = new DataGridViewImageCell();// (DataGridViewImageCell)dRows.Cells[16];
                        cellimage.Value = animatedImage;
                        cellimage.Description = simagedescripction;
                        cellimage.ImageLayout = DataGridViewImageCellLayout.Stretch;

                        dRows.Cells[0].Value = sPaisclave;
                        dRows.Cells[1].Value = sMarcasdescription;
                        dRows.Cells[2].Value = sCasoidconsulta;
                        dRows.Cells[3].Value = validareader("CasoNumero", "CasoId", respuestastring3).Text;
                        dRows.Cells[4].Value = sTiposolicitudDescrip;//estatus
                        dRows.Cells[5].Value = TipoMarcaDescrip;
                        dRows.Cells[6].Value = sLeydescript;
                        dRows.Cells[7].Value = validareader("Estatuscasodescript_", "CasoId", respuestastring3).Text; //dFechapresentacion; //fecha presentacion
                        dRows.Cells[8].Value = dFechapresentacion;
                        dRows.Cells[9].Value = validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text;
                        dRows.Cells[10].Value = dFechaconcesion;

                        dRows.Cells[11].Value = dCasoFechaDeclaUso;//Decla estricta
                        dRows.Cells[12].Value = dFechavigencia;//Vigencia
                        dRows.Cells[13].Value = dCasoFechaRenova;//Renova

                        dRows.Cells[14].Value = validareader("CasoNumConcedida", "CasoId", respuestastring3).Text;
                        dRows.Cells[15].Value = sQueryintere;// dFechaconcesion;//fecha Concesión
                        dRows.Cells[16].Value = sTitulodenomin;

                        dRows.Cells[17] = cellimage;

                        dRows.Cells[18].Value = sClientesdatos;
                        dRows.Cells[19].Value = sPrioridades;
                        dRows.Cells[20].Value = sReferenciacaso;
                        dRows.Cells[21].Value = validareader("HolderName", "CasoId", respuestastring3).Text;

                        cellimage = new DataGridViewImageCell();//para vaciarlo
                        animatedImage = null;

                        validafondo(residuo, dRows, "CasoFechaDeclaUso", 11, respuestastring3);
                        validafondo(residuo, dRows, "CasoFechaVigencia", 12, respuestastring3);
                        validafondo(residuo, dRows, "CasoFechaRenova", 13, respuestastring3);

                        if (residuo == 0)
                        {
                            dRows.DefaultCellStyle.BackColor = Color.LightGray;
                            if (validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text.Trim() != "00-00-0000" && validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text.Trim() != "")
                            {
                                dRows.Cells[8].Style.ForeColor = Color.Black;
                            }
                            else
                            {
                                dRows.Cells[8].Style.ForeColor = Color.LightGray;
                            }

                            if (validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text.Trim() != "00-00-0000" && validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text.Trim() != "")
                            {
                                dRows.Cells[10].Style.ForeColor = Color.Black;
                            }
                            else
                            {
                                dRows.Cells[10].Style.ForeColor = Color.LightGray;
                            }
                        }
                        else
                        {
                            dRows.DefaultCellStyle.BackColor = Color.Azure;
                            if (validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text.Trim() != "00-00-0000" && validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text.Trim() != "")
                            {
                                dRows.Cells[8].Style.ForeColor = Color.Black;
                            }
                            else
                            {
                                dRows.Cells[8].Style.ForeColor = Color.Azure;
                            }

                            if (validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text.Trim() != "00-00-0000" && validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text.Trim() != "")
                            {
                                dRows.Cells[10].Style.ForeColor = Color.Black;
                            }
                            else
                            {
                                dRows.Cells[10].Style.ForeColor = Color.Azure;
                            }
                        }

                        dgvBuscamarca.Rows.Add(dRows);
                        //HolderName
                        //listaitems.SubItems.Add(validareader("HolderName", "CasoId", respuestastring3).Text);
                        //listViewCasos.Items.Add(listaitems);
                        //this.listViewCasos.FullRowSelect = true;
                        rowcolor++;
                        try
                        {
                            configuracionfiles objfile = new configuracionfiles();
                            objfile.configuracionfilesinicio();
                            String sFileexist = objfile.sFileupload + @"\logos_marcas\0" + sCasoidconsulta + @"\0" + sCasoidconsulta + ".gif";
                            if (File.Exists(sFileexist))
                            {
                                //aqui buscamos el logo si existe en la carpeta y lo insertamos
                                //y preguntamos si ya existe en la base para agregarlo
                                int icount = 0;

                                conect con_imglogo = new conect();
                                String simglogo = "select count(*) As num from imagen_logo where casoid = " + sCasoidconsulta + " and TipoSolicitudId = " + stiposolicitud + ";";
                                MySqlDataReader resp_imglogo = con_imglogo.getdatareader(simglogo);
                                while (resp_imglogo.Read())
                                {
                                    icount = int.Parse(objfuncionesdicss.validareader("num", "num", resp_imglogo).Text);// objfuncionesdicss.validareader("num", "num", resp_imglogo));//consultar idioma
                                }
                                resp_imglogo.Close();
                                con_imglogo.Cerrarconexion();

                                if (icount == 0)
                                { //quiere decir que no está agregado en la base y hay que agregarlo y mostrar posteriomente con la nueva ruta y nombre
                                  //INSERT

                                    String sDatetime = DateTime.Now.ToString("ddMMyyyyHHmmss").ToString();

                                    String sRutaInsert = objfile.sFileupload + @"\logos_marcas\0" + sCasoidconsulta + @"\0" + sCasoidconsulta + "_" + sDatetime + ".gif";
                                    System.IO.File.Copy(sFileexist, sRutaInsert, true);
                                    System.IO.File.Delete(sFileexist);
                                    conect con_insert_imglogo = new conect();
                                    String simglogo_insert = "INSERT INTO `imagen_logo`(`ruta`,`casoid`,`TipoSolicitudId`,`fecha`)VALUES('" + sRutaInsert.Replace("\\", "\\\\") + "','" + sCasoidconsulta + "','" + stiposolicitud + "',now());" + ";";
                                    MySqlDataReader resp_insert_imglogo = con_insert_imglogo.getdatareader(simglogo_insert);
                                    if (resp_insert_imglogo.RecordsAffected > 0)
                                    {//quiere decir que hicimos el insert correctamente
                                        obj = Image.FromFile(sRutaInsert);
                                        pbDimage.Image = obj;
                                    }
                                    resp_insert_imglogo.Close();
                                    con_insert_imglogo.Cerrarconexion();
                                }
                                else
                                {//si tiene más de uno lo consultamos y lo colocamos en el picturebox

                                    String simglogo_consulta = "select * from imagen_logo where casoid = " + sCasoidconsulta + " and TipoSolicitudId = " + stiposolicitud + " order by fecha desc limit 1;";
                                    conect con_consul_imglogo = new conect();
                                    MySqlDataReader resp_consul_imglogo = con_consul_imglogo.getdatareader(simglogo_consulta);
                                    if (resp_consul_imglogo.Read())
                                    {//quiere decir que hicimos el insert correctamente
                                        String sRutaactual = objfuncionesdicss.validareader("ruta", "ruta", resp_consul_imglogo).Text;
                                        obj = Image.FromFile(sRutaactual); ;//
                                        pbDimage.Image = obj;
                                    }
                                    resp_consul_imglogo.Close();
                                    con_consul_imglogo.Cerrarconexion();
                                }
                                //obj.Dispose();
                            }
                            else
                            {
                                int icount = 0;

                                conect con_imglogo = new conect();
                                String simglogo = "select count(*) As num from imagen_logo where casoid = " + sCasoidconsulta + " and TipoSolicitudId = " + stiposolicitud + ";";
                                MySqlDataReader resp_imglogo = con_imglogo.getdatareader(simglogo);
                                while (resp_imglogo.Read())
                                {

                                    icount = int.Parse(objfuncionesdicss.validareader("num", "num", resp_imglogo).Text);// objfuncionesdicss.validareader("num", "num", resp_imglogo));//consultar idioma
                                }
                                resp_imglogo.Close();
                                con_imglogo.Cerrarconexion();

                                if (icount > 0)
                                {//si la consulta arrojo algun resultado colocamos esa ruta
                                    String simglogo_consulta = "select * from imagen_logo where casoid = " + sCasoidconsulta + " and TipoSolicitudId = " + stiposolicitud + " order by fecha desc limit 1;";
                                    conect con_consul_imglogo = new conect();
                                    MySqlDataReader resp_consul_imglogo = con_consul_imglogo.getdatareader(simglogo_consulta);
                                    while (resp_consul_imglogo.Read())
                                    {//quiere decir que hicimos el insert correctamente
                                        String sRutaactual = objfuncionesdicss.validareader("ruta", "ruta", resp_consul_imglogo).Text;
                                        obj = Image.FromFile(sRutaactual); ;//
                                        pbDimage.Image = obj;
                                    }
                                    resp_consul_imglogo.Close();
                                    con_consul_imglogo.Cerrarconexion();
                                }
                                else
                                {//si no tiene resultados entonces no hay imagen para ésta Marca
                                    pbDimage.Image = null;
                                }
                            }
                        }
                        catch (Exception exs)
                        {
                            new filelog("Al cargar logo", " :" + exs.Message);
                        }

                    }
                    respuestastring3.Close();
                    con_marcas.Cerrarconexion();
                    textBox10.Text = rowcolor + "";
                }
                else
                {
                    MessageBox.Show("Debe buscar por lo menos en un campo de busqueda");
                }
                this.Enabled = true;
                Cursor.Current = Cursors.Default;

            }
            catch (Exception E)
            {
                this.Enabled = true;
                Cursor.Current = Cursors.Default;
                textBox10.Text = rowcolor + "";
                new filelog(fLoguin.sId, E.ToString());
                MessageBox.Show("Ocurrió un error, por favor verificar el archivo log");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            captura.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            captura.Close();
            fLoguin.Close();
        }

        private void cbPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int iValuepais = Convert.ToInt32((cbPais.SelectedItem as ComboboxItem).Value.ToString());
                tbClave.Text = paises[iValuepais];
            }
            catch (Exception ex)
            {
                new filelog("", "E:" + ex.Message);
            }

        }

        private void listViewCasos_DoubleClick(object sender, EventArgs e)
        {
            //String sClavepaiscaso = listViewCasos.SelectedItems[0].SubItems[0].Text;
            //String sClasemarcas = listViewCasos.SelectedItems[0].SubItems[1].Text;
            //String sCasoIdcaso = listViewCasos.SelectedItems[0].SubItems[2].Text;
            //String sCasonumerocaso = listViewCasos.SelectedItems[0].SubItems[3].Text;
            //String sTipocaso = listViewCasos.SelectedItems[0].SubItems[4].Text;
            //String sExpedientecaso = listViewCasos.SelectedItems[0].SubItems[5].Text;
            //String sRegistrocaso = listViewCasos.SelectedItems[0].SubItems[6].Text;
            //String sInteresadocaso = listViewCasos.SelectedItems[0].SubItems[7].Text;
            //String sTitulodenomcaso = listViewCasos.SelectedItems[0].SubItems[8].Text;
            //String sClientecaso = listViewCasos.SelectedItems[0].SubItems[9].Text;
            //String sPrioridadcaso = listViewCasos.SelectedItems[0].SubItems[10].Text;
            //String sReferenciacaso = listViewCasos.SelectedItems[0].SubItems[11].Text;

            //listViewCasos.Items.Clear();
            //tbexpediente.Clear();
            //tbInteresado.Clear();
            //tbPrioridad.Clear();
            //tbDenominacion.Clear();
            //tbregistro.Clear();
            //tbCliente.Clear();
            //tbreferencia.Clear();
            //tbCasoid.Clear();
            //fTmarcas objmarca = new fTmarcas(fLoguin, captura, this, sCasoIdcaso);
            //this.Hide();
            //objmarca.Show();

            //switch (sTipocaso)
            //{
            //    case "Patente":
            //        {
            //            consultacaso cConsultaid = new consultacaso(fLoguin, captura, this, sCasoIdcaso);
            //            this.Hide();
            //            cConsultaid.Show();
            //        } break;
            //    case "Modelo de utilidad":
            //        {
            //            //MessageBox.Show("En desarrollo");
            //        } break;
            //    case "Dis. industrial (Modelo)":
            //        {
            //            //MessageBox.Show("En desarrollo");
            //        } break;
            //    case "Dis. industrial (Dibujo)":
            //        {
            //            //MessageBox.Show("En desarrollo");
            //        } break;
            //    case "Patente PCT(ante wipo)":
            //        {

            //        } break;
            //    case "Variedad vegetal":
            //        {
            //            //MessageBox.Show("En desarrollo");
            //        } break;
            //    case "Marca":
            //        {

            //        } break;
            //    case "Nombre Comercial":
            //        {
            //            // MessageBox.Show("En desarrollo");
            //        } break;
            //    case "Aviso Comercial":
            //        {
            //            //MessageBox.Show("En desarrollo");
            //        } break;
            //    case "Procedimiento contencioso":
            //        {
            //            fTcontencioso obj = new fTcontencioso(fLoguin, captura, this, sCasoIdcaso);
            //            obj.Show();
            //            conect con = new conect();
            //        } break;
            //    case "Juicio de Nulidad":
            //        {
            //            //MessageBox.Show("En desarrollo");
            //        } break;
            //    case "Juicio de Amparo":
            //        {
            //            //MessageBox.Show("En desarrollo");
            //        } break;
            //    case "Consulta":
            //        {
            //            //MessageBox.Show("En desarrollo");
            //        } break;
            //    case "Oposición":
            //        {
            //            fToposiciones obbj = new fToposiciones(fLoguin, captura, this, sCasoIdcaso);
            //            obbj.Show();

            //        } break;
            //    case "Registro de Obra":
            //        {
            //            fTderechosdeautor objDerechosaut = new fTderechosdeautor(fLoguin, captura, this, sCasoIdcaso);
            //            //consultacaso cConsultaid = new consultacaso(loguin, captura, this, sCasoIdcaso);
            //            //this.Hide();
            //            objDerechosaut.Show();
            //        } break;
            //    case "Reserva de Derechos":
            //        {

            //            fTreservaderechos objReserva = new fTreservaderechos(fLoguin, captura, this, sCasoIdcaso);
            //            //consultacaso cConsultaid = new consultacaso(loguin, captura, this, sCasoIdcaso);
            //            //this.Hide();
            //            objReserva.Show();

            //        } break;
            //    case "Recurso de revisión":
            //        {
            //            MessageBox.Show("En desarrollo");
            //        } break;
            //    case "trazado de circuito":
            //        {
            //            MessageBox.Show("En desarrollo");
            //        } break;
            //    case "nombre de dominio":
            //        {
            //            MessageBox.Show("En desarrollo");
            //        } break;
            //}
        }

        private void bMarcas_Load(object sender, EventArgs e)
        {

        }

        private void textBoxCasonumero_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //MessageBox.Show("Mensaje enviado");
                    button1_Click(sender, e);
                    if (textBox10.Text == "1")
                    {
                        // MessageBox.Show(listViewCasos.Items + "");
                        System.Threading.Thread.Sleep(100);
                        String sCasoIdcaso = dgvBuscamarca.Rows[0].Cells[2].Value.ToString();//.Items[0].SubItems[2].Text;
                        //listViewCasos.Items.Clear();
                        dgvBuscamarca.Rows.Clear();
                        tbexpediente.Clear();
                        tbInteresado.Clear();
                        tbPrioridad.Clear();
                        tbDenominacion.Clear();
                        tbregistro.Clear();
                        tbCliente.Clear();
                        tbreferencia.Clear();
                        tbCasoid.Clear();
                        fTmarcas objmarca = new fTmarcas(fLoguin, captura, this, sCasoIdcaso);
                        this.Hide();
                        objmarca.Show();
                    }

                }
            }
            catch (Exception E)
            {
                new filelog(fLoguin.sId, E.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;

            if (validaversion(fLoguin.sVersion))
            {
                return;
            }
            conect con = new conect();
            //listViewCasos.Items.Clear();
            int rowcolor = 0;
            try
            {
                String sQuerywhere = "";
                String sQuerywherecaso = "";
                String sCampoconsulta = "";
                String stablaconsulta = "";
                String sWhereconsulta = "";
                String sQuerybusqueda = "";
                
                if (cbFiltroestatus.SelectedItem != null)
                {
                    sQuerywhere += " AND consulta_casosmarcas.EstatusCasoId = '" + (cbFiltroestatus.SelectedItem as ComboboxItem).Value + "'";
                }
                //20220427FVS Agregra Filtro Estatus en Ultimos Casos

                /*Validamos si está el check de la imagen de la marca*/
                if (cbLogo.Checked)
                {
                    sQuerywhere += " AND consulta_casosmarcas.Nombre_logo != '' ";
                    dgvBuscamarca.Columns[17].Visible = true;
                }
                else {
                    dgvBuscamarca.Rows[0].Height = 22;
                    dgvBuscamarca.Columns[17].Visible = false;
                }

                if (sQuerywhere!="") {
                    sQuerywhere = sQuerywhere.Substring(4, sQuerywhere.Length - 4);
                }
                
                sQuerybusqueda = " SELECT  * " +
                                 " FROM " +
                                 " consulta_casosmarcas ";
                if (sQuerywhere != "")
                {
                    sQuerybusqueda = sQuerybusqueda + " WHERE " + sQuerywhere;
                }
                sQuerybusqueda = sQuerybusqueda + " order by CasoId desc limit " + tbLimitcasos.Text + ";";
                sQuerywhere = "";



                respuestastring3_2 = con.getdatareader(sQuerybusqueda);
                dgvBuscamarca.Rows.Clear();
                int iCountimg = 0;
                while (respuestastring3_2.Read())
                {
                    String sCasoidconsulta = validareader("CasoId", "CasoId", respuestastring3_2).Text;
                    String sPrioridades = validareader("PrioridadNumero", "CasoId", respuestastring3_2).Text;
                    String sQueryintere = validareader("InteresadoNombre", "CasoId", respuestastring3_2).Text;
                    int residuo = rowcolor % 2;
                    String sMarcasdescription = validareader("CasoProductosClase", "CasoId", respuestastring3_2).Text;//CasoProductosClase
                    String sClientesdatos = validareader("NombreUtilClient", "CasoId", respuestastring3_2).Text;
                    String sPaisclave = validareader("PaisClave", "CasoId", respuestastring3_2).Text;
                    String sTiposolicitudDescrip = validareader("TipoSolicitudDescrip", "CasoId", respuestastring3_2).Text;
                    String sReferenciacaso = validareader("referencia", "CasoId", respuestastring3_2).Text;
                    String sTituloiIngles = validareader("CasoTituloingles", "CasoId", respuestastring3_2).Text;
                    String TipoMarcaDescrip = validareader("TipoMarcaDescrip", "CasoId", respuestastring3_2).Text;

                    String sCasoFechaVigencia = validareader("CasoFechaVigencia", "CasoId", respuestastring3_2).Text;
                    String sCasoFechaDeclaUso = validareader("CasoFechaDeclaUso", "CasoId", respuestastring3_2).Text;
                    String sCasoFechaRenova = validareader("CasoFechaRenova", "CasoId", respuestastring3_2).Text;
                    String sLeydescript = validareader("Leydescript", "CasoId", respuestastring3_2).Text;
                    //Leydescript

                    DateTime dFechavigencia = DateTime.MinValue;
                    DateTime dCasoFechaRenova = DateTime.MinValue;
                    DateTime dCasoFechaDeclaUso = DateTime.MinValue; //Convert.ToDateTime(sCasoFechaDeclaUso);
                    

                    if (sCasoFechaDeclaUso != "")
                    {
                        dCasoFechaDeclaUso = Convert.ToDateTime(sCasoFechaDeclaUso);
                    }

                    if (sCasoFechaRenova != "")
                    {
                        dCasoFechaRenova = Convert.ToDateTime(sCasoFechaRenova);
                    }

                    if (sCasoFechaVigencia != "")
                    {
                        dFechavigencia = Convert.ToDateTime(sCasoFechaVigencia);
                    }
                    // dgvBuscamarca
                    DateTime dFechapresentacion = DateTime.MinValue;
                    try
                    {
                        if (validareader("CasoFechaRecepcion", "CasoId", respuestastring3_2).Text.Trim() != "00-00-0000" && validareader("CasoFechaRecepcion", "CasoId", respuestastring3_2).Text.Trim() != "")
                        {
                            String fecha = validareader("CasoFechaRecepcion", "CasoId", respuestastring3_2).Text;
                            dFechapresentacion = Convert.ToDateTime(fecha);
                        }
                    }
                    catch (Exception exdate)
                    {
                        new filelog("CasoFechaRecepcion", exdate.Message);
                    }


                    DateTime dFechaconcesion = DateTime.MinValue;
                    try
                    {
                        if (validareader("CasoFechaConcesion", "CasoId", respuestastring3_2).Text.Trim() != "00-00-0000" && validareader("CasoFechaConcesion", "CasoId", respuestastring3_2).Text.Trim() != "")
                        {
                            dFechaconcesion = Convert.ToDateTime(validareader("CasoFechaConcesion", "CasoId", respuestastring3_2).Text);
                        }
                    }
                    catch (Exception exdates)
                    {
                        new filelog("CasoFechaConcesion", exdates.Message);
                    }

                    /*Validamos el checkbox de logos de marcas*///Nombre_logo
                    string simageFilename = validareader("Nombre_logo", "CasoId", respuestastring3_2).Text;////@"C:\\facturador_lalo\\029875.gif";
                    Image animatedImage = null;
                    String simagedescripction = "";
                    if (simageFilename!="") {
                        try {
                            //new MemoryStream(File.ReadAllBytes(path))
                            MemoryStream mArrayimg = new MemoryStream(File.ReadAllBytes(simageFilename));
                            //FileStream file = new FileStream("temp_" + iCountimg + ".gif", FileMode.Create, FileAccess.Write);
                            //mArrayimg.WriteTo(file);
                            //file.Dispose();
                            //file.
                            //file.Close();
                            //animatedImage = Image.FromFile("temp_"+ iCountimg + ".gif");
                            animatedImage = Image.FromStream(mArrayimg);
                            mArrayimg.Dispose();
                            mArrayimg.Close();

                            //animatedImage = Image.FromFile(simageFilename);
                            simagedescripction = simageFilename;
                        } catch (Exception EXS) {
                            new filelog("", ""+EXS.Message);
                        }
                        
                    }

                    /*Fin Validamos el checkbox de logos de marcas*/
                    if (!cbLogo.Checked)
                    {
                        dgvBuscamarca.Rows[0].Height = 22;
                    }
                    DataGridViewRow dRows = (DataGridViewRow)dgvBuscamarca.Rows[0].Clone();
                    DataGridViewImageCell cellimage = new DataGridViewImageCell();// (DataGridViewImageCell)dRows.Cells[16];
                    cellimage.Value = animatedImage;
                    cellimage.Description = simagedescripction;
                    cellimage.ImageLayout = DataGridViewImageCellLayout.Stretch;
                    // DataGridViewImageCellLayout.Stretch;


                    dRows.Cells[0].Value = sPaisclave;
                    dRows.Cells[1].Value = sMarcasdescription;
                    dRows.Cells[2].Value = sCasoidconsulta;
                    dRows.Cells[3].Value = validareader("CasoNumero", "CasoId", respuestastring3_2).Text;
                    dRows.Cells[4].Value = sTiposolicitudDescrip;
                    dRows.Cells[5].Value = TipoMarcaDescrip;

                    dRows.Cells[6].Value = sLeydescript;

                    //sLeydescript
                    dRows.Cells[7].Value = validareader("Estatuscasodescript_", "CasoId", respuestastring3_2).Text;
                    dRows.Cells[8].Value = dFechapresentacion;
                    dRows.Cells[9].Value = validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3_2).Text;
                    dRows.Cells[10].Value = dFechaconcesion;
                    
                    dRows.Cells[11].Value = dCasoFechaDeclaUso;//Decla estricta
                    dRows.Cells[12].Value = dFechavigencia;//Vigencia
                    dRows.Cells[13].Value = dCasoFechaRenova;//Renova

                    dRows.Cells[14].Value = validareader("CasoNumConcedida", "CasoId", respuestastring3_2).Text;
                    dRows.Cells[15].Value = sQueryintere;
                    dRows.Cells[16].Value = sTituloiIngles;
                    
                    dRows.Cells[17] = cellimage;

                    dRows.Cells[18].Value = sClientesdatos;
                    dRows.Cells[19].Value = sPrioridades;
                    dRows.Cells[20].Value = sReferenciacaso;
                    dRows.Cells[21].Value = validareader("HolderName", "CasoId", respuestastring3_2).Text;

                    cellimage = new DataGridViewImageCell();//para vaciarlo
                    animatedImage = null;

                    validafondo(residuo, dRows, "CasoFechaDeclaUso", 11, respuestastring3_2);
                    validafondo(residuo, dRows, "CasoFechaVigencia", 12, respuestastring3_2);
                    validafondo(residuo, dRows, "CasoFechaRenova", 13, respuestastring3_2);

                    if (residuo == 0)
                    {
                        dRows.DefaultCellStyle.BackColor = Color.LightGray;

                        if(validareader("CasoFechaRecepcion", "CasoId", respuestastring3_2).Text.Trim() != "00-00-0000" 
                            && validareader("CasoFechaRecepcion", "CasoId", respuestastring3_2).Text.Trim() != "")
                        {
                            dRows.Cells[8].Style.ForeColor = Color.Black;
                        }else{
                            dRows.Cells[8].Style.ForeColor = Color.LightGray;
                        }

                        if(validareader("CasoFechaConcesion", "CasoId", respuestastring3_2).Text.Trim() != "00-00-0000" 
                            && validareader("CasoFechaConcesion", "CasoId", respuestastring3_2).Text.Trim() != "")
                        {
                            dRows.Cells[10].Style.ForeColor = Color.Black;
                        }else{
                            dRows.Cells[10].Style.ForeColor = Color.LightGray;
                        }

                    }
                    else{
                        dRows.DefaultCellStyle.BackColor = Color.Azure;
                        if(validareader("CasoFechaRecepcion", "CasoId", respuestastring3_2).Text.Trim() != "00-00-0000" 
                            && validareader("CasoFechaRecepcion", "CasoId", respuestastring3_2).Text.Trim() != "")
                        {
                            dRows.Cells[8].Style.ForeColor = Color.Black;
                        }else{
                            dRows.Cells[8].Style.ForeColor = Color.Azure;
                        }
                        
                        if(validareader("CasoFechaConcesion", "CasoId", respuestastring3_2).Text.Trim() != "00-00-0000" 
                            && validareader("CasoFechaConcesion", "CasoId", respuestastring3_2).Text.Trim() != "")
                        {
                            dRows.Cells[10].Style.ForeColor = Color.Black;
                        }else{
                            dRows.Cells[10].Style.ForeColor = Color.Azure;
                        }
                    }

                    if (validareader("CasoFechaRecepcion", "CasoId", respuestastring3_2).Text.Trim() != "00-00-0000" 
                        && validareader("CasoFechaRecepcion", "CasoId", respuestastring3_2).Text.Trim() != "")
                    {
                        dRows.Cells[8].Style.ForeColor = Color.Black;
                    }
                    else
                    {
                        dRows.Cells[8].Style.ForeColor = Color.LightGray;
                    }
                    dgvBuscamarca.Rows.Add(dRows);
                    //listViewCasos.Items.Add(listaitems);
                    //this.listViewCasos.FullRowSelect = true;
                    rowcolor++;
                }
                respuestastring3_2.Close();
                con.Cerrarconexion();
                textBox10.Text = rowcolor + "";

                this.Enabled = true;
                Cursor.Current = Cursors.Default;
                //objfuncionesdicss.lipiafechas_busquedas(dgvBuscamarca);

            }
            catch (Exception E){
                this.Enabled = true;
                Cursor.Current = Cursors.Default;
                if (respuestastring3_2 != null)
                    respuestastring3_2.Close();
                textBox10.Text = rowcolor + "";
                MessageBox.Show("Se encontraron más de " + rowcolor + " la busqueda debe ser más especifica.");
            }
        }


        public void validafondo(int iResiduo, DataGridViewRow dRows, String sNombrecolumna, int iColumna, MySqlDataReader respuest) {
            try {
                if (iResiduo == 0)
                {
                    dRows.DefaultCellStyle.BackColor = Color.LightGray;

                    if (validareader(sNombrecolumna, "CasoId", respuest).Text.Trim() != "00-00-0000"
                        && validareader(sNombrecolumna, "CasoId", respuest).Text.Trim() != "")
                    {
                        dRows.Cells[iColumna].Style.ForeColor = Color.Black;
                    }
                    else
                    {
                        dRows.Cells[iColumna].Style.ForeColor = Color.LightGray;
                    }
                }
                else {
                    dRows.DefaultCellStyle.BackColor = Color.Azure;
                    if (validareader(sNombrecolumna, "CasoId", respuest).Text.Trim() != "00-00-0000"
                        && validareader(sNombrecolumna, "CasoId", respuest).Text.Trim() != "")
                    {
                        dRows.Cells[iColumna].Style.ForeColor = Color.Black;
                    }
                    else
                    {
                        dRows.Cells[iColumna].Style.ForeColor = Color.Azure;
                    }

                }
            }
            catch (Exception exs) {
                new filelog("", ""+exs.Message);
            }
        }

        private void tbClave_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (tbClave.Text.Length > 1)
                {

                    String valorclave = tbClave.Text;
                    int index = Array.IndexOf(paises, valorclave);
                    cbPais.Text = tbClave.Text + " - " + paisesclave[index];
                }
                //int iValuepais = Convert.ToInt32((cbPais.SelectedItem as ComboboxItem).Value.ToString());
                //tbClave.Text = paises[iValuepais];
            }
            catch (Exception Ex)
            {

            }
        }

        private void bMarcas_FormClosing(object sender, FormClosingEventArgs e)
        {
            captura.Show();
        }

        private void cbHolder_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //MessageBox.Show("Mensaje enviado");
                    button1_Click(sender, e);
                    if (textBox10.Text == "1")
                    {
                        // MessageBox.Show(listViewCasos.Items + "");
                        System.Threading.Thread.Sleep(100);
                        String sCasoIdcaso = dgvBuscamarca.Rows[0].Cells[2].Value.ToString(); //listViewCasos.Items[0].SubItems[2].Text;
                        //listViewCasos.Items.Clear();
                        tbexpediente.Clear();
                        tbInteresado.Clear();
                        tbPrioridad.Clear();
                        tbDenominacion.Clear();
                        tbregistro.Clear();
                        tbCliente.Clear();
                        tbreferencia.Clear();
                        tbCasoid.Clear();
                        fTmarcas objmarca = new fTmarcas(fLoguin, captura, this, sCasoIdcaso);
                        this.Hide();
                        objmarca.Show();
                    }

                }
            }
            catch (Exception E)
            {
                new filelog(fLoguin.sId, E.ToString());
            }
        }

        private void bMarcas_Resize(object sender, EventArgs e)
        {
            dgvBuscamarca.Location = new Point(this.dgvBuscamarca.Location.X, 75);
            dgvBuscamarca.Size = new Size(dgvBuscamarca.Width, this.Height - 320);
        }

        private void dgvBuscamarca_DoubleClick(object sender, EventArgs e)
        {
            try 
            {

                if (dgvBuscamarca.SelectedRows != null) {
                    String sCasoIdcaso = dgvBuscamarca.SelectedRows[0].Cells[2].Value.ToString(); //listViewCasos.SelectedItems[0].SubItems[2].Text;
                                                                                                  //String sCasonumerocaso = listViewCasos.SelectedItems[0].SubItems[3].Text;
                                                                                                  //String sTipocaso = listViewCasos.SelectedItems[0].SubItems[4].Text;
                                                                                                  //String sExpedientecaso = listViewCasos.SelectedItems[0].SubItems[5].Text;
                                                                                                  //String sRegistrocaso = listViewCasos.SelectedItems[0].SubItems[6].Text;
                                                                                                  //String sInteresadocaso = listViewCasos.SelectedItems[0].SubItems[7].Text;
                                                                                                  //String sTitulodenomcaso = listViewCasos.SelectedItems[0].SubItems[8].Text;
                                                                                                  //String sClientecaso = listViewCasos.SelectedItems[0].SubItems[9].Text;
                                                                                                  //String sPrioridadcaso = listViewCasos.SelectedItems[0].SubItems[10].Text;
                                                                                                  //String sReferenciacaso = listViewCasos.SelectedItems[0].SubItems[11].Text;
                    dgvBuscamarca.Rows.Clear();
                    //listViewCasos.Items.Clear();
                    tbexpediente.Clear();
                    tbInteresado.Clear();
                    tbPrioridad.Clear();
                    tbDenominacion.Clear();
                    tbregistro.Clear();
                    tbCliente.Clear();
                    tbreferencia.Clear();
                    tbCasoid.Clear();
                    fTmarcas objmarca = new fTmarcas(fLoguin, captura, this, sCasoIdcaso);
                    this.Hide();
                    objmarca.Show();
                }
                
            }
            catch (Exception exs) {
                new filelog("mensaje", exs.Message);
            }
        }

        private void tbLimitcasos_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //MessageBox.Show("Mensaje enviado");
                    //button1_Click(sender, e);
                    button2_Click(sender, e); 
                    if (textBox10.Text == "1")
                    {
                        // MessageBox.Show(listViewCasos.Items + "");
                        System.Threading.Thread.Sleep(100);
                        String sCasoIdcaso = dgvBuscamarca.SelectedRows[0].Cells[2].Value.ToString();// .Items[0].SubItems[2].Text;
                        //listViewCasos.Items.Clear();
                        dgvBuscamarca.Rows.Clear();
                        tbexpediente.Clear();
                        tbInteresado.Clear();
                        tbPrioridad.Clear();
                        tbDenominacion.Clear();
                        tbregistro.Clear();
                        tbCliente.Clear();
                        tbreferencia.Clear();
                        tbCasoid.Clear();
                        fTmarcas objmarca = new fTmarcas(fLoguin, captura, this, sCasoIdcaso);
                        this.Hide();
                        objmarca.Show();
                    }

                }
            }
            catch (Exception E)
            {
                new filelog(fLoguin.sId, E.ToString());
            }
        }

        /*
         SLPicture Pic = new SLPicture(@"C:\Users\Direccion de la imagen\Pictures\Logo Cummunity.jpg");        
         Pic.ResizeInPercentage(20, 20);   //Aquí le das las dimensiones a la imagen si quieres modificarla
         Pic.SetPosition(0.3,  0.2);  // aquí las cooredenadas eso será según tu escenario 
         Xl.InsertPicture(Pic);  //Luego lo introducimos en el Documento SlDocument
         */

        public void generaexcelconimagen(DataGridView tabla)
        {
            try
            {
                SLDocument obj = new SLDocument();
                obj.SetRowHeight(2, tabla.Rows.Count, 80);
                SLStyle estilofechas = obj.CreateStyle();
                estilofechas.FormatCode = "dd/mm/yyyy";
                button25.Enabled = false;
                //agregamos el nombre de las columnas
                int ic = 1;
                foreach (DataGridViewColumn column in tabla.Columns)
                {
                    String svalorheader = column.HeaderText.ToString();
                    obj.SetCellValue(1, ic, svalorheader);
                    ic++;
                }
                //agregamos el contenido de la tabla
                int ir = 2;
                double iAlturaimg = 1.3;
                foreach (DataGridViewRow row in tabla.Rows)
                {
                    ic = 1;
                    foreach (DataGridViewColumn column in tabla.Columns)
                    {
                        String sValor = "";
                        String sFormat = "";
                        String sIsimage = "";
                        String sDescriptfilename = "";
                        if (!(row.Cells[ic - 1].Value is null))
                        {
                            sFormat = row.Cells[ic - 1].InheritedStyle.Format.ToString();
                            sValor = row.Cells[ic - 1].Value.ToString();
                            sIsimage = row.Cells[ic - 1].ValueType.Name;
                            DataGridViewImageCell imageobj = new DataGridViewImageCell();
                            if (sIsimage =="Image") {
                                imageobj = (DataGridViewImageCell)row.Cells[ic - 1];
                                //row.Cells[ic - 1].Value = "";
                                sDescriptfilename = @imageobj.Description;
                                imageobj = new DataGridViewImageCell();//para vaciarlo
                            }
                        }

                        if (sValor == "01/01/0001 12:00:00 a. m." || sValor == "01/01/0001")
                        {
                            sValor = "";
                        }

                        if (sFormat == "d" && sValor != "01/01/0001 12:00:00 a. m." && sValor != "")
                        {
                            //para insertar un date debemos converitrlo primero
                            DateTime dValorfecha = DateTime.Parse(sValor);
                            if (dValorfecha.ToString("dd/MM/yyyy") == "01/01/0001")//si el formato de la fecha es minimo agregamos texto vacio
                            {
                                obj.SetCellValue(ir, ic, "");
                            }
                            else
                            {
                                //20220425FSV Aplicamos el formato definido
                                obj.SetCellStyle(ir, ic, estilofechas);
                                //20220425 Fin de Formato
                                obj.SetCellValue(ir, ic, dValorfecha, "dd/MM/yyyy");
                            }
                        }
                        else
                        {
                            if (sValor == "01/01/0001" || sValor == "System.Drawing.Bitmap")
                            {
                                sValor = "";
                            }
                            obj.SetCellValue(ir, ic, sValor);
                        }

                        if (sIsimage == "Image" && sDescriptfilename !="") {
                            SLPicture Pic = new SLPicture(sDescriptfilename);//C:\facturador_lalo
                            //SLPicture Pic = new SLPicture(sDescriptfilename);//C:\facturador_lalo
                            Pic.ResizeInPixels(70, 70);
                            //Pic.ResizeInPercentage(13, 13);   //Aquí le das las dimensiones a la imagen si quieres modificarla
                            Pic.SetPosition(iAlturaimg, 17.2);  // aquí las cooredenadas eso será según tu escenario 
                            obj.InsertPicture(Pic);  //Luego lo introducimos en el Documento SlDocument
                            //File.Delete(sDescriptfilename);
                            Pic = null;
                            iAlturaimg = iAlturaimg + 1;
                        }

                        //, "MM/dd/yyyy"
                        ic++;
                    }
                    ir++;
                }
                //tabla.Rows.Clear();
                //captura.Show();
                //this.Close();


                //generamos la ruta
                String fechalog = DateTime.Now.ToString("MM_dd_yyyy_HH_mm_ss");
                String ruta = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + "\\casosking\\";
                button25.Enabled = true;
                //limpiamos para que al guardar no diga que se está utilizando
                
                //File.Delete(@"\\\\DESKTOP-SU3GNQT\\LaloIPfactsApp\\documentosserver\\logos_marcas\\040477\\040477_12072022153720.gif");
                //guardamos el archivo
                obj.SaveAs(ruta + "excel_marcas" + fechalog + ".xlsx");
                //abrirmos el archivo
                Process.Start(ruta + "excel_marcas" + fechalog + ".xlsx");

            }
            catch (Exception exs)
            {
                new filelog("error al generar excel ", " :" + exs.Message);
                MessageBox.Show(exs.Message);

            }
        }

        public void generaexcel(DataGridView tabla)
        {
            try
            {

                SLDocument obj = new SLDocument();
                SLStyle estilofechas = obj.CreateStyle();
                estilofechas.FormatCode = "dd/mm/yyyy";

                button25.Enabled = false;

                //agregamos el nombre de las columnas
                int ic = 1;
                foreach (DataGridViewColumn column in tabla.Columns)
                {
                    String svalorheader = column.HeaderText.ToString();
                    obj.SetCellValue(1, ic, svalorheader);
                    ic++;
                }

                //agregamos el contenido de la tabla
                int ir = 2;
                foreach (DataGridViewRow row in tabla.Rows)
                {
                    ic = 1;
                    foreach (DataGridViewColumn column in tabla.Columns)
                    {
                        String sValor = "";
                        String sFormat = "";
                        if (!(row.Cells[ic - 1].Value is null))
                        {
                            sFormat = row.Cells[ic - 1].InheritedStyle.Format.ToString();
                            sValor = row.Cells[ic - 1].Value.ToString();
                        }

                        if (sValor == "01/01/0001 12:00:00 a. m." || sValor == "01/01/0001")
                        {
                            sValor = "";
                        }

                        if (sFormat == "d" && sValor != "01/01/0001 12:00:00 a. m." && sValor != "")
                        {
                            //para insertar un date debemos converitrlo primero
                            DateTime dValorfecha = DateTime.Parse(sValor);
                            if (dValorfecha.ToString("dd/MM/yyyy") == "01/01/0001")//si el formato de la fecha es minimo agregamos texto vacio
                            {
                                obj.SetCellValue(ir, ic, "");
                            }
                            else {
                                //20220425FSV Aplicamos el formato definido
                                obj.SetCellStyle(ir, ic, estilofechas);
                                //20220425 Fin de Formato
                                obj.SetCellValue(ir, ic, dValorfecha, "dd/MM/yyyy");
                            }
                            
                        }
                        else
                        {
                            if (sValor== "01/01/0001") {
                                sValor = "";
                            }
                            obj.SetCellValue(ir, ic, sValor);
                        }

                        //, "MM/dd/yyyy"
                        ic++;
                    }
                    ir++;
                }
                //generamos la ruta
                String fechalog = DateTime.Now.ToString("MM_dd_yyyy_HH_mm_ss");
                String ruta = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + "\\casosking\\";

                //guardamos el archivo
                obj.SaveAs(ruta + "excel_marcas" + fechalog + ".xlsx");
                //abrirmos el archivo
                Process.Start(ruta + "excel_marcas" + fechalog + ".xlsx");

            }
            catch (Exception exs)
            {
                new filelog("error al generar excel ", " :" + exs.Message);
                MessageBox.Show(exs.Message);

            }
        }
        private void button25_Click(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                if (cbLogo.Checked)
                {
                    generaexcelconimagen(dgvBuscamarca);
                }
                else {
                    generaexcel(dgvBuscamarca);
                }
                this.Enabled = true;
                Cursor.Current = Cursors.Default;
                //

            }
            catch (Exception E)
            {
                this.Enabled = true;
                Cursor.Current = Cursors.Default;
                Console.Write("Se canceló la exportación");
                new filelog(fLoguin.sId, E.ToString());
                MessageBox.Show(E.Message);

            }
        }
        public bool validaversion(String sVersion)
        {
            bool breinicia = false;
            return breinicia;
            /*try
            {
                conect con_filev = new conect();
                String sQuery = "SELECT * FROM act_version order by idact_version desc limit 1;";
                MySqlDataReader resp_consltv = con_filev.getdatareader(sQuery);
                String sIdversionact = "";
                String sFechaversion = "";
                while (resp_consltv.Read())
                {
                    sIdversionact = validareader("v_actual", "v_actual", resp_consltv).Text;
                    sFechaversion = validareader("fecha", "fecha", resp_consltv).Text;
                    if (sIdversionact != sVersion)
                    {
                        MessageBox.Show("Deben actualizar la versión de casos king");
                        breinicia = true;
                    }
                }
                con_filev.Cerrarconexion();
                resp_consltv.Close();

                //if (breinicia) {
                //    buscarclienteform.Show();
                //    this.Close();
                //}
                return breinicia;
            }
            catch (Exception exs)
            {
                return breinicia;
            }
            */
        }

        private void dgvBuscamarca_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message); 
        }
    }
}
