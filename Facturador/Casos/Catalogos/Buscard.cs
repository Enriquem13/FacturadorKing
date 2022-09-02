using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
using System.Diagnostics;

namespace Facturador
{
    public partial class Buscard : Form
    {
        public captura capt;
        public Form1 login;
        public Process myProcess;
        public String titulo;
        public String cliente;
        public String interesado;
        public String sClienteID;
        public String sDenominacion;
        public String sTiposolicitud;
        //caso.CasoFechaConcesion, caso.CasoFechaRecepcion, caso.CasoFechaVigencia, caso.CasoNumConcedida, caso.CasoNumeroExpediente, caso.CasoNumeroExpedienteLargo, idioma.IdiomaDescripcion, caso.CasoTitulo, caso.CasoDenominacion, cliente.ClienteId, cliente.ClienteNombre, interesado.InteresadoNombre 
        public String sCasoFechaConcesion, CasoFechaRecepcion, CasoFechaVigencia, CasoNumConcedida, CasoNumeroExpediente, CasoNumeroExpedienteLargo, IdiomaDescripcion, CasoTitulo, CasoDenominacion, ClienteId, ClienteNombre, InteresadoNombre, sEdtatuscasoid, sEstatuscasoDesc;
        public  Buscard(captura cap, Form1 log)
        {
            capt = cap;
            login = log;
            InitializeComponent();
            comboServicio.Items.Clear();
            //combInteresado.Enabled = false;
            String sIdservicios = "";
            conect con = new conect();
            String query = "SELECT ConceptoCargoId, ConceptoDescripcionEsp" +
                            " FROM conceptocargo  ORDER BY `conceptocargo`.`ConceptoDescripcionEsp` ASC";
            MySqlDataReader respuestastring = con.getdatareader(query);
            String ConceptoDescripcionEsp = "";

            while (respuestastring.Read())
            {
                if (!respuestastring.IsDBNull(respuestastring.GetOrdinal("ConceptoDescripcionEsp")))
                {
                    ConceptoDescripcionEsp = respuestastring.GetString(respuestastring.GetOrdinal("ConceptoDescripcionEsp"));
                }
                else
                {
                    ConceptoDescripcionEsp = "";
                }

                if (!respuestastring.IsDBNull(respuestastring.GetOrdinal("ConceptoCargoId")))
                {
                    sIdservicios = respuestastring.GetString(respuestastring.GetOrdinal("ConceptoCargoId"));
                }
                else
                {
                    sIdservicios = "";
                }
                tNumservicio.Text = sIdservicios;
                comboServicio.Items.Add(sIdservicios + "-" + ConceptoDescripcionEsp);
            }

            conect con2 = new conect();
            String query2 = "SELECT ClienteId, ClienteNombre" +
                            " FROM cliente ORDER BY `cliente`.`ClienteNombre` ASC";
            MySqlDataReader respuestastring2 = con.getdatareader(query2);
            String ClienteId = "";
            String ClienteNombre = "";

            while (respuestastring2.Read())
            {
                if (!respuestastring2.IsDBNull(respuestastring2.GetOrdinal("ClienteId")))
                {
                    ClienteId = respuestastring2.GetString(respuestastring2.GetOrdinal("ClienteId"));
                }
                else
                {
                    ClienteId = "";
                }
                if (!respuestastring2.IsDBNull(respuestastring2.GetOrdinal("ClienteNombre")))
                {
                    ClienteNombre = respuestastring2.GetString(respuestastring2.GetOrdinal("ClienteNombre"));
                }
                else
                {
                    ClienteNombre = "";
                }
                cClientebox.Items.Add(ClienteNombre + " - " + ClienteId);
            }
            //listBox2 agregamos los servicios ó conceptos de cargo


            //conect con3 = new conect();
            //String query3 = "SELECT c_ClaveProdServ, Descripcion FROM `f4_c_claveprodserv` ORDER BY `f4_c_claveprodserv`.`Descripcion` ASC";
            //MySqlDataReader respuestastring3 = con.getdatareader(query3);
            //String sIdservicio = "";
            //String sDescripcionservicio = "";

            //while (respuestastring3.Read())
            //{
            //    if (!respuestastring3.IsDBNull(respuestastring3.GetOrdinal("c_ClaveProdServ")))
            //    {
            //        sIdservicio = respuestastring3.GetString(respuestastring3.GetOrdinal("c_ClaveProdServ"));
            //    }
            //    else
            //    {
            //        sIdservicio = "";
            //    }
            //    if (!respuestastring3.IsDBNull(respuestastring3.GetOrdinal("Descripcion")))
            //    {
            //        sDescripcionservicio = respuestastring3.GetString(respuestastring3.GetOrdinal("Descripcion"));
            //    }
            //    else
            //    {
            //        sDescripcionservicio = "";
            //    }
            //    combSatservicios.Items.Add(sIdservicio + " - " + sDescripcionservicio);
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            myProcess = new Process();
            conect con = new conect();
            Random ran = new Random(1);
            String numbusqueda = tNumCaso.Text;
            //combInteresado.Items.Clear();

            /*  select grupo.GrupoDescripcion, caso.TipoSolicitudId 
                from caso, grupo, grupotiposolicitud
                where caso.CasoNumero = "6300" and
                grupotiposolicitud.TipoSolicitudId = caso.TipoSolicitudId and 
                grupo.GrupoId = grupotiposolicitud.GrupoId;*/


            if (!numbusqueda.Equals(""))
            {

                conect con3 = new conect();
                String query3 = "select grupo.GrupoDescripcion, caso.TipoSolicitudId "+
                "from caso, grupo, grupotiposolicitud "+
                "where caso.CasoNumero = \""+6300+"\" and "+
                "grupotiposolicitud.TipoSolicitudId = caso.TipoSolicitudId and "+
                "grupo.GrupoId = grupotiposolicitud.GrupoId;";
                MySqlDataReader respuestastring3 = con.getdatareader(query3);
                String sIdservicio = "";
                String sDescripcionservicio = "";

                while (respuestastring3.Read())
                {
                    if (!respuestastring3.IsDBNull(respuestastring3.GetOrdinal("GrupoDescripcion")))
                    {
                        sTiposolicitud = respuestastring3.GetString(respuestastring3.GetOrdinal("GrupoDescripcion"));
                    }
                    else
                    {
                        sTiposolicitud = "";
                    }

                    lTiposol.Text = sTiposolicitud;
                }
                //SELECT * FROM `caso`,`casocliente`,`casointeresado` where caso.CasoId = 27634 and casocliente.CasoId = 27634 and casointeresado.CasoId = 27634
                //String consulta = "SELECT * FROM `caso`,`casocliente`,`casointeresado` where caso.CasoId = " + numbusqueda + " and casocliente.CasoId = " + numbusqueda + " and casointeresado.CasoId = " + numbusqueda;
                //String query = "SELECT caso.CasoTitulo, caso.CasoDenominacion, cliente.ClienteId, cliente.ClienteNombre, interesado.InteresadoNombre" +
                //                " FROM caso, casocliente, casointeresado, cliente, interesado" +
                //                " where " +
                //                "caso.CasoId = " + numbusqueda + " and " +
                //                "casocliente.CasoId = " + numbusqueda + " and " +
                //                "casointeresado.CasoId = " + numbusqueda + " and " +
                //                "casocliente.ClienteId = cliente.ClienteId and " +
                //                "casointeresado.InteresadoId = interesado.InteresadoID";
                String query = "SELECT estatuscaso.EstatusCasoId, estatuscaso.EstatusCasoDescrip, caso.CasoFechaConcesion, caso.CasoFechaRecepcion, caso.CasoFechaVigencia, " +
                                "caso.CasoNumConcedida, caso.CasoNumeroExpediente, caso.CasoNumeroExpedienteLargo, idioma.IdiomaDescripcion, caso.CasoTitulo, " +
                                "caso.CasoDenominacion, cliente.ClienteId, cliente.ClienteNombre, interesado.InteresadoNombre " +

                                "FROM caso, estatuscaso, casocliente, casointeresado, cliente, interesado, idioma " +
                                
                                "where caso.CasoNumero = \"" + numbusqueda + "\" and " +
                                "casocliente.CasoId = caso.CasoId and "+
                                "casointeresado.CasoId = caso.CasoId and "+
                                "casocliente.ClienteId = cliente.ClienteId and "+
                                "casointeresado.InteresadoId = interesado.InteresadoID and "+
                                "(casointeresado.TipoRelacionId = 1 or casointeresado.TipoRelacionId = 3) and "+
                                "caso.IdiomaId = idioma.IdiomaId and caso.EstatusCasoId = estatuscaso.EstatusCasoId;";

                MySqlDataReader respuestastring = con.getdatareader(query);
                
                /*listView1.GridLines = true;
                listView1.FullRowSelect = true;
                //Add column header
                listView1.Columns.Add("Campouno", 100);
                listView1.Columns.Add("campodos", 70);
                listView1.Columns.Add("campostres", 70);*/
                int contador = 0;
                //, 
                //, 
                //, 
                //, 
                //, 
                //, 
                //, 
                //CasoTitulo, 
                //CasoDenominacion, 
                //ClienteId, 
                //ClienteNombre, 
                //InteresadoNombre;
                //sEdtatuscasoid, sEstatuscasoDesc;
                while (respuestastring.Read())
                {

                    if (!respuestastring.IsDBNull(respuestastring.GetOrdinal("CasoFechaConcesion")))
                    {
                        sCasoFechaConcesion = respuestastring.GetString(respuestastring.GetOrdinal("CasoFechaConcesion"));
                    }
                    else
                    {
                        sCasoFechaConcesion = "vacio";
                    }
                    if (!respuestastring.IsDBNull(respuestastring.GetOrdinal("CasoFechaRecepcion")))
                    {
                        CasoFechaRecepcion = respuestastring.GetString(respuestastring.GetOrdinal("CasoFechaRecepcion"));
                    }
                    else
                    {
                        CasoFechaRecepcion = "vacio";
                    }
                    if (!respuestastring.IsDBNull(respuestastring.GetOrdinal("CasoFechaVigencia")))
                    {
                        CasoFechaVigencia = respuestastring.GetString(respuestastring.GetOrdinal("CasoFechaVigencia"));
                    }
                    else
                    {
                        CasoFechaVigencia = "vacio";
                    }
                    if (!respuestastring.IsDBNull(respuestastring.GetOrdinal("CasoNumConcedida")))
                    {
                        CasoNumConcedida = respuestastring.GetString(respuestastring.GetOrdinal("CasoNumConcedida"));
                    }
                    else
                    {
                        CasoNumConcedida = "vacio";
                    }
                    if (!respuestastring.IsDBNull(respuestastring.GetOrdinal("CasoNumeroExpediente")))
                    {
                        CasoNumeroExpediente = respuestastring.GetString(respuestastring.GetOrdinal("CasoNumeroExpediente"));
                    }
                    else
                    {
                        CasoNumeroExpediente = "vacio";
                    }
                    if (!respuestastring.IsDBNull(respuestastring.GetOrdinal("CasoNumeroExpedienteLargo")))
                    {
                        CasoNumeroExpedienteLargo = respuestastring.GetString(respuestastring.GetOrdinal("CasoNumeroExpedienteLargo"));
                    }
                    else
                    {
                        CasoNumeroExpedienteLargo = "vacio";
                    }
                    if (!respuestastring.IsDBNull(respuestastring.GetOrdinal("IdiomaDescripcion")))
                    {
                        IdiomaDescripcion = respuestastring.GetString(respuestastring.GetOrdinal("IdiomaDescripcion"));
                    }
                    else
                    {
                        IdiomaDescripcion = "vacio";
                    }


                    if (!respuestastring.IsDBNull(respuestastring.GetOrdinal("EstatusCasoId")))
                    {
                        sEdtatuscasoid = respuestastring.GetString(respuestastring.GetOrdinal("EstatusCasoId"));
                    }
                    else
                    {
                        sEdtatuscasoid = "vacio";
                    }
                    if (!respuestastring.IsDBNull(respuestastring.GetOrdinal("EstatusCasoDescrip")))
                    {
                        sEstatuscasoDesc = respuestastring.GetString(respuestastring.GetOrdinal("EstatusCasoDescrip"));
                    }
                    else
                    {
                        sEstatuscasoDesc = "vacio";
                    }

                    //campos requeridos
                    if (!respuestastring.IsDBNull(respuestastring.GetOrdinal("CasoTitulo")))
                    {
                        titulo = respuestastring.GetString(respuestastring.GetOrdinal("CasoTitulo"));
                    }
                    else
                    {
                        titulo = "vacio";
                    }

                    if (!respuestastring.IsDBNull(respuestastring.GetOrdinal("CasoDenominacion")))
                    {
                        sDenominacion = respuestastring.GetString(respuestastring.GetOrdinal("CasoDenominacion"));
                    }
                    else
                    {
                        sDenominacion = "vacio";
                    }

                    if (!respuestastring.IsDBNull(respuestastring.GetOrdinal("ClienteId")))
                    {
                        sClienteID = respuestastring.GetString(respuestastring.GetOrdinal("ClienteId"));
                    }
                    else
                    {
                        sClienteID = "vacio";
                    }


                    if (!respuestastring.IsDBNull(respuestastring.GetOrdinal("ClienteNombre")))
                    {
                        cliente = respuestastring.GetString(respuestastring.GetOrdinal("ClienteNombre"));
                    }
                    else
                    {
                        cliente = "vacio";
                    }


                    if (!respuestastring.IsDBNull(respuestastring.GetOrdinal("InteresadoNombre")))
                    {
                        interesado = respuestastring.GetString(respuestastring.GetOrdinal("InteresadoNombre"));
                    }
                    else
                    {
                        interesado = "vacio";
                    }

                    ListViewItem lista = new ListViewItem("id");
                    lista.SubItems.Add(titulo);
                    lista.SubItems.Add(cliente);
                    lista.SubItems.Add(interesado);
                  
                    //combInteresado.Items.Add(interesado);
                    //combInteresado.Hide();
                    contador++;
                }
                    lInteresado.Text = interesado;
                //if (contador > 1)
                //{
                //    combInteresado.Enabled = true;
                //    lInteresado.Text = interesado;
                //}
                //else {
                //    lInteresado.Text = interesado;
                //    combInteresado.Enabled = false;
                //}

                textTitulo.Text = titulo;
                cClientebox.Text = cliente;
                tNcliente.Text = sClienteID;
                textDenom.Text = sDenominacion;
                
                 //, CasoFechaRecepcion, CasoNumConcedida, CasoNumeroExpedienteLargo, CasoTitulo
                //, , , CasoDenominacion, ClienteId, ClienteNombre, InteresadoNombre;
                textConcecion.Text = sCasoFechaConcesion;
                textVigencia.Text = CasoFechaVigencia;
                textNumexp.Text = CasoNumeroExpediente;
                textIdioma.Text = IdiomaDescripcion;
                textTipo.Text = sTiposolicitud;
                textEstatus.Text = sEstatuscasoDesc;
            }
            else
            {
                MessageBox.Show("Debe ingresar un número de caso");
            }
        }
        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("ffff");
        }
        public static String getfecha(DateTime value)
        {
            //return value.ToString("yy-MM-dd-HH:mm:ss");
            return value.ToString("dd-MM-yyyy");
        }


