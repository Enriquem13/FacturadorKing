using MySql.Data.MySqlClient;
using MySql.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
using System.IO;
using MailBee.Pop3Mail;
using MailBee.Mime;
using Facturador.PlantillaFactura;
using Facturador.Modulo_facturacion;
using System.Xml;
using System.Xml.Linq;
using System.Web;
using Facturador.plazos;
using Facturador.plazos_forms;
using Facturador.Casos.Documentos;
using Facturador.serviciocorreoking;
using emailking;

namespace Facturador
{
    public partial class captura : Form
    {
        //inica buscar por caso
        //public servcorreo obj;
        public Form1 form1contruct;
        public System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.GroupBox groupBox4;
        public System.Windows.Forms.GroupBox groupBox5;
        public System.Windows.Forms.GroupBox groupBox6;
        public System.Windows.Forms.RadioButton radioButton3;
        public System.Windows.Forms.RadioButton radioButton2;
        public System.Windows.Forms.RadioButton radioButton1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.RadioButton radioButton5;
        public System.Windows.Forms.RadioButton radioButton4;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label3;
        //public System.Windows.Forms.ListBox listBox1;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox textBox5;
        public System.Windows.Forms.TextBox textBox4;
        public System.Windows.Forms.TextBox textBox3;
        public System.Windows.Forms.TextBox textBox2;
        public System.Windows.Forms.Label label10;
        public System.Windows.Forms.Label label9;
        public System.Windows.Forms.Label label8;
        public System.Windows.Forms.Label label7;
        public System.Windows.Forms.GroupBox groupBox8;
        public System.Windows.Forms.GroupBox groupBox7;
        public System.Windows.Forms.Label label12;
        public System.Windows.Forms.TextBox textBox6;
        public System.Windows.Forms.Label label11;
        public System.Windows.Forms.Label label16;
        public System.Windows.Forms.Label label15;
        public System.Windows.Forms.TextBox textBox9;
        public System.Windows.Forms.Label label14;
        public System.Windows.Forms.TextBox textBox8;
        public System.Windows.Forms.Label label13;
        public System.Windows.Forms.TextBox textBox7;
        public System.Windows.Forms.TextBox textBox10;
        public System.Windows.Forms.Label label17;
        public System.Windows.Forms.Label label20;
        public System.Windows.Forms.CheckBox checkBox2;
        public System.Windows.Forms.Label label19;
        public System.Windows.Forms.Label label18;
        public System.Windows.Forms.CheckBox checkBox1;
        public System.Windows.Forms.Label label22;
        public System.Windows.Forms.Label label21;
        //public System.Windows.Forms.VScrollBar vScrollBar1;
        public System.Windows.Forms.Label label23;
        public System.Windows.Forms.ListBox listBox2;
        public System.Windows.Forms.ListView listView1;
        public System.Windows.Forms.GroupBox groupBox9;
        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.Label label24;
        public System.Windows.Forms.TextBox textBox11;
        public ListView lConceptos = new ListView();
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox cClientebox;
        private System.Windows.Forms.ComboBox combSatservicios;
        private System.Windows.Forms.Label labelsat;
        public String[] DifferArray;
        public MailMessage Mesageemail;
        private funcionesdicss funcionesgenerales = new funcionesdicss();
        
        public captura(Form1 form1)
        {
            form1contruct = form1;
            InitializeComponent();
            this.Text = "    "+this.Text + "                                                                                                                                                                                                             "+
                "                                                                                       Usuario: " + form1.sUsername;
            funcionesgenerales.activaaviso(tbAvisoprueba);
            //menuStrip1
            if (validaversion(form1.sVersion))
            {
                menuStrip1.Enabled = false;
            }
        }

        private void nuevaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Genera Nueva factura");
            //resetform();
            //clearform();
           
        }

