using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Office.Interop.Word;
using MySql.Data.MySqlClient;

using System.Reflection;

using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using System.IO;
using System.Diagnostics;

namespace Facturador
{
    class generacartas
    {

        public String sCasoId;
        public String sTipoformato;
        public String sIdcarta;
        object missing = Missing.Value;
        Word.Application wordApp;
        Word.Document aDoc = null;
        object missing2 = Missing.Value;
        conect condoc;
        String cartaruta = "";
        public String srutacarpeta;
        String fecha ="";
        String fechamesreporte ="";
        String fechadiareporte ="";
        String fechaanoreporte ="";
        String fechamesreportei = "";
        String Sinicioruta;
        String sinirutacartas;


        public void generacartass(String CasoId, String valueformato, String valorcarta)
        {
            try {
                condoc = new conect();
                String strRutaArchivo = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                wordApp = new Word.Application();
                aDoc = null;
                sCasoId = CasoId;
                wordApp.Quit(ref missing, ref missing, ref missing);
                wordApp = new Word.Application();
                aDoc = null;
                sIdcarta = valorcarta;
                sTipoformato = valueformato;
                Sinicioruta = strRutaArchivo + @"\casosking\DocumentosCasosPrueba\";
                sinirutacartas = strRutaArchivo + @"\casosking\Cartas\";
                CreateIconInWordDoc();
            }catch(Exception E){
                String ruta = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + "\\casosking\\";
                if (!Directory.Exists(ruta))
                {
                    System.IO.Directory.CreateDirectory(ruta);
                }
                String fechalog = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                StringBuilder sb = new StringBuilder();
                sb.Append(fechalog + ":class:generacartas.cs Error:" + E + "\n");
                System.IO.File.AppendAllText(ruta + "sistema_casosking.log", sb.ToString());
                sb.Clear();
            }
            
            //String kwery = "SELECT * FROM documentos_dicss where id_documentos_dicss = " + sIdcarta + ";";
            //MySqlDataReader respuestastring = condoc.getdatareader(kwery);
            //while (respuestastring.Read())
            //{
            //    cartaruta = validareader("ruta_cartas", "id_documentos_dicss", respuestastring).Text;
            //}
        }

        private void CreateIconInWordDoc()
        {
            try { 
            switch (sTipoformato)
            {
                case "1":

                    //PATENTES
                    object missing = Missing.Value;
                    wordApp.Quit(ref missing, ref missing, ref missing);
                    wordApp = new Word.Application();
                    aDoc = null;
                    String querycartapru0 = "SELECT "+
                                                "caso_patente.CasoId, "+
                                                "cliente.ClienteNombre, "+
                                                "interesado.InteresadoNombre, "+
                                                "interesado.InteresadoApPaterno, "+
                                                "interesado.InteresadoApMaterno, "+
                                                "caso_patente.CasoNumeroExpedienteLargo, "+
                                                "caso_patente.CasoTituloingles, "+
                                                "caso_patente.CasoTituloespanol, "+
                                                "caso_patente.TipoSolicitudId, "+
                                                "DATE_FORMAT(caso_patente.CasoFechaConcesion, '%d-%m-%Y') as CasoFechaConcesion, "+
                                                "DATE_FORMAT(caso_patente.CasoFechaRecepcion, '%d-%m-%Y') as CasoFechaRecepcion, "+
                                                "DATE_FORMAT(caso_patente.CasoFechaVigencia, '%d-%m-%Y') as CasoFechaVigencia, "+
                                                "DATE_FORMAT(caso_patente.CasoFechaLegal, '%d-%m-%Y') as CasoFechaLegal, "+
                                                "DATE_FORMAT(caso_patente.CasoFechaCartaCliente, '%d-%m-%Y') as CasoFechaCartaCliente, "+
                                                "caso_patente.CasoNumero, "+
                                                "direccion.DireccionCalle, "+ 
                                                "direccion.DireccionNumExt, "+
                                                "direccion.DireccionNumInt, "+
                                                "direccion.DireccionColonia, "+
                                                "direccion.DireccionPoblacion, "+
                                                "direccion.DireccionEstado, "+
                                                "direccion.PaisId, "+
                                                "direccion.DireccionCP, "+
                                                "tiposolicitud.TipoSolicitudDescrip, " +
                                                "tiposolicitud.TipoSolicitudDescripI, " +
                                                "caso_patente.CasoNumConcedida, "+
                                                "casointeresado.CasoInteresadoSecuencia, "+
                                                "caso_patente.CasoTitular, " +
                                                "casointeresado.TipoRelacionId "+
                                            "FROM "+
                                                "caso_patente, "+
                                                "cliente, "+
                                                "casocliente, "+
                                                "interesado, "+
                                                "casointeresado, "+
                                                "direccion, "+
                                                "tiposolicitud "+                                        
                                            "WHERE "+
                                                "caso_patente.CasoId = " +sCasoId+                                                
                                                " AND caso_patente.CasoId = casocliente.CasoId "+
                                                "AND caso_patente.CasoId = casointeresado.CasoId "+
                                                "AND casointeresado.InteresadoId = interesado.InteresadoID "+
                                                "AND casocliente.ClienteId = cliente.ClienteId "+
                                                "AND (cliente.ClienteId = direccion.ClienteId or interesado.InteresadoID = direccion.InteresadoId) "+
                                                "AND  caso_patente.TipoSolicitudId = tiposolicitud.TipoSolicitudId "+
                                                "AND casointeresado.TipoRelacionId = 1 "+
                                                "ORDER BY CasoInteresadoSecuencia ASC limit 1; ";
                    MySqlDataReader respuestastringanex0 = condoc.getdatareader(querycartapru0);
                    object abrirDoc = sinirutacartas + sIdcarta;


                    object readOnly = false;
                    object isVisible = false;

                    aDoc = wordApp.Documents.Open(ref abrirDoc, ref isVisible, ref readOnly,
                                                ref missing, ref missing, ref missing,
                                                ref missing, ref missing, ref missing,
                                                ref missing, ref missing, ref missing,
                                                ref missing, ref missing, ref missing, ref missing);
                    String fechaconcecion0 = "", fechavigencia0 = "", fechalegal0 = "", fecharecepcion0 = "", siniCasoTituloespanol="", siniCasoNumero="";
                    while (respuestastringanex0.Read())
                    {
                        //primer parametro es el texto, el segundo parametro es el id o valor y el tercer parametro es el mysqldatareader
                        this.FindAndReplace(wordApp, "«CLIENTENOMBRE»", validareader("ClienteNombre", "CasoId", respuestastringanex0).Text);
                        this.FindAndReplace(wordApp, "«ClienteDireccion»", validareader("DireccionCalle", "CasoId", respuestastringanex0).Text + " " +
                              validareader("DireccionNumExt", "CasoId", respuestastringanex0).Text + " " + validareader("DireccionNumInt", "CasoId", respuestastringanex0).Text + " " +
                              validareader("DireccionColonia", "CasoId", respuestastringanex0).Text + " " + validareader("DireccionPoblacion", "CasoId", respuestastringanex0).Text + " " +
                             validareader("DireccionEstado", "CasoId", respuestastringanex0).Text);
                        this.FindAndReplace(wordApp, "«TitularNombre»", validareader("InteresadoNombre", "CasoId", respuestastringanex0).Text + " " +
                           validareader("InteresadoApPaterno", "CasoId", respuestastringanex0).Text + " " + validareader("InteresadoApMaterno", "CasoId", respuestastringanex0).Text);
                        this.FindAndReplace(wordApp, "«ClienteDireccion»", validareader("ClienteNombre", "CasoId", respuestastringanex0).Text);
                        this.FindAndReplace(wordApp, "«AsuntoE»", validareader("TipoSolicitudDescrip", "CasoId", respuestastringanex0).Text +
                             " N° " + validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastringanex0).Text);
                        this.FindAndReplace(wordApp, "«Titulo»", validareader("CasoTituloespanol", "CasoId", respuestastringanex0).Text);
                        this.FindAndReplace(wordApp, "«NombreTituloEspanol»", validareader("CasoTituloespanol", "CasoId", respuestastringanex0).Text);
                        this.FindAndReplace(wordApp, "«TituloIngles»", validareader("CasoTituloingles", "CasoId", respuestastringanex0).Text);
                        this.FindAndReplace(wordApp, "«NombreTituloIngles»", validareader("CasoTituloingles", "CasoId", respuestastringanex0).Text);

                        this.FindAndReplace(wordApp, "«ReferenciaDespacho»", validareader("CasoNumero", "CasoId", respuestastringanex0).Text);


                        fechaconcecion0 = validareader("CasoFechaConcesion", "CasoId", respuestastringanex0).Text;
                        fechavigencia0 = validareader("CasoFechaVigencia", "CasoId", respuestastringanex0).Text;
                        fechalegal0 = validareader("CasoFechaLegal", "CasoId", respuestastringanex0).Text;
                        fecharecepcion0 = validareader("CasoFechaRecepcion", "CasoId", respuestastringanex0).Text;


                        this.FindAndReplace(wordApp, "«RegistroDatoEspanol»", "Registro N° " + validareader("CasoNumConcedida", "CasoId", respuestastringanex0).Text);
                        this.FindAndReplace(wordApp, "«TipoSolicitudEspanol»", validareader("TipoSolicitudDescrip", "CasoId", respuestastringanex0).Text);

                        this.FindAndReplace(wordApp, "«Asunto»", validareader("TipoSolicitudDescripI", "CasoId", respuestastringanex0).Text +
                            " N° " + validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastringanex0).Text);

                        this.FindAndReplace(wordApp, "«TituloRegistroDatoEspanol»", validareader("TipoSolicitudDescrip", "CasoId", respuestastringanex0).Text + " N° " + validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastringanex0).Text);
                        this.FindAndReplace(wordApp, "«TituloRegistroDato»", validareader("TipoSolicitudDescripI", "CasoId", respuestastringanex0).Text + " N° " + validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastringanex0).Text);

                        siniCasoTituloespanol = validareader("CasoTituloespanol", "CasoId", respuestastringanex0).Text;
                        
                        siniCasoNumero = validareader("CasoNumero", "CasoId", respuestastringanex0).Text;

                    }
                    respuestastringanex0.Close();
                    
                    String querycartapru20 = "SELECT * FROM referencia where referencia.CasoId = " +sCasoId+";" ;
                    MySqlDataReader respuestastringanex20 = condoc.getdatareader(querycartapru20);
                        while (respuestastringanex20.Read())
                        {
                            this.FindAndReplace(wordApp, "«ReferenciaCliente»", validareader("ReferenciaNombre", "ReferenciaId", respuestastringanex20).Text);
                        }

                        this.FindAndReplace(wordApp, "«ReferenciaCliente»", "");

                        respuestastringanex20.Close();

                    String querycartapru30 = "SELECT  usuario.UsuarioClave, usuario.UsuarioId FROM caso_marcas, usuario where caso_marcas.CasoId = "+sCasoId+" AND caso_marcas.UsuarioId = usuario.UsuarioId; ";
                    MySqlDataReader respuestastringanex30 = condoc.getdatareader(querycartapru30);
                    while (respuestastringanex30.Read())
                        {
                            this.FindAndReplace(wordApp, "«Iniciales»", validareader("UsuarioClave", "UsuarioId", respuestastringanex30).Text);
                        }

                    this.FindAndReplace(wordApp, "«Iniciales»", "");

                    respuestastringanex30.Close();


                    if (siniCasoTituloespanol.Length > 14)
                    {

                        String nombrecarpeta = siniCasoTituloespanol.Substring(0, 15);
                        srutacarpeta = Sinicioruta + nombrecarpeta + "_" + siniCasoNumero;
                        System.IO.Directory.CreateDirectory(srutacarpeta);

                    }
                    else
                    {

                        String nombrecarpeta = siniCasoTituloespanol;
                        srutacarpeta = Sinicioruta + nombrecarpeta + "_" + siniCasoNumero;
                        System.IO.Directory.CreateDirectory(srutacarpeta);
                    }


                                        Random random = new Random();
                    int randomNumber = random.Next(0, 100);
                    object fileNametres = srutacarpeta + "\\Carta_" + sIdcarta + randomNumber + " " + sCasoId + ".doc";
                    object fileName = srutacarpeta + "\\Carta_" + sIdcarta + randomNumber + " " + sCasoId + ".pdf";




                    if (!fechalegal0.Equals(""))
                    {
                        String fechadialegal0 = fechalegal0.Substring(0, 2);
                        String fechameslegal0 = fechalegal0.Substring(3, 2);
                        String fechameslegali0 = fechalegal0.Substring(3, 2);
                        String fechaanolegal0 = fechalegal0.Substring(6, 4);

                        switch (fechameslegal0)
                        {
                            case "01":
                                fechameslegal0 = "Enero";
                                break;
                            case "02":
                                fechameslegal0 = "Febrero";
                                break;
                            case "03":
                                fechameslegal0 = "Marzo";
                                break;
                            case "04":
                                fechameslegal0 = "Abril";
                                break;
                            case "05":
                                fechameslegal0 = "Mayo";
                                break;
                            case "06":
                                fechameslegal0 = "Junio";
                                break;
                            case "07":
                                fechameslegal0 = "Julio";
                                break;
                            case "08":
                                fechameslegal0 = "Agosto";
                                break;
                            case "09":
                                fechameslegal0 = "Septiembre";
                                break;
                            case "10":
                                fechameslegal0 = "Octubre";
                                break;
                            case "11":
                                fechameslegal0 = "Noviembre";
                                break;
                            case "12":
                                fechameslegali0 = "Diciembre";
                                break;

                        }

                        switch (fechameslegali0)
                        {
                            case "01":
                                fechameslegali0 = "January";
                                break;
                            case "02":
                                fechameslegali0 = "February";
                                break;
                            case "03":
                                fechameslegali0 = "March";
                                break;
                            case "04":
                                fechameslegali0 = "April";
                                break;
                            case "05":
                                fechameslegali0 = "May";
                                break;
                            case "06":
                                fechameslegali0 = "June";
                                break;
                            case "07":
                                fechameslegali0 = "July";
                                break;
                            case "08":
                                fechameslegali0 = "August";
                                break;
                            case "09":
                                fechameslegali0 = "September";
                                break;
                            case "10":
                                fechameslegali0 = "October";
                                break;
                            case "11":
                                fechameslegali0 = "November";
                                break;
                            case "12":
                                fechameslegali0 = "December";
                                break;

                        }
                        String FechaLegalEspanol = fechadialegal0 + " de " + fechameslegali0 + " de " + fechaanolegal0;
                        String FechaLegalIngles = fechameslegali0 + " " + fechadialegal0 + ", " + fechaanolegal0;
                        this.FindAndReplace(wordApp, "«FechaLegalEspanol»", FechaLegalEspanol);
                        this.FindAndReplace(wordApp, "«FechaLegalIngles»", FechaLegalIngles);
                        this.FindAndReplace(wordApp, "«FechaPresentacionE»", FechaLegalEspanol);
                        this.FindAndReplace(wordApp, "«FechaPresentacionI»", FechaLegalIngles);


                    }
                    else
                    {
                        this.FindAndReplace(wordApp, "«FechaLegalEspanol»", "");
                        this.FindAndReplace(wordApp, "«FechaLegalIngles»", "");
                        this.FindAndReplace(wordApp, "«FechaPresentacionE»", "");
                        this.FindAndReplace(wordApp, "«FechaPresentacionI»", "");

                    }




