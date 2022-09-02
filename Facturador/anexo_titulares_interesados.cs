using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;
namespace Facturador
{
    public class anexo_titulares_interesados
    {
        public String sTipo_solicitante_uno { get; set; }
        public String pf_curp_uno { get; set; }
        public String sPf_nombre_uno { get; set; }
        public String sPf_paterno_uno { get; set; }
        public String sPfNacionalidad_uno { get; set; }
        public String sPftelefono_uno { get; set; }
        public String sPmrfc_uno { get; set; }
        public String sPmDenominacion_uno { get; set; }
        public String sPmNacionalidad_uno { get; set; }

        public String sCP_uno { get; set; }
        public String sCalle_uno { get; set; }
        public String sNumext_uno { get; set; }
        public String sNumint_uno { get; set; }
        public String sColonia_uno { get; set; }
        public String sMunicipio_uno { get; set; }
        public String sEntidad_uno { get; set; }
        public String sPais_uno { get; set; }

        public String sTipo_solicitante_dos { get; set; }
        public String pf_curp_dos { get; set; }
        public String sPf_nombre_dos { get; set; }
        public String sPf_paterno_dos { get; set; }
        public String sPfNacionalidad_dos { get; set; }
        public String sPftelefono_dos { get; set; }
        public String sPmrfc_dos { get; set; }
        public String sPmDenominacion_dos { get; set; }
        public String sPmNacionalidad_dos { get; set; }

        public String sCP_dos { get; set; }
        public String sCalle_dos { get; set; }
        public String sNumext_dos { get; set; }
        public String sNumint_dos { get; set; }
        public String sColonia_dos { get; set; }
        public String sMunicipio_dos { get; set; }
        public String sEntidad_dos { get; set; }
        public String sPais_dos { get; set; }

        public Word.Document document;
        public Word.Application application;
        public String OficinaCP = "";
        public String OficinaCalle = "";
        public String OficinaNumExt = "";
        public String OficinaNumInt = "";
        public String OficinaColonia = "";
        public String OficinaMunicipio = "";
        public String OficinaEstado = "";
        public String OficinaPaisId = "";
        public String OficinaTelefono = "";
        public String OficinaCorreo = "";
        public String ApoderadoNonbre = "";
        public String ApoderadoApellidoPat = "";
        public String ApoderadoApellidoMat = "";
        public String AutorizadoNombre = "";
        public String AutorizadApellidoPat = "";
        public String AutorizadoApellidoMat = "";
        funcionesdicss dicssdfunctions = new funcionesdicss();

        public anexo_titulares_interesados()
        {
           
        }
        public void inicializamos(String sCasoId, String sTiposolicitante, String sCasonumero)
        {
            configuracionfiles confilepth = new configuracionfiles();
            confilepth.configuracionfilesinicio();
            String sruta_plantilla = confilepth.sFileupload + @"\formatosconfigurables\anexo_interesados.docx";
            Random r = new Random();
            DateTime fecha = DateTime.Now;
            String sFehacss = fecha.ToString("HHmmss");
            try
            {
                conect con_3 = new conect();
                String squerydatoficinas = "select * from datosoficina limit 1;";
                MySqlDataReader resp_datofi = con_3.getdatareader(squerydatoficinas);
                while (resp_datofi.Read())
                {
                    OficinaCP = dicssdfunctions.validareader("OficinaCP", "id_datosoficina", resp_datofi).Text;
                    OficinaCalle = dicssdfunctions.validareader("OficinaCalle", "id_datosoficina", resp_datofi).Text;
                    OficinaNumExt = dicssdfunctions.validareader("OficinaNumExt", "id_datosoficina", resp_datofi).Text;
                    OficinaNumInt = dicssdfunctions.validareader("OficinaNumInt", "id_datosoficina", resp_datofi).Text;
                    OficinaColonia = dicssdfunctions.validareader("OficinaColonia", "id_datosoficina", resp_datofi).Text;
                    OficinaMunicipio = dicssdfunctions.validareader("OficinaMunicipio", "id_datosoficina", resp_datofi).Text;
                    OficinaEstado = dicssdfunctions.validareader("OficinaEstado", "id_datosoficina", resp_datofi).Text;
                    OficinaPaisId = dicssdfunctions.validareader("OficinaPaisId", "id_datosoficina", resp_datofi).Text;
                    OficinaTelefono = dicssdfunctions.validareader("OficinaTelefono", "id_datosoficina", resp_datofi).Text;
                    OficinaCorreo = dicssdfunctions.validareader("OficinaCorreo", "id_datosoficina", resp_datofi).Text;
                    ApoderadoNonbre = dicssdfunctions.validareader("ApoderadoNonbre", "id_datosoficina", resp_datofi).Text;
                    ApoderadoApellidoPat = dicssdfunctions.validareader("ApoderadoApellidoPat", "id_datosoficina", resp_datofi).Text;
                    ApoderadoApellidoMat = dicssdfunctions.validareader("ApoderadoApellidoMat", "id_datosoficina", resp_datofi).Text;
                    AutorizadoNombre = dicssdfunctions.validareader("AutorizadoNombre", "id_datosoficina", resp_datofi).Text;
                    AutorizadApellidoPat = dicssdfunctions.validareader("AutorizadApellidoPat", "id_datosoficina", resp_datofi).Text;
                    AutorizadoApellidoMat = dicssdfunctions.validareader("AutorizadoApellidoMat", "id_datosoficina", resp_datofi).Text;
                }
                resp_datofi.Close();
                con_3.Cerrarconexion();
                String carpeta = "C:\\Formatos_CasosKing";
                //si no existe la carpeta temporal la creamos
                if (!(Directory.Exists(carpeta)))
                {
                    Directory.CreateDirectory(carpeta);
                }
                String sNombre_Doc = "";
                switch (sTiposolicitante)
                {
                    case "1": { sNombre_Doc = "Titular"; } break;
                    case "2": { sNombre_Doc = "Inventor"; } break;
                    case "3": { sNombre_Doc = "TitularInventor"; } break;
                }
                String sArchivogenerado = carpeta + "\\anexo_" + sNombre_Doc + " "+ sCasonumero + " " + sCasoId + " " + sFehacss + ".docx";
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

        public void terminardoc()
        {
            try {
                application.Visible = true;
                document.Save();
                ((Word._Document)application.ActiveDocument).Close();
                ((Word._Application)application).Quit();
            }
            catch (Exception exsd) { 
            }
            
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
