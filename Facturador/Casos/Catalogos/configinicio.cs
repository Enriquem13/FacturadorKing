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

namespace Facturador
{
    public partial class configinicio : Form
    {
        public string[] lineas;
        public configinicio()
        {
            InitializeComponent();
            String ruta = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\casosking";
            string fichero = ruta+"\\confacturador.prop";
            string contenido = String.Empty;
            if (File.Exists(fichero))
            {
                contenido = File.ReadAllText(fichero);
                lineas = contenido.Split('\n');
                tbHost.Text = lineas[0];
                tbDB.Text = lineas[1];
                tbUser.Text = lineas[2];
                tbContrasena.Text = lineas[3];
                tbUrldoc.Text = lineas[4];
                tbPuerto.Text = lineas[5];
            }
            else
            {
                MessageBox.Show("Llene lo campos");
            }
            
            //String conexion = "server=" + lineas[0] + ";database=" + lineas[1] + ";Uid=" + lineas[2] + ";pwd=" + lineas[3] + ";";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try{
                    //string ruta = "c:\\facturador";
                String ruta = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\casosking";
                    if (Directory.Exists(ruta))
                    {
                        //creamos el archivo 
                        

                        StreamWriter escrito = File.CreateText(ruta+"\\confacturador.prop"); // en el 
                        String sArchivoconf = tbHost.Text + "\n" + tbDB.Text + "\n" + tbUser.Text + "\n" + tbContrasena.Text + "\n" + tbUrldoc.Text + "\n" + tbPuerto.Text;
                        String contenido = sArchivoconf;
                        escrito.Write(contenido.ToString());
                        escrito.Flush();
                        escrito.Close();
                    }else{
                        //Creamos la carpeta
                        System.IO.Directory.CreateDirectory(ruta);
                        StreamWriter escrito = File.CreateText(ruta + "\\confacturador.prop"); // en el 
                        String sArchivoconf = tbHost.Text + "\n" + tbDB.Text + "\n" + tbUser.Text + "\n" + tbContrasena.Text + "\n" + tbUrldoc.Text + "\n" + tbPuerto.Text;
                    String contenido = sArchivoconf;
                        escrito.Write(contenido.ToString());
                        escrito.Flush();
                        escrito.Close();
                        
                    }
                    MessageBox.Show("Configuración guardada exitosamente");
                    this.Close();
                }catch(Exception E){
                    MessageBox.Show("Error al intentar guardar la configuración: "+E);

                }
                //StreamWriter escritos = File.CreateText(@"\\192.168.1.95\documentosserver\confacturador.prop"); // en el 
                //String sArchivoconfs = tbHost.Text + "\n" + tbDB.Text + "\n" + tbUser.Text + "\n" + tbContrasena.Text;
                //String contenidos = sArchivoconfs;
                //escritos.Write(contenidos.ToString());
                //escritos.Flush();
                //escritos.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