                    if (!fecharecepcion0.Equals(""))
                    {
                        String fechadiarecepcion0 = fecharecepcion0.Substring(0, 2);
                        String fechamesrecepcion0 = fecharecepcion0.Substring(3, 2);
                        String fechamesrecepcioni0 = fecharecepcion0.Substring(3, 2);
                        String fechaanorecepcion0 = fecharecepcion0.Substring(6, 4);

                        switch (fechamesrecepcion0)
                        {
                            case "01":
                                fechamesrecepcion0 = "Enero";
                                break;
                            case "02":
                                fechamesrecepcion0 = "Febrero";
                                break;
                            case "03":
                                fechamesrecepcion0 = "Marzo";
                                break;
                            case "04":
                                fechamesrecepcion0 = "Abril";
                                break;
                            case "05":
                                fechamesrecepcion0 = "Mayo";
                                break;
                            case "06":
                                fechamesrecepcion0 = "Junio";
                                break;
                            case "07":
                                fechamesrecepcion0 = "Julio";
                                break;
                            case "08":
                                fechamesrecepcion0 = "Agosto";
                                break;
                            case "09":
                                fechamesrecepcion0 = "Septiembre";
                                break;
                            case "10":
                                fechamesrecepcion0 = "Octubre";
                                break;
                            case "11":
                                fechamesrecepcion0 = "Noviembre";
                                break;
                            case "12":
                                fechamesrecepcion0 = "Diciembre";
                                break;

                        }

                        switch (fechamesrecepcioni0)
                        {
                            case "01":
                                fechamesrecepcioni0 = "January";
                                break;
                            case "02":
                                fechamesrecepcioni0 = "February";
                                break;
                            case "03":
                                fechamesrecepcioni0 = "March";
                                break;
                            case "04":
                                fechamesrecepcioni0 = "April";
                                break;
                            case "05":
                                fechamesrecepcioni0 = "May";
                                break;
                            case "06":
                                fechamesrecepcioni0 = "June";
                                break;
                            case "07":
                                fechamesrecepcioni0 = "July";
                                break;
                            case "08":
                                fechamesrecepcioni0 = "August";
                                break;
                            case "09":
                                fechamesrecepcioni0 = "September";
                                break;
                            case "10":
                                fechamesrecepcioni0 = "October";
                                break;
                            case "11":
                                fechamesrecepcioni0 = "November";
                                break;
                            case "12":
                                fechamesrecepcioni0 = "December";
                                break;

                        }
                        String FechaLegalEspanol = fechadiarecepcion0 + " de " + fechamesrecepcion0 + " de " + fechaanorecepcion0;
                        String FechaLegalIngles = fechamesrecepcioni0 + " " + fechadiarecepcion0 + ", " + fechaanorecepcion0;
                        this.FindAndReplace(wordApp, "«FechaLegalEspanol»", FechaLegalEspanol);
                        this.FindAndReplace(wordApp, "«FechaLegalIngles»", FechaLegalIngles);
                        this.FindAndReplace(wordApp, "«FechaPresentacionE»", FechaLegalEspanol);
                        this.FindAndReplace(wordApp, "«FechaPresentacionI»", FechaLegalIngles);


                    }
                    else
                    {
                        this.FindAndReplace(wordApp, "«FechaLegalEspanol»", "");
                        this.FindAndReplace(wordApp, "«FechaLegalIngles»", "");
                        this.FindAndReplace(wordApp, "«FechaPresentacionE»", "");
                        this.FindAndReplace(wordApp, "«FechaPresentacionI»", "");

                    }


                    if (!fechaconcecion0.Equals(""))
                    {
                        String fechadiaconcecion0 = fechaconcecion0.Substring(0, 2);
                        String fechamesconcecion0 = fechaconcecion0.Substring(3, 2);
                        String fechamesconcecioni0 = fechaconcecion0.Substring(3, 2);
                        String fechaanoconcecion0 = fechaconcecion0.Substring(6, 4);

                        switch (fechamesconcecion0)
                        {
                            case "01":
                                fechamesconcecion0 = "Enero";
                                break;
                            case "02":
                                fechamesconcecion0 = "Febrero";
                                break;
                            case "03":
                                fechamesconcecion0 = "Marzo";
                                break;
                            case "04":
                                fechamesconcecion0 = "Abril";
                                break;
                            case "05":
                                fechamesconcecion0 = "Mayo";
                                break;
                            case "06":
                                fechamesconcecion0 = "Junio";
                                break;
                            case "07":
                                fechamesconcecion0 = "Julio";
                                break;
                            case "08":
                                fechamesconcecion0 = "Agosto";
                                break;
                            case "09":
                                fechamesconcecion0 = "Septiembre";
                                break;
                            case "10":
                                fechamesconcecion0 = "Octubre";
                                break;
                            case "11":
                                fechamesconcecion0 = "Noviembre";
                                break;
                            case "12":
                                fechamesconcecion0 = "Diciembre";
                                break;

                        }

                        switch (fechamesconcecioni0)
                        {
                            case "01":
                                fechamesconcecioni0 = "January";
                                break;
                            case "02":
                                fechamesconcecioni0 = "February";
                                break;
                            case "03":
                                fechamesconcecioni0 = "March";
                                break;
                            case "04":
                                fechamesconcecioni0 = "April";
                                break;
                            case "05":
                                fechamesconcecioni0 = "May";
                                break;
                            case "06":
                                fechamesconcecioni0 = "June";
                                break;
                            case "07":
                                fechamesconcecioni0 = "July";
                                break;
                            case "08":
                                fechamesconcecioni0 = "August";
                                break;
                            case "09":
                                fechamesconcecioni0 = "September";
                                break;
                            case "10":
                                fechamesconcecioni0 = "October";
                                break;
                            case "11":
                                fechamesconcecioni0 = "November";
                                break;
                            case "12":
                                fechamesconcecioni0 = "December";
                                break;

                        }
                        String FechaConcesionE = fechadiaconcecion0 + " de " + fechamesconcecion0 + " de " + fechaanoconcecion0;
                        String FechaConcesionI = fechamesconcecioni0 + " " + fechadiaconcecion0 + ", " + fechaanoconcecion0;
                        this.FindAndReplace(wordApp, "«FechaConcesionE»", FechaConcesionE);
                        this.FindAndReplace(wordApp, "«FechaConcesionI»", FechaConcesionI);


                    }
                    else
                    {
                        this.FindAndReplace(wordApp, "«FechaConcesionE»", "");
                        this.FindAndReplace(wordApp, "«FechaConcesionI»", "");

                    }


                    if (!fechavigencia0.Equals(""))
                    {
                        String fechadiavigencia0 = fechavigencia0.Substring(0, 2);
                        String fechamesvigencia0 = fechavigencia0.Substring(3, 2);
                        String fechamesvigenciani0 = fechavigencia0.Substring(3, 2);
                        String fechaanovigencia0 = fechavigencia0.Substring(6, 4);

                        switch (fechamesvigencia0)
                        {
                            case "01":
                                fechamesvigencia0 = "Enero";
                                break;
                            case "02":
                                fechamesvigencia0 = "Febrero";
                                break;
                            case "03":
                                fechamesvigencia0 = "Marzo";
                                break;
                            case "04":
                                fechamesvigencia0 = "Abril";
                                break;
                            case "05":
                                fechamesvigencia0 = "Mayo";
                                break;
                            case "06":
                                fechamesvigencia0 = "Junio";
                                break;
                            case "07":
                                fechamesvigencia0 = "Julio";
                                break;
                            case "08":
                                fechamesvigencia0 = "Agosto";
                                break;
                            case "09":
                                fechamesvigencia0 = "Septiembre";
                                break;
                            case "10":
                                fechamesvigencia0 = "Octubre";
                                break;
                            case "11":
                                fechamesvigencia0 = "Noviembre";
                                break;
                            case "12":
                                fechamesvigencia0 = "Diciembre";
                                break;

                        }

                        switch (fechamesvigenciani0)
                        {
                            case "01":
                                fechamesvigenciani0 = "January";
                                break;
                            case "02":
                                fechamesvigenciani0 = "February";
                                break;
                            case "03":
                                fechamesvigenciani0 = "March";
                                break;
                            case "04":
                                fechamesvigenciani0 = "April";
                                break;
                            case "05":
                                fechamesvigenciani0 = "May";
                                break;
                            case "06":
                                fechamesvigenciani0 = "June";
                                break;
                            case "07":
                                fechamesvigenciani0 = "July";
                                break;
                            case "08":
                                fechamesvigenciani0 = "August";
                                break;
                            case "09":
                                fechamesvigenciani0 = "September";
                                break;
                            case "10":
                                fechamesvigenciani0 = "October";
                                break;
                            case "11":
                                fechamesvigenciani0 = "November";
                                break;
                            case "12":
                                fechamesvigenciani0 = "December";
                                break;

                        }
                        String FechaVigenciaE = fechadiavigencia0 + " de " + fechamesvigencia0 + " de " + fechaanovigencia0;
                        String FechaVigenciaI = fechamesvigenciani0 + " " + fechadiavigencia0 + ", " + fechaanovigencia0;
                        this.FindAndReplace(wordApp, "«FechaVigenciaE»", FechaVigenciaE);
                        this.FindAndReplace(wordApp, "«FechaVigenciaI»", FechaVigenciaI);


                    }
                    else
                    {
                        this.FindAndReplace(wordApp, "«FechaVigenciaE»", "");
                        this.FindAndReplace(wordApp, "«FechaVigenciaI»", "");
                    }
                    // fin de fecha vigencia 



                     fecha = DateTime.Now.ToString("MM/dd/yyyy");
                     fechamesreporte = fecha.Substring(0, 2);
                     fechadiareporte = fecha.Substring(3, 2);
                     fechaanoreporte = fecha.Substring(3, 2);
                     fechamesreportei = fecha.Substring(0, 2);


                    switch (fechamesreporte)
                    {
                        case "01":
                            fechamesreporte = "Enero";
                            break;
                        case "02":
                            fechamesreporte = "Febrero";
                            break;
                        case "03":
                            fechamesreporte = "Marzo";
                            break;
                        case "04":
                            fechamesreporte = "Abril";
                            break;
                        case "05":
                            fechamesreporte = "Mayo";
                            break;
                        case "06":
                            fechamesreporte = "Junio";
                            break;
                        case "07":
                            fechamesreporte = "Julio";
                            break;
                        case "08":
                            fechamesreporte = "Agosto";
                            break;
                        case "09":
                            fechamesreporte = "Septiembre";
                            break;
                        case "10":
                            fechamesreporte = "Octubre";
                            break;
                        case "11":
                            fechamesreporte = "Noviembre";
                            break;
                        case "12":
                            fechamesreporte = "Diciembre";
                            break;
                       
                    }

                    switch (fechamesreportei)
                    {
                        case "01":
                            fechamesreportei = "January";
                            break;
                        case "02":
                            fechamesreportei = "February";
                            break;
                        case "03":
                            fechamesreportei = "March";
                            break;
                        case "04":
                            fechamesreportei = "April";
                            break;
                        case "05":
                            fechamesreportei = "May";
                            break;
                        case "06":
                            fechamesreportei = "June";
                            break;
                        case "07":
                            fechamesreportei = "July";
                            break;
                        case "08":
                            fechamesreportei = "August";
                            break;
                        case "09":
                            fechamesreportei = "September";
                            break;
                        case "10":
                            fechamesreportei = "October";
                            break;
                        case "11":
                            fechamesreportei = "November";
                            break;
                        case "12":
                            fechamesreportei = "December";
                            break;

                    }
                    String Fechareportespanol0 = fechadiareporte + " de " + fechamesreporte + " de " + fechaanoreporte;
                    String Fechareporteingles0 =  fechamesreportei + " "+fechadiareporte +", "+ fechaanoreporte;

                    this.FindAndReplace(wordApp, "«FechaReporteEspanol»", Fechareportespanol0);
                    this.FindAndReplace(wordApp, "«FechaReporte»", Fechareporteingles0);

                    Object fileformat = Word.WdSaveFormat.wdFormatPDF;
                    Object SaveChange = Word.WdSaveOptions.wdDoNotSaveChanges;
                    Object OrianalForamt = Word.WdOriginalFormat.wdOriginalDocumentFormat;

                    aDoc.Activate();
                    aDoc.SaveAs2(ref fileNametres,
                                    ref missing, ref missing, ref missing, ref missing, ref missing,
                                    ref missing, ref missing, ref missing, ref missing, ref missing,
                                    ref missing, ref missing, ref missing, ref missing, ref missing);