        //private void createPDF(String tempfilename, String newPath)
        //{
        //    //MessageBox.Show("PDF");
        //    BaseFont fuenteText = FontFactory.GetFont(FontFactory.HELVETICA, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL).BaseFont;
        //    String timeStamp = GetTimestamp(DateTime.Now);
        //    string newFileName = tempfilename + "_" + timeStamp + "_" + ".pdf";
        //    newPath = System.IO.Path.Combine(newPath, newFileName);
        //    //int sizeofwrite = random.Next(1, 10000);

        //    if (!System.IO.File.Exists(newPath))
        //    {
        //        // step 1: creation of a document-object
        //        iTextSharp.text.Document myDocument = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 0,0,0,0);

        //        try
        //        {
        //            // step 2:
        //            // Now create a writer that listens to this doucment and writes the document to desired Stream.
        //            PdfWriter pdfwriter = iTextSharp.text.pdf.PdfWriter.GetInstance(myDocument, new FileStream(newPath, FileMode.Create, FileAccess.Write, FileShare.None));
        //            //PdfWriter pdfw = PdfWriter.GetInstance(myDocument, new FileStream("C:\Reporte\Cedes_constancia_deposito__2.pdf",FileMode.Create, FileAccess.Write, FileShare.None));
        //            // step 3:  Open the document now using
        //            myDocument.Open();

