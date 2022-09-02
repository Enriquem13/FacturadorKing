using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;

namespace Facturador.Casos.Documentos
{
    class generacarta
    {
        public String sMensajeerror = "";

        public generacarta(String sNombrecartaplantilla, String sSubtipodocumentoid, view_caso_marcas objmarca) {
            try
            {
                
                funcionesdicss objfun = new funcionesdicss();
                String[] sNombre = sNombrecartaplantilla.Split('.');
                configuracionfiles confilepth = new configuracionfiles();
                confilepth.configuracionfilesinicio();
                String sruta_plantilla = confilepth.sFileupload + @"\formatosconfigurables\" + sNombrecartaplantilla;
                String carpeta = "C:\\Formatos_CasosKing";
                //si no existe la carpeta temporal la creamos
                if (!(Directory.Exists(carpeta)))
                {
                    Directory.CreateDirectory(carpeta);
                }
                Random r = new Random();
                int srandonm = r.Next(9, 9999);
                DateTime dDatenow = DateTime.Now;
                String sArchivogenerado = carpeta + "\\" + sNombre[0] + dDatenow.ToString("yyyy_MM_dd") + "_" + srandonm + ".doc";
                File.Copy(sruta_plantilla, sArchivogenerado);
                //abrimos el archivo temporal y lo reemplzamos con los datos
                object oMissing = System.Reflection.Missing.Value;
                Word.Application application = new Word.Application();
                Word.Document document = application.Documents.Open(sArchivogenerado);

                String sValor = "";
                int iCountbookmarks = document.Bookmarks.Count;
                do
                {
                    sValor = document.Bookmarks[iCountbookmarks].Name;
                    conect con_carta = new conect();
                    String sQuery_carta = " SELECT CampoTabla FROM caso_campossubtipodocumentos where CampoDocumento= '" + sValor + "' and  grupoid = 2";
                    MySqlDataReader respuesta_carta = con_carta.getdatareader(sQuery_carta);
                    while (respuesta_carta.Read())
                    {
                        try
                        {
                            //document.Bookmarks.Count
                            bool bValor = document.Bookmarks.Exists(sValor);//[objfun.validareader("SubTipoDocumentoTemplateEspanol", "SubTipoDocumentoTemplateEspanol", respuesta_carta).Text].Column;//.Select();
                            if (bValor)
                            {
                                document.Bookmarks[sValor].Select();
                                //consultamos el dato de la base de datos con get del objeto
                                //traemos el dato de el objeto marca
                                String valorcampodb = objmarca.getAtributo(objfun.validareader("CampoTabla", "CampoTabla", respuesta_carta).Text);
                                if (valorcampodb=="") {
                                    valorcampodb = " ";
                                }
                                //String valorcampodb = objmarca.getAtributo(objfun.validareader("CampoTabla", "CampoTabla", respuesta_carta).Text);
                                application.Selection.TypeText(valorcampodb);
                            }
                        }
                        catch (Exception exs)
                        {
                            new filelog("error", "Error:" + exs.Message);
                        }

                        String svalor = "";
                        //application.Selection.TypeText("Valor nuevo ");

                        //sCartanombreESPfile = objfun.validareader("SubTipoDocumentoTemplateEspanol", "SubTipoDocumentoTemplateEspanol", respuesta_carta).Text;
                        //sCartanombreENfile = objfun.validareader("SubTipoDocumentoTemplateIngles", "SubTipoDocumentoTemplateIngles", respuesta_carta).Text;
                        //Aquí podemos validar el idioma y si existe la plantilla
                    }
                    respuesta_carta.Close();
                    con_carta.Cerrarconexion();
                    iCountbookmarks--;
                } while (iCountbookmarks > 0);//(int x = 1; x <= iCountbookmarks; x++)
                                              //object FindText = "sample";
                                              //object ReplaceWith = "replacement";
                                              //object Replace = Word.WdReplace.wdReplaceAll;
                                              //object Forward = true;
                                              //((Word._Document)application.ActiveDocument).Paragraphs[1].Range.InsertParagraphBefore();
                                              //Word.Bookmark bookmark1 = ((Word._Document)application.ActiveDocument).Controls.AddBookmark(((Word._Document)application.ActiveDocument).Paragraphs[1].Range, "bookmark1");
                                              //bookmark1.Text = "This is sample bookmark text.";
                                              ////MessageBox.Show("Bookmark text before using Find method: " + bookmark1.Text);
                                              //ref missing;
                                              //bookmark1.Find.Execute(ref FindText, ref missing, ref missing,
                                              //    ref missing, ref missing, ref Forward, ref missing, ref
                                              //    missing, ref missing, ref ReplaceWith, ref Replace, ref missing,
                                              //    ref missing, ref missing, ref missing);

                /*Aquí agregaremos una imagen*/
                try {
                    conect con_carta_logo = new conect();
                    String sQuery_carta_logo = "SELECT * FROM casos_king.imagen_logo where casoid = "+ objmarca.sValorescampos[0][1] + " and TipoSolicitudId = "+ objmarca.getAtributo("TipoSolicitudId") +
                                                " order by idimagen_logoId desc limit 1;";
                    MySqlDataReader respuesta_carta_logo = con_carta_logo.getdatareader(sQuery_carta_logo);
                    while (respuesta_carta_logo.Read())
                    {

                        string imagePath = @objfun.validareader("ruta", "ruta", respuesta_carta_logo).Text; // @"C:\facturador_lalo\029875.gif";// uno.jpg";// c:\temp\win10.jpg";
                        object o_CollapseEnd = Word.WdCollapseDirection.wdCollapseEnd;
                        Word.Range imgrng = document.Content;

                        imgrng.Collapse(ref o_CollapseEnd);
                        //imgrng.InlineShapes.AddPicture(imagePath, oMissing, oMissing, imgrng);

                        ////Word.Application wordApp = new Word.Application();
                        ////Word.Document wordDoc = wordApp.Documents.Add();
                        Word.Range docRange = document.Range();

                        //// Create an InlineShape in the InlineShapes collection where the picture should be added later
                        //// It is used to get automatically scaled sizes.
                        //Word.InlineShape autoScaledInlineShape = imgrng.InlineShapes.AddPicture(imagePath);
                        //float scaledWidth = autoScaledInlineShape.Width;
                        //float scaledHeight = autoScaledInlineShape.Height;
                        //autoScaledInlineShape.Delete();

                        ////// Create a new Shape and fill it with the picture
                        Word.Shape newShape = document.Shapes.AddShape(1, 380, 130, 100, 100);
                        //newShape.Width = 0;
                        //Word.LineFormat lineFormat =;
                        Console.WriteLine(newShape.Line.EndArrowheadStyle.ToString());
                        newShape.Line.Visible = 0;
                        //Console.WriteLine();

                        //newShape.Line.BackColor = 

                        //newShape.Line.ForeColor = 0;//.Fill.UserTextured("");
                        newShape.Fill.UserPicture(imagePath);
                        

                        //// Convert the Shape to an InlineShape and optional disable Border
                        //Word.InlineShape finalInlineShape = newShape.ConvertToInlineShape();
                        //finalInlineShape.Line.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        //document.Bookmarks[sValor].Select();
                        //// Cut the range of the InlineShape to clipboard
                        //finalInlineShape.Range.Cut();


                        //application.Selection.CopyAsPicture();
                        //// And paste it to the target Range
                        //docRange.Paste();
                    }
                    respuesta_carta_logo.Close();
                    con_carta_logo.Cerrarconexion();


                    
                }
                catch (Exception exs) {
                    new filelog("", "Al consultar logo marcas: " + exs.Message);
                }
                
                /* FIN ... Aquí agregaremos una imagen*/

                application.Visible = true;
                document.Save();
                //application.Quit();
                //((Word._Document)application.ActiveDocument).Close();
            }
            catch (Exception ex) {
                sMensajeerror = ex.Message;
                new filelog("error", "Error:"+ex.Message);
            }
            
        }


