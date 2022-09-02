using MailBee.Mime;
using MailBee.Pop3Mail;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Facturador
{
    public partial class corrreospendientes : Form
    {
        public MailMessage Mesageemail;
        public corrreospendientes(string [] correos)
        {
            InitializeComponent();
            listView1.FullRowSelect = true;
            MailBee.Global.LicenseKey = "MN110-8B8932A44B8239779277420FE843-E158";
            Pop3 pop = new Pop3();
            try
            {
                pop.Connect("mail.dicss.com.mx");
                pop.Login("eduardor@dicss.com.mx", "Upiicsa1990");
                Console.WriteLine("Successfully logged in. __mail dicss ");
            }
            catch (MailBeePop3LoginNegativeResponseException ex)
            {
                Console.WriteLine("POP3 server replied with a negative response at login:" + ex.ToString());
            }

            for (int y = correos.Length - 1; y >= 0; y--)
            {
                Mesageemail = pop.DownloadEntireMessage(pop.GetMessageIndexFromUid(correos[y]));
                Console.WriteLine("Asunto: " + Mesageemail.Subject);
                Console.WriteLine("Mensaje: " + Mesageemail.BodyPlainText);
                Console.WriteLine(pop.GetMessageIndexFromUid(correos[y]));

                ListViewItem newitem = new ListViewItem(correos[y]);
                newitem.SubItems.Add(pop.GetMessageIndexFromUid(correos[y]).ToString());
                newitem.SubItems.Add(Mesageemail.Subject);
                newitem.SubItems.Add(Mesageemail.BodyPlainText);
                newitem.SubItems.Add("Adjutno");
                listView1.Items.Add(newitem);

            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
