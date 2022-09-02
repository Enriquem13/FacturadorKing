using MailBee.Mime;
using MailBee.Pop3Mail;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Diagnostics.Process;

namespace Facturador
{
    public partial class Form1 : Form
    {
        public String sId = "";
        public String sNombre = "";
        public String sPaterno = "";
        public String sMaterno = "";
        public String sUsername = "";
        public String sCorreousr = "";
        public String sContrasenacorreo = "";
        public String sUsuarioCodigo = "";
        public String sUsuarioparadocs = "";
        public String sUsuarioIndAdmin = "";
        public MailMessage Mesageemail;
        public string[] DifferArray;
        public String sVersion = "1.0190";
        public bool sIniciosesion = true;


        public Form1()
        {
            InitializeComponent();
            /**
             *  
             *  192.168.1.160
                casos_king
                root
                Alejandra5m
                \\192.168.1.95\documentosserver
                3306
             * **/
            //Debe existir una validación para poder generar el archivo 
            //de la configuración y poder direccionar a la base de pruiebas o productiva
            lbCompilaicon.Text = "V. - ("+ sVersion + ")";
            /*por unica ocacion cambiaremos la configuracion del archivo de conexion para que se conecte al servidor servidor 18 / 01 / 2022*/
            //validaip();


        }