        //            //agregamos la imagen de encabezado
        //            //myDocument.Add(new Paragraph("JPG"));
        //            //string url = @"C:\facturador\img\encabezadoking.jpg";

        //            iTextSharp.text.Image tif = iTextSharp.text.Image.GetInstance(@"C:\facturador\img\encabezadoking.jpg");
        //            tif.ScalePercent(30f);
        //            tif.SetAbsolutePosition(myDocument.PageSize.Width/12,
        //                  750);

        //            myDocument.Add(tif);


        //            PdfContentByte recNoSolicitud = pdfwriter.DirectContent;
        //            recNoSolicitud.SetLineWidth(1);
        //            recNoSolicitud.SetColorStroke(BaseColor.BLACK);
        //            //recNoSolicitud.Rectangle(30, 792, 535, -120);
        //            //recNoSolicitud.SetLineWidth(0.6);
        //            //recNoSolicitud.Stroke();
        //            //recNoSolicitud.Rectangle(30, 662, 535, -290);
        //            //recNoSolicitud.Stroke();
        //            //recNoSolicitud.Rectangle(30, 662, 535, -40);
        //            //recNoSolicitud.Stroke();
        //            //recNoSolicitud.Rectangle(330, 662, 80, -290);
        //            //recNoSolicitud.Stroke();
        //            //recNoSolicitud.Rectangle(410, 662, 80, -290);
        //            //recNoSolicitud.Stroke();
        //            //recNoSolicitud.Rectangle(490, 662, 75, -290);
        //            //recNoSolicitud.Stroke();

