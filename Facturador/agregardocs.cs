using Microsoft.Office.Interop.Word;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace Facturador
{
    class agregardocs
    {

        public void agregardocsgenera(){
            //object pbjmiss = System.Reflection.Missing.Value;
            //Word.Application objss = new Word.Application();
            ////docprueba.docx
            //string strRutaArchivo = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\casosking\" + "Cartas\\formatounopreba.docx";
            ////string strRutaArchivo = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\casosking\" + "docprueba.docx";
            //object parametro = strRutaArchivo;
            //object Nombre = "Texto65";
            //object valor2 = "telefono";
            //Word.Document ObjDoc = objss.Documents.Open(parametro, pbjmiss);
            //string bookmark = "Texto65";
            //Bookmark bm =  ObjDoc.Bookmarks[bookmark];
            //Range range = bm.Range;
            //range.Text = "valornuevo";
            //ObjDoc.Bookmarks.Add(bookmark,range);



            //Word.Range nom = ObjDoc.Bookmarks.get_Item(ref Nombre).Range;
            //for (int i = 1; i <= ObjDoc.Bookmarks.Count; i++)
            //{
            //    object objI = i;
            //    //here is your name, store it where ever you want:
            //    Console.WriteLine(ObjDoc.Bookmarks.get_Item(ref objI).Name);
            //    string sNombre = ObjDoc.Bookmarks.get_Item(ref objI).Name;
            //    object sobjectnombre = Nombre;
            //    Word.Range nom = ObjDoc.Bookmarks.get_Item(ref sobjectnombre).Range;
            //    //nom.F = "" + i;
            //    nom.Text = "123";
            //    object range1 = nom;
                
            //    //nom.FormattedText.Bookmarks.Add(sNombre, range1);
                

            //    ObjDoc.Bookmarks.Add(sNombre, range1);
                
            //}
            //Console.WriteLine("Hello World!");
            //ObjDoc.Close();
            //Word.Range nom2 = ObjDoc.Bookmarks.Add("nombre").Range;
            //nom2.Text = "valor nuevo";
            //object range1 = nom;
            //ObjDoc.Bookmarks.Add("1", range1);
            //objss.Visible = true;
            /*Nuevo fomrato*/
            //\\EDUARXIMO-PC\documentosserver\formatosconfigurables
            try {
                String sruta_plantilla = @"\\EDUARXIMO-PC\documentosserver\formatosconfigurables\IMPI-00-002_B.docx";
                String[] sArrays;
                Random r = new Random();
                int srandonm = r.Next(0, 99);
                String sArchivogenerado = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\casosking\" + "Nuevos_formatos\\IMPI-00-002_B_copia_"+srandonm+".docx";
                File.Copy(sruta_plantilla, sArchivogenerado);
                //String sruta = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\casosking\" + "Nuevos_formatos\\IMPI-00-002_B_copia.docx";
                //string[] readText = File.ReadAllLines(@"D:\CARTAP1.txt");
                Word.Application application = new Word.Application();
                Word.Document document = application.Documents.Open(sArchivogenerado);
                sArrays = new String[document.Bookmarks.Count];
                for (int i = 1; i <= document.Bookmarks.Count; i++)
                {
                    //obtenemos los nombres para despues obtener los remplazos por nombre, no por indice de nonbre porque ya no existe
                    object objI = i;
                    sArrays[i-1] = document.Bookmarks.get_Item(ref objI).Name;
                    Console.Write("Nombre: "+sArrays[i-1]+"\n");
                }
                application.Visible = true;
                int nomprueba = 0;
                for(int x=0; x<sArrays.Length;x++){
                    document.Bookmarks[sArrays[x]].Select();
                    application.Selection.TypeText("" + nomprueba);
                    
                    if (nomprueba == 9) {
                        nomprueba = 0;
                    }
                    nomprueba++;
                }
                //Console.Write("se escribe " + document.Bookmarks.get_Item(ref objI).Name + " en: "+i);
                document.Save();
                //application.Quit();
                
            }catch(Exception E){
                Console.Write("Error: " + E + "\n");
            }
        }
    }
}
