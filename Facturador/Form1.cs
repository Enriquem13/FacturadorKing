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
        public String sVersion = "Versión Enrique";
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

                
        }

        private void lb_fechaversion_Click(object sender, EventArgs e)
        {

        }
    }
}