        //private void BookmarkFind()
        //{
        //    object FindText = "sample";
        //    object ReplaceWith = "replacement";
        //    object Replace = Word.WdReplace.wdReplaceAll;
        //    object Forward = true;
        //    ((Word._Document)application.ActiveDocument).Paragraphs[1].Range.InsertParagraphBefore();
        //    Word.Bookmark bookmark1 = this.Controls.AddBookmark(this.Paragraphs[1].Range,"bookmark1");
        //    bookmark1.Text = "This is sample bookmark text.";
        //    MessageBox.Show("Bookmark text before using Find method: " + bookmark1.Text);

        //    bookmark1.Find.Execute(ref FindText, ref missing, ref missing,
        //        ref missing, ref missing, ref Forward, ref missing, ref
        //        missing, ref missing, ref ReplaceWith, ref Replace, ref missing,
        //        ref missing, ref missing, ref missing);

        //    MessageBox.Show("Bookmark text after using Find method: " + bookmark1.Text);
        //}

    }

    class generacarta_pat
    {
        public String sMensajeerror = "";
        public generacarta_pat(String sNombrecartaplantilla, String sSubtipodocumentoid, view_caso_patentes objpatente)
        {
            try
            {
                funcionesdicss objfun = new funcionesdicss();
                String[] sNombre = sNombrecartaplantilla.Split('.');
                configuracionfiles confilepth = new configuracionfiles();
                confilepth.configuracionfilesinicio();
                String sruta_plantilla = confilepth.sFileupload + @"\formatosconfigurables\" + sNombrecartaplantilla;
                String carpeta = "C:\\Formatos_CasosKing";
                //si no existe la carpeta temporal la creamos
                if (!(Directory.Exists(carpeta)))
                {
                    Directory.CreateDirectory(carpeta);
                }
                Random r = new Random();
                int srandonm = r.Next(9, 9999);
                DateTime dDatenow = DateTime.Now;
                String sArchivogenerado = carpeta + "\\" + sNombre[0] + dDatenow.ToString("yyyy_MM_dd") + "_" + srandonm + ".doc";
                File.Copy(sruta_plantilla, sArchivogenerado);
                //abrimos el archivo temporal y lo reemplzamos con los datos
                Word.Application application = new Word.Application();
                Word.Document document = application.Documents.Open(sArchivogenerado);

                String sValor = "";
                int iCountbookmarks = document.Bookmarks.Count;
                do
                {

                    sValor = document.Bookmarks[iCountbookmarks].Name;
                    conect con_carta = new conect();
                    String sQuery_carta = " SELECT CampoTabla FROM caso_campossubtipodocumentos where CampoDocumento= '" + sValor + "' and  grupoid = 1";
                    MySqlDataReader respuesta_carta = con_carta.getdatareader(sQuery_carta);
                    while (respuesta_carta.Read())
                    {
                        try
                        {
                            bool bValor = document.Bookmarks.Exists(sValor);//[objfun.validareader("SubTipoDocumentoTemplateEspanol", "SubTipoDocumentoTemplateEspanol", respuesta_carta).Text].Column;//.Select();
                            if (bValor)
                            {
                                document.Bookmarks[sValor].Select();
                                String valorcampodb = objpatente.getAtributo(objfun.validareader("CampoTabla", "CampoTabla", respuesta_carta).Text);
                                application.Selection.TypeText(valorcampodb);
                            }
                        }
                        catch (Exception exs)
                        {
                            new filelog("error", "Error:" + exs.Message);
                        }

                        String svalor = "";
                    }
                    respuesta_carta.Close();
                    con_carta.Cerrarconexion();
                    iCountbookmarks--;
                } while (iCountbookmarks > 0);
                application.Visible = true;
                document.Save();
                //application.Quit();
                //((Word._Document)application.ActiveDocument).Close();
            }
            catch (Exception ex)
            {
                sMensajeerror = ex.Message;
                new filelog("error", "Error:" + ex.Message);
            }
        }
    }

