using Microsoft.Office.Interop.Word;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace Facturador
{
    class solicitudpat2
    {
        public String sCasoId;
        //public String sTipoformato;
        object missing = Missing.Value;
        Word.Application wordApp;
        Word.Document aDoc = null;
        object missing2 = Missing.Value;
        conect condoc;
        public String srutacarpeta;
        public String Sinicioruta;
        public String rutapatentes;
        public String rutapatentes2;
        public String rutamarcas;
        public String rutainteresado;
        public String rutaprioridades;
        

        public void nuevasolicitud(String CasoId)
        {
            condoc = new conect();
            wordApp = new Word.Application();
            aDoc = null;
            sCasoId = CasoId;
            wordApp.Quit(ref missing, ref missing, ref missing);
            wordApp = new Word.Application();
            aDoc = null;
            String strRutaArchivo = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            //Sinicioruta = strRutaArchivo"\\Ca\\";
            Sinicioruta = strRutaArchivo + @"\casosking\DocumentosCasosPrueba\";
            //rutainteresado = "C:\\Pclientes\\Interesados.doc";
            String srutaacceso = strRutaArchivo + @"\casosking\";
            rutainteresado = srutaacceso+"Interesados.doc";
            rutapatentes = srutaacceso+"documento1.docx";
            rutapatentes2 = srutaacceso + "Cartas\\documentonuevo.docx";
            rutamarcas = srutaacceso+"documento3.doc";
            rutaprioridades = srutaacceso+"prioridades3.doc";

            var bankAccounts = new List<Account> 
            {
                new Account { 
                              ID = 345678,
                              Balance = 541.27
                            },
                new Account {
                              ID = 1230221,
                              Balance = -127.44
                            }
            };

            // Display the list in an Excel spreadsheet.
            //DisplayInExcel(bankAccounts);

            // Create a Word document that contains an icon that links to
            // the spreadsheet.
            CreateIconInWordDoc();

        }

                private void CreateIconInWordDoc()
        {
            try
            {
                conect condoc = new conect();

                //comienza codigo para abir word 
                object abrirDocnuevo = rutapatentes2;


                ///object fileNameword = @"C:\Pclientes\dos.docx";

                object missingnuevo = Missing.Value;


                object readOnlynuevo = false; //default
                object isVisiblenuevo = false;

                aDoc = wordApp.Documents.Open(ref abrirDocnuevo, ref isVisiblenuevo, ref missingnuevo,
                            ref missingnuevo, ref missingnuevo, ref missingnuevo,
                            ref missingnuevo, ref missingnuevo, ref missingnuevo,
                            ref missingnuevo, ref missingnuevo, ref missingnuevo,
                            ref missingnuevo, ref missingnuevo, ref missingnuevo, ref missingnuevo);

                                                        String querydocnuevo = "SELECT " +
                                                                        "casointeresado.CasoId, " +
                                                                        "casointeresado.InteresadoId, " +
                                                                        "casointeresado.TipoRelacionId, " +
                                                                        "casointeresado.CasoInteresadoSecuencia, " +
                                                                        "caso_patente.TipoSolicitudId, " +
                                                                        "caso_patente.SubTipoSolicitudId, " +
                                                                        "caso_patente.TipoPctId, " +
                                                                        "caso_patente.CasoNumero, " +
                                                                        "caso_patente.CasoNumeroExpedienteLargo, " +
                                                                        "interesado.InteresadoTipoPersonaSAT, " +
                                                                        "interesado.InteresadoCurp, " +
                                                                        "interesado.InteresadoRFC, " +
                                                                        "interesado.InteresadoNombre, " +
                                                                        "interesado.InteresadoApPaterno, " +
                                                                        "interesado.InteresadoApMaterno, " +
                                                                        "interesado.PaisId as nacionalidadid, " +
                                                                        "datosoficina.OficinaTelefono, " +
                                                                        "datosoficina.OficinaCorreo, " +
                                                                        "direccion.DireccionCP, " +
                                                                        "direccion.DireccionCalle, " +
                                                                        "direccion.DireccionNumExt, " +
                                                                        "direccion.DireccionNumInt, " +
                                                                        "direccion.DireccionColonia, " +
                                                                        "direccion.DireccionPoblacion, " +
                                                                        "direccion.DireccionEstado, " +
                                                                        "direccion.PaisId as paisdireccioninteresado, " +
                                                                        "datosoficina.OficinaCalle, " +
                                                                        "datosoficina.OficinaNumExt, " +
                                                                        "datosoficina.OficinaNumInt, " +
                                                                        "datosoficina.OficinaColonia, " +
                                                                        "datosoficina.OficinaPaisId, " +
                                                                        "datosoficina.OficinaCP, " +
                                                                        "datosoficina.ApoderadoNonbre, " +
                                                                        "datosoficina.ApoderadoApellidoPat, " +
                                                                        "datosoficina.ApoderadoApellidoMat, " +
                                                                        "datosoficina.OficinaEstado, " +
                                                                        "datosoficina.OficinaMunicipio, " +
                                                                        "datosoficina.AutorizadoNombre, " +
                                                                        "datosoficina.AutorizadApellidoPat, " +
                                                                        "datosoficina.AutorizadoApellidoMat, " +
                                                                        "DATE_FORMAT(caso_patente.CasoFechaDivulgacionPrevia,  '%d-%m-%Y') AS CasoFechaDivulgacionPrevia,    " +
                                                                        "DATE_FORMAT(caso_patente.CasoFechaRecepcion,  '%d-%m-%Y') AS CasoFechaRecepcion,    " +
                                                                        "caso_patente.CasoTituloespanol " +
                                                                        "from caso_patente, casointeresado, interesado, datosoficina, direccion " +
                                                                        "WHERE caso_patente.CasoId = " + sCasoId +
                                                                        " AND  caso_patente.CasoId = casointeresado.CasoId " +
                                                                        "AND casointeresado.InteresadoId = interesado.InteresadoID " +
                                                                        "AND direccion.InteresadoId = interesado.InteresadoID group by interesado.InteresadoID order by CasoInteresadoSecuencia ASC;";
                                                                        MySqlDataReader respuestastrindoc2 = condoc.getdatareader(querydocnuevo);

                                                                        int continven0 = 0;
                                                                        int Icontadorg0 = 0;//contador para conocer cuantos interesados y titulares son en total (resultado de la consulta a interesados)
                                                                        String[] aNombre0 = new String[25];
                                                                        String[] aApellidopa0 = new String[25];
                                                                        String[] aApellidoma0 = new String[25];
                                                                        String[] aCurp0 = new String[25];
                                                                        String[] aNacionalidad0 = new String[25];
                                                                        String[] aTelefono0 = new String[25];
                                                                        String[] aCorreo0 = new String[25];
                                                                        String[] aCP0 = new String[25];
                                                                        String[] aCalle0 = new String[25];
                                                                        String[] aNumeroexterior0 = new String[25];
                                                                        String[] aNumint0 = new String[25];
                                                                        String[] aColonia0 = new String[25];
                                                                        String[] aMunicipio0 = new String[25];
                                                                        String[] aEstado0 = new String[25];
                                                                        String[] aPais0 = new String[25];
                                                                        String[] aLocalidad0 = new String[25];
                                                                        String[] aRFC0 = new String[25];





                                                                        while (respuestastrindoc2.Read())//EMPIEZA EL WHILE PARA CONSULTAR INTERESADO (TITULARES E INVENTORES)
                                                                        {


     //
                                                                            if ((validareader("CasoTituloespanol", "CasoId", respuestastrindoc2).Text).Length > 14)
                                                                            {

                                                                                String nombrecarpeta = (validareader("CasoTituloespanol", "CasoId", respuestastrindoc2).Text).Substring(0, 15);
                                                                                srutacarpeta = Sinicioruta + nombrecarpeta + "_" + (validareader("CasoNumero", "CasoId", respuestastrindoc2).Text);
                                                                                System.IO.Directory.CreateDirectory(srutacarpeta);

                                                                            }
                                                                            else
                                                                            {

                                                                                String nombrecarpeta = (validareader("CasoTituloespanol", "CasoId", respuestastrindoc2).Text);
                                                                                srutacarpeta = Sinicioruta + nombrecarpeta + "_" + (validareader("CasoNumero", "CasoId", respuestastrindoc2).Text);
                                                                                System.IO.Directory.CreateDirectory(srutacarpeta);
                                                                            }

                                                                            if (Icontadorg0 > 1)
                                                                            {
                                                                                aNombre0[Icontadorg0 - 2] = (validareader("InteresadoNombre", "CasoId", respuestastrindoc2).Text);
                                                                                aApellidopa0[Icontadorg0 - 2] = (validareader("InteresadoApPaterno", "CasoId", respuestastrindoc2).Text);
                                                                                aApellidoma0[Icontadorg0 - 2] = (validareader("InteresadoApMaterno", "CasoId", respuestastrindoc2).Text);
                                                                                aCurp0[Icontadorg0 - 2] = (validareader("InteresadoCurp", "CasoId", respuestastrindoc2).Text);
                                                                                aNacionalidad0[Icontadorg0 - 2] = (validareader("nacionalidadid", "CasoId", respuestastrindoc2).Text);
                                                                                aTelefono0[Icontadorg0 - 2] = (validareader("OficinaTelefono", "CasoId", respuestastrindoc2).Text);
                                                                                aCorreo0[Icontadorg0 - 2] = (validareader("OficinaCorreo", "CasoId", respuestastrindoc2).Text);
                                                                                aCP0[Icontadorg0 - 2] = (validareader("DireccionCP", "CasoId", respuestastrindoc2).Text);
                                                                                aCalle0[Icontadorg0 - 2] = (validareader("DireccionCalle", "CasoId", respuestastrindoc2).Text);
                                                                                aNumeroexterior0[Icontadorg0 - 2] = (validareader("DireccionNumExt", "CasoId", respuestastrindoc2).Text);
                                                                                aNumint0[Icontadorg0 - 2] = (validareader("DireccionNumInt", "CasoId", respuestastrindoc2).Text);
                                                                                aColonia0[Icontadorg0 - 2] = (validareader("DireccionColonia", "CasoId", respuestastrindoc2).Text);
                                                                                aMunicipio0[Icontadorg0 - 2] = (validareader("DireccionPoblacion", "CasoId", respuestastrindoc2).Text);
                                                                                // aEstado0[Icontadorg0 - 2] = (validareader("InteresadoNombre", "CasoId", respuestastrindoc2).Text);
                                                                                aPais0[Icontadorg0 - 2] = (validareader("paisdireccioninteresado", "CasoId", respuestastrindoc2).Text);
                                                                                // aLocalidad0[Icontadorg0 - 2] = (validareader("InteresadoNombre", "CasoId", respuestastrindoc2).Text);
                                                                                aRFC0[Icontadorg0 - 2] = (validareader("InteresadoRFC", "CasoId", respuestastrindoc2).Text);
                                                                            }

                                                                            switch ((validareader("TipoSolicitudId", "CasoId", respuestastrindoc2).Text))
                                                                            {
                                                                                case "1":
                                                                                    this.FindAndReplace(wordApp, "X1", "X");

                                                                                    if ((validareader("SubTipoSolicitudId", "CasoId", respuestastrindoc2).Text) == "1")
                                                                                    {
                                                                                        // SubTipoSolicitudId
                                                                                        this.FindAndReplace(wordApp, "X2", "X");
                                                                                    }

                                                                                    break;
                                                                                case "2":
                                                                                    this.FindAndReplace(wordApp, "X3", "X");
                                                                                    if ((validareader("SubTipoSolicitudId", "CasoId", respuestastrindoc2).Text) == "1")
                                                                                    {
                                                                                        // SubTipoSolicitudId
                                                                                        this.FindAndReplace(wordApp, "X4", "X");
                                                                                    }
                                                                                    break;
                                                                                case "3":
                                                                                    this.FindAndReplace(wordApp, "X5", "X");
                                                                                    this.FindAndReplace(wordApp, "X6", "X");

                                                                                    break;
                                                                                case "4":
                                                                                    this.FindAndReplace(wordApp, "X5", "X");
                                                                                    this.FindAndReplace(wordApp, "X7", "X");
                                                                                    break;
                                                                                case "5":
                                                                                    // PARECE QUE NUNCA SE UTILIZA.
                                                                                    break;
                                                                            }

                                                                            

                                                                            this.FindAndReplace(wordApp, "<CURP_APODERADO>", "");
                                                                            this.FindAndReplace(wordApp, "<NOMBRE_APODERADO>", validareader("ApoderadoNonbre", "CasoId", respuestastrindoc2).Text);
                                                                            this.FindAndReplace(wordApp, "<APELLIDOPAT_APODERADO>", validareader("ApoderadoApellidoPat", "CasoId", respuestastrindoc2).Text);
                                                                            this.FindAndReplace(wordApp, "<APELLIDOMAT_APODERADO>", validareader("ApoderadoApellidoMat", "CasoId", respuestastrindoc2).Text);
                                                                            this.FindAndReplace(wordApp, "<CODIGOPOSTAL_OFICINA>", validareader("OficinaCP", "CasoId", respuestastrindoc2).Text);
                                                                            this.FindAndReplace(wordApp, "<CALLE_OFICINA>", validareader("OficinaCalle", "CasoId", respuestastrindoc2).Text);
                                                                            this.FindAndReplace(wordApp, "<NUMEXT_OFICINA>", validareader("OficinaNumExt", "CasoId", respuestastrindoc2).Text);
                                                                            this.FindAndReplace(wordApp, "<NUMINT_OFICINA>", validareader("OficinaNumInt", "CasoId", respuestastrindoc2).Text);
                                                                            this.FindAndReplace(wordApp, "<LOCALIDAD_OFICINA>", validareader("OficinaMunicipio", "CasoId", respuestastrindoc2).Text);
                                                                            this.FindAndReplace(wordApp, "<ESTADO_OFICINA>", validareader("OficinaEstado", "CasoId", respuestastrindoc2).Text);
                                                                            this.FindAndReplace(wordApp, "<NOMBRE_AUT>", validareader("AutorizadoNombre", "CasoId", respuestastrindoc2).Text);
                                                                            this.FindAndReplace(wordApp, "<APELLIDOPAT_AUT>", validareader("AutorizadApellidoPat", "CasoId", respuestastrindoc2).Text);
                                                                            this.FindAndReplace(wordApp, "<APELLIDOMAT_AUT>", validareader("AutorizadoApellidoMat", "CasoId", respuestastrindoc2).Text);
                                                                            this.FindAndReplace(wordApp, "<TITULO_SOLICITUD>", validareader("CasoTituloespanol", "CasoId", respuestastrindoc2).Text);
                                                                            this.FindAndReplace(wordApp, "<TITULO_SOLICITUD>", validareader("CasoTituloespanol", "CasoId", respuestastrindoc2).Text);
                                                                            this.FindAndReplace(wordApp, "<TITULO_SOLICITUD>", validareader("CasoTituloespanol", "CasoId", respuestastrindoc2).Text);
                                                                            this.FindAndReplace(wordApp, "<CORREO_OFICINA_APODERADO>", validareader("OficinaCorreo", "CasoId", respuestastrindoc2).Text);
                                                                            this.FindAndReplace(wordApp, "<TELEFONO_OFICINA_APO>", validareader("OficinaTelefono", "CasoId", respuestastrindoc2).Text);
                                                                            this.FindAndReplace(wordApp, " <COLONIA_OFICINA>", validareader("OficinaColonia", "CasoId", respuestastrindoc2).Text); 
                                                                            this.FindAndReplace(wordApp, "<CURP_APODERADO>", "");
                                                                            this.FindAndReplace(wordApp, "<NOMBRE_APODERADO>", validareader("ApoderadoNonbre", "CasoId", respuestastrindoc2).Text);
                                                                            this.FindAndReplace(wordApp, "<APELLIDOPAT_APODERADO>", validareader("ApoderadoApellidoPat", "CasoId", respuestastrindoc2).Text);
                                                                            this.FindAndReplace(wordApp, "<APELLIDOMAT_APODERADO>", validareader("ApoderadoApellidoMat", "CasoId", respuestastrindoc2).Text);
                                                                            this.FindAndReplace(wordApp, "<CODIGOPOSTAL_OFICINA>", validareader("OficinaCP", "CasoId", respuestastrindoc2).Text);
                                                                            this.FindAndReplace(wordApp, "<CALLE_OFICINA>", validareader("OficinaCalle", "CasoId", respuestastrindoc2).Text);
                                                                            this.FindAndReplace(wordApp, "<NUMEXT_OFICINA>", validareader("OficinaNumExt", "CasoId", respuestastrindoc2).Text);
                                                                            this.FindAndReplace(wordApp, "<NUMINT_OFICINA>", validareader("OficinaNumInt", "CasoId", respuestastrindoc2).Text);
                                                                            this.FindAndReplace(wordApp, "<LOCALIDAD_OFICINA>", validareader("OficinaMunicipio", "CasoId", respuestastrindoc2).Text);
                                                                            this.FindAndReplace(wordApp, "<ESTADO_OFICINA>", validareader("OficinaEstado", "CasoId", respuestastrindoc2).Text);
                                                                            this.FindAndReplace(wordApp, "<NOMBRE_AUT>", validareader("AutorizadoNombre", "CasoId", respuestastrindoc2).Text);
                                                                            this.FindAndReplace(wordApp, "<APELLIDOPAT_AUT>", validareader("AutorizadApellidoPat", "CasoId", respuestastrindoc2).Text);
                                                                            this.FindAndReplace(wordApp, "<APELLIDOMAT_AUT>", validareader("AutorizadoApellidoMat", "CasoId", respuestastrindoc2).Text);
                                                                            this.FindAndReplace(wordApp, "<TITULO_SOLICITUD>", validareader("CasoTituloespanol", "CasoId", respuestastrindoc2).Text);
                                                                            this.FindAndReplace(wordApp, "<TITULO_SOLICITUD>", validareader("CasoTituloespanol", "CasoId", respuestastrindoc2).Text);
                                                                            this.FindAndReplace(wordApp, "<TITULO_SOLICITUD>", validareader("CasoTituloespanol", "CasoId", respuestastrindoc2).Text);
                                                                            this.FindAndReplace(wordApp, "<CORREO_OFICINA_APODERADO>", validareader("OficinaCorreo", "CasoId", respuestastrindoc2).Text);
                                                                            this.FindAndReplace(wordApp, "<TELEFONO_OFICINA_APO>", validareader("OficinaTelefono", "CasoId", respuestastrindoc2).Text);
                                                                            this.FindAndReplace(wordApp, "<COLONIA_OFICINA>", validareader("OficinaColonia", "CasoId", respuestastrindoc2).Text);
                                                                            this.FindAndReplace(wordApp, "<EXPEDIENTE>", validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastrindoc2).Text);


                                                                            if (validafechavacia(validareader("CasoFechaRecepcion", "CasoId", respuestastrindoc2).Text) != "")
                                                                            {
                                                                                this.FindAndReplace(wordApp, "<DIA_EXPE>", (validareader("CasoFechaRecepcion", "CasoId", respuestastrindoc2).Text).Substring(0, 2));
                                                                                this.FindAndReplace(wordApp, "<MES_EXPE>", (validareader("CasoFechaRecepcion", "CasoId", respuestastrindoc2).Text).Substring(3, 2));
                                                                                this.FindAndReplace(wordApp, "<ANO_EXPE>", (validareader("CasoFechaRecepcion", "CasoId", respuestastrindoc2).Text).Substring(6, 4));
                                                                            }

                                                                            if (validafechavacia(validareader("CasoFechaDivulgacionPrevia", "CasoId", respuestastrindoc2).Text) != "")
                                                                            {
                                                                                this.FindAndReplace(wordApp, "<DIA_DIVUL>", (validareader("CasoFechaDivulgacionPrevia", "CasoId", respuestastrindoc2).Text).Substring(0, 2));
                                                                                this.FindAndReplace(wordApp, "<MES_DIVUL>", (validareader("CasoFechaDivulgacionPrevia", "CasoId", respuestastrindoc2).Text).Substring(3, 2));
                                                                                this.FindAndReplace(wordApp, "<ANO_DIVUL>", (validareader("CasoFechaDivulgacionPrevia", "CasoId", respuestastrindoc2).Text).Substring(6, 4));
                                                                            }

                                                                          

                                                                            //this.FindAndReplace(wordApp, "<DIA_PA>", (validareader("PrioridadFecha", "PrioridadId", respuestastrindoc4).Text).Substring(0, 2));
                                                                            //this.FindAndReplace(wordApp, "<MES_PA>", (validareader("PrioridadFecha", "PrioridadId", respuestastrindoc4).Text).Substring(3, 2));
                                                                            //this.FindAndReplace(wordApp, "<ANO_PA>", (validareader("PrioridadFecha", "PrioridadId", respuestastrindoc4).Text).Substring(6, 4));
                                                                            conect con_pais = new conect();
                                                                            String kwerypaisoficina = "SELECT * FROM  pais where PaisId  = " + (validareader("OficinaPaisId", "CasoId", respuestastrindoc2).Text) + ";";
                                                                            MySqlDataReader respuestapaisoficina = con_pais.getdatareader(kwerypaisoficina);
                                                                            while (respuestapaisoficina.Read())
                                                                            {
                                                                                // sininacionalidad = validareader("PaisNacionalidad", "PaisId", respuestastring144).Text;
                                                                                this.FindAndReplace(wordApp, "<PAIS_OFICINA>", validareader("PaisNombre", "PaisId", respuestapaisoficina).Text);
                                                                            }
                                                                            respuestapaisoficina.Close();
                                                                            con_pais.Cerrarconexion();
                                                                            
                                                                            
                                                                            // me falta el pais de la oficina

                                                                                                                            //consulta los inventores

                                                                            if ((validareader("nacionalidadid", "CasoId", respuestastrindoc2).Text) != "148")
                                                                            { 
                                                                                                                                    //Es extrangero

                                                                               
                                                                                //if ((validareader("TipoRelacionId", "CasoId", respuestastrindoc2).Text) == "1")
                                                                                //{ 
                                                                                //    // Es un titular
                                                                                //}

                                                                                switch ((validareader("TipoRelacionId", "CasoId", respuestastrindoc2).Text))
                                                                                {
                                                                                    case "1":
                                                                                        //titular 
                                                                                        if ((validareader("InteresadoTipoPersonaSAT", "CasoId", respuestastrindoc2).Text) != "ME")
                                                                                        {
                                                                                            // es una persona fisica
                                                                                            this.FindAndReplace(wordApp, "<CURP_FISICA>", validareader("InteresadoCurp", "CasoId", respuestastrindoc2).Text);
                                                                                            this.FindAndReplace(wordApp, "<NOMBRE_FISICA>", validareader("InteresadoNombre", "CasoId", respuestastrindoc2).Text);
                                                                                            this.FindAndReplace(wordApp, "<APELLIDOPAT_FISICA>", validareader("InteresadoApPaterno", "CasoId", respuestastrindoc2).Text);
                                                                                            this.FindAndReplace(wordApp, "<APELLIDOMAT_FISICA>", validareader("InteresadoApMaterno", "CasoId", respuestastrindoc2).Text);
                                                                                            this.FindAndReplace(wordApp, "<TELEFONO_OFICINA_FI>", validareader("OficinaTelefono", "CasoId", respuestastrindoc2).Text);
                                                                                            this.FindAndReplace(wordApp, "<CORREO_OFICINA_FI>", validareader("OficinaCorreo", "CasoId", respuestastrindoc2).Text);




                                                                                            conect con_pais_2 = new conect();
                                                                                            String kwery44 = "SELECT * FROM  pais where PaisId  = " + (validareader("nacionalidadid", "CasoId", respuestastrindoc2).Text) + ";";
                                                                                            MySqlDataReader respuestastring144 = con_pais_2.getdatareader(kwery44);

                                                                                            while (respuestastring144.Read())
                                                                                            {
                                                                                                // sininacionalidad = validareader("PaisNacionalidad", "PaisId", respuestastring144).Text;
                                                                                                this.FindAndReplace(wordApp, "<NACIONALIDAD_FISICA>", validareader("PaisNacionalidad", "PaisId", respuestastring144).Text);

                                                                                            }
                                                                                            respuestastring144.Close();
                                                                                            con_pais_2.Cerrarconexion();
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            // es una persona moral
                                                                                            this.FindAndReplace(wordApp, "<RFC_MORAL>", validareader("InteresadoRFC", "CasoId", respuestastrindoc2).Text);
                                                                                            this.FindAndReplace(wordApp, "<RAZON_SOCIAL>", validareader("InteresadoNombre", "CasoId", respuestastrindoc2).Text);
                                                                                            this.FindAndReplace(wordApp, "<TEL_OFICINA_MO>", validareader("OficinaTelefono", "CasoId", respuestastrindoc2).Text);
                                                                                            this.FindAndReplace(wordApp, "<CORREO_OFICINA_MO>", validareader("OficinaCorreo", "CasoId", respuestastrindoc2).Text);
                                                                                            
                                                                                            conect con_pais_3 = new conect();
                                                                                            String kwery44 = "SELECT * FROM  pais where PaisId  = " + (validareader("nacionalidadid", "CasoId", respuestastrindoc2).Text) + ";";
                                                                                            MySqlDataReader respuestastring144 = con_pais_3.getdatareader(kwery44);

                                                                                            while (respuestastring144.Read())
                                                                                            {
                                                                                                // sininacionalidad = validareader("PaisNacionalidad", "PaisId", respuestastring144).Text;
                                                                                                this.FindAndReplace(wordApp, "<NACIONALIDAD_MORAL>", validareader("PaisNacionalidad", "PaisId", respuestastring144).Text);
                                                                                                this.FindAndReplace(wordApp, "<PAIS_TITULAR>", validareader("PaisNombre", "PaisId", respuestastring144).Text);

                                                                                            }
                                                                                            respuestastring144.Close();
                                                                                            con_pais_3.Cerrarconexion();
                                                                                        }

                                                                                          string colonia = validareader("DireccionColonia", "CasoId", respuestastrindoc2).Text; //la colonia trae saltos de linea
                                                                                          colonia = colonia.Replace(Environment.NewLine, " ");

                                                                                            this.FindAndReplace(wordApp, "<CODIGOPOSTAL_TITULAR>", validareader("DireccionCP", "CasoId", respuestastrindoc2).Text);
                                                                                            this.FindAndReplace(wordApp, "<CALLE_TITULAR>", validareader("DireccionCalle", "CasoId", respuestastrindoc2).Text);
                                                                                            this.FindAndReplace(wordApp, "<NUMEXTERIOR_TITULAR>", validareader("DireccionNumExt", "CasoId", respuestastrindoc2).Text);
                                                                                            this.FindAndReplace(wordApp, "<NUMINTERIOR_TITULAR>", validareader("DireccionNumInt", "CasoId", respuestastrindoc2).Text);
                                                                                            this.FindAndReplace(wordApp, "<COLONIA_TITULAR>", colonia);
                                                                                            //  this.FindAndReplace(wordApp, "<MUNICIPIO_TITULAR>", validareader("DireccionPoblacion", "CasoId", respuestastrindoc2).Text);
                                                                                            this.FindAndReplace(wordApp, "<ESTADO_TITULAR>", validareader("DireccionPoblacion", "CasoId", respuestastrindoc2).Text);
                                                                                            this.FindAndReplace(wordApp, "<LOCALIDAD_TITULAR>", validareader("DireccionEstado", "CasoId", respuestastrindoc2).Text);

                                                                                        break;
                                                                                    case "2":
                                                                                        // Inventor
                                                                                                if (continven0 == 0)
                                                                                                {
                                                                                                    //es el primer inventor
                                                                                                    this.FindAndReplace(wordApp, "<CURP_INVENTOR>", validareader("InteresadoCurp", "CasoId", respuestastrindoc2).Text);
                                                                                                    this.FindAndReplace(wordApp, "<NOMBRE_INVENTOR>", validareader("InteresadoNombre", "CasoId", respuestastrindoc2).Text);
                                                                                                    this.FindAndReplace(wordApp, "<APELLIDOPAT_INVENTOR>", validareader("InteresadoApPaterno", "CasoId", respuestastrindoc2).Text);
                                                                                                    this.FindAndReplace(wordApp, "<APELLIDOMAT_INVNETOR>", validareader("InteresadoApMaterno", "CasoId", respuestastrindoc2).Text);
                                                                                                    this.FindAndReplace(wordApp, "<TELEFONO_OFICINA_INVENTOR>", validareader("OficinaTelefono", "CasoId", respuestastrindoc2).Text);
                                                                                                    this.FindAndReplace(wordApp, "<CORREO_OFICINA_INVENTOR>", validareader("OficinaCorreo", "CasoId", respuestastrindoc2).Text);

                                                                                                    conect con_pais_4 = new conect();
                                                                                                    String kwery44 = "SELECT * FROM  pais where PaisId  = " + (validareader("nacionalidadid", "CasoId", respuestastrindoc2).Text) + ";";
                                                                                                    MySqlDataReader respuestastring144 = con_pais_4.getdatareader(kwery44);

                                                                                                    while (respuestastring144.Read())
                                                                                                    {
                                                                                                        //   sininacionalidad_inventor = validareader("PaisNacionalidad", "PaisId", respuestastring144).Text;
                                                                                                        this.FindAndReplace(wordApp, "<NACIONALIDAD_INVENTOR>", validareader("PaisNacionalidad", "PaisId", respuestastring144).Text);
                                                                                                        this.FindAndReplace(wordApp, "<PAIS_INVENTOR>", validareader("PaisNombre", "PaisId", respuestastring144).Text);

                                                                                                    }
                                                                                                    respuestastring144.Close();
                                                                                                    con_pais_4.Cerrarconexion();


                                                                                                    string coloniain = validareader("DireccionColonia", "CasoId", respuestastrindoc2).Text; //la colonia trae saltos de linea
                                                                                                    colonia = coloniain.Replace(Environment.NewLine, " ");

                                                                                                    this.FindAndReplace(wordApp, "<CODIGOPOSTAL_INVENTOR>", validareader("DireccionCP", "CasoId", respuestastrindoc2).Text);
                                                                                                    this.FindAndReplace(wordApp, "<CALLE_INVENTOR>", validareader("DireccionCalle", "CasoId", respuestastrindoc2).Text);
                                                                                                    this.FindAndReplace(wordApp, "<NUMEXTERIOR_INVENTOR>", validareader("DireccionNumExt", "CasoId", respuestastrindoc2).Text);
                                                                                                    this.FindAndReplace(wordApp, "<NUMINTERIOR_INVENTOR>", validareader("DireccionNumInt", "CasoId", respuestastrindoc2).Text);
                                                                                                    this.FindAndReplace(wordApp, "<COLONIA_INVENTOR>", coloniain);
                                                                                                    //  this.FindAndReplace(wordApp, "<MUNICIPIO_TITULAR>", validareader("DireccionPoblacion", "CasoId", respuestastrindoc2).Text);
                                                                                                    this.FindAndReplace(wordApp, "<ESTADO_INVENTOR>", validareader("DireccionPoblacion", "CasoId", respuestastrindoc2).Text);
                                                                                                    this.FindAndReplace(wordApp, "<LOCALIDAD_INVENTOR>", validareader("DireccionEstado", "CasoId", respuestastrindoc2).Text);

                                                                                                }
                                                                                                continven0++;

                                                                                        break;
                                                                                    case "3":
                                                                                        // Titular/Inventor

                                                                                        break;
                                                                                    //case "4":
                  
                                                                                    //    break;
                                                                                    //case "5":
                                                                                 
                                                                                    //    break;
                                                                                }

                                                                            }
                                                                            else
                                                                            {
                                                                                //Es nacional

                                                                                switch ((validareader("TipoRelacionId", "CasoId", respuestastrindoc2).Text))
                                                                                {
                                                                                    case "1":
                                                                                        //titular 
                                                                                        if ((validareader("InteresadoTipoPersonaSAT", "CasoId", respuestastrindoc2).Text) != "MN")
                                                                                        {
                                                                                            // es una persona fisica                                                            this.FindAndReplace(wordApp, "<CURP_FISICA>", validareader("InteresadoCurp", "CasoId", respuestastrindoc2).Text);
                                                                                            this.FindAndReplace(wordApp, "<NOMBRE_FISICA>", validareader("InteresadoNombre", "CasoId", respuestastrindoc2).Text);
                                                                                            this.FindAndReplace(wordApp, "<APELLIDOPAT_FISICA>", validareader("InteresadoApPaterno", "CasoId", respuestastrindoc2).Text);
                                                                                            this.FindAndReplace(wordApp, "<APELLIDOMAT_FISICA>", validareader("InteresadoApMaterno", "CasoId", respuestastrindoc2).Text);
                                                                                            this.FindAndReplace(wordApp, "<TELEFONO_OFICINA_FI>", validareader("OficinaTelefono", "CasoId", respuestastrindoc2).Text);
                                                                                            this.FindAndReplace(wordApp, "<CORREO_OFICINA_FI>", validareader("OficinaCorreo", "CasoId", respuestastrindoc2).Text);




                                                                                            conect con_pais_5 = new conect();
                                                                                            String kwery44 = "SELECT * FROM  pais where PaisId  = " + (validareader("nacionalidadid", "CasoId", respuestastrindoc2).Text) + ";";
                                                                                            MySqlDataReader respuestastring144 = con_pais_5.getdatareader(kwery44);

                                                                                            while (respuestastring144.Read())
                                                                                            {
                                                                                                // sininacionalidad = validareader("PaisNacionalidad", "PaisId", respuestastring144).Text;
                                                                                                this.FindAndReplace(wordApp, "<NACIONALIDAD_FISICA>", validareader("PaisNacionalidad", "PaisId", respuestastring144).Text);

                                                                                            }
                                                                                            respuestastring144.Close();
                                                                                            con_pais_5.Cerrarconexion();
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            // es una persona moral
                                                                                            this.FindAndReplace(wordApp, "<RFC_MORAL>", validareader("InteresadoRFC", "CasoId", respuestastrindoc2).Text);
                                                                                            this.FindAndReplace(wordApp, "<RAZON_SOCIAL>", validareader("InteresadoNombre", "CasoId", respuestastrindoc2).Text);
                                                                                            this.FindAndReplace(wordApp, "<TEL_OFICINA_MO>", validareader("OficinaTelefono", "CasoId", respuestastrindoc2).Text);
                                                                                            this.FindAndReplace(wordApp, "<CORREO_OFICINA_MO>", validareader("OficinaCorreo", "CasoId", respuestastrindoc2).Text);
                                                                                            
                                                                                            conect con_pais_6 = new conect();
                                                                                            String kwery44 = "SELECT * FROM  pais where PaisId  = " + (validareader("nacionalidadid", "CasoId", respuestastrindoc2).Text) + ";";
                                                                                            MySqlDataReader respuestastring144 = con_pais_6.getdatareader(kwery44);

                                                                                            while (respuestastring144.Read())
                                                                                            {
                                                                                                // sininacionalidad = validareader("PaisNacionalidad", "PaisId", respuestastring144).Text;
                                                                                                this.FindAndReplace(wordApp, "<NACIONALIDAD_MORAL>", validareader("PaisNacionalidad", "PaisId", respuestastring144).Text);
                                                                                                this.FindAndReplace(wordApp, "<PAIS_TITULAR>", validareader("PaisNombre", "PaisId", respuestastring144).Text);

                                                                                            }
                                                                                            respuestastring144.Close();
                                                                                            con_pais_6.Cerrarconexion();
                                                                                        }

                                                                                          //string calle = validareader("DireccionCalle", "CasoId", respuestastrindoc2).Text;
                                                                                          string colonia = validareader("DireccionColonia", "CasoId", respuestastrindoc2).Text; //la colonia trae saltos de linea
                                                                                          colonia = colonia.Replace(Environment.NewLine, " ");

                                                                                            this.FindAndReplace(wordApp, "<CODIGOPOSTAL_TITULAR>", validareader("DireccionCP", "CasoId", respuestastrindoc2).Text);
                                                                                            this.FindAndReplace(wordApp, "<CALLE_TITULAR>", validareader("DireccionCalle", "CasoId", respuestastrindoc2).Text);
                                                                                            this.FindAndReplace(wordApp, "<NUMEXTERIOR_TITULAR>", validareader("DireccionNumExt", "CasoId", respuestastrindoc2).Text);
                                                                                            this.FindAndReplace(wordApp, "<NUMINTERIOR_TITULAR>", validareader("DireccionNumInt", "CasoId", respuestastrindoc2).Text);
                                                                                            this.FindAndReplace(wordApp, "<COLONIA_TITULAR>", colonia);
                                                                                            //  this.FindAndReplace(wordApp, "<MUNICIPIO_TITULAR>", validareader("DireccionPoblacion", "CasoId", respuestastrindoc2).Text);
                                                                                            this.FindAndReplace(wordApp, "<ESTADO_TITULAR>", validareader("DireccionPoblacion", "CasoId", respuestastrindoc2).Text);
                                                                                            this.FindAndReplace(wordApp, "<LOCALIDAD_TITULAR>", validareader("DireccionEstado", "CasoId", respuestastrindoc2).Text);

                                                                                        break;
                                                                                    case "2":
                                                                                        // Inventor
                                                                                                if (continven0 == 0)
                                                                                                {
                                                                                                    //es el primer inventor
                                                                                                    this.FindAndReplace(wordApp, "<CURP_INVENTOR>", validareader("InteresadoCurp", "CasoId", respuestastrindoc2).Text);
                                                                                                    this.FindAndReplace(wordApp, "<NOMBRE_INVENTOR>", validareader("InteresadoNombre", "CasoId", respuestastrindoc2).Text);
                                                                                                    this.FindAndReplace(wordApp, "<APELLIDOPAT_INVENTOR>", validareader("InteresadoApPaterno", "CasoId", respuestastrindoc2).Text);
                                                                                                    this.FindAndReplace(wordApp, "<APELLIDOMAT_INVNETOR>", validareader("InteresadoApMaterno", "CasoId", respuestastrindoc2).Text);
                                                                                                    this.FindAndReplace(wordApp, "<TELEFONO_OFICINA_INVENTOR>", validareader("OficinaTelefono", "CasoId", respuestastrindoc2).Text);
                                                                                                    this.FindAndReplace(wordApp, "<CORREO_OFICINA_INVENTOR>", validareader("OficinaCorreo", "CasoId", respuestastrindoc2).Text);
                                                                                                    
                                                                                                    conect con_pais_7 = new conect();
                                                                                                    String kwery44 = "SELECT * FROM  pais where PaisId  = " + (validareader("nacionalidadid", "CasoId", respuestastrindoc2).Text) + ";";
                                                                                                    MySqlDataReader respuestastring144 = con_pais_7.getdatareader(kwery44);

                                                                                                    while (respuestastring144.Read())
                                                                                                    {
                                                                                                        //   sininacionalidad_inventor = validareader("PaisNacionalidad", "PaisId", respuestastring144).Text;
                                                                                                        this.FindAndReplace(wordApp, "<NACIONALIDAD_INVENTOR>", validareader("PaisNacionalidad", "PaisId", respuestastring144).Text);
                                                                                                        this.FindAndReplace(wordApp, "<PAIS_INVENTOR>", validareader("PaisNombre", "PaisId", respuestastring144).Text);

                                                                                                    }
                                                                                                    respuestastring144.Close();
                                                                                                    con_pais_7.Cerrarconexion();

                                                                                                    string coloniain = validareader("DireccionColonia", "CasoId", respuestastrindoc2).Text; //la colonia trae saltos de linea
                                                                                                    colonia = coloniain.Replace(Environment.NewLine, " ");
                                                                                                   
                                                                                                    this.FindAndReplace(wordApp, "<CODIGOPOSTAL_INVENTOR>", validareader("DireccionCP", "CasoId", respuestastrindoc2).Text);
                                                                                                    this.FindAndReplace(wordApp, "<CALLE_INVENTOR>", validareader("DireccionCalle", "CasoId", respuestastrindoc2).Text);
                                                                                                    this.FindAndReplace(wordApp, "<NUMEXTERIOR_INVENTOR>", validareader("DireccionNumExt", "CasoId", respuestastrindoc2).Text);
                                                                                                    this.FindAndReplace(wordApp, "<NUMINTERIOR_INVENTOR>", validareader("DireccionNumInt", "CasoId", respuestastrindoc2).Text);
                                                                                                    this.FindAndReplace(wordApp, "<COLONIA_INVENTOR>", colonia);
                                                                                                    //  this.FindAndReplace(wordApp, "<MUNICIPIO_TITULAR>", validareader("DireccionPoblacion", "CasoId", respuestastrindoc2).Text);
                                                                                                    this.FindAndReplace(wordApp, "<ESTADO_INVENTOR>", validareader("DireccionPoblacion", "CasoId", respuestastrindoc2).Text);
                                                                                                    this.FindAndReplace(wordApp, "<LOCALIDAD_INVENTOR>", validareader("DireccionEstado", "CasoId", respuestastrindoc2).Text);

                                                                                                }
                                                                                                continven0++;

                                                                                        break;
                                                                                    case "3":
                                                                                        // Titular/Inventor

                                                                                        break;
                                                                                    //case "4":

                                                                                    //    break;
                                                                                    //case "5":

                                                                                    //    break;
                                                                                }

                                                                            }

                                                                            Icontadorg0++;
                                                                        }// FIN DE LA CONSULTA DE INTERESADOS



                                                                        //fin de primera consulta ( se llena campos titular, interesado, y apoderado FECHAS DE DIVULCACION Y DE PRESENTACION)

                                                                        // COMIENZA CUARTA SE MEJORAN PARA HACER 3 CONSULTAS EN UN SOLO KWERY
                                                                        
                                                                        String querydoc4 = "SELECT * FROM prioridad WHERE prioridad.CasoId = " + sCasoId + " order by prioridad.PrioridadFecha DESC ";
                                                                        MySqlDataReader respuestastrindoc4 = condoc.getdatareader(querydoc4);
                                                                        int count = 0;
                                                                        while (respuestastrindoc4.Read())
                                                                        {
                                                                            if (validareader("TipoPrioridadId", "PrioridadId", respuestastrindoc4).Text != "1")
                                                                            {
                                                                                // es tipo paris
                                                                                this.FindAndReplace(wordApp, "<NUMERO_PARIS>", validareader("PrioridadNumero", "TipoPrioridadId", respuestastrindoc4).Text);
                                                                                if (validareader("PrioridadFecha", "PrioridadId", respuestastrindoc4).Text != "")
                                                                                {
                                                                                    //sinifechaprioridaddia = sinifechaprioridad.Substring(0, 2);
                                                                                    //sinifechaprioridadmes = sinifechaprioridad.Substring(3, 2);
                                                                                    //sinifechaprioridadano = sinifechaprioridad.Substring(6, 4);

                                                                                    this.FindAndReplace(wordApp, "<DIA_PA>", (validareader("PrioridadFecha", "PrioridadId", respuestastrindoc4).Text).Substring(0, 2));
                                                                                    this.FindAndReplace(wordApp, "<MES_PA>", (validareader("PrioridadFecha", "PrioridadId", respuestastrindoc4).Text).Substring(3, 2));
                                                                                    this.FindAndReplace(wordApp, "<ANO_PA>", (validareader("PrioridadFecha", "PrioridadId", respuestastrindoc4).Text).Substring(6, 4));



                                                                                }
                                                                                conect con_pais_8 = new conect();
                                                                                String kwery45 = "SELECT * FROM  pais WHERE PaisId  = " + validareader("PaisID", "PrioridadId", respuestastrindoc4).Text + ";";
                                                                                MySqlDataReader respuestastring145 = con_pais_8.getdatareader(kwery45);
                                                                                while (respuestastring145.Read())
                                                                                {
                                                                                    //siniprioridadpais = validareader("PaisNombre", "PaisId", respuestastring145).Text;
                                                                                    this.FindAndReplace(wordApp, "<PAIS_PARIS>", validareader("PaisNombre", "PaisId", respuestastring145).Text);

                                                                                }

                                                                                respuestastring145.Close();
                                                                                con_pais_8.Cerrarconexion();

                                                                                count++;

                                                                            }
                                                                            else
                                                                            {
                                                                                // es pct
                                                                                this.FindAndReplace(wordApp, "<NUMERO_PCT>", validareader("PrioridadNumero", "TipoPrioridadId", respuestastrindoc4).Text);


                                                                                if (validareader("PrioridadFecha", "PrioridadId", respuestastrindoc4).Text != "")
                                                                                {
                                                                                    this.FindAndReplace(wordApp, "<DIA_PCT>", (validareader("PrioridadFecha", "PrioridadId", respuestastrindoc4).Text).Substring(0, 2));
                                                                                    this.FindAndReplace(wordApp, "<MES_PCT>", (validareader("PrioridadFecha", "PrioridadId", respuestastrindoc4).Text).Substring(3, 2));
                                                                                    this.FindAndReplace(wordApp, "<ANO_PCT>", (validareader("PrioridadFecha", "PrioridadId", respuestastrindoc4).Text).Substring(6, 4));
                                                                                }
                                                                            }// termina tipo de prioridad

                                                                        }

                                                                        respuestastrindoc4.Close();

                                                                        if (count > 1)
                                                                        {
                                                                            // sinix13 = "X";
                                                                            this.FindAndReplace(wordApp, "X13", "X");
                                                                            this.FindAndReplace(wordApp, "X31", "X");
                                                                            //genereaanexoprio();
                                                                        }

                                                                        if (continven0 > 1)
                                                                        {
                                                                            // sinix10 = "X";
                                                                            this.FindAndReplace(wordApp, "X10", "X");
                                                                            this.FindAndReplace(wordApp, "X28", "X");


                                                                        }

                                                                        object fileNametresnuevo = srutacarpeta + "\\Patentes_" + "_" + sCasoId + ".docx";
                                                                        object fileNamenuevo = srutacarpeta + "\\Patentes_" + "_" + sCasoId + ".pdf";
                                                                        limpiadocumentosolicitud();


                                                                        Object fileformatnuevo = Word.WdSaveFormat.wdFormatPDF;
                                                                        Object SaveChangenuevo = Word.WdSaveOptions.wdDoNotSaveChanges;
                                                                        Object OrianalForamtnuevo = Word.WdOriginalFormat.wdOriginalDocumentFormat;

                                                                        aDoc.Activate();
                                                                        aDoc.SaveAs2(ref fileNametresnuevo,
                                                                                       ref missingnuevo, ref missingnuevo, ref missingnuevo, ref missingnuevo, ref missingnuevo,
                                                                                       ref missingnuevo, ref missingnuevo, ref missingnuevo, ref missingnuevo, ref missingnuevo,
                                                                                       ref missingnuevo, ref missingnuevo, ref missingnuevo, ref missingnuevo, ref missingnuevo);
                                                                        aDoc.SaveAs(ref fileNamenuevo, ref fileformatnuevo, ref missingnuevo, ref missingnuevo, ref missingnuevo, ref missingnuevo,
                                                                                    ref missingnuevo, ref missingnuevo, ref missingnuevo, ref missingnuevo, ref missingnuevo, ref missingnuevo, ref missingnuevo, ref missingnuevo, ref missingnuevo, ref missingnuevo);
                                                                        aDoc.Saved = true;
                                                                        //wordApp.Documents.Close(ref fileNametresnuevo, ref missingnuevo, ref missingnuevo);
                                                                        //wordApp.Quit(ref missingnuevo, ref missingnuevo, ref missingnuevo);
                                                                        //wordApp = null;
                                                                        Process.Start(fileNametresnuevo.ToString());
                                                                        Process.Start(fileNamenuevo.ToString());
                                                                        if (count > 1)
                                                                        {
                                                                            // sinix13 = "X";
                                                                            // this.FindAndReplace(wordApp, "X13", "X");
                                                                            genereaanexoprio();
                                                                        }
                                                                        
                
                if (Icontadorg0 > 2)//si los tituares o interesados con mayores a dos(2) debemos generar un anexo titulares                                                  
                {//generamos el anexo, como ya sabemos cuál es el número de interesados y o titulares generamos el número de anexos necesarios sean par o non}
                    
                    int iNumerodeanexonuevo = Icontadorg0 / 2;
                    int iNumerdearchivosnuevo = 0;

                    if ((Icontadorg0 % 2) == 0)
                    {
                        iNumerdearchivosnuevo = iNumerodeanexonuevo - 1;
                    }
                    else
                    {
                        iNumerdearchivosnuevo = iNumerodeanexonuevo;
                    }

                    int contadorfor2 = 0;


                    for (int z = 0; z < iNumerdearchivosnuevo; z++)                            
                    {

                        Random random = new Random();
                        int randomNumber = random.Next(0, 100);
                        //object fileNametres = srutacarpeta + "\\Patentes_" + randomNumber + "_" + sCasoId + ".docx";
                        object fileNametres1nuevo = srutacarpeta + "\\Interesados_" + randomNumber + "_" + sCasoId + ".doc";
                        object fileName1nuevo = srutacarpeta + "\\Interesados_" + randomNumber + "_" + sCasoId + ".pdf";
                        object missing2nuevo = Missing.Value;

                        object missing3 = Missing.Value;
                        wordApp.Quit(ref missing3, ref missing3, ref missing3);
                        wordApp = new Word.Application();
                        aDoc = null;
                        int contadoranexo2 = 0;
                        object missing = Missing.Value;
                        wordApp.Quit(ref missing, ref missing, ref missing);
                        wordApp = new Word.Application();
                        aDoc = null;

                        object abrirDoc2 = rutainteresado;


                        object readOnly1 = false; //default
                        object isVisible1 = true;

                        aDoc = wordApp.Documents.Open(ref abrirDoc2, ref isVisible1, ref readOnly1,
                           ref missing2nuevo, ref missing2nuevo, ref missing2nuevo,
                           ref missing2nuevo, ref missing2nuevo, ref missing2nuevo,
                           ref missing2nuevo, ref missing2, ref missing2,
                           ref missing2nuevo, ref missing2nuevo, ref missing2nuevo, ref missing2nuevo);

                        Random random2 = new Random();
                        int randomNumber2 = random2.Next(0, 100);


                        for (int x = contadorfor2; x < contadorfor2 + 2; x++)
                        {
                            if (x >= Icontadorg0 - 2)
                            {
                                Console.Write("se termina el ciclo");
                                limpiardocumentoanexointeresados();
                            }
                            else
                            {
                                string siniInteresadoNombre0 = aNombre0[x];
                                string siniInteresadoApPaterno0 = aApellidopa0[x];
                                string siniInteresadoApMaterno0 = aApellidoma0[x];
                                string siniInteresadoCurp0 = aCurp0[x];
                                string sininacionalidadid0 = aNacionalidad0[x]; /// 
                                string siniOficinaTelefono0 = aTelefono0[x];
                                string siniOficinaCorreo0 = aCorreo0[x];
                                string siniDireccionCP0 = aCP0[x];
                                string siniDireccionCalle0 = aCalle0[x];
                                string siniDireccionNumExt0 = aNumeroexterior0[x];
                                string siniDireccionNumInt0 = aNumint0[x];
                                string siniDireccionColonia0 = aColonia0[x];
                                string siniDireccionPoblacion0 = aMunicipio0[x];
                                string siniDireccionEstado0 = aEstado0[x];
                                string sinipaisdireccioninteresado0 = aPais0[x]; // aqui
                                string siniDireccionPoblacion000 = aLocalidad0[x];
                                string siniInteresadoRFC0 = aRFC0[x];
                                string sininacionalidad220 = "";
                                conect con_pais_9 = new conect();
                                String kwery47 = "SELECT * FROM  pais where PaisId  = " + sininacionalidadid0 + ";";
                                MySqlDataReader respuestastring147 = con_pais_9.getdatareader(kwery47);
                                if (respuestastring147 == null)
                                {
                                    sininacionalidad220 = "";
                                }
                                else
                                {
                                    while (respuestastring147.Read())
                                    {
                                        sininacionalidad220 = validareader("PaisNacionalidad", "PaisId", respuestastring147).Text;
                                    }
                                }
                                respuestastring147.Close();
                                con_pais_9.Cerrarconexion();

                                conect con_pais_10 = new conect();
                                String kwery4 = "SELECT * FROM  pais where PaisId  = " + sinipaisdireccioninteresado0 + ";";
                                MySqlDataReader respuestastring4 = con_pais_10.getdatareader(kwery4);
                                if (respuestastring4 == null)
                                {
                                    sinipaisdireccioninteresado0 = "";
                                }
                                else
                                {
                                    while (respuestastring4.Read())
                                    {
                                        sinipaisdireccioninteresado0 = validareader("PaisNombre", "PaisId", respuestastring4).Text; ;
                                    }
                                }
                                respuestastring4.Close();
                                con_pais_10.Cerrarconexion();

                                if (!sininacionalidadid0.Equals("148"))
                                {
                                    // es extrangero
                                    if ((validareader("TipoRelacionId", "CasoId", respuestastrindoc2).Text) != "1")
                                    { //es inventor

                                        if (contadoranexo2 > 0)
                                        {
                                            //this.FindAndReplace(wordApp, "X2", "");
                                            this.FindAndReplace(wordApp, "X4", "X");

                                        }
                                        else
                                        {
                                            this.FindAndReplace(wordApp, "X2", "X");
                                            //this.FindAndReplace(wordApp, "X4", "");

                                        }


                                        if ((validareader("InteresadoTipoPersonaSAT", "CasoId", respuestastrindoc2).Text) != "ME")
                                        {
                                            //FISICA EXTRANGERA
                                            this.FindAndReplace(wordApp, "<CURP_FISICA" + (contadoranexo2 + 1) + ">", "");
                                            this.FindAndReplace(wordApp, "<NOMBRE_FISICA" + (contadoranexo2 + 1) + ">", siniInteresadoNombre0);
                                            this.FindAndReplace(wordApp, "<APELLIDO_PATFISICA" + (contadoranexo2 + 1) + ">", siniInteresadoApPaterno0);
                                            this.FindAndReplace(wordApp, "<APELLIDO_MATFISICA" + (contadoranexo2 + 1) + ">", siniInteresadoApMaterno0);
                                            this.FindAndReplace(wordApp, "<NACIONALIDAD_FISICA" + (contadoranexo2 + 1) + ">", sininacionalidad220);
                                            this.FindAndReplace(wordApp, "<TELEFONO_FISICA" + (contadoranexo2 + 1) + ">", siniOficinaTelefono0);
                                            this.FindAndReplace(wordApp, "<CORREO_FISICA" + (contadoranexo2 + 1) + ">", siniOficinaCorreo0);
                                            this.FindAndReplace(wordApp, "<CP_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionCP0);
                                            this.FindAndReplace(wordApp, "<CALLE_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionCalle0);
                                            this.FindAndReplace(wordApp, "<NUMERO_EXTERIOR" + (contadoranexo2 + 1) + ">", siniDireccionNumExt0);
                                            this.FindAndReplace(wordApp, "<NUMERO_INTERIOR" + (contadoranexo2 + 1) + ">", siniDireccionNumInt0);
                                            this.FindAndReplace(wordApp, "<COLONIA_INTERESADO" + (contadoranexo2 + 1) + ">", "");
                                            this.FindAndReplace(wordApp, "<MUNICIPIO_INTERESADO" + (contadoranexo2 + 1) + ">", "");
                                            this.FindAndReplace(wordApp, "<LOCALIDAD_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionPoblacion0);
                                            this.FindAndReplace(wordApp, "<ESTADO_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionEstado0);
                                            this.FindAndReplace(wordApp, "<PAIS_INTERESADO" + (contadoranexo2 + 1) + ">", sinipaisdireccioninteresado0);

                                        }
                                        else
                                        {
                                            //MORAL EXTRANGERA
                                            this.FindAndReplace(wordApp, "<RFC_MORAL" + (contadoranexo2 + 1) + ">", "");
                                            this.FindAndReplace(wordApp, "<NOMBRE_MORAL" + (contadoranexo2 + 1) + ">", siniInteresadoNombre0);
                                            this.FindAndReplace(wordApp, "<NACIONALIDAD_MORAL" + (contadoranexo2 + 1) + ">", sininacionalidad220);
                                            this.FindAndReplace(wordApp, "<TELEFONO_MORAL" + (contadoranexo2 + 1) + ">", siniOficinaTelefono0);
                                            this.FindAndReplace(wordApp, "<CORREO_MORAL" + (contadoranexo2 + 1) + ">", siniOficinaTelefono0);
                                            this.FindAndReplace(wordApp, "<CP_MORAL" + (contadoranexo2 + 1) + ">", siniOficinaCorreo0);
                                            this.FindAndReplace(wordApp, "<CALLE_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionCalle0);
                                            this.FindAndReplace(wordApp, "<NUMERO_EXTERIOR" + (contadoranexo2 + 1) + ">", siniDireccionNumExt0);
                                            this.FindAndReplace(wordApp, "<NUMERO_INTERIOR" + (contadoranexo2 + 1) + ">", siniDireccionNumInt0);
                                            this.FindAndReplace(wordApp, "<COLONIA_INTERESADO" + (contadoranexo2 + 1) + ">", "");
                                            this.FindAndReplace(wordApp, "<MUNICIPIO_INTERESADO" + (contadoranexo2 + 1) + ">", "");
                                            this.FindAndReplace(wordApp, "<LOCALIDAD_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionPoblacion0);
                                            this.FindAndReplace(wordApp, "<ESTADO_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionEstado0);
                                            this.FindAndReplace(wordApp, "<PAIS_INTERESADO" + (contadoranexo2 + 1) + ">", sinipaisdireccioninteresado0);

                                        }
                                    }
                                    else
                                    {
                                        // es titular

                                        if (contadoranexo2 > 0)
                                        {
                                            //  this.FindAndReplace(wordApp, "X1", "");
                                            this.FindAndReplace(wordApp, "X3", "X");
                                            //x titular 1
                                        }
                                        else
                                        {
                                            //x titular 2
                                            this.FindAndReplace(wordApp, "X1", "X");
                                            //this.FindAndReplace(wordApp, "X3", "");

                                        }
                                        if ((validareader("InteresadoTipoPersonaSAT", "CasoId", respuestastrindoc2).Text) != "ME")
                                        {
                                            //FISICA EXTRANGERA
                                            this.FindAndReplace(wordApp, "<CURP_FISICA" + (contadoranexo2 + 1) + ">", "");
                                            this.FindAndReplace(wordApp, "<NOMBRE_FISICA" + (contadoranexo2 + 1) + ">", siniInteresadoNombre0);
                                            this.FindAndReplace(wordApp, "<APELLIDO_PATFISICA" + (contadoranexo2 + 1) + ">", siniInteresadoApPaterno0);
                                            this.FindAndReplace(wordApp, "<APELLIDO_MATFISICA" + (contadoranexo2 + 1) + ">", siniInteresadoApMaterno0);
                                            this.FindAndReplace(wordApp, "<NACIONALIDAD_FISICA" + (contadoranexo2 + 1) + ">", sininacionalidad220);
                                            this.FindAndReplace(wordApp, "<TELEFONO_FISICA" + (contadoranexo2 + 1) + ">", siniOficinaTelefono0);
                                            this.FindAndReplace(wordApp, "<CORREO_FISICA" + (contadoranexo2 + 1) + ">", siniOficinaCorreo0);
                                            this.FindAndReplace(wordApp, "<CP_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionCP0);
                                            this.FindAndReplace(wordApp, "<CALLE_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionCalle0);
                                            this.FindAndReplace(wordApp, "<NUMERO_EXTERIOR" + (contadoranexo2 + 1) + ">", siniDireccionNumExt0);
                                            this.FindAndReplace(wordApp, "<NUMERO_INTERIOR" + (contadoranexo2 + 1) + ">", siniDireccionNumInt0);
                                            this.FindAndReplace(wordApp, "<COLONIA_INTERESADO" + (contadoranexo2 + 1) + ">", "");
                                            this.FindAndReplace(wordApp, "<MUNICIPIO_INTERESADO" + (contadoranexo2 + 1) + ">", "");
                                            this.FindAndReplace(wordApp, "<LOCALIDAD_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionPoblacion0);
                                            this.FindAndReplace(wordApp, "<ESTADO_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionEstado0);
                                            this.FindAndReplace(wordApp, "<PAIS_INTERESADO" + (contadoranexo2 + 1) + ">", sinipaisdireccioninteresado0);

                                        }
                                        else
                                        {
                                            //MORAL EXTRANGERA
                                            this.FindAndReplace(wordApp, "<RFC_MORAL" + (contadoranexo2 + 1) + ">", "");
                                            this.FindAndReplace(wordApp, "<NOMBRE_MORAL" + (contadoranexo2 + 1) + ">", siniInteresadoNombre0);
                                            this.FindAndReplace(wordApp, "<NACIONALIDAD_MORAL" + (contadoranexo2 + 1) + ">", sininacionalidad220);
                                            this.FindAndReplace(wordApp, "<TELEFONO_MORAL" + (contadoranexo2 + 1) + ">", siniOficinaTelefono0);
                                            this.FindAndReplace(wordApp, "<CORREO_MORAL" + (contadoranexo2 + 1) + ">", siniOficinaTelefono0);
                                            this.FindAndReplace(wordApp, "<CP_MORAL" + (contadoranexo2 + 1) + ">", siniOficinaCorreo0);
                                            this.FindAndReplace(wordApp, "<CALLE_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionCalle0);
                                            this.FindAndReplace(wordApp, "<NUMERO_EXTERIOR" + (contadoranexo2 + 1) + ">", siniDireccionNumExt0);
                                            this.FindAndReplace(wordApp, "<NUMERO_INTERIOR" + (contadoranexo2 + 1) + ">", siniDireccionNumInt0);
                                            this.FindAndReplace(wordApp, "<COLONIA_INTERESADO" + (contadoranexo2 + 1) + ">", "");
                                            this.FindAndReplace(wordApp, "<MUNICIPIO_INTERESADO" + (contadoranexo2 + 1) + ">", "");
                                            this.FindAndReplace(wordApp, "<LOCALIDAD_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionPoblacion0);
                                            this.FindAndReplace(wordApp, "<ESTADO_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionEstado0);
                                            this.FindAndReplace(wordApp, "<PAIS_INTERESADO" + (contadoranexo2 + 1) + ">", sinipaisdireccioninteresado0);

                                        }

                                    }

                                }
                                else
                                {
                                    // es mexicano

                                    if ((validareader("TipoRelacionId", "CasoId", respuestastrindoc2).Text) != "1")
                                    { //es inventor}

                                        if (contadoranexo2 > 0)
                                        {
                                            //this.FindAndReplace(wordApp, "X2", "");
                                            this.FindAndReplace(wordApp, "X4", "X");

                                        }
                                        else
                                        {
                                            this.FindAndReplace(wordApp, "X2", "X");
                                            //this.FindAndReplace(wordApp, "X4", "");

                                        }

                                        if ((validareader("InteresadoTipoPersonaSAT", "CasoId", respuestastrindoc2).Text) != "ME")
                                        {
                                            //FISICA EXTRANGERA
                                            this.FindAndReplace(wordApp, "<CURP_FISICA" + (contadoranexo2 + 1) + ">", "");
                                            this.FindAndReplace(wordApp, "<NOMBRE_FISICA" + (contadoranexo2 + 1) + ">", siniInteresadoNombre0);
                                            this.FindAndReplace(wordApp, "<APELLIDO_PATFISICA" + (contadoranexo2 + 1) + ">", siniInteresadoApPaterno0);
                                            this.FindAndReplace(wordApp, "<APELLIDO_MATFISICA" + (contadoranexo2 + 1) + ">", siniInteresadoApMaterno0);
                                            this.FindAndReplace(wordApp, "<NACIONALIDAD_FISICA" + (contadoranexo2 + 1) + ">", sininacionalidad220);
                                            this.FindAndReplace(wordApp, "<TELEFONO_FISICA" + (contadoranexo2 + 1) + ">", siniOficinaTelefono0);
                                            this.FindAndReplace(wordApp, "<CORREO_FISICA" + (contadoranexo2 + 1) + ">", siniOficinaCorreo0);
                                            this.FindAndReplace(wordApp, "<CP_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionCP0);
                                            this.FindAndReplace(wordApp, "<CALLE_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionCalle0);
                                            this.FindAndReplace(wordApp, "<NUMERO_EXTERIOR" + (contadoranexo2 + 1) + ">", siniDireccionNumExt0);
                                            this.FindAndReplace(wordApp, "<NUMERO_INTERIOR" + (contadoranexo2 + 1) + ">", siniDireccionNumInt0);
                                            this.FindAndReplace(wordApp, "<COLONIA_INTERESADO" + (contadoranexo2 + 1) + ">", "");
                                            this.FindAndReplace(wordApp, "<MUNICIPIO_INTERESADO" + (contadoranexo2 + 1) + ">", "");
                                            this.FindAndReplace(wordApp, "<LOCALIDAD_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionPoblacion0);
                                            this.FindAndReplace(wordApp, "<ESTADO_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionEstado0);
                                            this.FindAndReplace(wordApp, "<PAIS_INTERESADO" + (contadoranexo2 + 1) + ">", sinipaisdireccioninteresado0);

                                        }
                                        else
                                        {
                                            //MORAL EXTRANGERA
                                            this.FindAndReplace(wordApp, "<RFC_MORAL" + (contadoranexo2 + 1) + ">", "");
                                            this.FindAndReplace(wordApp, "<NOMBRE_MORAL" + (contadoranexo2 + 1) + ">", siniInteresadoNombre0);
                                            this.FindAndReplace(wordApp, "<NACIONALIDAD_MORAL" + (contadoranexo2 + 1) + ">", sininacionalidad220);
                                            this.FindAndReplace(wordApp, "<TELEFONO_MORAL" + (contadoranexo2 + 1) + ">", siniOficinaTelefono0);
                                            this.FindAndReplace(wordApp, "<CORREO_MORAL" + (contadoranexo2 + 1) + ">", siniOficinaTelefono0);
                                            this.FindAndReplace(wordApp, "<CP_MORAL" + (contadoranexo2 + 1) + ">", siniOficinaCorreo0);
                                            this.FindAndReplace(wordApp, "<CALLE_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionCalle0);
                                            this.FindAndReplace(wordApp, "<NUMERO_EXTERIOR" + (contadoranexo2 + 1) + ">", siniDireccionNumExt0);
                                            this.FindAndReplace(wordApp, "<NUMERO_INTERIOR" + (contadoranexo2 + 1) + ">", siniDireccionNumInt0);
                                            this.FindAndReplace(wordApp, "<COLONIA_INTERESADO" + (contadoranexo2 + 1) + ">", "");
                                            this.FindAndReplace(wordApp, "<MUNICIPIO_INTERESADO" + (contadoranexo2 + 1) + ">", "");
                                            this.FindAndReplace(wordApp, "<LOCALIDAD_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionPoblacion0);
                                            this.FindAndReplace(wordApp, "<ESTADO_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionEstado0);
                                            this.FindAndReplace(wordApp, "<PAIS_INTERESADO" + (contadoranexo2 + 1) + ">", sinipaisdireccioninteresado0);

                                        }

                                    }
                                    else
                                    {
                                        // es titular

                                        if (contadoranexo2 > 0)
                                        {
                                            //  this.FindAndReplace(wordApp, "X1", "");
                                            this.FindAndReplace(wordApp, "X3", "X");
                                            //x titular 1
                                        }
                                        else
                                        {
                                            //x titular 2
                                            this.FindAndReplace(wordApp, "X1", "X");
                                            //this.FindAndReplace(wordApp, "X3", "");

                                        }


                                        if ((validareader("InteresadoTipoPersonaSAT", "CasoId", respuestastrindoc2).Text) != "ME")
                                        {
                                            //FISICA EXTRANGERA
                                            this.FindAndReplace(wordApp, "<CURP_FISICA" + (contadoranexo2 + 1) + ">", "");
                                            this.FindAndReplace(wordApp, "<NOMBRE_FISICA" + (contadoranexo2 + 1) + ">", siniInteresadoNombre0);
                                            this.FindAndReplace(wordApp, "<APELLIDO_PATFISICA" + (contadoranexo2 + 1) + ">", siniInteresadoApPaterno0);
                                            this.FindAndReplace(wordApp, "<APELLIDO_MATFISICA" + (contadoranexo2 + 1) + ">", siniInteresadoApMaterno0);
                                            this.FindAndReplace(wordApp, "<NACIONALIDAD_FISICA" + (contadoranexo2 + 1) + ">", sininacionalidad220);
                                            this.FindAndReplace(wordApp, "<TELEFONO_FISICA" + (contadoranexo2 + 1) + ">", siniOficinaTelefono0);
                                            this.FindAndReplace(wordApp, "<CORREO_FISICA" + (contadoranexo2 + 1) + ">", siniOficinaCorreo0);
                                            this.FindAndReplace(wordApp, "<CP_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionCP0);
                                            this.FindAndReplace(wordApp, "<CALLE_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionCalle0);
                                            this.FindAndReplace(wordApp, "<NUMERO_EXTERIOR" + (contadoranexo2 + 1) + ">", siniDireccionNumExt0);
                                            this.FindAndReplace(wordApp, "<NUMERO_INTERIOR" + (contadoranexo2 + 1) + ">", siniDireccionNumInt0);
                                            this.FindAndReplace(wordApp, "<COLONIA_INTERESADO" + (contadoranexo2 + 1) + ">", "");
                                            this.FindAndReplace(wordApp, "<MUNICIPIO_INTERESADO" + (contadoranexo2 + 1) + ">", "");
                                            this.FindAndReplace(wordApp, "<LOCALIDAD_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionPoblacion0);
                                            this.FindAndReplace(wordApp, "<ESTADO_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionEstado0);
                                            this.FindAndReplace(wordApp, "<PAIS_INTERESADO" + (contadoranexo2 + 1) + ">", sinipaisdireccioninteresado0);

                                        }
                                        else
                                        {
                                            //MORAL EXTRANGERA
                                            this.FindAndReplace(wordApp, "<RFC_MORAL" + (contadoranexo2 + 1) + ">", "");
                                            this.FindAndReplace(wordApp, "<NOMBRE_MORAL" + (contadoranexo2 + 1) + ">", siniInteresadoNombre0);
                                            this.FindAndReplace(wordApp, "<NACIONALIDAD_MORAL" + (contadoranexo2 + 1) + ">", sininacionalidad220);
                                            this.FindAndReplace(wordApp, "<TELEFONO_MORAL" + (contadoranexo2 + 1) + ">", siniOficinaTelefono0);
                                            this.FindAndReplace(wordApp, "<CORREO_MORAL" + (contadoranexo2 + 1) + ">", siniOficinaTelefono0);
                                            this.FindAndReplace(wordApp, "<CP_MORAL" + (contadoranexo2 + 1) + ">", siniOficinaCorreo0);
                                            this.FindAndReplace(wordApp, "<CALLE_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionCalle0);
                                            this.FindAndReplace(wordApp, "<NUMERO_EXTERIOR" + (contadoranexo2 + 1) + ">", siniDireccionNumExt0);
                                            this.FindAndReplace(wordApp, "<NUMERO_INTERIOR" + (contadoranexo2 + 1) + ">", siniDireccionNumInt0);
                                            this.FindAndReplace(wordApp, "<COLONIA_INTERESADO" + (contadoranexo2 + 1) + ">", "");
                                            this.FindAndReplace(wordApp, "<MUNICIPIO_INTERESADO" + (contadoranexo2 + 1) + ">", "");
                                            this.FindAndReplace(wordApp, "<LOCALIDAD_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionPoblacion0);
                                            this.FindAndReplace(wordApp, "<ESTADO_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionEstado0);
                                            this.FindAndReplace(wordApp, "<PAIS_INTERESADO" + (contadoranexo2 + 1) + ">", sinipaisdireccioninteresado0);

                                        }
                                    }
                                }
                                // es mexicano
                            }
                            contadoranexo2++;
                            if (contadoranexo2 > 1)
                            {
                                limpiardocumentoanexointeresados();
                            }
                        }

                        Thread.Sleep(3000);
                        contadorfor2 += 2;

                        Object fileformat2 = Word.WdSaveFormat.wdFormatPDF;
                        Object SaveChange2 = Word.WdSaveOptions.wdDoNotSaveChanges;
                        Object OrianalForamt2 = Word.WdOriginalFormat.wdOriginalDocumentFormat;

                        aDoc.Activate();
                        aDoc.SaveAs2(ref fileNametres1nuevo,
                                         ref missing2nuevo, ref missing2nuevo, ref missing2nuevo, ref missing2nuevo, ref missing2nuevo,
                                        ref missing2nuevo, ref missing2nuevo, ref missing2nuevo, ref missing2nuevo, ref missing2nuevo,
                                        ref missing2nuevo, ref missing2nuevo, ref missing2nuevo, ref missing2nuevo, ref missing2nuevo);
                        aDoc.SaveAs(ref fileName1nuevo, ref fileformat2, ref missing2, ref missing2, ref missing2, ref missing2,
                                  ref missing2, ref missing2, ref missing2, ref missing2, ref missing2, ref missing2, ref missing2, ref missing2, ref missing2, ref missing2);
                        aDoc.Saved = true;
                        //wordApp.Documents.Close(ref fileNametres1nuevo, ref missing2nuevo, ref missing2nuevo);
                        wordApp.Quit(ref missing3, ref missing3, ref missing3);
                        //wordApp = null;
                        Process.Start(fileNametres1nuevo.ToString());
                        Process.Start(fileName1nuevo.ToString());

                    }// AQUI TERMINA EL FOR





                }// AQUI TERMINA EL ANEXO DE INTERESADOS

                condoc.Cerrarconexion();
            }
            catch (Exception E)
            {
                //condoc.Cerrarconexion();
                //String strRutaArchivo = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
                //String fechalog = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                //StringBuilder sb = new StringBuilder();
                //sb.Append(fechalog + ":user:documentos: " + E + "\n");
                //System.IO.File.AppendAllText(strRutaArchivo+"\\casosking\\" + "events.log", sb.ToString());
                //sb.Clear();
                new filelog("desde nuvo doc", E.ToString());

            }

            
        }


        private void FindAndReplace(Microsoft.Office.Interop.Word.Application wordApp, object findText, object replaceWithText)
        {
            try
            {
                object matchCase = true;
                object matchWholeWord = true;
                object matchWildCards = false;
                object matchSoundLike = false;
                object nmatchAllForms = false;
                object forward = true;
                object format = false;
                object matchKashida = false;
                object matchDiactitics = false;
                object matchAlefHamza = false;
                object matchControl = false;
                object read_only = false;
                object visible = true;
                object replace = 2;
                object wrap = 1;

                wordApp.Selection.Find.Execute(ref findText,
                            ref matchCase, ref matchWholeWord,
                            ref matchWildCards, ref matchSoundLike,
                            ref nmatchAllForms, ref forward,
                            ref wrap, ref format, ref replaceWithText,
                            ref replace, ref matchKashida,
                            ref matchDiactitics, ref matchAlefHamza,
                            ref matchControl);
            }
            catch (Exception E)
            {
                String strRutaArchivo = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
                String fechalog = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                StringBuilder sb = new StringBuilder();
                sb.Append(fechalog + ":user:documentos: " + E + "\n");
                System.IO.File.AppendAllText(strRutaArchivo+"\\casosking\\" + "events.log", sb.ToString());
                sb.Clear();

            }
        }

        public ComboboxItem validareader(String campoText, String campoValue, MySqlDataReader mresultado)
        {

            ComboboxItem cItemresult = new ComboboxItem();

            if (!mresultado.IsDBNull(mresultado.GetOrdinal(campoText)))
            {
                cItemresult.Text = mresultado.GetString(mresultado.GetOrdinal(campoText));
            }
            else
            {
                cItemresult.Text = "";
            }

            if (!mresultado.IsDBNull(mresultado.GetOrdinal(campoValue)))
            {
                cItemresult.Value = mresultado.GetString(mresultado.GetOrdinal(campoValue));
            }
            else
            {
                cItemresult.Value = "";
            }
            return cItemresult;
        }

        public void limpiardocumentoanexointeresados()
        {
            try
            {

                this.FindAndReplace(wordApp, "<CURP_FISICA1>", "");
                this.FindAndReplace(wordApp, "<NOMBRE_FISICA1>", "");
                this.FindAndReplace(wordApp, "<APELLIDO_PATFISICA1>", "");
                this.FindAndReplace(wordApp, "<APELLIDO_MATFISICA1>", "");
                this.FindAndReplace(wordApp, "<NACIONALIDAD_FISICA1>", "");
                this.FindAndReplace(wordApp, "<TELEFONO_FISICA1>", "");
                this.FindAndReplace(wordApp, "<CORREO_FISICA1>", "");
                this.FindAndReplace(wordApp, "<RFC_MORAL1>", "");
                this.FindAndReplace(wordApp, "<NOMBRE_MORAL1>", "");
                this.FindAndReplace(wordApp, "<NACIONALIDAD_MORAL1>", "");
                this.FindAndReplace(wordApp, "<TELEFONO_MORAL1>", "");
                this.FindAndReplace(wordApp, "<CORREO_MORAL1>", "");
                this.FindAndReplace(wordApp, "<CP_INTERESADO1>", "");
                this.FindAndReplace(wordApp, "<CALLE_INTERESADO1>", "");
                this.FindAndReplace(wordApp, "<NUMERO_EXTERIOR1>", "");
                this.FindAndReplace(wordApp, "<MUNICIPIO_INTERESADO1>", "");
                this.FindAndReplace(wordApp, "<ESTADO_INTERESADO1>", "");
                this.FindAndReplace(wordApp, "<PAIS_INTERESADO1>", "");
                this.FindAndReplace(wordApp, "<LOCALIDAD_INTERESADO1>", "");
                this.FindAndReplace(wordApp, "<CURP_FISICA2>", "");
                this.FindAndReplace(wordApp, "<NOMBRE_FISICA2>", "");
                this.FindAndReplace(wordApp, "<APELLIDO_PATFISICA2>", "");
                this.FindAndReplace(wordApp, "X1", "");
                this.FindAndReplace(wordApp, "X2", "");
                this.FindAndReplace(wordApp, "X3", "");
                this.FindAndReplace(wordApp, "X4", "");
                this.FindAndReplace(wordApp, "<APELLIDO_MATFISICA2>", "");
                this.FindAndReplace(wordApp, "<NACIONALIDAD_FISICA2>", "");
                this.FindAndReplace(wordApp, "<TELEFONO_FISICA2>", "");
                this.FindAndReplace(wordApp, "<CORREO_FISICA2>", "");
                this.FindAndReplace(wordApp, "<RFC_MORAL2>", "");
                this.FindAndReplace(wordApp, "<NOMBRE_MORAL2>", "");
                this.FindAndReplace(wordApp, "<NACIONALIDAD_MORAL2>", "");
                this.FindAndReplace(wordApp, "<TELEFONO_MORAL2>", "");
                this.FindAndReplace(wordApp, "<CORREO_MORAL2>", "");
                this.FindAndReplace(wordApp, "<CP_INTERESADO2>", "");
                this.FindAndReplace(wordApp, "<CALLE_INTERESADO2>", "");
                this.FindAndReplace(wordApp, "<NUMERO_EXTERIOR2>", "");
                this.FindAndReplace(wordApp, "<COLONIA_INTERESADO2>", "");
                this.FindAndReplace(wordApp, "<NUMERO_INTERIOR2>", "");
                this.FindAndReplace(wordApp, "<MUNICIPIO_INTERESADO2>", "");
                this.FindAndReplace(wordApp, "<ESTADO_INTERESADO2>", "");
                this.FindAndReplace(wordApp, "<LOCALIDAD_INTERESADO2>", "");
                this.FindAndReplace(wordApp, "<PAIS_INTERESADO2>", "");
                this.FindAndReplace(wordApp, "<COLONIA_INTERESADO1>", "");
                this.FindAndReplace(wordApp, "<NUMERO_INTERIOR1>", "");
            }
            catch (Exception E)
            {
                String strRutaArchivo = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
                String fechalog = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                StringBuilder sb = new StringBuilder();
                sb.Append(fechalog + ":user:documentos: " + E + "\n");
                System.IO.File.AppendAllText(strRutaArchivo+"\\casosking\\" + "events.log", sb.ToString());
                sb.Clear();

            }
        }

        public void limpiadocumentosolicitud()
        {
            try
            {

                this.FindAndReplace(wordApp, "X1", "");
                this.FindAndReplace(wordApp, "X2", "");
                this.FindAndReplace(wordApp, "X3", "");
                this.FindAndReplace(wordApp, "X4", "");
                this.FindAndReplace(wordApp, "X5", "");
                this.FindAndReplace(wordApp, "X6", "");
                this.FindAndReplace(wordApp, "X7", "");
                this.FindAndReplace(wordApp, "X8", "");
                this.FindAndReplace(wordApp, "X9", "");
                this.FindAndReplace(wordApp, "X10", "");
                this.FindAndReplace(wordApp, "X11", "");
                this.FindAndReplace(wordApp, "X12", "");
                this.FindAndReplace(wordApp, "X13", "");
                this.FindAndReplace(wordApp, "X14", "");
                this.FindAndReplace(wordApp, "X15", "");
                this.FindAndReplace(wordApp, "X16", "");
                this.FindAndReplace(wordApp, "X17", "");
                this.FindAndReplace(wordApp, "X18", "");
                this.FindAndReplace(wordApp, "X19", "");
                this.FindAndReplace(wordApp, "X20", "");
                this.FindAndReplace(wordApp, "X21", "");
                this.FindAndReplace(wordApp, "X22", "");
                this.FindAndReplace(wordApp, "X23", "");
                this.FindAndReplace(wordApp, "X24", "");
                this.FindAndReplace(wordApp, "X25", "");
                this.FindAndReplace(wordApp, "X26", "");
                this.FindAndReplace(wordApp, "X27", "");
                this.FindAndReplace(wordApp, "X28", "");
                this.FindAndReplace(wordApp, "X29", "");
                this.FindAndReplace(wordApp, "X30", "");
                this.FindAndReplace(wordApp, "X31", "");
                this.FindAndReplace(wordApp, "<TELEFONO_OFICINA_INVENTOR>", "");
                this.FindAndReplace(wordApp, "<CORREO_OFICINA_INVENTOR>", "");
                this.FindAndReplace(wordApp, "<CURP_FISICA>", "");
                this.FindAndReplace(wordApp, "<NOMBRE_FISICA>", "");
                this.FindAndReplace(wordApp, "<APELLIDOPAT_FISICA>", "");
                this.FindAndReplace(wordApp, "<APELLIDOMAT_FISICA>", "");
                this.FindAndReplace(wordApp, "<NACIONALIDAD_FISICA>", "");
                this.FindAndReplace(wordApp, "<TEL_OFICINA_MO>", "");
                this.FindAndReplace(wordApp, "<CORREO_OFICINA_MO>", "");
                this.FindAndReplace(wordApp, "<RFC_MORAL>", "");
                this.FindAndReplace(wordApp, "<RAZON_SOCIAL>", "");
                this.FindAndReplace(wordApp, "<NACIONALIDAD_MORAL>", "");
                this.FindAndReplace(wordApp, "<CODIGOPOSTAL_TITULAR>", "");
                this.FindAndReplace(wordApp, "<CALLE_TITULAR>", "");
                this.FindAndReplace(wordApp, "<NUMEXTERIOR_TITULAR>", "");
                this.FindAndReplace(wordApp, "<NUMINTERIOR_TITULAR>", "");
                this.FindAndReplace(wordApp, "<COLONIA_TITULAR>", "");
                this.FindAndReplace(wordApp, "<MUNICIPIO_TITULAR>", "");
                this.FindAndReplace(wordApp, "<LOCALIDAD_TITULAR>", "");
                this.FindAndReplace(wordApp, "<ESTADO_TITULAR>", "");
                this.FindAndReplace(wordApp, "<PAIS_TITULAR>", "");
                this.FindAndReplace(wordApp, "<CURP_INVENTOR>", "");
                this.FindAndReplace(wordApp, "<NOMBRE_INVENTOR>", "");
                this.FindAndReplace(wordApp, "<APELLIDOPAT_INVENTOR>", "");
                this.FindAndReplace(wordApp, "<APELLIDOMAT_INVNETOR>", "");
                this.FindAndReplace(wordApp, "<NACIONALIDAD_INVENTOR>", "");
                this.FindAndReplace(wordApp, "<TELEFONO_OFICINA>", "");
                this.FindAndReplace(wordApp, "<CORREO_OFICINA>", "");
                this.FindAndReplace(wordApp, "<CODIGOPOSTAL_INVENTOR>", "");
                this.FindAndReplace(wordApp, "<CALLE_INVENTOR>", "");
                this.FindAndReplace(wordApp, "<NUMEXTERIOR_INVENTOR>", "");
                this.FindAndReplace(wordApp, "<NUMINTERIOR_INVENTOR>", "");
                this.FindAndReplace(wordApp, "<COLONIA_INVENTOR>", "");
                this.FindAndReplace(wordApp, "<MUNICIPIO_INVENTOR>", "");
                this.FindAndReplace(wordApp, "<LOCALIDAD_INVENTOR>", "");
                this.FindAndReplace(wordApp, "<ESTADO_INVENTOR>", "");
                this.FindAndReplace(wordApp, "<PAIS_INVENTOR>", "");
                this.FindAndReplace(wordApp, "<CURP_APODERADO>", "");
                this.FindAndReplace(wordApp, "<NOMBRE_APODERADO>", "");
                this.FindAndReplace(wordApp, "<APELLIDOPAT_APODERADO>", "");
                this.FindAndReplace(wordApp, "<APELLIDOMAT_APODERADO>", "");
                this.FindAndReplace(wordApp, "<CORREO_OFICINA>", "");
                this.FindAndReplace(wordApp, "<TELEFONO_OFICINA>", "");
                this.FindAndReplace(wordApp, "<CODIGOPOSTAL_OFICINA>", "");
                this.FindAndReplace(wordApp, "<CALLE_OFICINA>", "");
                this.FindAndReplace(wordApp, "<NUMEXT_OFICINA>", "");
                this.FindAndReplace(wordApp, "<NUMINT_OFICINA>", "");
                this.FindAndReplace(wordApp, "<COLONIA_OFICINA>", "");
                this.FindAndReplace(wordApp, "<LOCALIDAD_OFICINA>", "");
                this.FindAndReplace(wordApp, "<ESTADO_OFICINA>", "");
                this.FindAndReplace(wordApp, "<PAIS_OFICINA>", "");
                this.FindAndReplace(wordApp, "<NOMBRE_AUT>", "");
                this.FindAndReplace(wordApp, "<APELLIDOPAT_AUT>", "");
                this.FindAndReplace(wordApp, "<APELLIDOMAT_AUT>", "");
                this.FindAndReplace(wordApp, "<TITULO_SOLICITUD>", "");
                this.FindAndReplace(wordApp, "<DIA_DIVUL>", "");
                this.FindAndReplace(wordApp, "<MES_DIVUL>", "");
                this.FindAndReplace(wordApp, "<ANO_DIVUL>", "");
                this.FindAndReplace(wordApp, "<EXPEDIENTE>", "");
                this.FindAndReplace(wordApp, "<FIGURA_JURIDICA>", "");
                this.FindAndReplace(wordApp, "<DIA_EXPE>", "");
                this.FindAndReplace(wordApp, "<MES_EXPE>", "");
                this.FindAndReplace(wordApp, "<ANO_EXPE>", "");
                this.FindAndReplace(wordApp, "<NUMERO_PCT>", "");
                this.FindAndReplace(wordApp, "<DIA_PCT>", "");
                this.FindAndReplace(wordApp, "<MES_PCT>", "");
                this.FindAndReplace(wordApp, "<ANO_PCT>", "");
                this.FindAndReplace(wordApp, "<PAIS_PARIS>", "");
                this.FindAndReplace(wordApp, "<DIA_PA>", "");
                this.FindAndReplace(wordApp, "<MES_PA>", "");
                this.FindAndReplace(wordApp, "<ANO_PA>", "");
                this.FindAndReplace(wordApp, "<NUMERO_PARIS>", "");
                this.FindAndReplace(wordApp, "<TELEFONO_OFICINA_FI>", "");
                this.FindAndReplace(wordApp, "<CORREO_OFICINA_FI>", "");
            }
            catch (Exception E)
            {
                String strRutaArchivo = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
                String fechalog = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                StringBuilder sb = new StringBuilder();
                sb.Append(fechalog + ":user:documentos: " + E + "\n");
                System.IO.File.AppendAllText(strRutaArchivo+ "\\casoskng\\" + "events.log", sb.ToString());
                sb.Clear();

            }
        }

        public String validafechavacia(String fecha)
        {
            String fechanueva = fecha;
            String datoabuscar = "0000";
            //fechanueva.IndexOf(datoabuscar);

            if (fechanueva.IndexOf(datoabuscar) == -1)
            {
                return fechanueva;
            }
            else
            {
                return "";
            }
        }

        public void genereaanexoprio()
        {
            try
            {
                object missing = Missing.Value;
                wordApp.Quit(ref missing, ref missing, ref missing);
                wordApp = new Word.Application();
                aDoc = null;
                String queryanex1 = "SET lc_time_names = 'es_ES'; SELECT prioridad.PrioridadId, prioridad.PrioridadNumero, " +
                "prioridad.PaisID, DATE_FORMAT(prioridad.PrioridadFecha , '%d-%M-%Y') as  PrioridadFecha,  " +
                "prioridad.TipoPrioridadId " +
                "FROM prioridad " +
                "WHERE prioridad.CasoId = " + sCasoId + "  order by prioridad.PrioridadFecha ASC ; ";

                MySqlDataReader respuestaanexoprio = condoc.getdatareader(queryanex1);
                //primer parametro es el texto, el segundo parametro es el id o valor y el tercer parametro es el mysqldatareader



                int conpriopct = 0;
                int conpriopar = 0;

                String siniPrioridadId = "", siniPrioridadNumero = "", siniPaisID = "",
                        siniPrioridadFecha = "", siniReferenciaNombre = "", siniTipoPrioridadId = "";




                object abrirDoc = rutaprioridades;


                object readOnly = false;
                object isVisible = false;

                aDoc = wordApp.Documents.Open(ref abrirDoc, ref isVisible, ref readOnly,
                                            ref missing, ref missing, ref missing,
                                            ref missing, ref missing, ref missing,
                                            ref missing, ref missing, ref missing,
                                            ref missing, ref missing, ref missing, ref missing);


                Random random = new Random();
                int randomNumber = random.Next(0, 100);
                int iContwhileprioridades = 0;
                String Remplazopar = "", Remplazopct = "";
                object fileNametres = srutacarpeta + "\\anexoprioridades_" + randomNumber + "_" + sCasoId + ".doc";
                object fileName = srutacarpeta + "\\anexoprioridades_" + randomNumber + "_" + sCasoId + ".pdf";

                while (respuestaanexoprio.Read())
                {
                    //primer parametro es el texto, el segundo parametro es el id o valor y el tercer parametro es el mysqldatareader
                    siniPrioridadId = validareader("PrioridadId", "PrioridadId", respuestaanexoprio).Text;
                    siniPrioridadNumero = validareader("PrioridadNumero", "PrioridadId", respuestaanexoprio).Text;
                    siniPaisID = validareader("PaisID", "PrioridadId", respuestaanexoprio).Text;
                    siniPrioridadFecha = validareader("PrioridadFecha", "PrioridadId", respuestaanexoprio).Text.Replace("-", " de ");
                    siniTipoPrioridadId = validareader("TipoPrioridadId", "PrioridadId", respuestaanexoprio).Text;


                    String kwery47 = "SELECT * FROM  pais where PaisId  = " + siniPaisID + ";";
                    MySqlDataReader respuestastring147 = condoc.getdatareader(kwery47);

                    while (respuestastring147.Read())
                    {
                        siniPaisID = validareader("PaisNombre", "PaisId", respuestastring147).Text;
                    }
                    respuestastring147.Close();

                    if (!siniTipoPrioridadId.Equals("2"))
                    {


                        if (conpriopct > 0)
                        {

                            Remplazopct = siniPaisID + ", fecha  " + siniPrioridadFecha + ", con número  de solicitud " + siniPrioridadNumero + "\n\r";
                            switch (iContwhileprioridades)
                            {//reemplazamos hasta 8 variables de PCT
                                case 1:
                                    {
                                        this.FindAndReplace(wordApp, "<PRIORIDADESPCT1>", Remplazopct);
                                    } break;
                                case 2:
                                    {
                                        this.FindAndReplace(wordApp, "<PRIORIDADESPCT2>", Remplazopct);
                                    } break;
                                case 3:
                                    {
                                        this.FindAndReplace(wordApp, "<PRIORIDADESPCT3>", Remplazopct);
                                    } break;
                                case 4:
                                    {
                                        this.FindAndReplace(wordApp, "<PRIORIDADESPCT4>", Remplazopct);
                                    } break;
                                case 5:
                                    {
                                        this.FindAndReplace(wordApp, "<PRIORIDADESPCT5>", Remplazopct);
                                    } break;

                            }
                        }

                        else
                        {

                            this.FindAndReplace(wordApp, "<PRIORIDADESPCT>", " ");
                        }
                        conpriopct++;
                    }
                    else
                    {
                        //es tipo paris  
                        if (conpriopar > 0)
                        {
                            Remplazopar = siniPaisID + ",  fecha  " + siniPrioridadFecha + ", con número  de solicitud " + siniPrioridadNumero + "\n\r";
                            switch (iContwhileprioridades)
                            {//reemplazamos hasta 8 variables de paris
                                case 1:
                                    {
                                        this.FindAndReplace(wordApp, "<PRIORIDADESPAR1>", Remplazopar);
                                    } break;
                                case 2:
                                    {
                                        this.FindAndReplace(wordApp, "<PRIORIDADESPAR2>", Remplazopar);
                                    } break;
                                case 3:
                                    {
                                        this.FindAndReplace(wordApp, "<PRIORIDADESPAR3>", Remplazopar);
                                    } break;
                                case 4:
                                    {
                                        this.FindAndReplace(wordApp, "<PRIORIDADESPAR4>", Remplazopar);
                                    } break;
                                case 5:
                                    {
                                        this.FindAndReplace(wordApp, "<PRIORIDADESPAR5>", Remplazopar);
                                    } break;
                                case 6:
                                    {
                                        this.FindAndReplace(wordApp, "<PRIORIDADESPAR6>", Remplazopar);
                                    } break;
                                case 7:
                                    {
                                        this.FindAndReplace(wordApp, "<PRIORIDADESPAR7>", Remplazopar);

                                    } break;
                                case 8:
                                    {
                                        this.FindAndReplace(wordApp, "<PRIORIDADESPAR8>", Remplazopar);

                                    } break;
                                case 9:
                                    {
                                        this.FindAndReplace(wordApp, "<PRIORIDADESPAR9>", Remplazopar);

                                    } break;
                                case 10:
                                    {
                                        this.FindAndReplace(wordApp, "<PRIORIDADESPAR10>", Remplazopar);

                                    } break;
                                case 11:
                                    {
                                        this.FindAndReplace(wordApp, "<PRIORIDADESPAR11>", Remplazopar);

                                    } break;
                                case 12:
                                    {
                                        this.FindAndReplace(wordApp, "<PRIORIDADESPAR12>", Remplazopar);
                                    } break;
                                case 13:
                                    {
                                        this.FindAndReplace(wordApp, "<PRIORIDADESPAR13>", Remplazopar);
                                    } break;
                                case 14:
                                    {
                                        this.FindAndReplace(wordApp, "<PRIORIDADESPAR14>", Remplazopar);

                                    } break;
                                case 15:
                                    {
                                        this.FindAndReplace(wordApp, "<PRIORIDADESPAR15>", Remplazopar);

                                    } break;
                                case 16:
                                    {
                                        this.FindAndReplace(wordApp, "<PRIORIDADESPAR16>", Remplazopar);

                                    } break;
                                case 17:
                                    {
                                        this.FindAndReplace(wordApp, "<PRIORIDADESPAR17>", Remplazopar);

                                    } break;
                                case 18:
                                    {
                                        this.FindAndReplace(wordApp, "<PRIORIDADESPAR18>", Remplazopar);

                                    } break;
                            }
                        }
                        conpriopar++;
                    }
                    //wordApp.Selection.TypeText(Remplazopar); ojo 
                    iContwhileprioridades++;

                }
                respuestaanexoprio.Close();


                for (int i = 0; i < 20; i++)
                {
                    this.FindAndReplace(wordApp, "<PRIORIDADESPCT" + (i + 1) + ">", "");
                    this.FindAndReplace(wordApp, "<PRIORIDADESPAR" + (i + 1) + ">", "");
                }



                String queryanexref = "SELECT * FROM referencia where CasoId = " + sCasoId + ";";

                MySqlDataReader respuestaanexref = condoc.getdatareader(queryanexref);


                while (respuestaanexref.Read())
                {

                    siniReferenciaNombre = validareader("ReferenciaNombre", "ReferenciaId", respuestaanexref).Text;
                }
                respuestaanexref.Close();


                this.FindAndReplace(wordApp, "«ReferenciaDespacho»", siniReferenciaNombre);
                Object fileformat2 = Word.WdSaveFormat.wdFormatPDF;
                Object SaveChange2 = Word.WdSaveOptions.wdDoNotSaveChanges;
                Object OrianalForamt2 = Word.WdOriginalFormat.wdOriginalDocumentFormat;

                respuestaanexref.Close();
                respuestaanexoprio.Close();

                aDoc.Activate();
                aDoc.SaveAs2(ref fileNametres,
                                ref missing2, ref missing2, ref missing2, ref missing2, ref missing2,
                                ref missing2, ref missing2, ref missing2, ref missing2, ref missing2,
                                ref missing2, ref missing2, ref missing2, ref missing2, ref missing2);


                aDoc.SaveAs(ref fileName, ref fileformat2, ref missing2, ref missing2, ref missing2, ref missing2,
                            ref missing2, ref missing2, ref missing2, ref missing2, ref missing2, ref missing2, ref missing2, ref missing2, ref missing2, ref missing2);
                aDoc.Saved = true;
                //wordApp.Documents.Close(ref fileNametres, ref missing2, ref missing2);
                wordApp.Quit(ref missing2, ref missing2, ref missing2);
                //wordApp = null;

                Process.Start(fileNametres.ToString());
                Process.Start(fileName.ToString());

            }
            catch (Exception E)
            {
                String strRutaArchivo = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
                String fechalog = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                StringBuilder sb = new StringBuilder();
                sb.Append(fechalog + ":user:documentos: " + E + "\n");
                System.IO.File.AppendAllText(strRutaArchivo+"\\casosking\\" + "events.log", sb.ToString());
                sb.Clear();

            }


            
        }




    }
}