                    aDoc.SaveAs(ref fileName, ref fileformat, ref missing, ref missing, ref missing, ref missing,
                                ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
                    aDoc.Saved = true;

                    wordApp.Quit(ref missing, ref missing, ref missing);
                    
                    Process.Start(fileNametres.ToString());
                    Process.Start(fileName.ToString());



                    break;
                case "2":
                    //MARCAS

                    object missing8 = Missing.Value;
                    wordApp.Quit(ref missing8, ref missing8, ref missing8);
                    wordApp = new Word.Application();
                    aDoc = null;
                    String querycartapru = "SELECT "+
                                            "caso_marcas.CasoId, "+
                                            "cliente.ClienteNombre, "+
                                            "interesado.InteresadoNombre, "+
                                            "interesado.InteresadoApPaterno, "+
                                            "interesado.InteresadoApMaterno, "+
                                            "caso_marcas.CasoNumeroExpedienteLargo, "+
                                            "caso_marcas.CasoTituloingles, "+
                                            "caso_marcas.CasoTituloespanol, "+
                                            "caso_marcas.TipoSolicitudId, "+
                                            "DATE_FORMAT(caso_marcas.CasoFechaConcesion, '%d-%m-%Y') as CasoFechaConcesion, "+
                                            "DATE_FORMAT(caso_marcas.CasoFechaRecepcion, '%d-%m-%Y') as CasoFechaRecepcion, "+
                                            "DATE_FORMAT(caso_marcas.CasoFechaVigencia, '%d-%m-%Y') as CasoFechaVigencia, "+
                                            "DATE_FORMAT(caso_marcas.CasoFechaLegal, '%d-%m-%Y') as CasoFechaLegal, "+
                                            "DATE_FORMAT(caso_marcas.CasoFechaCartaCliente, '%d-%m-%Y') as CasoFechaCartaCliente, "+
                                            "DATE_FORMAT(caso_marcas.CasoFechaprobouso, '%d-%m-%Y') as CasoFechaprobouso, "+
                                            "casoproductos.CasoProductosClase, "+
                                            "caso_marcas.CasoNumero, "+
                                            "direccion.DireccionCalle, "+
                                            "direccion.DireccionNumExt, "+
                                            "direccion.DireccionNumInt, "+
                                            "direccion.DireccionColonia, "+
                                            "direccion.DireccionPoblacion, "+
                                            "direccion.DireccionEstado, "+
                                            "direccion.PaisId, "+
                                            "direccion.DireccionCP, "+
                                            "tiposolicitud.TipoSolicitudDescrip, " +
                                            "tiposolicitud.TipoSolicitudDescripI, " +
                                            "casoproductos.CasoProductosDescripcion, " +
                                            "caso_marcas.CasoTitular, " +
                                            "caso_marcas.CasoNumConcedida " +   
                                        "FROM "+
                                            "caso_marcas, "+
                                            "cliente, "+
                                            "casocliente, "+
                                            "interesado, "+
                                            "casointeresado, "+
                                            "casoproductos, "+
                                            "direccion, "+
                                            "tiposolicitud " +
                                        "WHERE "+
                                            "caso_marcas.CasoId = " +sCasoId+
                                            " AND caso_marcas.CasoId = casocliente.CasoId "+
                                            "AND caso_marcas.CasoId = casointeresado.CasoId "+
                                            "AND casointeresado.InteresadoId = interesado.InteresadoID "+
                                            "AND casocliente.ClienteId = cliente.ClienteId "+
                                            "AND caso_marcas.CasoId = casoproductos.CasoId "+ //
                                            "AND (cliente.ClienteId = direccion.ClienteId or interesado.InteresadoID = direccion.InteresadoId) "+
                                            "AND  caso_marcas.TipoSolicitudId = tiposolicitud.TipoSolicitudId "+
                                            "LIMIT 1";


                    MySqlDataReader respuestastringanex = condoc.getdatareader(querycartapru);



                    object abrirDoc8 = sinirutacartas + sIdcarta;


                    object readOnly8 = false;
                    object isVisible9 = false;

                    aDoc = wordApp.Documents.Open(ref abrirDoc8, ref isVisible9, ref readOnly8,
                                                ref missing8, ref missing8, ref missing8,
                                                ref missing8, ref missing8, ref missing8,
                                                ref missing8, ref missing8, ref missing8,
                                                ref missing8, ref missing8, ref missing8, ref missing8);



                    String producto_servicio = ""; String fechaconcecion="",  fechavigencia="", fechapruebauso="", fecharecepcion ="", titular="", sinititulo="", sinicasonumenro=""; 

                    while (respuestastringanex.Read())
                    {
                        //primer parametro es el texto, el segundo parametro es el id o valor y el tercer parametro es el mysqldatareader
                        this.FindAndReplace(wordApp, "«CLIENTENOMBRE»", validareader("ClienteNombre", "CasoId", respuestastringanex).Text);
                        this.FindAndReplace(wordApp, "«ClienteDireccion»", validareader("DireccionCalle", "CasoId", respuestastringanex).Text + " " +
                              validareader("DireccionNumExt", "CasoId", respuestastringanex).Text + " " + validareader("DireccionNumInt", "CasoId", respuestastringanex).Text + " " +
                              validareader("DireccionColonia", "CasoId", respuestastringanex).Text + " " + validareader("DireccionPoblacion", "CasoId", respuestastringanex).Text + " " +
                             validareader("DireccionEstado", "CasoId", respuestastringanex).Text);
                        
                       
                        
                        this.FindAndReplace(wordApp, "«ClienteDireccion»", validareader("ClienteNombre", "CasoId", respuestastringanex).Text);
                        this.FindAndReplace(wordApp, "«AsuntoE»", validareader("TipoSolicitudDescrip", "CasoId", respuestastringanex).Text +
                             " N° " + validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastringanex).Text);
                        this.FindAndReplace(wordApp, "«Titulo»", validareader("CasoTituloespanol", "CasoId", respuestastringanex).Text);
                        this.FindAndReplace(wordApp, "«TituloIngles»", validareader("CasoTituloingles", "CasoId", respuestastringanex).Text);
                        this.FindAndReplace(wordApp, "«CLASE»", validareader("CasoProductosClase", "CasoId", respuestastringanex).Text);
                        
                        this.FindAndReplace(wordApp, "«ReferenciaDespacho»", validareader("CasoNumero", "CasoId", respuestastringanex).Text);
                        titular = validareader("CasoTitular", "CasoId", respuestastringanex).Text;
                        if (titular.Equals("")) {
                            this.FindAndReplace(wordApp, "«TitularNombre»", validareader("InteresadoNombre", "CasoId", respuestastringanex).Text + " " +
                           validareader("InteresadoApPaterno", "CasoId", respuestastringanex).Text + " " + validareader("InteresadoApMaterno", "CasoId", respuestastringanex).Text);
                        }
                        else {
                            this.FindAndReplace(wordApp, "«TitularNombre»", titular);
                        }
                        producto_servicio = validareader("CasoProductosClase", "CasoId", respuestastringanex).Text;
                        fechaconcecion = validareader("CasoFechaConcesion", "CasoId", respuestastringanex).Text;
                        fechavigencia = validareader("CasoFechaVigencia", "CasoId", respuestastringanex).Text;
                        fechapruebauso = validareader("CasoFechaprobouso", "CasoId", respuestastringanex).Text;
                        fecharecepcion = validareader("CasoFechaRecepcion", "CasoId", respuestastringanex).Text;
                        this.FindAndReplace(wordApp, "«RegistroDatoEspanol»", "Registro N° " + validareader("CasoNumConcedida", "CasoId", respuestastringanex).Text);
                        this.FindAndReplace(wordApp, "«PRODUCTOS»", validareader("CasoProductosDescripcion", "CasoId", respuestastringanex).Text);
                        this.FindAndReplace(wordApp, "«TipoSolicitudEspanol»", validareader("TipoSolicitudDescrip", "CasoId", respuestastringanex).Text);

                        this.FindAndReplace(wordApp, "«Asunto»", validareader("TipoSolicitudDescripI", "CasoId", respuestastringanex).Text +
                            " N° " + validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastringanex).Text);

