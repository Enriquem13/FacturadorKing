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
using Word = Microsoft.Office.Interop.Word;
namespace Facturador
{//
    //Solicitud de Inscripción de Licencia de Uso o Franquicia
    //IMPI-00-004.doc
    public partial class FLicenciaFranquicia : Form
    {
        public Form1 loguin;
        public String sgCasooid;
        funcionesdicss dicssdfunctions = new funcionesdicss();
        fTmarcas fGtmarcas;
        public String OficinaCP = "";
        public String OficinaCalle = "";
        public String OficinaNumExt = "";
        public String OficinaNumInt = "";
        public String OficinaColonia = "";
        public String OficinaMunicipio = "";
        public String OficinaEstado = "";
        public String OficinaPaisId = "";
        public String OficinaTelefono = "";
        public String OficinaCorreo = "";
        public String ApoderadoNonbre = "";
        public String ApoderadoApellidoPat = "";
        public String ApoderadoApellidoMat = "";
        public String AutorizadoNombre = "";
        public String AutorizadApellidoPat = "";
        public String AutorizadoApellidoMat = "";
        public FLicenciaFranquicia(Form1 login, String sCasoid, fTmarcas formmarcas)
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

        private void TB_anexo6_KeyPress(object sender, KeyPressEventArgs e)
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
                DateTime Hoy = DateTime.Today;
                string dd_fecha = Hoy.ToString("dd");
                string mm_fecha = Hoy.ToString("MM");
                string yyyy_fecha = Hoy.ToString("yyyy");
                try
                {
                    //copiamos la plantilla a un archivo temporal
                    configuracionfiles confilepth = new configuracionfiles();
                    confilepth.configuracionfilesinicio();
                    DateTime fecha = DateTime.Now;
                    String sFehacss = fecha.ToString("HHmmss");
                    String carpeta = "C:\\Formatos_CasosKing";
                    //si no existe la carpeta temporal la creamos
                    if (!(Directory.Exists(carpeta)))
                    {
                        Directory.CreateDirectory(carpeta);
                    }
                    String sruta_plantilla = confilepth.sFileupload + @"\formatosconfigurables\IMPI-00-004_1.docx";
                    Random r = new Random();
                    int srandonm = r.Next(9, 9999);
                    //String sArchivogenerado = carpeta + "\\" + fGtmarcas.sCasoId + " IMPI-00-004_" + sFehacss + "licenciadeusoofranquicia" + ".docx";
                    String sArchivogenerado = dicssdfunctions.carpeta + "\\" + fGtmarcas.sCasoId + " IMPI-00-004_" + dicssdfunctions.sGetfechehhmmss() + "licenciadeusoofranquicia" + ".docx";
                    //String sArchivogenerado = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\casosking\" + "Nuevos_formatos\\IMPI-00-004" + srandonm + ".docx";
                    File.Copy(sruta_plantilla, sArchivogenerado);

                    //abrimos el archivo temporal y lo reemplzamos con los datos
                    Word.Application application = new Word.Application();
                    Word.Document document = application.Documents.Open(sArchivogenerado);

                    /*Agergamos la direccion de notificaciones*/
                    conect con_3 = new conect();
                    String squerydatoficinas = "select * from datosoficina limit 1;";
                    MySqlDataReader resp_datofi = con_3.getdatareader(squerydatoficinas);
                    while (resp_datofi.Read())
                    {
                        OficinaCP = dicssdfunctions.validareader("OficinaCP", "id_datosoficina", resp_datofi).Text;
                        OficinaCalle = dicssdfunctions.validareader("OficinaCalle", "id_datosoficina", resp_datofi).Text;
                        OficinaNumExt = dicssdfunctions.validareader("OficinaNumExt", "id_datosoficina", resp_datofi).Text;
                        OficinaNumInt = dicssdfunctions.validareader("OficinaNumInt", "id_datosoficina", resp_datofi).Text;
                        OficinaColonia = dicssdfunctions.validareader("OficinaColonia", "id_datosoficina", resp_datofi).Text;
                        OficinaMunicipio = dicssdfunctions.validareader("OficinaMunicipio", "id_datosoficina", resp_datofi).Text;
                        OficinaEstado = dicssdfunctions.validareader("OficinaEstado", "id_datosoficina", resp_datofi).Text;
                        OficinaPaisId = dicssdfunctions.validareader("OficinaPaisId", "id_datosoficina", resp_datofi).Text;
                        OficinaTelefono = dicssdfunctions.validareader("OficinaTelefono", "id_datosoficina", resp_datofi).Text;
                        OficinaCorreo = dicssdfunctions.validareader("OficinaCorreo", "id_datosoficina", resp_datofi).Text;
                        ApoderadoNonbre = dicssdfunctions.validareader("ApoderadoNonbre", "id_datosoficina", resp_datofi).Text;
                        ApoderadoApellidoPat = dicssdfunctions.validareader("ApoderadoApellidoPat", "id_datosoficina", resp_datofi).Text;
                        ApoderadoApellidoMat = dicssdfunctions.validareader("ApoderadoApellidoMat", "id_datosoficina", resp_datofi).Text;
                        AutorizadoNombre = dicssdfunctions.validareader("AutorizadoNombre", "id_datosoficina", resp_datofi).Text;
                        AutorizadApellidoPat = dicssdfunctions.validareader("AutorizadApellidoPat", "id_datosoficina", resp_datofi).Text;
                        AutorizadoApellidoMat = dicssdfunctions.validareader("AutorizadoApellidoMat", "id_datosoficina", resp_datofi).Text;
                    }
                    resp_datofi.Close();
                    con_3.Cerrarconexion();

                    //agregamos la fecha
                    document.Bookmarks["dd_fecha"].Select();
                    application.Selection.TypeText(dd_fecha);

                    document.Bookmarks["mm_fecha"].Select();
                    application.Selection.TypeText(mm_fecha);

                    document.Bookmarks["yyyy_fecha"].Select();
                    application.Selection.TypeText(yyyy_fecha);
                    
                    application.Visible = true;

                    //numexpedientesol
                    if (fGtmarcas.tbDExpediente.Text!="")
                    {
                        document.Bookmarks["numexpedientesol"].Select();
                        application.Selection.TypeText(fGtmarcas.tbDExpediente.Text);

                    }
                    /*Tipo de inscripcion que solicita */
                    if (RB_tipoinscripcion1.Checked)
                    {
                        document.Bookmarks["licenciadeuso"].Select();
                        application.Selection.TypeText("X");

                    }

                    if (RB_tipoinscripcion2.Checked)
                    {
                        document.Bookmarks["franquicia"].Select();
                        application.Selection.TypeText("X");

                    }
                    /*FIN se solicita inscripcion*/


                    /*Revisamos Tipo de inscripción */
                    if (RB_presentada1.Checked)
                    {
                        document.Bookmarks["titularesfranquisi"].Select();
                        application.Selection.TypeText("X");
                        //aqui consultamos los datos de la direcci+on del interesado cedentes
                        //lvinteresados
                        //validamos si la persona es Persona fisica o persona Moral para guardar los datos
                        if (fGtmarcas.lvinteresados.Items.Count > 0)
                        {
                            if (fGtmarcas.lvinteresados.Items[0].SubItems[7].Text != "")
                            {
                                switch (fGtmarcas.lvinteresados.Items[0].SubItems[7].Text)
                                {
                                    case "Moral Extranjera":
                                        {
                                            document.Bookmarks["PM_rfc_cambintermed1"].Select();
                                            application.Selection.TypeText(fGtmarcas.rfc_cambintermed2);

                                            document.Bookmarks["PM_rasonsoc_cambint1"].Select();
                                            application.Selection.TypeText(fGtmarcas.rasonsoc_cambint2);


                                            document.Bookmarks["PM_telefono_1"].Select();
                                            application.Selection.TypeText(OficinaTelefono);

                                            if (fGtmarcas.lvinteresados.Items.Count > 1)
                                            {
                                                document.Bookmarks["PM_anexo_1"].Select();
                                                application.Selection.TypeText("X");
                                                //generar el anexo de interesados
                                            }
                                        } break;
                                    case "Física Extranjera":
                                        {
                                            document.Bookmarks["PF_curp_cambinterme1"].Select();
                                            application.Selection.TypeText(fGtmarcas.curp_1);

                                            document.Bookmarks["PF_nombre_cambinter1"].Select();
                                            application.Selection.TypeText(fGtmarcas.nombre_1);

                                            document.Bookmarks["PF_appl1_cambinter1"].Select();
                                            application.Selection.TypeText(fGtmarcas.appl1_1);

                                            document.Bookmarks["PF_appl2_cambinter1"].Select();
                                            application.Selection.TypeText(fGtmarcas.appl2_1);


                                            document.Bookmarks["PF_telefono_1"].Select();
                                            application.Selection.TypeText(OficinaTelefono);

                                            if (fGtmarcas.lvinteresados.Items.Count > 1)
                                            {
                                                document.Bookmarks["anexo_PF_1"].Select();
                                                application.Selection.TypeText("X");
                                                //generar el anexo de interesados
                                            }

                                        } break;
                                    case "Moral Nacional":
                                        {
                                            document.Bookmarks["PM_rfc_cambintermed1"].Select();
                                            application.Selection.TypeText(fGtmarcas.rfc_cambintermed2);

                                            document.Bookmarks["PM_rasonsoc_cambint1"].Select();
                                            application.Selection.TypeText(fGtmarcas.rasonsoc_cambint2);


                                            document.Bookmarks["PM_telefono_1"].Select();
                                            application.Selection.TypeText(OficinaTelefono);

                                            if (fGtmarcas.lvinteresados.Items.Count > 1)
                                            {
                                                document.Bookmarks["PM_anexo_1"].Select();
                                                application.Selection.TypeText("X");
                                                //generar el anexo de interesados
                                            }

                                        } break;
                                    case "Física Nacional":
                                        {
                                            document.Bookmarks["PF_curp_cambinterme1"].Select();
                                            application.Selection.TypeText(fGtmarcas.curp_1);

                                            document.Bookmarks["PF_nombre_cambinter1"].Select();
                                            application.Selection.TypeText(fGtmarcas.nombre_1);

                                            document.Bookmarks["PF_appl1_cambinter1"].Select();
                                            application.Selection.TypeText(fGtmarcas.appl1_1);

                                            document.Bookmarks["PF_appl2_cambinter1"].Select();
                                            application.Selection.TypeText(fGtmarcas.appl2_1);


                                            document.Bookmarks["PF_telefono_1"].Select();
                                            application.Selection.TypeText(OficinaTelefono);

                                            if (fGtmarcas.lvinteresados.Items.Count > 1)
                                            {
                                                document.Bookmarks["anexo_PF_1"].Select();
                                                application.Selection.TypeText("X");
                                                //generar el anexo de interesados
                                            }
                                        } break;
                                }
                                //viene de la base por que son datos de king
                            }
                        }
                    }
                    else
                    {
                        if (RB_presentada2.Checked)
                        {

                            document.Bookmarks["licenciatariosfranqu"].Select();
                            application.Selection.TypeText("X");
                            //aqui consultamos los datos de la direcci+on del interesado cesionarios
                            if (fGtmarcas.lvinteresados.Items.Count > 0)
                            {
                                if (fGtmarcas.lvinteresados.Items[0].SubItems[7].Text != "")
                                {
                                    switch (fGtmarcas.lvinteresados.Items[0].SubItems[7].Text)
                                    {
                                        case "ME":
                                            {
                                                document.Bookmarks["PM_rfc_cambintermed2"].Select();
                                                application.Selection.TypeText(fGtmarcas.rfc_cambintermed2);

                                                document.Bookmarks["PM_rasonsoc_cambint2"].Select();
                                                application.Selection.TypeText(fGtmarcas.rasonsoc_cambint2);

                                                document.Bookmarks["PM_nacional_licfra2"].Select();
                                                application.Selection.TypeText(fGtmarcas.nacionalidad_1);

                                                document.Bookmarks["PM_telefono_2"].Select();
                                                application.Selection.TypeText(OficinaTelefono);

                                                if (fGtmarcas.lvinteresados.Items.Count > 1)
                                                {
                                                    document.Bookmarks["PM_anexo_2"].Select();
                                                    application.Selection.TypeText("X");
                                                    //generar el anexo de interesados
                                                }
                                            } break;
                                        case "FE":
                                            {
                                                document.Bookmarks["PF_curp_cambinterme2"].Select();
                                                application.Selection.TypeText(fGtmarcas.curp_1);

                                                document.Bookmarks["PF_nombre_cambinter2"].Select();
                                                application.Selection.TypeText(fGtmarcas.nombre_1);

                                                document.Bookmarks["PF_appl1_cambinter2"].Select();
                                                application.Selection.TypeText(fGtmarcas.appl1_1);

                                                document.Bookmarks["PF_appl2_cambinter2"].Select();
                                                application.Selection.TypeText(fGtmarcas.appl2_1);

                                                document.Bookmarks["PF_nacional_licfra2"].Select();
                                                application.Selection.TypeText(fGtmarcas.nacionalidad_1);

                                                document.Bookmarks["PF_telefono_2"].Select();
                                                application.Selection.TypeText(OficinaTelefono);

                                                if (fGtmarcas.lvinteresados.Items.Count > 1)
                                                {
                                                    document.Bookmarks["anexo_PF2"].Select();
                                                    application.Selection.TypeText("X");
                                                    //generar el anexo de interesados
                                                }
                                            } break;
                                        case "MN":
                                            {
                                                document.Bookmarks["PM_rfc_cambintermed2"].Select();
                                                application.Selection.TypeText(fGtmarcas.rfc_cambintermed2);

                                                document.Bookmarks["PM_rasonsoc_cambint2"].Select();
                                                application.Selection.TypeText(fGtmarcas.rasonsoc_cambint2);

                                                document.Bookmarks["PM_nacional_licfra2"].Select();
                                                application.Selection.TypeText(fGtmarcas.nacionalidad_1);

                                                document.Bookmarks["PM_telefono_2"].Select();
                                                application.Selection.TypeText(OficinaTelefono);

                                                if (fGtmarcas.lvinteresados.Items.Count > 1)
                                                {
                                                    document.Bookmarks["PM_anexo_2"].Select();
                                                    application.Selection.TypeText("X");
                                                    //generar el anexo de interesados
                                                }
                                            } break;
                                        case "FN":
                                            {
                                                document.Bookmarks["PF_curp_cambinterme2"].Select();
                                                application.Selection.TypeText(fGtmarcas.curp_1);

                                                document.Bookmarks["PF_nombre_cambinter2"].Select();
                                                application.Selection.TypeText(fGtmarcas.nombre_1);

                                                document.Bookmarks["PF_appl1_cambinter2"].Select();
                                                application.Selection.TypeText(fGtmarcas.appl1_1);

                                                document.Bookmarks["PF_appl2_cambinter2"].Select();
                                                application.Selection.TypeText(fGtmarcas.appl2_1);

                                                document.Bookmarks["PF_nacional_licfra2"].Select();
                                                application.Selection.TypeText(fGtmarcas.nacionalidad_1);

                                                document.Bookmarks["PF_telefono_2"].Select();
                                                application.Selection.TypeText(OficinaTelefono);

                                                if (fGtmarcas.lvinteresados.Items.Count > 1)
                                                {
                                                    document.Bookmarks["anexo_PF2"].Select();
                                                    application.Selection.TypeText("X");
                                                    //generar el anexo de interesados
                                                }
                                            } break;
                                    }
                                    //viene de la base por que son datos de king
                                }
                            }

                        }
                        /*Aquí agregamos la dirección si es licenciatario(s) o frnaquiciatario(s)*/
                        
                    }
                    /*Aquí agregamos la dirección si es licenciatario(s) o frnaquiciatario(s)*/
                    document.Bookmarks["calle_1"].Select();
                    application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtmarcas.sDireccionCalle));

