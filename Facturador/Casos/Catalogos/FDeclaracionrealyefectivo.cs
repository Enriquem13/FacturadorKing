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
{
    public partial class FDeclaracionrealyefectivo : Form
    {
        public Form1 loguin;
        public String sgCasooid;
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

        funcionesdicss dicssdfunctions = new funcionesdicss();
        fTmarcas fGtmarcas;
        public FDeclaracionrealyefectivo(Form1 login, String sCasoid, fTmarcas formmarcas)
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
                    String sruta_plantilla = confilepth.sFileupload + @"\formatosconfigurables\IMPI-00-014_1.docx";
                    Random r = new Random();
                    int srandonm = r.Next(9, 9999);
                    //String sArchivogenerado = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\casosking\" + "Nuevos_formatos\\IMPI-00-014_1_" + srandonm + ".docx";
                    String sArchivogenerado = dicssdfunctions.carpeta + "\\" + fGtmarcas.sCasoId + " IMPI-00-014_1_" + dicssdfunctions.sGetfechehhmmss() + "declaraciondeuso" + ".docx";
                    File.Copy(sruta_plantilla, sArchivogenerado);
                    //abrimos el archivo temporal y lo reemplzamos con los datos
                    Word.Application application = new Word.Application();
                    Word.Document document = application.Documents.Open(sArchivogenerado);

                    /*
                     Cargamos la información del aoficina
                     */
                    conect con_3 = new conect();
                    String squerydatoficinas = "select * from datosoficina limit 1;";
                    MySqlDataReader resp_datofi = con_3.getdatareader(squerydatoficinas);
                    while (resp_datofi.Read())
                    {
                        //String OficinaCP = dicssdfunctions.validareader("OficinaCP", "id_datosoficina", resp_datofi).Text;
                        //String OficinaCalle = dicssdfunctions.validareader("OficinaCalle", "id_datosoficina", resp_datofi).Text;
                        //String OficinaNumExt = dicssdfunctions.validareader("OficinaNumExt", "id_datosoficina", resp_datofi).Text;
                        //String OficinaNumInt = dicssdfunctions.validareader("OficinaNumInt", "id_datosoficina", resp_datofi).Text;
                        //String OficinaColonia = dicssdfunctions.validareader("OficinaColonia", "id_datosoficina", resp_datofi).Text;
                        //String OficinaMunicipio = dicssdfunctions.validareader("OficinaMunicipio", "id_datosoficina", resp_datofi).Text;
                        //String OficinaEstado = dicssdfunctions.validareader("OficinaEstado", "id_datosoficina", resp_datofi).Text;

                        //String OficinaPaisId = dicssdfunctions.validareader("OficinaPaisId", "id_datosoficina", resp_datofi).Text;
                        //String OficinaTelefono = dicssdfunctions.validareader("OficinaTelefono", "id_datosoficina", resp_datofi).Text;
                        //String OficinaCorreo = dicssdfunctions.validareader("OficinaCorreo", "id_datosoficina", resp_datofi).Text;
                        //String ApoderadoNonbre = dicssdfunctions.validareader("ApoderadoNonbre", "id_datosoficina", resp_datofi).Text;
                        //String ApoderadoApellidoPat = dicssdfunctions.validareader("ApoderadoApellidoPat", "id_datosoficina", resp_datofi).Text;
                        //String ApoderadoApellidoMat = dicssdfunctions.validareader("ApoderadoApellidoMat", "id_datosoficina", resp_datofi).Text;
                        //String AutorizadoNombre = dicssdfunctions.validareader("AutorizadoNombre", "id_datosoficina", resp_datofi).Text;
                        //String AutorizadApellidoPat = dicssdfunctions.validareader("AutorizadApellidoPat", "id_datosoficina", resp_datofi).Text;
                        //String AutorizadoApellidoMat = dicssdfunctions.validareader("AutorizadoApellidoMat", "id_datosoficina", resp_datofi).Text;

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
                    //validamos que tipo de solicitud es
                    switch (fGtmarcas.sTipoSolicitudId)
                    {
                        case "7":
                            {
                                document.Bookmarks["registro_marca_1"].Select();
                                application.Selection.TypeText("X");
                            } break;
                        case "8":
                            {
                                document.Bookmarks["PublicacionNomComerc"].Select();
                                application.Selection.TypeText("X");
                            } break;
                        case "9":
                            {
                                document.Bookmarks["Avisocomercial"].Select();
                                application.Selection.TypeText("X");
                            } break;
                        //faltan casos que si existen en el formato
                        //case "77": {//existe en la base como nombre de dominio
                        //    //document.Bookmarks["Nombrededominio"].Select();// no existe en el formato
                        //    //application.Selection.TypeText("X");
                        //} break;
                    }

                    //agregamos la clase y la descripción
                    String sProducos = fGtmarcas.tbclase.Text.Trim();
                    if (sProducos.Length < 3)
                    {
                        switch (sProducos.Length)
                        {
                            case 1:
                                {
                                    document.Bookmarks["Clase1"].Select();
                                    application.Selection.TypeText("0");
                                    document.Bookmarks["Clase2"].Select();
                                    application.Selection.TypeText(sProducos);
                                } break;

                            case 2:
                                {
                                    document.Bookmarks["Clase1"].Select();
                                    application.Selection.TypeText(sProducos.Substring(0, 1));
                                    document.Bookmarks["Clase2"].Select();
                                    application.Selection.TypeText(sProducos.Substring(1, 1));
                                } break;
                        }
                    }
                    else
                    {
                        document.Bookmarks["Clase1"].Select();
                        application.Selection.TypeText(sProducos.Substring(0, 1));
                        document.Bookmarks["Clase2"].Select();
                        application.Selection.TypeText(sProducos.Substring(1, 1));
                        document.Bookmarks["anex_datrenovacion"].Select();
                        application.Selection.TypeText("X");
                        //datrenovacion
                    }
                    //obtenemos la descripcion del producto o productos
                    if (fGtmarcas.dGVProductos.Rows.Count > 0)
                    {
                        document.Bookmarks["Clase_descripcion"].Select();
                        //if (fGtmarcas.lvProductos.Items[0].SubItems[2].Text.Length > 758)
                        if (fGtmarcas.dGVProductos.Rows[0].Cells[2].Value.ToString().Length > 758)
                        {
                            //application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtmarcas.dGVProductos.Rows[0].Cells[2].Value.ToString().Substring(0, 758)));
                            application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtmarcas.dGVProductos.Rows[0].Cells[2].Value.ToString().Substring(0, 738) + " ...(continua en anexo)"));
                            /*agregamos las acciones para el anexo*/
                            creanexo(fGtmarcas.dGVProductos.Rows[0].Cells[2].Value.ToString());
                            document.Bookmarks["anex_datrenovacion"].Select();
                            application.Selection.TypeText("X");
                        }
                        else
                        {
                            application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtmarcas.dGVProductos.Rows[0].Cells[2].Value.ToString()));
                        }
                    }
                    //numero de registro
                    if (fGtmarcas.tbDNumeroReg.Text != "")
                    {
                        document.Bookmarks["Numeroderegistro"].Select();
                        application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtmarcas.tbDNumeroReg.Text));
                    }

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
                                        document.Bookmarks["PM_rfc_1"].Select();
                                        application.Selection.TypeText(fGtmarcas.rfc_cambintermed2);

                                        document.Bookmarks["PM_rasonsoc_1"].Select();
                                        application.Selection.TypeText(fGtmarcas.rasonsoc_cambint2);

                                        document.Bookmarks["PM_nacionalidad_1"].Select();
                                        application.Selection.TypeText(fGtmarcas.nacionalidad_1);

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
                                        document.Bookmarks["PF_curp_1"].Select();
                                        application.Selection.TypeText(fGtmarcas.curp_1);

                                        document.Bookmarks["PF_nombre_1"].Select();
                                        application.Selection.TypeText(fGtmarcas.nombre_1);

                                        document.Bookmarks["PF_appl1_1"].Select();
                                        application.Selection.TypeText(fGtmarcas.appl1_1);

                                        document.Bookmarks["PF_appl2_1"].Select();
                                        application.Selection.TypeText(fGtmarcas.appl2_1);

                                        document.Bookmarks["PF_nacionalidad_1"].Select();
                                        application.Selection.TypeText(fGtmarcas.nacionalidad_1);

                                        document.Bookmarks["PF_telefono_1"].Select();
                                        application.Selection.TypeText(OficinaTelefono);

                                        if (fGtmarcas.lvinteresados.Items.Count > 1)
                                        {
                                            document.Bookmarks["PF_anexo_x"].Select();
                                            application.Selection.TypeText("X");
                                            //generar el anexo de interesados
                                        }

                                    } break;
                                case "Moral Nacional":
                                    {
                                        document.Bookmarks["PM_rfc_1"].Select();
                                        application.Selection.TypeText(fGtmarcas.rfc_cambintermed2);

                                        document.Bookmarks["PM_rasonsoc_1"].Select();
                                        application.Selection.TypeText(fGtmarcas.rasonsoc_cambint2);

                                        document.Bookmarks["PM_nacionalidad_1"].Select();
                                        application.Selection.TypeText(fGtmarcas.nacionalidad_1);

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
                                        document.Bookmarks["PF_curp_1"].Select();
                                        application.Selection.TypeText(fGtmarcas.curp_1);

                                        document.Bookmarks["PF_nombre_1"].Select();
                                        application.Selection.TypeText(fGtmarcas.nombre_1);

                                        document.Bookmarks["PF_appl1_1"].Select();
                                        application.Selection.TypeText(fGtmarcas.appl1_1);

                                        document.Bookmarks["PF_appl2_1"].Select();
                                        application.Selection.TypeText(fGtmarcas.appl2_1);

                                        document.Bookmarks["PF_nacionalidad_1"].Select();
                                        application.Selection.TypeText(fGtmarcas.nacionalidad_1);

                                        document.Bookmarks["PF_telefono_1"].Select();
                                        application.Selection.TypeText(OficinaTelefono);

                                        if (fGtmarcas.lvinteresados.Items.Count > 1)
                                        {
                                            document.Bookmarks["PF_anexo_x"].Select();
                                            application.Selection.TypeText("X");
                                            //generar el anexo de interesados
                                        }
                                    } break;
                            }
                            //viene de la base por que son datos de king
                        }
                    }
                    //consultamos la cdireccion
                    


                        document.Bookmarks["CodPostal_notif"].Select();
                        application.Selection.TypeText(OficinaCP);

                        document.Bookmarks["Calle_notif"].Select();
                        application.Selection.TypeText(OficinaCalle);

                        document.Bookmarks["Num_ext_notific"].Select();
                        application.Selection.TypeText(OficinaNumExt);

                        document.Bookmarks["Colonia_notifica"].Select();
                        application.Selection.TypeText(OficinaColonia);

                        document.Bookmarks["Num_int_notific"].Select();
                        application.Selection.TypeText(OficinaNumInt);

                        //document.Bookmarks["EntidadFederativa_no"].Select();
                        //application.Selection.TypeText(OficinaEstado);

                        ////document.Bookmarks["Municipio_notific"].Select();//si es extranjero
                        ////application.Selection.TypeText(OficinaMunicipio);

                        document.Bookmarks["Localidad_notific"].Select();
                        application.Selection.TypeText(OficinaMunicipio);

                        document.Bookmarks["EntidadFederativa_no"].Select();
                        application.Selection.TypeText(OficinaEstado);

                        //document.Bookmarks["Entrecalles_notific"].Select();
                        //application.Selection.TypeText("");

                        document.Bookmarks["Correo_notific"].Select();
                        application.Selection.TypeText(OficinaCorreo);


                        document.Bookmarks["Nombreyfirmamanda"].Select();
                        application.Selection.TypeText(ApoderadoNonbre + " " + ApoderadoApellidoPat + " " + ApoderadoApellidoMat);
                        //Nombreyfirmdeltitula
                        //CodPostal_notif
                        //Calle_notif
                        //Num_ext_notific
                        //Colonia_notifica
                        //Num_int_notific
                        //Municipio_notific
                        //Localidad_notific

                        //EntidadFederativa_no
                        //Entrecalles_notific
                        //Calleposterior_notif
                        //Correo_notific
                    
                    
                    //validamos los anexos
                    //checkBox1
                    //textBox2
                    if (CheckB1anexosolicitud.Checked)
                    {
                        document.Bookmarks["anexo_comprpago"].Select();
                        application.Selection.TypeText("X");

                    }

                    if (CheckB2anexosolicitud.Checked)
                    {
                        document.Bookmarks["anexo_datogenpersona"].Select();
                        application.Selection.TypeText("X");

                    }

                    if (CheckB3anexosolitud.Checked)
                    {
                        document.Bookmarks["anexo_declarausodete"].Select();
                        application.Selection.TypeText("X");

                    }

                    if (CheckB4anexosolicitud.Checked)
                    {
                        document.Bookmarks["anexo_acredpersmanda"].Select();
                        application.Selection.TypeText("X");

                    }

                    if (CheckB5anexosolicitud.Checked)
                    {
                        document.Bookmarks["anexo_continsRGPimpi"].Select();
                        application.Selection.TypeText("X");

                    }

                    if (CheckB6anexosolicitud.Checked)
                    {
                        document.Bookmarks["anexo_traducc"].Select();
                        application.Selection.TypeText("X");

                    }

                    if (CheckB7anexosolicitud.Checked)
                    {
                        document.Bookmarks["anexo_legislaopostil"].Select();
                        application.Selection.TypeText("X");

                    }
                   
                    application.Visible = true;
                    document.Save();
                    this.Close();
                    //application.Quit();
                }
                catch (Exception E)
                {
                    Console.Write("Error: " + E + "\n");
                    new filelog(loguin.sId, E.ToString());
                }
            }
            catch (Exception E)
            {
                new filelog(loguin.sId, E.ToString());
            }
        }
        public void creanexo(String sContenido)
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
                    String sruta_plantilla = confilepth.sFileupload + @"\formatosconfigurables\Anexo_IMPI-00-014_1.docx";
                    Random r = new Random();
                    int srandonm = r.Next(9, 9999);
                    //String sArchivogenerado = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\casosking\" + "Nuevos_formatos\\IMPI-00-014_1_" + srandonm + ".docx";
                    String sArchivogenerado = dicssdfunctions.carpeta + "\\" + fGtmarcas.sCasoId + " IMPI-00-014_1_anexo_" + dicssdfunctions.sGetfechehhmmss() + "" + ".docx";
                    File.Copy(sruta_plantilla, sArchivogenerado);
                    //abrimos el archivo temporal y lo reemplzamos con los datos
                    Word.Application application = new Word.Application();
                    Word.Document document = application.Documents.Open(sArchivogenerado);

                    /*
                     Cargamos la información del aoficina
                     */

                    //agregamos la fecha
                    document.Bookmarks["contenidoanexo"].Select();
                    application.Selection.TypeText(sContenido);

                    application.Visible = true;
                    document.Save();
                    this.Close();
                    //application.Quit();
                }
                catch (Exception E)
                {
                    Console.Write("Error: " + E + "\n");
                    new filelog(loguin.sId, E.ToString());
                }
            }
            catch (Exception E)
            {
                new filelog(loguin.sId, E.ToString());
            }
        }

    }
}