                        this.FindAndReplace(wordApp, "«TituloRegistroDatoEspanol»", validareader("TipoSolicitudDescrip", "CasoId", respuestastringanex).Text + " N° " + validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastringanex).Text);
                        this.FindAndReplace(wordApp, "«TituloRegistroDato»", validareader("TipoSolicitudDescripI", "CasoId", respuestastringanex).Text + " N° " + validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastringanex).Text);

                        sinititulo = validareader("CasoTituloespanol", "CasoId", respuestastringanex).Text;
                        sinicasonumenro = validareader("CasoNumero", "CasoId", respuestastringanex).Text;

                    }

                    respuestastringanex.Close();

                    if (sinititulo.Length > 14)
                    {

                        String nombrecarpeta = sinititulo.Substring(0, 15);
                        srutacarpeta = Sinicioruta + nombrecarpeta + "_" + sinicasonumenro;
                        System.IO.Directory.CreateDirectory(srutacarpeta);

                    }
                    else
                    {

                        String nombrecarpeta = sinititulo;
                        srutacarpeta = Sinicioruta + nombrecarpeta + "_" + sinicasonumenro;
                        System.IO.Directory.CreateDirectory(srutacarpeta);
                    }


                                       
                    Random random8 = new Random();
                    int randomNumber8 = random8.Next(0, 100);

                    object fileNametres8 = srutacarpeta + "\\Carta" + sIdcarta + randomNumber8 + "_" + sCasoId + ".doc";
                    object fileName8 = srutacarpeta + "\\Carta" + sIdcarta + randomNumber8 + "_" + sCasoId + ".pdf";



                    String querycartapru2 = "SELECT * FROM referencia where referencia.CasoId =  " +sCasoId+";";
                    MySqlDataReader respuestastringanex2 = condoc.getdatareader(querycartapru2);
                        while (respuestastringanex2.Read())
                        {
                            this.FindAndReplace(wordApp, "«ReferenciaCliente»", validareader("ReferenciaNombre", "ReferenciaId", respuestastringanex2).Text);
                        }

                        this.FindAndReplace(wordApp, "«ReferenciaCliente»", "");

                        respuestastringanex2.Close();

                        String querycartapru3 = "SELECT  usuario.UsuarioClave, usuario.UsuarioId FROM caso_marcas, usuario where caso_marcas.CasoId = "+sCasoId+" AND caso_marcas.UsuarioId = usuario.UsuarioId; ";
                        MySqlDataReader respuestastringanex3 = condoc.getdatareader(querycartapru3);
                            while (respuestastringanex3.Read())
                        {
                            this.FindAndReplace(wordApp, "«Iniciales»", validareader("UsuarioClave", "UsuarioId", respuestastringanex3).Text);
                        }

                    this.FindAndReplace(wordApp, "«Iniciales»", "");
                                respuestastringanex3.Close();


                        if (!fecharecepcion.Equals(""))
                        {
                            String fechadiarecepcion = fecharecepcion.Substring(0, 2);
                            String fechamesrecepcion = fecharecepcion.Substring(3, 2);
                            String fechamesrecepcioni = fecharecepcion.Substring(3, 2);
                            String fechaanorecepcion = fecharecepcion.Substring(6, 4);

                            switch (fechamesrecepcion)
                            {
                                case "01":
                                    fechamesrecepcion = "Enero";
                                    break;
                                case "02":
                                    fechamesrecepcion = "Febrero";
                                    break;
                                case "03":
                                    fechamesrecepcion = "Marzo";
                                    break;
                                case "04":
                                    fechamesrecepcion = "Abril";
                                    break;
                                case "05":
                                    fechamesrecepcion = "Mayo";
                                    break;
                                case "06":
                                    fechamesrecepcion = "Junio";
                                    break;
                                case "07":
                                    fechamesrecepcion = "Julio";
                                    break;
                                case "08":
                                    fechamesrecepcion = "Agosto";
                                    break;
                                case "09":
                                    fechamesrecepcion = "Septiembre";
                                    break;
                                case "10":
                                    fechamesrecepcion = "Octubre";
                                    break;
                                case "11":
                                    fechamesrecepcion = "Noviembre";
                                    break;
                                case "12":
                                    fechamesrecepcion = "Diciembre";
                                    break;

                            }

                            switch (fechamesrecepcioni)
                            {
                                case "01":
                                    fechamesrecepcioni = "January";
                                    break;
                                case "02":
                                    fechamesrecepcioni = "February";
                                    break;
                                case "03":
                                    fechamesrecepcioni = "March";
                                    break;
                                case "04":
                                    fechamesrecepcioni = "April";
                                    break;
                                case "05":
                                    fechamesrecepcioni = "May";
                                    break;
                                case "06":
                                    fechamesrecepcioni = "June";
                                    break;
                                case "07":
                                    fechamesrecepcioni = "July";
                                    break;
                                case "08":
                                    fechamesrecepcioni = "August";
                                    break;
                                case "09":
                                    fechamesrecepcioni = "September";
                                    break;
                                case "10":
                                    fechamesrecepcioni = "October";
                                    break;
                                case "11":
                                    fechamesrecepcioni = "November";
                                    break;
                                case "12":
                                    fechamesrecepcioni = "December";
                                    break;

                            }
                            String FechaLegalEspanol = fechadiarecepcion + " de " + fechamesrecepcion + " de " + fechaanorecepcion;
                            String FechaLegalIngles = fechamesrecepcioni + " " + fechadiarecepcion + ", " + fechaanorecepcion;
                            this.FindAndReplace(wordApp, "«FechaLegalEspanol»", FechaLegalEspanol);
                            this.FindAndReplace(wordApp, "«FechaLegalIngles»", FechaLegalIngles);
                            this.FindAndReplace(wordApp, "«FechaPresentacionE»", FechaLegalEspanol);
                            this.FindAndReplace(wordApp, "«FechaPresentacionI»", FechaLegalIngles);


                        }
                        else
                        {
                            this.FindAndReplace(wordApp, "«FechaLegalEspanol»", "");
                            this.FindAndReplace(wordApp, "«FechaLegalIngles»", "");
                            this.FindAndReplace(wordApp, "«FechaPresentacionE»", "");
                            this.FindAndReplace(wordApp, "«FechaPresentacionI»", "");

                        }
                    // FECHA 1

                        if (!fechapruebauso.Equals(""))
                        {
                            String fechadiapruebauso = fechapruebauso.Substring(0, 2);
                            String fechamespruebauso = fechapruebauso.Substring(3, 2);
                            String fechamespruebausoi = fechapruebauso.Substring(3, 2);
                            String fechaanopruebauso = fechapruebauso.Substring(6, 4);

                            switch (fechamespruebauso)
                            {
                                case "01":
                                    fechamespruebauso = "Enero";
                                    break;
                                case "02":
                                    fechamespruebauso = "Febrero";
                                    break;
                                case "03":
                                    fechamespruebauso = "Marzo";
                                    break;
                                case "04":
                                    fechamespruebauso = "Abril";
                                    break;
                                case "05":
                                    fechamespruebauso = "Mayo";
                                    break;
                                case "06":
                                    fechamespruebauso = "Junio";
                                    break;
                                case "07":
                                    fechamespruebauso = "Julio";
                                    break;
                                case "08":
                                    fechamespruebauso = "Agosto";
                                    break;
                                case "09":
                                    fechamespruebauso = "Septiembre";
                                    break;
                                case "10":
                                    fechamespruebauso = "Octubre";
                                    break;
                                case "11":
                                    fechamespruebauso = "Noviembre";
                                    break;
                                case "12":
                                    fechamespruebauso = "Diciembre";
                                    break;

                            }

                            switch (fechamespruebausoi)
                            {
                                case "01":
                                    fechamespruebausoi = "January";
                                    break;
                                case "02":
                                    fechamespruebausoi = "February";
                                    break;
                                case "03":
                                    fechamespruebausoi = "March";
                                    break;
                                case "04":
                                    fechamespruebausoi = "April";
                                    break;
                                case "05":
                                    fechamespruebausoi = "May";
                                    break;
                                case "06":
                                    fechamespruebausoi = "June";
                                    break;
                                case "07":
                                    fechamespruebausoi = "July";
                                    break;
                                case "08":
                                    fechamespruebausoi = "August";
                                    break;
                                case "09":
                                    fechamespruebausoi = "September";
                                    break;
                                case "10":
                                    fechamespruebausoi = "October";
                                    break;
                                case "11":
                                    fechamespruebausoi = "November";
                                    break;
                                case "12":
                                    fechamespruebausoi = "December";
                                    break;

                            }
                            String FechaProbarUsoE = fechadiapruebauso + " de " + fechamespruebauso + " de " + fechaanopruebauso;
                            String FechaProbarUsoI = fechamespruebausoi + " " + fechadiapruebauso + ", " + fechaanopruebauso;
                            this.FindAndReplace(wordApp, "«FechaProbarUsoE»", FechaProbarUsoE);
                            this.FindAndReplace(wordApp, "«FechaProbarUsoI»", FechaProbarUsoI);


                        }
                        else
                        {
                            this.FindAndReplace(wordApp, "«FechaProbarUsoE»", "");
                            this.FindAndReplace(wordApp, "«FechaProbarUsoI»", "");
                        }

                    ///FECHA2


                    if (!fechavigencia.Equals(""))
                    {
                        String fechadiavigencia = fechavigencia.Substring(0, 2);
                        String fechamesvigencia = fechavigencia.Substring(3, 2);
                        String fechamesvigenciani = fechavigencia.Substring(3, 2);
                        String fechaanovigencia = fechavigencia.Substring(6, 4);

                   switch (fechamesvigencia)
                    {
                        case "01":
                            fechamesvigencia = "Enero";
                            break;
                        case "02":
                            fechamesvigencia = "Febrero";
                            break;
                        case "03":
                            fechamesvigencia = "Marzo";
                            break;
                        case "04":
                            fechamesvigencia = "Abril";
                            break;
                        case "05":
                            fechamesvigencia = "Mayo";
                            break;
                        case "06":
                            fechamesvigencia = "Junio";
                            break;
                        case "07":
                            fechamesvigencia = "Julio";
                            break;
                        case "08":
                            fechamesvigencia = "Agosto";
                            break;
                        case "09":
                            fechamesvigencia = "Septiembre";
                            break;
                        case "10":
                            fechamesvigencia = "Octubre";
                            break;
                        case "11":
                            fechamesvigencia = "Noviembre";
                            break;
                        case "12":
                            fechamesvigencia = "Diciembre";
                            break;
                       
                    }

                    switch (fechamesvigenciani)
                    {
                        case "01":
                            fechamesvigenciani = "January";
                            break;
                        case "02":
                            fechamesvigenciani = "February";
                            break;
                        case "03":
                            fechamesvigenciani = "March";
                            break;
                        case "04":
                            fechamesvigenciani = "April";
                            break;
                        case "05":
                            fechamesvigenciani = "May";
                            break;
                        case "06":
                            fechamesvigenciani = "June";
                            break;
                        case "07":
                            fechamesvigenciani = "July";
                            break;
                        case "08":
                            fechamesvigenciani = "August";
                            break;
                        case "09":
                            fechamesvigenciani = "September";
                            break;
                        case "10":
                            fechamesvigenciani = "October";
                            break;
                        case "11":
                            fechamesvigenciani = "November";
                            break;
                        case "12":
                            fechamesvigenciani = "December";
                            break;

                    }
                        String FechaVigenciaE = fechadiavigencia + " de " + fechamesvigencia + " de " + fechaanovigencia;
                        String FechaVigenciaI = fechamesvigenciani + " " + fechadiavigencia + ", " + fechaanovigencia;
                        this.FindAndReplace(wordApp, "«FechaVigenciaE»", FechaVigenciaE);
                        this.FindAndReplace(wordApp, "«FechaVigenciaI»", FechaVigenciaI);


                    }else{
                        this.FindAndReplace(wordApp, "«FechaVigenciaE»", "");
                        this.FindAndReplace(wordApp, "«FechaVigenciaI»", "");
                    }
                    /// FECHA 3



                    if (!fechaconcecion.Equals(""))
                    {
                        String fechadiaconcecion = fechaconcecion.Substring(0, 2);
                        String fechamesconcecion = fechaconcecion.Substring(3, 2);
                        String fechamesconcecioni = fechaconcecion.Substring(3, 2);
                        String fechaanoconcecion = fechaconcecion.Substring(6, 4);

                   switch (fechamesconcecion)
                    {
                        case "01":
                            fechamesconcecion = "Enero";
                            break;
                        case "02":
                            fechamesconcecion = "Febrero";
                            break;
                        case "03":
                            fechamesconcecion = "Marzo";
                            break;
                        case "04":
                            fechamesconcecion = "Abril";
                            break;
                        case "05":
                            fechamesconcecion = "Mayo";
                            break;
                        case "06":
                            fechamesconcecion = "Junio";
                            break;
                        case "07":
                            fechamesconcecion = "Julio";
                            break;
                        case "08":
                            fechamesconcecion = "Agosto";
                            break;
                        case "09":
                            fechamesconcecion = "Septiembre";
                            break;
                        case "10":
                            fechamesconcecion = "Octubre";
                            break;
                        case "11":
                            fechamesconcecion = "Noviembre";
                            break;
                        case "12":
                            fechamesconcecion = "Diciembre";
                            break;
                       
                    }

                    switch (fechamesconcecioni)
                    {
                        case "01":
                            fechamesconcecioni = "January";
                            break;
                        case "02":
                            fechamesconcecioni = "February";
                            break;
                        case "03":
                            fechamesconcecioni = "March";
                            break;
                        case "04":
                            fechamesconcecioni = "April";
                            break;
                        case "05":
                            fechamesconcecioni = "May";
                            break;
                        case "06":
                            fechamesconcecioni = "June";
                            break;
                        case "07":
                            fechamesconcecioni = "July";
                            break;
                        case "08":
                            fechamesconcecioni = "August";
                            break;
                        case "09":
                            fechamesconcecioni = "September";
                            break;
                        case "10":
                            fechamesconcecioni = "October";
                            break;
                        case "11":
                            fechamesconcecioni = "November";
                            break;
                        case "12":
                            fechamesconcecioni = "December";
                            break;

                    }
                        String FechaConcesionE = fechadiaconcecion +" de "+fechamesconcecion +" de " +fechaanoconcecion;
                        String FechaConcesionI = fechamesconcecioni + " "+fechadiaconcecion +", "+fechadiaconcecion;
                        this.FindAndReplace(wordApp, "«FechaConcesionE»", FechaConcesionE );
                        this.FindAndReplace(wordApp, "«FechaConcesionI»", FechaConcesionI );


                    }else{
                        this.FindAndReplace(wordApp, "«FechaConcesionE»", "" );
                        this.FindAndReplace(wordApp, "«FechaConcesionI»", "");

                    }

                    if(!producto_servicio.Equals("")){
                        
                        int numval = Int32.Parse(producto_servicio);
                        if (numval > 34)
                        {
                            this.FindAndReplace(wordApp, "«TipoClaseEspanol»", "Servicios");
                            this.FindAndReplace(wordApp, "«TipoClaseIngles»", "Services");

                        }
                        else
                        {
                            this.FindAndReplace(wordApp, "«TipoClaseEspanol»", "Productos");
                            this.FindAndReplace(wordApp, "«TipoClaseIngles»", "Products");

                        }
                    }


                     fecha = DateTime.Now.ToString("MM/dd/yyyy");
                     fechamesreporte = fecha.Substring(0, 2);
                     fechadiareporte = fecha.Substring(3, 2);
                     fechaanoreporte = fecha.Substring(6, 4);
                     fechamesreportei = fecha.Substring(0, 2);


                    switch (fechamesreporte)
                    {
                        case "01":
                            fechamesreporte = "Enero";
                            break;
                        case "02":
                            fechamesreporte = "Febrero";
                            break;
                        case "03":
                            fechamesreporte = "Marzo";
                            break;
                        case "04":
                            fechamesreporte = "Abril";
                            break;
                        case "05":
                            fechamesreporte = "Mayo";
                            break;
                        case "06":
                            fechamesreporte = "Junio";
                            break;
                        case "07":
                            fechamesreporte = "Julio";
                            break;
                        case "08":
                            fechamesreporte = "Agosto";
                            break;
                        case "09":
                            fechamesreporte = "Septiembre";
                            break;
                        case "10":
                            fechamesreporte = "Octubre";
                            break;
                        case "11":
                            fechamesreporte = "Noviembre";
                            break;
                        case "12":
                            fechamesreporte = "Diciembre";
                            break;
                       
                    }

                    switch (fechamesreportei)
                    {
                        case "01":
                            fechamesreportei = "January";
                            break;
                        case "02":
                            fechamesreportei = "February";
                            break;
                        case "03":
                            fechamesreportei = "March";
                            break;
                        case "04":
                            fechamesreportei = "April";
                            break;
                        case "05":
                            fechamesreportei = "May";
                            break;
                        case "06":
                            fechamesreportei = "June";
                            break;
                        case "07":
                            fechamesreportei = "July";
                            break;
                        case "08":
                            fechamesreportei = "August";
                            break;
                        case "09":
                            fechamesreportei = "September";
                            break;
                        case "10":
                            fechamesreportei = "October";
                            break;
                        case "11":
                            fechamesreportei = "November";
                            break;
                        case "12":
                            fechamesreportei = "December";
                            break;

                    }
                    String Fechareportespanol = fechadiareporte + " de " + fechamesreporte + " de " + fechaanoreporte;
                    String Fechareporteingles =  fechamesreportei + " "+fechadiareporte +", "+ fechaanoreporte;

                    this.FindAndReplace(wordApp, "«FechaReporteEspanol»", Fechareportespanol);
                    this.FindAndReplace(wordApp, "«FechaReporte»", Fechareporteingles);

                    Object fileformat8 = Word.WdSaveFormat.wdFormatPDF;
                    Object SaveChange8 = Word.WdSaveOptions.wdDoNotSaveChanges;
                    Object OrianalForamt8 = Word.WdOriginalFormat.wdOriginalDocumentFormat;

                    aDoc.Activate();
                    aDoc.SaveAs2(ref fileNametres8,
                                    ref missing8, ref missing8, ref missing8, ref missing8, ref missing8,
                                    ref missing8, ref missing8, ref missing8, ref missing8, ref missing8,
                                    ref missing8, ref missing8, ref missing8, ref missing8, ref missing8);


                    aDoc.SaveAs(ref fileName8, ref fileformat8, ref missing8, ref missing8, ref missing8, ref missing8,
                                ref missing8, ref missing8, ref missing8, ref missing8, ref missing8, ref missing8, ref missing8, ref missing8, ref missing8, ref missing8);
                    aDoc.Saved = true;

                    wordApp.Quit(ref missing8, ref missing8, ref missing8);
                    Process.Start(fileNametres8.ToString());
                    Process.Start(fileName8.ToString());

                    break;
                case "3":
                    //statements CONTENCIOSO 2
                    
                    object missingcont = Missing.Value;
                    wordApp.Quit(ref missingcont, ref missingcont, ref missingcont);
                    wordApp = new Word.Application();
                    aDoc = null;
                    String querycartacont = "SELECT " +
                                                "caso_contencioso.CasoId, " +
                                                "cliente.ClienteNombre, " +
                                                "caso_contencioso.CasoTituloespanol, " +
                                                "caso_contencioso.CasoTituloingles, " +
                                                "interesado.InteresadoNombre, " +
                                                "interesado.InteresadoApPaterno, " +
                                                "interesado.InteresadoApMaterno, " +
                                                "caso_contencioso.CasoNumeroExpedienteLargo, " +
                                                "caso_contencioso.TipoSolicitudId, " +
                                                "caso_contencioso.CasoNumero, " +
                                                "direccion.DireccionCalle, " +
                                                "direccion.DireccionNumExt, " +
                                                "direccion.DireccionNumInt, " +
                                                "direccion.DireccionColonia, " +
                                                "direccion.DireccionPoblacion, " +
                                                "direccion.DireccionEstado, " +
                                                "direccion.PaisId, " +
                                                "direccion.DireccionCP, " +
                                                "tiposolicitud.TipoSolicitudDescrip, " +
                                                "tiposolicitud.TipoSolicitudDescripI, " +
                                                "casointeresado.CasoInteresadoSecuencia, " +
                                                "caso_contencioso.CasoTitular, " +
                                                "DATE_FORMAT(caso_contencioso.CasoFechaPresentacion,'%d-%m-%Y') AS CasoFechaPresentacion, " +
                                                "casointeresado.TipoRelacionId " +
                                            "FROM " +
                                                "caso_contencioso, " +
                                                "cliente, " +
                                                "casocliente, " +
                                                "interesado, " +
                                                "casointeresado, " +
                                                "direccion, " +
                                                "tiposolicitud " +
                                            "WHERE " +
                                                "caso_contencioso.CasoId = " + sCasoId +
                                                 " AND caso_contencioso.CasoId = casocliente.CasoId " +
                                                 "AND caso_contencioso.CasoId = casointeresado.CasoId " +
                                                 "AND casointeresado.InteresadoId = interesado.InteresadoID " +
                                                 "AND casocliente.ClienteId = cliente.ClienteId " +
                                                 "AND (cliente.ClienteId = direccion.ClienteId " +
                                                 "OR interesado.InteresadoID = direccion.InteresadoId) " +
                                                 "AND caso_contencioso.TipoSolicitudId = tiposolicitud.TipoSolicitudId " +
                                                 "AND casointeresado.TipoRelacionId = 1 " +
                                                 "ORDER BY CasoInteresadoSecuencia ASC limit 1;";
                    MySqlDataReader respuestastringcont = condoc.getdatareader(querycartacont);

                    object abrirDoccont = sinirutacartas + sIdcarta;
                    object readOnlycont = false;
                    object isVisiblecont = false;

                    aDoc = wordApp.Documents.Open(ref abrirDoccont, ref isVisiblecont, ref readOnlycont,
                                                ref missingcont, ref missingcont, ref missingcont,
                                                ref missingcont, ref missingcont, ref missingcont,
                                                ref missingcont, ref missingcont, ref missingcont,
                                                ref missingcont, ref missingcont, ref missingcont, ref missingcont);



                    String  titularcont="", casofechapresentacioncont ="", sinitituloopsicioncont="", sininumerocasooposicioncont="";
                    while (respuestastringcont.Read())
                    {
                        //primer parametro es el texto, el segundo parametro es el id o valor y el tercer parametro es el mysqldatareader
                        this.FindAndReplace(wordApp, "«CLIENTENOMBRE»", validareader("ClienteNombre", "CasoId", respuestastringcont).Text);
                        this.FindAndReplace(wordApp, "«ClienteDireccion»", validareader("DireccionCalle", "CasoId", respuestastringcont).Text + " " +
                              validareader("DireccionNumExt", "CasoId", respuestastringcont).Text + " " + validareader("DireccionNumInt", "CasoId", respuestastringcont).Text + " " +
                              validareader("DireccionColonia", "CasoId", respuestastringcont).Text + " " + validareader("DireccionPoblacion", "CasoId", respuestastringcont).Text + " " +
                             validareader("DireccionEstado", "CasoId", respuestastringcont).Text);



                        this.FindAndReplace(wordApp, "«ClienteDireccion»", validareader("ClienteNombre", "CasoId", respuestastringcont).Text);
                        this.FindAndReplace(wordApp, "«AsuntoE»", validareader("TipoSolicitudDescrip", "CasoId", respuestastringcont).Text +
                             " N° " + validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastringcont).Text);
                        this.FindAndReplace(wordApp, "«Titulo»", validareader("CasoTituloespanol", "CasoId", respuestastringcont).Text);
                        this.FindAndReplace(wordApp, "«TituloIngles»", validareader("CasoTituloingles", "CasoId", respuestastringcont).Text);

                        this.FindAndReplace(wordApp, "«ReferenciaDespacho»", validareader("CasoNumero", "CasoId", respuestastringcont).Text);
                        titularcont = validareader("CasoTitular", "CasoId", respuestastringcont).Text;
                        if (titularcont.Equals(""))
                        {
                            this.FindAndReplace(wordApp, "«TitularNombre»", validareader("InteresadoNombre", "CasoId", respuestastringcont).Text + " " +
                           validareader("InteresadoApPaterno", "CasoId", respuestastringcont).Text + " " + validareader("InteresadoApMaterno", "CasoId", respuestastringcont).Text);
                        }
                        else
                        {
                            this.FindAndReplace(wordApp, "«TitularNombre»", titularcont);
                        }

                        casofechapresentacioncont = validareader("CasoFechaPresentacion", "CasoId", respuestastringcont).Text;

                        sinitituloopsicioncont = validareader("CasoFechaPresentacion", "CasoId", respuestastringcont).Text;
                        sininumerocasooposicioncont = validareader("CasoNumero", "CasoId", respuestastringcont).Text;

                        this.FindAndReplace(wordApp, "«TipoSolicitudEspanol»", validareader("TipoSolicitudDescrip", "CasoId", respuestastringcont).Text);

                        this.FindAndReplace(wordApp, "«Asunto»", validareader("TipoSolicitudDescripI", "CasoId", respuestastringcont).Text +
                            " N° " + validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastringcont).Text);

                        this.FindAndReplace(wordApp, "«TituloRegistroDatoEspanol»", validareader("TipoSolicitudDescrip", "CasoId", respuestastringcont).Text + " N° " + validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastringcont).Text);
                        this.FindAndReplace(wordApp, "«TituloRegistroDato»", validareader("TipoSolicitudDescripI", "CasoId", respuestastringcont).Text + " N° " + validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastringcont).Text);
                    }

                    respuestastringcont.Close();

                    if (sinitituloopsicioncont.Length > 14)
                    {

                        String nombrecarpeta = sinitituloopsicioncont.Substring(0, 15);
                        srutacarpeta = Sinicioruta + nombrecarpeta + "_" + sininumerocasooposicioncont;
                        System.IO.Directory.CreateDirectory(srutacarpeta);

                    }
                    else
                    {

                        String nombrecarpeta = sinitituloopsicioncont;
                        srutacarpeta = Sinicioruta + nombrecarpeta + "_" + sininumerocasooposicioncont;
                        System.IO.Directory.CreateDirectory(srutacarpeta);
                    }



                    Random randomcont = new Random();
                    int randomNumbercont = randomcont.Next(0, 100);

                    object fileNametrescont = srutacarpeta + "\\Carta" + sIdcarta + randomNumbercont + "_" + sCasoId + ".doc";
                    object fileNamecont = srutacarpeta + "\\Carta" + sIdcarta + randomNumbercont + "_" + sCasoId + ".pdf";


                    String querycartaprucont = "SELECT * FROM referencia where referencia.CasoId =  " +sCasoId+";";
                    MySqlDataReader respuestastringanexcont = condoc.getdatareader(querycartaprucont);
                    while (respuestastringanexcont.Read())
                        {
                            this.FindAndReplace(wordApp, "«ReferenciaCliente»", validareader("ReferenciaNombre", "ReferenciaId", respuestastringanexcont).Text);
                        }

                        this.FindAndReplace(wordApp, "«ReferenciaCliente»", "");

                        respuestastringanexcont.Close();

                        String querycartapru3cont = "SELECT  usuario.UsuarioClave, usuario.UsuarioId FROM caso_marcas, usuario where caso_marcas.CasoId = "+sCasoId+" AND caso_marcas.UsuarioId = usuario.UsuarioId; ";
                        MySqlDataReader respuestastringanex3cont = condoc.getdatareader(querycartapru3cont);
                        while (respuestastringanex3cont.Read())
                        {
                            this.FindAndReplace(wordApp, "«Iniciales»", validareader("UsuarioClave", "UsuarioId", respuestastringanex3cont).Text);
                        }
                        this.FindAndReplace(wordApp, "«Iniciales»", "");

                        respuestastringanex3cont.Close();


                    //fecha caso fecha presentacion


                        if (!casofechapresentacioncont.Equals(""))
                        {
                            String fechadiapresentacion4 = casofechapresentacioncont.Substring(0, 2);
                            String fechamespresentacion4 = casofechapresentacioncont.Substring(3, 2);
                            String fechamespresentacion4i = casofechapresentacioncont.Substring(3, 2);
                            String fechaanopresentacion4 = casofechapresentacioncont.Substring(6, 4);

                            switch (fechamespresentacion4)
                            {
                                case "01":
                                    fechamespresentacion4 = "Enero";
                                    break;
                                case "02":
                                    fechamespresentacion4 = "Febrero";
                                    break;
                                case "03":
                                    fechamespresentacion4 = "Marzo";
                                    break;
                                case "04":
                                    fechamespresentacion4 = "Abril";
                                    break;
                                case "05":
                                    fechamespresentacion4 = "Mayo";
                                    break;
                                case "06":
                                    fechamespresentacion4 = "Junio";
                                    break;
                                case "07":
                                    fechamespresentacion4 = "Julio";
                                    break;
                                case "08":
                                    fechamespresentacion4 = "Agosto";
                                    break;
                                case "09":
                                    fechamespresentacion4 = "Septiembre";
                                    break;
                                case "10":
                                    fechamespresentacion4 = "Octubre";
                                    break;
                                case "11":
                                    fechamespresentacion4 = "Noviembre";
                                    break;
                                case "12":
                                    fechamespresentacion4 = "Diciembre";
                                    break;

                            }

                            switch (fechamespresentacion4i)
                            {
                                case "01":
                                    fechamespresentacion4i = "January";
                                    break;
                                case "02":
                                    fechamespresentacion4i = "February";
                                    break;
                                case "03":
                                    fechamespresentacion4i = "March";
                                    break;
                                case "04":
                                    fechamespresentacion4i = "April";
                                    break;
                                case "05":
                                    fechamespresentacion4i = "May";
                                    break;
                                case "06":
                                    fechamespresentacion4i = "June";
                                    break;
                                case "07":
                                    fechamespresentacion4i = "July";
                                    break;
                                case "08":
                                    fechamespresentacion4i = "August";
                                    break;
                                case "09":
                                    fechamespresentacion4i = "September";
                                    break;
                                case "10":
                                    fechamespresentacion4i = "October";
                                    break;
                                case "11":
                                    fechamespresentacion4i = "November";
                                    break;
                                case "12":
                                    fechamespresentacion4i = "December";
                                    break;

                            }
                            String FechaPresentacionE = fechadiapresentacion4 + " de " + fechamespresentacion4 + " de " + fechaanopresentacion4;
                            String FechaPresentacionI = fechamespresentacion4i + " " + fechadiapresentacion4 + ", " + fechaanopresentacion4;
                            this.FindAndReplace(wordApp, "«FechaPresentacionE»", FechaPresentacionE);
                            this.FindAndReplace(wordApp, "«FechaPresentacionI»", FechaPresentacionI);


                        }
                        else
                        {
                            this.FindAndReplace(wordApp, "«FechaPresentacionE»", "");
                            this.FindAndReplace(wordApp, "«FechaPresentacionI»", "");
                        }



                     fecha = DateTime.Now.ToString("MM/dd/yyyy");
                     fechamesreporte = fecha.Substring(0, 2);
                     fechadiareporte = fecha.Substring(3, 2);
                     fechaanoreporte = fecha.Substring(6, 4);
                     fechamesreportei = fecha.Substring(0, 2);


                    switch (fechamesreporte)
                    {
                        case "01":
                            fechamesreporte = "Enero";
                            break;
                        case "02":
                            fechamesreporte = "Febrero";
                            break;
                        case "03":
                            fechamesreporte = "Marzo";
                            break;
                        case "04":
                            fechamesreporte = "Abril";
                            break;
                        case "05":
                            fechamesreporte = "Mayo";
                            break;
                        case "06":
                            fechamesreporte = "Junio";
                            break;
                        case "07":
                            fechamesreporte = "Julio";
                            break;
                        case "08":
                            fechamesreporte = "Agosto";
                            break;
                        case "09":
                            fechamesreporte = "Septiembre";
                            break;
                        case "10":
                            fechamesreporte = "Octubre";
                            break;
                        case "11":
                            fechamesreporte = "Noviembre";
                            break;
                        case "12":
                            fechamesreporte = "Diciembre";
                            break;
                       
                    }

                    switch (fechamesreportei)
                    {
                        case "01":
                            fechamesreportei = "January";
                            break;
                        case "02":
                            fechamesreportei = "February";
                            break;
                        case "03":
                            fechamesreportei = "March";
                            break;
                        case "04":
                            fechamesreportei = "April";
                            break;
                        case "05":
                            fechamesreportei = "May";
                            break;
                        case "06":
                            fechamesreportei = "June";
                            break;
                        case "07":
                            fechamesreportei = "July";
                            break;
                        case "08":
                            fechamesreportei = "August";
                            break;
                        case "09":
                            fechamesreportei = "September";
                            break;
                        case "10":
                            fechamesreportei = "October";
                            break;
                        case "11":
                            fechamesreportei = "November";
                            break;
                        case "12":
                            fechamesreportei = "December";
                            break;

                    }

                    String Fechareportespanolcont = fechadiareporte + " de " + fechamesreporte + " de " + fechaanoreporte;
                    String Fechareporteinglescont =  fechamesreportei + " "+fechadiareporte +", "+ fechaanoreporte;

                    this.FindAndReplace(wordApp, "«FechaReporteEspanol»", Fechareportespanolcont);
                    this.FindAndReplace(wordApp, "«FechaReporte»", Fechareporteinglescont);

                    Object fileformat4 = Word.WdSaveFormat.wdFormatPDF;
                    Object SaveChange4 = Word.WdSaveOptions.wdDoNotSaveChanges;
                    Object OrianalForamt4 = Word.WdOriginalFormat.wdOriginalDocumentFormat;

                    aDoc.Activate();
                    aDoc.SaveAs2(ref fileNametrescont,
                                    ref missingcont, ref missingcont, ref missingcont, ref missingcont, ref missingcont,
                                    ref missingcont, ref missingcont, ref missingcont, ref missingcont, ref missingcont,
                                    ref missingcont, ref missingcont, ref missingcont, ref missingcont, ref missingcont);


                    aDoc.SaveAs(ref fileNamecont, ref fileformat4, ref missingcont, ref missingcont, ref missingcont, ref missingcont,
                                ref missingcont, ref missingcont, ref missingcont, ref missingcont, ref missingcont, ref missingcont, ref missingcont, ref missingcont, ref missingcont, ref missingcont);
                    aDoc.Saved = true;
                    wordApp.Quit(ref missingcont, ref missingcont, ref missingcont);
                    Process.Start(fileNametrescont.ToString());
                    Process.Start(fileNamecont.ToString());

                    break;
                case "4":
                    //statements CONSULTA3
                    break;
                case "5":
                    //statements OPOSICION A SOLICITUDES4
                    object missing4 = Missing.Value;
                    wordApp.Quit(ref missing4, ref missing4, ref missing4);
                    wordApp = new Word.Application();
                    aDoc = null;
                    String querycartapru4 = "SELECT "+
                                                "caso_oposicion.CasoId, "+
                                                "cliente.ClienteNombre, "+
                                                "caso_oposicion.CasoTituloespanol, " +
                                                "caso_oposicion.CasoTituloingles, " +
                                                "interesado.InteresadoNombre, "+
                                                "interesado.InteresadoApPaterno, "+
                                                "interesado.InteresadoApMaterno, "+
                                                "caso_oposicion.CasoNumeroExpedienteLargo, "+
                                                "caso_oposicion.TipoSolicitudId, "+
                                                "caso_oposicion.CasoNumero, "+
                                                "direccion.DireccionCalle, "+
                                                "direccion.DireccionNumExt, "+
                                                "direccion.DireccionNumInt, "+
                                                "direccion.DireccionColonia, "+
                                                "direccion.DireccionPoblacion, "+
                                                "direccion.DireccionEstado, "+
                                                "direccion.PaisId, "+
                                                "direccion.DireccionCP, "+
                                                "tiposolicitud.TipoSolicitudDescrip, "+
                                                "tiposolicitud.TipoSolicitudDescripI, "+
                                                "casointeresado.CasoInteresadoSecuencia, " +
                                                "caso_oposicion.CasoTitular, " +
                                                "DATE_FORMAT(caso_oposicion.CasoFechaPresentacion,'%d-%m-%Y') AS CasoFechaPresentacion, "+
                                                "casointeresado.TipoRelacionId "+
                                            "FROM "+
                                                "caso_oposicion, "+
                                                "cliente, "+
                                                "casocliente, "+
                                                "interesado, "+
                                                "casointeresado, "+
                                                "direccion, "+
                                                "tiposolicitud "+
                                            "WHERE "+
                                                "caso_oposicion.CasoId = "+sCasoId+
                                                 " AND caso_oposicion.CasoId = casocliente.CasoId "+
                                                 "AND caso_oposicion.CasoId = casointeresado.CasoId "+
                                                 "AND casointeresado.InteresadoId = interesado.InteresadoID "+
                                                 "AND casocliente.ClienteId = cliente.ClienteId "+
                                                 "AND (cliente.ClienteId = direccion.ClienteId "+
                                                 "OR interesado.InteresadoID = direccion.InteresadoId) "+
                                                 "AND caso_oposicion.TipoSolicitudId = tiposolicitud.TipoSolicitudId "+
                                                 "AND casointeresado.TipoRelacionId = 1 "+
                                                 "ORDER BY CasoInteresadoSecuencia ASC limit 1;";
                    MySqlDataReader respuestastringanex4 = condoc.getdatareader(querycartapru4);

                    object abrirDoc4 = sinirutacartas + sIdcarta;
                    object readOnly4 = false;
                    object isVisible4 = false;

                    aDoc = wordApp.Documents.Open(ref abrirDoc4, ref isVisible4, ref readOnly4,
                                                ref missing4, ref missing4, ref missing4,
                                                ref missing4, ref missing4, ref missing4,
                                                ref missing4, ref missing4, ref missing4,
                                                ref missing4, ref missing4, ref missing4, ref missing4);



                    String  titular4="", casofechapresentacion4 ="", sinitituloopsicion="", sininumerocasooposicion="";
                    while (respuestastringanex4.Read())
                    {
                        //primer parametro es el texto, el segundo parametro es el id o valor y el tercer parametro es el mysqldatareader
                        this.FindAndReplace(wordApp, "«CLIENTENOMBRE»", validareader("ClienteNombre", "CasoId", respuestastringanex4).Text);
                        this.FindAndReplace(wordApp, "«ClienteDireccion»", validareader("DireccionCalle", "CasoId", respuestastringanex4).Text + " " +
                              validareader("DireccionNumExt", "CasoId", respuestastringanex4).Text + " " + validareader("DireccionNumInt", "CasoId", respuestastringanex4).Text + " " +
                              validareader("DireccionColonia", "CasoId", respuestastringanex4).Text + " " + validareader("DireccionPoblacion", "CasoId", respuestastringanex4).Text + " " +
                             validareader("DireccionEstado", "CasoId", respuestastringanex4).Text);



                        this.FindAndReplace(wordApp, "«ClienteDireccion»", validareader("ClienteNombre", "CasoId", respuestastringanex4).Text);
                        this.FindAndReplace(wordApp, "«AsuntoE»", validareader("TipoSolicitudDescrip", "CasoId", respuestastringanex4).Text +
                             " N° " + validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastringanex4).Text);
                        this.FindAndReplace(wordApp, "«Titulo»", validareader("CasoTituloespanol", "CasoId", respuestastringanex4).Text);
                        this.FindAndReplace(wordApp, "«TituloIngles»", validareader("CasoTituloingles", "CasoId", respuestastringanex4).Text);

                        this.FindAndReplace(wordApp, "«ReferenciaDespacho»", validareader("CasoNumero", "CasoId", respuestastringanex4).Text);
                        titular4 = validareader("CasoTitular", "CasoId", respuestastringanex4).Text;
                        if (titular4.Equals(""))
                        {
                            this.FindAndReplace(wordApp, "«TitularNombre»", validareader("InteresadoNombre", "CasoId", respuestastringanex4).Text + " " +
                           validareader("InteresadoApPaterno", "CasoId", respuestastringanex4).Text + " " + validareader("InteresadoApMaterno", "CasoId", respuestastringanex4).Text);
                        }
                        else
                        {
                            this.FindAndReplace(wordApp, "«TitularNombre»", titular4);
                        }

                        casofechapresentacion4 = validareader("CasoFechaPresentacion", "CasoId", respuestastringanex4).Text;
                       
                        sinitituloopsicion = validareader("CasoFechaPresentacion", "CasoId", respuestastringanex4).Text;
                        sininumerocasooposicion = validareader("CasoNumero", "CasoId", respuestastringanex4).Text;

                        this.FindAndReplace(wordApp, "«TipoSolicitudEspanol»", validareader("TipoSolicitudDescrip", "CasoId", respuestastringanex4).Text);

                        this.FindAndReplace(wordApp, "«Asunto»", validareader("TipoSolicitudDescripI", "CasoId", respuestastringanex4).Text +
                            " N° " + validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastringanex4).Text);

                        this.FindAndReplace(wordApp, "«TituloRegistroDatoEspanol»", validareader("TipoSolicitudDescrip", "CasoId", respuestastringanex4).Text + " N° " + validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastringanex4).Text);
                        this.FindAndReplace(wordApp, "«TituloRegistroDato»", validareader("TipoSolicitudDescripI", "CasoId", respuestastringanex4).Text + " N° " + validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastringanex4).Text);
                    }
                    respuestastringanex4.Close();

                    if (sinitituloopsicion.Length > 14)
                    {

                        String nombrecarpeta = sinitituloopsicion.Substring(0, 15);
                        srutacarpeta = Sinicioruta + nombrecarpeta + "_" + sininumerocasooposicion;
                        System.IO.Directory.CreateDirectory(srutacarpeta);

                    }
                    else
                    {

                        String nombrecarpeta = sinitituloopsicion;
                        srutacarpeta = Sinicioruta + nombrecarpeta + "_" + sininumerocasooposicion;
                        System.IO.Directory.CreateDirectory(srutacarpeta);
                    }



                    Random random4 = new Random();
                    int randomNumber4 = random4.Next(0, 100);

                    object fileNametres4 = srutacarpeta + "\\Carta" + sIdcarta + randomNumber4 + "_" + sCasoId + ".doc";
                    object fileName4 = srutacarpeta + "\\Carta" + sIdcarta + randomNumber4 + "_" + sCasoId + ".pdf";


                    String querycartapru24 = "SELECT * FROM referencia where referencia.CasoId =  " +sCasoId+";";
                    MySqlDataReader respuestastringanex24 = condoc.getdatareader(querycartapru24);
                    while (respuestastringanex24.Read())
                        {
                            this.FindAndReplace(wordApp, "«ReferenciaCliente»", validareader("ReferenciaNombre", "ReferenciaId", respuestastringanex24).Text);
                        }

                        this.FindAndReplace(wordApp, "«ReferenciaCliente»", "");

                        respuestastringanex24.Close();

                        String querycartapru34 = "SELECT  usuario.UsuarioClave, usuario.UsuarioId FROM caso_marcas, usuario where caso_marcas.CasoId = "+sCasoId+" AND caso_marcas.UsuarioId = usuario.UsuarioId; ";
                        MySqlDataReader respuestastringanex34 = condoc.getdatareader(querycartapru34);
                        while (respuestastringanex34.Read())
                        {
                            this.FindAndReplace(wordApp, "«Iniciales»", validareader("UsuarioClave", "UsuarioId", respuestastringanex34).Text);
                        }
                        this.FindAndReplace(wordApp, "«Iniciales»", "");

                        respuestastringanex34.Close();


                    //fecha caso fecha presentacion


                        if (!casofechapresentacion4.Equals(""))
                        {
                            String fechadiapresentacion4 = casofechapresentacion4.Substring(0, 2);
                            String fechamespresentacion4 = casofechapresentacion4.Substring(3, 2);
                            String fechamespresentacion4i = casofechapresentacion4.Substring(3, 2);
                            String fechaanopresentacion4 = casofechapresentacion4.Substring(6, 4);

                            switch (fechamespresentacion4)
                            {
                                case "01":
                                    fechamespresentacion4 = "Enero";
                                    break;
                                case "02":
                                    fechamespresentacion4 = "Febrero";
                                    break;
                                case "03":
                                    fechamespresentacion4 = "Marzo";
                                    break;
                                case "04":
                                    fechamespresentacion4 = "Abril";
                                    break;
                                case "05":
                                    fechamespresentacion4 = "Mayo";
                                    break;
                                case "06":
                                    fechamespresentacion4 = "Junio";
                                    break;
                                case "07":
                                    fechamespresentacion4 = "Julio";
                                    break;
                                case "08":
                                    fechamespresentacion4 = "Agosto";
                                    break;
                                case "09":
                                    fechamespresentacion4 = "Septiembre";
                                    break;
                                case "10":
                                    fechamespresentacion4 = "Octubre";
                                    break;
                                case "11":
                                    fechamespresentacion4 = "Noviembre";
                                    break;
                                case "12":
                                    fechamespresentacion4 = "Diciembre";
                                    break;

                            }

                            switch (fechamespresentacion4i)
                            {
                                case "01":
                                    fechamespresentacion4i = "January";
                                    break;
                                case "02":
                                    fechamespresentacion4i = "February";
                                    break;
                                case "03":
                                    fechamespresentacion4i = "March";
                                    break;
                                case "04":
                                    fechamespresentacion4i = "April";
                                    break;
                                case "05":
                                    fechamespresentacion4i = "May";
                                    break;
                                case "06":
                                    fechamespresentacion4i = "June";
                                    break;
                                case "07":
                                    fechamespresentacion4i = "July";
                                    break;
                                case "08":
                                    fechamespresentacion4i = "August";
                                    break;
                                case "09":
                                    fechamespresentacion4i = "September";
                                    break;
                                case "10":
                                    fechamespresentacion4i = "October";
                                    break;
                                case "11":
                                    fechamespresentacion4i = "November";
                                    break;
                                case "12":
                                    fechamespresentacion4i = "December";
                                    break;

                            }
                            String FechaPresentacionE = fechadiapresentacion4 + " de " + fechamespresentacion4 + " de " + fechaanopresentacion4;
                            String FechaPresentacionI = fechamespresentacion4i + " " + fechadiapresentacion4 + ", " + fechaanopresentacion4;
                            this.FindAndReplace(wordApp, "«FechaPresentacionE»", FechaPresentacionE);
                            this.FindAndReplace(wordApp, "«FechaPresentacionI»", FechaPresentacionI);


                        }
                        else
                        {
                            this.FindAndReplace(wordApp, "«FechaPresentacionE»", "");
                            this.FindAndReplace(wordApp, "«FechaPresentacionI»", "");
                        }



                     fecha = DateTime.Now.ToString("MM/dd/yyyy");
                     fechamesreporte = fecha.Substring(0, 2);
                     fechadiareporte = fecha.Substring(3, 2);
                     fechaanoreporte = fecha.Substring(6, 4);
                     fechamesreportei = fecha.Substring(0, 2);


                    switch (fechamesreporte)
                    {
                        case "01":
                            fechamesreporte = "Enero";
                            break;
                        case "02":
                            fechamesreporte = "Febrero";
                            break;
                        case "03":
                            fechamesreporte = "Marzo";
                            break;
                        case "04":
                            fechamesreporte = "Abril";
                            break;
                        case "05":
                            fechamesreporte = "Mayo";
                            break;
                        case "06":
                            fechamesreporte = "Junio";
                            break;
                        case "07":
                            fechamesreporte = "Julio";
                            break;
                        case "08":
                            fechamesreporte = "Agosto";
                            break;
                        case "09":
                            fechamesreporte = "Septiembre";
                            break;
                        case "10":
                            fechamesreporte = "Octubre";
                            break;
                        case "11":
                            fechamesreporte = "Noviembre";
                            break;
                        case "12":
                            fechamesreporte = "Diciembre";
                            break;
                       
                    }

                    switch (fechamesreportei)
                    {
                        case "01":
                            fechamesreportei = "January";
                            break;
                        case "02":
                            fechamesreportei = "February";
                            break;
                        case "03":
                            fechamesreportei = "March";
                            break;
                        case "04":
                            fechamesreportei = "April";
                            break;
                        case "05":
                            fechamesreportei = "May";
                            break;
                        case "06":
                            fechamesreportei = "June";
                            break;
                        case "07":
                            fechamesreportei = "July";
                            break;
                        case "08":
                            fechamesreportei = "August";
                            break;
                        case "09":
                            fechamesreportei = "September";
                            break;
                        case "10":
                            fechamesreportei = "October";
                            break;
                        case "11":
                            fechamesreportei = "November";
                            break;
                        case "12":
                            fechamesreportei = "December";
                            break;

                    }

                    String Fechareportespanol4 = fechadiareporte + " de " + fechamesreporte + " de " + fechaanoreporte;
                    String Fechareporteingles4 =  fechamesreportei + " "+fechadiareporte +", "+ fechaanoreporte;

                    this.FindAndReplace(wordApp, "«FechaReporteEspanol»", Fechareportespanol4);
                    this.FindAndReplace(wordApp, "«FechaReporte»", Fechareporteingles4);

                    Object fileformatcont = Word.WdSaveFormat.wdFormatPDF;
                    Object SaveChangecont = Word.WdSaveOptions.wdDoNotSaveChanges;
                    Object OrianalForamtcont = Word.WdOriginalFormat.wdOriginalDocumentFormat;

                    aDoc.Activate();
                    aDoc.SaveAs2(ref fileNametres4,
                                    ref missing4, ref missing4, ref missing4, ref missing4, ref missing4,
                                    ref missing4, ref missing4, ref missing4, ref missing4, ref missing4,
                                    ref missing4, ref missing4, ref missing4, ref missing4, ref missing4);


                    aDoc.SaveAs(ref fileName4, ref fileformatcont, ref missing4, ref missing4, ref missing4, ref missing4,
                                ref missing4, ref missing4, ref missing4, ref missing4, ref missing4, ref missing4, ref missing4, ref missing4, ref missing4, ref missing4);
                    aDoc.Saved = true;

                    wordApp.Quit(ref missing4, ref missing4, ref missing4);
                    Process.Start(fileNametres4.ToString());
                    Process.Start(fileName4.ToString());
                    break;
                case "6":
                    //statements VARIEDADES VEGETALES
                    break;
                case "7":
                    //statements DERECHOS DE AUTOR
                    object missing6 = Missing.Value;
                    wordApp.Quit(ref missing6, ref missing6, ref missing6);
                    wordApp = new Word.Application();
                    aDoc = null;
                    String querycartapru6 = "SELECT "+
                                            "caso_registrodeobra.CasoId, "+
                                            "cliente.ClienteNombre, "+
                                            "interesado.InteresadoNombre, "+
                                            "interesado.InteresadoApPaterno, "+
                                            "interesado.InteresadoApMaterno, "+
                                            "caso_registrodeobra.CasoNumeroExpedienteLargo, "+
                                            "caso_registrodeobra.TipoSolicitudId, " +
                                            "caso_registrodeobra.CasoTituloingles, " +
                                            "caso_registrodeobra.CasoTituloespanol, " +
                                            "DATE_FORMAT(caso_registrodeobra.CasoFechaRecepcion, '%d-%m-%Y') AS CasoFechaRecepcion, "+
                                            "DATE_FORMAT(caso_registrodeobra.CasoFechaVigencia, '%d-%m-%Y') AS CasoFechaVigencia, "+
                                            "DATE_FORMAT(caso_registrodeobra.CasoFechaLegal, '%d-%m-%Y') AS CasoFechaLegal, "+
                                            "DATE_FORMAT(caso_registrodeobra.CasoFechaCartaCliente,  '%d-%m-%Y') AS CasoFechaCartaCliente, "+
                                            "caso_registrodeobra.CasoNumero, " +
                                            "caso_registrodeobra.CasoTitular, " +
                                            "direccion.DireccionCalle, "+
                                            "direccion.DireccionNumExt, "+
                                            "direccion.DireccionNumInt, "+
                                            "direccion.DireccionColonia, "+
                                            "direccion.DireccionPoblacion, "+
                                            "direccion.DireccionEstado, "+
                                            "direccion.PaisId, "+
                                            "direccion.DireccionCP, "+
                                            "tiposolicitud.TipoSolicitudDescrip, "+
                                            "tiposolicitud.TipoSolicitudDescripI, "+
                                            "caso_registrodeobra.CasoNumConcedida, "+
                                            "casointeresado.CasoInteresadoSecuencia, "+
                                            "casointeresado.TipoRelacionId "+
                                        "FROM "+
                                            "caso_registrodeobra, "+
                                            "cliente, "+
                                            "casocliente, "+
                                            "interesado, "+
                                            "casointeresado, "+
                                            "direccion, "+
                                            "tiposolicitud "+
                                        "WHERE "+
                                            "caso_registrodeobra.CasoId = "+sCasoId+
                                             "   AND caso_registrodeobra.CasoId = casocliente.CasoId "+
                                             "   AND caso_registrodeobra.CasoId = casointeresado.CasoId "+
                                             "   AND casointeresado.InteresadoId = interesado.InteresadoID "+
                                             "   AND casocliente.ClienteId = cliente.ClienteId "+
                                             "   AND (cliente.ClienteId = direccion.ClienteId "+
                                             "   OR interesado.InteresadoID = direccion.InteresadoId) "+
                                             "   AND caso_registrodeobra.TipoSolicitudId = tiposolicitud.TipoSolicitudId "+
                                             "   AND casointeresado.TipoRelacionId = 1 "+
                                             "   ORDER BY CasoInteresadoSecuencia ASC limit 1;";

                    MySqlDataReader respuestastringanex6 = condoc.getdatareader(querycartapru6);



                    object abrirDoc6 = sinirutacartas + sIdcarta;


                    object readOnly6 = false;
                    object isVisible6 = false;

                    aDoc = wordApp.Documents.Open(ref abrirDoc6, ref isVisible6, ref readOnly6,
                                                ref missing6, ref missing6, ref missing6,
                                                ref missing6, ref missing6, ref missing6,
                                                ref missing6, ref missing6, ref missing6,
                                                ref missing6, ref missing6, ref missing6, ref missing6);



                    String fechacarta6="",  fechavigencia6="", fechalegal6="", fecharecepcion6 ="", titular6="", sinitituloregistro ="", sinicasonumeroregistro="";

                    while (respuestastringanex6.Read())
                    {
                        //primer parametro es el texto, el segundo parametro es el id o valor y el tercer parametro es el mysqldatareader
                        this.FindAndReplace(wordApp, "«CLIENTENOMBRE»", validareader("ClienteNombre", "CasoId", respuestastringanex6).Text);
                        this.FindAndReplace(wordApp, "«ClienteDireccion»", validareader("DireccionCalle", "CasoId", respuestastringanex6).Text + " " +
                              validareader("DireccionNumExt", "CasoId", respuestastringanex6).Text + " " + validareader("DireccionNumInt", "CasoId", respuestastringanex6).Text + " " +
                              validareader("DireccionColonia", "CasoId", respuestastringanex6).Text + " " + validareader("DireccionPoblacion", "CasoId", respuestastringanex6).Text + " " +
                             validareader("DireccionEstado", "CasoId", respuestastringanex6).Text);



                        this.FindAndReplace(wordApp, "«ClienteDireccion»", validareader("ClienteNombre", "CasoId", respuestastringanex6).Text);
                        this.FindAndReplace(wordApp, "«AsuntoE»", validareader("TipoSolicitudDescrip", "CasoId", respuestastringanex6).Text +
                             " N° " + validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastringanex6).Text);
                        this.FindAndReplace(wordApp, "«Titulo»", validareader("CasoTituloespanol", "CasoId", respuestastringanex6).Text);
                        this.FindAndReplace(wordApp, "«TituloIngles»", validareader("CasoTituloingles", "CasoId", respuestastringanex6).Text);


                        this.FindAndReplace(wordApp, "«ReferenciaDespacho»", validareader("CasoNumero", "CasoId", respuestastringanex6).Text);
                        titular6 = validareader("CasoTitular", "CasoId", respuestastringanex6).Text;
                        if (titular6.Equals(""))
                        {
                            this.FindAndReplace(wordApp, "«TitularNombre»", validareader("InteresadoNombre", "CasoId", respuestastringanex6).Text + " " +
                           validareader("InteresadoApPaterno", "CasoId", respuestastringanex6).Text + " " + validareader("InteresadoApMaterno", "CasoId", respuestastringanex6).Text);
                        }
                        else
                        {
                            this.FindAndReplace(wordApp, "«TitularNombre»", titular6);
                        }


                        fechavigencia6 = validareader("CasoFechaVigencia", "CasoId", respuestastringanex6).Text;
                        fechacarta6 = validareader("CasoFechaCartaCliente", "CasoId", respuestastringanex6).Text;
                        fechalegal6 = validareader("CasoFechaLegal", "CasoId", respuestastringanex6).Text;
                    

                        fecharecepcion6 = validareader("CasoFechaRecepcion", "CasoId", respuestastringanex6).Text;

                        sinitituloregistro = validareader("CasoTituloespanol", "CasoId", respuestastringanex6).Text;
                        sinicasonumeroregistro = validareader("CasoNumero", "CasoId", respuestastringanex6).Text;



                        this.FindAndReplace(wordApp, "«RegistroDatoEspanol»", "Registro N° " + validareader("CasoNumConcedida", "CasoId", respuestastringanex6).Text);
                        this.FindAndReplace(wordApp, "«TipoSolicitudEspanol»", validareader("TipoSolicitudDescrip", "CasoId", respuestastringanex6).Text);

                        this.FindAndReplace(wordApp, "«Asunto»", validareader("TipoSolicitudDescripI", "CasoId", respuestastringanex6).Text +
                            " N° " + validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastringanex6).Text);

                        this.FindAndReplace(wordApp, "«TituloRegistroDatoEspanol»", validareader("TipoSolicitudDescrip", "CasoId", respuestastringanex6).Text + " N° " + validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastringanex6).Text);
                        this.FindAndReplace(wordApp, "«TituloRegistroDato»", validareader("TipoSolicitudDescripI", "CasoId", respuestastringanex6).Text + " N° " + validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastringanex6).Text);
                    }

                    respuestastringanex6.Close();

                    if (sinitituloregistro.Length > 14)
                    {

                        String nombrecarpeta = sinitituloregistro.Substring(0, 15);
                        srutacarpeta = Sinicioruta + nombrecarpeta + "_" + sinicasonumeroregistro;
                        System.IO.Directory.CreateDirectory(srutacarpeta);

                    }
                    else
                    {

                        String nombrecarpeta = sinitituloregistro;
                        srutacarpeta = Sinicioruta + nombrecarpeta + "_" + sinicasonumeroregistro;
                        System.IO.Directory.CreateDirectory(srutacarpeta);
                    }


                    Random random6 = new Random();
                    int randomNumber6 = random6.Next(0, 100);

                    object fileNametres6 = srutacarpeta + "\\Carta" + sIdcarta + randomNumber6 + "_" + sCasoId + ".doc";
                    object fileName6 = srutacarpeta + "\\Carta" + sIdcarta + randomNumber6 + "_" + sCasoId + ".pdf";

                    String querycartapru64 = "SELECT * FROM referencia where referencia.CasoId =  " +sCasoId+";";
                    MySqlDataReader respuestastringanex64 = condoc.getdatareader(querycartapru64);
                    while (respuestastringanex64.Read())
                        {
                            this.FindAndReplace(wordApp, "«ReferenciaCliente»", validareader("ReferenciaNombre", "ReferenciaId", respuestastringanex64).Text);
                        }

                        this.FindAndReplace(wordApp, "«ReferenciaCliente»", "");
                        respuestastringanex64.Close();

                        String querycartapru36 = "SELECT  usuario.UsuarioClave, usuario.UsuarioId FROM caso_marcas, usuario where caso_marcas.CasoId = "+sCasoId+" AND caso_marcas.UsuarioId = usuario.UsuarioId; ";
                        MySqlDataReader respuestastringanex36 = condoc.getdatareader(querycartapru36);
                        while (respuestastringanex36.Read())
                        {
                            this.FindAndReplace(wordApp, "«Iniciales»", validareader("UsuarioClave", "UsuarioId", respuestastringanex36).Text);
                        }
                        this.FindAndReplace(wordApp, "«Iniciales»", "");
                        respuestastringanex36.Close();



                     fecha = DateTime.Now.ToString("MM/dd/yyyy");
                     fechamesreporte = fecha.Substring(0, 2);
                     fechadiareporte = fecha.Substring(3, 2);
                     fechaanoreporte = fecha.Substring(6, 4);
                     fechamesreportei = fecha.Substring(0, 2);


                    switch (fechamesreporte)
                    {
                        case "01":
                            fechamesreporte = "Enero";
                            break;
                        case "02":
                            fechamesreporte = "Febrero";
                            break;
                        case "03":
                            fechamesreporte = "Marzo";
                            break;
                        case "04":
                            fechamesreporte = "Abril";
                            break;
                        case "05":
                            fechamesreporte = "Mayo";
                            break;
                        case "06":
                            fechamesreporte = "Junio";
                            break;
                        case "07":
                            fechamesreporte = "Julio";
                            break;
                        case "08":
                            fechamesreporte = "Agosto";
                            break;
                        case "09":
                            fechamesreporte = "Septiembre";
                            break;
                        case "10":
                            fechamesreporte = "Octubre";
                            break;
                        case "11":
                            fechamesreporte = "Noviembre";
                            break;
                        case "12":
                            fechamesreporte = "Diciembre";
                            break;
                       
                    }

                    switch (fechamesreportei)
                    {
                        case "01":
                            fechamesreportei = "January";
                            break;
                        case "02":
                            fechamesreportei = "February";
                            break;
                        case "03":
                            fechamesreportei = "March";
                            break;
                        case "04":
                            fechamesreportei = "April";
                            break;
                        case "05":
                            fechamesreportei = "May";
                            break;
                        case "06":
                            fechamesreportei = "June";
                            break;
                        case "07":
                            fechamesreportei = "July";
                            break;
                        case "08":
                            fechamesreportei = "August";
                            break;
                        case "09":
                            fechamesreportei = "September";
                            break;
                        case "10":
                            fechamesreportei = "October";
                            break;
                        case "11":
                            fechamesreportei = "November";
                            break;
                        case "12":
                            fechamesreportei = "December";
                            break;

                    }

                    String Fechareportespanol6 = fechadiareporte + " de " + fechamesreporte + " de " + fechaanoreporte;
                    String Fechareporteingles6 =  fechamesreportei + " "+fechadiareporte +", "+ fechaanoreporte;

                    this.FindAndReplace(wordApp, "«FechaReporteEspanol»", Fechareportespanol6);
                    this.FindAndReplace(wordApp, "«FechaReporte»", Fechareporteingles6);


                    Object fileformat6 = Word.WdSaveFormat.wdFormatPDF;
                    Object SaveChange6 = Word.WdSaveOptions.wdDoNotSaveChanges;
                    Object OrianalForamt6 = Word.WdOriginalFormat.wdOriginalDocumentFormat;

                    aDoc.Activate();
                    aDoc.SaveAs2(ref fileNametres6,
                                    ref missing6, ref missing6, ref missing6, ref missing6, ref missing6,
                                    ref missing6, ref missing6, ref missing6, ref missing6, ref missing6,
                                    ref missing6, ref missing6, ref missing6, ref missing6, ref missing6);


                    aDoc.SaveAs(ref fileName6, ref fileformat6, ref missing6, ref missing6, ref missing6, ref missing6,
                                ref missing6, ref missing6, ref missing6, ref missing6, ref missing6, ref missing6, ref missing6, ref missing6, ref missing6, ref missing6);
                    aDoc.Saved = true;

                    wordApp.Quit(ref missing6, ref missing6, ref missing6);

                    Process.Start(fileNametres6.ToString());
                    Process.Start(fileName6.ToString());

                    break;
                case "8":

                    object missing7 = Missing.Value;
                    wordApp.Quit(ref missing7, ref missing7, ref missing7);
                    wordApp = new Word.Application();
                    aDoc = null;
                    String querycartapru7 = "SELECT "+
                                            "caso_reservadederechos.CasoId, " +
                                            "caso_reservadederechos.CasoTitular, " +
                                            "cliente.ClienteNombre, "+
                                            "interesado.InteresadoNombre, "+
                                            "interesado.InteresadoApPaterno, "+
                                            "interesado.InteresadoApMaterno, "+
                                            "caso_reservadederechos.CasoNumeroExpedienteLargo, "+
                                            "caso_reservadederechos.CasoTituloespanol, " +
                                            "caso_reservadederechos.CasoTituloingles, " +
                                            "caso_reservadederechos.TipoSolicitudId, "+
                                            "DATE_FORMAT(caso_reservadederechos.CasoFechaRecepcion,  '%d-%m-%Y') AS CasoFechaRecepcion, "+
                                            "DATE_FORMAT(caso_reservadederechos.CasoFechaVigencia, '%d-%m-%Y') AS CasoFechaVigencia, "+
                                            "DATE_FORMAT(caso_reservadederechos.CasoFechaLegal, '%d-%m-%Y') AS CasoFechaLegal, "+
                                            "caso_reservadederechos.CasoNumero, "+
                                            "direccion.DireccionCalle, "+
                                            "direccion.DireccionNumExt, "+
                                            "direccion.DireccionNumInt, "+
                                            "direccion.DireccionColonia, "+
                                            "direccion.DireccionPoblacion, "+
                                            "direccion.DireccionEstado, "+
                                            "direccion.PaisId, "+
                                            "direccion.DireccionCP, "+
                                            "tiposolicitud.TipoSolicitudDescrip, "+
                                            "tiposolicitud.TipoSolicitudDescripI, "+
                                            "caso_reservadederechos.CasoNumConcedida, "+
                                            "casointeresado.CasoInteresadoSecuencia, "+
                                            "casointeresado.TipoRelacionId "+
                                        "FROM "+
                                            "caso_reservadederechos, "+
                                            "cliente, "+
                                            "casocliente, "+
                                            "interesado, "+
                                            "casointeresado, "+
                                            "direccion, "+
                                            "tiposolicitud "+
                                        "WHERE "+
                                            "caso_reservadederechos.CasoId = "+sCasoId+
                                                " AND caso_reservadederechos.CasoId = casocliente.CasoId "+
                                                "AND caso_reservadederechos.CasoId = casointeresado.CasoId "+
                                                "AND casointeresado.InteresadoId = interesado.InteresadoID "+
                                                "AND casocliente.ClienteId = cliente.ClienteId "+
                                                "AND (cliente.ClienteId = direccion.ClienteId "+
                                                "OR interesado.InteresadoID = direccion.InteresadoId) "+
                                                "AND caso_reservadederechos.TipoSolicitudId = tiposolicitud.TipoSolicitudId "+
                                                "AND casointeresado.TipoRelacionId = 1 "+
                                                "ORDER BY CasoInteresadoSecuencia ASC limit 1;";

                    MySqlDataReader respuestastringanex7 = condoc.getdatareader(querycartapru7);

                    object abrirDoc7 = sinirutacartas + sIdcarta;
                    object readOnly7 = false;
                    object isVisible7 = false;

                    aDoc = wordApp.Documents.Open(ref abrirDoc7, ref isVisible7, ref readOnly7,
                                                ref missing7, ref missing7, ref missing7,
                                                ref missing7, ref missing7, ref missing7,
                                                ref missing7, ref missing7, ref missing7,
                                                ref missing7, ref missing7, ref missing7, ref missing7);



                    String  fechavigencia7="", fechalegal7="", fecharecepcion7 ="", titular7="", sinitituloreserva="", sinicasonumeroreserva="";


                    while (respuestastringanex7.Read())
                    {
                        //primer parametro es el texto, el segundo parametro es el id o valor y el tercer parametro es el mysqldatareader
                        this.FindAndReplace(wordApp, "«CLIENTENOMBRE»", validareader("ClienteNombre", "CasoId", respuestastringanex7).Text);
                        this.FindAndReplace(wordApp, "«ClienteDireccion»", validareader("DireccionCalle", "CasoId", respuestastringanex7).Text + " " +
                              validareader("DireccionNumExt", "CasoId", respuestastringanex7).Text + " " + validareader("DireccionNumInt", "CasoId", respuestastringanex7).Text + " " +
                              validareader("DireccionColonia", "CasoId", respuestastringanex7).Text + " " + validareader("DireccionPoblacion", "CasoId", respuestastringanex7).Text + " " +
                             validareader("DireccionEstado", "CasoId", respuestastringanex7).Text);



                        this.FindAndReplace(wordApp, "«ClienteDireccion»", validareader("ClienteNombre", "CasoId", respuestastringanex7).Text);
                        this.FindAndReplace(wordApp, "«AsuntoE»", validareader("TipoSolicitudDescrip", "CasoId", respuestastringanex7).Text +
                             " N° " + validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastringanex7).Text);
                        this.FindAndReplace(wordApp, "«Titulo»", validareader("CasoTituloespanol", "CasoId", respuestastringanex7).Text);
                        this.FindAndReplace(wordApp, "«TituloIngles»", validareader("CasoTituloingles", "CasoId", respuestastringanex7).Text);

                        this.FindAndReplace(wordApp, "«ReferenciaDespacho»", validareader("CasoNumero", "CasoId", respuestastringanex7).Text);
                        titular7 = validareader("CasoTitular", "CasoId", respuestastringanex7).Text;
                        if (titular7.Equals(""))
                        {
                            this.FindAndReplace(wordApp, "«TitularNombre»", validareader("InteresadoNombre", "CasoId", respuestastringanex7).Text + " " +
                           validareader("InteresadoApPaterno", "CasoId", respuestastringanex7).Text + " " + validareader("InteresadoApMaterno", "CasoId", respuestastringanex7).Text);
                        }
                        else
                        {
                            this.FindAndReplace(wordApp, "«TitularNombre»", titular7);
                        }

                        fechalegal7 = validareader("CasoFechaLegal", "CasoId", respuestastringanex7).Text;
                        fechavigencia7 = validareader("CasoFechaVigencia", "CasoId", respuestastringanex7).Text; //OK
                        fecharecepcion7 = validareader("CasoFechaRecepcion", "CasoId", respuestastringanex7).Text; //OK

                        sinitituloreserva = validareader("CasoTituloespanol", "CasoId", respuestastringanex7).Text; //OK
                        sinicasonumeroreserva = validareader("CasoNumero", "CasoId", respuestastringanex7).Text; //OK


                        this.FindAndReplace(wordApp, "«RegistroDatoEspanol»", "Registro N° " + validareader("CasoNumConcedida", "CasoId", respuestastringanex7).Text);
                        this.FindAndReplace(wordApp, "«TipoSolicitudEspanol»", validareader("TipoSolicitudDescrip", "CasoId", respuestastringanex7).Text);

                        this.FindAndReplace(wordApp, "«Asunto»", validareader("TipoSolicitudDescripI", "CasoId", respuestastringanex7).Text +
                            " N° " + validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastringanex7).Text);

                        this.FindAndReplace(wordApp, "«TituloRegistroDatoEspanol»", validareader("TipoSolicitudDescrip", "CasoId", respuestastringanex7).Text + " N° " + validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastringanex7).Text);
                        this.FindAndReplace(wordApp, "«TituloRegistroDato»", validareader("TipoSolicitudDescripI", "CasoId", respuestastringanex7).Text + " N° " + validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastringanex7).Text);
                    }

                    respuestastringanex7.Close();
                    if (sinitituloreserva.Length > 14)
                    {

                        String nombrecarpeta = sinitituloreserva.Substring(0, 15);
                        srutacarpeta = Sinicioruta + nombrecarpeta + "_" + sinicasonumeroreserva;
                        System.IO.Directory.CreateDirectory(srutacarpeta);

                    }
                    else
                    {

                        String nombrecarpeta = sinitituloreserva;
                        srutacarpeta = Sinicioruta + nombrecarpeta + "_" + sinicasonumeroreserva;
                        System.IO.Directory.CreateDirectory(srutacarpeta);
                    }




                    Random random7 = new Random();
                    int randomNumber7 = random7.Next(0, 100);
                    object fileNametres7 = srutacarpeta + "\\Carta" + sIdcarta + randomNumber7 + "_" + sCasoId + ".doc";
                    object fileName7 = srutacarpeta + "\\Carta" + sIdcarta + randomNumber7 + "_" + sCasoId + ".pdf";


                    String querycartapru27 = "SELECT * FROM referencia where referencia.CasoId =  " + sCasoId + ";";
                    
                    MySqlDataReader respuestastringanex27 = condoc.getdatareader(querycartapru27);
                    while (respuestastringanex27.Read())
                        {
                            this.FindAndReplace(wordApp, "«ReferenciaCliente»", validareader("ReferenciaNombre", "ReferenciaId", respuestastringanex27).Text);
                        }

                        this.FindAndReplace(wordApp, "«ReferenciaCliente»", "");
                        respuestastringanex27.Close();

                        String querycartapru37 = "SELECT  usuario.UsuarioClave, usuario.UsuarioId FROM caso_marcas, usuario where caso_marcas.CasoId = "+sCasoId+" AND caso_marcas.UsuarioId = usuario.UsuarioId; ";
                        MySqlDataReader respuestastringanex37 = condoc.getdatareader(querycartapru37);
                        while (respuestastringanex37.Read())
                        {
                            this.FindAndReplace(wordApp, "«Iniciales»", validareader("UsuarioClave", "UsuarioId", respuestastringanex37).Text);
                        }
                        this.FindAndReplace(wordApp, "«Iniciales»", "");
                        respuestastringanex37.Close();

                    fecha = DateTime.Now.ToString("MM/dd/yyyy");
                     fechamesreporte = fecha.Substring(0, 2);
                     fechadiareporte = fecha.Substring(3, 2);
                     fechaanoreporte = fecha.Substring(6, 4);
                     fechamesreportei = fecha.Substring(0, 2);


                    switch (fechamesreporte)
                    {
                        case "01":
                            fechamesreporte = "Enero";
                            break;
                        case "02":
                            fechamesreporte = "Febrero";
                            break;
                        case "03":
                            fechamesreporte = "Marzo";
                            break;
                        case "04":
                            fechamesreporte = "Abril";
                            break;
                        case "05":
                            fechamesreporte = "Mayo";
                            break;
                        case "06":
                            fechamesreporte = "Junio";
                            break;
                        case "07":
                            fechamesreporte = "Julio";
                            break;
                        case "08":
                            fechamesreporte = "Agosto";
                            break;
                        case "09":
                            fechamesreporte = "Septiembre";
                            break;
                        case "10":
                            fechamesreporte = "Octubre";
                            break;
                        case "11":
                            fechamesreporte = "Noviembre";
                            break;
                        case "12":
                            fechamesreporte = "Diciembre";
                            break;
                       
                    }

                    switch (fechamesreportei)
                    {
                        case "01":
                            fechamesreportei = "January";
                            break;
                        case "02":
                            fechamesreportei = "February";
                            break;
                        case "03":
                            fechamesreportei = "March";
                            break;
                        case "04":
                            fechamesreportei = "April";
                            break;
                        case "05":
                            fechamesreportei = "May";
                            break;
                        case "06":
                            fechamesreportei = "June";
                            break;
                        case "07":
                            fechamesreportei = "July";
                            break;
                        case "08":
                            fechamesreportei = "August";
                            break;
                        case "09":
                            fechamesreportei = "September";
                            break;
                        case "10":
                            fechamesreportei = "October";
                            break;
                        case "11":
                            fechamesreportei = "November";
                            break;
                        case "12":
                            fechamesreportei = "December";
                            break;

                    }

                    String Fechareportespanol7 = fechadiareporte + " de " + fechamesreporte + " de " + fechaanoreporte;
                    String Fechareporteingles7 =  fechamesreportei + " "+fechadiareporte +", "+ fechaanoreporte;

                    this.FindAndReplace(wordApp, "«FechaReporteEspanol»", Fechareportespanol7);
                    this.FindAndReplace(wordApp, "«FechaReporte»", Fechareporteingles7);


                    Object fileformat7 = Word.WdSaveFormat.wdFormatPDF;
                    Object SaveChange7 = Word.WdSaveOptions.wdDoNotSaveChanges;
                    Object OrianalForamt7 = Word.WdOriginalFormat.wdOriginalDocumentFormat;

                    aDoc.Activate();
                    aDoc.SaveAs2(ref fileNametres7,
                                    ref missing7, ref missing7, ref missing7, ref missing7, ref missing7,
                                    ref missing7, ref missing7, ref missing7, ref missing7, ref missing7,
                                    ref missing7, ref missing7, ref missing7, ref missing7, ref missing7);


                    aDoc.SaveAs(ref fileName7, ref fileformat7, ref missing7, ref missing7, ref missing7, ref missing7,
                                ref missing7, ref missing7, ref missing7, ref missing7, ref missing7, ref missing7, ref missing7, ref missing7, ref missing7, ref missing7);
                    aDoc.Saved = true;

                    wordApp.Quit(ref missing7, ref missing7, ref missing7);

                    Process.Start(fileNametres7.ToString());
                    Process.Start(fileName7.ToString());
                    break;

                }
            }
            catch (Exception E)
            {
                String ruta_log = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + "\\casosking\\";
                if (!Directory.Exists(ruta_log))
                {
                    System.IO.Directory.CreateDirectory(ruta_log);
                }
                String fechalog = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                StringBuilder sb_log = new StringBuilder();
                sb_log.Append(fechalog + ":Calss:cartas" + " Error:" + E + "\n");
                System.IO.File.AppendAllText(ruta_log + "sistema_casosking.log", sb_log.ToString());
                sb_log.Clear();

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



        private void FindAndReplace(Microsoft.Office.Interop.Word.Application wordApp, object findText, object replaceWithText)
        {
            try { 
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
                String ruta_log = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + "\\casosking\\";
                if (!Directory.Exists(ruta_log))
                {
                    System.IO.Directory.CreateDirectory(ruta_log);
                }
                String fechalog = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                StringBuilder sb_log = new StringBuilder();
                sb_log.Append(fechalog + ":Class: cartas"+ " Error:" + E + "\n");
                System.IO.File.AppendAllText(ruta_log + "sistema_casosking.log", sb_log.ToString());
                sb_log.Clear();

            }  

        }

    }
}

