using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using System.Diagnostics;
using System.IO;
using MySql.Data.MySqlClient;

namespace Facturador
{
    class generadocscambiodom
    {
        object missing = Missing.Value;
        Word.Application wordApp;
        Word.Document aDoc = null;
        funcionesdicss objfuncionesdicss = new funcionesdicss();
        public string sCasos { get; set; }

        public void generadocs(String sQuery)
        {
            sCasos = sQuery;
            generaoficiosproductivos();
        }

        public void generaoficiosproductivos()
        {
            DateTime Hoy = DateTime.Today;
            string dd_fecha = Hoy.ToString("dd");
            string mm_fecha = Hoy.ToString("MM");
            string yyyy_fecha = Hoy.ToString("yyyy");
            try
            {
                int sContanexos = 0;
                int sexistetitularinventor = 1;
                //copiamos la plantilla a un archivo temporal
                configuracionfiles confilepth = new configuracionfiles();
                confilepth.configuracionfilesinicio();
                String sruta_plantilla ="C:\\Formatos_CasosKing" + @"\formatosconfigurables\Escritotehuantepec.docx";
                String carpeta = "C:\\Formatos_CasosKing";
                //si no existe la carpeta temporal la creamos
                if (!(Directory.Exists(carpeta)))
                {
                    Directory.CreateDirectory(carpeta);
                }
                Random r = new Random();
                conect_ipfacts con_3 = new conect_ipfacts();
                String squerydatoficinas = " select " +
                                            " caso.CasoId," +
                                            " caso.CasoNumero," +
                                            " caso.CasoDenominacion, " +
                                            " caso.CasoTitulo, " +
                                            " caso.CasoNumeroExpedienteLargo, " +
                                            " interesado.InteresadoNombre," +
                                            " tiposolicitud.TipoSolicitudDescrip," +
                                            " usuario.UsuarioClave" +
                                            " from " +
                                            " caso, " +
                                            " casointeresado, " +
                                            " interesado," +
                                            " usuario," +
                                            " tiposolicitud" +
                                            " where caso.CasoId in(29373, 29422, 29458, 29573, 29662, 29736, 29826, 38738, 38950, 28989, 29183, 29367, 29471, 29520, 29534, 29577, 29580, 29604, 29608, 29610, 29618, 29663, 29673, 29707, 38806, 38925, 39100, 39208, 39230, 39311, 39312, 27703, 39152, 29704, 29709, 29738, 29742, 38475, 39010, 39033, 39075, 39228, 39241, 39242, 39269, 39284, 39291, 39292, 39294, 39295, 39296, 39309, 39316, 39317, 39318, 39319, 39320, 39322, 39323, 39324, 39327, 39328, 39329, 39334, 39335, 39337, 39339, 39340, 39342, 39346, 39347, 39357, 39364, 39365, 39366, 39367, 39368, 39369, 39370, 39397, 39403, 39404, 39411, 39413, 39421)" +
                                            " AND casointeresado.CasoId = caso.CasoId" +
                                            " AND interesado.InteresadoID = casointeresado.InteresadoId" +
                                            " AND casointeresado.TipoRelacionId = 1" +
                                            " AND usuario.UsuarioId = caso.UsuarioId" +
                                            " AND tiposolicitud.TipoSolicitudId = caso.TipoSolicitudId";
                MySqlDataReader resp_datofi = con_3.getdatareader(sCasos);
                while (resp_datofi.Read())
                {
                    String sCasonumero = objfuncionesdicss.validareader("CasoNumero", "CasoId", resp_datofi).Text;
                    int srandonm = r.Next(9, 9999);
                    String sArchivogenerado = carpeta + "\\Formato " + sCasonumero + " " + srandonm + ".docx";
                    File.Copy(sruta_plantilla, sArchivogenerado);
                    //abrimos el archivo temporal y lo reemplzamos con los datos
                    Word.Application application = new Word.Application();
                    Word.Document document = application.Documents.Open(sArchivogenerado);

                    DateTime localDate = DateTime.Now;
                    String sFechaactual = localDate.ToString("dd/MM/yyyy");
                    String sDAteformat = DateTime.Now.ToString("dd MMMM yyyy");
                    String sTitutlo = "";
                    String sTitesp = objfuncionesdicss.validareader("CasoDenominacion", "CasoId", resp_datofi).Text.Replace("\0", "");
                    String sTitingles = objfuncionesdicss.validareader("CasoTitulo", "CasoId", resp_datofi).Text.Replace("\0", "");


                    if (sTitesp != "")
                    {
                        sTitutlo = sTitesp;
                    }
                    else
                    {
                        if (sTitingles != "")
                        {
                            sTitutlo = sTitingles;
                        }
                        else
                        {
                            sTitutlo = "--";
                        }
                    }

                    //num_concedida

                    //document.Bookmarks["num_concedida"].Select();
                    //application.Selection.TypeText(objfuncionesdicss.validareader("CasoNumeroExpedienteLargo", "CasoId", resp_datofi).Text);

                    document.Bookmarks["TipoSolicitudDescrip"].Select();
                    application.Selection.TypeText(objfuncionesdicss.validareader("TipoSolicitudDescrip", "CasoId", resp_datofi).Text);

                    document.Bookmarks["Num_expedientelargo"].Select();
                    application.Selection.TypeText(objfuncionesdicss.validareader("CasoNumeroExpedienteLargo", "CasoId", resp_datofi).Text);

                    document.Bookmarks["sTitulo"].Select();
                    application.Selection.TypeText(sTitutlo);

                    document.Bookmarks["s_titutlar"].Select();
                    application.Selection.TypeText(objfuncionesdicss.validareader("InteresadoNombre", "CasoId", resp_datofi).Text);

                    document.Bookmarks["fecha_actual"].Select();
                    application.Selection.TypeText(sDAteformat);

                    document.Bookmarks["sTitular_dos"].Select();
                    application.Selection.TypeText(objfuncionesdicss.validareader("InteresadoNombre", "CasoId", resp_datofi).Text);

                    document.Bookmarks["usuarioclave"].Select();
                    application.Selection.TypeText(objfuncionesdicss.validareader("UsuarioClave", "CasoId", resp_datofi).Text);

                    document.Bookmarks["s_caso_numero"].Select();
                    application.Selection.TypeText(objfuncionesdicss.validareader("CasoNumero", "CasoId", resp_datofi).Text);

                    document.Save();
                    application.Quit();
                }

            }
            catch (Exception Ex)
            {
                new filelog("", Ex.Message);
            }
        }
        public void generaoficiosmarcas(){
            DateTime Hoy = DateTime.Today;
            string dd_fecha = Hoy.ToString("dd");
            string mm_fecha = Hoy.ToString("MM");
            string yyyy_fecha = Hoy.ToString("yyyy");
            try
            {
                int sContanexos = 0;
                int sexistetitularinventor = 1;
                //copiamos la plantilla a un archivo temporal
                configuracionfiles confilepth = new configuracionfiles();
                confilepth.configuracionfilesinicio();
                String sruta_plantilla = confilepth.sFileupload + @"\formatosconfigurables\Escritotehuantepec.docx";
                String carpeta = "C:\\Formatos_CasosKing";
                //si no existe la carpeta temporal la creamos
                if (!(Directory.Exists(carpeta)))
                {
                    Directory.CreateDirectory(carpeta);
                }
                Random r = new Random();
                conect con_3 = new conect();
                String squerydatoficinas = " select " +
                                            " caso_marcas.CasoId," +
                                            " caso_marcas.CasoNumero," +
                                            " caso_marcas.CasoTituloespanol, " +
                                            " caso_marcas.CasoTituloingles, " +
                                            " caso_marcas.CasoNumeroExpedienteLargo, " +
                                            " interesado.InteresadoNombre," +
                                            " estatuscaso.EstatusCasoDescrip " +
                                            " from " +
                                            " caso_marcas, " +
                                            " estatuscaso," +
                                            " casointeresado," +
                                            " interesado" +
                                            " where " +
                                            " caso_marcas.EstatusCasoId = estatuscaso.EstatusCasoId" +
                                            " AND casointeresado.CasoId = caso_marcas.CasoId" +
                                            " AND casointeresado.InteresadoId = interesado.InteresadoID" +
                                            " AND casointeresado.TipoRelacionId = 1" +
                                            " AND caso_marcas.EstatusCasoId IN(7) LIMIT 2;";
                MySqlDataReader resp_datofi = con_3.getdatareader(squerydatoficinas);
                while (resp_datofi.Read())
                {
                    String sCasonumero = objfuncionesdicss.validareader("CasoNumero", "CasoId", resp_datofi).Text;
                    int srandonm = r.Next(9, 9999);
                    String sArchivogenerado = carpeta + "\\Formato " + sCasonumero + " " + srandonm + ".docx";
                    File.Copy(sruta_plantilla, sArchivogenerado);
                    //abrimos el archivo temporal y lo reemplzamos con los datos
                    Word.Application application = new Word.Application();
                    Word.Document document = application.Documents.Open(sArchivogenerado);

                    DateTime localDate = DateTime.Now;
                    String sFechaactual = localDate.ToString("dd/MM/yyyy");
                    String sTitutlo = "";
                    String sTitesp = objfuncionesdicss.validareader("CasoTituloespanol", "CasoId", resp_datofi).Text.Replace("\0", "");
                    String sTitingles = objfuncionesdicss.validareader("CasoTituloingles", "CasoId", resp_datofi).Text.Replace("\0", "");


                    if (sTitesp != "")
                    {
                        sTitutlo = sTitesp;
                    }
                    else
                    {
                        if (sTitingles != "")
                        {
                            sTitutlo = sTitingles;
                        }
                        else
                        {
                            sTitutlo = "--";
                        }
                    }

                    document.Bookmarks["n_expediente"].Select();
                    application.Selection.TypeText(objfuncionesdicss.validareader("CasoNumeroExpedienteLargo", "CasoId", resp_datofi).Text);

                    document.Bookmarks["titulodelainvencion"].Select();
                    application.Selection.TypeText(sTitutlo);

                    document.Bookmarks["stitular"].Select();
                    application.Selection.TypeText(objfuncionesdicss.validareader("InteresadoNombre", "CasoId", resp_datofi).Text);

                    document.Bookmarks["sFecha"].Select();
                    application.Selection.TypeText(sFechaactual);

                    document.Bookmarks["sTitulardos"].Select();
                    application.Selection.TypeText(objfuncionesdicss.validareader("InteresadoNombre", "CasoId", resp_datofi).Text);

                    document.Save();
                    //is.Close();
                    application.Quit();
                }

            }
            catch (Exception Ex)
            {
                new filelog("", "");
            }
        }