                    document.Bookmarks["num_ext_1"].Select();
                    application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtmarcas.sDireccionNumExt));

                    document.Bookmarks["num_int_1"].Select();
                    application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtmarcas.sDireccionNumInt));

                    document.Bookmarks["col_1"].Select();
                    application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtmarcas.sDireccionColonia));

                    document.Bookmarks["cp_1"].Select();
                    application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtmarcas.sDireccionCP));

                    document.Bookmarks["pais_1"].Select();
                    application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtmarcas.sPaisId));
                    /*inicio peticiones adicionales*/

                    if (CheckB1peticionesadicionales.Checked)
                    {
                        document.Bookmarks["anexo_copiascertific"].Select();
                        application.Selection.TypeText("X");

                    }

                    if (CheckB2peticionesadicionales.Checked)
                    {
                        document.Bookmarks["anexo_acreditacionpe"].Select();
                        application.Selection.TypeText("X");

                    }
                    /*fin peticiones adicionales*/

                    /*aquí comienzan los anexos*/
                    if (CheckB1anexosolicitud.Checked)
                    {
                        document.Bookmarks["anexo_comprpago"].Select();
                        application.Selection.TypeText("X");

                        //textBox2
                    }

                    if (CheckB2anexosolicitud.Checked)
                    {
                        document.Bookmarks["anexo_acredpersmanda"].Select();
                        application.Selection.TypeText("X");

                        //textBox2
                    }

                    if (CheckB3anexosolicitud.Checked)
                    {
                        document.Bookmarks["anexo_constanciainsc"].Select();
                        application.Selection.TypeText("X");

                        //textBox2
                    }

                    if (CheckB4anexosolicitud.Checked)
                    {
                        document.Bookmarks["anexo_constlicencuso"].Select();
                        application.Selection.TypeText("X");

                        //textBox2
                    }

                    if (CheckB5anexosolicitud.Checked)
                    {
                        document.Bookmarks["anexo_traducc"].Select();
                        application.Selection.TypeText("X");

                        //textBox2
                    }

                    if (CheckB6anexosolicitud.Checked)
                    {
                        document.Bookmarks["anexo_legislaopostil"].Select();
                        application.Selection.TypeText("X");

                        //textBox2
                    }

                    if (CheckB7anexosolicitud.Checked)
                    {
                        document.Bookmarks["anexo_numexp"].Select();
                        application.Selection.TypeText("X");

                        //textBox2
                    }

                    if (CheckB8anexosolicitud.Checked)
                    {
                        document.Bookmarks["anexo_datogenpersona"].Select();
                        application.Selection.TypeText("X");

                        //textBox2
                    }

                    if (CheckB9anexosolicitud.Checked)
                    {
                        document.Bookmarks["anexo_domiclicencata"].Select();
                        application.Selection.TypeText("X");

                        //textBox2
                    }

                    //conect con_3 = new conect();
                    //String squerydatoficinas = "select * from datosoficina limit 1;";
                    //MySqlDataReader resp_datofi = con_3.getdatareader(squerydatoficinas);
                    //while (resp_datofi.Read())
                    //{
                    //    String OficinaCP = dicssdfunctions.validareader("OficinaCP", "id_datosoficina", resp_datofi).Text;
                    //    String OficinaCalle = dicssdfunctions.validareader("OficinaCalle", "id_datosoficina", resp_datofi).Text;
                    //    String OficinaNumExt = dicssdfunctions.validareader("OficinaNumExt", "id_datosoficina", resp_datofi).Text;
                    //    String OficinaNumInt = dicssdfunctions.validareader("OficinaNumInt", "id_datosoficina", resp_datofi).Text;
                    //    String OficinaColonia = dicssdfunctions.validareader("OficinaColonia", "id_datosoficina", resp_datofi).Text;
                    //    String OficinaMunicipio = dicssdfunctions.validareader("OficinaMunicipio", "id_datosoficina", resp_datofi).Text;
                    //    String OficinaEstado = dicssdfunctions.validareader("OficinaEstado", "id_datosoficina", resp_datofi).Text;
                    //    String OficinaPaisId = dicssdfunctions.validareader("OficinaPaisId", "id_datosoficina", resp_datofi).Text;
                    //    String OficinaTelefono = dicssdfunctions.validareader("OficinaTelefono", "id_datosoficina", resp_datofi).Text;
                    //    String OficinaCorreo = dicssdfunctions.validareader("OficinaCorreo", "id_datosoficina", resp_datofi).Text;
                    //    String ApoderadoNonbre = dicssdfunctions.validareader("ApoderadoNonbre", "id_datosoficina", resp_datofi).Text;
                    //    String ApoderadoApellidoPat = dicssdfunctions.validareader("ApoderadoApellidoPat", "id_datosoficina", resp_datofi).Text;
                    //    String ApoderadoApellidoMat = dicssdfunctions.validareader("ApoderadoApellidoMat", "id_datosoficina", resp_datofi).Text;
                    //    String AutorizadoNombre = dicssdfunctions.validareader("AutorizadoNombre", "id_datosoficina", resp_datofi).Text;
                    //    String AutorizadApellidoPat = dicssdfunctions.validareader("AutorizadApellidoPat", "id_datosoficina", resp_datofi).Text;
                    //    String AutorizadoApellidoMat = dicssdfunctions.validareader("AutorizadoApellidoMat", "id_datosoficina", resp_datofi).Text;

                    document.Bookmarks["correo"].Select();
                    application.Selection.TypeText(OficinaCorreo);

                    document.Bookmarks["Nombrefirmarepresent"].Select();
                    application.Selection.TypeText(ApoderadoNonbre + " " + ApoderadoApellidoPat + " " + ApoderadoApellidoMat);

                    document.Bookmarks["calle_2"].Select();
                    application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(OficinaCalle));

                    document.Bookmarks["num_ext_2"].Select();
                    application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(OficinaNumExt));

                    document.Bookmarks["num_int_2"].Select();
                    application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(OficinaNumInt));

                    document.Bookmarks["col_2"].Select();
                    application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(OficinaColonia));

                    document.Bookmarks["cp_2"].Select();
                    application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(OficinaCP));

                    document.Bookmarks["localidad_2"].Select();
                    application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(OficinaMunicipio));

                    document.Bookmarks["entfed_2"].Select();
                    application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(OficinaEstado));
                    //}
                    //resp_datofi.Close();
                    //con_3.Cerrarconexion();
                    document.Save();
                    //application.Quit();
                    this.Close();
                }
                catch (Exception E)
                {
                    Console.Write("Error: " + E + "\n");
                }
            }
            catch (Exception E)
            {
                new filelog(loguin.sId, E.ToString());
            }
        }
    }
}
