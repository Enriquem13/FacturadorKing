using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;
namespace Facturador
{
    class anexo_prioridades_doc
    {
        public Word.Document document;
        public Word.Application application;
        public anexo_prioridades_doc(String sCasoId, String sCasonumero)
        {
            configuracionfiles confilepth = new configuracionfiles();
            confilepth.configuracionfilesinicio();
            String sruta_plantilla = confilepth.sFileupload + @"\formatosconfigurables\anexoprioridades.docx";
            Random r = new Random();
            DateTime fecha = DateTime.Now;
            String sFehacss = fecha.ToString("ddMyyyy_HHmmss");
            try
            {
                String carpeta = "C:\\Formatos_CasosKing";
                //si no existe la carpeta temporal la creamos
                if (!(Directory.Exists(carpeta)))
                {
                    Directory.CreateDirectory(carpeta);
                }
            
            String sArchivogenerado = carpeta +"\\anexo_prioridades " + sCasonumero + " " + sFehacss + ".docx";
            File.Copy(sruta_plantilla, sArchivogenerado);
            //abrimos el archivo temporal y lo reemplzamos con los datos
            application = new Word.Application();
            document = application.Documents.Open(sArchivogenerado);
            document.Bookmarks["nuestra_referencia"].Select();
            application.Selection.TypeText(sCasonumero);
            }
            catch (Exception E)
            {
                new filelog("Creando carpeta c: formatos: ", E.ToString());
            }
        }

        public void terminardoc() {
            //application.Visible = true;
            document.Save();
            ((Word._Document)application.ActiveDocument).Close();
            ((Word._Application)application).Quit();
        }

        public String addcampo(String sCampo, String sValormarcador)
        {
            String sResult = "";
            try
            {
                document.Bookmarks[sCampo].Select();
                application.Selection.TypeText(sValormarcador);
                sResult = "1";
            }
            catch (Exception E)
            {
                new filelog("anexo_interesados", E.ToString());
                sResult = "";
            }
            return sResult;

        }
    }
}