    //genera carta contencioso 
    class generacarta_contencioso
    {
        public String sMensajeerror = "";
        public generacarta_contencioso(String sNombrecartaplantilla, String sSubtipodocumentoid, view_caso_contencioso objpatente)
        {
            try
            {
                funcionesdicss objfun = new funcionesdicss();
                String[] sNombre = sNombrecartaplantilla.Split('.');
                configuracionfiles confilepth = new configuracionfiles();
                confilepth.configuracionfilesinicio();
                String sruta_plantilla = confilepth.sFileupload + @"\formatosconfigurables\" + sNombrecartaplantilla;
                String carpeta = "C:\\Formatos_CasosKing";
                //si no existe la carpeta temporal la creamos
                if (!(Directory.Exists(carpeta)))
                {
                    Directory.CreateDirectory(carpeta);
                }
                Random r = new Random();
                int srandonm = r.Next(9, 9999);
                DateTime dDatenow = DateTime.Now;
                String sArchivogenerado = carpeta + "\\" + sNombre[0] + dDatenow.ToString("yyyy_MM_dd") + "_" + srandonm + ".doc";
                File.Copy(sruta_plantilla, sArchivogenerado);
                //abrimos el archivo temporal y lo reemplzamos con los datos
                Word.Application application = new Word.Application();
                Word.Document document = application.Documents.Open(sArchivogenerado);

                String sValor = "";
                int iCountbookmarks = document.Bookmarks.Count;
                do
                {

                    sValor = document.Bookmarks[iCountbookmarks].Name;
                    conect con_carta = new conect();
                    String sQuery_carta = " SELECT CampoTabla FROM caso_campossubtipodocumentos where CampoDocumento= '" + sValor + "' and  grupoid = 3";//3 = contencioso
                    MySqlDataReader respuesta_carta = con_carta.getdatareader(sQuery_carta);
                    while (respuesta_carta.Read())
                    {
                        try
                        {
                            bool bValor = document.Bookmarks.Exists(sValor);//[objfun.validareader("SubTipoDocumentoTemplateEspanol", "SubTipoDocumentoTemplateEspanol", respuesta_carta).Text].Column;//.Select();
                            if (bValor)
                            {
                                document.Bookmarks[sValor].Select();
                                String valorcampodb = objpatente.getAtributo(objfun.validareader("CampoTabla", "CampoTabla", respuesta_carta).Text);
                                application.Selection.TypeText(valorcampodb);
                            }
                        }
                        catch (Exception exs)
                        {
                            new filelog("error", "Error:" + exs.Message);
                        }
                        String svalor = "";
                    }
                    respuesta_carta.Close();
                    con_carta.Cerrarconexion();
                    iCountbookmarks--;
                } while (iCountbookmarks > 0);
                application.Visible = true;
                document.Save();
                //application.Quit();
                //((Word._Document)application.ActiveDocument).Close();
            }
            catch (Exception ex)
            {
                sMensajeerror = ex.Message;
                new filelog("error", "Error:" + ex.Message);
            }
        }
    }
}