        public void generaoficiopatentes() {
            DateTime Hoy = DateTime.Today;
            string dd_fecha = Hoy.ToString("dd");
            string mm_fecha = Hoy.ToString("MM");
            string yyyy_fecha = Hoy.ToString("yyyy");
            try
            {
                int sContanexos = 0;
                int sexistetitularinventor = 1;
                //copiamos la plantilla a un archivo temporal
                configuracionfiles confilepth = new configuracionfiles();
                confilepth.configuracionfilesinicio();
                String sruta_plantilla = confilepth.sFileupload + @"\formatosconfigurables\Escritotehuantepec.docx";
                String carpeta = "C:\\Formatos_CasosKing";
                //si no existe la carpeta temporal la creamos
                if (!(Directory.Exists(carpeta)))
                {
                    Directory.CreateDirectory(carpeta);
                }
                Random r = new Random();
                conect con_3 = new conect();
                String squerydatoficinas = " select " +
                                            " caso_patente.CasoId," +
                                            " caso_patente.CasoNumero," +
                                            " caso_patente.CasoTituloespanol, " +
                                            " caso_patente.CasoTituloingles, " +
                                            " caso_patente.CasoNumeroExpedienteLargo, " +
                                            " interesado.InteresadoNombre," +
                                            " estatuscaso.EstatusCasoDescrip " +
                                            " from " +
                                            " caso_patente, " +
                                            " estatuscaso," +
                                            " casointeresado," +
                                            " interesado" +
                                            " where " +
                                            " caso_patente.EstatusCasoId = estatuscaso.EstatusCasoId" +
                                            " AND casointeresado.CasoId = caso_patente.CasoId" +
                                            " AND casointeresado.InteresadoId = interesado.InteresadoID" +
                                            " AND casointeresado.TipoRelacionId = 1" +
                                            " AND caso_patente.EstatusCasoId IN(7) LIMIT 2;";
                MySqlDataReader resp_datofi = con_3.getdatareader(squerydatoficinas);
                while (resp_datofi.Read())
                {
                    String sCasonumero = objfuncionesdicss.validareader("CasoNumero", "CasoId", resp_datofi).Text;
                    int srandonm = r.Next(9, 9999);
                    String sArchivogenerado = carpeta + "\\Formato " + sCasonumero + " " + srandonm + ".docx";
                    File.Copy(sruta_plantilla, sArchivogenerado);
                    //abrimos el archivo temporal y lo reemplzamos con los datos
                    Word.Application application = new Word.Application();
                    Word.Document document = application.Documents.Open(sArchivogenerado);

                    DateTime localDate = DateTime.Now;
                    String sFechaactual = localDate.ToString("dd/MM/yyyy");
                    String sTitutlo = "";
                    String sTitesp = objfuncionesdicss.validareader("CasoTituloespanol", "CasoId", resp_datofi).Text.Replace("\0", "");
                    String sTitingles = objfuncionesdicss.validareader("CasoTituloingles", "CasoId", resp_datofi).Text.Replace("\0", "");


                    if (sTitesp != "")
                    {
                        sTitutlo = sTitesp;
                    }
                    else
                    {
                        if (sTitingles != "")
                        {
                            sTitutlo = sTitingles;
                        }
                        else
                        {
                            sTitutlo = "--";
                        }
                    }

                    document.Bookmarks["n_expediente"].Select();
                    application.Selection.TypeText(objfuncionesdicss.validareader("CasoNumeroExpedienteLargo", "CasoId", resp_datofi).Text);

                    document.Bookmarks["titulodelainvencion"].Select();
                    application.Selection.TypeText(sTitutlo);

                    document.Bookmarks["stitular"].Select();
                    application.Selection.TypeText(objfuncionesdicss.validareader("InteresadoNombre", "CasoId", resp_datofi).Text);

                    document.Bookmarks["sFecha"].Select();
                    application.Selection.TypeText(sFechaactual);

                    document.Bookmarks["sTitulardos"].Select();
                    application.Selection.TypeText(objfuncionesdicss.validareader("InteresadoNombre", "CasoId", resp_datofi).Text);

                    document.Save();
                    //is.Close();
                    application.Quit();
                }

            }
            catch (Exception Ex)
            {
                new filelog("", "");
            }
        }



    }
}
