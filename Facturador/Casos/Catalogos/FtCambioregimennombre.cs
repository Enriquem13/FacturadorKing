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
using Word = Microsoft.Office.Interop.Word;

namespace Facturador
{
    public partial class FCambioReginemNombreDenominacionRazon : Form
    {
        public Form1 loguin;
        public String sgCasooid;
        funcionesdicss dicssdfunctions = new funcionesdicss();
        fTmarcas fGtmarcas;
        public FCambioReginemNombreDenominacionRazon(Form1 login, String sCasoid, fTmarcas formmarcas)
        {
            loguin = login;
            sgCasooid = sCasoid;
            fGtmarcas = formmarcas;
            InitializeComponent();
        }
        private void BT_cancelarsolicitud_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TB_anexo2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void TB_anexo3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void TB_anexo4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void TB_anexo5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void TB_anexo7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void TB_anexo8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void TB_anexo9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void BT_generar_solicitud_Click(object sender, EventArgs e)
        {
            try
            {
                //DateTime Hoy = DateTime.Today;
                //string dd_fecha = Hoy.ToString("dd");
                //string mm_fecha = Hoy.ToString("MM");
                //string yyyy_fecha = Hoy.ToString("yyyy");
                //try
                //{
                //    //copiamos la plantilla a un archivo temporal
                //    configuracionfiles confilepth = new configuracionfiles();
                //    confilepth.configuracionfilesinicio();
                //    String sruta_plantilla = confilepth.sFileupload + @"\formatosconfigurables\IMPI-00-006_1.docx";
                //    Random r = new Random();
                //    int srandonm = r.Next(9, 999);
                //    String sArchivogenerado = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\casosking\" + "Nuevos_formatos\\IMPI-00-002_B_copia_" + srandonm + ".docx";
                //    File.Copy(sruta_plantilla, sArchivogenerado);
                //    //abrimos el archivo temporal y lo reemplzamos con los datos
                //    Word.Application application = new Word.Application();
                //    Word.Document document = application.Documents.Open(sArchivogenerado);
                //    //agregamos la fecha
                //    document.Bookmarks["dd_fecha"].Select();
                //    application.Selection.TypeText(dd_fecha);

                //    document.Bookmarks["mm_fecha"].Select();
                //    application.Selection.TypeText(mm_fecha);

                //    document.Bookmarks["yyyy_fecha"].Select();
                //    application.Selection.TypeText(yyyy_fecha);
                //    //validamos que tipo de solicitud es
                //    switch (fGtmarcas.sTipoSolicitudId)
                //    {
                //        case "7":
                //            {
                //                document.Bookmarks["registro_marca"].Select();
                //                application.Selection.TypeText("X");
                //            } break;
                //        case "8":
                //            {
                //                document.Bookmarks["PublicacionNomComerc"].Select();
                //                application.Selection.TypeText("X");
                //            } break;
                //        case "9":
                //            {
                //                document.Bookmarks["Avisocomercial"].Select();
                //                application.Selection.TypeText("X");
                //            } break;
                //        //faltan casos que si existen en el formato
                //        //case "77": {//existe en la base como nombre de dominio
                //        //    //document.Bookmarks["Nombrededominio"].Select();// no existe en el formato
                //        //    //application.Selection.TypeText("X");
                //        //} break;
                //    }

                //    //agregamos la clase y la descripción
                //    String sProducos = fGtmarcas.tbclase.Text.Trim();
                //    if (sProducos.Length < 3)
                //    {
                //        switch (sProducos.Length)
                //        {
                //            case 1:
                //                {
                //                    document.Bookmarks["Clase1"].Select();
                //                    application.Selection.TypeText("0");
                //                    document.Bookmarks["Clase2"].Select();
                //                    application.Selection.TypeText(sProducos);
                //                } break;

                //            case 2:
                //                {
                //                    document.Bookmarks["Clase1"].Select();
                //                    application.Selection.TypeText(sProducos.Substring(0, 1));
                //                    document.Bookmarks["Clase2"].Select();
                //                    application.Selection.TypeText(sProducos.Substring(1, 1));
                //                } break;
                //        }
                //    }
                //    else
                //    {
                //        document.Bookmarks["Clase1"].Select();
                //        application.Selection.TypeText(sProducos.Substring(0, 1));
                //        document.Bookmarks["Clase2"].Select();
                //        application.Selection.TypeText(sProducos.Substring(1, 1));
                //        document.Bookmarks["anex_datrenovacion"].Select();
                //        application.Selection.TypeText("X");
                //        //datrenovacion
                //    }
                //    //obtenemos la descripcion del producto o productos
                //    if (fGtmarcas.lvProductos.Items.Count > 0)
                //    {
                //        document.Bookmarks["Clase_descripcion"].Select();
                //        if (fGtmarcas.lvProductos.Items[0].SubItems[2].Text.Length > 758)
                //        {
                //            application.Selection.TypeText(RemoveLineEndings(fGtmarcas.lvProductos.Items[0].SubItems[2].Text.Substring(0, 758)));
                //        }
                //        else
                //        {
                //            application.Selection.TypeText(RemoveLineEndings(fGtmarcas.lvProductos.Items[0].SubItems[2].Text));
                //        }
                //    }
                //    //numero de registro
                //    if (fGtmarcas.tbDNumeroReg.Text != "")
                //    {
                //        document.Bookmarks["Numeroderegistro"].Select();
                //        application.Selection.TypeText(RemoveLineEndings(fGtmarcas.tbDNumeroReg.Text));
                //    }

                //    //lvinteresados
                //    //validamos si la persona es Persona fisica o persona Moral para guardar los datos
                //    if (fGtmarcas.lvinteresados.Items.Count > 0)
                //    {
                //        if (fGtmarcas.lvinteresados.Items[0].SubItems[7].Text != "")
                //        {
                //            switch (fGtmarcas.lvinteresados.Items[0].SubItems[7].Text)
                //            {
                //                case "ME":
                //                    {
                //                        document.Bookmarks["PM_RFC"].Select();
                //                        application.Selection.TypeText(fGtmarcas.rfc_cambintermed2);

                //                        document.Bookmarks["PM_Razonsocial"].Select();
                //                        application.Selection.TypeText(fGtmarcas.rasonsoc_cambint2);

                //                        document.Bookmarks["PM_Nacionalidad"].Select();
                //                        application.Selection.TypeText(fGtmarcas.nacionalidad_1);

                //                        document.Bookmarks["PM_telefono"].Select();
                //                        application.Selection.TypeText("(52 55)-55 84-5014");

                //                        if (fGtmarcas.lvinteresados.Items.Count > 1)
                //                        {
                //                            document.Bookmarks["PM_anexo_x"].Select();
                //                            application.Selection.TypeText("X");
                //                            //generar el anexo de interesados
                //                        }
                //                    } break;
                //                case "FE":
                //                    {
                //                        document.Bookmarks["PF_curp"].Select();
                //                        application.Selection.TypeText(fGtmarcas.curp_1);

                //                        document.Bookmarks["PF_Nombres"].Select();
                //                        application.Selection.TypeText(fGtmarcas.nombre_1);

                //                        document.Bookmarks["PF_Primerapellido"].Select();
                //                        application.Selection.TypeText(fGtmarcas.appl1_1);

                //                        document.Bookmarks["PF_segundoapellido"].Select();
                //                        application.Selection.TypeText(fGtmarcas.appl2_1);

                //                        document.Bookmarks["PF_Nacionalidad"].Select();
                //                        application.Selection.TypeText(fGtmarcas.nacionalidad_1);

                //                        document.Bookmarks["PFTelefonoladanumext"].Select();
                //                        application.Selection.TypeText("(52 55)-55 84-5014");

                //                        if (fGtmarcas.lvinteresados.Items.Count > 1)
                //                        {
                //                            document.Bookmarks["PF_anexo_x"].Select();
                //                            application.Selection.TypeText("X");
                //                            //generar el anexo de interesados
                //                        }

                //                    } break;
                //                case "MN":
                //                    {
                //                        document.Bookmarks["PM_RFC"].Select();
                //                        application.Selection.TypeText(fGtmarcas.rfc_cambintermed2);

                //                        document.Bookmarks["PM_Razonsocial"].Select();
                //                        application.Selection.TypeText(fGtmarcas.rasonsoc_cambint2);

                //                        document.Bookmarks["PM_Nacionalidad"].Select();
                //                        application.Selection.TypeText(fGtmarcas.nacionalidad_1);

                //                        document.Bookmarks["PM_telefono"].Select();
                //                        application.Selection.TypeText("(52 55)-55 84-5014");

                //                        if (fGtmarcas.lvinteresados.Items.Count > 1)
                //                        {
                //                            document.Bookmarks["PM_anexo_x"].Select();
                //                            application.Selection.TypeText("X");
                //                            //generar el anexo de interesados
                //                        }

                //                    } break;
                //                case "FN":
                //                    {
                //                        document.Bookmarks["PF_curp"].Select();
                //                        application.Selection.TypeText(fGtmarcas.curp_1);

                //                        document.Bookmarks["PF_Nombres"].Select();
                //                        application.Selection.TypeText(fGtmarcas.nombre_1);

                //                        document.Bookmarks["PF_Primerapellido"].Select();
                //                        application.Selection.TypeText(fGtmarcas.appl1_1);

                //                        document.Bookmarks["PF_segundoapellido"].Select();
                //                        application.Selection.TypeText(fGtmarcas.appl2_1);

                //                        document.Bookmarks["PF_Nacionalidad"].Select();
                //                        application.Selection.TypeText(fGtmarcas.nacionalidad_1);

                //                        document.Bookmarks["PFTelefonoladanumext"].Select();
                //                        application.Selection.TypeText("(52 55)-55 84-5014");

                //                        if (fGtmarcas.lvinteresados.Items.Count > 1)
                //                        {
                //                            document.Bookmarks["PF_anexo_x"].Select();
                //                            application.Selection.TypeText("X");
                //                            //generar el anexo de interesados
                //                        }
                //                    } break;
                //            }
                //            //viene de la base por que son datos de king
                //        }
                //    }
                //    //consultamos la cdireccion
                //    conect con_3 = new conect();
                //    String squerydatoficinas = "select * from datosoficina limit 1;";
                //    MySqlDataReader resp_datofi = con_3.getdatareader(squerydatoficinas);
                //    while (resp_datofi.Read())
                //    {
                //        String OficinaCP = dicssdfunctions.validareader("OficinaCP", "id_datosoficina", resp_datofi).Text;
                //        String OficinaCalle = dicssdfunctions.validareader("OficinaCalle", "id_datosoficina", resp_datofi).Text;
                //        String OficinaNumExt = dicssdfunctions.validareader("OficinaNumExt", "id_datosoficina", resp_datofi).Text;
                //        String OficinaNumInt = dicssdfunctions.validareader("OficinaNumInt", "id_datosoficina", resp_datofi).Text;
                //        String OficinaColonia = dicssdfunctions.validareader("OficinaColonia", "id_datosoficina", resp_datofi).Text;
                //        String OficinaMunicipio = dicssdfunctions.validareader("OficinaMunicipio", "id_datosoficina", resp_datofi).Text;
                //        String OficinaEstado = dicssdfunctions.validareader("OficinaEstado", "id_datosoficina", resp_datofi).Text;
                //        String OficinaPaisId = dicssdfunctions.validareader("OficinaPaisId", "id_datosoficina", resp_datofi).Text;
                //        String OficinaTelefono = dicssdfunctions.validareader("OficinaTelefono", "id_datosoficina", resp_datofi).Text;
                //        String OficinaCorreo = dicssdfunctions.validareader("OficinaCorreo", "id_datosoficina", resp_datofi).Text;
                //        String ApoderadoNonbre = dicssdfunctions.validareader("ApoderadoNonbre", "id_datosoficina", resp_datofi).Text;
                //        String ApoderadoApellidoPat = dicssdfunctions.validareader("ApoderadoApellidoPat", "id_datosoficina", resp_datofi).Text;
                //        String ApoderadoApellidoMat = dicssdfunctions.validareader("ApoderadoApellidoMat", "id_datosoficina", resp_datofi).Text;
                //        String AutorizadoNombre = dicssdfunctions.validareader("AutorizadoNombre", "id_datosoficina", resp_datofi).Text;
                //        String AutorizadApellidoPat = dicssdfunctions.validareader("AutorizadApellidoPat", "id_datosoficina", resp_datofi).Text;
                //        String AutorizadoApellidoMat = dicssdfunctions.validareader("AutorizadoApellidoMat", "id_datosoficina", resp_datofi).Text;


                //        document.Bookmarks["CodPostal_notif"].Select();
                //        application.Selection.TypeText(OficinaCP);

                //        document.Bookmarks["Calle_notif"].Select();
                //        application.Selection.TypeText(OficinaCalle);

                //        document.Bookmarks["Num_ext_notific"].Select();
                //        application.Selection.TypeText(OficinaNumExt);

                //        document.Bookmarks["Colonia_notifica"].Select();
                //        application.Selection.TypeText(OficinaColonia);

                //        document.Bookmarks["Num_int_notific"].Select();
                //        application.Selection.TypeText(OficinaNumInt);

                //        document.Bookmarks["Municipio_notific"].Select();
                //        application.Selection.TypeText(OficinaMunicipio);

                //        document.Bookmarks["Localidad_notific"].Select();
                //        application.Selection.TypeText(OficinaMunicipio);

                //        document.Bookmarks["EntidadFederativa_no"].Select();
                //        application.Selection.TypeText("");

                //        document.Bookmarks["Entrecalles_notific"].Select();
                //        application.Selection.TypeText("");

                //        document.Bookmarks["Correo_notific"].Select();
                //        application.Selection.TypeText(OficinaCorreo);


                //        //CodPostal_notif
                //        //Calle_notif
                //        //Num_ext_notific
                //        //Colonia_notifica
                //        //Num_int_notific
                //        //Municipio_notific
                //        //Localidad_notific

                //        //EntidadFederativa_no
                //        //Entrecalles_notific
                //        //Calleposterior_notif
                //        //Correo_notific
                //    }
                //    resp_datofi.Close();
                //    con_3.Cerrarconexion();
                //    //validamos los anexos
                //    //checkBox1
                //    //textBox2
                //    if (checkBox1.Checked)
                //    {
                //        document.Bookmarks["anexo_comprdepago"].Select();
                //        application.Selection.TypeText("X");

                //        document.Bookmarks["nhojas_anexo1"].Select();
                //        application.Selection.TypeText(textBox2.Text);
                //        //textBox2
                //    }

                //    if (checkBox_2.Checked)
                //    {
                //        document.Bookmarks["anexo_eltitulardecla"].Select();
                //        application.Selection.TypeText("X");

                //        document.Bookmarks["anexo_2"].Select();
                //        application.Selection.TypeText(tbNumhanexo2.Text);
                //        //textBox2
                //    }

                //    if (checkBox_3.Checked)
                //    {
                //        document.Bookmarks["anexo_datgenerpers_x"].Select();
                //        application.Selection.TypeText("X");

                //        document.Bookmarks["anexo_3"].Select();
                //        application.Selection.TypeText(tbNuhjAnexo3.Text);
                //        //textBox2
                //    }

                //    if (checkBox_4.Checked)
                //    {
                //        document.Bookmarks["anexo_acredita_perso"].Select();
                //        application.Selection.TypeText("X");

                //        document.Bookmarks["anexo_4"].Select();
                //        application.Selection.TypeText(tbNuhojanexo4.Text);
                //        //textBox2
                //    }

                //    if (checkBox_5.Checked)
                //    {
                //        document.Bookmarks["anexo_constanciadein"].Select();
                //        application.Selection.TypeText("X");

                //        document.Bookmarks["anexo_5"].Select();
                //        application.Selection.TypeText(tbNumhojAnexo5.Text);
                //        //textBox2
                //    }

                //    if (checkBox_6.Checked)
                //    {
                //        document.Bookmarks["anexo_tradicciondoc"].Select();
                //        application.Selection.TypeText("X");

                //        document.Bookmarks["anexo_6"].Select();
                //        application.Selection.TypeText(tbMumanex6.Text);
                //        //textBox2
                //    }
                    //checkBox_2
                    //tbNumhanexo2
                    //
                    //
                    //
                    //
                    //checkBox_5
                    //
                    //checkBox_6
                    //

                    //eliminamos lo marcadores extras
                    //String [] sArrays = new String[document.Bookmarks.Count];
                    //for (int i = 1; i <= document.Bookmarks.Count; i++)
                    //{
                    //    //obtenemos los nombres para despues obtener los remplazos por nombre, no por indice de nonbre porque ya no existe
                    //    object objI = i;
                    //    sArrays[i - 1] = document.Bookmarks.get_Item(ref objI).Name;
                    // }
                    //for (int x = 0; x < sArrays.Length; x++)
                    //{
                    //    document.Bookmarks[sArrays[x]].Select();
                    //    //application.Selection.Delete();
                    //    application.Selection.TypeText(" ");
                    //}
                    //application.Visible = true;
                    //document.Save();
                    //application.Quit();
                //}
                //catch (Exception E)
                //{
                //    Console.Write("Error: " + E + "\n");
                //}
            }
            catch (Exception E)
            {
                new filelog(loguin.sId, E.ToString());
            }
        }
     }
}