        //            //String[] serviciodividido = new String[10]; ;
        //            //int rows = comboBox1.Text.Length;
        //            //if (comboBox1.Text.Length > 106)
        //            //{

        //            //    for (int x = 0; x <= rows / 106; x++)
        //            //    {
        //            //        if (x.Equals(0))
        //            //        {
        //            //            serviciodividido[x] = comboBox1.Text.Substring(0, 106);
        //            //        }
        //            //        else
        //            //        {
        //            //            if (x.Equals(1))
        //            //            {
        //            //                serviciodividido[x] = comboBox1.Text.Substring(107, 213);
        //            //            }
        //            //            else
        //            //            {
        //            //                if (x.Equals(2))
        //            //                {
        //            //                    serviciodividido[x] = comboBox1.Text.Substring(214, 320);
        //            //                }
        //            //                else
        //            //                {
        //            //                    serviciodividido[x] = comboBox1.Text.Substring(321, 426);
        //            //                }
        //            //            }
        //            //        }

        //            //    }
        //            //}
        //            PdfContentByte cb = pdfwriter.DirectContent;
        //            cb.RoundRectangle(30f, 592f, 535f, 120f, 20f);
        //            cb.RoundRectangle(30f, 236f, 535f, 356f, 20f);
        //            //y permanece
        //            cb.RoundRectangle(350f, 142f, 215f, 94f, 20f);
        //            cb.RoundRectangle(30f, 142f, 315f, 64f, 20f);
        //            //cb.LineTo(35f, 0f);
        //            cb.Stroke();

