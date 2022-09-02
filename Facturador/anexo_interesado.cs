using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;
namespace Facturador
{
    class anexo_interesado
    {
        //capturmos primer interesado del anexo
        public String sTipo_colicitante = "";
        public String sTipopersona = "";
        public String sCurp = "";
        public String sNombre = "";
        public String sPaterno = "";
        public String sMaterno = "";
        public String sNacionalidad = "";
        public String sTelefono = "";
        public String sRfc = "";
        public String sDenominacio = "";
        public String sNacionalidad_pm = "";
        public String sTelefono_pm = "";
        public String sCp = "";
        public String sCalle = "";
        public String sNum_Ext = "";
        public String sNum_int = "";
        public String sColonia = "";
        public String sMunicipio = "";
        public String sLocalidad = "";
        public String sEntidad = "";
        public String sEntrecalles = "";
        public String sPais = "";
        public String sCalleposterior = "";

        //capturmos segundo interesado del anexo
        public String sTipo_colicitante_2 = "";
        public String sTipopersona_2 = "";
        public String sCurp_2 = "";
        public String sNombre_2 = "";
        public String sPaterno_2 = "";
        public String sMaterno_2 = "";
        public String sNacionalidad_2 = "";
        public String sTelefono_2 = "";
        public String sRfc_2 = "";
        public String sDenominacio_2 = "";
        public String sNacionalidad_pm_2 = "";
        public String sTelefono_pm_2 = "";
        public String sCp_2 = "";
        public String sCalle_2 = "";
        public String sNum_Ext_2 = "";
        public String sNum_int_2 = "";
        public String sColonia_2 = "";
        public String sMunicipio_2 = "";
        public String sLocalidad_2 = "";
        public String sEntidad_2 = "";
        public String sEntrecalles_2 = "";
        public String sPais_2 = "";
        public String sCalleposterior_2 = "";
        public Word.Document document;
        public Word.Application application;
        public anexo_interesado(String sCasoId) {
            configuracionfiles confilepth = new configuracionfiles();
            confilepth.configuracionfilesinicio();
            String sruta_plantilla = confilepth.sFileupload + @"\formatosconfigurables\anexo_interesados.docx";
            Random r = new Random();
            DateTime fecha = DateTime.Now;
            String sFehacss = fecha.ToString("HHmmss");
            try
            {
                String carpeta = "C:\\Formatos_CasosKing";
                //si no existe la carpeta temporal la creamos
                if (!(Directory.Exists(carpeta)))
                {
                    Directory.CreateDirectory(carpeta);
                }
            
            String sArchivogenerado = carpeta +"\\anexo_interesado " + sCasoId + " " + sFehacss + ".docx";
            File.Copy(sruta_plantilla, sArchivogenerado);
            //abrimos el archivo temporal y lo reemplzamos con los datos
            application = new Word.Application();
            document = application.Documents.Open(sArchivogenerado);
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
        public String sTipopersona_set(String sCampo, String sValormarcador)
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
        public String sCurp_set(String sCampo, String sValormarcador)
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
        public String sNombre_set(String sCampo, String sValormarcador)
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
        public String sPaterno_set(String sCampo, String sValormarcador)
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
        public String sMaterno_set(String sCampo, String sValormarcador)
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
        public String sNacionalidad_set(String sCampo, String sValormarcador)
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
        public String sTelefono_set(String sCampo, String sValormarcador)
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
        public String sRfc_set(String sCampo, String sValormarcador)
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
        public String sDenominacio_set(String sCampo, String sValormarcador)
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
        public String sNacionalidad_pm_set(String sCampo, String sValormarcador)
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
        public String sTelefono_pm_set(String sCampo, String sValormarcador)
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
        public String sCp_set(String sCampo, String sValormarcador)
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
        public String sCalle_set(String sCampo, String sValormarcador)
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
        public String sNum_Ext_set(String sCampo, String sValormarcador)
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
        public String sNum_int_set(String sCampo, String sValormarcador)
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
        public String sColonia_set(String sCampo, String sValormarcador)
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
        public String sMunicipio_set(String sCampo, String sValormarcador)
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
        public String sLocalidad_set(String sCampo, String sValormarcador)
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
        public String sEntidad_set(String sCampo, String sValormarcador)
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
        public String sEntrecalles_set(String sCampo, String sValormarcador)
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
        public String sPais_set(String sCampo, String sValormarcador)
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
        public String sCalleposterior_set(String sCampo, String sValormarcador)
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
        public String sTipo_colicitante_2_set(String sCampo, String sValormarcador)
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
        public String sTipopersona_2_set(String sCampo, String sValormarcador)
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
        public String sCurp_2_set(String sCampo, String sValormarcador)
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
        public String sNombre_2_set(String sCampo, String sValormarcador)
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
        public String sPaterno_2_set(String sCampo, String sValormarcador)
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
        public String sMaterno_2_set(String sCampo, String sValormarcador)
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
        public String sNacionalidad_2_set(String sCampo, String sValormarcador)
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
        public String sTelefono_2_set(String sCampo, String sValormarcador)
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
        public String sRfc_2_set(String sCampo, String sValormarcador)
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
        public String sDenominacio_2_set(String sCampo, String sValormarcador)
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
        public String sNacionalidad_pm_2_set(String sCampo, String sValormarcador)
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
        public String sTelefono_pm_2_set(String sCampo, String sValormarcador)
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
        public String sCp_2_set(String sCampo, String sValormarcador)
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
        public String sCalle_2_set(String sCampo, String sValormarcador)
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
        public String sNum_Ext_2_set(String sCampo, String sValormarcador)
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
        public String sNum_int_2_set(String sCampo, String sValormarcador)
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
        public String sColonia_2_set(String sCampo, String sValormarcador)
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
        public String sMunicipio_2_set(String sCampo, String sValormarcador)
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
        public String sLocalidad_2_set(String sCampo, String sValormarcador)
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
        public String sEntidad_2_set(String sCampo, String sValormarcador)
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
        public String sEntrecalles_2_set(String sCampo, String sValormarcador)
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
        public String sPais_2_set(String sCampo, String sValormarcador)
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
        public String sCalleposterior_2_set(String sCampo, String sValormarcador)
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
