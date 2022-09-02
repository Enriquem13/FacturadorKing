using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace emailking
{
    public partial class listviewcheck : Form
    {
        String valordetalle;
        public loading mensaje;
        public String sBodymail;
        public String sAsunto;
        public String strRutaArchivo = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        
        public listviewcheck( String sValor)
        {
            InitializeComponent();
            String valorEmailESP = "";
            String valorEmailENG = "";
            valordetalle = sValor;
            conect_robot con1 = new conect_robot();
            String sQueryselect = "USE JobConfig; Select * from job_config where job_config.UID_JobConfig = '" + valordetalle + "';";


           

            MySqlDataReader respues_detalle = con1.getdatareader(sQueryselect);
            while (respues_detalle.Read())
            {
                ListViewItem listaagregar = new ListViewItem(validareader("UID_JobConfig", "UID_JobConfig", respues_detalle).Text);
                listaagregar.SubItems.Add(validareader("TipoPlazoId", "UID_JobConfig", respues_detalle).Text);
                listaagregar.SubItems.Add(validareader("EstatusPlazoId", "UID_JobConfig", respues_detalle).Text);
                listaagregar.SubItems.Add(validareader("FieldDate", "UID_JobConfig", respues_detalle).Text);
                //listaagregar.SubItems.Add(validareader("Days", "UID_JobConfig", respues_detalle).Text);


                //string getemailname = validareader("GetEmailName", "UID_JobConfig", respues_detalle).Text;
                //MySqlCommand myCommand = new MySqlCommand(getemailname, con1.conecto());
                //myCommand.CommandTimeout = 3600;
                //myCommand.CommandType = System.Data.CommandType.StoredProcedure;
                //myCommand.CommandTimeout = 3600;
                //myCommand.Parameters.AddWithValue("@xCasoId", CasoId);
                //myCommand.Parameters.AddWithValue("@xUID_JobConfig", UID_JobConfig);
                //myCommand.Parameters.AddWithValue("@xIdiomaClave", IdiomaClave);

                //MySqlParameter myRetParam = new MySqlParameter();
                //myRetParam.Direction = System.Data.ParameterDirection.ReturnValue;
                //myCommand.Parameters.Add(myRetParam);


                ////Execute the function. ReturnValue parameter receives result of the stored function
                //myCommand.ExecuteNonQuery();
                //rstl = myRetParam.Value.ToString();

                //myConn.Dispose();
                //myCommand.Dispose();
                String respuesta = Warning_execute(validareader("UID_JobConfig", "UID_JobConfig", respues_detalle).Text, validareader("SPName", "UID_JobConfig", respues_detalle).Text, validareader("ViewName", "UID_JobConfig", respues_detalle).Text, validareader("FunctionName", "UID_JobConfig", respues_detalle).Text, Int32.Parse(validareader("Days", "UID_JobConfig", respues_detalle).Text));
                

                valorEmailESP = validareader("EmailES", "UID_JobConfig", respues_detalle).Text;
                valorEmailENG = validareader("EmailEN", "UID_JobConfig", respues_detalle).Text;

                lvDetalle.Items.Add(listaagregar);
            }
            respues_detalle.Close();
            //webBrowser1.Navigate("about:blank");
            //webBrowser1.Document.OpenNew(false);
            //webBrowser1.Document.Write(valorEmailESP);
            //webBrowser1.Refresh();

            //webBrowser2.Navigate("about:blank");
            //webBrowser2.Document.OpenNew(false);
            //webBrowser2.Document.Write(valorEmailENG);
            //webBrowser2.Refresh();
            
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
        public String FileUpName = "";
        public String FileUpNNET = "";
        private string Warning_execute(string UID_JobConfig, string SPName, string ViewName, string FunctionName, int Days)
        {
            string rstl = "";
            string fichero = strRutaArchivo+"\\conemailking.prop";
            string contenido = String.Empty;
            if (File.Exists(fichero))
            {
                contenido = File.ReadAllText(fichero);
                lineas = contenido.Split('\n');
            }
            else
            {

            }
            //String conexion = "server=" + lineas[0] + ";database=" + lineas[1] + ";Uid=" + lineas[2] + ";pwd=" + lineas[3] + ";";
            string con = "user id=" + lineas[2] + ";database=jobconfig"+ ";host=" + lineas[0] + ";password=" + lineas[3];
            try
            {
                MySqlConnection ConnWarning = new MySqlConnection(con);
                ConnWarning.Open();
                MySqlCommand myCommand = new MySqlCommand(SPName, ConnWarning);
                myCommand.CommandTimeout = 3600;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@xUID_JobConfig", UID_JobConfig);
                MySqlDataReader rWarning = myCommand.ExecuteReader();
                while (rWarning.Read())
                {
                    rstl = "";
                    int PlazoID = rWarning.GetInt32("PlazoID");
                    int CasoId = rWarning.GetInt32("CasoId");
                    int DocumentoId = rWarning.GetInt32("DocumentoId");
                    int EstatusPlazoId = rWarning.GetInt32("EstatusPlazoId");
                    string ContactoEmail = rWarning.GetString("ContactoEmail");
                    string IdiomaClave = rWarning.GetString("IdiomaClave");
                    string Subject = rWarning.GetString("Subject");
                    sAsunto = Subject;
                    string CasoNumero = rWarning.GetString("CasoNumero");
                    //int REC = Get_Rec(PlazoID);
                    string body = get_body(con, FunctionName, CasoId, UID_JobConfig, IdiomaClave);
                    sBodymail = body;
                    //String valorEmailESP = get_body(con, FunctionName, CasoId, UID_JobConfig, "ES");
                    //String valorEmailENG = get_body(con, FunctionName, CasoId, UID_JobConfig, "EN");
                    if (body != "")
                    {
                        FileUpName = "";
                        FileUpNNET = "";
                        
                        webBrowser1.Navigate("about:blank");
                        webBrowser1.Document.OpenNew(false);
                        webBrowser1.Document.Write(body);
                        webBrowser1.Refresh();

                        //webBrowser2.Navigate("about:blank");
                        //webBrowser2.Document.OpenNew(false);
                        //webBrowser2.Document.Write(valorEmailENG);
                        //webBrowser2.Refresh();
                        //Aquí enviamos el mail
                        //string send = SendEmail(ContactoEmail, Subject, body, CasoId, REC);
                        //if (send == "")
                        //{
                            //EventLog.WriteEntry("Email enviado exitosamente.", EventLogEntryType.Information);
                            //string UpdateMov = Update_Mov(con, UID_JobConfig, PlazoID, CasoId, DocumentoId, EstatusPlazoId, ViewName, FileUpName, FileUpNNET, CasoNumero, Days, REC);
                            //if (UpdateMov == "")
                                //EventLog.WriteEntry("Procesado Exitosamente", EventLogEntryType.Information);
                            //else
                            //{
                            //    rstl = "Error al actualizar la tabla de enviados. " + UpdateMov;
                                //EventLog.WriteEntry(rstl, EventLogEntryType.Information);
                            //}
                        //}
                        //else
                        //{
                            //rstl = "Error al envair el email. " + send;
                            //EventLog.WriteEntry(rstl, EventLogEntryType.Information);
                        //}
                        FileUpName = "";
                        FileUpNNET = "";
                    }
                    else
                    {
                        rstl = "No existe configuracion o mensaje para este email." + body;
                        //EventLog.WriteEntry(rstl, EventLogEntryType.Information);
                    }
                }
                rWarning.Close();
                rWarning.Dispose();
                myCommand.Dispose();
                ConnWarning.Dispose();

               // EventLog.WriteEntry("mail Send", EventLogEntryType.Information);
            }
            catch (Exception exception)
            {
                rstl = ViewName + " ERROR: " + con + exception.Message;
                MessageBox.Show("");
                //EventLog.WriteEntry(rstl, EventLogEntryType.Information);
            }
            return rstl;
        }
        private string get_body(string con, string GetEmailName, int CasoId, string UID_JobConfig, string IdiomaClave)
        {
            string rstl = "";
            try
            {
                MySqlConnection myConn = new MySqlConnection(con);
                myConn.Open();

                MySqlCommand myCommand = new MySqlCommand(GetEmailName, myConn);
                myCommand.CommandTimeout = 3600;
                myCommand.CommandType = System.Data.CommandType.StoredProcedure;
                myCommand.CommandTimeout = 3600;
                myCommand.Parameters.AddWithValue("@xCasoId", CasoId);
                myCommand.Parameters.AddWithValue("@xUID_JobConfig", UID_JobConfig);
                myCommand.Parameters.AddWithValue("@xIdiomaClave", IdiomaClave);

                MySqlParameter myRetParam = new MySqlParameter();
                myRetParam.Direction = System.Data.ParameterDirection.ReturnValue;
                myCommand.Parameters.Add(myRetParam);


                //Execute the function. ReturnValue parameter receives result of the stored function
                myCommand.ExecuteNonQuery();
                rstl = myRetParam.Value.ToString();

                myConn.Dispose();
                myCommand.Dispose();
            }
            catch (Exception e)
            {
                rstl = "Error al obtener el cuerpo del email: " + e.Message;
                //EventLog.WriteEntry(e.Message, EventLogEntryType.Information);
                throw;
            }
            return rstl;

        }

        public string[] lineas;
        private int Get_Rec(int PlazoId)
        {
            int Rslt = 0;

            MySqlConnectionStringBuilder conRec = new MySqlConnectionStringBuilder();
            string fichero = strRutaArchivo+"\\conemailking.prop";
            string contenido = String.Empty;
            if (File.Exists(fichero))
            {
                contenido = File.ReadAllText(fichero);
                lineas = contenido.Split('\n');
            }
            conRec.Server = lineas[0];
            conRec.UserID = lineas[2];
            conRec.Password = lineas[3];
            conRec.Database = lineas[1];

            MySqlConnection ConnRec = new MySqlConnection(conRec.ToString());
            ConnRec.Open();
            string qryRec = "select count(*) as REC from job_sended where PlazoId = " + PlazoId.ToString();

            MySqlCommand myCommand = new MySqlCommand(qryRec, ConnRec);
            myCommand.CommandTimeout = 3600;
            MySqlDataReader myData = myCommand.ExecuteReader();
            if (myData.HasRows)
            {
                while (myData.Read())
                {
                    Rslt = myData.GetInt32("REC");
                }
            }

            myData.Dispose();
            myCommand.Dispose();
            ConnRec.Dispose();

            return (Rslt + 1);
        }
        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (sBodymail != "")
            {

                

                //SmtpUser=mail@vadillo-king.mx
                //smtp.gmail.com


                //Credenciales KING
                String sMailsend = "mail@vadillo-king.mx";
                String sSmtpserver = "smtp.gmail.com";
                String sSmtpport = "465";//465
                String sSmtpuser = "mail@vadillo-king.mx";
                String sSmtppwd = "bety1976";
                
                
                
                //Credenciales Eduardo prueba
                //@dicss.com.mx
                //String sMailsend = "eduardor@dicss.com.mx";
                //String sSmtpserver = "mail.dicss.com.mx";
                //String sSmtpport = "587";
                //String sSmtpuser = "eduardor@dicss.com.mx";
                //String sSmtppwd = "Upiicsa1990";

                //@gmail.com
                //String sMailsend = "eramiref92@gmail.com";
                //String sSmtpserver = "smtp.gmail.com";
                //String sSmtpport = "465";
                //String sSmtpuser = "eramiref92@gmail.com";
                //String sSmtppwd = "Upiicsa1";

                //MailMessage email = new MailMessage();
                //email.To.Add(new MailAddress("eduardor@dicss.com.mx"));
                //email.From = new MailAddress(sMailsend);
                //email.Subject = sAsunto;
                ////email.Subject = "Asunto (Test: " + DateTime.Now.ToString("dd / MMM / yyy hh:mm:ss") + " ) ";
                //email.Body = sBodymail;
                //email.IsBodyHtml = true;
                //email.Priority = MailPriority.Normal;
                //Stream Logo = Get_Logo();
                //Attachment emailAttachments = new Attachment(Logo, "KingEmail.jpg", "image/gif");
                //email.Attachments.Add(emailAttachments);
                ////email.Attachments.Add(Logo, "KingEmail.jpg", "<12s4a8a8778c$5664i1b1$ir671781@tlffmdqjobxj>", "image/gif", null, NewAttachmentOptions.None, MailTransferEncoding.Base64);
                ////Smtp mailer = new Smtp();
                //SmtpClient smtp = new SmtpClient(sSmtpserver, Convert.ToInt32(sSmtpport));
                ////smtp.Host = "dicss.com.mx";
                ////smtp.Port = 465;
                //smtp.Timeout = 60000;
                //            //300000
                //smtp.UseDefaultCredentials = false;
                //smtp.Credentials = new System.Net.NetworkCredential(sSmtpuser, sSmtppwd);
                
                //smtp.EnableSsl = true;
                //try
                //{
                //    smtp.Send(email);
                //    email.Dispose();
                //    MessageBox.Show("Correo enviado");
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show("Error al enviar: " + ex);

                //}
                //String sMailsend = "mail@vadillo-king.mx";
                //String sSmtpserver = "smtp.gmail.com";
                //String sSmtpport = "465";//465
                //String sSmtpuser = "mail@vadillo-king.mx";
                //String sSmtppwd = "bety1976";
                using (SmtpClient smtpClient = new SmtpClient())
                {
                    var basicCredential = new NetworkCredential(sSmtpuser, sSmtppwd);
                    using (MailMessage message = new MailMessage())
                    {
                        MailAddress fromAddress = new MailAddress(sMailsend);

                        smtpClient.Host = sSmtpserver;
                        smtpClient.Port = 587;
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = basicCredential;
                        smtpClient.EnableSsl = true;

                        message.From = fromAddress;
                        message.Subject = sAsunto;
                        // Set IsBodyHtml to true means you can send HTML email.
                        message.IsBodyHtml = true;
                        message.Body = sBodymail;
                        message.To.Add(tbCorreoprueba.Text);
                        Stream Logo = Get_Logo();
                        Attachment emailAttachments = new Attachment(Logo, "KingEmail.jpg", "image/gif");
                        message.Attachments.Add(emailAttachments);
                        //message.Attachments.Add(Logo, "KingEmail.jpg", "<12s4a8a8778c$5664i1b1$ir671781@tlffmdqjobxj>", "image/gif", null, NewAttachmentOptions.None, MailTransferEncoding.Base64);

                        try
                        {
                            smtpClient.Send(message);

                            MessageBox.Show("Mensaje enviado correctamente");
                        }
                        catch (Exception ex)
                        {
                            //Error, could not send the message
                            //Response.Write(ex.Message);
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            else {
                MessageBox.Show("No se genero el correo, verifique por favor ");
            }
        }
        private Stream Get_Logo()
        {
            MySqlConnectionStringBuilder conImg = new MySqlConnectionStringBuilder();
            conImg.Server = "192.168.1.160";
            conImg.UserID = "root";
            conImg.Password = "Alejandra5m";
            conImg.Database = "jobconfig";

            MySqlConnection ConnIMG = new MySqlConnection(conImg.ToString());
            ConnIMG.Open();
            string qryIMG = "select p.PIMG from job_params p where p.Param = 'LOGO'";

            MySqlCommand myCommand = new MySqlCommand(qryIMG, ConnIMG);
            myCommand.CommandTimeout = 3600;
            MySqlDataReader myData;
            myData = myCommand.ExecuteReader();
            if (!myData.HasRows)
                throw new Exception("There are no BLOBs to save");

            myData.Read();

            byte[] imageBytes = (byte[])myData[0];
            Stream buf = new MemoryStream(imageBytes);


            myData.Dispose();
            myCommand.Dispose();
            ConnIMG.Dispose();

            return buf;
        }
    }
}