        //            //linea vetical
        //            cb.MoveTo(455, 712);
        //            cb.LineTo(455, 142);

        //            //lineas uno primer rectangulo
        //            cb.MoveTo(30, 685);
        //            cb.LineTo(565, 685);

        //            //lineas uno segundo rectangulo
        //            cb.MoveTo(30, 557);
        //            cb.LineTo(565, 557);


        //            //lineas dos primer rectangulo
        //            cb.MoveTo(455, 660);
        //            cb.LineTo(565, 660);

        //            //lineas tres primer rectangulo
        //            cb.MoveTo(455, 620);
        //            cb.LineTo(565, 620);

        //            //lineas uno tercer rectangulo
        //            cb.MoveTo(350, 207);
        //            cb.LineTo(565, 207);

        //            //lineas dos tercer rectangulo
        //            cb.MoveTo(350, 172);
        //            cb.LineTo(565, 172);

                    


                    

        //            cb.Stroke();
        //            cb.BeginText();
        //            cb.SetFontAndSize(fuenteText, 11);
        //            cb.SetColorFill(iTextSharp.text.BaseColor.DARK_GRAY);
        //            //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "FORMATO ELECTRÓNICO DE PAGOS POR SERVICIO", PageSize.A4.Width/4, PageSize.A4.Height - 34, 0);
        //            //cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, titulo, PageSize.A4.Width / 2, PageSize.A4.Height - 34, 0);
        //            //Agregamos titulos de la plantilla
        //            cb.SetFontAndSize(fuenteText, 10);
        //            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "FOLIO", 475, PageSize.A4.Height - 150, 0);
        //            //AQUÍ PODEMOS AGREGAR EL FOLIO DE LA FACTURA
        //            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, tNumCaso.Text, 475, PageSize.A4.Height - 177, 0);
        //            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "FECHA  ", 475, 635, 0);
        //            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "" + getfecha(DateTime.Now), 475, 605, 0);

