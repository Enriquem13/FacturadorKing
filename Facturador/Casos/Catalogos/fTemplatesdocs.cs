//using iTextSharp.text;
//using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Facturador
{
    public partial class fTemplatesdocs : Form
    {
        public Boolean bBandera = false;
        public Boolean bBandera1 = false;
        public Boolean bBandera2 = false;
        public Boolean bBandera3 = false;
        public Boolean bBandera4 = false;
        public Boolean bBandera5 = false;
        public Boolean bBandera6 = false;
        public Boolean bBandera7 = false;
        public Boolean bBandera8 = false;
        public Boolean bBandera9 = false;
        public Boolean bBandera10 = false;
        public Boolean bBandera11 = false;
        public Boolean bBandera12 = false;
        public Process myProcess;
        public fTemplatesdocs()
        {
            InitializeComponent();
        }

        private void fTemplatesdocs_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try {
                var fileContent = string.Empty;
                var filePath = string.Empty;

                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = "c:\\facturador";
                    openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    openFileDialog.FilterIndex = 2;
                    openFileDialog.RestoreDirectory = true;
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //Get the path of specified file
                        filePath = openFileDialog.FileName;
                        //Read the contents of the file into a stream
                        //var fileStream = openFileDialog.OpenFile();
                        //using (StreamReader reader = new StreamReader(fileStream))
                        //{
                        //    fileContent = reader.ReadToEnd();
                        //}
                    }
                }

                string fileName = "imagen.png";
                string sourcePath = @"C:\Users\Public\TestFolder";
                string targetPath = @"C:\facturador\templates";

                // Use Path class to manipulate file and directory paths.
                //string sourceFile = System.IO.Path.Combine(filePath, fileName);
                string destFile = System.IO.Path.Combine(targetPath, fileName);

                // To copy a folder's contents to a new location:
                // Create a new target folder, if necessary.
                if (!System.IO.Directory.Exists(targetPath))
                {
                    System.IO.Directory.CreateDirectory(targetPath);
                }

                // To copy a file to another location and 
                // overwrite the destination file if it already exists.
                System.IO.File.Copy(filePath, destFile, true);
                
                System.Drawing.Image myimage = new Bitmap(destFile);

                pictureBox1.BackgroundImage = myimage;
                pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;

                //////this.BackgroundImage =
                // To copy all the files in one directory to another directory.
                // Get the files in the source folder. (To recursively iterate through
                // all subfolders under the current directory, see
                // "How to: Iterate Through a Directory Tree.")
                // Note: Check for target path was performed previously
                //       in this code example.
                //if (System.IO.Directory.Exists(sourcePath))
                //{
                //    string[] files = System.IO.Directory.GetFiles(sourcePath);

                //    // Copy the files and overwrite destination files if they already exist.
                //    foreach (string s in files)
                //    {
                //        // Use static Path methods to extract only the file name from the path.
                //        fileName = System.IO.Path.GetFileName(s);
                //        destFile = System.IO.Path.Combine(targetPath, fileName);
                //        System.IO.File.Copy(s, destFile, true);
                //    }
                //}
                //else
                //{
                //    MessageBox.Show("La ubicación es incorrecta");
                //}
            }
            catch (Exception E)
            {
                MessageBox.Show(""+E);
            }

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {//para mover dentro de la imagen
            if (bBandera)
                label1.Location = new Point(e.X, e.Y);
            if (bBandera2)
                label4.Location = new Point(e.X, e.Y);
        }

        private void fTemplatesdocs_MouseMove(object sender, MouseEventArgs e)
        {
            if (bBandera){
                label1.Location = new Point(e.X, e.Y);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            bBandera2 = false;
        }

        private void label4_DoubleClick(object sender, EventArgs e)
        {
            bBandera2 = true;
        }
        private void label1_Click(object sender, EventArgs e)
        {
            bBandera = false;
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            bBandera = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int iXcoor = label4.Location.X;
            int iYcoor = label4.Location.Y;
            myProcess = new Process();
            //createPDF("caso_", @"C:\facturador\archivos\", iXcoor, iYcoor);
            //MessageBox.Show("las coordenadas del label1 son:" + label1.Location.X + " Y: " + label1.Location.Y + "\nlas coordenadas del label4 son:" + label4.Location.X + " Y: " + label4.Location.Y);
            MessageBox.Show("la plantilla se genero correctamente");
            
        }
        //private void createPDF(String tempfilename, String newPath, int iXcoor, int iYcoor)
        //{
        //    //MessageBox.Show("PDF");
        //    BaseFont fuenteText = FontFactory.GetFont(FontFactory.HELVETICA, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL).BaseFont;
        //    //String timeStamp = GetTimestamp(DateTime.Now);
        //    Random random = new Random();
        //    int randomNumber = random.Next(0, 100);
        //    string newFileName = tempfilename + "_" + randomNumber + ".pdf";
        //    newPath = System.IO.Path.Combine(newPath, newFileName);
        //    //int sizeofwrite = random.Next(1, 10000);

        //    if (!System.IO.File.Exists(newPath))
        //    {
        //        // step 1: creation of a document-object
        //        iTextSharp.text.Document myDocument = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 0, 0, 0, 0);

        //        try
        //        {
        //            // step 2:
        //            // Now create a writer that listens to this doucment and writes the document to desired Stream.
        //            PdfWriter pdfwriter = iTextSharp.text.pdf.PdfWriter.GetInstance(myDocument, new FileStream(newPath, FileMode.Create, FileAccess.Write, FileShare.None));
        //            //PdfWriter pdfw = PdfWriter.GetInstance(myDocument, new FileStream("C:\Reporte\Cedes_constancia_deposito__2.pdf",FileMode.Create, FileAccess.Write, FileShare.None));
        //            // step 3:  Open the document now using
        //            myDocument.Open();

        //            //agregamos la imagen de encabezado
        //            //myDocument.Add(new Paragraph("JPG"));
        //            //string url = @"C:\facturador\img\encabezadoking.jpg";
                    
        //            iTextSharp.text.Image tif = iTextSharp.text.Image.GetInstance(@"C:\facturador\img\formato.png");
        //            tif.ScalePercent(100f);
        //            tif.SetAbsolutePosition(0,0);

        //            myDocument.Add(tif);

        //            PdfContentByte recNoSolicitud = pdfwriter.DirectContent;
        //            recNoSolicitud.SetLineWidth(1);
        //            recNoSolicitud.SetColorStroke(BaseColor.BLACK);

        //            PdfContentByte cb = pdfwriter.DirectContent;
                    
        //            cb.BeginText();
        //            cb.SetFontAndSize(fuenteText, 11);
        //            cb.SetColorFill(iTextSharp.text.BaseColor.DARK_GRAY);
        //            //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "FORMATO ELECTRÓNICO DE PAGOS POR SERVICIO", PageSize.A4.Width/4, PageSize.A4.Height - 34, 0);
        //            //cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, titulo, PageSize.A4.Width / 2, PageSize.A4.Height - 34, 0);
        //            //Agregamos titulos de la plantilla
        //            cb.SetFontAndSize(fuenteText, 6);
        //            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "MZM, S.A. de C.V.", iXcoor, PageSize.A4.Height - iYcoor, 0);
        //            //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "FOLIO", 475, PageSize.A4.Height - 150, 0);
        //            ////AQUÍ PODEMOS AGREGAR EL FOLIO DE LA FACTURA
                    
        //            //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "FECHA  ", 475, 635, 0);
                    

        //            //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "IMPORTE ", 475, PageSize.A4.Height - 275, 0);

        //            //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "CLIENTE ", PageSize.A4.Width / 3, PageSize.A4.Height - 150, 0);
        //            //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "DESCRIPCIÓN ", PageSize.A4.Width / 3, PageSize.A4.Height - 275, 0);

        //            //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "SUBTOTAL ", 380, 218, 0);
        //            //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "I.V.A.  ", 400, 185, 0);
        //            //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "TOTAL ", 400, 152, 0);

        //            ////AQUÍ AGREAMOS LOS VALORES 
        //            //cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "$" + 1000, 530, 218, 0);
        //            //cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "$" + 160, 530, 185, 0);
        //            //cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "$" + 1160, 530, 152, 0);


        //            //cb.SetFontAndSize(fuenteText, 8);
                    
        //            //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Interesado: ", 37, PageSize.A4.Height - 529, 0);
                    
        //            //cb.SetFontAndSize(fuenteText, 6);
                    

        //            ////float iPosiciony = PageSize.A4.Height - 251;
        //            ////for (int x = 0; x <= rows / 106; x++) {
        //            ////    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "" + comboBox1.Text, 57, iPosiciony, 0);
        //            ////    iPosiciony += 11;
        //            ////}


        //            ////DATOS DEL CLIENTE
                    
        //            //cb.SetFontAndSize(fuenteText, 9);

        //            cb.EndText();
        //            pdfwriter.Flush();
                    

        //            cb.SetFontAndSize(fuenteText, 11);
        //            cb.SetColorFill(iTextSharp.text.BaseColor.DARK_GRAY);
                    
        //            cb.SetFontAndSize(fuenteText, 10);
        //           //MessageBox.Show("tamaño en x," + myDocument.PageSize.Width + " tamaño en y:" + myDocument.PageSize.Height);
                    
                    
        //            cb.EndText();
        //            pdfwriter.Flush();
                    
        //        }
        //        catch (iTextSharp.text.DocumentException de)
        //        {
        //            Console.Error.WriteLine(de.Message);
        //        }
        //        catch (IOException ioe)
        //        {
        //            Console.Error.WriteLine(ioe.Message);
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.Error.WriteLine(ex.Message);
        //        }
        //        finally
        //        {
        //            // step 5: Remember to close the documnet
        //            MessageBox.Show("El archivo fué creado en: " + newPath.ToString() + System.Environment.NewLine);
        //            myDocument.Close();
        //            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //            //myProcess.StartInfo.Verb = "print";
        //            myProcess.StartInfo.CreateNoWindow = true;
        //            //URL del documento
        //            string Val = "" + newPath;
        //            Val.Replace("\\\\", "\\");
        //            myProcess.StartInfo.FileName = Val;
        //            myProcess.Start();
        //            myProcess.WaitForExit(1000);
        //            //myProcess.CloseMainWindow();
        //            //myProcess.Close();

        //            //textBox4.AppendText("Created file: " + newPath.ToString() + System.Environment.NewLine + "SIZE: " + sizeofwrite.ToString() + "\r\n\r\n");
        //        }
        //    }
        //}
    }
}
