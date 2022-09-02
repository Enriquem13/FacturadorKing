using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using MailBee;
using MailBee.Pop3Mail;
using MailBee.Mime;
using System.IO;

namespace Facturador
{
    class readmail
    {
        public MailMessage Mesageemail;
        public String[] readmailone()
        {
            MailBee.Global.LicenseKey = "MN110-8B8932A44B8239779277420FE843-E158";
            Pop3 pop = new Pop3();
            try{
                pop.Connect("mail.dicss.com.mx");
                pop.Login("eduardor@dicss.com.mx","Upiicsa1990");
                Console.WriteLine("Successfully logged in. __mail dicss ");
            }
            catch (MailBeePop3LoginNegativeResponseException ex)
            {
                Console.WriteLine("POP3 server replied with a negative response at login:" + ex.ToString());
            }
            //if (pop.IsLoggedIn && pop.InboxMessageCount > 1)
            //{
            //    MailMessage msg = pop.DownloadEntireMessage(pop.InboxMessageCount);
            //    try
            //    {
            //        if (!string.IsNullOrEmpty(msg.BodyHtmlText))
            //        {
            //            Console.WriteLine(msg.BodyHtmlText);
            //            Console.WriteLine("Asunto: " + msg.Subject);
            //            Console.WriteLine("Para: " + msg.To);
            //            Console.WriteLine("De: " + msg.From);
            //        }
            //        else if (!string.IsNullOrEmpty(msg.BodyPlainText))
            //        {
            //            Console.WriteLine(msg.BodyPlainText);
            //            Console.WriteLine("Asunto: " + msg.Subject);
            //            Console.WriteLine("Para: " + msg.To);
            //            Console.WriteLine("De: " + msg.From);
            //        }
            //        else
            //        {
            //            Console.WriteLine("The message body is empty.");
            //        }
            //    }catch(Exception E){
            //        Console.WriteLine("Error: "+E);
            //    }
            //}
            Console.WriteLine(pop.InboxMessageCount);
            string[]mensjaesids = pop.GetMessageUids();
            Console.WriteLine(mensjaesids[0]);
            MailMessageCollection msgs = pop.DownloadMessageHeaders();



            DateTime dt = new DateTime();
            String fechalog = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            StringBuilder sb = new StringBuilder();
            sb.Append("\n");
            String textFile = "c:\\facturador\\" + "mails.log";
            string[] lines= null;
            if (File.Exists(textFile))
            {
                // Read a text file line by line.
                lines = File.ReadAllLines(textFile);
            }
            string[] DifferArray = mensjaesids.Except(lines).ToArray();

            for (int y = DifferArray.Length - 1; y >= 0; y--)
            {
                //File.AppendAllText("c:\\facturador\\" + "mails.log", mensjaesids[y]+"\n");

                Mesageemail = pop.DownloadEntireMessage(pop.GetMessageIndexFromUid(DifferArray[y]));
                Console.WriteLine("Asunto: "+Mesageemail.Subject);
                Console.WriteLine("Mensaje: " + Mesageemail.BodyHtmlText);
                Console.WriteLine(pop.GetMessageIndexFromUid(DifferArray[y]));// GetMessageIndexFromUid();
            }
            sb.Clear();
            // For each message, write its details to the console
            //foreach (MailMessage msg in msgs)
            //{
            //    //Console.WriteLine("From: " + msg.From.AsString + ", To: " + msg.To.AsString);
            //    //Console.WriteLine("Subject: " + msg.Subject);
            //}
            pop.Disconnect();

            return DifferArray;
        }
        
    }
}