        //            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "IMPORTE ", 475, PageSize.A4.Height - 275, 0);

        //            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "CLIENTE ", PageSize.A4.Width/3, PageSize.A4.Height - 150, 0);
        //            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "DESCRIPCIÓN ", PageSize.A4.Width / 3, PageSize.A4.Height - 275, 0);

        //            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "SUBTOTAL ", 380, 218, 0);
        //            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "I.V.A.  ", 400, 185, 0);
        //            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "TOTAL ", 400, 152, 0);

        //            //AQUÍ AGREAMOS LOS VALORES 
        //            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "$" + 1000, 530, 218, 0);
        //            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "$" + 160, 530, 185, 0);
        //            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "$" + 1160, 530, 152, 0);


        //            cb.SetFontAndSize(fuenteText, 8);
        //            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Servicio: " + tNumservicio.Text, 37, PageSize.A4.Height - 320, 0);
        //            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Interesado: ", 37, PageSize.A4.Height - 529, 0);
        //            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Caso: " + tNumCaso.Text, 37, PageSize.A4.Height - 552, 0);
        //            cb.SetFontAndSize(fuenteText, 6);
        //            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "" + comboServicio.Text, 37, PageSize.A4.Height - 331, 0);
        //            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "" + interesado, 37, PageSize.A4.Height - 541, 0);
                    
        //            //float iPosiciony = PageSize.A4.Height - 251;
        //            //for (int x = 0; x <= rows / 106; x++) {
        //            //    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "" + comboBox1.Text, 57, iPosiciony, 0);
        //            //    iPosiciony += 11;
        //            //}
                    
                    
        //            //DATOS DEL CLIENTE
        //            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "WW" + cClientebox.Text, 37, PageSize.A4.Height - 190, 0);
        //            cb.SetFontAndSize(fuenteText, 9);
                    
        //            cb.EndText();
        //            pdfwriter.Flush();
        //            //for (int x = 0; x < sizeofwrite; x++)
        //            //{
        //            //    Byte[] b = new Byte[1];
        //            //    random.NextBytes(b);

        //            //    // step 4: Now add some contents to the document
        //            //    myDocument.Add(new iTextSharp.text.Paragraph(b[0].ToString()));
        //            //}
        //            //myDocument.Add(new iTextSharp.text.Paragraph("AGREGARMOS CONTENIDO TITULO"));
        //            //myDocument.Add(new iTextSharp.text.Paragraph("AGREGARMOS CONTENIDO TITULO DOS"));
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
        //            MessageBox.Show("El archivo fué creado en: " + newPath.ToString() + System.Environment.NewLine);
        //            myDocument.Close();
        //            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //            //myProcess.StartInfo.Verb = "print";
        //            myProcess.StartInfo.CreateNoWindow = true;
        //            //URL del documento
        //            string Val = ""+newPath;
        //            Val.Replace("\\\\", "\\");
        //            myProcess.StartInfo.FileName = Val;
        //            myProcess.Start();
        //            myProcess.WaitForExit(1000);
        //            //myProcess.CloseMainWindow();
        //            //myProcess.Close();
                    
        //            //textBox4.AppendText("Created file: " + newPath.ToString() + System.Environment.NewLine + "SIZE: " + sizeofwrite.ToString() + "\r\n\r\n");
        //        }
        //    }
        //}

        private void button2_Click(object sender, EventArgs e)
        {
            if (!tNumservicio.Text.Equals("") && !tNcliente.Text.Equals("") && !cClientebox.Text.Equals("") && !tNumCaso.Text.Equals("") && !comboServicio.Text.Equals("") && !textTitulo.Text.Equals(""))
            {
                //createPDF("caso_" + tNumCaso.Text, @"C:\facturador\archivos\");
            }
            else {
                MessageBox.Show("Verique que los campos requeridos estén completos");
            }
            
        }

        private void comboServicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            String[] idservicio = comboServicio.Text.Split('-');
            tNumservicio.Text = idservicio[0];

        }

        private void salirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
            capt.Close();
            login.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            capt.Show();
        }

     


        

        

    }
}
