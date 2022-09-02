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
    public partial class FCambioReginemNombreDenominacionRazon_2 : Form
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
        //Solicitud de Inscripción de Transformación de Régimen Jurídico o Cambio de Nombre, Denominación o Razón Social
        //IMPI-00-006.doc
        public FCambioReginemNombreDenominacionRazon_2(Form1 login, String sCasoid, fTmarcas formmarcas)
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
                    String sruta_plantilla = confilepth.sFileupload + @"\formatosconfigurables\IMPI-00-006_1.docx";
                    Random r = new Random();
                    int srandonm = r.Next(9, 9999);
                    //String sArchivogenerado = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\casosking\" + "Nuevos_formatos\\IMPI-00-006_" + srandonm + ".docx";
                    String sArchivogenerado = dicssdfunctions.carpeta + "\\" + fGtmarcas.sCasoId + " IMPI-00-006_" + dicssdfunctions.sGetfechehhmmss() + "trasnformacionderegimen" + ".docx";
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
                    
                    ////validamos que tipo inscripción que solicita
                    if (radioButton1.Checked)
                    {
                        document.Bookmarks["Transregimenjuridico"].Select();
                        application.Selection.TypeText("X");
                    }
                    else {
                        if (radioButton2.Checked)
                        {
                            document.Bookmarks["cambionombrersocial"].Select();
                            application.Selection.TypeText("X");
                        }
                    }

                    //colocamos el número de expediente
                    if (fGtmarcas.tbDExpediente.Text != "")
                    {
                        document.Bookmarks["Numero_tramita_Regpu"].Select();
                        application.Selection.TypeText(fGtmarcas.tbDExpediente.Text);

                    }
                    /*Agregamos los datos del interesado*/
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
                                        document.Bookmarks["PM_rfc_1"].Select();
                                        application.Selection.TypeText(fGtmarcas.rfc_cambintermed2);

                                        document.Bookmarks["PM_rasonsoc_1"].Select();
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
                                        document.Bookmarks["PF_curp_1"].Select();
                                        application.Selection.TypeText(fGtmarcas.curp_1);

                                        document.Bookmarks["PF_nombre_1"].Select();
                                        application.Selection.TypeText(fGtmarcas.nombre_1);

                                        document.Bookmarks["PF_appl1_1"].Select();
                                        application.Selection.TypeText(fGtmarcas.appl1_1);

                                        document.Bookmarks["PF_appl2_1"].Select();
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
                    /*Agregams los datos de las notificaciones*/
                    //conect con_3 = new conect();
                    //String squerydatoficinas = "select * from datosoficina limit 1;";
                    //MySqlDataReader resp_datofi = con_3.getdatareader(squerydatoficinas);
                    //while (resp_datofi.Read())
                    //{
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

                    document.Bookmarks["correo_1"].Select();
                    application.Selection.TypeText(OficinaCorreo);

                    document.Bookmarks["Nombreyfirmarepresen"].Select();
                    application.Selection.TypeText(ApoderadoNonbre + " " + ApoderadoApellidoPat + " " + ApoderadoApellidoMat);

                    document.Bookmarks["calle_1"].Select();
                    application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(OficinaCalle));

                    document.Bookmarks["num_ext_1"].Select();
                    application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(OficinaNumExt));

                    document.Bookmarks["num_int_1"].Select();
                    application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(OficinaNumInt));

                    document.Bookmarks["col_1"].Select();
                    application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(OficinaColonia));

                    document.Bookmarks["cp_1"].Select();
                    application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(OficinaCP));

                    document.Bookmarks["localidad_1"].Select();
                    application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(OficinaMunicipio));

                    document.Bookmarks["entfed_1"].Select();
                    application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(OficinaEstado));
                    //}
                    //resp_datofi.Close();
                    //con_3.Cerrarconexion();

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
                        document.Bookmarks["anexo_constalatransf"].Select();
                        application.Selection.TypeText("X");

                        //textBox2
                    }

                    if (CheckB5anexosolicitud.Checked)
                    {
                        document.Bookmarks["anexo_traducc"].Select();
                        application.Selection.TypeText("X");

                        //textBox2
                    }

                    if (CheckB7anexosolicitud.Checked)
                    {
                        document.Bookmarks["anexo_legislaopostil"].Select();
                        application.Selection.TypeText("X");

                        //textBox2
                    }

                    if (CheckB8anexosolicitud.Checked)
                    {
                        document.Bookmarks["anexo_numexp"].Select();
                        application.Selection.TypeText("X");

                        //textBox2
                    }

                    if (CheckB9anexosolicitud.Checked)
                    {
                        document.Bookmarks["anexo_datogenpersona"].Select();
                        application.Selection.TypeText("X");

                        //textBox2
                    }
                
                

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
                application.Visible = true;
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