        private void cClientebox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cClientebox.SelectedIndex == 0)
            {
                MessageBox.Show("llegamos al cambio si es igual a cero");
            }
            else
            {
                MessageBox.Show("llegamos al cambio si es diferente de cero");
            }
        }

       
        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            form1contruct.Show();
            this.Hide();
            
        }

        private void salirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            form1contruct.Close();
            this.Close();
            Application.Exit();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //SavePDF(@"C:\Users\Public\Pictures\53333103_2188414748086971_3611708080187768832_n.jpg", @"C:\facturador\pdf.pdf");
            
            conect con = new conect();
            Random ran = new Random(20);
            String numbusqueda = textBox11.Text;
            if (!numbusqueda.Equals(""))
            {
                //createPDF("caso_" + numbusqueda, @"C:\facturador\pdf.pdf", ran);
            //SELECT * FROM `caso`,`casocliente`,`casointeresado` where caso.CasoId = 27634 and casocliente.CasoId = 27634 and casointeresado.CasoId = 27634
            //String consulta = "SELECT * FROM `caso`,`casocliente`,`casointeresado` where caso.CasoId = " + numbusqueda + " and casocliente.CasoId = " + numbusqueda + " and casointeresado.CasoId = " + numbusqueda;
            String query = "SELECT caso.CasoTitulo, caso.CasoDenominacion, cliente.ClienteId, cliente.ClienteNombre, interesado.InteresadoNombre" +
                            " FROM caso, casocliente, casointeresado, cliente, interesado"+
                            " where "+
                            "caso.CasoId = " + numbusqueda + " and " +
                            "casocliente.CasoId = " + numbusqueda + " and " +
                            "casointeresado.CasoId = " + numbusqueda + " and " +
                            "casocliente.ClienteId = cliente.ClienteId and "+
                            "casointeresado.InteresadoId = interesado.InteresadoID";
            
            MySqlDataReader respuestastring = con.getdatareader(query);
            String titulo = "";
            String cliente = "";
            String interesado = "";
            /*listView1.GridLines = true;
            listView1.FullRowSelect = true;
            //Add column header
            listView1.Columns.Add("Campouno", 100);
            listView1.Columns.Add("campodos", 70);
            listView1.Columns.Add("campostres", 70);*/
            while (respuestastring.Read())
            {
                if (!respuestastring.IsDBNull(respuestastring.GetOrdinal("CasoTitulo"))) {
                    titulo = respuestastring.GetString(respuestastring.GetOrdinal("CasoTitulo"));
                } else {
                    titulo = "vacio";
                }
                    
                if (!respuestastring.IsDBNull(respuestastring.GetOrdinal("ClienteNombre")))
                {
                    cliente = respuestastring.GetString(respuestastring.GetOrdinal("ClienteNombre"));
                }else{
                    cliente = "vacio";
                }
                    

                if (!respuestastring.IsDBNull(respuestastring.GetOrdinal("InteresadoNombre")))
                {
                    interesado = respuestastring.GetString(respuestastring.GetOrdinal("InteresadoNombre")); 
                }else{
                    interesado = "vacio";
                }

                ListViewItem lista = new ListViewItem("id");
                lista.SubItems.Add(titulo);
                lista.SubItems.Add(cliente);
                lista.SubItems.Add(interesado);
                //ListView lConceptos = new ListView();
                
                this.listView1.Items.Add(lista);
                
                /*lConceptos.Location = new System.Drawing.Point(28, 89);
                lConceptos.Name = "lConceptos";
                lConceptos.Size = new System.Drawing.Size(911, 97);
                lConceptos.GridLines = true;
                lConceptos.TabIndex = 9;
                lConceptos.Items.Add(lista);
                lConceptos.UseCompatibleStateImageBehavior = false;
                lConceptos.View = System.Windows.Forms.View.Details;
                groupBox6.Controls.Add(lConceptos);
                lConceptos.BeginUpdate();*/

                /*this.listView1.GridLines = true;
                this.listView1.Location = new System.Drawing.Point(12, 167);
                this.listView1.Name = "listView1";
                this.listView1.Size = new System.Drawing.Size(953, 206);
                this.listView1.TabIndex = 0;
                this.listView1.UseCompatibleStateImageBehavior = false;
                this.listView1.View = System.Windows.Forms.View.Details;*/
                //bla bla bla

                  

            }
            //MessageBox.Show(titulo);
            //MessageBox.Show(cliente);
            //MessageBox.Show(interesado);
            //MessageBox.Show(interesado);
            }
            else
            {
                MessageBox.Show("Debe ingresar un número de caso");
            }
        }
         
            private void label22_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("antes de borrar la lista");
            
            //MessageBox.Show("después de borrar lista");
        }
            //public static string SavePDF(string strInputFile, string strOutputFile)
            //{
            //    iTextSharp.text.Document doc = null;
            //    try
            //    {
            //        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(strInputFile);
            //        iTextSharp.text.Rectangle rectDocSize = new iTextSharp.text.Rectangle(img.Width, img.Height);
            //        doc = new iTextSharp.text.Document(rectDocSize);

            //        iTextSharp.text.pdf.PdfWriter.GetInstance(doc, new FileStream(strOutputFile, FileMode.Create));
            //        doc.Open();
            //        //doc.Add(new iTextSharp.text.Paragraph("GIF"));
            //        doc.Add(img);
            //    }
            //    catch (iTextSharp.text.DocumentException dex)
            //    {
            //        throw dex;
            //    }
            //    catch (IOException ioex)
            //    {
            //        throw ioex;
            //    }
            //    catch (Exception ex)
            //    {
            //        throw ex;
            //    }
            //    finally
            //    {
            //        if (doc != null)
            //            doc.Close();
            //    }
            //    return strOutputFile;
            //}
            //private void createPDF(String tempfilename, String newPath, Random random)
            //{
            //    //MessageBox.Show("PDF");

            //    string newFileName = tempfilename + ".pdf";
            //    newPath = System.IO.Path.Combine(newPath, newFileName);
            //    int sizeofwrite = random.Next(1, 10000);

            //    if (!System.IO.File.Exists(newPath))
            //    {

            //        // step 1: creation of a document-object
            //        iTextSharp.text.Document myDocument = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate());

            //        try
            //        {
            //            // step 2:
            //            // Now create a writer that listens to this doucment and writes the document to desired Stream.
            //            iTextSharp.text.pdf.PdfWriter.GetInstance(myDocument, new FileStream(newPath, FileMode.Create));

            //            // step 3:  Open the document now using
            //            myDocument.Open();

            //            for (int x = 0; x < sizeofwrite; x++)
            //            {
            //                Byte[] b = new Byte[1];
            //                random.NextBytes(b);

            //                // step 4: Now add some contents to the document
            //                myDocument.Add(new iTextSharp.text.Paragraph(b[0].ToString()));
            //            }
            //        }
            //        catch (iTextSharp.text.DocumentException de)
            //        {
            //            Console.Error.WriteLine(de.Message);
            //        }
            //        catch (IOException ioe)
            //        {
            //            Console.Error.WriteLine(ioe.Message);
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.Error.WriteLine(ex.Message);
            //        }
            //        finally
            //        {
            //            // step 5: Remember to close the documnet
            //            myDocument.Close();
            //            textBox4.AppendText("Created file: " + newPath.ToString() + System.Environment.NewLine + "SIZE: " + sizeofwrite.ToString() + "\r\n\r\n");
            //        }
            //    }
            //}

            private void reporteFacturaToolStripMenuItem_Click(object sender, EventArgs e)
            {

                
            }

            private void reporteQuincenalToolStripMenuItem_Click(object sender, EventArgs e)
            {
                
               
            }

            private void altaCasoToolStripMenuItem_Click(object sender, EventArgs e)
            {
                //Nuevocaso ncaso = new Nuevocaso(form1contruct, this);
                
            }

            private void consultarToolStripMenuItem_Click(object sender, EventArgs e)
            {
                Form obj = this;
                Cliente cOnuevocliente = new Cliente(form1contruct, this, obj);
                cOnuevocliente.Show();
                this.Hide();
            }

            private void consultaCasoToolStripMenuItem_Click(object sender, EventArgs e)
            {
                //Consutlacaso conObj = new Consutlacaso(form1contruct, this);
                //conObj.Show();

                this.Hide();
                bBuscargrupo conulta = new bBuscargrupo(this, form1contruct);
                conulta.Show();
            }

            private void altaToolStripMenuItem_Click(object sender, EventArgs e)
            {
                Consulacliente consulta = new Consulacliente(form1contruct, this);
                consulta.Show();
                this.Hide();
            }

            private void agregarContactoToolStripMenuItem_Click(object sender, EventArgs e)
            {
                Form obj = this;
                Cliente cliente = new Cliente(form1contruct, this, obj);
                cliente.Show();
                this.Hide();
            }

            private void consultarToolStripMenuItem1_Click(object sender, EventArgs e)
            {
                FConsultaInteresado fconsultainteresa = new FConsultaInteresado(form1contruct, this);
                fconsultainteresa.Show();
                this.Hide();
            }

            private void monedaToolStripMenuItem1_Click(object sender, EventArgs e)
            {
                Fmoneda fmoneda = new Fmoneda(form1contruct, this);
                fmoneda.Show();
                this.Hide();
            }

            private void idiomaToolStripMenuItem1_Click(object sender, EventArgs e)
            {
                Ftipoenvioinf envioinfor = new Ftipoenvioinf(form1contruct, this);
                envioinfor.Show();
                this.Hide();
            }

            private void tipoDeClientesToolStripMenuItem_Click(object sender, EventArgs e)
            {
                Ftipo_cliente ftipoclientes = new Ftipo_cliente(form1contruct, this);
                ftipoclientes.Show();
                this.Hide();
            }

            private void tipoProvToolStripMenuItem_Click(object sender, EventArgs e)
            {
                Fprovedorfact proveedorfac = new Fprovedorfact(form1contruct, this);
                proveedorfac.Show();
                this.Hide();
            }

            private void tipoDeEnvíoDeInformaciónToolStripMenuItem_Click(object sender, EventArgs e)
            {
                Fpais fpais = new Fpais(form1contruct, this);
                fpais.Show();
                this.Hide();
            }

            private void altaUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
            {
                Fusuarios fusuarios = new Fusuarios(form1contruct, this);
                fusuarios.Show();
                this.Hide();
            }

            private void cartasToolStripMenuItem_Click(object sender, EventArgs e)
            {

            }

            private void formatosToolStripMenuItem_Click(object sender, EventArgs e)
            {              
            }
            private String getcampo(String campo) {
                try {
                    bool b = campo.Contains('"');
                    if (b)
                    {
                        //MessageBox.Show("si contiene comillas dobles");
                        return "'" + campo + "'";
                    }
                    else
                    {
                        bool c = campo.Contains("'");
                        if (c)
                        {
                            //MessageBox.Show("si contiene comillas simples");
                            return '"' + campo + '"';
                        }
                        else
                        {
                            //es indiferente porque no marca error en el query
                            return '"' + campo + '"';
                        }
                    }
                }catch(Exception ex){
                    return '"' + campo + '"';
                }
                
                
                
                //return "";
            }


            public void actualizamarcas()
            {
                try
                {
                    //Para marcas
                    //borramos la tabla 
                    conect con_pruebas_tr = new conect();
                    MySqlDataReader ejecutaacualizapatentes = con_pruebas_tr.getdatareader("truncate table caso_marcas");
                    ejecutaacualizapatentes.Read();
                    ejecutaacualizapatentes.Close();
                    con_pruebas_tr.Cerrarconexion();
                    //actualizamos la tabla  --con_ipfacts solo consultamos
                    conect_ipfacts con_ipfacts = new conect_ipfacts();
                    String stringquerypatentes = "";
                    String querymarcas = "    SELECT " +
                                        "   CasoId, " +
                                        "   TipoSolicitudId, " +
                                        "   SubTipoSolicitudId, " +
                                        "   CasoTitulo, " +
                                        "   CasoDenominacion, " +
                                        "   IdiomaId, " +
                                        "   DATE_FORMAT(CasoFechaConcesion , '%Y-%m-%d') as  CasoFechaConcesion, " +
                                        "   DATE_FORMAT(CasoFechaLegal , '%Y-%m-%d') as  CasoFechaLegal, " +
                                        "   DATE_FORMAT(CasoFechaDivulgacionPrevia , '%Y-%m-%d') as  CasoFechaDivulgacionPrevia, " +
                                        "   DATE_FORMAT(CasoFechaRecepcion , '%Y-%m-%d') as  CasoFechaRecepcion, " +
                                        "   DATE_FORMAT(CasoFechaVigencia , '%Y-%m-%d') as  CasoFechaVigencia, " +
                                        "   CasoNumeroConcedida, " +
                                        "   CasoNumeroExpedienteLargo, " +
                                        "   CasoNumero, " +
                                        "   ResponsableId, " +
                                        "   TipoMarcaId, " +
                                        "   CasoLeyendaNoReservable, " +
                                        "   DATE_FORMAT(CasoFechaAlta , '%Y-%m-%d') as  CasoFechaAlta,  " +
                                        "   CasoTipoCaptura, " +
                                        "   CasoTitular,  " +
                                        "   DATE_FORMAT(CasoFechaFilingSistema , '%Y-%m-%d') as  CasoFechaFilingSistema, " +
                                        "   DATE_FORMAT(CasoFechaFilingCliente , '%Y-%m-%d') as  CasoFechaFilingCliente,  " +
                                        "   DATE_FORMAT(CasoFechaCartaCliente , '%Y-%m-%d') as  CasoFechaCartaCliente,  " +
                                        "   EstatusCasoId,  " +
                                        "   UsuarioId,  " +
                                        "   PaisId, " +
                                        "   DATE_FORMAT(CasoFechaPruebaUsoSig , '%Y-%m-%d') as  CasoFechaPruebaUsoSig, " +
                                        "   CasoNumConcedida, " +
                                        "   CasoFechaPruebaUso " +
                                        //"   CasoFechainiciouso " +
                                        " FROM " +
                                        "     caso " +
                                        " WHERE " +
                                        "     tiposolicitudid = 7 or tiposolicitudid = 8 or tiposolicitudid = 9 or tiposolicitudid = 77; ";
                    MySqlDataReader respuestastring3 = con_ipfacts.getdatareader(querymarcas);
                    String stringquerymarcas = "";
                    while (respuestastring3.Read())
                    {
                        String sQuerymarcasinsert = " INSERT INTO `caso_marcas` (" +
                                                    " `CasoId`, " +
                                                    " `TipoSolicitudId`, " +
                                                    " `SubTipoSolicitudId`, " +
                                                    " `CasoTituloespanol`, " +
                                                    " `CasoTituloingles`, " +
                                                    " `IdiomaId`, " +
                                                    " `CasoFechaConcesion`," +
                                                    " `CasoFechaLegal`,  " +
                                                    " `CasoFechaDivulgacionPrevia`, " +
                                                    " `CasoFechaRecepcion`, " +
                                                    " `CasoFechaVigencia`, " +
                                                    " `CasoNumeroConcedida`, " +
                                                    " `CasoNumeroExpedienteLargo`, " +
                                                    " `CasoNumero`, " +
                                                    " `ResponsableId`, " +
                                                    " `TipoMarcaId`,  " +
                                                    " `CasoLeyendaNoReservable`, " +
                                                    " `CasoFechaAlta`, " +
                                                    " `CasoTipoCaptura`, " +
                                                    " `CasoTitular`, " +
                                                    " `CasoFechaFilingSistema`, " +
                                                    " `CasoFechaFilingCliente`, " +
                                                    " `CasoFechaCartaCliente`, " +
                                                    " `EstatusCasoId`, " +
                                                    " `UsuarioId`,  " +
                                                    " `PaisId`,  " +
                                                    " `CasoFechaPruebaUsoSig`, " +
                                                    " `CasoNumConcedida` ," +
                                                    " `CasoFechaprobouso`, " +
                                                    " `CasoFechainiciouso`" +
                                                    " )  " +
                                                    " VALUES  " +
                                                    " ( " +
                                                    getcampo(validareader("CasoId", "CasoId", respuestastring3).Text) + ',' +
                                                    getcampo(validareader("TipoSolicitudId", "CasoId", respuestastring3).Text) + ',' +
                                                    getcampo(validareader("SubTipoSolicitudId", "CasoId", respuestastring3).Text) + ',' +
                                                    getcampo(validareader("CasoTitulo", "CasoId", respuestastring3).Text) + ',' +
                                                    getcampo(validareader("CasoDenominacion", "CasoId", respuestastring3).Text) + ',' +
                                                    getcampo(validareader("IdiomaId", "CasoId", respuestastring3).Text) + ',' +
                                                    getcampo(validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text) + ',' +
                                                    getcampo(validareader("CasoFechaLegal", "CasoId", respuestastring3).Text) + ',' +
                                                    getcampo(validareader("CasoFechaDivulgacionPrevia", "CasoId", respuestastring3).Text) + ',' +
                                                    getcampo(validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text) + ',' +
                                                    getcampo(validareader("CasoFechaVigencia", "CasoId", respuestastring3).Text) + ',' +
                                                    getcampo(validareader("CasoNumeroConcedida", "CasoId", respuestastring3).Text) + ',' +
                                                    getcampo(validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text) + ',' +
                                                    getcampo(validareader("CasoNumero", "CasoId", respuestastring3).Text) + ',' +
                                                    getcampo(validareader("ResponsableId", "CasoId", respuestastring3).Text) + ',' +
                                                    getcampo(validareader("TipoMarcaId", "CasoId", respuestastring3).Text) + ',' +
                                                    getcampo(validareader("CasoLeyendaNoReservable", "CasoId", respuestastring3).Text) + ',' +
                                                    getcampo(validareader("CasoFechaAlta", "CasoId", respuestastring3).Text) + ',' +
                                                    getcampo(validareader("CasoTipoCaptura", "CasoId", respuestastring3).Text) + ',' +
                                                    getcampo(validareader("CasoTitular", "CasoId", respuestastring3).Text) + ',' +
                                                    getcampo(validareader("CasoFechaFilingSistema", "CasoId", respuestastring3).Text) + ',' +
                                                    getcampo(validareader("CasoFechaFilingCliente", "CasoId", respuestastring3).Text) + ',' +
                                                    getcampo(validareader("CasoFechaCartaCliente", "CasoId", respuestastring3).Text) + ',' +
                                                    getcampo(validareader("EstatusCasoId", "CasoId", respuestastring3).Text) + ',' +
                                                    getcampo(validareader("UsuarioId", "CasoId", respuestastring3).Text) + ',' +
                                                    getcampo(validareader("PaisId", "CasoId", respuestastring3).Text) + ',' +
                                                    getcampo(validareader("CasoFechaPruebaUsoSig", "CasoId", respuestastring3).Text) + ',' +
                                                    getcampo(validareader("CasoNumConcedida", "CasoId", respuestastring3).Text) + ',' +
                                                    getcampo(validareader("CasoFechaPruebaUso", "CasoId", respuestastring3).Text) + ',' +
                                                    "''" +
                                                    ");";

                        conect con_pruebas = new conect();
                        sQuerymarcasinsert = sQuerymarcasinsert.Replace("\"\"", "null");
                        MySqlDataReader ejecutaacualiza_marcas_insert = con_pruebas.getdatareader(sQuerymarcasinsert);
                        ejecutaacualiza_marcas_insert.Read();
                        ejecutaacualiza_marcas_insert.Close();
                        con_pruebas.Cerrarconexion();
                        //stringquerypatentes += sInsert + "\n";
                    }
                    respuestastring3.Close();
                    con_ipfacts.Cerrarconexion();
                }
                catch (Exception E)
                {
                    new filelog(form1contruct.sId, E.ToString());
                }
            }

            public void actualizapatentes() {
                try {
                    //Para patentes

                    //borramos la tabla 
                    conect con_pruebas_tr = new conect();
                    MySqlDataReader ejecutaacualizapatentes = con_pruebas_tr.getdatareader("truncate table caso_patente");
                    ejecutaacualizapatentes.Read();
                    ejecutaacualizapatentes.Close();
                    con_pruebas_tr.Cerrarconexion();
                    //actualizamos la tabla
                    conect_ipfacts con_ipfacts = new conect_ipfacts();
                    String stringquerypatentes = "";
                    String sPatentesconsulta = " SELECT " +
                                            "     CasoId," +
                                            "     TipoSolicitudId," +
                                            "     SubTipoSolicitudId," +
                                            "     TipoPctId," +
                                            "     CasoDenominacion," +
                                            "     CasoTitulo," +
                                            "     IdiomaId," +
                                            "     DATE_FORMAT(CasoFechaConcesion , '%Y-%m-%d') as  CasoFechaConcesion," +
                                            "     DATE_FORMAT(CasoFechaRecepcion , '%Y-%m-%d') as  CasoFechaRecepcion," +
                                            "     DATE_FORMAT(CasoFechaVigencia , '%Y-%m-%d') as  CasoFechaVigencia," +
                                            "     DATE_FORMAT(CasoFechaPublicacionSolicitud , '%Y-%m-%d') as  CasoFechaPublicacionSolicitud," +
                                            "     DATE_FORMAT(CasoFechaLegal , '%Y-%m-%d') as  CasoFechaLegal," +
                                            "     CasoNumConcedida," +
                                            "     CasoNumeroExpedienteLargo," +
                                            "     CasoNumero," +
                                            "     ResponsableId," +
                                            "     CasoTipoCaptura," +
                                            "     CasoTitular," +
                                            "     EstatusCasoId," +
                                            "     UsuarioId," +
                                            "     AreaImpiId," +
                                            "     CasoDisenoClasificacion," +
                                            "     DATE_FORMAT(CasoFechaInternacional , '%Y-%m-%d') as  CasoFechaInternacional," +
                                            "     PaisId," +
                                            "     DATE_FORMAT(CasoFechaPruebaUsoSig , '%Y-%m-%d') as  CasoFechaPruebaUsoSig," +
                                            "     DATE_FORMAT(CasoFechaFilingCliente , '%Y-%m-%d') as  CasoFechaFilingCliente," +
                                            "     DATE_FORMAT(CasoFechaFilingSistema , '%Y-%m-%d') as  CasoFechaFilingSistema," +
                                            "     DATE_FORMAT(CasoFechaDivulgacionPrevia , '%Y-%m-%d') as  CasoFechaDivulgacionPrevia," +
                                            "     DATE_FORMAT(CasoFechaCartaCliente , '%Y-%m-%d') as  CasoFechaCartaCliente" +
                                            " FROM" +
                                            "     caso" +
                                            " WHERE" +
                                            "     tiposolicitudid = 1 or tiposolicitudid = 2 or tiposolicitudid = 3 or tiposolicitudid = 4 or tiposolicitudid = 5 or tiposolicitudid = 19;";
                    MySqlDataReader respuestastring3 = con_ipfacts.getdatareader(sPatentesconsulta);
                    while (respuestastring3.Read())
                    {
                    String sInsert_patentes = "INSERT INTO `caso_patente` " +
                                            " (`CasoId`, " +
                                            " `TipoSolicitudId`, " +
                                            " `SubTipoSolicitudId`, " +
                                            " `TipoPctId`, " +
                                            " `CasoTituloespanol`, " +
                                            " `CasoTituloingles`, " +
                                            " `IdiomaId`, " +
                                            " `CasoFechaConcesion`, " +
                                            " `CasoFechaRecepcion`, " +
                                            " `CasoFechaVigencia`, " +
                                            " `CasoFechaPublicacionSolicitud`, " +
                                            " `CasoFechaLegal`, " +
                                            " `CasoNumConcedida`, " +
                                            " `CasoNumeroExpedienteLargo`, " +
                                            " `CasoNumero`, " +
                                            " `ResponsableId`," +
                                            " `CasoTipoCaptura`, " +
                                            " `CasoTitular`, " +
                                            " `EstatusCasoId`, " +
                                            " `UsuarioId`, " +
                                            " `AreaImpiId`, " +
                                            " `CasoFechaInternacional`, " +
                                            " `PaisId`, " +
                                            " `CasoFechaPruebaUsoSig`, " +
                                            "  CasoFechaFilingCliente," +
                                            " `CasoFechaFilingSistema`, " +
                                            " `CasoFechaDivulgacionPrevia`, " +
                                            " `CasoDisenoClasificacion`, " +
                                            " `CasoFechaCartaCliente`) " +
                                            " VALUES (" + getcampo(validareader("CasoId", "CasoId", respuestastring3).Text) + ", " +
                                            getcampo(validareader("TipoSolicitudId", "CasoId", respuestastring3).Text) + " , " +
                                            getcampo(validareader("SubTipoSolicitudId", "CasoId", respuestastring3).Text) + " , " +
                                            getcampo(validareader("TipoPctId", "CasoId", respuestastring3).Text) + " , " +
                                            getcampo(validareader("CasoDenominacion", "CasoId", respuestastring3).Text) + " , " +
                                            getcampo(validareader("CasoTitulo", "CasoId", respuestastring3).Text) + " , " +
                                            getcampo(validareader("IdiomaId", "CasoId", respuestastring3).Text) + " , " +
                                            getcampo(validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text) + " , " +
                                            getcampo(validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text) + " , " +
                                            getcampo(validareader("CasoFechaVigencia", "CasoId", respuestastring3).Text) + " , " +
                                            getcampo(validareader("CasoFechaPublicacionSolicitud", "CasoId", respuestastring3).Text) + " , " +
                                            getcampo(validareader("CasoFechaLegal", "CasoId", respuestastring3).Text) + " , " +
                                            getcampo(validareader("CasoNumConcedida", "CasoId", respuestastring3).Text) + " , " +
                                            getcampo(validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text) + " , " +
                                            getcampo(validareader("CasoNumero", "CasoId", respuestastring3).Text) + " , " +
                                            getcampo(validareader("ResponsableId", "CasoId", respuestastring3).Text) + " , " +
                                            getcampo(validareader("CasoTipoCaptura", "CasoId", respuestastring3).Text) + " , " +
                                            getcampo(validareader("CasoTitular", "CasoId", respuestastring3).Text) + " , " +
                                            getcampo(validareader("EstatusCasoId", "CasoId", respuestastring3).Text) + " , " +
                                            getcampo(validareader("UsuarioId", "CasoId", respuestastring3).Text) + " , " +
                                            getcampo(validareader("AreaImpiId", "CasoId", respuestastring3).Text) + " , " +
                                            getcampo(validareader("CasoFechaInternacional", "CasoId", respuestastring3).Text) + " , " +
                                            getcampo(validareader("PaisId", "CasoId", respuestastring3).Text) + " , " +
                                            getcampo(validareader("CasoFechaPruebaUsoSig", "CasoId", respuestastring3).Text) + " , " +
                                            getcampo(validareader("CasoFechaFilingCliente", "CasoId", respuestastring3).Text) + " , " +
                                            getcampo(validareader("CasoFechaFilingSistema", "CasoId", respuestastring3).Text) + " , " +
                                            getcampo(validareader("CasoFechaDivulgacionPrevia", "CasoId", respuestastring3).Text) + " , " +
                                            getcampo(validareader("CasoDisenoClasificacion", "CasoId", respuestastring3).Text) + " , " +
                                            getcampo(validareader("CasoFechaCartaCliente", "CasoId", respuestastring3).Text) + " );";
                        conect con_pruebas = new conect();
                        sInsert_patentes = sInsert_patentes.Replace("\"\"", "null");
                        MySqlDataReader ejecutaacualizapatentes_insert = con_pruebas.getdatareader(sInsert_patentes);
                        ejecutaacualizapatentes_insert.Read();
                        ejecutaacualizapatentes_insert.Close();
                        con_pruebas.Cerrarconexion();
                        //stringquerypatentes += sInsert + "\n";
                    }
                    respuestastring3.Close();
                    con_ipfacts.Cerrarconexion();
                }catch(Exception E){
                    new filelog(form1contruct.sId, E.Message.ToString());
                }
            }



            public void actualizatablasrelacionales()
            {
                try
                {
                    //borramos la tabla 
                    conect con_pruebas_tr = new conect();
                    //borramos todas las tablas de relacion
                    MySqlDataReader ejecutaacualizapatentes = con_pruebas_tr.getdatareader("truncate table casointeresado; truncate table casocliente; truncate table prioridad; truncate table plazo; truncate table casoproductos; truncate table referencia; truncate table anualidad; truncate table relaciondocumento;");
                    ejecutaacualizapatentes.Read();
                    ejecutaacualizapatentes.Close();
                    con_pruebas_tr.Cerrarconexion();
                    //actualizamos la tabla
                    conect_ipfacts con = new conect_ipfacts();
                    //Para updates
                    String stringquerypatentes = "";
                    String sPatentesconsulta = " SELECT * from interesado ";
                    MySqlDataReader respuestastring3 = con.getdatareader(sPatentesconsulta);
                    while (respuestastring3.Read())
                    {
                        conect coninteresado = new conect();
                        String sInsert = "UPDATE `casointeresado` SET `TipoSolicitudId` = " + getcampo(validareader("TipoSolicitudId", "CasoId", respuestastring3).Text) +
                                         " WHERE `casointeresado`.`CasoId` = " + validareader("CasoId", "CasoId", respuestastring3).Text + ";";
                        MySqlDataReader respuestastrig_interesado = coninteresado.getdatareader(sPatentesconsulta);
                        respuestastrig_interesado.Close();
                        coninteresado.Cerrarconexion();
                    }

                }
                catch (Exception E)
                {
                    new filelog(form1contruct.sId, E.ToString());
                }
            }

            public void actualizadirecciones()
            {
                try
                {
                    //borramos la tabla 
                    conect con_pruebas_tr = new conect();
                    //borramos todas las tablas de relacion
                    MySqlDataReader ejecutaacualizapatentes = con_pruebas_tr.getdatareader("truncate table direccion;");
                    ejecutaacualizapatentes.Read();
                    ejecutaacualizapatentes.Close();
                    con_pruebas_tr.Cerrarconexion();
                    //actualizamos la tabla
                    conect_ipfacts con = new conect_ipfacts();
                    //Para updates
                    String stringquerypatentes = "";
                    String sPatentesconsulta = " SELECT * from direccion;";

                    String squeryinteresados = "";

                    MySqlDataReader respuestastring3 = con.getdatareader(sPatentesconsulta);
                    while (respuestastring3.Read())
                    {
                        squeryinteresados = " INSERT INTO `direccion` " +
                                            " (`DireccionID`, " +
                                            " `DireccionCalle`, " +
                                            " `DireccionNumExt`, " +
                                            " `DireccionNumInt`, " +
                                            " `DireccionColonia`, " +
                                            " `DireccionPoblacion`, " +
                                            " `DireccionEstado`, " +
                                            " `DireccionCP`, " +
                                            " `DireccionIndAct`, " +
                                            " `PaisId`, " +
                                            " `ContactoId`, " +
                                            " `InteresadoId`, " +
                                            " `ClienteId`, " +
                                            " `CasoId`, " +
                                            " `TipoDireccionId`) " +
                                            " VALUES " +
                                            " ( " +
                                            getcampo(validareader("DireccionID", "DireccionID", respuestastring3).Text) + ", " +
                                            getcampo(validareader("DireccionCalle", "DireccionID", respuestastring3).Text) + ", " +
                                            getcampo(validareader("DireccionNumExt", "DireccionID", respuestastring3).Text) + ", " +
                                            getcampo(validareader("DireccionNumInt", "DireccionID", respuestastring3).Text) + ", " +
                                            getcampo(validareader("DireccionColonia", "DireccionID", respuestastring3).Text) + ", " +
                                            getcampo(validareader("DireccionPoblacion", "DireccionID", respuestastring3).Text) + ", " +
                                            getcampo(validareader("DireccionEstado", "DireccionID", respuestastring3).Text) + ", " +
                                            getcampo(validareader("DireccionCP", "DireccionID", respuestastring3).Text) + ", " +
                                            getcampo(validareader("DireccionIndAct", "DireccionID", respuestastring3).Text) + ", " +
                                            getcampo(validareader("PaisId", "DireccionID", respuestastring3).Text) + ", " +
                                            getcampo(validareader("ContactoId", "DireccionID", respuestastring3).Text) + ", " +
                                            getcampo(validareader("InteresadoId", "DireccionID", respuestastring3).Text) + ", " +
                                            getcampo(validareader("ClienteId", "DireccionID", respuestastring3).Text) + ", " +
                                            getcampo(validareader("CasoId", "DireccionID", respuestastring3).Text) + ", " +
                                            getcampo(validareader("TipoDireccionId", "DireccionID", respuestastring3).Text) + ");";

                        conect coninteresado = new conect();
                        MySqlDataReader respuestastrig_interesado = coninteresado.getdatareader(squeryinteresados);
                        respuestastrig_interesado.Close();
                        coninteresado.Cerrarconexion();
                    }
                    respuestastring3.Close();
                    con.Cerrarconexion();
                }
                catch (Exception E)
                {
                    new filelog(form1contruct.sId, E.ToString());
                }
            }

            public void actualizainteresados()
            {
                try {
                    //borramos la tabla 
                    conect con_pruebas_tr = new conect();
                    //borramos todas las tablas de relacion
                    MySqlDataReader ejecutaacualizapatentes = con_pruebas_tr.getdatareader("truncate table interesado;");
                    ejecutaacualizapatentes.Read();
                    ejecutaacualizapatentes.Close();
                    con_pruebas_tr.Cerrarconexion();
                    //actualizamos la tabla
                    conect_ipfacts con = new conect_ipfacts();
                    //Para updates
                    String stringquerypatentes = "";
                    String sPatentesconsulta = "  SELECT `InteresadoID`, " +
                                                " `InteresadoTipoPersonaSAT`, " +
                                                " `InteresadoNombre`, " +
                                                " `InteresadoApPaterno`, " +
                                                " `InteresadoApMaterno`, " +
                                                " `InteresadoRFC`, " +
                                                " `SociedadID`, " +
                                                " `InteresadoRGP`, " +
                                                " DATE_FORMAT(InteresadoFechaAlta , '%Y-%m-%d %H:%i:%S') as `InteresadoFechaAlta`, " +
                                                " `PaisId`, " +
                                                " `InteresadoIndAct`, " +
                                                " `InteresadoShort`, " +
                                                " `InteresadoPoder`, " +
                                                " `InteresadoCurp`, " +
                                                " `InteresadoMail`, " +
                                                " `InteresadoTelefono`, " +
                                                " `holderid` " +
                                                " FROM `interesado`; ";

                    String squeryinteresados = "";

                    MySqlDataReader respuestastring3 = con.getdatareader(sPatentesconsulta);
                    while (respuestastring3.Read())
                    {
                        squeryinteresados = " INSERT INTO `interesado`" +
                                            " (`InteresadoID`," +
                                            " `InteresadoTipoPersonaSAT`," +
                                            " `InteresadoNombre`," +
                                            " `InteresadoApPaterno`," +
                                            " `InteresadoApMaterno`," +
                                            " `InteresadoRFC`," +
                                            " `SociedadID`," +
                                            " `InteresadoRGP`," +
                                            " `InteresadoFechaAlta`," +
                                            " `PaisId`," +
                                            " `InteresadoIndAct`," +
                                            " `InteresadoShort`," +
                                            " `InteresadoPoder`," +
                                            " `InteresadoCurp`," +
                                            " `InteresadoMail`," +
                                            " `InteresadoTelefono`," +
                                            " `holderid`)" +
                                            " VALUES" +
                                            " ("+
                                            " " + getcampo(validareader("InteresadoID", "InteresadoID", respuestastring3).Text) + "," +
                                            " " + getcampo(validareader("InteresadoTipoPersonaSAT", "InteresadoID", respuestastring3).Text) + "," +
                                            " " + getcampo(validareader("InteresadoNombre", "InteresadoID", respuestastring3).Text) + "," +
                                            " " + getcampo(validareader("InteresadoApPaterno", "InteresadoID", respuestastring3).Text) + "," +
                                            " " + getcampo(validareader("InteresadoApMaterno", "InteresadoID", respuestastring3).Text) + "," +
                                            " " + getcampo(validareader("InteresadoRFC", "InteresadoID", respuestastring3).Text) + "," +
                                            " " + getcampo(validareader("SociedadID", "InteresadoID", respuestastring3).Text) + "," +
                                            " " + getcampo(validareader("InteresadoRGP", "InteresadoID", respuestastring3).Text) + "," +
                                            " " + getcampo(validareader("InteresadoFechaAlta", "InteresadoID", respuestastring3).Text) + "," +
                                            " " + getcampo(validareader("PaisId", "InteresadoID", respuestastring3).Text) + "," +
                                            " " + getcampo(validareader("InteresadoIndAct", "InteresadoID", respuestastring3).Text) + "," +
                                            " " + getcampo(validareader("InteresadoShort", "InteresadoID", respuestastring3).Text) + "," +
                                            " " + getcampo(validareader("InteresadoPoder", "InteresadoID", respuestastring3).Text) + "," +
                                            " " + getcampo(validareader("InteresadoCurp", "InteresadoID", respuestastring3).Text) + "," +
                                            " " + getcampo(validareader("InteresadoMail", "InteresadoID", respuestastring3).Text) + "," +
                                            " " + getcampo(validareader("InteresadoTelefono", "InteresadoID", respuestastring3).Text) + "," +
                                            " " + getcampo(validareader("holderid", "InteresadoID", respuestastring3).Text) + ");";

                        conect coninteresado = new conect();
                        MySqlDataReader respuestastrig_interesado = coninteresado.getdatareader(squeryinteresados);
                        respuestastrig_interesado.Close();
                        coninteresado.Cerrarconexion();

                    }
                    respuestastring3.Close();
                    con.Cerrarconexion();
                    

                }catch (Exception E){
                    new filelog(form1contruct.sId, E.ToString());
                }
            }
            private void actDbToolStripMenuItem_Click(object sender, EventArgs e)
            {
                //getcampo("\"Rostekhnologii'\"");
                conect con = new conect();
                //String sQcasooposicion = " SELECT  " +
                //                "     CasoId, " +
                //                "     TipoSolicitudId, " +
                //                "     SubTipoSolicitudId, " +
                //                "     CasoDenominacion, " +
                //                "     CasoTitulo, " +
                //                "     DATE_FORMAT(CasoFechaPresentacion , '%Y-%m-%d') as  CasoFechaPresentacion, " +
                //                "     CasoNumeroExpedienteLargo, " +
                //                "     CasoNumero, " +
                //                "     ResponsableId, " +
                //                "     DATE_FORMAT(CasoFechaAlta , '%Y-%m-%d') as  CasoFechaAlta, " +
                //                "     CasoTipoCaptura, " +
                //                "     CasoTitular, " +
                //                "     DATE_FORMAT(CasoFechaFilingSistema , '%Y-%m-%d') as  CasoFechaFilingSistema, " +
                //                "     DATE_FORMAT(CasoFechaFilingCliente , '%Y-%m-%d') as  CasoFechaFilingCliente, " +
                //                "     DATE_FORMAT(CasoFechaCartaCliente , '%Y-%m-%d') as  CasoFechaCartaCliente, " +
                //                "     EstatusCasoId, " +
                //                "     UsuarioId, " +
                //                "     PaisId " +
                //                " FROM " +
                //                "     caso " +
                //                " WHERE " +
                //                "     tiposolicitudid = 14; ";
                //String stringquerymarcas = "";
                //MySqlDataReader respuestastring3 = con.getdatareader(sQcasooposicion);

                //while (respuestastring3.Read())
                //{
                //    String sInsertopo = " INSERT INTO caso_oposicion ("+
                //                        " CasoId, "+
                //                        " TipoSolicitudId, "+
                //                        " SubTipoSolicitudId, "+
                //                        " CasoTituloespanol, " +
                //                        " CasoTituloingles, " +
                //                        " CasoFechaPresentacion, "+
                //                        " CasoNumeroExpedienteLargo, "+
                //                        " CasoNumero, "+
                //                        " ResponsableId, "+
                //                        " CasoFechaAlta, " +
                //                        " CasoTipoCaptura, "+
                //                        " CasoTitular, "+
                //                        " CasoFechaFilingSistema, "+
                //                        " CasoFechaFilingCliente, "+
                //                        " CasoFechaCartaCliente," +
                //                        " EstatusCasoId, "+
                //                        " UsuarioId, "+
                //                        " PaisId) " +
                //                        " VALUES (" +
                //                        "  '" + validareader("CasoId", "CasoId", respuestastring3).Text + "'," +
                //                        "  '" + validareader("TipoSolicitudId", "CasoId", respuestastring3).Text + "'," +
                //                        "  '" + validareader("SubTipoSolicitudId", "CasoId", respuestastring3).Text + "'," +
                //                        "  '" + validareader("CasoDenominacion", "CasoId", respuestastring3).Text + "'," +
                //                        "  '" + validareader("CasoTitulo", "CasoId", respuestastring3).Text + "'," +
                //                        "  '" + validareader("CasoFechaPresentacion", "CasoId", respuestastring3).Text + "'," +
                //                        "  '" + validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text + "'," +
                //                        "  '" + validareader("CasoNumero", "CasoId", respuestastring3).Text + "'," +
                //                        "  '" + validareader("ResponsableId", "CasoId", respuestastring3).Text + "'," +
                //                        "  '" + validareader("CasoFechaAlta", "CasoId", respuestastring3).Text + "'," +
                //                        "  '" + validareader("CasoTipoCaptura", "CasoId", respuestastring3).Text + "'," +
                //                        "  '" + validareader("CasoTitular", "CasoId", respuestastring3).Text + "'," +
                //                        "  '" + validareader("CasoFechaFilingSistema", "CasoId", respuestastring3).Text + "'," +
                //                        "  '" + validareader("CasoFechaFilingCliente", "CasoId", respuestastring3).Text + "'," +
                //                        "  '" + validareader("CasoFechaCartaCliente", "CasoId", respuestastring3).Text + "'," +
                //                        "  '" + validareader("EstatusCasoId", "CasoId", respuestastring3).Text + "'," +
                //                        "  '" + validareader("UsuarioId", "CasoId", respuestastring3).Text + "'," +
                //                        "  '" + validareader("PaisId", "CasoId", respuestastring3).Text + "');";
                //    //MySqlDataReader respuestastring4 = con.getdatareader(sInsertopo);
                //    stringquerymarcas += sInsertopo;
                //}


                ////PARA MARCAS 
                //String querymarcas = "    SELECT " +
                //                        "   CasoId, " +
                //                        "   TipoSolicitudId, " +
                //                        "   SubTipoSolicitudId, " +
                //                        "   CasoTitulo, " +
                //                        "   CasoDenominacion, " +
                //                        "   IdiomaId, " +
                //                        "   DATE_FORMAT(CasoFechaConcesion , '%Y-%m-%d') as  CasoFechaConcesion, " +
                //                        "   DATE_FORMAT(CasoFechaLegal , '%Y-%m-%d') as  CasoFechaLegal, " +
                //                        "   DATE_FORMAT(CasoFechaDivulgacionPrevia , '%Y-%m-%d') as  CasoFechaDivulgacionPrevia, " +
                //                        "   DATE_FORMAT(CasoFechaRecepcion , '%Y-%m-%d') as  CasoFechaRecepcion, " +
                //                        "   DATE_FORMAT(CasoFechaVigencia , '%Y-%m-%d') as  CasoFechaVigencia, " +
                //                        "   CasoNumeroConcedida, " +
                //                        "   CasoNumeroExpedienteLargo, " +
                //                        "   CasoNumero, " +
                //                        "   ResponsableId, " +
                //                        "   TipoMarcaId, " +
                //                        "   CasoLeyendaNoReservable, " +
                //                        "   DATE_FORMAT(CasoFechaAlta , '%Y-%m-%d') as  CasoFechaAlta,  " +
                //                        "   CasoTipoCaptura, " +
                //                        "   CasoTitular,  " +
                //                        "   DATE_FORMAT(CasoFechaFilingSistema , '%Y-%m-%d') as  CasoFechaFilingSistema, " +
                //                        "   DATE_FORMAT(CasoFechaFilingCliente , '%Y-%m-%d') as  CasoFechaFilingCliente,  " +
                //                        "   DATE_FORMAT(CasoFechaCartaCliente , '%Y-%m-%d') as  CasoFechaCartaCliente,  " +
                //                        "   EstatusCasoId,  " +
                //                        "   UsuarioId,  " +
                //                        "   PaisId, " +
                //                        "   DATE_FORMAT(CasoFechaPruebaUsoSig , '%Y-%m-%d') as  CasoFechaPruebaUsoSig, " +
                //                        "   CasoNumConcedida, " +
                //                        "   CasoFechaPruebaUso " +
                //    //"   CasoFechainiciouso " +
                //                        " FROM " +
                //                        "     caso " +
                //                        " WHERE " +
                //                        "     tiposolicitudid = 7 or tiposolicitudid = 8 or tiposolicitudid = 9 or tiposolicitudid = 77; ";
                //MySqlDataReader respuestastring3 = con.getdatareader(querymarcas);
                //String stringquerymarcas = "";
                //while (respuestastring3.Read())
                //{

                //    String sQuerymarcasinsert = " INSERT INTO `caso_marcas` (" +
                //                                " `CasoId`, " +
                //                                " `TipoSolicitudId`, " +
                //                                " `SubTipoSolicitudId`, " +
                //                                " `CasoTituloespanol`, " +
                //                                " `CasoTituloingles`, " +
                //                                " `IdiomaId`, " +
                //                                " `CasoFechaConcesion`," +
                //                                " `CasoFechaLegal`,  " +
                //                                " `CasoFechaDivulgacionPrevia`, " +
                //                                " `CasoFechaRecepcion`, " +
                //                                " `CasoFechaVigencia`, " +
                //                                " `CasoNumeroConcedida`, " +
                //                                " `CasoNumeroExpedienteLargo`, " +
                //                                " `CasoNumero`, " +
                //                                " `ResponsableId`, " +
                //                                " `TipoMarcaId`,  " +
                //                                " `CasoLeyendaNoReservable`, " +
                //                                " `CasoFechaAlta`, " +
                //                                " `CasoTipoCaptura`, " +
                //                                " `CasoTitular`, " +
                //                                " `CasoFechaFilingSistema`, " +
                //                                " `CasoFechaFilingCliente`, " +
                //                                " `CasoFechaCartaCliente`, " +
                //                                " `EstatusCasoId`, " +
                //                                " `UsuarioId`,  " +
                //                                " `PaisId`,  " +
                //                                " `CasoFechaPruebaUsoSig`, " +
                //                                " `CasoNumConcedida` ," +
                //                                " `CasoFechaprobouso`, " +
                //                                " `CasoFechainiciouso`" +
                //                                " )  " +
                //                                " VALUES  " +
                //                                " ( " +
                //                                getcampo(validareader("CasoId", "CasoId", respuestastring3).Text) + ',' +
                //                                getcampo(validareader("TipoSolicitudId", "CasoId", respuestastring3).Text) + ',' +
                //                                getcampo(validareader("SubTipoSolicitudId", "CasoId", respuestastring3).Text) + ',' +
                //                                getcampo(validareader("CasoTitulo", "CasoId", respuestastring3).Text) + ',' +
                //                                getcampo(validareader("CasoDenominacion", "CasoId", respuestastring3).Text) + ',' +
                //                                getcampo(validareader("IdiomaId", "CasoId", respuestastring3).Text) + ',' +
                //                                getcampo(validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text) + ',' +
                //                                getcampo(validareader("CasoFechaLegal", "CasoId", respuestastring3).Text) + ',' +
                //                                getcampo(validareader("CasoFechaDivulgacionPrevia", "CasoId", respuestastring3).Text) + ',' +
                //                                getcampo(validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text) + ',' +
                //                                getcampo(validareader("CasoFechaVigencia", "CasoId", respuestastring3).Text) + ',' +
                //                                getcampo(validareader("CasoNumeroConcedida", "CasoId", respuestastring3).Text) + ',' +
                //                                getcampo(validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text) + ',' +
                //                                getcampo(validareader("CasoNumero", "CasoId", respuestastring3).Text) + ',' +
                //                                getcampo(validareader("ResponsableId", "CasoId", respuestastring3).Text) + ',' +
                //                                getcampo(validareader("TipoMarcaId", "CasoId", respuestastring3).Text) + ',' +
                //                                getcampo(validareader("CasoLeyendaNoReservable", "CasoId", respuestastring3).Text) + ',' +
                //                                getcampo(validareader("CasoFechaAlta", "CasoId", respuestastring3).Text) + ',' +
                //                                getcampo(validareader("CasoTipoCaptura", "CasoId", respuestastring3).Text) + ',' +
                //                                getcampo(validareader("CasoTitular", "CasoId", respuestastring3).Text) + ',' +
                //                                getcampo(validareader("CasoFechaFilingSistema", "CasoId", respuestastring3).Text) + ',' +
                //                                getcampo(validareader("CasoFechaFilingCliente", "CasoId", respuestastring3).Text) + ',' +
                //                                getcampo(validareader("CasoFechaCartaCliente", "CasoId", respuestastring3).Text) + ',' +
                //                                getcampo(validareader("EstatusCasoId", "CasoId", respuestastring3).Text) + ',' +
                //                                getcampo(validareader("UsuarioId", "CasoId", respuestastring3).Text) + ',' +
                //                                getcampo(validareader("PaisId", "CasoId", respuestastring3).Text) + ',' +
                //                                getcampo(validareader("CasoFechaPruebaUsoSig", "CasoId", respuestastring3).Text) + ',' +
                //                                getcampo(validareader("CasoNumConcedida", "CasoId", respuestastring3).Text) + ',' +
                //                                getcampo(validareader("CasoFechaPruebaUso", "CasoId", respuestastring3).Text) + ',' +
                //                                "''" +
                //                                ");";
                //    stringquerymarcas += sQuerymarcasinsert + "\n";
                //    //MySqlDataReader respuestastring4 = con.getdatareader(sQuerymarcasinsert);
                //}

                //Para patentes
                //conect con = new conect();
                //String stringquerypatentes = "";
                //String sPatentesconsulta = " SELECT " +
                //                        "     CasoId," +
                //                        "     TipoSolicitudId," +
                //                        "     SubTipoSolicitudId," +
                //                        "     TipoPctId," +
                //                        "     CasoDenominacion," +
                //                        "     CasoTitulo," +
                //                        "     IdiomaId," +
                //                        "     DATE_FORMAT(CasoFechaConcesion , '%Y-%m-%d') as  CasoFechaConcesion," +
                //                        "     DATE_FORMAT(CasoFechaRecepcion , '%Y-%m-%d') as  CasoFechaRecepcion," +
                //                        "     DATE_FORMAT(CasoFechaVigencia , '%Y-%m-%d') as  CasoFechaVigencia," +
                //                        "     DATE_FORMAT(CasoFechaPublicacionSolicitud , '%Y-%m-%d') as  CasoFechaPublicacionSolicitud," +
                //                        "     DATE_FORMAT(CasoFechaLegal , '%Y-%m-%d') as  CasoFechaLegal," +
                //                        "     CasoNumConcedida," +
                //                        "     CasoNumeroExpedienteLargo," +
                //                        "     CasoNumero," +
                //                        "     ResponsableId," +
                //                        "     CasoTipoCaptura," +
                //                        "     CasoTitular," +
                //                        "     EstatusCasoId," +
                //                        "     UsuarioId," +
                //                        "     AreaImpiId," +
                //                        "     DATE_FORMAT(CasoFechaInternacional , '%Y-%m-%d') as  CasoFechaInternacional," +
                //                        "     PaisId," +
                //                        "     DATE_FORMAT(CasoFechaPruebaUsoSig , '%Y-%m-%d') as  CasoFechaPruebaUsoSig," +
                //                        "     DATE_FORMAT(CasoFechaFilingCliente , '%Y-%m-%d') as  CasoFechaFilingCliente," +
                //                        "     DATE_FORMAT(CasoFechaFilingSistema , '%Y-%m-%d') as  CasoFechaFilingSistema," +
                //                        "     DATE_FORMAT(CasoFechaDivulgacionPrevia , '%Y-%m-%d') as  CasoFechaDivulgacionPrevia," +
                //                        "     DATE_FORMAT(CasoFechaCartaCliente , '%Y-%m-%d') as  CasoFechaCartaCliente" +
                //                        " FROM" +
                //                        "     caso" +
                //                        " WHERE" +
                //                        "     tiposolicitudid = 1 or tiposolicitudid = 2 or tiposolicitudid = 3 or tiposolicitudid = 4 or tiposolicitudid = 5 or tiposolicitudid = 19;";
                //MySqlDataReader respuestastring3 = con.getdatareader(sPatentesconsulta);
                //while (respuestastring3.Read())
                //{
                //    String sInsert = "INSERT INTO `caso_patente` " +
                //                        " (`CasoId`, " +
                //                        " `TipoSolicitudId`, " +
                //                        " `SubTipoSolicitudId`, " +
                //                        " `TipoPctId`, " +
                //                        " `CasoTituloespanol`, " +
                //                        " `CasoTituloingles`, " +
                //                        " `IdiomaId`, " +
                //                        " `CasoFechaConcesion`, " +
                //                        " `CasoFechaRecepcion`, " +
                //                        " `CasoFechaVigencia`, " +
                //                        " `CasoFechaPublicacionSolicitud`, " +
                //                        " `CasoFechaLegal`, " +
                //                        " `CasoNumConcedida`, " +
                //                        " `CasoNumeroExpedienteLargo`, " +
                //                        " `CasoNumero`, " +
                //                        " `ResponsableId`," +
                //                        " `CasoTipoCaptura`, " +
                //                        " `CasoTitular`, " +
                //                        " `EstatusCasoId`, " +
                //                        " `UsuarioId`, " +
                //                        " `AreaImpiId`, " +
                //                        " `CasoFechaInternacional`, " +
                //                        " `PaisId`, " +
                //                        " `CasoFechaPruebaUsoSig`, " +
                //                        "  CasoFechaFilingCliente," +
                //                        " `CasoFechaFilingSistema`, " +
                //                        " `CasoFechaDivulgacionPrevia`, " +
                //                        " `CasoFechaCartaCliente`) " +
                //                        " VALUES (" + getcampo(validareader("CasoId", "CasoId", respuestastring3).Text) + ", " +
                //                        getcampo(validareader("TipoSolicitudId", "CasoId", respuestastring3).Text) + " , " +
                //                        getcampo(validareader("SubTipoSolicitudId", "CasoId", respuestastring3).Text) + " , " +
                //                        getcampo(validareader("TipoPctId", "CasoId", respuestastring3).Text) + " , " +
                //                        getcampo(validareader("CasoDenominacion", "CasoId", respuestastring3).Text) + " , " +
                //                        getcampo(validareader("CasoTitulo", "CasoId", respuestastring3).Text) + " , " +
                //                        getcampo(validareader("IdiomaId", "CasoId", respuestastring3).Text) + " , " +
                //                        getcampo(validareader("CasoFechaConcesion", "CasoId", respuestastring3).Text) + " , " +
                //                        getcampo(validareader("CasoFechaRecepcion", "CasoId", respuestastring3).Text) + " , " +
                //                        getcampo(validareader("CasoFechaVigencia", "CasoId", respuestastring3).Text) + " , " +
                //                        getcampo(validareader("CasoFechaPublicacionSolicitud", "CasoId", respuestastring3).Text) + " , " +
                //                        getcampo(validareader("CasoFechaLegal", "CasoId", respuestastring3).Text) + " , " +
                //                        getcampo(validareader("CasoNumConcedida", "CasoId", respuestastring3).Text) + " , " +
                //                        getcampo(validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text) + " , " +
                //                        getcampo(validareader("CasoNumero", "CasoId", respuestastring3).Text) + " , " +
                //                        getcampo(validareader("ResponsableId", "CasoId", respuestastring3).Text) + " , " +
                //                        getcampo(validareader("CasoTipoCaptura", "CasoId", respuestastring3).Text) + " , " +
                //                        getcampo(validareader("CasoTitular", "CasoId", respuestastring3).Text) + " , " +
                //                        getcampo(validareader("EstatusCasoId", "CasoId", respuestastring3).Text) + " , " +
                //                        getcampo(validareader("UsuarioId", "CasoId", respuestastring3).Text) + " , " +
                //                        getcampo(validareader("AreaImpiId", "CasoId", respuestastring3).Text) + " , " +
                //                        getcampo(validareader("CasoFechaInternacional", "CasoId", respuestastring3).Text) + " , " +
                //                        getcampo(validareader("PaisId", "CasoId", respuestastring3).Text) + " , " +
                //                        getcampo(validareader("CasoFechaPruebaUsoSig", "CasoId", respuestastring3).Text) + " , " +
                //                        getcampo(validareader("CasoFechaFilingCliente", "CasoId", respuestastring3).Text) + " , " +
                //                        getcampo(validareader("CasoFechaFilingSistema", "CasoId", respuestastring3).Text) + " , " +
                //                        getcampo(validareader("CasoFechaDivulgacionPrevia", "CasoId", respuestastring3).Text) + " , " +
                //                        getcampo(validareader("CasoFechaCartaCliente", "CasoId", respuestastring3).Text) + " );";
                //    stringquerypatentes += sInsert + "\n";
                //}
                //respuestastring3.Close();
                //con.Cerrarconexion();

                //Para updates
                String stringquerypatentes = "";
                String sPatentesconsulta = " SELECT " +
                                        "     CasoId," +
                                        "     TipoSolicitudId," +
                                        "     SubTipoSolicitudId," +
                                        "     TipoPctId," +
                                        "     CasoDenominacion," +
                                        "     CasoTitulo," +
                                        "     IdiomaId," +
                                        "     DATE_FORMAT(CasoFechaConcesion , '%Y-%m-%d') as  CasoFechaConcesion," +
                                        "     DATE_FORMAT(CasoFechaRecepcion , '%Y-%m-%d') as  CasoFechaRecepcion," +
                                        "     DATE_FORMAT(CasoFechaVigencia , '%Y-%m-%d') as  CasoFechaVigencia," +
                                        "     DATE_FORMAT(CasoFechaPublicacionSolicitud , '%Y-%m-%d') as  CasoFechaPublicacionSolicitud," +
                                        "     DATE_FORMAT(CasoFechaLegal , '%Y-%m-%d') as  CasoFechaLegal," +
                                        "     CasoNumConcedida," +
                                        "     CasoNumeroExpedienteLargo," +
                                        "     CasoNumero," +
                                        "     ResponsableId," +
                                        "     CasoTipoCaptura," +
                                        "     CasoTitular," +
                                        "     EstatusCasoId," +
                                        "     UsuarioId," +
                                        "     AreaImpiId," +
                                        "     DATE_FORMAT(CasoFechaInternacional , '%Y-%m-%d') as  CasoFechaInternacional," +
                                        "     PaisId," +
                                        "     DATE_FORMAT(CasoFechaPruebaUsoSig , '%Y-%m-%d') as  CasoFechaPruebaUsoSig," +
                                        "     DATE_FORMAT(CasoFechaFilingCliente , '%Y-%m-%d') as  CasoFechaFilingCliente," +
                                        "     DATE_FORMAT(CasoFechaFilingSistema , '%Y-%m-%d') as  CasoFechaFilingSistema," +
                                        "     DATE_FORMAT(CasoFechaDivulgacionPrevia , '%Y-%m-%d') as  CasoFechaDivulgacionPrevia," +
                                        "     DATE_FORMAT(CasoFechaCartaCliente , '%Y-%m-%d') as  CasoFechaCartaCliente" +
                                        " FROM" +
                                        "     caso";
                MySqlDataReader respuestastring3 = con.getdatareader(sPatentesconsulta);
                while (respuestastring3.Read())
                {
                    String sInsert = "UPDATE `casointeresado` SET `TipoSolicitudId` = " + getcampo(validareader("TipoSolicitudId", "CasoId", respuestastring3).Text) +
                                     " WHERE `casointeresado`.`CasoId` = " + validareader("CasoId", "CasoId", respuestastring3).Text + ";";

                    String sUpdatecasocliente = "UPDATE `casocliente` SET `TipoSolicitudId` = " + getcampo(validareader("TipoSolicitudId", "CasoId", respuestastring3).Text) +
                                     " WHERE `casocliente`.`CasoId` = " + validareader("CasoId", "CasoId", respuestastring3).Text + ";";

                    String sUpdateprioridad = "UPDATE `prioridad` SET `TipoSolicitudId` = " + getcampo(validareader("TipoSolicitudId", "CasoId", respuestastring3).Text) +
                                     " WHERE `prioridad`.`CasoId` = " + validareader("CasoId", "CasoId", respuestastring3).Text + ";";

                    String sUpdateplazo = "UPDATE `plazo` SET `TipoSolicitudId` = " + getcampo(validareader("TipoSolicitudId", "CasoId", respuestastring3).Text) +
                                     " WHERE `plazo`.`CasoId` = " + validareader("CasoId", "CasoId", respuestastring3).Text + ";";
                    
                    String sUpdate_casoproductos = "UPDATE `casoproductos` SET `TipoSolicitudId` = " + getcampo(validareader("TipoSolicitudId", "CasoId", respuestastring3).Text) +
                                     " WHERE `casoproductos`.`CasoId` = " + validareader("CasoId", "CasoId", respuestastring3).Text + ";";

                    String sUpdate_referencia = "UPDATE `referencia` SET `TipoSolicitudId` = " + getcampo(validareader("TipoSolicitudId", "CasoId", respuestastring3).Text) +
                                     " WHERE `referencia`.`CasoId` = " + validareader("CasoId", "CasoId", respuestastring3).Text + ";";

                    String sUpdate_anualidad = "UPDATE `anualidad` SET `TipoSolicitudId` = " + getcampo(validareader("TipoSolicitudId", "CasoId", respuestastring3).Text) +
                                     " WHERE `anualidad`.`CasoId` = " + validareader("CasoId", "CasoId", respuestastring3).Text + ";";

                    String sUpdate_relaciondocumento = "UPDATE `relaciondocumento` SET `TipoSolicitudId` = " + getcampo(validareader("TipoSolicitudId", "CasoId", respuestastring3).Text) +
                                     " WHERE `relaciondocumento`.`CasoId` = " + validareader("CasoId", "CasoId", respuestastring3).Text + ";";
                    //referencia
                    //casoproductos
                    //relaciondocumento


                    stringquerypatentes += sUpdate_relaciondocumento + "\n";
                }
                respuestastring3.Close();
                con.Cerrarconexion();




                ////fijamos dondevamos a crear el archivo 
                StreamWriter escrito = File.CreateText("c:\\facturador\\update.sql"); // en el 
                //para agregar datos al archivo existente 
                //StreamWriter escrito = File.AppendText("c:\\file.txt"); // en el 
                //En una variable tipo string ubicamos el contenido del Textbox 
                //Se podría hacer directamente. 
                String contenido = stringquerypatentes;
                //escribimos. 
                escrito.Write(contenido.ToString());
                escrito.Flush();
                //Cerramos 
                escrito.Close();
                
                //DateTime dt = new DateTime();
                //String fechalog = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                //StringBuilder sb = new StringBuilder();
                //sb.Append(fechalog + ":user:" + form1contruct.sId + ": evento\n");
                //File.AppendAllText("c:\\facturador\\"+"events.log", sb.ToString());
                //sb.Clear();
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

            private void capturaDeEscritosToolStripMenuItem_Click(object sender, EventArgs e)
            {
                //
                bBuscargrupodocs bgroup = new bBuscargrupodocs(this, form1contruct);
                if (bgroup.ShowDialog() == DialogResult.OK)
                {
                    String sgroup = bgroup.comGrop.Value.ToString();
                    CapturaEscritos obj = new CapturaEscritos(this, form1contruct, sgroup, "0");
                    obj.Show();
                }
                else { 
                //No hacemos nada porque cancelamos y regresamos
                }
                //this.Hide();
            }

            private void capturaDeOficiosToolStripMenuItem_Click(object sender, EventArgs e)
            {
                //CapturadeOficios capform = new CapturadeOficios(this, form1contruct);
                //capform.Show();
                //this.Hide();
                bBuscargrupodocs bgroup = new bBuscargrupodocs(this, form1contruct);
                if (bgroup.ShowDialog() == DialogResult.OK)
                {
                    String sgroup = bgroup.comGrop.Value.ToString();
                    CapturadeOficios obj = new CapturadeOficios(this, form1contruct, sgroup, "0", "");
                    obj.Show();
                }
            }

            private void configuraciónDocumentosToolStripMenuItem_Click(object sender, EventArgs e)
            {
                subtipodocumentoestatus sSubtipo = new subtipodocumentoestatus();
                sSubtipo.ShowDialog();
                
            }

            private void capturaDeSolicitudesToolStripMenuItem_Click(object sender, EventArgs e)
            {

                bBuscargrupodocs bgroup = new bBuscargrupodocs(this, form1contruct);
                if (bgroup.ShowDialog() == DialogResult.OK)
                {
                    String sgroup = bgroup.comGrop.Value.ToString();
                    CapturaSolicitud obj = new CapturaSolicitud(this, form1contruct, sgroup, "0");
                    obj.Show();
                }
                else
                {
                    //No hacemos nada porque cancelamos y regresamos
                }
            }

            private void capturaDeTítulosToolStripMenuItem_Click(object sender, EventArgs e)
            {
                bBuscargrupodocs bgroup = new bBuscargrupodocs(this, form1contruct);
                if (bgroup.ShowDialog() == DialogResult.OK)
                {
                    String sgroup = bgroup.comGrop.Value.ToString();
                    Capturadetitulo obj = new Capturadetitulo(this, form1contruct, sgroup, "0");
                    obj.Show();
                }
                else
                {
                    //No hacemos nada porque cancelamos y regresamos
                }
            }

            private void serviciosPorGrupoToolStripMenuItem_Click(object sender, EventArgs e)
            {
                Conceptos form = new Conceptos(this, form1contruct);
                this.Hide();
                form.Show();
            }

            private void reporteQuincenalToolStripMenuItem1_Click(object sender, EventArgs e)
            {
                usuarios usrform = new usuarios();
                usrform.Show();
                ComboBox inputnameusuario = new ComboBox();
            }

            private void tarifasToolStripMenuItem_Click(object sender, EventArgs e)
            {
                tarifas objtaf = new tarifas(form1contruct, this);
                objtaf.Show();
                this.Hide();
            }

            private void buscarFacturaToolStripMenuItem_Click(object sender, EventArgs e)
            {
                //Facturar_CFDI obj_fac = new Facturar_CFDI();
                //obj_fac.ShowDialog();
                
                //Buscard busca = new Buscard(this, form1contruct);
                //busca.Show();
                ////form1contruct.Close();
                //this.Hide();
            }

            private void correosPendientesToolStripMenuItem_Click(object sender, EventArgs e)
            {
                /*

                using (loadpendientes form = new loadpendientes(buscarcorreos))
                {
                    form.ShowDialog();
                }
                corrreospendientes correospendientes = new corrreospendientes(DifferArray);
                correospendientes.Show();*/

            }
            public void buscarcorreos()
            {
                MailBee.Global.LicenseKey = "MN110-8B8932A44B8239779277420FE843-E158";
                Pop3 pop = new Pop3();
                try
                {
                    pop.Connect("mail.dicss.com.mx");
                    pop.Login("eduardor@dicss.com.mx", "Upiicsa1990");
                    Console.WriteLine("Successfully logged in. __mail dicss ");
                }
                catch (MailBeePop3LoginNegativeResponseException ex)
                {
                    Console.WriteLine("POP3 server replied with a negative response at login:" + ex.ToString());
                } Console.WriteLine(pop.InboxMessageCount);
                string[] mensjaesids = pop.GetMessageUids();
                Console.WriteLine(mensjaesids[0]);
                MailMessageCollection msgs = pop.DownloadMessageHeaders();



                DateTime dt = new DateTime();
                String fechalog = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                StringBuilder sb = new StringBuilder();
                sb.Append("\n");
                String textFile = "c:\\facturador\\" + "mails.log";
                string[] lines = null;
                if (File.Exists(textFile))
                {
                    // Read a text file line by line.
                    lines = File.ReadAllLines(textFile);
                }
                DifferArray = mensjaesids.Except(lines).ToArray();

                for (int y = DifferArray.Length - 1; y >= 0; y--)
                {
                    //File.AppendAllText("c:\\facturador\\" + "mails.log", mensjaesids[y]+"\n");
                    Mesageemail = pop.DownloadEntireMessage(pop.GetMessageIndexFromUid(DifferArray[y]));
                    Console.WriteLine("Asunto: " + Mesageemail.Subject);
                    Console.WriteLine("Mensaje: " + Mesageemail.BodyHtmlText);
                    Console.WriteLine(pop.GetMessageIndexFromUid(DifferArray[y]));// GetMessageIndexFromUid();
                }
                sb.Clear();

                pop.Disconnect();
            }

            private void consultarUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
            {

            }

            private void generaciónDeDocumentosToolStripMenuItem_Click(object sender, EventArgs e)
            {
               
            }

            private void agregarNuevoToolStripMenuItem_Click(object sender, EventArgs e)
            {
                //agregardocs obj =new agregardocs();
                //obj.agregardocsgenera();
                Configuradocumento obj = new Configuradocumento(form1contruct);
                obj.Show();
            }

            private void consultarYEditarToolStripMenuItem_Click(object sender, EventArgs e)
            {
                agregardocs nobj = new agregardocs();
                nobj.agregardocsgenera();
            }

            private void procesaImagenesToolStripMenuItem_Click(object sender, EventArgs e)
            {
                //using (loadpendientes form = new loadpendientes(procesaimagenesycarpetas))
                //{
                //    form.ShowDialog();
                //}
            }
            string[] filePaths;
            public void procesaimagenesycarpetas() {
                try { 
                        configuracionfiles obj = new configuracionfiles();
                        obj.configuracionfilesinicio();
                        String sServer = obj.sFileupload;
                        DirectoryInfo dir = new DirectoryInfo(@"C:\Users\eduarximo\Documents\documentosserver\logosactualizado");
                        //DirectoryInfo [] filePaths = dir.GetDirectories();
                        int count = 0;
                        foreach (FileInfo file in dir.GetFiles("*", SearchOption.AllDirectories))
                        {
                            count++;
                            Console.Write(file.FullName);
                            String[] sPath = file.FullName.Split('\\');
                            Directory.CreateDirectory(@"C:\Users\eduarximo\Documents\documentosserver\logosactualizado\" + sPath[6] + sPath[7] + sPath[8]);
                            File.Copy(file.FullName, @"C:\Users\eduarximo\Documents\documentosserver\logosactualizado\" + sPath[6] + sPath[7] + sPath[8] + "\\" + sPath[9]);
                            //if (filePaths.Length > 0)
                            //{
                            //    DirectoryInfo[] filePaths_2 = dir.GetDirectories();
                            //    MessageBox.Show("Mensajes");
                            //}
                        }
                        Console.Write("Count: " + count);                        
                        //foreach (FileInfo file in dir.GetFiles())
                        //{
                        //    filePaths = Directory.GetFiles(file.FullName);
                        //}
                        //Console.Read();
                        //for(int x=0; x <filePaths.Length; x++){
                        //    Console.Write("Carpeta: "+filePaths[x]);
                        //}
                    }catch(Exception E){
                        MessageBox.Show("Mensaje de error:"+E);
                    }

            }
            public String getdirecorio(DirectoryInfo dir)
            {
                String sDirectorio = "";
                foreach (DirectoryInfo file in dir.GetDirectories())
                {
                    file.GetDirectories();
                }

                return "";
            }

            private void altaInteresadoToolStripMenuItem_Click(object sender, EventArgs e)
            {
                Finteresado finteresado = new Finteresado(form1contruct, this);
                finteresado.Show();
                this.Hide();
            }

            private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
            {

            }
            public void procesosdb() {
                actualizapatentes();
                actualizamarcas();
                actualizainteresados();
                actualizadirecciones();
                actualizatabla_documento();
                actualizatabla_relaciondocumento();
                actualizatabla_anualidad();
                actualizatabla_referencia();
                actualizatabla_casoproductos();
                actualizatabla_plazo();
                actualizatabla_prioridad();
                actualizatabla_casocliente();
                actualizatablasrelacionales_casointeresado();
                //Actualizatiposolicitud();//actualizaba el ripo de solicitud pero ya no es necesario porque lo carga en cada tabla
                //Actualizasubtiposolicitud();
                //Actualiza_subtipodocumentoestatuscaso();//estaba cometado por alguna razon
                //Actualiza_estatuscasosubtipodocumento();
                //Actualiza_gruposubtipodocumento();
            }

            private void actualizarDeIpfactsToolStripMenuItem_Click(object sender, EventArgs e)
            {
                using (loadinprocess form = new loadinprocess(procesosdb))
                {
                    form.ShowDialog();
                }
            }

            private void exportartablesToolStripMenuItem_Click(object sender, EventArgs e)
            {
                //string constring = "server=localhost;user=root;pwd=1234;database=test1;";
                //string file = "Y:\\backup.sql";
                //using (MySqlConnection conn = new MySqlConnection(constring))
                //{
                //    using (MySqlCommand cmd = new MySqlCommand())
                //    {
                //        using (MySqlBackup mb = new MySqlBackup(cmd))
                //        {
                //            cmd.Connection = conn;
                //            conn.Open();
                //            mb.ExportInfo.TablesToBeExportedList = new List<string> {
                //                "Table1",
                //                "Table2"
                //            };
                //            mb.ExportToFile(file);
                //        }
                //    }
                //}
            }
            public void actualizatablasrelacionales_casointeresado()
            {
                String sGsInsertcasointeresado = "";
                try
                {

                    //borramos la tabla 
                    conect con_pruebas_tr = new conect();
                    //borramos todas las tablas de relacion
                    MySqlDataReader ejecutaacualizapatentes = con_pruebas_tr.getdatareader("truncate table casointeresado;");
                    ejecutaacualizapatentes.Read();
                    ejecutaacualizapatentes.Close();
                    con_pruebas_tr.Cerrarconexion();
                    //actualizamos la tabla
                    conect_ipfacts con = new conect_ipfacts();
                    //Para updates
                    String stringquerypatentes = "";
                    String sPatentesconsulta = "SELECT casointeresado.*, caso.TipoSolicitudId from casointeresado, caso where casointeresado.CasoId = caso.CasoId;";

                    String squeryinteresados = "";

                    MySqlDataReader respuestastring3 = con.getdatareader(sPatentesconsulta);
                    while (respuestastring3.Read())
                    {
                        String sInsertcasointeresado = " INSERT INTO `casointeresado` " +
                                                        " (`CasoInteresadoId`, " +
                                                        " `InteresadoId`, " +
                                                        " `CasoId`, " +
                                                        " `TipoSolicitudId`, " +
                                                        " `CasoInteresadoSecuencia`, " +
                                                        " `TipoRelacionId`, " +
                                                        " `DireccionId`) " +
                                                        " VALUES " +
                                                        " ( " +
                                                        getcampo(validareader("CasoInteresadoId", "DireccionID", respuestastring3).Text) + ", " +
                                                        getcampo(validareader("InteresadoId", "DireccionID", respuestastring3).Text) + ", " +
                                                        getcampo(validareader("CasoId", "DireccionID", respuestastring3).Text) + ", " +
                                                        getcampo(validareader("TipoSolicitudId", "DireccionID", respuestastring3).Text) + ", " +
                                                        getcampo(validareader("CasoInteresadoSecuencia", "DireccionID", respuestastring3).Text) + ", " +
                                                        getcampo(validareader("TipoRelacionId", "DireccionID", respuestastring3).Text) + ", " +
                                                        getcampo(validareader("DireccionId", "DireccionID", respuestastring3).Text) + ");";
                        sGsInsertcasointeresado = sInsertcasointeresado;
                        conect coninteresado = new conect();
                        MySqlDataReader respuestastrig_interesado = coninteresado.getdatareader(sInsertcasointeresado);
                        respuestastrig_interesado.Close();
                        coninteresado.Cerrarconexion();
                    }
                    respuestastring3.Close();
                    con.Cerrarconexion();
                }
                catch (Exception E)
                {
                    new filelog(form1contruct.sId, E.ToString() + " query: " + sGsInsertcasointeresado);
                }
            }

            public void actualizatabla_casocliente()
            {
                String sGsInsertcasointeresado = "";
                try
                {
                    //borramos la tabla 
                    conect con_pruebas_tr = new conect();
                    //borramos todas las tablas de relacion
                    MySqlDataReader ejecutaacualizapatentes = con_pruebas_tr.getdatareader("truncate table casocliente;");
                    ejecutaacualizapatentes.Read();
                    ejecutaacualizapatentes.Close();
                    con_pruebas_tr.Cerrarconexion();
                    //actualizamos la tabla
                    conect_ipfacts con = new conect_ipfacts();
                    //Para updates
                    String stringquerypatentes = "";
                    String sPatentesconsulta = "SELECT casocliente.*, caso.TipoSolicitudId from casocliente, caso where casocliente.CasoId = caso.CasoId;";

                    String squeryinteresados = "";

                    MySqlDataReader respuestastring3 = con.getdatareader(sPatentesconsulta);
                    while (respuestastring3.Read())
                    {
                        String sInsertcasocliente = " INSERT INTO `casocliente` " +
                                                        " (`CasoClienteId`, " +
                                                        " `ClienteId`, " +
                                                        " `contactoid`, " +
                                                        " `CasoId`, " +
                                                        " `TipoSolicitudId`, " +
                                                        " `CasoClienteIndCorres`) " +
                                                        " VALUES " +
                                                        " ( " + getcampo(validareader("CasoClienteId", "CasoClienteId", respuestastring3).Text) + ", " +
                                                        getcampo(validareader("ClienteId", "CasoClienteId", respuestastring3).Text) + ", " +
                                                        getcampo(validareader("contactoid", "CasoClienteId", respuestastring3).Text) + ", " +
                                                        getcampo(validareader("CasoId", "CasoClienteId", respuestastring3).Text) + ", " +
                                                        getcampo(validareader("TipoSolicitudId", "CasoClienteId", respuestastring3).Text) + ", " +
                                                        getcampo(validareader("CasoClienteIndCorres", "CasoClienteId", respuestastring3).Text)+" );";
                        sGsInsertcasointeresado = sInsertcasocliente;
                        conect coninteresado = new conect();
                        MySqlDataReader respuestastrig_interesado = coninteresado.getdatareader(sInsertcasocliente);
                        respuestastrig_interesado.Close();
                        coninteresado.Cerrarconexion();
                    }
                    respuestastring3.Close();
                    con.Cerrarconexion();
                }
                catch (Exception E)
                {
                    new filelog(form1contruct.sId, E.ToString() + " query:" + sGsInsertcasointeresado);
                }
            }


            public void actualizatabla_cliente()
            {
                String sGsInsertcasointeresado = "";
                try
                {
                    //borramos la tabla 
                    conect con_pruebas_tr = new conect();
                    //borramos todas las tablas de relacion
                    MySqlDataReader ejecutaacualizapatentes = con_pruebas_tr.getdatareader("truncate table cliente;");
                    ejecutaacualizapatentes.Read();
                    ejecutaacualizapatentes.Close();
                    con_pruebas_tr.Cerrarconexion();
                    //actualizamos la tabla
                    conect_ipfacts con = new conect_ipfacts();
                    //Para updates
                    String stringquerypatentes = "";
                    String sPatentesconsulta = "SELECT * from cliente";

                    String squeryinteresados = "";

                    MySqlDataReader respuestastring3 = con.getdatareader(sPatentesconsulta);
                    while (respuestastring3.Read())
                    {
                        //String sInsertcasocliente = " INSERT INTO `casocliente` " +
                        //                                " (`CasoClienteId`, " +
                        //                                " `ClienteId`, " +
                        //                                " `contactoid`, " +
                        //                                " `CasoId`, " +
                        //                                " `TipoSolicitudId`, " +
                        //                                " `CasoClienteIndCorres`) " +
                        //                                " VALUES " +
                        //                                " ( " + getcampo(validareader("CasoClienteId", "CasoClienteId", respuestastring3).Text) + ", " +
                        //                                getcampo(validareader("ClienteId", "CasoClienteId", respuestastring3).Text) + ", " +
                        //                                getcampo(validareader("contactoid", "CasoClienteId", respuestastring3).Text) + ", " +
                        //                                getcampo(validareader("CasoId", "CasoClienteId", respuestastring3).Text) + ", " +
                        //                                getcampo(validareader("TipoSolicitudId", "CasoClienteId", respuestastring3).Text) + ", " +
                        //                                getcampo(validareader("CasoClienteIndCorres", "CasoClienteId", respuestastring3).Text)+" );";

                        String sInsertcasocliente = " INSERT INTO `cliente` " +
                                                        " (`ClienteId`, " +
                                                        " `ClienteClave`, " +
                                                        " `ClienteNombre`, " +
                                                        " `ClienteTipoPersonaSAT`, " +
                                                        " `ClienteRFC`, " +
                                                        " `ClienteCurp`, " +
                                                        " `ClienteVAT`, " +
                                                        " `IdiomaId`, " +
                                                        " `TipoComunicacionId`, " +
                                                        " `TipoClienteId`, " +
                                                        " `MonedaId`, " +
                                                        " `ClienteWebSite`, " +
                                                        " `ClienteEmail`, " +
                                                        " `ProveedorFacElecId`, " +
                                                        " `TipoEnvioFacId`, " +
                                                        " `ClienteObservacion`, " +
                                                        " `ClienteRecMarca`, " +
                                                        " `ClienteRecPatente`, " +
                                                        " `ClienteFechaAlta`, " +
                                                        " `ResponsableId`, " +
                                                        " `TipoTarifaId`, " +
                                                        " `HolderId`) " +
                                                        " VALUES " +
                                                        "( " +
                                                        getcampo(validareader("ClienteId", "ClienteId", respuestastring3).Text) + " , " +
                                                        getcampo(validareader("ClienteClave", "ClienteClave", respuestastring3).Text) + " , " +
                                                        getcampo(validareader("ClienteNombre", "ClienteNombre", respuestastring3).Text) + " , " +
                                                        getcampo(validareader("ClienteTipoPersonaSAT", "ClienteTipoPersonaSAT", respuestastring3).Text) + " , " +
                                                        getcampo(validareader("ClienteRFC", "ClienteRFC", respuestastring3).Text) + " , " +
                                                        getcampo(validareader("ClienteCurp", "ClienteCurp", respuestastring3).Text) + " , " +
                                                        getcampo(validareader("ClienteVAT", "ClienteVAT", respuestastring3).Text) + " , " +
                                                        getcampo(validareader("IdiomaId", "IdiomaId", respuestastring3).Text) + " , " +
                                                        getcampo(validareader("TipoComunicacionId", "TipoComunicacionId", respuestastring3).Text) + " , " +
                                                        getcampo(validareader("TipoClienteId", "TipoClienteId", respuestastring3).Text) + " , " +
                                                        getcampo(validareader("MonedaId", "MonedaId", respuestastring3).Text) + " , " +
                                                        getcampo(validareader("ClienteWebSite", "ClienteWebSite", respuestastring3).Text) + " , " +
                                                        getcampo(validareader("ClienteEmail", "ClienteEmail", respuestastring3).Text) + " , " +
                                                        getcampo(validareader("ProveedorFacElecId", "ProveedorFacElecId", respuestastring3).Text) + " , " +
                                                        getcampo(validareader("TipoEnvioFacId", "TipoEnvioFacId", respuestastring3).Text) + " , " +
                                                        getcampo(validareader("ClienteObservacion", "ClienteObservacion", respuestastring3).Text) + " , " +
                                                        getcampo(validareader("ClienteRecMarca", "ClienteRecMarca", respuestastring3).Text) + " , " +
                                                        getcampo(validareader("ClienteRecPatente", "ClienteRecPatente", respuestastring3).Text) + " , " +
                                                        getcampo(validareader("ClienteFechaAlta", "ClienteFechaAlta", respuestastring3).Text) + " , " +
                                                        getcampo(validareader("ResponsableId", "ResponsableId", respuestastring3).Text) + " , " +
                                                        getcampo(validareader("TipoTarifaId", "TipoTarifaId", respuestastring3).Text) + " , " +
                                                        getcampo(validareader("HolderId", "HolderId", respuestastring3).Text) + ");";
                        sGsInsertcasointeresado += sInsertcasocliente;
                        
                    }

                    conect coninteresado = new conect();
                    MySqlDataReader respuestastrig_interesado = coninteresado.getdatareader(sGsInsertcasointeresado);
                    respuestastrig_interesado.Close();
                    coninteresado.Cerrarconexion();

                    respuestastring3.Close();
                    con.Cerrarconexion();
                }catch (Exception E){
                    new filelog(form1contruct.sId, E.ToString() + " query:" + sGsInsertcasointeresado);
                }
            }

            public void actualizatabla_contacto()
            {
                String sGsInsertcasointeresado = "";
                try
                {
                    //borramos la tabla 
                    conect con_pruebas_tr = new conect();
                    //borramos todas las tablas de relacion
                    MySqlDataReader ejecutaacualizapatentes = con_pruebas_tr.getdatareader("truncate table contacto;");
                    ejecutaacualizapatentes.Read();
                    ejecutaacualizapatentes.Close();
                    con_pruebas_tr.Cerrarconexion();
                    //actualizamos la tabla
                    conect_ipfacts con = new conect_ipfacts();
                    //Para updates
                    String stringquerypatentes = "";
                    String sPatentesconsulta = "select * from contacto";

                    String squeryinteresados = "";

                    MySqlDataReader respuestastring3 = con.getdatareader(sPatentesconsulta);
                    while (respuestastring3.Read())
                    {

                        String sInsertcasocliente = " INSERT INTO `contacto` " +
                                                    " (`ContactoId`, " +
                                                    " `ClienteId`, " +
                                                    " `ContactoNick`, " +
                                                    " `ContactoNombre`, " +
                                                    " `ContactoTelefono`, " +
                                                    " `ContactoEmail`, " +
                                                    " `ContactoSexo`, " +
                                                    " `ContactoArea`, " +
                                                    " `ContactoPuesto`, " +
                                                    " `ContactoIndAct`, " +
                                                    " `InteresadoId`, " +
                                                    " `GrupoId`) " +
                                                    " VALUES " +
                                                    " ( " +
                                                    getcampo(validareader("ContactoId", "ContactoId", respuestastring3).Text) + " , " +
                                                    getcampo(validareader("ClienteId", "ClienteId", respuestastring3).Text) + " , " +
                                                    getcampo(validareader("ContactoNick", "ContactoNick", respuestastring3).Text) + " , " +
                                                    getcampo(validareader("ContactoNombre", "ContactoNombre", respuestastring3).Text) + " , " +
                                                    getcampo(validareader("ContactoTelefono", "ContactoTelefono", respuestastring3).Text) + " , " +
                                                    getcampo(validareader("ContactoEmail", "ContactoEmail", respuestastring3).Text) + " , " +
                                                    getcampo(validareader("ContactoSexo", "ContactoSexo", respuestastring3).Text) + " , " +
                                                    getcampo(validareader("ContactoArea", "ContactoArea", respuestastring3).Text) + " , " +
                                                    getcampo(validareader("ContactoPuesto", "ContactoPuesto", respuestastring3).Text) + " , " +
                                                    getcampo(validareader("ContactoIndAct", "ContactoIndAct", respuestastring3).Text) + " , " +
                                                    getcampo(validareader("InteresadoId", "InteresadoId", respuestastring3).Text) + " , " +
                                                    getcampo(validareader("GrupoId", "GrupoId", respuestastring3).Text) + ");";
                        sGsInsertcasointeresado += sInsertcasocliente;

                    }

                    conect coninteresado = new conect();
                    MySqlDataReader respuestastrig_interesado = coninteresado.getdatareader(sGsInsertcasointeresado);
                    respuestastrig_interesado.Close();
                    coninteresado.Cerrarconexion();

                    respuestastring3.Close();
                    con.Cerrarconexion();
                }
                catch (Exception E)
                {
                    new filelog(form1contruct.sId, E.ToString() + " query:" + sGsInsertcasointeresado);
                }
            }


            public void actualizatabla_prioridad()
            {
                String sGsInsertcasocliente = "";
                try
                {
                    //borramos la tabla 
                    conect con_pruebas_tr = new conect();
                    //borramos todas las tablas de relacion
                    MySqlDataReader ejecutaacualizapatentes = con_pruebas_tr.getdatareader("truncate table prioridad;");
                    ejecutaacualizapatentes.Read();
                    ejecutaacualizapatentes.Close();
                    con_pruebas_tr.Cerrarconexion();
                    //actualizamos la tabla
                    conect_ipfacts con = new conect_ipfacts();
                    //Para updates
                    String stringquerypatentes = "";
                    String sPatentesconsulta = "SELECT `PrioridadId`, " +
                                                "prioridad.`CasoId`, " +
                                                "prioridad.`PrioridadNumero`, " +
                                                "prioridad.`PaisID`, " +
                                                "DATE_FORMAT(PrioridadFecha , '%Y-%m-%d') as  `PrioridadFecha`, " +
                                                "`TipoPrioridadId`," +
                                                "caso.TipoSolicitudId" +
                                                " FROM `prioridad`, `caso` where `prioridad`.`CasoId` = `caso`.`CasoId`;";

                    String squeryinteresados = "";

                    MySqlDataReader respuestastring3 = con.getdatareader(sPatentesconsulta);
                    while (respuestastring3.Read())
                    {
                        String sInsertcasocliente = " INSERT INTO `prioridad` "+
                                                    " (`PrioridadId`, "+
                                                    " `CasoId`, " +
                                                    " `TipoSolicitudId`, " +
                                                    " `PrioridadNumero`, "+
                                                    " `PaisID`, "+
                                                    " `PrioridadFecha`, "+
                                                    " `TipoPrioridadId`) "+
                                                    " VALUES "+
                                                    " ("+
                                                    getcampo(validareader("PrioridadId", "PrioridadId", respuestastring3).Text)+", "+
                                                    getcampo(validareader("CasoId", "PrioridadId", respuestastring3).Text)+", "+
                                                    getcampo(validareader("TipoSolicitudId", "PrioridadId", respuestastring3).Text) + ", " +
                                                    getcampo(validareader("PrioridadNumero", "PrioridadId", respuestastring3).Text)+", "+
                                                    getcampo(validareader("PaisID", "PrioridadId", respuestastring3).Text)+", "+
                                                    getcampo(validareader("PrioridadFecha", "PrioridadId", respuestastring3).Text)+", "+
                                                    getcampo(validareader("TipoPrioridadId", "PrioridadId", respuestastring3).Text)+ "); ";
                        conect coninteresado = new conect();
                        MySqlDataReader respuestastrig_interesado = coninteresado.getdatareader(sInsertcasocliente);
                        sGsInsertcasocliente = sInsertcasocliente;
                        respuestastrig_interesado.Close();
                        coninteresado.Cerrarconexion();
                    }

                    respuestastring3.Close();
                    con.Cerrarconexion();
                }
                catch (Exception E)
                {
                    new filelog(form1contruct.sId, E.ToString() + " query error:" + sGsInsertcasocliente);
                }
            }

            public void actualizatabla_plazo()
            {
                String query_errror = "";
                try
                {
                    //borramos la tabla 
                    conect con_pruebas_tr = new conect();
                    //borramos todas las tablas de relacion
                    MySqlDataReader ejecutaacualizapatentes = con_pruebas_tr.getdatareader("truncate table plazos;");
                    ejecutaacualizapatentes.Read();
                    ejecutaacualizapatentes.Close();
                    con_pruebas_tr.Cerrarconexion();


                    /**/
                    conect con_pruebas_trplazos = new conect();
                    //borramos todas las tablas de relacion
                    MySqlDataReader ejecutaacualiza = con_pruebas_trplazos.getdatareader("truncate table plazos_detalle;");
                    ejecutaacualiza.Read();
                    ejecutaacualiza.Close();
                    con_pruebas_trplazos.Cerrarconexion();
                    /**/

                    //actualizamos la tabla
                    conect_ipfacts con = new conect_ipfacts();
                    //Para updates
                    String stringquerypatentes = "";
                    String sPatentesconsulta = " SELECT `PlazoId`, " +
                                                "     `TipoPlazoId`, " +
                                                "     plazo.`CasoId`, " +
                                                "     plazo.`DocumentoId`, " +
                                                "     plazo.`AnualidadId`, " +
                                                "     plazo.`ClienteId`, " +
                                                "     plazo.`PlazoMotivoCancelacion`, " +
                                                "     DATE_FORMAT(plazo.PlazoFecha , '%Y-%m-%d') as   `PlazoFecha`, " +
                                                "     DATE_FORMAT(plazo.PlazoFechaProrroga , '%Y-%m-%d') as   `PlazoFechaProrroga`, " +
                                                "     plazo.`UsuarioId`, " +
                                                "     DATE_FORMAT(plazo.PlazoFechaAtencion , '%Y-%m-%d') as    `PlazoFechaAtencion`, " +
                                                "     plazo.`EstatusPlazoId`, " +
                                                "     plazo.`UsuarioIdCancelo`, " +
                                                "     plazo.`PlazoDescripcion`, " +
                                                "     plazo.`PlazoIdRef`, " +
                                                "     plazo.`usuarioIdAtendio`," +
                                                "     caso.TipoSolicitudId" +
                                                " FROM `plazo`, caso" +
                                                " where caso.casoid = plazo.casoid;";
                    MySqlDataReader respuestastring3 = con.getdatareader(sPatentesconsulta);
                    while (respuestastring3.Read())
                    {
                        //String sInsertcasocliente = " INSERT INTO `plazo` " +
                        //                            " (`PlazoId`, " +
                        //                            " `TipoPlazoId`, " +
                        //                            " `CasoId`, " +
                        //                            " `DocumentoId`, " +
                        //                            " `AnualidadId`, " +
                        //                            " `ClienteId`, " +
                        //                            " `PlazoMotivoCancelacion`, " +
                        //                            " `PlazoFecha`, " +
                        //                            " `PlazoFechaProrroga`, " +
                        //                            " `UsuarioId`, " +
                        //                            " `PlazoFechaAtencion`, " +
                        //                            " `EstatusPlazoId`, " +
                        //                            " `UsuarioIdCancelo`, " +
                        //                            " `PlazoDescripcion`, " +
                        //                            " `PlazoIdRef`, " +
                        //                            " `usuarioIdAtendio`) " +
                        //                            " VALUES " +
                        //                            " ( " +
                        //                            getcampo(validareader("PlazoId", "PlazoId", respuestastring3).Text) + ", " +
                        //                            getcampo(validareader("TipoPlazoId", "PlazoId", respuestastring3).Text) + ", " +
                        //                            getcampo(validareader("CasoId", "PlazoId", respuestastring3).Text) + ", " +
                        //                            getcampo(validareader("DocumentoId", "PlazoId", respuestastring3).Text) + ", " +
                        //                            getcampo(validareader("AnualidadId", "PlazoId", respuestastring3).Text) + ", " +
                        //                            getcampo(validareader("ClienteId", "PlazoId", respuestastring3).Text) + ", " +
                        //                            getcampo(validareader("PlazoMotivoCancelacion", "PlazoId", respuestastring3).Text) + ", " +
                        //                            getcampo(validareader("PlazoFecha", "PlazoId", respuestastring3).Text) + ", " +
                        //                            getcampo(validareader("PlazoFechaProrroga", "PlazoId", respuestastring3).Text) + ", " +
                        //                            getcampo(validareader("UsuarioId", "PlazoId", respuestastring3).Text) + ", " +
                        //                            getcampo(validareader("PlazoFechaAtencion", "PlazoId", respuestastring3).Text) + ", " +
                        //                            getcampo(validareader("EstatusPlazoId", "PlazoId", respuestastring3).Text) + ", " +
                        //                            getcampo(validareader("UsuarioIdCancelo", "PlazoId", respuestastring3).Text) + ", " +
                        //                            getcampo(validareader("PlazoDescripcion", "PlazoId", respuestastring3).Text) + ", " +
                        //                            getcampo(validareader("PlazoIdRef", "PlazoId", respuestastring3).Text) + ", " +
                        //                            getcampo(validareader("usuarioIdAtendio", "PlazoId", respuestastring3).Text) + "); ";
                        //conect coninteresado = new conect();
                        //MySqlDataReader respuestastrig_interesado = coninteresado.getdatareader(sInsertcasocliente);
                        //query_errror = sInsertcasocliente;
                        //respuestastrig_interesado.Close();
                        //coninteresado.Cerrarconexion();

                        /*Creamos los registros de plazos*/
                        String sInsertcasocliente = " INSERT INTO `plazos` " +
                                                    " (`Plazosid`, " +
                                                    " `TipoSolicitudId`, " +
                                                    " `CasoId`) " +
                                                    " VALUES " +
                                                    " ( " +
                                                    getcampo(validareader("PlazoId", "PlazoId", respuestastring3).Text) + ", " +
                                                    getcampo(validareader("TipoSolicitudId", "TipoSolicitudId", respuestastring3).Text) + ", " +
                                                    getcampo(validareader("CasoId", "CasoId", respuestastring3).Text) + "); ";
                        conect coninteresado = new conect();
                        MySqlDataReader respuestastrig_interesado = coninteresado.getdatareader(sInsertcasocliente);
                        query_errror = sInsertcasocliente;
                        respuestastrig_interesado.Close();
                        coninteresado.Cerrarconexion();

                        /*Creamos los registros de plazos_detalle*/
                        String sinsertaplazosdetalle = " INSERT INTO `plazos_detalle` " +
                                                    " (`Plazos_detalleid`, " +
                                                    " `Plazosid`, " +
                                                    " `Tipo_plazoid`, " +
                                                    " `documentoid`, " +
                                                    " `Motivo_cancelacion_plazo`, " +
                                                    " `Fecha_Vencimiento`, " +
                                                    " `Fecha_vencimiento_3m`, " +
                                                    " `usuario_creo_plazodetalle`, " +
                                                    " `Fecha_atendio_plazo`, " +
                                                    " `Estatus_plazoid`, " +
                                                    " `Usuarioid_atendio_plazo`, " +
                                                    " `usuario_cancelo`) " +
                                                    " VALUES " +
                                                    " ( " +
                                                        getcampo(validareader("PlazoId", "PlazoId", respuestastring3).Text) + ", " +
                                                        getcampo(validareader("PlazoId", "PlazoId", respuestastring3).Text) + ", " +
                                                        getcampo(validareader("TipoPlazoId", "PlazoId", respuestastring3).Text) + ", " +
                                                        getcampo(validareader("DocumentoId", "PlazoId", respuestastring3).Text) + ", " +
                                                        getcampo(validareader("PlazoMotivoCancelacion", "PlazoId", respuestastring3).Text) + ", " +
                                                        getcampo(validareader("PlazoFecha", "PlazoId", respuestastring3).Text) + ", " +
                                                        getcampo(validareader("PlazoFechaProrroga", "PlazoId", respuestastring3).Text) + ", " +
                                                        getcampo(validareader("UsuarioId", "PlazoId", respuestastring3).Text) + ", " +
                                                        getcampo(validareader("PlazoFechaAtencion", "PlazoId", respuestastring3).Text) + ", " +
                                                        getcampo(validareader("EstatusPlazoId", "PlazoId", respuestastring3).Text) + ", " +
                                                        getcampo(validareader("UsuarioIdCancelo", "PlazoId", respuestastring3).Text) + ", " +
                                                        getcampo(validareader("usuarioIdAtendio", "PlazoId", respuestastring3).Text) + "); ";
                        conect con_plazos = new conect();
                        MySqlDataReader respuesta_plazos = con_plazos.getdatareader(sinsertaplazosdetalle);
                        query_errror = sInsertcasocliente;
                        respuesta_plazos.Close();
                        con_plazos.Cerrarconexion();
                    }
                    respuestastring3.Close();
                    con.Cerrarconexion();
                }
                catch (Exception E)
                {
                    new filelog(form1contruct.sId, E.ToString());
                }
            }

            public void actualizatabla_casoproductos()
            {
                try
                {
                    //borramos la tabla 
                    conect con_pruebas_tr = new conect();
                    //borramos todas las tablas de relacion
                    MySqlDataReader ejecutaacualizapatentes = con_pruebas_tr.getdatareader("truncate table casoproductos;");
                    ejecutaacualizapatentes.Read();
                    ejecutaacualizapatentes.Close();
                    con_pruebas_tr.Cerrarconexion();
                    //actualizamos la tabla
                    conect_ipfacts con = new conect_ipfacts();
                    //Para updates
                    String stringquerypatentes = "";
                    String sPatentesconsulta = "SELECT casoproductos.*, caso.TipoSolicitudId from casoproductos, `caso` where `casoproductos`.`CasoId` = `caso`.`CasoId`;";
                    MySqlDataReader respuestastring3 = con.getdatareader(sPatentesconsulta);
                    while (respuestastring3.Read())
                    {
                        String sInsertcasoproductos = " INSERT INTO `casoproductos` " +
                                                    " (`CasoProductosId`, " +
                                                    " `CasoProductosDescripcion`, " +
                                                    " `CasoProductosClase`, " +
                                                    " `ClasificadorNizaId`, " +
                                                    " `CasoId`, " +
                                                    " `TipoSolicitudId`, " +
                                                    " `PaisId`) " +
                                                    " VALUES " +
                                                    " ( " +
                                                    getcampo(validareader("CasoProductosId", "CasoProductosId", respuestastring3).Text) + ", " +
                                                    getcampo(validareader("CasoProductosDescripcion", "CasoProductosId", respuestastring3).Text) + ", " +
                                                    getcampo(validareader("CasoProductosClase", "CasoProductosId", respuestastring3).Text) + ", " +
                                                    getcampo(validareader("ClasificadorNizaId", "CasoProductosId", respuestastring3).Text) + ", " +
                                                    getcampo(validareader("CasoId", "CasoProductosId", respuestastring3).Text) + ", " +
                                                    getcampo(validareader("TipoSolicitudId", "CasoProductosId", respuestastring3).Text) + ", " +
                                                    getcampo(validareader("PaisId", "CasoProductosId", respuestastring3).Text) + "); ";
                        conect coninteresado = new conect();
                        MySqlDataReader respuestastrig_interesado = coninteresado.getdatareader(sInsertcasoproductos);
                        respuestastrig_interesado.Close();
                        coninteresado.Cerrarconexion();
                    }
                    respuestastring3.Close();
                    con.Cerrarconexion();
                }
                catch (Exception E)
                {
                    new filelog(form1contruct.sId, E.ToString());
                }
            }

            public void actualizatabla_referencia()
            {
                try
                {
                    //borramos la tabla 
                    conect con_pruebas_tr = new conect();
                    //borramos todas las tablas de relacion
                    MySqlDataReader ejecutaacualizapatentes = con_pruebas_tr.getdatareader("truncate table referencia;");
                    ejecutaacualizapatentes.Read();
                    ejecutaacualizapatentes.Close();
                    con_pruebas_tr.Cerrarconexion();
                    //actualizamos la tabla
                    conect_ipfacts con = new conect_ipfacts();
                    //Para updates
                    String stringquerypatentes = "";
                    String sPatentesconsulta = " select referencia.*, caso.TipoSolicitudId from referencia , `caso` where `referencia`.`CasoId` = `caso`.`CasoId`;";
                    MySqlDataReader respuestastring3 = con.getdatareader(sPatentesconsulta);
                    while (respuestastring3.Read())
                    {
                        String sInsert_referencia = "INSERT INTO `referencia` " +
                                                    "(`ReferenciaId`, " +
                                                    "`CasoId`, " +
                                                    "`TipoSolicitudId`, " +
                                                    "`TipoReferenciaId`, " +
                                                    "`ReferenciaNombre`) " +
                                                    "VALUES " +
                                                    "( " +
                                                    getcampo(validareader("ReferenciaId", "ReferenciaId", respuestastring3).Text) + ", " +
                                                    getcampo(validareader("CasoId", "ReferenciaId", respuestastring3).Text) + ", " +
                                                    getcampo(validareader("TipoSolicitudId", "ReferenciaId", respuestastring3).Text) + ", " +
                                                    getcampo(validareader("TipoReferenciaId", "ReferenciaId", respuestastring3).Text) + ", " +
                                                    getcampo(validareader("ReferenciaNombre", "ReferenciaId", respuestastring3).Text) + "); ";
                        conect coninteresado = new conect();
                        MySqlDataReader respuestastrig_interesado = coninteresado.getdatareader(sInsert_referencia);
                        respuestastrig_interesado.Close();
                        coninteresado.Cerrarconexion();
                    }
                    respuestastring3.Close();
                    con.Cerrarconexion();
                }
                catch (Exception E)
                {
                    new filelog(form1contruct.sId, E.ToString());
                }
            }


            public void actualizatabla_anualidad()
            {
                String sInsert_anualidadg = "";
                try
                {
                    //borramos la tabla 
                    conect con_pruebas_tr = new conect();
                    //borramos todas las tablas de relacion
                    MySqlDataReader ejecutaacualizapatentes = con_pruebas_tr.getdatareader("truncate table anualidad;");
                    ejecutaacualizapatentes.Read();
                    ejecutaacualizapatentes.Close();
                    con_pruebas_tr.Cerrarconexion();
                    //actualizamos la tabla
                    conect_ipfacts con = new conect_ipfacts();
                    //Para updates
                    String stringquerypatentes = "";
                    String sPatentesconsulta = " SELECT  " +
                                                 " `anualidad`.`AnualidadId`, " +
                                                 " `anualidad`.`AnualidadSecuencia`, " +
                                                 " `anualidad`.`AnualidadIndExe`, " +
                                                 " `anualidad`.`AnualidadAno`, " +
                                                 " `anualidad`.`AnualidadMes`, " +
                                                 " `anualidad`.`AnualidadQuinquenio`, " +
                                                 " `anualidad`.`EstatusAnualidadId`, " +
                                                 " `anualidad`.`CasoId`, " +
                                                 " `caso`.`TipoSolicitudId`, " +
                                                " DATE_FORMAT(AnualidadFechaPago , '%Y-%m-%d') as   `AnualidadFechaPago`, " +
                                                " DATE_FORMAT(AnualidadFechaLimitePago , '%Y-%m-%d') as   `AnualidadFechaLimitePago`, " +
                                                " DATE_FORMAT(AnualidadFechaFinVigencia , '%Y-%m-%d') as  `AnualidadFechaFinVigencia`, " +
                                                 " `anualidad`.`AnualidadTipo` " +
                                                 " FROM `anualidad`, `caso` where `anualidad`.`CasoId` = `caso`.`CasoId`;";
                    MySqlDataReader respuestastring3 = con.getdatareader(sPatentesconsulta);
                    
                    while (respuestastring3.Read())
                    {
                        String sInsert_anualidad = " INSERT INTO `anualidad` " +
                                                    " (`AnualidadId`, " +
                                                    " `AnualidadSecuencia`, " +
                                                    " `AnualidadIndExe`, " +
                                                    " `AnualidadAno`, " +
                                                    " `AnualidadMes`, " +
                                                    " `AnualidadQuinquenio`, " +
                                                    " `EstatusAnualidadId`, " +
                                                    " `CasoId`, " +
                                                    " `TipoSolicitudId`, " +
                                                    " `AnualidadFechaPago`, " +
                                                    " `AnualidadFechaLimitePago`, " +
                                                    " `AnualidadFechaFinVigencia`, " +
                                                    " `AnualidadTipo`) " +
                                                    " VALUES " +
                                                    " ( " +
                                                    getcampo(validareader("AnualidadId", "AnualidadId", respuestastring3).Text) + ", " +
                                                    getcampo(validareader("AnualidadSecuencia", "AnualidadId", respuestastring3).Text) + ", " +
                                                    getcampo(validareader("AnualidadIndExe", "AnualidadId", respuestastring3).Text) + ", " +
                                                    getcampo(validareader("AnualidadAno", "AnualidadId", respuestastring3).Text) + ", " +
                                                    getcampo(validareader("AnualidadMes", "AnualidadId", respuestastring3).Text) + ", " +
                                                    getcampo(validareader("AnualidadQuinquenio", "AnualidadId", respuestastring3).Text) + ", " +
                                                    getcampo(validareader("EstatusAnualidadId", "AnualidadId", respuestastring3).Text) + ", " +
                                                    getcampo(validareader("CasoId", "AnualidadId", respuestastring3).Text) + ", " +
                                                    getcampo(validareader("TipoSolicitudId", "AnualidadId", respuestastring3).Text) + ", " +
                                                    getcampo(validareader("AnualidadFechaPago", "AnualidadId", respuestastring3).Text) + ", " +
                                                    getcampo(validareader("AnualidadFechaLimitePago", "AnualidadId", respuestastring3).Text) + ", " +
                                                    getcampo(validareader("AnualidadFechaFinVigencia", "AnualidadId", respuestastring3).Text) + ", " +
                                                    getcampo(validareader("AnualidadTipo", "AnualidadId", respuestastring3).Text) + "); ";
                        conect coninteresado = new conect();
                        MySqlDataReader respuestastrig_interesado = coninteresado.getdatareader(sInsert_anualidad);
                        sInsert_anualidadg = sInsert_anualidad;
                        respuestastrig_interesado.Close();
                        coninteresado.Cerrarconexion();
                    }
                    respuestastring3.Close();
                    con.Cerrarconexion();
                }
                catch (Exception E)
                {
                    new filelog(form1contruct.sId, E.ToString() + " query: " + sInsert_anualidadg);
                }
            }

            public void actualizatabla_relaciondocumento()
            {
                try
                {
                    //borramos la tabla 
                    conect con_pruebas_tr = new conect();
                    //borramos todas las tablas de relacion
                    MySqlDataReader ejecutaacualizapatentes = con_pruebas_tr.getdatareader("truncate table relaciondocumento;");
                    ejecutaacualizapatentes.Read();
                    ejecutaacualizapatentes.Close();
                    con_pruebas_tr.Cerrarconexion();
                    //actualizamos la tabla
                    conect_ipfacts con = new conect_ipfacts();
                    //Para updates
                    String stringquerypatentes = "";
                    String sPatentesconsulta = "SELECT relaciondocumento.*, caso.TipoSolicitudId from relaciondocumento, caso where relaciondocumento.CasoId = caso.CasoId;";
                    MySqlDataReader respuestastring3 = con.getdatareader(sPatentesconsulta);
                    while (respuestastring3.Read())
                    {
                        String sInsert_relaciondocumento = " INSERT INTO `relaciondocumento` " +
                                                            " (`RelacionDocumentoId`, " +
                                                            " `DocumentoId`, " +
                                                            " `CasoId`, " +
                                                            " `TipoSolicitudId`, " +
                                                            " `ClienteId`, " +
                                                            " `RelacionDocumentoLink`) " +
                                                            " VALUES " +
                                                            " ( " +
                                                            getcampo(validareader("RelacionDocumentoId", "RelacionDocumentoId", respuestastring3).Text) + ", " +
                                                            getcampo(validareader("DocumentoId", "RelacionDocumentoId", respuestastring3).Text) + ", " +
                                                            getcampo(validareader("CasoId", "RelacionDocumentoId", respuestastring3).Text) + ", " +
                                                            getcampo(validareader("TipoSolicitudId", "RelacionDocumentoId", respuestastring3).Text) + ", " +
                                                            getcampo(validareader("ClienteId", "RelacionDocumentoId", respuestastring3).Text) + ", " +
                                                            getcampo(validareader("RelacionDocumentoLink", "RelacionDocumentoId", respuestastring3).Text.Replace("\\", "\\\\")) + "); ";
                        conect coninteresado = new conect();
                        MySqlDataReader respuestastrig_interesado = coninteresado.getdatareader(sInsert_relaciondocumento);
                        respuestastrig_interesado.Close();
                        coninteresado.Cerrarconexion();
                    }
                    respuestastring3.Close();
                    con.Cerrarconexion();
                }
                catch (Exception E)
                {
                    new filelog(form1contruct.sId, E.ToString());
                }
            }

            public void actualizatabla_documento()
            {
                try
                {
                    //borramos la tabla 
                    conect con_pruebas_tr = new conect();
                    //borramos todas las tablas de relacion
                    MySqlDataReader ejecutaacualizapatentes = con_pruebas_tr.getdatareader("truncate table documento;");
                    ejecutaacualizapatentes.Read();
                    ejecutaacualizapatentes.Close();
                    con_pruebas_tr.Cerrarconexion();
                    //actualizamos la tabla
                    conect_ipfacts con = new conect_ipfacts();
                    //Para updates
                    String stringquerypatentes = "";
                    String sPatentesconsulta = " SELECT `DocumentoId`, " +
                                                " `DocumentoCodigoBarras`, " +
                                                " `SubTipoDocumentoId`, " +
                                                " DATE_FORMAT(DocumentoFecha , '%Y-%m-%d') as  `DocumentoFecha`, " +
                                                " `DocumentoFolio`, " +
                                                " DATE_FORMAT(DocumentoFechaRecepcion , '%Y-%m-%d') as  `DocumentoFechaRecepcion`, " +
                                                " DATE_FORMAT(DocumentoFechaVencimiento , '%Y-%m-%d') as  `DocumentoFechaVencimiento`, " +
                                                " DATE_FORMAT(DocumentoFechaCaptura , '%Y-%m-%d') as  `DocumentoFechaCaptura`, " +
                                                " DATE_FORMAT(DocumentoFechaEscaneo , '%Y-%m-%d') as  `DocumentoFechaEscaneo`, " +
                                                " `DocumentoObservacion`, " +
                                                " `DocumentoIdRef`, " +
                                                " `UsuarioId`, " +
                                                " `CompaniaMensajeriaId`, " +
                                                " DATE_FORMAT(DocumentoFechaEnvio , '%Y-%m-%d') as  `DocumentoFechaEnvio`, " +
                                                " `DocumentoNumeroGuia`, " +
                                                " DATE_FORMAT(DocumentoFechaEntrega , '%Y-%m-%d') as  `DocumentoFechaEntrega`, " +
                                                " `usuarioIdPreparo` " +
                                                " FROM `documento`; ";
                    MySqlDataReader respuestastring3 = con.getdatareader(sPatentesconsulta);
                    while (respuestastring3.Read())
                    {
                        String sInsert_documento = " INSERT INTO `documento` " +
                                                            " (`DocumentoId`, " +
                                                            " `DocumentoCodigoBarras`, " +
                                                            " `SubTipoDocumentoId`, " +
                                                            " `DocumentoFecha`, " +
                                                            " `DocumentoFolio`, " +
                                                            " `DocumentoFechaRecepcion`, " +
                                                            " `DocumentoFechaVencimiento`, " +
                                                            " `DocumentoFechaCaptura`, " +
                                                            " `DocumentoFechaEscaneo`, " +
                                                            " `DocumentoObservacion`, " +
                                                            " `DocumentoIdRef`, " +
                                                            " `UsuarioId`, " +
                                                            " `CompaniaMensajeriaId`, " +
                                                            " `DocumentoFechaEnvio`, " +
                                                            " `DocumentoNumeroGuia`, " +
                                                            " `DocumentoFechaEntrega`, " +
                                                            " `usuarioIdPreparo`) " +
                                                            " VALUES " +
                                                            " ( " +
                                                            getcampo(validareader("DocumentoId", "DocumentoId", respuestastring3).Text) + ", " +
                                                            getcampo(validareader("DocumentoCodigoBarras", "DocumentoId", respuestastring3).Text) + ", " +
                                                            getcampo(validareader("SubTipoDocumentoId", "DocumentoId", respuestastring3).Text) + ", " +
                                                            getcampo(validareader("DocumentoFecha", "DocumentoId", respuestastring3).Text) + ", " +
                                                            getcampo(validareader("DocumentoFolio", "DocumentoId", respuestastring3).Text) + ", " +
                                                            getcampo(validareader("DocumentoFechaRecepcion", "DocumentoId", respuestastring3).Text) + ", " +
                                                            getcampo(validareader("DocumentoFechaVencimiento", "DocumentoId", respuestastring3).Text) + ", " +
                                                            getcampo(validareader("DocumentoFechaCaptura", "DocumentoId", respuestastring3).Text) + ", " +
                                                            getcampo(validareader("DocumentoFechaEscaneo", "DocumentoId", respuestastring3).Text) + ", " +
                                                            getcampo(validareader("DocumentoObservacion", "DocumentoId", respuestastring3).Text) + ", " +
                                                            getcampo(validareader("DocumentoIdRef", "DocumentoId", respuestastring3).Text) + ", " +
                                                            getcampo(validareader("UsuarioId", "DocumentoId", respuestastring3).Text) + ", " +
                                                            getcampo(validareader("CompaniaMensajeriaId", "DocumentoId", respuestastring3).Text) + ", " +
                                                            getcampo(validareader("DocumentoFechaEnvio", "DocumentoId", respuestastring3).Text) + ", " +
                                                            getcampo(validareader("DocumentoNumeroGuia", "DocumentoId", respuestastring3).Text) + ", " +
                                                            getcampo(validareader("DocumentoFechaEntrega", "DocumentoId", respuestastring3).Text) + ", " +
                                                            getcampo(validareader("usuarioIdPreparo", "DocumentoId", respuestastring3).Text) + "); ";
                        conect coninteresado = new conect();
                        MySqlDataReader respuestastrig_interesado = coninteresado.getdatareader(sInsert_documento);
                        respuestastrig_interesado.Close();
                        coninteresado.Cerrarconexion();
                    }
                    respuestastring3.Close();
                    con.Cerrarconexion();
                }
                catch (Exception E)
                {
                    new filelog(form1contruct.sId, E.ToString());
                }
            }


            public void Actualizasubtiposolicitud()
            {
                try
                {
                    //borramos la tabla 
                    conect con_pruebas_tr = new conect();
                    //borramos todas las tablas de relacion
                    MySqlDataReader ejecutaacualizapatentes = con_pruebas_tr.getdatareader("truncate table tipodocumento;");
                    ejecutaacualizapatentes.Read();
                    ejecutaacualizapatentes.Close();
                    con_pruebas_tr.Cerrarconexion();
                    //actualizamos la tabla
                    conect_ipfacts con = new conect_ipfacts();
                    //Para updates
                    String stringquerypatentes = "";
                    String sPatentesconsulta = "select * from `tipodocumento`;";
                    MySqlDataReader respuestastring3 = con.getdatareader(sPatentesconsulta);
                    while (respuestastring3.Read())
                    {
                        String sInsert_documento = " INSERT INTO `tipodocumento` " +
                                                            " (`TipoDocumentoId`, " +
                                                            " `TipoDocumentoDescrip`, " +
                                                            " `TipoDocumentoIndAct`) " +
                                                            " VALUES " +
                                                            " ( " +
                                                            " '"+ validareader("TipoDocumentoId", "TipoDocumentoId", respuestastring3).Text + "', " +
                                                            " '"+ validareader("TipoDocumentoDescrip", "TipoDocumentoId", respuestastring3).Text + ", " +
                                                            " '"+ validareader("TipoDocumentoIndAct", "TipoDocumentoId", respuestastring3).Text + "); ";
                        conect coninteresado = new conect();
                        MySqlDataReader respuestastrig_interesado = coninteresado.getdatareader(sInsert_documento);
                        respuestastrig_interesado.Close();
                        coninteresado.Cerrarconexion();
                    }
                    respuestastring3.Close();
                    con.Cerrarconexion();
                }
                catch (Exception E)
                {
                    new filelog(form1contruct.sId, E.ToString());
                }
            }
            public void Actualiza_subtipodocumento()
            {
                String sgsInsert_documento = "";
                try
                {
                    //borramos la tabla 
                    conect con_pruebas_tr = new conect();
                    //borramos todas las tablas de relacion
                    MySqlDataReader ejecutaacualizapatentes = con_pruebas_tr.getdatareader("truncate table subtipodocumento;");
                    ejecutaacualizapatentes.Read();
                    ejecutaacualizapatentes.Close();
                    con_pruebas_tr.Cerrarconexion();
                    //actualizamos la tabla
                    conect_ipfacts con = new conect_ipfacts();
                    //Para updates
                    String stringquerypatentes = "";
                    String sPatentesconsulta = "select * from `subtipodocumento`;";
                    MySqlDataReader respuestastring3 = con.getdatareader(sPatentesconsulta);
                    String sInsert_documento = "";
                    while (respuestastring3.Read())
                    {  /*HAY ALGUN TEMA PERO AL EJECUTAR EL QUERY , NO LO REALIZA, PERO ESTA BIEN LA ESTRUCTURA EN WORCKBREANCH SI LO EJECUTA CORRECTAMENTE*/
                        sInsert_documento += " INSERT INTO `subtipodocumento` " +
                                                            " (`SubTipoDocumentoId`, " +
                                                            " `TipoDocumentoId`, " +
                                                            " `SubTipoDocumentoClave`, " +
                                                            " `SubTipoDocumentoDescrip`, " +
                                                            " `SubTipoDocumentoIndAct`, " +
                                                            " `SubTipoDocumentoIndPlazo`, " +
                                                            " `SubTipoDocumentoPlazoMeses`, " +
                                                            " `SubTipoDocumentoPlazoDias`, " +
                                                            " `SubTipoDocumentoIndTipo`, " +
                                                            " `SubTipoDocumentoIndProrrogable`, " +
                                                            " `SubTipoDocumentoTemplateEspanol`, " +
                                                            " `SubTipoDocumentoTemplateIngles`, " +
                                                            " `SubTipoDocumentoDlg`, " +
                                                            " `SubTipoDocumentoDescripI`) " +
                                                            " VALUES " +
                                                            " ( " +
                                                            " '" + validareader("SubTipoDocumentoId", "SubTipoDocumentoId", respuestastring3).Text + "'," +
                                                            " '" + validareader("TipoDocumentoId", "SubTipoDocumentoId", respuestastring3).Text + "'," +
                                                            " '" + validareader("SubTipoDocumentoClave", "SubTipoDocumentoId", respuestastring3).Text + "'," +
                                                            " '" + validareader("SubTipoDocumentoDescrip", "SubTipoDocumentoId", respuestastring3).Text + "'," +
                                                            " '" + validareader("SubTipoDocumentoIndAct", "SubTipoDocumentoId", respuestastring3).Text + "'," +
                                                            " '" + validareader("SubTipoDocumentoIndPlazo", "SubTipoDocumentoId", respuestastring3).Text + "'," +
                                                            " '" + validareader("SubTipoDocumentoPlazoMeses", "SubTipoDocumentoId", respuestastring3).Text + "'," +
                                                            " '" + validareader("SubTipoDocumentoPlazoDias", "SubTipoDocumentoId", respuestastring3).Text + "'," +
                                                            " '" + validareader("SubTipoDocumentoIndTipo", "SubTipoDocumentoId", respuestastring3).Text + "'," +
                                                            " '" + validareader("SubTipoDocumentoIndProrrogable", "SubTipoDocumentoId", respuestastring3).Text + "'," +
                                                            " '" + validareader("SubTipoDocumentoTemplateEspanol", "SubTipoDocumentoId", respuestastring3).Text + "'," +
                                                            " '" + validareader("SubTipoDocumentoTemplateIngles", "SubTipoDocumentoId", respuestastring3).Text + "'," +
                                                            " '" + validareader("SubTipoDocumentoDlg", "SubTipoDocumentoId", respuestastring3).Text + "'," +
                                                            " '" + validareader("SubTipoDocumentoDescripI", "SubTipoDocumentoId", respuestastring3).Text + "'); ";
                        
                    }
                    conect coninteresado = new conect();
                    MySqlDataReader respuestastrig_interesado = coninteresado.getdatareader(sInsert_documento);
                    sgsInsert_documento = sInsert_documento;
                    respuestastrig_interesado.Close();
                    coninteresado.Cerrarconexion();
                    respuestastring3.Close();
                    con.Cerrarconexion();
                }catch (Exception E){
                    new filelog(form1contruct.sId, E.ToString() + " query:" + sgsInsert_documento);
                }
            }
            public void Actualiza_gruposubtipodocumento()
            {
                String sgsInsert_documento = "";
                try
                {
                    //borramos la tabla 
                    conect con_pruebas_tr = new conect();
                    //borramos todas las tablas de relacion
                    MySqlDataReader ejecutaacualizapatentes = con_pruebas_tr.getdatareader("truncate table gruposubtipodocumento;");
                    ejecutaacualizapatentes.Read();
                    ejecutaacualizapatentes.Close();
                    con_pruebas_tr.Cerrarconexion();
                    //actualizamos la tabla
                    conect_ipfacts con = new conect_ipfacts();
                    //Para updates
                    String stringquerypatentes = "";
                    String sPatentesconsulta = "select * from `gruposubtipodocumento`;";
                    MySqlDataReader respuestastring3 = con.getdatareader(sPatentesconsulta);
                    while (respuestastring3.Read())
                    {
                        String sInsert_documento = " INSERT INTO `gruposubtipodocumento` " +
                                                            " (`GrupoSubtipodocumentoId`, " +
                                                            " `GrupoId`, " +
                                                            " `SubtipodocumentoId`) " +
                                                            " VALUES " +
                                                            " ( " +
                                                            " '" + validareader("GrupoSubtipodocumentoId", "GrupoSubtipodocumentoId", respuestastring3).Text + "'," +
                                                            " '" + validareader("GrupoId", "GrupoId", respuestastring3).Text + "'," +
                                                            " '" + validareader("SubtipodocumentoId", "SubtipodocumentoId", respuestastring3).Text + "');";
                        conect coninteresado = new conect();
                        MySqlDataReader respuestastrig_interesado = coninteresado.getdatareader(sInsert_documento);
                        sgsInsert_documento = sInsert_documento;
                        respuestastrig_interesado.Close();
                        coninteresado.Cerrarconexion();
                        //mx/e/2020/000001
                    }
                    respuestastring3.Close();
                    con.Cerrarconexion();
                }
                catch (Exception E)
                {
                    new filelog(form1contruct.sId, E.ToString() + " query:" + sgsInsert_documento);
                }
            }

            public void Actualiza_estatuscasosubtipodocumento()
            {
                String sgsInsert_documento = "";
                try
                {
                    //borramos la tabla 
                    conect con_pruebas_tr = new conect();
                    //borramos todas las tablas de relacion
                    MySqlDataReader ejecutaacualizapatentes = con_pruebas_tr.getdatareader("truncate table estatuscasosubtipodocumento;");
                    ejecutaacualizapatentes.Read();
                    ejecutaacualizapatentes.Close();
                    con_pruebas_tr.Cerrarconexion();
                    //actualizamos la tabla
                    conect_ipfacts con = new conect_ipfacts();
                    //Para updates
                    String stringquerypatentes = "";
                    String sPatentesconsulta = "select * from `estatuscasosubtipodocumento`;";
                    MySqlDataReader respuestastring3 = con.getdatareader(sPatentesconsulta);
                    while (respuestastring3.Read())
                    {
                        String sInsert_documento = " INSERT INTO `estatuscasosubtipodocumento` " +
                                                            " (`EstatusCasoSubTipoDocumentoid`, " +
                                                            " `GrupoId`, " +
                                                            " `EstatusCasoId`, " +
                                                            " `SubTipoDocumentoId`) " +
                                                            " VALUES " +
                                                            " ( " +
                                                            " '" + validareader("EstatusCasoSubTipoDocumentoid", "EstatusCasoSubTipoDocumentoid", respuestastring3).Text + "'," +
                                                            " '" + validareader("GrupoId", "EstatusCasoSubTipoDocumentoid", respuestastring3).Text + "'," +
                                                            " '" + validareader("EstatusCasoId", "EstatusCasoSubTipoDocumentoid", respuestastring3).Text + "'," +
                                                            " '" + validareader("SubTipoDocumentoId", "EstatusCasoSubTipoDocumentoid", respuestastring3).Text + "');";
                        conect coninteresado = new conect();
                        MySqlDataReader respuestastrig_interesado = coninteresado.getdatareader(sInsert_documento);
                        sgsInsert_documento = sInsert_documento;
                        respuestastrig_interesado.Close();
                        coninteresado.Cerrarconexion();
                        //mx/e/2020/000001
                    }
                    respuestastring3.Close();
                    con.Cerrarconexion();
                }
                catch (Exception E)
                {
                    new filelog(form1contruct.sId, E.ToString() + " query:" + sgsInsert_documento);
                }
            }

            public void Actualizatiposolicitud() 
            {
                try { 
                conect_ipfacts con = new conect_ipfacts();
                String sPatentesconsulta = " SELECT " +
                                           "     CasoId," +
                                           "     TipoSolicitudId," +
                                           "     SubTipoSolicitudId," +
                                           "     TipoPctId," +
                                           "     CasoDenominacion," +
                                           "     CasoTitulo," +
                                           "     IdiomaId," +
                                           "     DATE_FORMAT(CasoFechaConcesion , '%Y-%m-%d') as  CasoFechaConcesion," +
                                           "     DATE_FORMAT(CasoFechaRecepcion , '%Y-%m-%d') as  CasoFechaRecepcion," +
                                           "     DATE_FORMAT(CasoFechaVigencia , '%Y-%m-%d') as  CasoFechaVigencia," +
                                           "     DATE_FORMAT(CasoFechaPublicacionSolicitud , '%Y-%m-%d') as  CasoFechaPublicacionSolicitud," +
                                           "     DATE_FORMAT(CasoFechaLegal , '%Y-%m-%d') as  CasoFechaLegal," +
                                           "     CasoNumConcedida," +
                                           "     CasoNumeroExpedienteLargo," +
                                           "     CasoNumero," +
                                           "     ResponsableId," +
                                           "     CasoTipoCaptura," +
                                           "     CasoTitular," +
                                           "     EstatusCasoId," +
                                           "     UsuarioId," +
                                           "     AreaImpiId," +
                                           "     DATE_FORMAT(CasoFechaInternacional , '%Y-%m-%d') as  CasoFechaInternacional," +
                                           "     PaisId," +
                                           "     DATE_FORMAT(CasoFechaPruebaUsoSig , '%Y-%m-%d') as  CasoFechaPruebaUsoSig," +
                                           "     DATE_FORMAT(CasoFechaFilingCliente , '%Y-%m-%d') as  CasoFechaFilingCliente," +
                                           "     DATE_FORMAT(CasoFechaFilingSistema , '%Y-%m-%d') as  CasoFechaFilingSistema," +
                                           "     DATE_FORMAT(CasoFechaDivulgacionPrevia , '%Y-%m-%d') as  CasoFechaDivulgacionPrevia," +
                                           "     DATE_FORMAT(CasoFechaCartaCliente , '%Y-%m-%d') as  CasoFechaCartaCliente" +
                                           " FROM" +
                                           "     caso";
                MySqlDataReader respuestastring3 = con.getdatareader(sPatentesconsulta);
                while (respuestastring3.Read())
                {
                    String sUpdate = "UPDATE `casointeresado` SET `TipoSolicitudId` = " + getcampo(validareader("TipoSolicitudId", "CasoId", respuestastring3).Text) +
                                     " WHERE `casointeresado`.`CasoId` = " + validareader("CasoId", "CasoId", respuestastring3).Text + ";";
                    conect coninteresado = new conect();
                    MySqlDataReader respuestastrig_interesado = coninteresado.getdatareader(sUpdate);
                    respuestastrig_interesado.Close();
                    coninteresado.Cerrarconexion();

                    String sUpdatecasocliente = "UPDATE `casocliente` SET `TipoSolicitudId` = " + getcampo(validareader("TipoSolicitudId", "CasoId", respuestastring3).Text) +
                                     " WHERE `casocliente`.`CasoId` = " + validareader("CasoId", "CasoId", respuestastring3).Text + ";";
                    conect conin_casocliente = new conect();
                    MySqlDataReader respuestastrig_casocliente = conin_casocliente.getdatareader(sUpdatecasocliente);
                    respuestastrig_casocliente.Close();
                    conin_casocliente.Cerrarconexion();

                    String sUpdateprioridad = "UPDATE `prioridad` SET `TipoSolicitudId` = " + getcampo(validareader("TipoSolicitudId", "CasoId", respuestastring3).Text) +
                                     " WHERE `prioridad`.`CasoId` = " + validareader("CasoId", "CasoId", respuestastring3).Text + ";";
                    conect conin_prioridad = new conect();
                    MySqlDataReader respuestastrig_prioridad = conin_prioridad.getdatareader(sUpdateprioridad);
                    respuestastrig_prioridad.Close();
                    conin_prioridad.Cerrarconexion();

                    conect conin_plazo = new conect();
                    String sUpdateplazo = "UPDATE `plazo` SET `TipoSolicitudId` = " + getcampo(validareader("TipoSolicitudId", "CasoId", respuestastring3).Text) +
                                     " WHERE `plazo`.`CasoId` = " + validareader("CasoId", "CasoId", respuestastring3).Text + ";";
                    MySqlDataReader respuestastrig_plazo = conin_plazo.getdatareader(sUpdateplazo);
                    respuestastrig_plazo.Close();
                    conin_plazo.Cerrarconexion();

                    String sUpdate_casoproductos = "UPDATE `casoproductos` SET `TipoSolicitudId` = " + getcampo(validareader("TipoSolicitudId", "CasoId", respuestastring3).Text) +
                                     " WHERE `casoproductos`.`CasoId` = " + validareader("CasoId", "CasoId", respuestastring3).Text + ";";
                    conect conin_casoproductos = new conect();
                    MySqlDataReader respuestastrig_casoproductos = conin_casoproductos.getdatareader(sUpdate_casoproductos);
                    respuestastrig_casoproductos.Close();
                    conin_casoproductos.Cerrarconexion();


                    String sUpdate_referencia = "UPDATE `referencia` SET `TipoSolicitudId` = " + getcampo(validareader("TipoSolicitudId", "CasoId", respuestastring3).Text) +
                                     " WHERE `referencia`.`CasoId` = " + validareader("CasoId", "CasoId", respuestastring3).Text + ";";
                    conect conin_referencia = new conect();
                    MySqlDataReader respuestastrig_referencia = conin_referencia.getdatareader(sUpdate_referencia);
                    respuestastrig_referencia.Close();
                    conin_referencia.Cerrarconexion();

                    String sUpdate_anualidad = "UPDATE `anualidad` SET `TipoSolicitudId` = " + getcampo(validareader("TipoSolicitudId", "CasoId", respuestastring3).Text) +
                                     " WHERE `anualidad`.`CasoId` = " + validareader("CasoId", "CasoId", respuestastring3).Text + ";";
                    conect conin_anualidad = new conect();
                    MySqlDataReader respuestastrig_anualidad = conin_anualidad.getdatareader(sUpdate_anualidad);
                    respuestastrig_anualidad.Close();
                    conin_anualidad.Cerrarconexion();

                    String sUpdate_relaciondocumento = "UPDATE `relaciondocumento` SET `TipoSolicitudId` = " + getcampo(validareader("TipoSolicitudId", "CasoId", respuestastring3).Text) +
                                     " WHERE `relaciondocumento`.`CasoId` = " + validareader("CasoId", "CasoId", respuestastring3).Text + ";";
                    conect conin_relaciondocumento = new conect();
                    MySqlDataReader respuestastrig_relaciondocumento = conin_relaciondocumento.getdatareader(sUpdate_relaciondocumento);
                    respuestastrig_relaciondocumento.Close();
                    conin_relaciondocumento.Cerrarconexion();
                   
                }
                respuestastring3.Close();
                con.Cerrarconexion();
                }catch(Exception E){
                    new filelog(form1contruct.sId, E.ToString());
                }
            }

            private void informaciónDeOficinaToolStripMenuItem_Click(object sender, EventArgs e)
            {
                FOficina obj = new FOficina(form1contruct, this);
                obj.ShowDialog();
            }

            private void relacionDocToolStripMenuItem_Click(object sender, EventArgs e)
            {
                relaciondoc obj = new relaciondoc();
                obj.Show();
            }

            private void consultarPlazosToolStripMenuItem_Click(object sender, EventArgs e)
            {
            plazos_consulta obj_plazo = new plazos_consulta(form1contruct, this);
                obj_plazo.ShowDialog();
            }

            private void catálogoDePlazosToolStripMenuItem_Click(object sender, EventArgs e)
            {
            //tipoplazo obj = new tipoplazo();
            //obj.ShowDialog();
            catTipoplazos catobj = new catTipoplazos();
            catobj.ShowDialog();
            }

            private void documentosGeneraPlazosToolStripMenuItem_Click(object sender, EventArgs e)
            {
                //relacionadocumentoplazo objrela = new relacionadocumentoplazo();
                //objrela.Show();

                CatalagoEstatus objdoc = new CatalagoEstatus();
                objdoc.Show();
            }

            private void subTipoDeDocumentosVsEstatusCasoToolStripMenuItem_Click(object sender, EventArgs e)
            {
                estatuspordocumento objnn = new estatuspordocumento();
                objnn.ShowDialog();
            }

            private void catálogoDeEstatusDelCasoToolStripMenuItem_Click(object sender, EventArgs e)
            {
                CatEstatus objnew = new CatEstatus();
                objnew.ShowDialog();

            }

            private void generaDocsCambioDireccionToolStripMenuItem_Click(object sender, EventArgs e)
            {
                generadocscambiodom obj = new generadocscambiodom();
                obj.generadocs("6300");
            }

            private void casoNuevoToolStripMenuItem_Click(object sender, EventArgs e)
            {
                //Nuevocaso ncaso = new Nuevocaso(form1contruct, this);
                //ncaso.Show();
                Caso_nuevo_bBuscargrupo obj = new Caso_nuevo_bBuscargrupo(this, form1contruct);
                obj.Show();
            }

            private void casoNuevoToolStripMenuItem1_Click(object sender, EventArgs e)
            {
                //ncaso.Show();
                Caso_nuevo_bBuscargrupo obj = new Caso_nuevo_bBuscargrupo(this, form1contruct);
                obj.Show();
            }

            private void casointeresadoToolStripMenuItem_Click(object sender, EventArgs e)
            {
                actualizatablasrelacionales_casointeresado();
            }

            private void actualizaprioridadToolStripMenuItem_Click(object sender, EventArgs e)
            {
                actualizatabla_prioridad();
            }

            private void anualidadToolStripMenuItem_Click(object sender, EventArgs e)
            {

            }

            private void facturadorYaoToolStripMenuItem_Click(object sender, EventArgs e)
            {
                var facturador = new Facturador.Facturador();
                facturador.ShowDialog(this);
            }

            private void tarigToolStripMenuItem_Click(object sender, EventArgs e)
            {
                Facturador.Actualizartarifas_excel obj = new Facturador.Actualizartarifas_excel();
                obj.ShowDialog();
            }

            private void facturademoToolStripMenuItem_Click(object sender, EventArgs e)
            {
                obj_factura_concepto obj_conceptos = new obj_factura_concepto("concepto uno","concepto descripcion ", "1000.00");
                obj_factura_concepto obj_conceptos2 = new obj_factura_concepto("concepto dos", "concepto descripcion dos", "1450.65");
                obj_factura_concepto obj_conceptos3 = new obj_factura_concepto("concepto dos", "concepto descripcion ", "450.00");
                obj_factura_concepto obj_conceptos4 = new obj_factura_concepto("concepto dos", "concepto descripcion dos", "150.11");
                obj_factura_concepto obj_conceptos5 = new obj_factura_concepto("concepto dos", "concepto descripcion dos", "10.00");
                List<obj_factura_concepto> conceptosfac = new List<obj_factura_concepto>();
                conceptosfac.Add(obj_conceptos);
                conceptosfac.Add(obj_conceptos2);
                conceptosfac.Add(obj_conceptos3);
                conceptosfac.Add(obj_conceptos4);
                conceptosfac.Add(obj_conceptos5);
                obj_factura_pdf obj_pdf = new obj_factura_pdf("Cliente Nombre", "1001", "15", "01", "150", conceptosfac);
                plantillafactura ejecut_tmp = new plantillafactura(obj_pdf);
                //PlantillaFactura.plantillafactura template = new PlantillaFactura.plantillafactura();
                //template.generadocs("");
            }

            private void relaciónSubtipodocumentoTarifaToolStripMenuItem_Click(object sender, EventArgs e)
            {
                relaciona_subtipodoocumento_concepto_tarifa obj = new relaciona_subtipodoocumento_concepto_tarifa();
                obj.ShowDialog();
            }

            private void casoTransferidoToolStripMenuItem_Click(object sender, EventArgs e)
            {
                Caso_nuevo_bBuscargrupo obj = new Caso_nuevo_bBuscargrupo(this, form1contruct);
                obj.iTransferido = true;
                obj.Show();
            }

            private void actualizarPlazosToolStripMenuItem_Click(object sender, EventArgs e)
            {
                //actualizadirecciones();
                actualizatabla_plazo();
            }

            private void actualizagruposubtipodocumentoToolStripMenuItem_Click(object sender, EventArgs e)
            {
                Actualiza_gruposubtipodocumento();
            }

            private void actualizasubtipodocumentoestatuscasoToolStripMenuItem_Click(object sender, EventArgs e)
            {
                //Actualiza_subtipodocumentoestatuscaso();
            }

            private void actualizasubtipodocumnetoToolStripMenuItem_Click(object sender, EventArgs e)
            {
                //subtipodocumento
                //Actualiza_subtipodocumento();
            }

            private void actualizaClientesToolStripMenuItem_Click(object sender, EventArgs e)
            {
                actualizatabla_cliente();
            }

            private void actualizaContactoToolStripMenuItem_Click(object sender, EventArgs e)
            {
                actualizatabla_contacto();
            }

            private void actualizaCasoPatentesToolStripMenuItem_Click(object sender, EventArgs e)
            {
                actualizapatentes();
            }

            private void actualizaAnualidadesToolStripMenuItem_Click(object sender, EventArgs e)
            {
                actualizatabla_anualidad();
            }

            private void excelXmlToolStripMenuItem_Click(object sender, EventArgs e)
            {    
            //XmlTextReader reader = new XmlTextReader("C:\\Users\\eduarximo\\Downloads\\MA_RE_2019_03_011.xml");
            XDocument xmlDoc1 = XDocument.Load("C:\\Users\\eduarximo\\Downloads\\MA_RE_2019_03_011.xml");
            string fileName = "C:\\Users\\eduarximo\\Downloads\\MA_RE_2019_03_011.xml";

            String text = File.ReadAllText(fileName);
               
            XDocument xmlDoc = XDocument.Parse(HttpUtility.HtmlDecode(text));
            
            String sEtiqueta = "";
            
            XmlNodeType nodo = xmlDoc.NodeType;
            
            String sValor = "";

            String que_onda = nodo.ToString();
            IEnumerable<XNode> lista;
            int icount = 0;
            foreach (var node in xmlDoc.DescendantNodes())
            {
                if (node is XText)
                {
                    MessageBox.Show(((XText)node).Value);
                    //some code...
                }
                if (node is XElement)
                {
                    //some code for XElement...
                    //MessageBox.Show("");
                    XName name ;//= new XName;
                    XElement elemento = ((XElement)node);//((XElement)node).Attributes();
                    IEnumerable<XElement> childElements = from el in elemento.Elements()
                    select el;


                    foreach (XElement el in childElements)
                    {
                        XElement hijo_ = el;
                        IEnumerable<XElement> childElements_ = from el_dos in hijo_.Elements()
                                                              select el_dos;
                        foreach (XElement el_dos in childElements_) {
                            
                            XElement hijo_del_hijo = el_dos;
                            IEnumerable<XElement> childElements_tres = from el_tres in hijo_del_hijo.Elements()
                                                                       select el_tres;
                            foreach (XElement el_tres in childElements_tres)
                            {
                                XElement hijo_del_hijo_del_hijo = el_tres;
                                IEnumerable<XElement> childElements_cuatro = from el_cuatro in hijo_del_hijo_del_hijo.Elements()
                                                                           select el_cuatro;
                                foreach (XElement el_cuatro in childElements_cuatro)
                                {
                                    Console.WriteLine("Name: " + el_cuatro.Name + "  Value: " + el_cuatro.Value);
                                }
                                MessageBox.Show("Elemento");
                            }
                            
                        }
                            
                    }

                        //IEnumerable<XElement> childElements_dos = from el_dos in el.Elements()
                        //                                          select el_dos;
                        //foreach (XElement el_dos in childElements_dos)
                        //    Console.WriteLine("Name: " + el_dos.Name);
                        
                        
                        //IEnumerable<XElement> childElementsdos = from el_dos in el.Elements()
                        //Console.WriteLine("Name: " + el.Name);
                }
            }
                //Avisos comerciales
            //XDocument xmlDoc_avisos = XDocument.Parse(HttpUtility.HtmlDecode(lista.First().ToString()));

            
                //while (reader.Read())
                //{

                //    if (icount<=10)
                //    {
                        

                //        if (reader.Name=="clave")
                //        {
                //            sEtiqueta += reader.Name + "\n";
                //            sValor += reader.Value + "\n";
                //            if ((reader.NodeType == XmlNodeType.CDATA)) {
                //                sValor += reader.Value + "\n";
                //            }
                //        }
                //        if (reader.Value == "clave")
                //        {
                //        }
                //    }
                    
                //    //if ((reader.NodeType == XmlNodeType.CDATA))
                //    //{
                        
                //    //    svariable += reader.ReadContentAsString();
                //    //    sValor = reader.ReadInnerXml();

                //    //}
                //    //else
                //    //{
                //    //    svariable += "\r";
                //    //}
                //    icount++;
                //}
                //MessageBox.Show(sEtiqueta + " " + sValor);
            }

        private void ultimoscasosMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void últimosCasosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            consultaulticasos ultim = new consultaulticasos();
            ultim.ShowDialog();
        }

        private void configuraciónDeParametrosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void actualizarLogosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            procesaimagenesycarpetas();
        }

        private void captura_Load(object sender, EventArgs e)
        {
            
        }

        private void crearToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void configuraciónDePlazosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            subtipodocplazos obj = new subtipodocplazos();
            obj.ShowDialog();
        }

        private void consultaDocumentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fDocumentos obj = new fDocumentos();
            obj.Show();
        }
        public bool validaversion(String sVersion)
        {
            bool breinicia = false;
            try
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

        }

        private void button2_Click(object sender, EventArgs e)
        {
            form1contruct.Close();
            this.Close();
            Application.Exit();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void mRobotPlazos_Click(object sender, EventArgs e)
        {
            home objrobot = new home(form1contruct);
            objrobot.ShowDialog();
        }

        private void solicitanteToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void inventorToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void agregarCorresponsalToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void correoPlazosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //serviciocorreoking sObj;
            //serviciocorreoking obj;
            //var oibj = new serviciocorreoking();
            //serviciocorreoking obj;
            servcorreo obj = new servcorreo();
            obj.ShowDialog();

        }

        private void variosCasosNuevosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