        public void validaip()
        {
            try
            {
                string[] lineas = { "", "", "", "", "", "" };
                String strRutaArchivo = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string ficheroleer = strRutaArchivo + "\\casosking\\confacturador.prop";
                string contenido = String.Empty;
                if (File.Exists(ficheroleer))
                {
                    contenido = File.ReadAllText(ficheroleer);
                    lineas = contenido.Split('\n');
                }

                //if (false) { //quitar linea en la version superior a la 82
                if (lineas[0] != "192.168.1.160")
                { //quitar linea en la version superior a la 82

                    //String ruta_log = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    //string fichero = ruta_log + "\\casosking\\confacturador.prop";
                    ////Open the File
                    //int x = 0;
                    //StreamWriter sw = new StreamWriter(fichero, false, Encoding.UTF8);//false para sobre escribir
                    //String[] sLineasarchivo = { "192.168.1.160", "casos_king", "root", "Alejandra5m", "\\\\192.168.1.95\\documentosserver", "3306" };
                    ////Writeout the numbers 1 to 10 on the same line.
                    //for (x = 0; x < 6; x++)
                    //{
                    //    sw.Write(sLineasarchivo[x] + "\n");
                    //}
                    ////close the file
                    //sw.Close();
                    sIniciosesion = false;
                }
                else
                {
                    sIniciosesion = true;
                }

            }
            catch (Exception exss)
            {
                MessageBox.Show("Verifique la conexión al servidor en configuración");
                sIniciosesion = false;
            }
        }
        public ComboboxItem validareader(String campoText, String campoValue, MySqlDataReader mresultado)
        {
            try
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
            catch (Exception E)
            {
                ComboboxItem cItemresult = new ComboboxItem();
                cItemresult.Text = "";
                cItemresult.Value = "";
                return cItemresult;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //String usuario = textBox1.Text;
            MessageBox.Show("usuario");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try {
                //validaip();
                if (!sIniciosesion) {
                    MessageBox.Show("Verifique la conexión al servidor en configuración.\n\n host:\\\\192.168.1.160\\documentosserver\npuerto:3306");
                    return;
                }
                conect con=  new conect();
                String usuariocod = Usuario.Text;
                String contrasenacod = contrasena.Text;
                String login = "SELECT * FROM `usuario` WHERE binary `UsuarioClave` = '" + usuariocod + "' and binary UsuarioPassword = '" + contrasenacod +"'";
                //String respuestastring = con.setquery(login);
                MySqlDataReader respuestastring2count = con.getdatareader(login);

                int countIntrows = 0;
                while (respuestastring2count.Read())
                {
                    countIntrows++;
                    sId = validareader("UsuarioId", "UsuarioId", respuestastring2count).Text;
                    sNombre = validareader("UsuarioNombre", "UsuarioId", respuestastring2count).Text; //respuestastring2count.GetString(respuestastring2count.GetOrdinal("UsuarioNombre"));
                    sPaterno = validareader("UsuarioPaterno", "UsuarioId", respuestastring2count).Text; //respuestastring2count.GetString(respuestastring2count.GetOrdinal("UsuarioPaterno"));
                    sMaterno = validareader("UsuarioMaterno", "UsuarioId", respuestastring2count).Text; //respuestastring2count.GetString(respuestastring2count.GetOrdinal("UsuarioMaterno"));
                    sUsername = validareader("UsuarioName", "UsuarioId", respuestastring2count).Text; //respuestastring2count.GetString(respuestastring2count.GetOrdinal("UsuarioName"));
                    sCorreousr = validareader("UsuarioEmail", "UsuarioId", respuestastring2count).Text;
                    sContrasenacorreo = validareader("Usuar0ioClaveEInvoice", "UsuarioId", respuestastring2count).Text;
                    sUsuarioCodigo = validareader("UsuarioCodigo", "UsuarioId", respuestastring2count).Text;//UsuarioCodigo
                    sUsuarioIndAdmin = validareader("UsuarioIndAdmin", "UsuarioId", respuestastring2count).Text;//UsuarioCodigo
                    sUsuarioparadocs = validareader("UsuarioClaveEInvoice", "UsuarioId", respuestastring2count).Text;//Usuario nombre para cartas escritos etc.

                    //sNombre = "";
                    //sPaterno = "";
                    //sMaterno = "";
                    //sUsername = "";
                }
                respuestastring2count.Close();
                con.Cerrarconexion();


                if (countIntrows>0)
                {
                    this.Hide();
                    Usuario.Text = "";
                    contrasena.Text = "";
                    Form captura = new captura(this);
                    captura.Show();
                    //if (sCorreousr != "" && sContrasenacorreo != "")
                    //{
                    //    if (sUsuarioCodigo == "1")
                    //    {
                    //        Thread thread = new Thread(() =>
                    //        {
                    //            readmail obj = new readmail();
                    //            using (loadpendientes form = new loadpendientes(buscarcorreos))
                    //            {
                    //                form.ShowDialog();
                    //            }
                    //            corrreospendientes correospendientes = new corrreospendientes(DifferArray);
                    //            correospendientes.Show();
                    //            Application.Run(correospendientes);
                    //        });
                    //        thread.ApartmentState = ApartmentState.STA;
                    //        thread.Start();
                    //    }else {
                    //        Thread thread = new Thread(() =>
                    //        {
                    //            readmail obj = new readmail();
                    //            using (loadpendientes form = new loadpendientes(cargacorreoshistorico))
                    //            {
                    //                form.ShowDialog();
                    //            }
                    //            //Application.Run(correospendientes);
                    //        });
                    //        thread.ApartmentState = ApartmentState.STA;
                    //        thread.Start();
                    //    }
                    //}
                }
                else
                {
                    MessageBox.Show("Verifica usuario y contraseña");
                }
            }catch (Exception E) {
                MessageBox.Show("Verifique la conexión a la red ");
                new filelog(sId, E.ToString());
            }
        }
        private void cargacorreoshistorico() {
            try
            {
                MailBee.Global.LicenseKey = "MN110-8B8932A44B8239779277420FE843-E158";
                Pop3 pop = new Pop3();
                try
                {
                    String sServermail = "";
                    bool b = sCorreousr.Contains("gmail");
                    if (b)
                    {//si el servidor es gmail entonces debe apuntar a uno diferente
                        sServermail = "pop.gmail.com";
                    }
                    else
                    {
                        sServermail = "mail.dicss.com.mx";
                    }
                    pop.Connect(sServermail);//pop
                    pop.Login(sCorreousr, sContrasenacorreo);
                    Console.WriteLine("Successfully logged in. __mail dicss ");
                }
                catch (MailBeePop3LoginNegativeResponseException ex)
                {
                    Console.WriteLine("POP3 server replied with a negative response at login:" + ex.ToString());
                    new filelog(sId, "POP3 server replied with a negative response at login:" + ex.ToString());
                } Console.WriteLine(pop.InboxMessageCount);
                string[] mensjaesids = pop.GetMessageUids();
                //File.AppendAllText("c:\\facturador\\" + "mails.log", mensjaesids[y]+"\n");
                Console.WriteLine(mensjaesids[0]);
                MailMessageCollection msgs = pop.DownloadMessageHeaders();
                
                //String fechalog = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                //StringBuilder sb = new StringBuilder();
                //sb.Append("\n");
                String ruta__mailfile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\casosking\\";
                //String textFile = ruta__mailfile + "\\mails.log";
                //string[] lines = null;
                //if (File.Exists(textFile))
                //{
                //    // Read a text file line by line.
                //    lines = File.ReadAllLines(textFile);
                //}
                //DifferArray = mensjaesids.Except(lines).ToArray();
                for (int y = mensjaesids.Length - 1; y >= 0; y--)
                {
                    File.AppendAllText(ruta__mailfile+"\\" + "mails.log", mensjaesids[y]+"\n");
                    Mesageemail = pop.DownloadEntireMessage(pop.GetMessageIndexFromUid(DifferArray[y]));
                    Console.WriteLine("Asunto: " + Mesageemail.Subject);
                    Console.WriteLine("Mensaje: " + Mesageemail.BodyHtmlText);
                    Console.WriteLine(pop.GetMessageIndexFromUid(DifferArray[y]));// GetMessageIndexFromUid();
                }
                //sb.Clear();
                pop.Disconnect();
            }
            catch (Exception E)
            {
                MessageBox.Show("Verifique la conexión a la red ");
                new filelog(sId, E.ToString());
            }
        }
        private void threadproc(){
            Form captura = new captura(this);
            captura.ShowDialog();
        }
        public void buscarcorreos() {
            try{
                MailBee.Global.LicenseKey = "MN110-8B8932A44B8239779277420FE843-E158";
                Pop3 pop = new Pop3();
                try
                {
                    pop.Connect("mail.dicss.com.mx");//pop
                    pop.Login(sCorreousr, sContrasenacorreo);
                    Console.WriteLine("Successfully logged in. __mail dicss ");
                }
                catch (MailBeePop3LoginNegativeResponseException ex)
                {
                  Console.WriteLine("POP3 server replied with a negative response at login:" + ex.ToString());
                  new filelog(sId, ex.ToString());
                } Console.WriteLine(pop.InboxMessageCount);
                string[] mensjaesids = pop.GetMessageUids();
                //File.AppendAllText("c:\\facturador\\" + "mails.log", mensjaesids[y]+"\n");
                Console.WriteLine(mensjaesids[0]);
                MailMessageCollection msgs = pop.DownloadMessageHeaders();
                DateTime dt = new DateTime();
                //String fechalog = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                StringBuilder sb = new StringBuilder();
                sb.Append("\n");
                String ruta__mailfile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\casosking\\";
                String textFile =ruta__mailfile+"\\mails.log";
                string[] lines = null;
                if (File.Exists(textFile))
                {
                    // Read a text file line by line.
                    lines = File.ReadAllLines(textFile);
                }
                DifferArray = mensjaesids.Except(lines).ToArray();

                for (int y = DifferArray.Length - 1; y >= 0; y--)
                {
                    Mesageemail = pop.DownloadEntireMessage(pop.GetMessageIndexFromUid(DifferArray[y]));
                    Console.WriteLine("Asunto: " + Mesageemail.Subject);
                    Console.WriteLine("Mensaje: " + Mesageemail.BodyHtmlText);
                    Console.WriteLine(pop.GetMessageIndexFromUid(DifferArray[y]));// GetMessageIndexFromUid();
                }
                sb.Clear();

                pop.Disconnect();
            }catch (Exception E) {
                MessageBox.Show("Verifique la conexión a la red ");
                new filelog(sId, E.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            configinicio aClientenuevo = new configinicio();
            if (aClientenuevo.ShowDialog() == DialogResult.OK)
            { 
            }
        }

        private void contrasena_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //MessageBox.Show("Mensaje enviado");
                button1_Click_1(sender, e);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            generaformatocambiodomicilio objeto = new generaformatocambiodomicilio();
            objeto.ShowDialog();
            //generadocscambiodom obj = new generadocscambiodom();
            //obj.generadocs("6300");
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

            try
            {
                //var dir = Directory.EnumerateFiles(@"C:\Users\eduarximo\Documents\king\casoskinggit_conv\proyect_Casos_king\Facturador\bin\Debug", "*.*",SearchOption.AllDirectories)
                //    .Where(s => s.EndsWith(".dll") || s.EndsWith(".xml"));

                //foreach (var di in dir) {
                //    System.Console.WriteLine(di);
                //}
                //Preguntamos si la ultima version de la base es la misma que la que se tiene en este archivo
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
                    { //version desactualizada
                        ////buscamos la ruta para ver si ya está actualizada en la base
                        //String path = System.Reflection.Assembly.GetEntryAssembly().Location;// Path del ejecutable actual
                        ////lo buscamos en la base de datos
                        //conect con = new conect();
                        //String consulta = "SELECT * FROM `actalizaversion` WHERE `url` = '" + path.Replace("\\", "\\\\") + "' and version= '" + sIdversionact + "';";
                        ////String respuestastring = con.setquery(login);
                        //MySqlDataReader respuestastring2count = con.getdatareader(consulta);
                        //String sIdversion = "";
                        
                        //while (respuestastring2count.Read())
                        //{
                        //    sIdversion = validareader("id_actalizaversion", "id_actalizaversion", respuestastring2count).Text;
                            
                        //}
                        //respuestastring2count.Close();
                        //con.Cerrarconexion();

                        //if (sIdversion == "")
                        //{ //no está actualizado y buscará actualizaro
                            //Buscamos si hay un archivo para actualizar
                            configuracionfiles confilepth = new configuracionfiles();
                            confilepth.configuracionfilesinicio();
                            String sruta_plantilla = confilepth.sFileupload + @"\CasosKingV\Facturador.exe";

                            String sruta_ref_uno = confilepth.sFileupload + @"\CasosKingV\SpreadsheetLight.dll";
                            String sruta_ref_dos = confilepth.sFileupload + @"\CasosKingV\SpreadsheetLight.xml";
                            String sruta_ref_tres = confilepth.sFileupload + @"\CasosKingV\DocumentFormat.OpenXml.dll";
                            String sruta_ref_cuatro = confilepth.sFileupload + @"\CasosKingV\DocumentFormat.OpenXml.xml";

                        string path_destino = System.Reflection.Assembly.GetEntryAssembly().Location;
                            //si existe este archivo entonces actualizamos
                            Boolean bBandera = false;
                            if (File.Exists(sruta_plantilla))
                            {
                                //SpreadsheetLight.dll
                                //SpreadsheetLight.xml
                                String sruta_referencias_destino_uno = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\SpreadsheetLight.dll";
                                String sruta_referencias_destino_dos = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\SpreadsheetLight.xml";
                                String sruta_referencias_destino_tres = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\DocumentFormat.OpenXml.dll";
                                String sruta_referencias_destino_cuatro = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\DocumentFormat.OpenXml.xml";
                                String sruta_referencias_destino_1 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.AppContext.dll";
                                String sruta_referencias_destino_2 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Buffers.dll";
                                String sruta_referencias_destino_3 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Buffers.xml";
                                String sruta_referencias_destino_4 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Collections.Concurrent.dll";
                                String sruta_referencias_destino_5 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Collections.dll";
                                String sruta_referencias_destino_6 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Collections.NonGeneric.dll";
                                String sruta_referencias_destino_7 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Collections.Specialized.dll";
                                String sruta_referencias_destino_8 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.ComponentModel.dll";
                                String sruta_referencias_destino_9 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.ComponentModel.EventBasedAsync.dll";
                                String sruta_referencias_destino_10 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.ComponentModel.Primitives.dll";
                                String sruta_referencias_destino_11 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.ComponentModel.TypeConverter.dll";
                                String sruta_referencias_destino_12 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Console.dll";
                                String sruta_referencias_destino_13 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Data.Common.dll";
                                String sruta_referencias_destino_14 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Diagnostics.Contracts.dll";
                                String sruta_referencias_destino_15 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Diagnostics.Debug.dll";
                                String sruta_referencias_destino_16 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Diagnostics.FileVersionInfo.dll";
                                String sruta_referencias_destino_17 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Diagnostics.Process.dll";
                                String sruta_referencias_destino_18 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Diagnostics.StackTrace.dll";
                                String sruta_referencias_destino_19 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Diagnostics.TextWriterTraceListener.dll";
                                String sruta_referencias_destino_20 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Diagnostics.Tools.dll";
                                String sruta_referencias_destino_21 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Diagnostics.TraceSource.dll";
                                String sruta_referencias_destino_22 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Diagnostics.Tracing.dll";
                                String sruta_referencias_destino_23 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Drawing.Common.dll";
                                String sruta_referencias_destino_24 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Drawing.Primitives.dll";
                                String sruta_referencias_destino_25 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Dynamic.Runtime.dll";
                                String sruta_referencias_destino_26 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Globalization.Calendars.dll";
                                String sruta_referencias_destino_27 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Globalization.dll";
                                String sruta_referencias_destino_28 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Globalization.Extensions.dll";
                                String sruta_referencias_destino_29 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.IO.Compression.dll";
                                String sruta_referencias_destino_30 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.IO.Compression.ZipFile.dll";
                                String sruta_referencias_destino_31 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.IO.dll";
                                String sruta_referencias_destino_32 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.IO.FileSystem.dll";
                                String sruta_referencias_destino_33 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.IO.FileSystem.DriveInfo.dll";
                                String sruta_referencias_destino_34 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.IO.FileSystem.Primitives.dll";
                                String sruta_referencias_destino_35 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.IO.FileSystem.Watcher.dll";
                                String sruta_referencias_destino_36 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.IO.IsolatedStorage.dll";
                                String sruta_referencias_destino_37 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.IO.MemoryMappedFiles.dll";
                                String sruta_referencias_destino_38 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.IO.Packaging.dll";
                                String sruta_referencias_destino_39 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.IO.Packaging.xml";
                                String sruta_referencias_destino_40 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.IO.Pipes.dll";
                                String sruta_referencias_destino_41 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.IO.UnmanagedMemoryStream.dll";
                                String sruta_referencias_destino_42 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Linq.dll";
                                String sruta_referencias_destino_43 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Linq.Expressions.dll";
                                String sruta_referencias_destino_44 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Linq.Parallel.dll";
                                String sruta_referencias_destino_45 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Linq.Queryable.dll";
                                String sruta_referencias_destino_46 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Memory.dll";
                                String sruta_referencias_destino_47 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Memory.xml";
                                String sruta_referencias_destino_48 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Net.Http.dll";
                                String sruta_referencias_destino_49 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Net.NameResolution.dll";
                                String sruta_referencias_destino_50 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Net.NetworkInformation.dll";
                                String sruta_referencias_destino_51 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Net.Ping.dll";
                                String sruta_referencias_destino_52 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Net.Primitives.dll";
                                String sruta_referencias_destino_53 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Net.Requests.dll";
                                String sruta_referencias_destino_54 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Net.Security.dll";
                                String sruta_referencias_destino_55 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Net.Sockets.dll";
                                String sruta_referencias_destino_56 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Net.WebHeaderCollection.dll";
                                String sruta_referencias_destino_57 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Net.WebSockets.Client.dll";
                                String sruta_referencias_destino_58 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Net.WebSockets.dll";
                                String sruta_referencias_destino_59 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.ObjectModel.dll";
                                String sruta_referencias_destino_60 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Reflection.dll";
                                String sruta_referencias_destino_61 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Reflection.Extensions.dll";
                                String sruta_referencias_destino_62 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Reflection.Primitives.dll";
                                String sruta_referencias_destino_63 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Resources.Reader.dll";
                                String sruta_referencias_destino_64 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Resources.ResourceManager.dll";
                                String sruta_referencias_destino_65 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Resources.Writer.dll";
                                String sruta_referencias_destino_66 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Runtime.CompilerServices.Unsafe.dll";
                                String sruta_referencias_destino_67 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Runtime.CompilerServices.Unsafe.xml";
                                String sruta_referencias_destino_68 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Runtime.CompilerServices.VisualC.dll";
                                String sruta_referencias_destino_69 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Runtime.dll";
                                String sruta_referencias_destino_70 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Runtime.Extensions.dll";
                                String sruta_referencias_destino_71 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Runtime.Handles.dll";
                                String sruta_referencias_destino_72 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Runtime.InteropServices.dll";
                                String sruta_referencias_destino_73 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Runtime.InteropServices.RuntimeInformation.dll";
                                String sruta_referencias_destino_74 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Runtime.Numerics.dll";
                                String sruta_referencias_destino_75 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Runtime.Serialization.Formatters.dll";
                                String sruta_referencias_destino_76 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Runtime.Serialization.Json.dll";
                                String sruta_referencias_destino_77 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Runtime.Serialization.Primitives.dll";
                                String sruta_referencias_destino_78 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Runtime.Serialization.Xml.dll";
                                String sruta_referencias_destino_79 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Security.Claims.dll";
                                String sruta_referencias_destino_80 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Security.Cryptography.Algorithms.dll";
                                String sruta_referencias_destino_81 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Security.Cryptography.Csp.dll";
                                String sruta_referencias_destino_82 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Security.Cryptography.Encoding.dll";
                                String sruta_referencias_destino_83 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Security.Cryptography.Primitives.dll";
                                String sruta_referencias_destino_84 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Security.Cryptography.X509Certificates.dll";
                                String sruta_referencias_destino_85 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Security.Principal.dll";
                                String sruta_referencias_destino_86 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Security.SecureString.dll";
                                String sruta_referencias_destino_87 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Text.Encoding.dll";
                                String sruta_referencias_destino_88 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Text.Encoding.Extensions.dll";
                                String sruta_referencias_destino_89 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Text.RegularExpressions.dll";
                                String sruta_referencias_destino_90 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Threading.dll";
                                String sruta_referencias_destino_91 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Threading.Overlapped.dll";
                                String sruta_referencias_destino_92 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Threading.Tasks.dll";
                                String sruta_referencias_destino_93 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Threading.Tasks.Parallel.dll";
                                String sruta_referencias_destino_94 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Threading.Thread.dll";
                                String sruta_referencias_destino_95 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Threading.ThreadPool.dll";
                                String sruta_referencias_destino_96 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Threading.Timer.dll";
                                String sruta_referencias_destino_97 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.ValueTuple.dll";
                                String sruta_referencias_destino_98 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Xml.ReaderWriter.dll";
                                String sruta_referencias_destino_99 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Xml.XDocument.dll";
                                String sruta_referencias_destino_100 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Xml.XmlDocument.dll";
                                String sruta_referencias_destino_101 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Xml.XmlSerializer.dll";
                                String sruta_referencias_destino_102 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Xml.XPath.dll";
                                String sruta_referencias_destino_103 = @"C:\Program Files (x86)\VADILLO&KING\CasosKing\System.Xml.XPath.XDocument.dll";




                                String srutadll1 = confilepth.sFileupload + @"\CasosKingV\System.AppContext.dll";
                                String srutadll2 = confilepth.sFileupload + @"\CasosKingV\System.Buffers.dll";
                                String srutadll3 = confilepth.sFileupload + @"\CasosKingV\System.Buffers.xml";
                                String srutadll4 = confilepth.sFileupload + @"\CasosKingV\System.Collections.Concurrent.dll";
                                String srutadll5 = confilepth.sFileupload + @"\CasosKingV\System.Collections.dll";
                                String srutadll6 = confilepth.sFileupload + @"\CasosKingV\System.Collections.NonGeneric.dll";
                                String srutadll7 = confilepth.sFileupload + @"\CasosKingV\System.Collections.Specialized.dll";
                                String srutadll8 = confilepth.sFileupload + @"\CasosKingV\System.ComponentModel.dll";
                                String srutadll9 = confilepth.sFileupload + @"\CasosKingV\System.ComponentModel.EventBasedAsync.dll";
                                String srutadll10 = confilepth.sFileupload + @"\CasosKingV\System.ComponentModel.Primitives.dll";
                                String srutadll11 = confilepth.sFileupload + @"\CasosKingV\System.ComponentModel.TypeConverter.dll";
                                String srutadll12 = confilepth.sFileupload + @"\CasosKingV\System.Console.dll";
                                String srutadll13 = confilepth.sFileupload + @"\CasosKingV\System.Data.Common.dll";
                                String srutadll14 = confilepth.sFileupload + @"\CasosKingV\System.Diagnostics.Contracts.dll";
                                String srutadll15 = confilepth.sFileupload + @"\CasosKingV\System.Diagnostics.Debug.dll";
                                String srutadll16 = confilepth.sFileupload + @"\CasosKingV\System.Diagnostics.FileVersionInfo.dll";
                                String srutadll17 = confilepth.sFileupload + @"\CasosKingV\System.Diagnostics.Process.dll";
                                String srutadll18 = confilepth.sFileupload + @"\CasosKingV\System.Diagnostics.StackTrace.dll";
                                String srutadll19 = confilepth.sFileupload + @"\CasosKingV\System.Diagnostics.TextWriterTraceListener.dll";
                                String srutadll20 = confilepth.sFileupload + @"\CasosKingV\System.Diagnostics.Tools.dll";
                                String srutadll21 = confilepth.sFileupload + @"\CasosKingV\System.Diagnostics.TraceSource.dll";
                                String srutadll22 = confilepth.sFileupload + @"\CasosKingV\System.Diagnostics.Tracing.dll";
                                String srutadll23 = confilepth.sFileupload + @"\CasosKingV\System.Drawing.Common.dll";
                                String srutadll24 = confilepth.sFileupload + @"\CasosKingV\System.Drawing.Primitives.dll";
                                String srutadll25 = confilepth.sFileupload + @"\CasosKingV\System.Dynamic.Runtime.dll";
                                String srutadll26 = confilepth.sFileupload + @"\CasosKingV\System.Globalization.Calendars.dll";
                                String srutadll27 = confilepth.sFileupload + @"\CasosKingV\System.Globalization.dll";
                                String srutadll28 = confilepth.sFileupload + @"\CasosKingV\System.Globalization.Extensions.dll";
                                String srutadll29 = confilepth.sFileupload + @"\CasosKingV\System.IO.Compression.dll";
                                String srutadll30 = confilepth.sFileupload + @"\CasosKingV\System.IO.Compression.ZipFile.dll";
                                String srutadll31 = confilepth.sFileupload + @"\CasosKingV\System.IO.dll";
                                String srutadll32 = confilepth.sFileupload + @"\CasosKingV\System.IO.FileSystem.dll";
                                String srutadll33 = confilepth.sFileupload + @"\CasosKingV\System.IO.FileSystem.DriveInfo.dll";
                                String srutadll34 = confilepth.sFileupload + @"\CasosKingV\System.IO.FileSystem.Primitives.dll";
                                String srutadll35 = confilepth.sFileupload + @"\CasosKingV\System.IO.FileSystem.Watcher.dll";
                                String srutadll36 = confilepth.sFileupload + @"\CasosKingV\System.IO.IsolatedStorage.dll";
                                String srutadll37 = confilepth.sFileupload + @"\CasosKingV\System.IO.MemoryMappedFiles.dll";
                                String srutadll38 = confilepth.sFileupload + @"\CasosKingV\System.IO.Packaging.dll";
                                String srutadll39 = confilepth.sFileupload + @"\CasosKingV\System.IO.Packaging.xml";
                                String srutadll40 = confilepth.sFileupload + @"\CasosKingV\System.IO.Pipes.dll";
                                String srutadll41 = confilepth.sFileupload + @"\CasosKingV\System.IO.UnmanagedMemoryStream.dll";
                                String srutadll42 = confilepth.sFileupload + @"\CasosKingV\System.Linq.dll";
                                String srutadll43 = confilepth.sFileupload + @"\CasosKingV\System.Linq.Expressions.dll";
                                String srutadll44 = confilepth.sFileupload + @"\CasosKingV\System.Linq.Parallel.dll";
                                String srutadll45 = confilepth.sFileupload + @"\CasosKingV\System.Linq.Queryable.dll";
                                String srutadll46 = confilepth.sFileupload + @"\CasosKingV\System.Memory.dll";
                                String srutadll47 = confilepth.sFileupload + @"\CasosKingV\System.Memory.xml";
                                String srutadll48 = confilepth.sFileupload + @"\CasosKingV\System.Net.Http.dll";
                                String srutadll49 = confilepth.sFileupload + @"\CasosKingV\System.Net.NameResolution.dll";
                                String srutadll50 = confilepth.sFileupload + @"\CasosKingV\System.Net.NetworkInformation.dll";
                                String srutadll51 = confilepth.sFileupload + @"\CasosKingV\System.Net.Ping.dll";
                                String srutadll52 = confilepth.sFileupload + @"\CasosKingV\System.Net.Primitives.dll";
                                String srutadll53 = confilepth.sFileupload + @"\CasosKingV\System.Net.Requests.dll";
                                String srutadll54 = confilepth.sFileupload + @"\CasosKingV\System.Net.Security.dll";
                                String srutadll55 = confilepth.sFileupload + @"\CasosKingV\System.Net.Sockets.dll";
                                String srutadll56 = confilepth.sFileupload + @"\CasosKingV\System.Net.WebHeaderCollection.dll";
                                String srutadll57 = confilepth.sFileupload + @"\CasosKingV\System.Net.WebSockets.Client.dll";
                                String srutadll58 = confilepth.sFileupload + @"\CasosKingV\System.Net.WebSockets.dll";
                                String srutadll59 = confilepth.sFileupload + @"\CasosKingV\System.ObjectModel.dll";
                                String srutadll60 = confilepth.sFileupload + @"\CasosKingV\System.Reflection.dll";
                                String srutadll61 = confilepth.sFileupload + @"\CasosKingV\System.Reflection.Extensions.dll";
                                String srutadll62 = confilepth.sFileupload + @"\CasosKingV\System.Reflection.Primitives.dll";
                                String srutadll63 = confilepth.sFileupload + @"\CasosKingV\System.Resources.Reader.dll";
                                String srutadll64 = confilepth.sFileupload + @"\CasosKingV\System.Resources.ResourceManager.dll";
                                String srutadll65 = confilepth.sFileupload + @"\CasosKingV\System.Resources.Writer.dll";
                                String srutadll66 = confilepth.sFileupload + @"\CasosKingV\System.Runtime.CompilerServices.Unsafe.dll";
                                String srutadll67 = confilepth.sFileupload + @"\CasosKingV\System.Runtime.CompilerServices.Unsafe.xml";
                                String srutadll68 = confilepth.sFileupload + @"\CasosKingV\System.Runtime.CompilerServices.VisualC.dll";
                                String srutadll69 = confilepth.sFileupload + @"\CasosKingV\System.Runtime.dll";
                                String srutadll70 = confilepth.sFileupload + @"\CasosKingV\System.Runtime.Extensions.dll";
                                String srutadll71 = confilepth.sFileupload + @"\CasosKingV\System.Runtime.Handles.dll";
                                String srutadll72 = confilepth.sFileupload + @"\CasosKingV\System.Runtime.InteropServices.dll";
                                String srutadll73 = confilepth.sFileupload + @"\CasosKingV\System.Runtime.InteropServices.RuntimeInformation.dll";
                                String srutadll74 = confilepth.sFileupload + @"\CasosKingV\System.Runtime.Numerics.dll";
                                String srutadll75 = confilepth.sFileupload + @"\CasosKingV\System.Runtime.Serialization.Formatters.dll";
                                String srutadll76 = confilepth.sFileupload + @"\CasosKingV\System.Runtime.Serialization.Json.dll";
                                String srutadll77 = confilepth.sFileupload + @"\CasosKingV\System.Runtime.Serialization.Primitives.dll";
                                String srutadll78 = confilepth.sFileupload + @"\CasosKingV\System.Runtime.Serialization.Xml.dll";
                                String srutadll79 = confilepth.sFileupload + @"\CasosKingV\System.Security.Claims.dll";
                                String srutadll80 = confilepth.sFileupload + @"\CasosKingV\System.Security.Cryptography.Algorithms.dll";
                                String srutadll81 = confilepth.sFileupload + @"\CasosKingV\System.Security.Cryptography.Csp.dll";
                                String srutadll82 = confilepth.sFileupload + @"\CasosKingV\System.Security.Cryptography.Encoding.dll";
                                String srutadll83 = confilepth.sFileupload + @"\CasosKingV\System.Security.Cryptography.Primitives.dll";
                                String srutadll84 = confilepth.sFileupload + @"\CasosKingV\System.Security.Cryptography.X509Certificates.dll";
                                String srutadll85 = confilepth.sFileupload + @"\CasosKingV\System.Security.Principal.dll";
                                String srutadll86 = confilepth.sFileupload + @"\CasosKingV\System.Security.SecureString.dll";
                                String srutadll87 = confilepth.sFileupload + @"\CasosKingV\System.Text.Encoding.dll";
                                String srutadll88 = confilepth.sFileupload + @"\CasosKingV\System.Text.Encoding.Extensions.dll";
                                String srutadll89 = confilepth.sFileupload + @"\CasosKingV\System.Text.RegularExpressions.dll";
                                String srutadll90 = confilepth.sFileupload + @"\CasosKingV\System.Threading.dll";
                                String srutadll91 = confilepth.sFileupload + @"\CasosKingV\System.Threading.Overlapped.dll";
                                String srutadll92 = confilepth.sFileupload + @"\CasosKingV\System.Threading.Tasks.dll";
                                String srutadll93 = confilepth.sFileupload + @"\CasosKingV\System.Threading.Tasks.Parallel.dll";
                                String srutadll94 = confilepth.sFileupload + @"\CasosKingV\System.Threading.Thread.dll";
                                String srutadll95 = confilepth.sFileupload + @"\CasosKingV\System.Threading.ThreadPool.dll";
                                String srutadll96 = confilepth.sFileupload + @"\CasosKingV\System.Threading.Timer.dll";
                                String srutadll97 = confilepth.sFileupload + @"\CasosKingV\System.ValueTuple.dll";
                                String srutadll98 = confilepth.sFileupload + @"\CasosKingV\System.Xml.ReaderWriter.dll";
                                String srutadll99 = confilepth.sFileupload + @"\CasosKingV\System.Xml.XDocument.dll";
                                String srutadll100 = confilepth.sFileupload + @"\CasosKingV\System.Xml.XmlDocument.dll";
                                String srutadll101 = confilepth.sFileupload + @"\CasosKingV\System.Xml.XmlSerializer.dll";
                                String srutadll102 = confilepth.sFileupload + @"\CasosKingV\System.Xml.XPath.dll";
                                String srutadll103 = confilepth.sFileupload + @"\CasosKingV\System.Xml.XPath.XDocument.dll";
                            try {
                                File.Copy(sruta_ref_uno, sruta_referencias_destino_uno, true);
                                File.Copy(sruta_ref_dos, sruta_referencias_destino_dos, true);
                                File.Copy(sruta_ref_tres, sruta_referencias_destino_tres, true);
                                File.Copy(sruta_ref_cuatro, sruta_referencias_destino_cuatro, true);
                                File.Copy(srutadll1, sruta_referencias_destino_1, true);
                                File.Copy(srutadll2, sruta_referencias_destino_2, true);
                                File.Copy(srutadll3, sruta_referencias_destino_3, true);
                                File.Copy(srutadll4, sruta_referencias_destino_4, true);
                                File.Copy(srutadll5, sruta_referencias_destino_5, true);
                                File.Copy(srutadll6, sruta_referencias_destino_6, true);
                                File.Copy(srutadll7, sruta_referencias_destino_7, true);
                                File.Copy(srutadll8, sruta_referencias_destino_8, true);
                                File.Copy(srutadll9, sruta_referencias_destino_9, true);
                                File.Copy(srutadll10, sruta_referencias_destino_10, true);
                                File.Copy(srutadll11, sruta_referencias_destino_11, true);
                                File.Copy(srutadll12, sruta_referencias_destino_12, true);
                                File.Copy(srutadll13, sruta_referencias_destino_13, true);
                                File.Copy(srutadll14, sruta_referencias_destino_14, true);
                                File.Copy(srutadll15, sruta_referencias_destino_15, true);
                                File.Copy(srutadll16, sruta_referencias_destino_16, true);
                                File.Copy(srutadll17, sruta_referencias_destino_17, true);
                                File.Copy(srutadll18, sruta_referencias_destino_18, true);
                                File.Copy(srutadll19, sruta_referencias_destino_19, true);
                                File.Copy(srutadll20, sruta_referencias_destino_20, true);
                                File.Copy(srutadll21, sruta_referencias_destino_21, true);
                                File.Copy(srutadll22, sruta_referencias_destino_22, true);
                                File.Copy(srutadll23, sruta_referencias_destino_23, true);
                                File.Copy(srutadll24, sruta_referencias_destino_24, true);
                                File.Copy(srutadll25, sruta_referencias_destino_25, true);
                                File.Copy(srutadll26, sruta_referencias_destino_26, true);
                                File.Copy(srutadll27, sruta_referencias_destino_27, true);
                                File.Copy(srutadll28, sruta_referencias_destino_28, true);
                                File.Copy(srutadll29, sruta_referencias_destino_29, true);
                                File.Copy(srutadll30, sruta_referencias_destino_30, true);
                                File.Copy(srutadll31, sruta_referencias_destino_31, true);
                                File.Copy(srutadll32, sruta_referencias_destino_32, true);
                                File.Copy(srutadll33, sruta_referencias_destino_33, true);
                                File.Copy(srutadll34, sruta_referencias_destino_34, true);
                                File.Copy(srutadll35, sruta_referencias_destino_35, true);
                                File.Copy(srutadll36, sruta_referencias_destino_36, true);
                                File.Copy(srutadll37, sruta_referencias_destino_37, true);
                                File.Copy(srutadll38, sruta_referencias_destino_38, true);
                                File.Copy(srutadll39, sruta_referencias_destino_39, true);
                                File.Copy(srutadll40, sruta_referencias_destino_40, true);
                                File.Copy(srutadll41, sruta_referencias_destino_41, true);
                                File.Copy(srutadll42, sruta_referencias_destino_42, true);
                                File.Copy(srutadll43, sruta_referencias_destino_43, true);
                                File.Copy(srutadll44, sruta_referencias_destino_44, true);
                                File.Copy(srutadll45, sruta_referencias_destino_45, true);
                                File.Copy(srutadll46, sruta_referencias_destino_46, true);
                                File.Copy(srutadll47, sruta_referencias_destino_47, true);
                                File.Copy(srutadll48, sruta_referencias_destino_48, true);
                                File.Copy(srutadll49, sruta_referencias_destino_49, true);
                                File.Copy(srutadll50, sruta_referencias_destino_50, true);
                                File.Copy(srutadll51, sruta_referencias_destino_51, true);
                                File.Copy(srutadll52, sruta_referencias_destino_52, true);
                                File.Copy(srutadll53, sruta_referencias_destino_53, true);
                                File.Copy(srutadll54, sruta_referencias_destino_54, true);
                                File.Copy(srutadll55, sruta_referencias_destino_55, true);
                                File.Copy(srutadll56, sruta_referencias_destino_56, true);
                                File.Copy(srutadll57, sruta_referencias_destino_57, true);
                                File.Copy(srutadll58, sruta_referencias_destino_58, true);
                                File.Copy(srutadll59, sruta_referencias_destino_59, true);
                                File.Copy(srutadll60, sruta_referencias_destino_60, true);
                                File.Copy(srutadll61, sruta_referencias_destino_61, true);
                                File.Copy(srutadll62, sruta_referencias_destino_62, true);
                                File.Copy(srutadll63, sruta_referencias_destino_63, true);
                                File.Copy(srutadll64, sruta_referencias_destino_64, true);
                                File.Copy(srutadll65, sruta_referencias_destino_65, true);
                                File.Copy(srutadll66, sruta_referencias_destino_66, true);
                                File.Copy(srutadll67, sruta_referencias_destino_67, true);
                                File.Copy(srutadll68, sruta_referencias_destino_68, true);
                                File.Copy(srutadll69, sruta_referencias_destino_69, true);
                                File.Copy(srutadll70, sruta_referencias_destino_70, true);
                                File.Copy(srutadll71, sruta_referencias_destino_71, true);
                                File.Copy(srutadll72, sruta_referencias_destino_72, true);
                                File.Copy(srutadll73, sruta_referencias_destino_73, true);
                                File.Copy(srutadll74, sruta_referencias_destino_74, true);
                                File.Copy(srutadll75, sruta_referencias_destino_75, true);
                                File.Copy(srutadll76, sruta_referencias_destino_76, true);
                                File.Copy(srutadll77, sruta_referencias_destino_77, true);
                                File.Copy(srutadll78, sruta_referencias_destino_78, true);
                                File.Copy(srutadll79, sruta_referencias_destino_79, true);
                                File.Copy(srutadll80, sruta_referencias_destino_80, true);
                                File.Copy(srutadll81, sruta_referencias_destino_81, true);
                                File.Copy(srutadll82, sruta_referencias_destino_82, true);
                                File.Copy(srutadll83, sruta_referencias_destino_83, true);
                                File.Copy(srutadll84, sruta_referencias_destino_84, true);
                                File.Copy(srutadll85, sruta_referencias_destino_85, true);
                                File.Copy(srutadll86, sruta_referencias_destino_86, true);
                                File.Copy(srutadll87, sruta_referencias_destino_87, true);
                                File.Copy(srutadll88, sruta_referencias_destino_88, true);
                                File.Copy(srutadll89, sruta_referencias_destino_89, true);
                                File.Copy(srutadll90, sruta_referencias_destino_90, true);
                                File.Copy(srutadll91, sruta_referencias_destino_91, true);
                                File.Copy(srutadll92, sruta_referencias_destino_92, true);
                                File.Copy(srutadll93, sruta_referencias_destino_93, true);
                                File.Copy(srutadll94, sruta_referencias_destino_94, true);
                                File.Copy(srutadll95, sruta_referencias_destino_95, true);
                                File.Copy(srutadll96, sruta_referencias_destino_96, true);
                                File.Copy(srutadll97, sruta_referencias_destino_97, true);
                                File.Copy(srutadll98, sruta_referencias_destino_98, true);
                                File.Copy(srutadll99, sruta_referencias_destino_99, true);
                                File.Copy(srutadll100, sruta_referencias_destino_100, true);
                                File.Copy(srutadll101, sruta_referencias_destino_101, true);
                                File.Copy(srutadll102, sruta_referencias_destino_102, true);
                                File.Copy(srutadll103, sruta_referencias_destino_103, true);
                            } catch (Exception exsd) {
                                new filelog("error al copiar la libreria", ""+exsd.Message);
                            }
                                



                                string path_destino2 = System.Reflection.Assembly.GetEntryAssembly().Location;
                                DateTime dFEchaactual = DateTime.Now;
                                File.Move(path_destino, path_destino.Replace("Facturador.exe", "Facturador_old_"+ dFEchaactual.ToString("yyyyMMddHHmmss") + ".exe")); ;
                                File.Copy(sruta_plantilla, path_destino, true);
                                
                                conect con_insert = new conect();
                                String sInsert = "INSERT INTO `actalizaversion`(`url`,`version`,`Actualizadoen`)VALUES('" + @path_destino.Replace("\\", "\\\\" ) + "', '" + sIdversionact + "', now());";
                                MySqlDataReader resp_insert = con_insert.getdatareader(sInsert);
                                if (resp_insert.RecordsAffected > 0)
                                {
                                    MessageBox.Show("Versión actualizada a " + sIdversionact);
                                    Process p = new Process();
                                    ProcessStartInfo psi = new ProcessStartInfo(path_destino);
                                    psi.Arguments = "";
                                    p.StartInfo = psi;
                                    p.Start();
                                    bBandera = true;

                                    lb_fechaversion.Text = sFechaversion;
                                    lbCompilaicon.Text = sIdversionact;
                                }
                                resp_insert.Close();
                                con_insert.Cerrarconexion();
                                if (bBandera) {
                                    this.Close();
                                }
                                

                            }
                        // }
                        new filelog("version db: "+ sIdversionact, " version soft: "+ sVersion);

                        //aquí buscaremos si hay una actualización

                        //String sRutas = "CasosKingV";

                        // Imprime algo como:
                        // C:\Users\sdkca\ConsoleApp1\ConsoleApp1\bin\Debug\ConsoleApp1.exe
                        //Console.WriteLine(path);
                        //this.Close();
                    }
                }
                resp_consltv.Close();
                con_filev.Cerrarconexion();


            }
            catch (Exception ex) {
                MessageBox.Show("Existe una versión para actualizar. \n\n"+ex.Message);
                new filelog("login", "Existe una versión para actualizar. \n\n" + ex.Message);

            }

                
        }

        private void lb_fechaversion_Click(object sender, EventArgs e)
        {

        }
    }
}
