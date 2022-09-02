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
    public partial class Formato_03 : Form
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

        public Formato_03(Form1 login, String sCasoid, fTmarcas formmarcas)
        {
            loguin = login;
            sgCasooid = sCasoid;
            fGtmarcas = formmarcas;
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
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
                    String sruta_plantilla = confilepth.sFileupload + @"\formatosconfigurables\IMPI-00-003_1.docx";
                    Random r = new Random();
                    int srandonm = r.Next(9, 9999);
                    DateTime fecha = DateTime.Now;
                    String sFehacss = fecha.ToString("HHmmss");
                    String carpeta = "C:\\Formatos_CasosKing";
                    //si no existe la carpeta temporal la creamos
                    if (!(Directory.Exists(carpeta)))
                    {
                        Directory.CreateDirectory(carpeta);
                    }
                    //Sring sArchivogenerado = carpeta + "\\"+ fGtmarcas.sCasoId +" IMPI-00-003_" + sFehacss + "transmiciondederechos" + ".docx";
                    String sArchivogenerado = dicssdfunctions.carpeta + "\\" + fGtmarcas.sCasoId + " IMPI-00-003" + dicssdfunctions.sGetfechehhmmss() + "transmiciondederechos" + ".docx";
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
                    /*se solicita inscripcion*/
                    if(cb_CheckBox_10.Checked){
                        document.Bookmarks["x_inscripciondelosca"].Select();
                        application.Selection.TypeText("X");
                    }
                    /*FIN se solicita inscripcion*/


                    /*Revisamos Tipo de inscripción */
                    if (rb_1.Checked)
                    {
                        document.Bookmarks["x_titularescedentes"].Select();
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
                                    case "ME":
                                        {
                                            document.Bookmarks["PM_rfc_ced"].Select();
                                            application.Selection.TypeText(fGtmarcas.rfc_cambintermed2);

                                            document.Bookmarks["PM_razonsocial_ced"].Select();
                                            application.Selection.TypeText(fGtmarcas.rasonsoc_cambint2);


                                            document.Bookmarks["PM_telefono_ced"].Select();
                                            application.Selection.TypeText(OficinaTelefono);

                                            if (fGtmarcas.lvinteresados.Items.Count > 1)
                                            {
                                                document.Bookmarks["x_anexoPM_ced"].Select();
                                                application.Selection.TypeText("X");
                                                //generar el anexo de interesados
                                            }
                                        } break;
                                    case "FE":
                                        {
                                            document.Bookmarks["PF_curp_cedentes"].Select();
                                            application.Selection.TypeText(fGtmarcas.curp_1);

                                            document.Bookmarks["PF_nombres_ced"].Select();
                                            application.Selection.TypeText(fGtmarcas.nombre_1);

                                            document.Bookmarks["PF_primerapellido_ce"].Select();
                                            application.Selection.TypeText(fGtmarcas.appl1_1);

                                            document.Bookmarks["PF_segundoapell_ced"].Select();
                                            application.Selection.TypeText(fGtmarcas.appl2_1);


                                            document.Bookmarks["PF_telefono_ced"].Select();
                                            application.Selection.TypeText(OficinaTelefono);

                                            if (fGtmarcas.lvinteresados.Items.Count > 1)
                                            {
                                                document.Bookmarks["x_anexoPF_ced"].Select();
                                                application.Selection.TypeText("X");
                                                //generar el anexo de interesados
                                            }

                                        } break;
                                    case "MN":
                                        {
                                            document.Bookmarks["PM_rfc_ced"].Select();
                                            application.Selection.TypeText(fGtmarcas.rfc_cambintermed2);

                                            document.Bookmarks["PM_razonsocial_ced"].Select();
                                            application.Selection.TypeText(fGtmarcas.rasonsoc_cambint2);

                                            document.Bookmarks["PM_telefono_ced"].Select();
                                            application.Selection.TypeText(OficinaTelefono);

                                            if (fGtmarcas.lvinteresados.Items.Count > 1)
                                            {
                                                document.Bookmarks["x_anexoPM_ced"].Select();
                                                application.Selection.TypeText("X");
                                                //generar el anexo de interesados
                                            }

                                        } break;
                                    case "FN":
                                        {
                                            document.Bookmarks["PF_curp_cedentes"].Select();
                                            application.Selection.TypeText(fGtmarcas.curp_1);

                                            document.Bookmarks["PF_nombres_ced"].Select();
                                            application.Selection.TypeText(fGtmarcas.nombre_1);

                                            document.Bookmarks["PF_primerapellido_ce"].Select();
                                            application.Selection.TypeText(fGtmarcas.appl1_1);

                                            document.Bookmarks["PF_segundoapell_ced"].Select();
                                            application.Selection.TypeText(fGtmarcas.appl2_1);


                                            document.Bookmarks["PF_telefono_ced"].Select();
                                            application.Selection.TypeText(OficinaTelefono);

                                            if (fGtmarcas.lvinteresados.Items.Count > 1)
                                            {
                                                document.Bookmarks["x_anexoPF_ced"].Select();
                                                application.Selection.TypeText("X");
                                                //generar el anexo de interesados
                                            }
                                        } break;
                                }
                                //viene de la base por que son datos de king
                            }
                        }
                    }
                    else {
                        if (rb_2.Checked)
                        {
                            
                            document.Bookmarks["x_titularecesionario"].Select();
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
                                                document.Bookmarks["PM_rfc_cesionado"].Select();
                                                application.Selection.TypeText(fGtmarcas.rfc_cambintermed2);

                                                document.Bookmarks["PM_Razonsocial_cesio"].Select();
                                                application.Selection.TypeText(fGtmarcas.rasonsoc_cambint2);

                                                document.Bookmarks["PM_nacion_cesionario"].Select();
                                                application.Selection.TypeText(fGtmarcas.nacionalidad_1);

                                                document.Bookmarks["PM_Telef_cesionario"].Select();
                                                application.Selection.TypeText(OficinaTelefono);

                                                if (fGtmarcas.lvinteresados.Items.Count > 1)
                                                {
                                                    document.Bookmarks["PM_anexo_cesionarios"].Select();
                                                    application.Selection.TypeText("X");
                                                    //generar el anexo de interesados
                                                }
                                            } break;
                                        case "FE":
                                            {
                                                document.Bookmarks["PF_curp_cesionaios"].Select();
                                                application.Selection.TypeText(fGtmarcas.curp_1);

                                                document.Bookmarks["PF_nombre_Cesionario"].Select();
                                                application.Selection.TypeText(fGtmarcas.nombre_1);

                                                document.Bookmarks["PF_primapellid_cesio"].Select();
                                                application.Selection.TypeText(fGtmarcas.appl1_1);

                                                document.Bookmarks["PF_segapell_cesionar"].Select();
                                                application.Selection.TypeText(fGtmarcas.appl2_1);

                                                document.Bookmarks["PF_nacio_cesionario"].Select();
                                                application.Selection.TypeText(fGtmarcas.nacionalidad_1);

                                                document.Bookmarks["PF_telefono_cesionar"].Select();
                                                application.Selection.TypeText(OficinaTelefono);

                                                if (fGtmarcas.lvinteresados.Items.Count > 1)
                                                {
                                                    document.Bookmarks["x_anexoPF_cesionario"].Select();
                                                    application.Selection.TypeText("X");
                                                    //generar el anexo de interesados
                                                }

                                            } break;
                                        case "MN":
                                            {
                                                document.Bookmarks["PM_rfc_cesionado"].Select();
                                                application.Selection.TypeText(fGtmarcas.rfc_cambintermed2);

                                                document.Bookmarks["PM_Razonsocial_cesio"].Select();
                                                application.Selection.TypeText(fGtmarcas.rasonsoc_cambint2);

                                                document.Bookmarks["PM_nacion_cesionario"].Select();
                                                application.Selection.TypeText(fGtmarcas.nacionalidad_1);

                                                document.Bookmarks["PM_Telef_cesionario"].Select();
                                                application.Selection.TypeText(OficinaTelefono);

                                                if (fGtmarcas.lvinteresados.Items.Count > 1)
                                                {
                                                    document.Bookmarks["PM_anexo_cesionarios"].Select();
                                                    application.Selection.TypeText("X");
                                                    //generar el anexo de interesados
                                                }
                                            } break;
                                        case "FN":
                                            {
                                                document.Bookmarks["PF_curp_cesionaios"].Select();
                                                application.Selection.TypeText(fGtmarcas.curp_1);

                                                document.Bookmarks["PF_nombre_Cesionario"].Select();
                                                application.Selection.TypeText(fGtmarcas.nombre_1);

                                                document.Bookmarks["PF_primapellid_cesio"].Select();
                                                application.Selection.TypeText(fGtmarcas.appl1_1);

                                                document.Bookmarks["PF_segapell_cesionar"].Select();
                                                application.Selection.TypeText(fGtmarcas.appl2_1);

                                                document.Bookmarks["PF_nacio_cesionario"].Select();
                                                application.Selection.TypeText(fGtmarcas.nacionalidad_1);

                                                document.Bookmarks["PF_telefono_cesionar"].Select();
                                                application.Selection.TypeText(OficinaTelefono);

                                                if (fGtmarcas.lvinteresados.Items.Count > 1)
                                                {
                                                    document.Bookmarks["x_anexoPF_cesionario"].Select();
                                                    application.Selection.TypeText("X");
                                                    //generar el anexo de interesados
                                                }
                                            } break;
                                    }
                                    //viene de la base por que son datos de king
                                }
                            }

                        }
                    }
                    /*inicio peticiones adicionales*/

                    if (checkBox2.Checked)
                    {
                        document.Bookmarks["peticion_1"].Select();
                        application.Selection.TypeText("X");

                    }

                    if (checkBox3.Checked)
                    {
                        document.Bookmarks["peticion_2"].Select();
                        application.Selection.TypeText("X");

                    }
                    /*fin peticiones adicionales*/
                    
                    /*FIN Revisamos Tipo de inscripción */
                    //Agregamos el numero de expediente largo
                    if(fGtmarcas.tbDExpediente.Text!=""){
                        document.Bookmarks["Numerosdeexpediente"].Select();
                        application.Selection.TypeText(fGtmarcas.tbDExpediente.Text);
                    }
                  
                    

                    //empiezan los anexos
                    if (checkBox1.Checked)
                    {
                        document.Bookmarks["anexo_comprpago"].Select();
                        application.Selection.TypeText("X");

                        //textBox2
                    }

                    if (checkBox_2.Checked)
                    {
                        document.Bookmarks["anexo_acredpersmanda"].Select();
                        application.Selection.TypeText("X");

                        //textBox2
                    }

                    if (checkBox_3.Checked)
                    {
                        document.Bookmarks["anexo_constanciainsc"].Select();
                        application.Selection.TypeText("X");

                        //textBox2
                    }

                    if (checkBox_4.Checked)
                    {
                        document.Bookmarks["anexo_transmiderecho"].Select();
                        application.Selection.TypeText("X");

                        //textBox2
                    }

                    if (checkBox_5.Checked)
                    {
                        document.Bookmarks["anexo_trad_docuprese"].Select();
                        application.Selection.TypeText("X");

                        //textBox2
                    }

                    if (CheckBox_6.Checked)
                    {
                        document.Bookmarks["anexo_legislacion_Ex"].Select();
                        application.Selection.TypeText("X");

                        //textBox2
                    }

                    if (CheckBox_7.Checked)
                    {
                        document.Bookmarks["anexo_hojaadicion"].Select();
                        application.Selection.TypeText("X");

                        //textBox2
                    }

                    if (CheckBox_8.Checked)
                    {
                        document.Bookmarks["anexo_datgendlperson"].Select();
                        application.Selection.TypeText("X");

                        //textBox2
                    }

                    if (CheckBox_9.Checked)
                    {
                        document.Bookmarks["anexo_datgendeltitul"].Select();
                        application.Selection.TypeText("X");

                        //textBox2
                    }

                    if (CheckBox_10.Checked)
                    {
                        document.Bookmarks["anexo_formcomplentaA"].Select();
                        application.Selection.TypeText("X");

                        //textBox2
                    }



                    //consultamos la cdireccion
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

                    /*Si contiene apderado*/

                    document.Bookmarks["Nombre_Apoderado"].Select();
                    application.Selection.TypeText(ApoderadoNonbre);

                    document.Bookmarks["primapellido_apodera"].Select();
                    application.Selection.TypeText(ApoderadoApellidoPat);

                    document.Bookmarks["segapellido_apoderad"].Select();
                    application.Selection.TypeText(ApoderadoApellidoMat);

                    document.Bookmarks["telefono_apoderado"].Select();
                    application.Selection.TypeText(OficinaTelefono);

                    document.Bookmarks["correo_apoderado"].Select();
                    application.Selection.TypeText(OficinaCorreo);

                    /*FIN Si contiene apderado*/

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

                    ////document.Bookmarks["Municipio_notific"].Select();//si es extranjero
                    ////application.Selection.TypeText(OficinaMunicipio);

                    document.Bookmarks["Localidad_notific"].Select();
                    application.Selection.TypeText(OficinaMunicipio);

                    document.Bookmarks["EntidadFederativa_no"].Select();
                    application.Selection.TypeText("");

                    document.Bookmarks["Entrecalles_notific"].Select();
                    application.Selection.TypeText("");

                    document.Bookmarks["Correo_notific"].Select();
                    application.Selection.TypeText(OficinaCorreo);

                    document.Bookmarks["Nombreyfirmadelrepre"].Select();
                    application.Selection.TypeText(ApoderadoNonbre + " " + ApoderadoApellidoPat + " " + ApoderadoApellidoMat);

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
                    //}
                    //resp_datofi.Close();
                    //con_3.Cerrarconexion();
                    document.Save();
                    this.Close();
                    //application.Quit();
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
