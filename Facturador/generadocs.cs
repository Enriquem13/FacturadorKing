
using Microsoft.Office.Interop.Word;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
    class generadocs
    {
        public String sCasoId;
        public String sTipoformato;
        object missing = Missing.Value;
        Word.Application wordApp;
        Word.Document aDoc = null;
        object missing2 = Missing.Value;
        conect condoc;
        public String srutacarpeta;
        public void generadocss(String CasoId,  String valueformato)
        {
            try
            {
                condoc = new conect();
                wordApp = new Word.Application();
                aDoc = null;
                sCasoId = CasoId;
                wordApp.Quit(ref missing, ref missing, ref missing);
                wordApp = new Word.Application();
                aDoc = null;
                sTipoformato = valueformato;
                // Create a list of accounts.
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
            }catch(Exception E){
                new filelog("generadocs", E.Message);
            }
        }

        static void DisplayInExcel(IEnumerable<Account> accounts)
        {
            var excelApp = new Excel.Application();
            // Make the object visible.
            excelApp.Visible = true;

            // Create a new, empty workbook and add it to the collection returned 
            // by property Workbooks. The new workbook becomes the active workbook.
            // Add has an optional parameter for specifying a praticular template. 
            // Because no argument is sent in this example, Add creates a new workbook. 
            excelApp.Workbooks.Add();

            // This example uses a single workSheet. 
            Excel._Worksheet workSheet = excelApp.ActiveSheet;

            // Earlier versions of C# require explicit casting.
            //Excel._Worksheet workSheet = (Excel.Worksheet)excelApp.ActiveSheet;

            // Establish column headings in cells A1 and B1.
            workSheet.Cells[1, "A"] = "ID Number";
            workSheet.Cells[1, "B"] = "Current Balance";

            var row = 1;
            foreach (var acct in accounts)
            {
                row++;
                workSheet.Cells[row, "A"] = acct.ID;
                workSheet.Cells[row, "B"] = acct.Balance;
            }

            workSheet.Columns[1].AutoFit();
            workSheet.Columns[2].AutoFit();

            // Call to AutoFormat in Visual C#. This statement replaces the 
            // two calls to AutoFit.
            workSheet.Range["A1", "B3"].AutoFormat(
                Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic2);

            // Put the spreadsheet contents on the clipboard. The Copy method has one
            // optional parameter for specifying a destination. Because no argument  
            // is sent, the destination is the Clipboard.
            workSheet.Range["A1:B3"].Copy();
        }

        private void CreateIconInWordDoc()
        {
            try{
                conect condoc = new conect();
                switch (sTipoformato)
                {
                    case "1":
                        //consulta de patentes
                    
                        String querydoc = "SELECT " +
                                            "casointeresado.InteresadoId, " +
                                            "casointeresado.TipoRelacionId, " +
                                            "casointeresado.CasoInteresadoSecuencia, " +
                                            "caso_patente.TipoSolicitudId, " +
                                            "caso_patente.SubTipoSolicitudId, " +
                                            "caso_patente.CasoNumero, " +
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
                                            "datosoficina.OficinaEstado, " +
                                            "datosoficina.OficinaMunicipio, " +
                                            "DATE_FORMAT(caso_patente.CasoFechaDivulgacionPrevia,  '%d-%m-%Y') AS CasoFechaDivulgacionPrevia,    " +
                                            "DATE_FORMAT(caso_patente.CasoFechaRecepcion,  '%d-%m-%Y') AS CasoFechaRecepcion,    " +                              
                                            "caso_patente.CasoTituloespanol " +
                                            "from caso_patente, casointeresado, interesado, datosoficina, direccion " +
                                            "WHERE caso_patente.CasoId = " + sCasoId +
                                            " AND  caso_patente.CasoId = casointeresado.CasoId " +
                                            "AND casointeresado.InteresadoId = interesado.InteresadoID " +
                                            "AND direccion.InteresadoId = interesado.InteresadoID group by interesado.InteresadoID order by CasoInteresadoSecuencia ASC;";
                        MySqlDataReader respuestastrindoc = condoc.getdatareader(querydoc);

                        String siniInteresadoId = "", siniTipoRelacionId = "", siniInteresadoTipoPersonaSAT = "", siniInteresadoCurp = "", siniInteresadoNombre = "", sinifechapctdia = "",
                               siniInteresadoApPaterno = "", siniInteresadoApMaterno = "", sininacionalidadid = "", siniOficinaTelefono = "", siniOficinaCorreo = "", sinifechapctmes = "",
                               siniDireccionCP = "", siniDireccionCalle = "", siniDireccionNumExt = "", siniDireccionNumInt = "", siniDireccionColonia = "", sinimoralrfc = "", sinifechapctano = "",
                               siniDireccionPoblacion = "", siniDireccionEstado = "", sinipaisdireccioninteresado = "", sinicurp_fisica = "", sininombrefisica = "", sinifechacompletapct = "",
                               siniapellidopat_fisica = "", siniapellidomat_fisica = "", sininacionalidad = "", sinitelefono_fisica = "", sinicorreofisica = "", siniCasoFechaDivulgacionPrevia = "",
                               sininombremoral = "", sininacionalidadmoral = "", sinitelefonomoral = "", sinicorreomoral = "", sinicodigopostal_titular = "", sinifechaprioridaddia = "",
                               sinicalle_titular = "", sininumeroexterior_titular = "", sininumerointerior_titular = "", sinicolonia_titular = "", sinimunicipio_titular = "", sinifechaprioridadmes = "",
                               sinientidadfederativa_titular = "", sinilocalidad_titular = "", sinipais_titular = "", siniCasoFechaDivulgacionPreviadia = "", siniCasoFechaDivulgacionPreviames = "",
                                sininombre_inventor = "", sinicurp_inventor = "", siniapellidopat_inventor = "", siniapellidomat_inv = "", sininacionalidad_inventor = "",
                               sinitelefonoinventor = "", sinicorreo_inventor = "", sinicodigopostal_inventor = "", sinicalle_inventor = "", sininumerointerior_inventor = "", sinifechaprioridadano = "",
                               sininumeroexterior_inventor = "", sinicolonia_inventor = "", sinimunicipio_inventor = "", sinientidadfederativa_inventor = "", sinilocalidad_inventor = "", sinix1 = "",
                               sinipais_inventor = "", sininumerosolicitudpct = "", sinitituloinvencion = "", siniInteresadoRFC = "", siniCasoFechaDivulgacionPreviaano = "", sinix2 = "", sinix3 = "",
                               siniprioridadpais = "", sinifechaprioridad = "", siniprioridadnumero = "", siniOficinaCalle = "", siniOficinaNumExt = "", siniTipoPrioridadId = "", sinix4 = "", sinix5 = "",
                               siniOficinaNumInt = "", siniOficinaColonia = "", siniOficinaEstado = "", siniOficinaPaisId = "", siniOficinaCP = "", siniOficinaMunicipio = "", sinix6 = "", sinix7 = "",
                               siniCasoFechaPresentacion = "", siniCasoFechaPresentaciondia = "", siniCasoFechaPresentacionmes = "", siniCasoFechaPresentacionano = "", siniCasoInteresadoSecuencia = "",
                               siniPaisIDprioridad = "", siniPrioridadFecha = "", sinix13 = "", siniTipoSolicitudId = "", siniSubTipoSolicitudId = "", sinix8 = "", sinix9 = "", sinix10 = "", sinix11 = "", sinix12 = "",
                               sinix14 = "", sinix15 = "", sinix16 = "", sinix17 = "", sinix18 = "", sinix19 = "", sinix20 = "", sinix21 = "", sinix22 = "", sinix23 = "", sinix24 = "", sinix25 = "", sinix26 = "",
                                sinix28 = "", sinix29 = "",  sinicasonumero ="", siniPrioridadNumero = "";

                        //primer parametro es el texto, el segundo parametro es el id o valor y el tercer parametro es el mysqldatareader
                        int continven = 0;
                        int Icontadorg = 0;//contador para conocer cuantos interesados y titulares son en total (resultado de la consulta a interesados)
                        String[] aNombre = new String[25];
                        String[] aApellidopa = new String[25];
                        String[] aApellidoma = new String[25];
                        String[] aCurp = new String[25];
                        String[] aNacionalidad = new String[25];
                        String[] aTelefono = new String[25];
                        String[] aCorreo = new String[25];
                        String[] aCP = new String[25];
                        String[] aCalle = new String[25];
                        String[] aNumeroexterior = new String[25];
                        String[] aNumint = new String[25];
                        String[] aColonia = new String[25];
                        String[] aMunicipio = new String[25];
                        String[] aEstado = new String[25];
                        String[] aPais = new String[25];
                        String[] aLocalidad = new String[25];
                        String[] aRFC = new String[25];



                    
                        while (respuestastrindoc.Read())//EMPIEZA EL WHILE PARA CONSULTAR INTERESADO (TITULARES E INVENTORES)
                        {
                            siniInteresadoId = validareader("InteresadoId", "InteresadoId", respuestastrindoc).Text;
                            siniTipoRelacionId = validareader("TipoRelacionId", "InteresadoId", respuestastrindoc).Text;
                            siniInteresadoTipoPersonaSAT = validareader("InteresadoTipoPersonaSAT", "InteresadoId", respuestastrindoc).Text;
                            siniInteresadoCurp = validareader("InteresadoCurp", "InteresadoId", respuestastrindoc).Text;
                            siniInteresadoRFC = validareader("InteresadoRFC", "InteresadoId", respuestastrindoc).Text;
                            siniInteresadoNombre = validareader("InteresadoNombre", "InteresadoId", respuestastrindoc).Text;
                            siniInteresadoApPaterno = validareader("InteresadoApPaterno", "InteresadoId", respuestastrindoc).Text;
                            siniInteresadoApMaterno = validareader("InteresadoApMaterno", "InteresadoId", respuestastrindoc).Text;
                            sininacionalidadid = validareader("nacionalidadid", "InteresadoId", respuestastrindoc).Text;
                            siniOficinaTelefono = validareader("OficinaTelefono", "InteresadoId", respuestastrindoc).Text;
                            siniOficinaCorreo = validareader("OficinaCorreo", "InteresadoId", respuestastrindoc).Text;
                            siniDireccionCP = validareader("DireccionCP", "InteresadoId", respuestastrindoc).Text;
                            siniDireccionCalle = validareader("DireccionCalle", "InteresadoId", respuestastrindoc).Text;
                            siniDireccionNumExt = validareader("DireccionNumExt", "InteresadoId", respuestastrindoc).Text;
                            siniDireccionNumInt = validareader("DireccionNumInt", "InteresadoId", respuestastrindoc).Text;
                            siniDireccionColonia = validareader("DireccionColonia", "InteresadoId", respuestastrindoc).Text;
                            siniDireccionPoblacion = validareader("DireccionPoblacion", "InteresadoId", respuestastrindoc).Text;
                            siniDireccionEstado = validareader("DireccionEstado", "InteresadoId", respuestastrindoc).Text;
                            sinipaisdireccioninteresado = validareader("paisdireccioninteresado", "InteresadoId", respuestastrindoc).Text;
                            siniOficinaCalle = validareader("OficinaCalle", "InteresadoId", respuestastrindoc).Text;
                            siniOficinaNumExt = validareader("OficinaNumExt", "InteresadoId", respuestastrindoc).Text;
                            siniOficinaNumInt = validareader("OficinaNumInt", "InteresadoId", respuestastrindoc).Text;
                            siniOficinaColonia = validareader("OficinaColonia", "InteresadoId", respuestastrindoc).Text;
                            siniOficinaEstado = validareader("OficinaEstado", "InteresadoId", respuestastrindoc).Text;
                            siniOficinaMunicipio = validareader("OficinaMunicipio", "InteresadoId", respuestastrindoc).Text;
                            siniOficinaPaisId = validareader("OficinaPaisId", "InteresadoId", respuestastrindoc).Text;
                            siniOficinaCP = validareader("OficinaCP", "InteresadoId", respuestastrindoc).Text;
                            siniCasoFechaPresentacion = validareader("CasoFechaRecepcion", "InteresadoId", respuestastrindoc).Text;
                            siniCasoFechaDivulgacionPrevia = validareader("CasoFechaDivulgacionPrevia", "InteresadoId", respuestastrindoc).Text;
                            sinitituloinvencion = validareader("CasoTituloespanol", "InteresadoId", respuestastrindoc).Text;
                            siniTipoSolicitudId = validareader("TipoSolicitudId", "InteresadoId", respuestastrindoc).Text;
                            siniSubTipoSolicitudId = validareader("SubTipoSolicitudId", "InteresadoId", respuestastrindoc).Text;
                            siniCasoInteresadoSecuencia = validareader("CasoInteresadoSecuencia", "InteresadoId", respuestastrindoc).Text;
                            sinicasonumero = validareader("CasoNumero", "InteresadoId", respuestastrindoc).Text;


                            if (sinitituloinvencion.Length > 14)
                            {
                                String nombrecarpeta = sinitituloinvencion.Substring(0, 15);
                                srutacarpeta = @"C:\facturador\DocumentosCasosPrueba\" + nombrecarpeta + "_" + sinicasonumero;
                                System.IO.Directory.CreateDirectory(srutacarpeta);
                            }
                            else
                            {
                                String nombrecarpeta = sinitituloinvencion;
                                srutacarpeta = @"C:\facturador\DocumentosCasosPrueba\" + nombrecarpeta + "_" + sinicasonumero;
                                System.IO.Directory.CreateDirectory(srutacarpeta);
                            }

                            //agregamos los valores a los arrays para poder setearlos en los anexos correspondientes
                            if (Icontadorg>1)
                            {
                                aNombre[Icontadorg - 2] = siniInteresadoNombre;
                                aApellidopa[Icontadorg - 2] = siniInteresadoApPaterno;
                                aApellidoma[Icontadorg - 2] = siniInteresadoApMaterno;
                                aCurp[Icontadorg - 2] = siniInteresadoCurp;
                                aNacionalidad[Icontadorg - 2] = sininacionalidadid;
                                aTelefono[Icontadorg - 2] = siniOficinaTelefono;
                                aCorreo[Icontadorg - 2] = siniOficinaCorreo;
                                aCP[Icontadorg - 2] = siniDireccionCP;
                                aCalle[Icontadorg - 2] = siniDireccionCalle;
                                aNumeroexterior[Icontadorg - 2] = siniDireccionNumExt;
                                aNumint[Icontadorg - 2] = siniDireccionNumInt;
                                aColonia[Icontadorg - 2] = siniDireccionColonia;
                                aMunicipio[Icontadorg - 2] = siniDireccionPoblacion;
                                aEstado[Icontadorg - 2] = siniDireccionEstado;
                                aPais[Icontadorg - 2] = sinipaisdireccioninteresado;
                                aLocalidad[Icontadorg - 2] = siniDireccionPoblacion;
                                aRFC[Icontadorg - 2] = siniInteresadoRFC;
                            }


                            String kwery46 = "SELECT * FROM  pais where PaisId  = " + siniOficinaPaisId + ";";
                            MySqlDataReader respuestastring146 = condoc.getdatareader(kwery46);
                            while (respuestastring146.Read())
                            {
                                siniOficinaPaisId = validareader("PaisNombre", "PaisId", respuestastring146).Text;
                            }



                            if (!siniCasoFechaPresentacion.Equals(""))
                            {
                                siniCasoFechaPresentaciondia = siniCasoFechaPresentacion.Substring(0, 2);
                                siniCasoFechaPresentacionmes = siniCasoFechaPresentacion.Substring(3, 2);
                                siniCasoFechaPresentacionano = siniCasoFechaPresentacion.Substring(6, 4);
                            }

                            if (!siniCasoFechaDivulgacionPrevia.Equals(""))
                            {
                                siniCasoFechaDivulgacionPreviadia = siniCasoFechaDivulgacionPrevia.Substring(0, 2);
                                siniCasoFechaDivulgacionPreviames = siniCasoFechaDivulgacionPrevia.Substring(3, 2);
                                siniCasoFechaDivulgacionPreviaano = siniCasoFechaDivulgacionPrevia.Substring(6, 4);
                            }




                            switch (siniTipoSolicitudId)
                            {
                                case "1":
                                    sinix1 = "X";
                                    break;
                                case "2":
                                    sinix2 = "X";
                                    break;
                                case "3":
                                    sinix3 = "X";
                                    break;
                            }

                            switch (siniSubTipoSolicitudId)
                            {
                                case "3":
                                    sinix4 = "X";
                                    break;
                                case "4":
                                    sinix5 = "X";
                                    break;
                            }


                            if (!sininacionalidadid.Equals("148"))
                            {
                                //ES EXTRANGERO
                                if (!siniTipoRelacionId.Equals("1"))
                                {
                                
                                    //ES INVENTOR
                                    if (continven == 0)
                                    {
                                        //es el primer inventor

                                        sininombre_inventor = siniInteresadoNombre;
                                        siniapellidopat_inventor = siniInteresadoApPaterno;
                                        siniapellidomat_inv = siniInteresadoApMaterno;
                                        sininacionalidad_inventor = sininacionalidadid;
                                        sinitelefonoinventor = siniOficinaTelefono;
                                        sinicorreo_inventor = siniOficinaCorreo;
                                        sinicodigopostal_inventor = siniDireccionCP;
                                        sinicalle_inventor = siniDireccionCalle;
                                        sininumeroexterior_inventor = siniDireccionNumExt;
                                        sininumerointerior_inventor = siniDireccionNumInt;
                                        sinicolonia_inventor = "";
                                        sinimunicipio_inventor = "";
                                        sinientidadfederativa_inventor = siniDireccionEstado;
                                        sinipais_inventor = sinipaisdireccioninteresado;
                                        sinilocalidad_inventor = siniDireccionPoblacion;


                                        String kwery44 = "SELECT * FROM  pais where PaisId  = " + sininacionalidad_inventor + ";";
                                        MySqlDataReader respuestastring144 = condoc.getdatareader(kwery44);

                                        while (respuestastring144.Read())
                                        {
                                            sininacionalidad_inventor = validareader("PaisNacionalidad", "PaisId", respuestastring144).Text;
                                        }

                                        String kwery45 = "SELECT * FROM  pais where PaisId  = " + sinipais_inventor + ";";
                                        MySqlDataReader respuestastring145 = condoc.getdatareader(kwery45);
                                        while (respuestastring145.Read())
                                        {
                                            sinipais_inventor = validareader("PaisNombre", "PaisId", respuestastring145).Text;
                                        }

                                    }
                                    continven++;


                                }
                                else
                                {
                                    //ES UN TITULAR
                                    if (!siniInteresadoTipoPersonaSAT.Equals("ME"))
                                    {
                                        //ES PERSONA FISICA EXTRANJERA

                                        sininombrefisica = siniInteresadoNombre;
                                        siniapellidopat_fisica = siniInteresadoApPaterno;
                                        siniapellidomat_fisica = siniInteresadoApMaterno;
                                        sininacionalidad = sininacionalidadid;
                                        sinitelefono_fisica = siniOficinaTelefono;
                                        sinicorreofisica = siniOficinaCorreo;
                                        sinitelefonomoral = "";
                                        sinicorreomoral = "";
                                        sinicodigopostal_titular = siniDireccionCP;
                                        sinicalle_titular = siniDireccionCalle;
                                        sininumeroexterior_titular = siniDireccionNumExt;
                                        sininumerointerior_titular = siniDireccionNumInt;
                                        sinicolonia_titular = "";
                                        sinimunicipio_inventor = "";
                                        sinientidadfederativa_titular = siniDireccionEstado;
                                        sinilocalidad_titular = siniDireccionPoblacion;

                                        String kwery44 = "SELECT * FROM  pais where PaisId  = " + sininacionalidad + ";";
                                        MySqlDataReader respuestastring144 = condoc.getdatareader(kwery44);

                                        while (respuestastring144.Read())
                                        {
                                            sininacionalidad = validareader("PaisNacionalidad", "PaisId", respuestastring144).Text;
                                        }

                                        //FIN ES PERSONA FISICA EXTRANJERA
                                    }
                                    else
                                    {
                                        //ES PERSONA MORAL EXTRANJERA
                                        sininombremoral = siniInteresadoNombre;
                                        sininacionalidadmoral = sininacionalidadid;
                                        sinitelefonomoral = siniOficinaTelefono;
                                        sinicorreomoral = siniOficinaCorreo;
                                        sinitelefono_fisica = "";
                                        sinicorreofisica = "";
                                        sinicodigopostal_titular = siniDireccionCP;
                                        sinicalle_titular = siniDireccionCalle;
                                        sininumeroexterior_titular = siniDireccionNumExt;
                                        sininumerointerior_titular = siniDireccionNumInt;
                                        sinicolonia_titular = "";
                                        sinimunicipio_inventor = "";
                                        sinientidadfederativa_titular = siniDireccionEstado;
                                        sinilocalidad_titular = siniDireccionPoblacion ;


                                        String kwery44 = "SELECT * FROM  pais where PaisId  = " + sininacionalidadmoral + ";";
                                        MySqlDataReader respuestastring144 = condoc.getdatareader(kwery44);

                                        while (respuestastring144.Read())
                                        {
                                            sininacionalidadmoral = validareader("PaisNacionalidad", "PaisId", respuestastring144).Text;
                                        }

                                        //FIN ES PERSONA MORAL EXTRANJERA
                                    }

                                    //ES UN TITULAR
                                }

                            }
                            else
                            {
                                //ES MEXICANO
                                if (!siniTipoRelacionId.Equals("1"))
                                {
                                
                                    //ES INVENTOR
                                    if (continven == 0)
                                    {
                                        //ES EL PRIMER INVENTOR
                                        sinicurp_inventor = siniInteresadoCurp;
                                        sininombre_inventor = siniInteresadoNombre;
                                        siniapellidopat_inventor = siniInteresadoApPaterno;
                                        siniapellidomat_inv = siniInteresadoApMaterno;
                                        sininacionalidad_inventor = sininacionalidadid;
                                        sinitelefonoinventor = siniOficinaTelefono;
                                        sinicorreo_inventor = siniOficinaCorreo;
                                        sinicodigopostal_inventor = siniDireccionCP;
                                        sinicalle_inventor = siniDireccionCalle;
                                        sininumeroexterior_inventor = siniDireccionNumExt;
                                        sininumerointerior_inventor = siniDireccionNumInt;
                                        sinicolonia_inventor = siniDireccionColonia;
                                        sinimunicipio_inventor = siniDireccionPoblacion;
                                        sinientidadfederativa_inventor = siniDireccionEstado;
                                        sinipais_inventor = sinipaisdireccioninteresado;
                                        sinilocalidad_inventor = "";


                                        String kwery44 = "SELECT * FROM  pais where PaisId  = " + sininacionalidad_inventor + ";";
                                        MySqlDataReader respuestastring144 = condoc.getdatareader(kwery44);

                                        while (respuestastring144.Read())
                                        {
                                            sininacionalidad_inventor = validareader("PaisNacionalidad", "PaisId", respuestastring144).Text;
                                        }

                                        String kwery45 = "SELECT * FROM  pais where PaisId  = " + sinipais_inventor + ";";
                                        MySqlDataReader respuestastring145 = condoc.getdatareader(kwery45);
                                        while (respuestastring145.Read())
                                        {
                                            sinipais_inventor = validareader("PaisNombre", "PaisId", respuestastring145).Text;
                                        }
                                    }
                                    continven++;

                                }
                                else
                                {
                                    //ES TITULAR
                                    if (!siniInteresadoTipoPersonaSAT.Equals("MN"))
                                    {
                                        sininombrefisica = siniInteresadoNombre;
                                        siniapellidopat_fisica = siniInteresadoApPaterno;
                                        siniapellidomat_fisica = siniInteresadoApMaterno;
                                        sininacionalidad = sininacionalidadid;
                                        sinitelefono_fisica = siniOficinaTelefono;
                                        sinicorreofisica = siniOficinaCorreo;
                                        sinitelefonomoral = "";
                                        sinicorreomoral = "";
                                        sinicodigopostal_titular = siniDireccionCP;
                                        sinicalle_titular = siniDireccionCalle;
                                        sininumeroexterior_titular = siniDireccionNumExt;
                                        sininumerointerior_titular = siniDireccionNumInt;
                                        sinicolonia_titular = siniDireccionColonia;
                                        sinimunicipio_inventor = siniDireccionPoblacion;
                                        sinientidadfederativa_titular = siniDireccionEstado;
                                        sinilocalidad_titular = "";

                                        String kwery44 = "SELECT * FROM  pais where PaisId  = " + sininacionalidad + ";";
                                        MySqlDataReader respuestastring144 = condoc.getdatareader(kwery44);

                                        while (respuestastring144.Read())
                                        {
                                            sininacionalidad = validareader("PaisNacionalidad", "PaisId", respuestastring144).Text;
                                        }
                                        //ES PERSONA FISICA NACIONAL
                                    }
                                    else
                                    {
                                        //ES PERSONA MORAL NACIONAL

                                        sininombremoral = siniInteresadoNombre;
                                        sinimoralrfc = siniInteresadoRFC;
                                        sininacionalidadmoral = sininacionalidadid;
                                        sinitelefonomoral = siniOficinaTelefono;
                                        sinicorreomoral = siniOficinaCorreo;
                                        sinitelefono_fisica = "";
                                        sinicorreofisica = "";
                                        sinicodigopostal_titular = siniDireccionCP;
                                        sinicalle_titular = siniDireccionCalle;
                                        sininumeroexterior_titular = siniDireccionNumExt;
                                        sininumerointerior_titular = siniDireccionNumInt;
                                        sinicolonia_titular = siniDireccionColonia;
                                        sinimunicipio_inventor = siniDireccionPoblacion;
                                        sinientidadfederativa_titular = siniDireccionEstado;
                                        sinilocalidad_titular = "";


                                        String kwery44 = "SELECT * FROM  pais where PaisId  = " + sininacionalidadmoral + ";";
                                        MySqlDataReader respuestastring144 = condoc.getdatareader(kwery44);

                                        while (respuestastring144.Read())
                                        {
                                            sininacionalidadmoral = validareader("PaisNacionalidad", "PaisId", respuestastring144).Text;
                                        }
                                    }
                                }
                            }


                            Icontadorg++;//aumentamos en uno el contdor para saber el numero total de interesados y o titulares

                        }//FIN DELA CONSULTA DE INTERESADO Y/O PRIORIDADES

                        if (Icontadorg>2)//si los tituares o interesados con mayores a dos(2) debemos generar un anexo titulares
                        {//generamos el anexo, como ya sabemos cuál es el número de interesados y o titulares generamos el número de anexos necesarios sean par o non
                            int iNumerodeanexo = Icontadorg / 2;
                            int iNumerdearchivos = 0;

                            if ((Icontadorg % 2) == 0)
                            {
                                iNumerdearchivos = iNumerodeanexo - 1;
                            }
                            else
                            {
                                iNumerdearchivos = iNumerodeanexo;
                            }

                            int contadorfor2 = 0;
                            for (int z = 0; z < iNumerdearchivos; z++)
                            {

                                object missing3 = Missing.Value;
                                wordApp.Quit(ref missing3, ref missing3, ref missing3);
                                wordApp = new Word.Application();
                                aDoc = null;
                                int contadoranexo2 = 0;
                                object missing = Missing.Value;
                                wordApp.Quit(ref missing, ref missing, ref missing);
                                wordApp = new Word.Application();
                                aDoc = null;


                                object abrirDoc2 = "C:\\facturador\\Interesados.doc";
                                object missing2 = Missing.Value;

                                object readOnly1 = false; //default
                                object isVisible1 = true;

                                aDoc = wordApp.Documents.Open(ref abrirDoc2, ref isVisible1, ref readOnly1,
                                        ref missing2, ref missing2, ref missing2,
                                        ref missing2, ref missing2, ref missing2,
                                        ref missing2, ref missing2, ref missing2,
                                        ref missing2, ref missing2, ref missing2, ref missing2);

                                Random random2 = new Random();
                                int randomNumber2 = random2.Next(0, 100);
                                object fileNametres1 = srutacarpeta+"\\Interesados_" + randomNumber2 + "_" + sCasoId + ".doc";
                                object fileName1 = srutacarpeta + "\\Interesados_" + +randomNumber2 + "_" + sCasoId + ".pdf";

                                for (int x = contadorfor2; x < contadorfor2 + 2; x++)
                                {

                                    if (x >= Icontadorg - 2)
                                    {
                                        Console.Write("se termina el ciclo");
                                        limpiardocumentoanexointeresados();
                                    }
                                    else
                                    {
                                        siniInteresadoNombre = aNombre[x];
                                        siniInteresadoApPaterno = aApellidopa[x];
                                        siniInteresadoApMaterno = aApellidoma[x];
                                        siniInteresadoCurp = aCurp[x];
                                        sininacionalidadid = aNacionalidad[x]; /// 
                                        siniOficinaTelefono = aTelefono[x];
                                        siniOficinaCorreo = aCorreo[x];
                                        siniDireccionCP = aCP[x];
                                        siniDireccionCalle = aCalle[x];
                                        siniDireccionNumExt = aNumeroexterior[x];
                                        siniDireccionNumInt = aNumint[x];
                                        siniDireccionColonia = aColonia[x];
                                        siniDireccionPoblacion = aMunicipio[x];
                                        siniDireccionEstado = aEstado[x];
                                        sinipaisdireccioninteresado = aPais[x]; // aqui
                                        siniDireccionPoblacion = aLocalidad[x];
                                        siniInteresadoRFC = aRFC[x];
                                        string sininacionalidad22 = "";

                                        String kwery47 = "SELECT * FROM  pais where PaisId  = " + sininacionalidadid + ";";
                                        MySqlDataReader respuestastring147 = condoc.getdatareader(kwery47);
                                        if (respuestastring147 == null) {
                                            sininacionalidad22 = "";
                                        }
                                        else
                                        {
                                            while (respuestastring147.Read())
                                            {
                                                sininacionalidad22 = validareader("PaisNacionalidad", "PaisId", respuestastring147).Text;
                                            }
                                        }


                                        String kwery4 = "SELECT * FROM  pais where PaisId  = " + sinipaisdireccioninteresado + ";";
                                        MySqlDataReader respuestastring4 = condoc.getdatareader(kwery4);
                                        if (respuestastring4 == null)
                                        {
                                            sinipaisdireccioninteresado = "";
                                        }
                                        else
                                        {
                                            while (respuestastring4.Read())
                                            {
                                                sinipaisdireccioninteresado = validareader("PaisNombre", "PaisId", respuestastring4).Text; ;
                                            }
                                        }



                                        if (!sininacionalidad.Equals("148"))
                                        {
                                            //  es extrangero

                                            if (!siniTipoRelacionId.Equals("1"))
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

                                                if (!siniInteresadoTipoPersonaSAT.Equals("ME"))
                                                {
                                                    //FISICA EXTRANGERA
                                                    this.FindAndReplace(wordApp, "<CURP_FISICA" + (contadoranexo2 + 1) + ">", "");
                                                    this.FindAndReplace(wordApp, "<NOMBRE_FISICA" + (contadoranexo2 + 1) + ">", siniInteresadoNombre);
                                                    this.FindAndReplace(wordApp, "<APELLIDO_PATFISICA" + (contadoranexo2 + 1) + ">", siniInteresadoApPaterno);
                                                    this.FindAndReplace(wordApp, "<APELLIDO_MATFISICA" + (contadoranexo2 + 1) + ">", siniInteresadoApMaterno);
                                                    this.FindAndReplace(wordApp, "<NACIONALIDAD_FISICA" + (contadoranexo2 + 1) + ">", sininacionalidad22);
                                                    this.FindAndReplace(wordApp, "<TELEFONO_FISICA" + (contadoranexo2 + 1) + ">", siniOficinaTelefono);
                                                    this.FindAndReplace(wordApp, "<CORREO_FISICA" + (contadoranexo2 + 1) + ">", siniOficinaCorreo);
                                                    this.FindAndReplace(wordApp, "<CP_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionCP);
                                                    this.FindAndReplace(wordApp, "<CALLE_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionCalle);
                                                    this.FindAndReplace(wordApp, "<NUMERO_EXTERIOR" + (contadoranexo2 + 1) + ">", siniDireccionNumExt);
                                                    this.FindAndReplace(wordApp, "<NUMERO_INTERIOR" + (contadoranexo2 + 1) + ">", siniDireccionNumInt);
                                                    this.FindAndReplace(wordApp, "<COLONIA_INTERESADO" + (contadoranexo2 + 1) + ">", "");
                                                    this.FindAndReplace(wordApp, "<MUNICIPIO_INTERESADO" + (contadoranexo2 + 1) + ">", "");
                                                    this.FindAndReplace(wordApp, "<LOCALIDAD_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionPoblacion);
                                                    this.FindAndReplace(wordApp, "<ESTADO_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionEstado);
                                                    this.FindAndReplace(wordApp, "<PAIS_INTERESADO" + (contadoranexo2 + 1) + ">", sinipaisdireccioninteresado);

                                                }
                                                else
                                                {
                                                    //MORAL EXTRANGERA
                                                    this.FindAndReplace(wordApp, "<RFC_MORAL" + (contadoranexo2 + 1) + ">", "");
                                                    this.FindAndReplace(wordApp, "<NOMBRE_MORAL" + (contadoranexo2 + 1) + ">", siniInteresadoNombre);
                                                    this.FindAndReplace(wordApp, "<NACIONALIDAD_MORAL" + (contadoranexo2 + 1) + ">", sininacionalidad22);
                                                    this.FindAndReplace(wordApp, "<TELEFONO_MORAL" + (contadoranexo2 + 1) + ">", sinitelefonomoral);
                                                    this.FindAndReplace(wordApp, "<CORREO_MORAL" + (contadoranexo2 + 1) + ">", sinicorreomoral);
                                                    this.FindAndReplace(wordApp, "<CP_MORAL" + (contadoranexo2 + 1) + ">", siniDireccionCP);
                                                    this.FindAndReplace(wordApp, "<CALLE_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionCalle);
                                                    this.FindAndReplace(wordApp, "<NUMERO_EXTERIOR" + (contadoranexo2 + 1) + ">", sininumeroexterior_inventor);
                                                    this.FindAndReplace(wordApp, "<NUMERO_INTERIOR" + (contadoranexo2 + 1) + ">", sininumerointerior_inventor);
                                                    this.FindAndReplace(wordApp, "<COLONIA_INTERESADO" + (contadoranexo2 + 1) + ">", "");
                                                    this.FindAndReplace(wordApp, "<MUNICIPIO_INTERESADO" + (contadoranexo2 + 1) + ">", "");
                                                    this.FindAndReplace(wordApp, "<LOCALIDAD_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionPoblacion);
                                                    this.FindAndReplace(wordApp, "<ESTADO_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionEstado);
                                                    this.FindAndReplace(wordApp, "<PAIS_INTERESADO" + (contadoranexo2 + 1) + ">", sinipaisdireccioninteresado);

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

                                                if (!siniInteresadoTipoPersonaSAT.Equals("ME"))
                                                {
                                                    //FISICA EXTRANGERA
                                                    this.FindAndReplace(wordApp, "<CURP_FISICA" + (contadoranexo2 + 1) + ">", "");
                                                    this.FindAndReplace(wordApp, "<NOMBRE_FISICA" + (contadoranexo2 + 1) + ">", siniInteresadoNombre);
                                                    this.FindAndReplace(wordApp, "<APELLIDO_PATFISICA" + (contadoranexo2 + 1) + ">", siniInteresadoApPaterno);
                                                    this.FindAndReplace(wordApp, "<APELLIDO_MATFISICA" + (contadoranexo2 + 1) + ">", siniInteresadoApMaterno);
                                                    this.FindAndReplace(wordApp, "<NACIONALIDAD_FISICA" + (contadoranexo2 + 1) + ">", sininacionalidad22);
                                                    this.FindAndReplace(wordApp, "<TELEFONO_FISICA" + (contadoranexo2 + 1) + ">", siniOficinaTelefono);
                                                    this.FindAndReplace(wordApp, "<CORREO_FISICA" + (contadoranexo2 + 1) + ">", siniOficinaCorreo);
                                                    this.FindAndReplace(wordApp, "<CP_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionCP);
                                                    this.FindAndReplace(wordApp, "<CALLE_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionCalle);
                                                    this.FindAndReplace(wordApp, "<NUMERO_EXTERIOR" + (contadoranexo2 + 1) + ">", siniDireccionNumExt);
                                                    this.FindAndReplace(wordApp, "<NUMERO_INTERIOR" + (contadoranexo2 + 1) + ">", siniDireccionNumInt);
                                                    this.FindAndReplace(wordApp, "<COLONIA_INTERESADO" + (contadoranexo2 + 1) + ">", "");
                                                    this.FindAndReplace(wordApp, "<MUNICIPIO_INTERESADO" + (contadoranexo2 + 1) + ">", "");
                                                    this.FindAndReplace(wordApp, "<LOCALIDAD_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionPoblacion);
                                                    this.FindAndReplace(wordApp, "<ESTADO_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionEstado);
                                                    this.FindAndReplace(wordApp, "<PAIS_INTERESADO" + (contadoranexo2 + 1) + ">", sinipaisdireccioninteresado);
                                                }
                                                else
                                                {
                                                    //MORAL EXTRANGERA
                                                    this.FindAndReplace(wordApp, "<RFC_MORAL" + (contadoranexo2 + 1) + ">", "");
                                                    this.FindAndReplace(wordApp, "<NOMBRE_MORAL" + (contadoranexo2 + 1) + ">", siniInteresadoNombre);
                                                    this.FindAndReplace(wordApp, "<NACIONALIDAD_MORAL" + (contadoranexo2 + 1) + ">", sininacionalidad22);
                                                    this.FindAndReplace(wordApp, "<TELEFONO_MORAL" + (contadoranexo2 + 1) + ">", sinitelefonomoral);
                                                    this.FindAndReplace(wordApp, "<CORREO_MORAL" + (contadoranexo2 + 1) + ">", sinicorreomoral);
                                                    this.FindAndReplace(wordApp, "<CP_MORAL" + (contadoranexo2 + 1) + ">", siniDireccionCP);
                                                    this.FindAndReplace(wordApp, "<CALLE_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionCalle);
                                                    this.FindAndReplace(wordApp, "<NUMERO_EXTERIOR" + (contadoranexo2 + 1) + ">", sininumeroexterior_inventor);
                                                    this.FindAndReplace(wordApp, "<NUMERO_INTERIOR" + (contadoranexo2 + 1) + ">", sininumerointerior_inventor);
                                                    this.FindAndReplace(wordApp, "<COLONIA_INTERESADO" + (contadoranexo2 + 1) + ">", "");
                                                    this.FindAndReplace(wordApp, "<MUNICIPIO_INTERESADO" + (contadoranexo2 + 1) + ">", "");
                                                    this.FindAndReplace(wordApp, "<LOCALIDAD_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionPoblacion);
                                                    this.FindAndReplace(wordApp, "<ESTADO_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionEstado);
                                                    this.FindAndReplace(wordApp, "<PAIS_INTERESADO" + (contadoranexo2 + 1) + ">", sinipaisdireccioninteresado);
                                                }

                                            }

                                        }
                                        else
                                        {
                                            if (!siniTipoRelacionId.Equals("1"))
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

                                                if (!siniInteresadoTipoPersonaSAT.Equals("MN"))
                                                {
                                                    //FISICA NACIONAL
                                                    this.FindAndReplace(wordApp, "<CURP_FISICA" + (contadoranexo2 + 1) + ">", siniInteresadoCurp);
                                                    this.FindAndReplace(wordApp, "<NOMBRE_FISICA" + (contadoranexo2 + 1) + ">", siniInteresadoNombre);
                                                    this.FindAndReplace(wordApp, "<APELLIDO_PATFISICA" + (contadoranexo2 + 1) + ">", siniInteresadoApPaterno);
                                                    this.FindAndReplace(wordApp, "<APELLIDO_MATFISICA" + (contadoranexo2 + 1) + ">", siniInteresadoApMaterno);
                                                    this.FindAndReplace(wordApp, "<NACIONALIDAD_FISICA" + (contadoranexo2 + 1) + ">", sininacionalidad22);
                                                    this.FindAndReplace(wordApp, "<TELEFONO_FISICA" + (contadoranexo2 + 1) + ">", siniOficinaTelefono);
                                                    this.FindAndReplace(wordApp, "<CORREO_FISICA" + (contadoranexo2 + 1) + ">", siniOficinaCorreo);
                                                    this.FindAndReplace(wordApp, "<CP_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionCP);
                                                    this.FindAndReplace(wordApp, "<CALLE_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionCalle);
                                                    this.FindAndReplace(wordApp, "<NUMERO_EXTERIOR" + (contadoranexo2 + 1) + ">", siniDireccionNumExt);
                                                    this.FindAndReplace(wordApp, "<NUMERO_INTERIOR" + (contadoranexo2 + 1) + ">", siniDireccionNumInt);
                                                    this.FindAndReplace(wordApp, "<COLONIA_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionColonia);
                                                    this.FindAndReplace(wordApp, "<MUNICIPIO_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionPoblacion);
                                                    this.FindAndReplace(wordApp, "<LOCALIDAD_INTERESADO" + (contadoranexo2 + 1) + ">", "");
                                                    this.FindAndReplace(wordApp, "<ESTADO_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionEstado);
                                                    this.FindAndReplace(wordApp, "<PAIS_INTERESADO" + (contadoranexo2 + 1) + ">", sinipaisdireccioninteresado);

                                                }
                                                else
                                                {
                                                    //MORAL NACIONAL
                                                    this.FindAndReplace(wordApp, "<RFC_MORAL" + (contadoranexo2 + 1) + ">", siniInteresadoRFC);
                                                    this.FindAndReplace(wordApp, "<NOMBRE_MORAL" + (contadoranexo2 + 1) + ">", siniInteresadoNombre);
                                                    this.FindAndReplace(wordApp, "<NACIONALIDAD_MORAL" + (contadoranexo2 + 1) + ">", sininacionalidad22);
                                                    this.FindAndReplace(wordApp, "<TELEFONO_MORAL" + (contadoranexo2 + 1) + ">", sinitelefonomoral);
                                                    this.FindAndReplace(wordApp, "<CORREO_MORAL" + (contadoranexo2 + 1) + ">", sinicorreomoral);
                                                    this.FindAndReplace(wordApp, "<CP_MORAL" + (contadoranexo2 + 1) + ">", siniDireccionCP);
                                                    this.FindAndReplace(wordApp, "<CALLE_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionCalle);
                                                    this.FindAndReplace(wordApp, "<NUMERO_EXTERIOR" + (contadoranexo2 + 1) + ">", sininumeroexterior_inventor);
                                                    this.FindAndReplace(wordApp, "<NUMERO_INTERIOR" + (contadoranexo2 + 1) + ">", sininumerointerior_inventor);
                                                    this.FindAndReplace(wordApp, "<COLONIA_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionColonia);
                                                    this.FindAndReplace(wordApp, "<MUNICIPIO_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionPoblacion);
                                                    this.FindAndReplace(wordApp, "<LOCALIDAD_INTERESADO" + (contadoranexo2 + 1) + ">", "");
                                                    this.FindAndReplace(wordApp, "<ESTADO_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionEstado);
                                                    this.FindAndReplace(wordApp, "<PAIS_INTERESADO" + (contadoranexo2 + 1) + ">", sinipaisdireccioninteresado);
                                                }
                                            }
                                            else
                                            {
                                                // es titular

                                                if (contadoranexo2 > 0)
                                                {
                                                    //this.FindAndReplace(wordApp, "X1", "");
                                                    this.FindAndReplace(wordApp, "X3", "X");
                                                    //x titular 1
                                                }
                                                else
                                                {
                                                    //x titular 2
                                                    this.FindAndReplace(wordApp, "X1", "X");
                                                    //this.FindAndReplace(wordApp, "X3", "");

                                                    if (!siniInteresadoTipoPersonaSAT.Equals("MN"))
                                                    {
                                                        //FISICA NACIONAL
                                                        //FISICA NACIONAL
                                                        this.FindAndReplace(wordApp, "<CURP_FISICA" + (contadoranexo2 + 1) + ">", sinicurp_fisica);
                                                        this.FindAndReplace(wordApp, "<NOMBRE_FISICA" + (contadoranexo2 + 1) + ">", siniInteresadoNombre);
                                                        this.FindAndReplace(wordApp, "<APELLIDO_PATFISICA" + (contadoranexo2 + 1) + ">", siniapellidopat_fisica);
                                                        this.FindAndReplace(wordApp, "<APELLIDO_MATFISICA" + (contadoranexo2 + 1) + ">", siniapellidomat_fisica);
                                                        this.FindAndReplace(wordApp, "<NACIONALIDAD_FISICA" + (contadoranexo2 + 1) + ">", sininacionalidadid);
                                                        this.FindAndReplace(wordApp, "<TELEFONO_FISICA" + (contadoranexo2 + 1) + ">", sinitelefono_fisica);
                                                        this.FindAndReplace(wordApp, "<CORREO_FISICA" + (contadoranexo2 + 1) + ">", sinicorreofisica);
                                                        this.FindAndReplace(wordApp, "<CP_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionCP);
                                                        this.FindAndReplace(wordApp, "<CALLE_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionCalle);
                                                        this.FindAndReplace(wordApp, "<NUMERO_EXTERIOR" + (contadoranexo2 + 1) + ">", sininumeroexterior_inventor);
                                                        this.FindAndReplace(wordApp, "<NUMERO_INTERIOR" + (contadoranexo2 + 1) + ">", sininumerointerior_inventor);
                                                        this.FindAndReplace(wordApp, "<COLONIA_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionColonia);
                                                        this.FindAndReplace(wordApp, "<MUNICIPIO_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionPoblacion);
                                                        this.FindAndReplace(wordApp, "<LOCALIDAD_INTERESADO" + (contadoranexo2 + 1) + ">", "");
                                                        this.FindAndReplace(wordApp, "<ESTADO_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionEstado);
                                                        this.FindAndReplace(wordApp, "<PAIS_INTERESADO" + (contadoranexo2 + 1) + ">", sinipaisdireccioninteresado);

                                                    }
                                                    else
                                                    {
                                                        //MORAL NACIONAL
                                                        this.FindAndReplace(wordApp, "<RFC_MORAL" + (contadoranexo2 + 1) + ">", siniInteresadoRFC);
                                                        this.FindAndReplace(wordApp, "<NOMBRE_MORAL" + (contadoranexo2 + 1) + ">", siniInteresadoNombre);
                                                        this.FindAndReplace(wordApp, "<NACIONALIDAD_MORAL" + (contadoranexo2 + 1) + ">", sininacionalidad22);
                                                        this.FindAndReplace(wordApp, "<TELEFONO_MORAL" + (contadoranexo2 + 1) + ">", sinitelefonomoral);
                                                        this.FindAndReplace(wordApp, "<CORREO_MORAL" + (contadoranexo2 + 1) + ">", sinicorreomoral);
                                                        this.FindAndReplace(wordApp, "<CP_MORAL" + (contadoranexo2 + 1) + ">", siniDireccionCP);
                                                        this.FindAndReplace(wordApp, "<CALLE_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionCalle);
                                                        this.FindAndReplace(wordApp, "<NUMERO_EXTERIOR" + (contadoranexo2 + 1) + ">", sininumeroexterior_inventor);
                                                        this.FindAndReplace(wordApp, "<NUMERO_INTERIOR" + (contadoranexo2 + 1) + ">", sininumerointerior_inventor);
                                                        this.FindAndReplace(wordApp, "<COLONIA_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionColonia);
                                                        this.FindAndReplace(wordApp, "<MUNICIPIO_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionPoblacion);
                                                        this.FindAndReplace(wordApp, "<LOCALIDAD_INTERESADO" + (contadoranexo2 + 1) + ">", "");
                                                        this.FindAndReplace(wordApp, "<ESTADO_INTERESADO" + (contadoranexo2 + 1) + ">", siniDireccionEstado);
                                                        this.FindAndReplace(wordApp, "<PAIS_INTERESADO" + (contadoranexo2 + 1) + ">", sinipaisdireccioninteresado);
                                                    }

                                                }


                                            }
                                            //es mexicano

                                        }
                                        contadoranexo2++;

                                        if (contadoranexo2 > 1)
                                        {
                                            limpiardocumentoanexointeresados();
                                        }

                               
                                    //Console.WriteLine("Mostramos el anexo: " + z);
                                    //genereaanexoinv(respuestastrindoc, z);
                                    //contadorfor2++;

                                         }
 
                                 
                                    }
                                Thread.Sleep(3000);
                                contadorfor2 += 2;

                                Object fileformat2 = Word.WdSaveFormat.wdFormatPDF;
                                Object SaveChange2 = Word.WdSaveOptions.wdDoNotSaveChanges;
                                Object OrianalForamt2 = Word.WdOriginalFormat.wdOriginalDocumentFormat;

                                aDoc.Activate();
                                aDoc.SaveAs2(ref fileNametres1,
                                                ref missing2, ref missing2, ref missing2, ref missing2, ref missing2,
                                                ref missing2, ref missing2, ref missing2, ref missing2, ref missing2,
                                                ref missing2, ref missing2, ref missing2, ref missing2, ref missing2);


                                aDoc.SaveAs(ref fileName1, ref fileformat2, ref missing2, ref missing2, ref missing2, ref missing2,
                                            ref missing2, ref missing2, ref missing2, ref missing2, ref missing2, ref missing2, ref missing2, ref missing2, ref missing2, ref missing2);
                                aDoc.Saved = true;
                              }   
                        }



                        if (continven > 1)
                        {
                            sinix10 = "X";

                        }

                        //fin de primera consulta ( se llena campos titular, interesado, y apoderado FECHAS DE DIVULCACION Y DE PRESENTACION)

                        // COMIENZA CUARTA SE MEJORAN PARA HACER 3 CONSULTAS EN UN SOLO KWERY

                        String querydoc4 = "SELECT * FROM king_dicss.prioridad WHERE prioridad.CasoId = " + sCasoId + " order by prioridad.PrioridadFecha DESC ";
                        MySqlDataReader respuestastrindoc4 = condoc.getdatareader(querydoc4);
                        int count = 0;
                        while (respuestastrindoc4.Read())
                        {
                        
                            siniTipoPrioridadId = validareader("TipoPrioridadId", "PrioridadId", respuestastrindoc4).Text;
                            siniPrioridadNumero = validareader("PrioridadNumero", "PrioridadId", respuestastrindoc4).Text;
                            siniPaisIDprioridad = validareader("PaisID", "PrioridadId", respuestastrindoc4).Text;
                            siniPrioridadFecha = validareader("PrioridadFecha", "PrioridadId", respuestastrindoc4).Text;



                            if (!siniTipoPrioridadId.Equals("1"))
                            {
                                //es tipo paris
                                siniprioridadpais = siniPaisIDprioridad;
                                sinifechaprioridad = siniPrioridadFecha;
                                siniprioridadnumero = siniPrioridadNumero;

                                if (!sinifechaprioridad.Equals(""))
                                {
                                    sinifechaprioridaddia = sinifechaprioridad.Substring(0, 2);
                                    sinifechaprioridadmes = sinifechaprioridad.Substring(3, 2);
                                    sinifechaprioridadano = sinifechaprioridad.Substring(6, 4);
                                }

                                String kwery45 = "SELECT * FROM  pais where PaisId  = " + siniprioridadpais + ";";
                                MySqlDataReader respuestastring145 = condoc.getdatareader(kwery45);
                                while (respuestastring145.Read())
                                {
                                    siniprioridadpais = validareader("PaisNombre", "PaisId", respuestastring145).Text;
                                }

                            

                                count++;
                            }
                            else
                            {
                                //es tipo pct
                                sininumerosolicitudpct = siniPrioridadNumero;
                                sinifechacompletapct = siniPrioridadFecha;

                                if (!sinifechacompletapct.Equals(""))
                                {
                                    sinifechacompletapct = sinifechacompletapct.Substring(0, 10);
                                    sinifechapctdia = sinifechacompletapct.Substring(0, 2);
                                    sinifechapctmes = sinifechacompletapct.Substring(3, 2);
                                    sinifechapctano = sinifechacompletapct.Substring(6, 4);
                                }
                            }




                        }

                        if (count > 1)
                        {
                            sinix13 = "X";
                            genereaanexoprio();

                        }



                        //comienza codigo para abir word 
                        object abrirDoc800 = "C:\\facturador\\documento2.docx";
                    

                        object fileNameword = @"C:\facturador\dos.docx";

                        object missing4 = Missing.Value;
                    

                        object readOnly = false; //default
                        object isVisible = false;


                        aDoc = wordApp.Documents.Open(ref abrirDoc800, ref isVisible, ref missing4,
                                                    ref missing4, ref missing4, ref missing4,
                                                    ref missing4, ref missing4, ref missing4,
                                                    ref missing4, ref missing4, ref missing4,
                                                    ref missing4, ref missing4, ref missing4, ref missing4);


                        Random random = new Random();
                        int randomNumber = random.Next(0, 100);
                        object fileNametres = srutacarpeta + "\\Patentes_" + randomNumber + "_" + sCasoId + ".docx";
                        object fileName = srutacarpeta + "\\Patentes_" + randomNumber + "_" + sCasoId + ".pdf";



                        this.FindAndReplace(wordApp, "X1", sinix1);
                        this.FindAndReplace(wordApp, "X2", sinix2);
                        this.FindAndReplace(wordApp, "X3", sinix3);
                        this.FindAndReplace(wordApp, "X4", sinix4);
                        this.FindAndReplace(wordApp, "X5", sinix5);
                        this.FindAndReplace(wordApp, "X6", sinix6);
                        this.FindAndReplace(wordApp, "X7", sinix7);
                        this.FindAndReplace(wordApp, "X8", sinix8);
                        this.FindAndReplace(wordApp, "X9", sinix9);
                        this.FindAndReplace(wordApp, "X10", sinix10);
                        this.FindAndReplace(wordApp, "X11", sinix11);
                        this.FindAndReplace(wordApp, "X12", sinix12);
                        this.FindAndReplace(wordApp, "X13", sinix13);
                        this.FindAndReplace(wordApp, "X14", sinix14);
                        this.FindAndReplace(wordApp, "X15", sinix15);
                        this.FindAndReplace(wordApp, "X16", sinix16);
                        this.FindAndReplace(wordApp, "X17", sinix17);
                        this.FindAndReplace(wordApp, "X18", sinix18);
                        this.FindAndReplace(wordApp, "X19", sinix19);
                        this.FindAndReplace(wordApp, "X20", sinix20);
                        this.FindAndReplace(wordApp, "X21", sinix21);
                        this.FindAndReplace(wordApp, "X22", sinix22);
                        this.FindAndReplace(wordApp, "X23", sinix23);
                        this.FindAndReplace(wordApp, "X24", sinix24);
                        this.FindAndReplace(wordApp, "X25", sinix25);
                        this.FindAndReplace(wordApp, "X26", sinix26);
                        this.FindAndReplace(wordApp, "X27", sinix10);
                        this.FindAndReplace(wordApp, "X28", sinix28);
                        this.FindAndReplace(wordApp, "X29", sinix29);
                        this.FindAndReplace(wordApp, "X30", sinix13);

                        this.FindAndReplace(wordApp, "<CURP_PISICA>", sinicurp_fisica);
                        this.FindAndReplace(wordApp, "<NOMBRE_FISICA>", sininombrefisica);
                        this.FindAndReplace(wordApp, "<APELLIDOPAT_FISICA>", siniapellidopat_fisica);
                        this.FindAndReplace(wordApp, "<APELLIDOMAT_FISICA>", siniapellidomat_fisica);
                        this.FindAndReplace(wordApp, "<NACIONALIDAD_FISICA>", sininacionalidad);
                        this.FindAndReplace(wordApp, "<TELEFONO_FISICA>", sinitelefono_fisica);
                        this.FindAndReplace(wordApp, "<CORREO_FISICA>", sinicorreofisica);
                        this.FindAndReplace(wordApp, "<RFC>", sinimoralrfc);
                        this.FindAndReplace(wordApp, "<NOMBRE_MORAL>", sininombremoral);
                        this.FindAndReplace(wordApp, "<NACIONALIDAD_MORAL>", sininacionalidadmoral);
                        this.FindAndReplace(wordApp, "<TELEFONO_MORAL>", sinitelefonomoral);
                        this.FindAndReplace(wordApp, "<CORREO_MORAL>", sinicorreomoral);

                        this.FindAndReplace(wordApp, "<CODIGOPOSTAL_TITULAR>", sinicodigopostal_titular);
                        this.FindAndReplace(wordApp, "<CALLE_TITULAR>", sinicalle_titular);
                        this.FindAndReplace(wordApp, "<NUMEROEXTERIOR_TITULAR>", sininumeroexterior_titular);
                        this.FindAndReplace(wordApp, "<NUMEROINTERIOR_TITULAR>", sininumerointerior_titular);
                        this.FindAndReplace(wordApp, "<COLONIA_TITULAR>", sinicolonia_titular);
                        this.FindAndReplace(wordApp, "<MUNICIPIO_TITULAR>", sinimunicipio_titular);
                        this.FindAndReplace(wordApp, "<LOCALIDAD TITULAR>", sinilocalidad_titular);
                        this.FindAndReplace(wordApp, "<ENTIDAD_FEDERATIVA_TITULAR>", sinientidadfederativa_titular);
                        this.FindAndReplace(wordApp, "<PAIS_TITULAR >", sinipais_titular);
                        this.FindAndReplace(wordApp, "<NOMBRE_INVENTOR>", sininombre_inventor);
                        this.FindAndReplace(wordApp, "<APELLIDOPAT_INVENTOR>", siniapellidopat_inventor);
                        this.FindAndReplace(wordApp, "<APELLIDOMAT_INVENTOR> ", siniapellidomat_inv);
                        this.FindAndReplace(wordApp, "<NACIONALIDAD_INVENTOR>", sininacionalidad_inventor);
                        this.FindAndReplace(wordApp, "<MAILKING>", siniOficinaCorreo);
                        this.FindAndReplace(wordApp, "<CODIGOPOSTAR_INVENTOR>", sinicodigopostal_inventor);
                        this.FindAndReplace(wordApp, "<CALLE_INVENTOR>", sinicalle_inventor);
                        this.FindAndReplace(wordApp, "<NUMEROEXTERIOR_INVENTOR>", sininumeroexterior_inventor);
                        this.FindAndReplace(wordApp, "<MUNICIPIO_INVENTOR>", sinicolonia_inventor);
                        this.FindAndReplace(wordApp, "<COLONIA_INVENTOR>", sinicolonia_inventor);
                        this.FindAndReplace(wordApp, "<NUMEROINTERIOR_INVENTOR>", sininumerointerior_inventor);
                        this.FindAndReplace(wordApp, "<LOCALIDAD_INVENTOR>", sinilocalidad_inventor);
                        this.FindAndReplace(wordApp, "<ENTIDADFEDERATIVA_INVENTOR>", sinientidadfederativa_inventor);
                        this.FindAndReplace(wordApp, "<PAIS_INVENTOR>", sinipais_inventor);
                        this.FindAndReplace(wordApp, "<TITULOInvencion>", sinitituloinvencion);
                        this.FindAndReplace(wordApp, "<FDI_DIA>", siniCasoFechaDivulgacionPreviadia);
                        this.FindAndReplace(wordApp, "<FDI_MES>", siniCasoFechaDivulgacionPreviames);
                        this.FindAndReplace(wordApp, "<FDI_ANO>", siniCasoFechaDivulgacionPreviaano);
                        this.FindAndReplace(wordApp, "<FD_DIA>", siniCasoFechaPresentaciondia);
                        this.FindAndReplace(wordApp, "<FD_MES>", siniCasoFechaPresentacionmes);
                        this.FindAndReplace(wordApp, "<FD_ANO>", siniCasoFechaPresentacionano);
                        this.FindAndReplace(wordApp, "<NUMEROSOLICITUD_PCT>", sininumerosolicitudpct);
                        this.FindAndReplace(wordApp, "<FECHAPCT_DIA>", sinifechapctdia);
                        this.FindAndReplace(wordApp, "<FECHAPCT_MES>", sinifechapctmes);
                        this.FindAndReplace(wordApp, "<FECHAPCT_ANO>", sinifechapctano);
                        this.FindAndReplace(wordApp, "<PRIORIDAD_PAIS>", siniprioridadpais);
                        this.FindAndReplace(wordApp, "<FECHAPRIORIDADIA>", sinifechaprioridaddia);
                        this.FindAndReplace(wordApp, "<FPM>", sinifechaprioridadmes);
                        this.FindAndReplace(wordApp, "<FPA>", sinifechaprioridadano);
                        this.FindAndReplace(wordApp, "<PRIORIDAD_NUMERO>", siniprioridadnumero);
                        this.FindAndReplace(wordApp, "<NOMBRE_APODERADO>", "HECTOR PATRICIO");
                        this.FindAndReplace(wordApp, "<APELLIDOPAT_APODERADO>", "VALDÉS");
                        this.FindAndReplace(wordApp, "<APELLIDOMAT_APODERADO>", "KING");
                        this.FindAndReplace(wordApp, "<TELEFONO_APODERADO>", siniOficinaTelefono);
                        this.FindAndReplace(wordApp, "<CORREO_APODERADO>", siniOficinaCorreo);
                        this.FindAndReplace(wordApp, "<CODIGOPOSTAL_APODERADO>", siniOficinaCP);
                        this.FindAndReplace(wordApp, "<CALLE_APODERADO>", siniOficinaCalle);
                        this.FindAndReplace(wordApp, "<NUMEXT_APODERADO>", siniOficinaNumExt);
                        this.FindAndReplace(wordApp, "<NUMINT_APODERADO>", siniOficinaNumInt);
                        this.FindAndReplace(wordApp, "<COLONIA_APODERADO>", siniOficinaColonia);
                        this.FindAndReplace(wordApp, "<MUNICIPIO_APODERADO>", siniOficinaMunicipio);
                        this.FindAndReplace(wordApp, "<ENTIDADF_APODERADO>", siniOficinaEstado);
                        this.FindAndReplace(wordApp, "<PAIS_APODERADO>", siniOficinaPaisId);
                        this.FindAndReplace(wordApp, "<NOMBRE_REC>", "ERNESTO GABRIEL");
                        this.FindAndReplace(wordApp, "<APELLIDOPAT_REC>", "VAZQUEZ");
                        this.FindAndReplace(wordApp, "<APELLIDOMAT_REC>", "BELMAN");

                        Object fileformat = Word.WdSaveFormat.wdFormatPDF;
                        Object SaveChange = Word.WdSaveOptions.wdDoNotSaveChanges;
                        Object OrianalForamt = Word.WdOriginalFormat.wdOriginalDocumentFormat;

                        aDoc.Activate();
                        aDoc.SaveAs2(ref fileNametres,
                                       ref missing4, ref missing4, ref missing4, ref missing4, ref missing4,
                                       ref missing4, ref missing4, ref missing4, ref missing4, ref missing4,
                                       ref missing4, ref missing4, ref missing4, ref missing4, ref missing4);
                        aDoc.SaveAs(ref fileName, ref fileformat, ref missing4, ref missing4, ref missing4, ref missing4,
                                    ref missing4, ref missing4, ref missing4, ref missing4, ref missing4, ref missing4, ref missing4, ref missing4, ref missing4, ref missing4);
                        aDoc.Saved = true;

                        wordApp.Quit(ref missing4, ref missing4, ref missing4);
                        //termina documento solicitud de patentes

                        break;
                    case "2":
                        //documento de marcas
                        // CONSULTA MARCAS
                        String querydoc2 = "SELECT "+
                                            "interesado.InteresadoId, "+
                                            "caso_marcas.CasoFechaRecepcion, " +
                                            "caso_marcas.CasoNumConcedida," +
                                            "caso_marcas.CasoNumero," +
                                            "caso_marcas.TipoSolicitudId, " +
                                            "caso_marcas.CasoTituloespanol, " +
                                            "interesado.InteresadoCurp, "+
                                            "interesado.InteresadoNombre, "+
                                            "interesado.InteresadoApPaterno, "+
                                            "interesado.InteresadoApMaterno, "+
                                            "interesado.PaisId as Nacionalidadtitular, "+
                                            "interesado.InteresadoTipoPersonaSAT, "+
                                            "datosoficina.OficinaTelefono, "+
                                            "datosoficina.OficinaCP, "+
                                            "datosoficina.OficinaCalle, "+
                                            "datosoficina.OficinaNumExt, "+
                                            "datosoficina.OficinaNumInt, "+
                                            "datosoficina.OficinaColonia, "+
                                            "datosoficina.OficinaMunicipio, "+
                                            "datosoficina.OficinaEstado, "+
                                            "datosoficina.OficinaPaisId, "+
                                            "datosoficina.OficinaCorreo, " +
                                            "casoproductos.CasoProductosClase, " +
                                            "casoproductos.CasoProductosDescripcion, " +
                                            "interesado.InteresadoRFC "+
                                            "FROM caso_marcas, casointeresado, interesado, datosoficina,casoproductos " +
                                            "WHERE caso_marcas.CasoId = " + sCasoId +
                                            " AND  caso_marcas.CasoId = casointeresado.CasoId " +
                                            " AND caso_marcas.CasoId = casoproductos.CasoId " +
                                            " AND casointeresado.InteresadoId = interesado.InteresadoID; ";


                        MySqlDataReader respuestastrindoc10 = condoc.getdatareader(querydoc2);
                        String siniCasoFechaRecepcion = "", siniCasoFechaRecepciondia = "", siniCasoFechaRecepcionmes = "", siniCasoFechaRecepcionano = "",
                               sinx1 = "", sinx2 = "", sinx3 = "", sinx4 = "", sinx5 = "", sinx6 = "", sinx7 = "", sinx8 = "", sinx9 = "", sinx10 = "", sinx11="", sinx12 = "",
                               sinx13 = "", siniCasoNumConcedida = "", sinTipoSolicitudId = "", sinInteresadoCurp = "", sinInteresadoNombre = "", sinInteresadoApPaterno="",
                               sinInteresadoApMaterno = "", sinNacionalidadtitular = "", sinInteresadoTipoPersonaSAT = "", sinOficinaTelefono = "", sinOficinaCP="",
                               sinOficinaCalle = "", sinOficinaNumExt = "", sinOficinaNumInt = "", sinOficinaColonia = "", sinOficinaMunicipio="", sincurpfisica="",
                               sinOficinaEstado = "", sinOficinaPaisId = "", sinOficinaCorreo = "", sinInteresadoId = "", siniCasoDenominacion="", sinnombrefisica="",
                               sinapellidopatfisica="", sinapellidomatfisica="", sinnacionalidadfisica="", sintelefonofisica="", sinrfcmoral="", sincasonumero="",
                               sinnacionalidadmoral = "", sintelefonomoral = "", sinInteresadoRFC = "", sinnombremoral = "", siniclase="", siniclasedescripcion=""; 
                           
                    
                   
                        while (respuestastrindoc10.Read())
                        {
                            sinInteresadoId = validareader("InteresadoId", "InteresadoId", respuestastrindoc10).Text;
                            sincasonumero = validareader("CasoNumero", "InteresadoId", respuestastrindoc10).Text;
                            siniCasoFechaRecepcion = validareader("CasoFechaRecepcion", "InteresadoId", respuestastrindoc10).Text;
                            siniCasoNumConcedida = validareader("CasoNumConcedida", "InteresadoId", respuestastrindoc10).Text;
                            sinTipoSolicitudId = validareader("TipoSolicitudId", "InteresadoId", respuestastrindoc10).Text;
                            siniCasoDenominacion = validareader("CasoTituloespanol", "InteresadoId", respuestastrindoc10).Text;
                            sinInteresadoCurp = validareader("InteresadoCurp", "InteresadoId", respuestastrindoc10).Text;
                            sinInteresadoNombre = validareader("InteresadoNombre", "InteresadoId", respuestastrindoc10).Text;
                            sinInteresadoApPaterno = validareader("InteresadoApPaterno", "InteresadoId", respuestastrindoc10).Text;
                            sinInteresadoApMaterno = validareader("InteresadoApMaterno", "InteresadoId", respuestastrindoc10).Text;
                            sinNacionalidadtitular = validareader("Nacionalidadtitular", "InteresadoId", respuestastrindoc10).Text;
                            sinInteresadoTipoPersonaSAT = validareader("InteresadoTipoPersonaSAT", "InteresadoId", respuestastrindoc10).Text;
                            sinOficinaTelefono = validareader("OficinaTelefono", "InteresadoId", respuestastrindoc10).Text;
                            sinOficinaCP = validareader("OficinaCP", "InteresadoId", respuestastrindoc10).Text;
                            sinOficinaCalle = validareader("OficinaCalle", "InteresadoId", respuestastrindoc10).Text;
                            sinOficinaNumExt = validareader("OficinaNumExt", "InteresadoId", respuestastrindoc10).Text;
                            sinOficinaNumInt = validareader("OficinaNumInt", "InteresadoId", respuestastrindoc10).Text;
                            sinOficinaColonia = validareader("OficinaColonia", "InteresadoId", respuestastrindoc10).Text;
                            sinOficinaMunicipio = validareader("OficinaMunicipio", "InteresadoId", respuestastrindoc10).Text;
                            sinOficinaEstado = validareader("OficinaEstado", "InteresadoId", respuestastrindoc10).Text;
                            sinOficinaPaisId = validareader("OficinaEstado", "InteresadoId", respuestastrindoc10).Text;
                            sinOficinaCorreo = validareader("OficinaCorreo", "InteresadoId", respuestastrindoc10).Text;
                            siniclase = validareader("CasoProductosClase", "InteresadoId", respuestastrindoc10).Text;
                            siniclasedescripcion = validareader("CasoProductosDescripcion", "InteresadoId", respuestastrindoc10).Text;
                        }


                        if (siniCasoDenominacion.Length > 14)
                        {

                            String nombrecarpeta = siniCasoDenominacion.Substring(0, 15);
                            srutacarpeta = @"C:\facturador\DocumentosCasosPrueba\" + nombrecarpeta + "_" + sincasonumero;
                            System.IO.Directory.CreateDirectory(srutacarpeta);

                        }
                        else
                        {

                            String nombrecarpeta = siniCasoDenominacion;
                            srutacarpeta = @"\\192.168.1.95\documentosserver\" + nombrecarpeta + "_" + sincasonumero;
                            System.IO.Directory.CreateDirectory(srutacarpeta);
                        }


                            String kwery470 = "SELECT * FROM  pais where PaisId  = " + sinNacionalidadtitular + ";";
                            MySqlDataReader respuestastring1470 = condoc.getdatareader(kwery470);
                            while (respuestastring1470.Read())
                            {
                                sinNacionalidadtitular = validareader("PaisNombre", "PaisId", respuestastring1470).Text;
                            }

                            if (!siniCasoFechaRecepcion.Equals(""))
                            {
                                siniCasoFechaRecepciondia = siniCasoFechaRecepcion.Substring(0, 2);
                                siniCasoFechaRecepcionmes = siniCasoFechaRecepcion.Substring(3, 2);
                                siniCasoFechaRecepcionano = siniCasoFechaRecepcion.Substring(6, 4);
                            }

                            switch (sinTipoSolicitudId)
                            {
                                case "7":
                                    sinx1 = "X";
                                    break;
                                case "8":
                                    sinx2 = "X";
                                    break;
                                case "9":
                                    sinx3 = "X";
                                    break;
                            }


                            if (!sinNacionalidadtitular.Equals("148"))
                            { 
                            // ES EXTRANGERO
                                if (!sinInteresadoTipoPersonaSAT.Equals("ME"))
                                { 
                                // ES FISICA EXTRANGERA
                                    sincurpfisica = sinInteresadoCurp;
                                    sinnombrefisica = sinInteresadoNombre;
                                    sinapellidopatfisica = sinInteresadoApPaterno;
                                    sinapellidomatfisica = sinInteresadoApMaterno;
                                    sinnacionalidadfisica = sinNacionalidadtitular;
                                    sintelefonofisica = sinOficinaTelefono;
                                    sintelefonomoral = "";
                                    sinnacionalidadmoral = "";
                                }
                                else
                                {
                                    // ES MORAL EXTRANGERA
                                    sinrfcmoral = sinInteresadoRFC;
                                    sinnombremoral = sinInteresadoNombre;
                                    sinnacionalidadmoral = sinNacionalidadtitular;
                                    sintelefonomoral = sinOficinaTelefono;
                                    sintelefonofisica = "";
                                    sinnacionalidadfisica = "";
                                

                                }
                            }
                            else
                            {
                                //ES MEXICANO
                                if (!sinInteresadoTipoPersonaSAT.Equals("MN"))
                                { 
                                    //ES FICICA NACIONAL
                                    sincurpfisica = sinInteresadoCurp;
                                    sinnombrefisica = sinInteresadoNombre;
                                    sinapellidopatfisica = sinInteresadoApPaterno;
                                    sinapellidomatfisica = sinInteresadoApMaterno;
                                    sinnacionalidadfisica = sinNacionalidadtitular;
                                    sintelefonofisica = sinOficinaTelefono;
                                    sintelefonomoral = "";
                                    sinnacionalidadmoral = "";
                                }
                                else
                                {
                                    //ES MORAL NACIONAL
                                    sinrfcmoral = sinInteresadoRFC;
                                    sinnombremoral = sinInteresadoNombre;
                                    sinnacionalidadmoral = sinNacionalidadtitular;
                                    sintelefonomoral = sinOficinaTelefono;
                                    sintelefonofisica = "";
                                    sinnacionalidadfisica = "";
                                }
                            }

                            object abrirDoc80 = "C:\\facturador\\documento3.doc";
                            object missing5= Missing.Value;

                            object readOnly15 = false; //default
                            object isVisible15 = true;

                            aDoc = wordApp.Documents.Open(ref abrirDoc80, ref isVisible15, ref readOnly15,
                                    ref missing5, ref missing5, ref missing5,
                                    ref missing5, ref missing5, ref missing5,
                                    ref missing5, ref missing5, ref missing5,
                                    ref missing5, ref missing5, ref missing5, ref missing5);

                        Random random25 = new Random();
                        int randomNumber25 = random25.Next(0, 100);
                        object fileNametres15 = srutacarpeta + "\\Marcas_" + randomNumber25 + "_" + sCasoId + ".doc";
                        object fileName15 = srutacarpeta + "\\Marcas_" + randomNumber25 + "_" + sCasoId + ".pdf";


                        this.FindAndReplace(wordApp, "X1", sinx1);
                        this.FindAndReplace(wordApp, "X2", sinx2);
                        this.FindAndReplace(wordApp, "X3", sinx3);
                        this.FindAndReplace(wordApp, "X4", sinx4);
                        this.FindAndReplace(wordApp, "X5", sinx5);
                        this.FindAndReplace(wordApp, "X6", sinx6);
                        this.FindAndReplace(wordApp, "X7", sinx7);
                        this.FindAndReplace(wordApp, "X8", sinx8);
                        this.FindAndReplace(wordApp, "X9", sinx9);
                        this.FindAndReplace(wordApp, "X10", sinx10);
                        this.FindAndReplace(wordApp, "X11", sinx11);
                        this.FindAndReplace(wordApp, "X12", sinx12);
                        this.FindAndReplace(wordApp, "X13", sinx13);

                        this.FindAndReplace(wordApp, "<DIA>", siniCasoFechaRecepciondia);
                        this.FindAndReplace(wordApp, "<MES>", siniCasoFechaRecepcionmes);
                        this.FindAndReplace(wordApp, "<ANO>", siniCasoFechaRecepcionano);
                        this.FindAndReplace(wordApp, "<NUMERO_REISTRO>", siniCasoNumConcedida);
                        this.FindAndReplace(wordApp, "<CURP_TITULAR_FISICA>", sincurpfisica);
                        this.FindAndReplace(wordApp, "<NOMBRE_TITULAR_FISICA>", sinnombrefisica);
                        this.FindAndReplace(wordApp, "<APELLIDOPAT_TITULAR_FISICA>", sinapellidopatfisica);
                        this.FindAndReplace(wordApp, "<APELLIDOMAT_TITULAR_FISICA>", sinapellidomatfisica);
                        this.FindAndReplace(wordApp, "<NACIONALIDAD_TITULAR_FISICA>", sinnacionalidadfisica);
                        this.FindAndReplace(wordApp, "<TELEFONO_TITULAR_FISICA>", sintelefonofisica);
                        this.FindAndReplace(wordApp, "<RFC_MORAL>", sinrfcmoral);
                        this.FindAndReplace(wordApp, "<NOMBRE_TITULAR_MORAL>", sinnombremoral);
                        this.FindAndReplace(wordApp, "<NACIONALIDAD_TITULAR_MORAL>", sinnacionalidadmoral);
                        this.FindAndReplace(wordApp, "<TELEFONO_TITLAR_MORAL>", sintelefonomoral);
                        this.FindAndReplace(wordApp, "<CP_NOTIFICACIONES>", sinOficinaCP);
                        this.FindAndReplace(wordApp, "<CALLE_NOTIFICACIONES>", sinOficinaCalle);
                        this.FindAndReplace(wordApp, "<NUMERO_EXTERIOR_NOTIFICACIONES>", sinOficinaNumExt);
                        this.FindAndReplace(wordApp, "<NUMERO_INTERIOR_NOTIFICACIONES>", sinOficinaNumInt);
                        this.FindAndReplace(wordApp, "<COLONIA_NOTIFICACIONES>", sinOficinaColonia);
                        this.FindAndReplace(wordApp, "<MUNICIPIO_NOTIFICACIONES>", sinOficinaMunicipio);
                        this.FindAndReplace(wordApp, "<ESTADO_NOTIFICACIONES>", sinOficinaEstado);
                        this.FindAndReplace(wordApp, "<CORREO_NOTIFICACIONES>", sinOficinaCorreo);
                        this.FindAndReplace(wordApp, "<CLASE>", siniclase);
                        this.FindAndReplace(wordApp, "<CLASE_MARCAS>", siniclasedescripcion);

                        Object fileformat25 = Word.WdSaveFormat.wdFormatPDF;
                        Object SaveChange25= Word.WdSaveOptions.wdDoNotSaveChanges;
                        Object OrianalForamt25 = Word.WdOriginalFormat.wdOriginalDocumentFormat;

                        aDoc.Activate();
                        aDoc.SaveAs2(ref fileNametres15,
                                        ref missing5, ref missing5, ref missing5, ref missing5, ref missing5,
                                        ref missing5, ref missing5, ref missing5, ref missing5, ref missing5,
                                        ref missing5, ref missing5, ref missing5, ref missing5, ref missing5);


                        aDoc.SaveAs(ref fileName15, ref fileformat25, ref missing5, ref missing5, ref missing5, ref missing5,
                                    ref missing5, ref missing5, ref missing5, ref missing5, ref missing5, ref missing5, ref missing5, ref missing5, ref missing5, ref missing5);
                        aDoc.Saved = true;

                        wordApp.Quit(ref missing5, ref missing5, ref missing5);


                        break;
                    case "3":
                //        object missing8 = Missing.Value;
                //        wordApp.Quit(ref missing8, ref missing8, ref missing8);
                //        wordApp = new Word.Application();
                //        aDoc = null;
                //        String querycartapru = "select caso.CasoId, cliente.ClienteNombre, interesado.InteresadoNombre, interesado.InteresadoApPaterno, interesado.InteresadoApMaterno, " +
                //                                "caso.CasoNumeroExpedienteLargo, caso.CasoDenominacion, casoproductos.CasoProductosClase, caso.CasoNumero, caso.TipoSolicitudId, "+
                //                                "DATE_FORMAT(caso.CasoFechaRecepcion  , '%d-%M-%Y') as CasoFechaRecepcion, caso.CasoFechaPresentacion, "+
                //                                "DATE_FORMAT(caso.CasoFechaConcesion  , '%d-%M-%Y') as CasoFechaConcesion, " +
                //                                "direccion.DireccionCalle, direccion.DireccionNumExt, " +
                //                                "direccion.DireccionNumInt, direccion.DireccionColonia, direccion.DireccionPoblacion,direccion.DireccionEstado, tiposolicitud.TipoSolicitudDescTituloEspanol, referencia.ReferenciaNombre, casoproductos.CasoProductosDescripcion  " +
                //                                "from cliente, caso, casocliente, interesado, casointeresado, casoproductos, direccion, tiposolicitud, referencia " +
                //                                "where caso.CasoId = 37812 "+
                //                                "and caso.CasoId = casocliente.CasoId "+
                //                                "and caso.CasoId = casointeresado.CasoId "+
                //                                "and casointeresado.InteresadoId = interesado.InteresadoID "+
                //                                "and caso.CasoId = casoproductos.CasoId "+
                //                                "and casocliente.ClienteId = cliente.ClienteId "+
                //                                "and  caso.TipoSolicitudId = tiposolicitud.TipoSolicitudId " +
                //                                "and caso.CasoId = referencia.CasoId "+
                //                                "and caso.CasoId = casoproductos.CasoId " +
                //                                "and (cliente.ClienteId = direccion.ClienteId or interesado.InteresadoID = direccion.InteresadoId);";

                //        MySqlDataReader respuestastringanex = condoc.getdatareader(querycartapru);

                //        object abrirDoc8 = "C:\\facturador\\CARTAPRUEBA.doc";

            
                //object readOnly8 = false;
                //object isVisible9 = false;

                //aDoc = wordApp.Documents.Open(ref abrirDoc8, ref isVisible9, ref readOnly8,
                //                            ref missing8, ref missing8, ref missing8,
                //                            ref missing8, ref missing8, ref missing8,
                //                            ref missing8, ref missing8, ref missing8,
                //                            ref missing8, ref missing8, ref missing8, ref missing8);


                //Random random8 = new Random();
                //int randomNumber8 = random8.Next(0, 100);
                //int iContwhileprioridades = 0;
                //String Remplazopar = "", Remplazopct ="";
                //object fileNametres8 = @"C:\facturador\Documentos_casos\cartas\CARTAPRUEBA" + randomNumber8 + "_" + sCasoId + ".doc";
                //object fileName8 = @"C:\facturador\Documentos_casos\cartas\CARTAPRUEBA" + randomNumber8 + "_" + sCasoId + ".pdf";
                //String producto_servicio = ""; String fechadia = "", fechames="", fechaano="", fechaconsulta="";

                //while (respuestastringanex.Read())
                //{
                //    //primer parametro es el texto, el segundo parametro es el id o valor y el tercer parametro es el mysqldatareader
                //    this.FindAndReplace(wordApp, "<CLIENTENOMBRE>", validareader("ClienteNombre", "CasoId", respuestastringanex).Text);
                //    this.FindAndReplace(wordApp, "<ClienteDireccion>", validareader("DireccionCalle", "CasoId", respuestastringanex).Text +" "+
                //          validareader("DireccionNumExt", "CasoId", respuestastringanex).Text + " "+ validareader("DireccionNumInt", "CasoId", respuestastringanex).Text+ " "+
                //          validareader("DireccionColonia", "CasoId", respuestastringanex).Text + " " +validareader("DireccionPoblacion", "CasoId", respuestastringanex).Text+ " "+
                //         validareader("DireccionEstado", "CasoId", respuestastringanex).Text);
                //    this.FindAndReplace(wordApp, "<TitularNombre>", validareader("InteresadoNombre", "CasoId", respuestastringanex).Text+ " "+
                //       validareader("InteresadoApPaterno", "CasoId", respuestastringanex).Text + " "+ validareader("InteresadoApMaterno", "CasoId", respuestastringanex).Text);
                //    this.FindAndReplace(wordApp, "<CLIENTENOMBRE>", validareader("ClienteNombre", "CasoId", respuestastringanex).Text);
                //    this.FindAndReplace(wordApp, "<AsuntoE>", validareader("TipoSolicitudDescTituloEspanol", "CasoId", respuestastringanex).Text +
                //         " N° " + validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastringanex).Text);
                //    this.FindAndReplace(wordApp, "<Titulo>", validareader("CasoDenominacion", "CasoId", respuestastringanex).Text);
                //    this.FindAndReplace(wordApp, "<CLASE>", validareader("CasoProductosClase", "CasoId", respuestastringanex).Text);
                //    this.FindAndReplace(wordApp, "<ReferenciaCliente>", validareader("ReferenciaNombre", "CasoId", respuestastringanex).Text);
                //    this.FindAndReplace(wordApp, "<ReferenciaDespacho>", validareader("CasoNumero", "CasoId", respuestastringanex).Text);

                //    producto_servicio = validareader("CasoProductosClase", "CasoId", respuestastringanex).Text;
                //    fechaconsulta = validareader("CasoFechaConcesion", "CasoId", respuestastringanex).Text;

                //    this.FindAndReplace(wordApp, "<FechaLegalEspanol>", validareader("CasoFechaRecepcion", "CasoId", respuestastringanex).Text);
                //    this.FindAndReplace(wordApp, "<RegistroDatoEspanol>", validareader("CasoFechaPresentacion", "CasoId", respuestastringanex).Text);
                //    this.FindAndReplace(wordApp, "<FechaConcesionE>", validareader("CasoFechaConcesion", "CasoId", respuestastringanex).Text);
                //    this.FindAndReplace(wordApp, "<PRODUCTOS>", validareader("CasoProductosDescripcion", "CasoId", respuestastringanex).Text);
                //    this.FindAndReplace(wordApp, "<TipoSolicitudEspanol>", "Marca");

                //}

                ////siniCasoFechaPresentaciondia = siniCasoFechaPresentacion.Substring(0, 2);Substring(3, 2);
                ////siniCasoFechaPresentacionano = siniCasoFechaPresentacion.Substring(6, 4);

                //if (!fechaconsulta.Equals(""))
                //{
                //    //fechadia = fechaconsulta.Substring(0, 2);
                //    //fechames = fechaconsulta.Substring(3, 2);
                //    //fechaano = fechaconsulta.Substring(3, 2);

                //}



                //this.FindAndReplace(wordApp, "<FechaVigenciaE>", fechadia + " " + fechames + " " + fechaano+10);
                //this.FindAndReplace(wordApp, "<FechaProbarUsoE>", "");
                     

                //        int numval = Int32.Parse(producto_servicio);
                //if (numval > 34)
                //{
                //    this.FindAndReplace(wordApp, "<TipoClaseEspanol>", "Servicios");

                //}
                //else
                //{
                //    this.FindAndReplace(wordApp, "<TipoClaseEspanol>", "Productos");

                //}

                //this.FindAndReplace(wordApp, "<FechaReporteEspanol>",  DateTime.Now);

                //Object fileformat8 = Word.WdSaveFormat.wdFormatPDF;
                //Object SaveChange8 = Word.WdSaveOptions.wdDoNotSaveChanges;
                //Object OrianalForamt8 = Word.WdOriginalFormat.wdOriginalDocumentFormat;

                //aDoc.Activate();
                //aDoc.SaveAs2(ref fileNametres8,
                //                ref missing8, ref missing8, ref missing8, ref missing8, ref missing8,
                //                ref missing8, ref missing8, ref missing8, ref missing8, ref missing8,
                //                ref missing8, ref missing8, ref missing8, ref missing8, ref missing8);


                //aDoc.SaveAs(ref fileName8, ref fileformat8, ref missing8, ref missing8, ref missing8, ref missing8,
                //            ref missing8, ref missing8, ref missing8, ref missing8, ref missing8, ref missing8, ref missing8, ref missing8, ref missing8, ref missing8);
                //aDoc.Saved = true;
            
                break;
                }
            }catch(Exception E){
                String ruta = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + "\\casosking\\";
                if (!Directory.Exists(ruta))
                {
                    System.IO.Directory.CreateDirectory(ruta);
                }
                String fechalog = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                StringBuilder sb = new StringBuilder();
                sb.Append(fechalog + ":class:generadocs.cs Error:" + E + "\n");
                System.IO.File.AppendAllText(ruta + "sistema_casosking.log", sb.ToString());
                sb.Clear();
            }
         
        }

        public void genereaanexoprio()
        {
            object missing = Missing.Value;
            wordApp.Quit(ref missing, ref missing, ref missing);
            wordApp = new Word.Application();
            aDoc = null;
            String queryanex1 = "SET lc_time_names = 'es_ES'; SELECT prioridad.PrioridadId, prioridad.PrioridadNumero, "+
            "prioridad.PaisID, DATE_FORMAT(prioridad.PrioridadFecha , '%d-%M-%Y') as  PrioridadFecha, referencia.ReferenciaNombre, "+
            "prioridad.TipoPrioridadId "+
            "FROM prioridad, referencia "+
            "WHERE prioridad.CasoId = "+sCasoId+ " AND prioridad.CasoId = referencia.CasoId order by prioridad.PrioridadFecha ASC ; ";

            MySqlDataReader respuestastringanex = condoc.getdatareader(queryanex1);
                    //primer parametro es el texto, el segundo parametro es el id o valor y el tercer parametro es el mysqldatareader
            int conpriopct = 0;
            int conpriopar = 0;
            
            String siniPrioridadId = "", siniPrioridadNumero = "", siniPaisID = "", 
                    siniPrioridadFecha = "", siniReferenciaNombre="", siniTipoPrioridadId = "";

            object abrirDoc = "C:\\facturador\\prioridades3.doc";

            
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
            String Remplazopar = "", Remplazopct ="";
            object fileNametres = srutacarpeta + "\\anexoprioridades_" + randomNumber + "_" + sCasoId + ".doc";
            object fileName = srutacarpeta + "\\anexoprioridades_" + randomNumber + "_" + sCasoId + ".pdf";
            while (respuestastringanex.Read())
            {
                //primer parametro es el texto, el segundo parametro es el id o valor y el tercer parametro es el mysqldatareader
                siniPrioridadId = validareader("PrioridadId", "PrioridadId", respuestastringanex).Text;
                siniPrioridadNumero = validareader("PrioridadNumero", "PrioridadId", respuestastringanex).Text;
                siniPaisID = validareader("PaisID", "PrioridadId", respuestastringanex).Text;
                siniPrioridadFecha = validareader("PrioridadFecha", "PrioridadId", respuestastringanex).Text.Replace("-", " de ");
                siniTipoPrioridadId = validareader("TipoPrioridadId", "PrioridadId", respuestastringanex).Text;
                siniReferenciaNombre = validareader("ReferenciaNombre", "PrioridadId", respuestastringanex).Text;

                String kwery47 = "SELECT * FROM  pais where PaisId  = " + siniPaisID + ";";
                MySqlDataReader respuestastring147 = condoc.getdatareader(kwery47);
                
                while (respuestastring147.Read())
                {
                    siniPaisID = validareader("PaisNombre", "PaisId", respuestastring147).Text;
                }
       
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
                    if (conpriopar>0)
                    {
                        Remplazopar = siniPaisID + ",  fecha  " + siniPrioridadFecha + ", con número  de solicitud " + siniPrioridadNumero + "\n\r";
                        switch (iContwhileprioridades)
                        {//reemplazamos hasta 8 variables de paris
                                case 1:{
                                   this.FindAndReplace(wordApp, "<PRIORIDADESPAR1>", Remplazopar);
                            }break;
                                case 2:{
                                    this.FindAndReplace(wordApp, "<PRIORIDADESPAR2>", Remplazopar);
                            }break;
                                case 3:{
                                    this.FindAndReplace(wordApp, "<PRIORIDADESPAR3>", Remplazopar);
                            }break;
                                case 4:{
                                    this.FindAndReplace(wordApp, "<PRIORIDADESPAR4>", Remplazopar);
                            }break;
                                case 5:{
                                    this.FindAndReplace(wordApp, "<PRIORIDADESPAR5>", Remplazopar);
                            }break;
                                case 6:{
                                    this.FindAndReplace(wordApp, "<PRIORIDADESPAR6>", Remplazopar);
                            }break;
                                case 7:{
                                    this.FindAndReplace(wordApp, "<PRIORIDADESPAR7>", Remplazopar);
                                
                            }break;
                                case 8:{
                                    this.FindAndReplace(wordApp, "<PRIORIDADESPAR8>", Remplazopar);
                            
                            }break;
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
            for (int i = 0; i < 20; i++)
            {
                this.FindAndReplace(wordApp, "<PRIORIDADESPCT" + (i + 1) + ">", "");
                this.FindAndReplace(wordApp, "<PRIORIDADESPAR" + (i + 1) + ">", "");
            }
            this.FindAndReplace(wordApp, "«ReferenciaDespacho»", siniReferenciaNombre); 
            Object fileformat2 = Word.WdSaveFormat.wdFormatPDF;
            Object SaveChange2 = Word.WdSaveOptions.wdDoNotSaveChanges;
            Object OrianalForamt2 = Word.WdOriginalFormat.wdOriginalDocumentFormat;

            aDoc.Activate();
            aDoc.SaveAs2(ref fileNametres,
                            ref missing2, ref missing2, ref missing2, ref missing2, ref missing2,
                            ref missing2, ref missing2, ref missing2, ref missing2, ref missing2,
                            ref missing2, ref missing2, ref missing2, ref missing2, ref missing2);


            aDoc.SaveAs(ref fileName, ref fileformat2, ref missing2, ref missing2, ref missing2, ref missing2,
                        ref missing2, ref missing2, ref missing2, ref missing2, ref missing2, ref missing2, ref missing2, ref missing2, ref missing2, ref missing2);
            aDoc.Saved = true;
        }


            
        private void FindAndReplace(Microsoft.Office.Interop.Word.Application wordApp, object findText, object replaceWithText)
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


        public void limpiardocumentoanexointeresados() {

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
    }

    public class Account
    {
        public int ID { get; set; }
        public double Balance { get; set; }
    }
}