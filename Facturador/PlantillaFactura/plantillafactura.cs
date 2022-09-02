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
using System.Globalization;
using System.Diagnostics.CodeAnalysis;

namespace Facturador.PlantillaFactura
{
    class plantillafactura
    {
        funcionesdicss fun_dicss = new funcionesdicss();
        object missing = Missing.Value;
        Word.Application wordApp;
        Word.Document aDoc = null;
        Application app;
        funcionesdicss objfuncionesdicss = new funcionesdicss();
        public string sCasos { get; set; }

        public plantillafactura(obj_factura_pdf oFactura)
        {
            generaoficiosproductivos(oFactura);
        }
        public void generadocs(String sQuery)
        {
            sCasos = sQuery;
        }

        public void generaoficiosproductivos(obj_factura_pdf oFactura)
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
                String sruta_plantilla = "C:\\Formatos_CasosKing" + @"\formatosconfigurables\plantilla_facturaking.docx";
                String carpeta = "C:\\Formatos_CasosKing";
                //si no existe la carpeta temporal la creamos
                if (!(Directory.Exists(carpeta)))
                {
                    Directory.CreateDirectory(carpeta);
                }
                    String sPrefijonombrearchivo = DateTime.Now.ToString("ddMMyyyyHHmmss");

                    String sArchivogenerado = carpeta + "\\Formato_PDF_base_" + sPrefijonombrearchivo + ".docx";
                    File.Copy(sruta_plantilla, sArchivogenerado);//copiamos la plantilla para generarlo en la carpeta destino
                    //abrimos el archivo temporal y lo reemplzamos con los datos
                    Word.Application application = new Word.Application();
                    Word.Document document = application.Documents.Open(sArchivogenerado);

                    DateTime localDate = DateTime.Now;
                    String sFechaactual = localDate.ToString("dd/MM/yyyy");
                    String sDAteformat = DateTime.Now.ToString("dd MMMM yyyy");

                    String Cliente_nombre = "";
                    String Direccionuno = "";
                    String Direcciondos = "";
                    String Direccion_tres = "";
                    String Direccion_cuatro = "";
                    String RFC_cliente = "";
                    String Nota_debitoNo = "0552";
                    String Fecha_factura = sFechaactual;
                    //String concepto1 = "Concepto uno de la factura";
                    //String conceptodesc1 = "descripción pequeña de la factura";
                    //String importe1 = "100";
                    String ExpNum = "1001-A";
                    String NuestraRef = "Referencia num";
                    String referenciacliente = "Referencia del cliente";
                    String cliente_id = "1235";
                    String usuario = "1";
                    String servicio = "servicio";
                    String Subtotal = "1250";
                    String Iva = "150";
                    String Total = "1400";
                    String importe_letra_esp = "Mil docientos cincuenta";
                    String importe_letra_eng = "One thousand two hundred and fifty";

                    /*Con el id del cliente consultamos la dirección*/
                    //oFactura.cliente_id
                    ////conect con_direccioncliente = new conect();
                    ////String sQuerycliente = " SELECT "+
                    ////                        "     cliente.IdiomaId,"+
                    ////                        "     idioma.IdiomaDescripcion,"+
                    ////                        "     cliente.ClienteRFC,"+
                    ////                        "     direccion.*,"+
                    ////                        "     pais.PaisNombre,"+
                    ////                        "     pais.PaisNombreIngles"+
                    ////                        " FROM"+
                    ////                        "     direccion,"+
                    ////                        "     cliente,"+
                    ////                        "     pais,"+
                    ////                        "     idioma"+
                    ////                        " WHERE"+
                    ////                        "     direccion.clienteid = cliente.clienteid"+
                    ////                        "         AND direccion.clienteid ="  + oFactura.cliente_id +
                    ////                        "         and cliente.IdiomaId = idioma.IdiomaId"+
                    ////                        "         AND direccion.PaisId = pais.PaisId;";
                    //////String sQuerycliente = "select cliente.ClienteRFC, direccion.* from direccion, cliente where direccion.clienteid = cliente.clienteid  and direccion.clienteid = " + oFactura.cliente_id + ";";
                    ////MySqlDataReader resp_direccion = con_direccioncliente.getdatareader(sQuerycliente);
                    ////while (resp_direccion.Read())
                    ////{
                    ////    Direccionuno = fun_dicss.validareader("DireccionCalle", "clienteid", resp_direccion).Text + " " + fun_dicss.validareader("DireccionNumExt", "clienteid", resp_direccion).Text + " " + fun_dicss.validareader("DireccionNumInt", "clienteid", resp_direccion).Text;
                    ////    Direcciondos = fun_dicss.validareader("DireccionColonia", "clienteid", resp_direccion).Text;
                    ////    Direccion_tres = fun_dicss.validareader("DireccionCP", "clienteid", resp_direccion).Text + " " + fun_dicss.validareader("DireccionEstado", "clienteid", resp_direccion).Text;
                    ////    RFC_cliente = fun_dicss.validareader("ClienteRFC", "clienteid", resp_direccion).Text + " " + fun_dicss.validareader("DireccionEstado", "clienteid", resp_direccion).Text;
                    ////    if(fun_dicss.validareader("ClienteRFC", "clienteid", resp_direccion).Text=="1"){//Si es inglés
                    ////        Direccion_cuatro = fun_dicss.validareader("PaisNombreIngles", "clienteid", resp_direccion).Text;
                    ////    }else{
                    ////        Direccion_cuatro = fun_dicss.validareader("PaisNombre", "clienteid", resp_direccion).Text;
                    ////    }
                        
                    ////    if (Direcciondos=="")
                    ////    {
                    ////        Direcciondos = fun_dicss.validareader("DireccionCP", "clienteid", resp_direccion).Text + " " + fun_dicss.validareader("DireccionEstado", "clienteid", resp_direccion).Text;
                    ////            if(fun_dicss.validareader("IdiomaId", "clienteid", resp_direccion).Text=="1"){//Si es inglés
                    ////                Direccion_tres = fun_dicss.validareader("PaisNombreIngles", "clienteid", resp_direccion).Text;
                    ////            }else{
                    ////                Direccion_tres = fun_dicss.validareader("PaisNombre", "clienteid", resp_direccion).Text;
                    ////            }
                    ////            Direccion_cuatro = "";
                    ////    }

                    ////}
                    ////resp_direccion.Close();
                    ////con_direccioncliente.Cerrarconexion();

                    

                    document.Bookmarks["Cliente_nombre"].Select();
                    application.Selection.TypeText(oFactura.Cliente_nombre);
                    document.Bookmarks["Direccionuno"].Select();
                    application.Selection.TypeText(oFactura.Direccionuno);
                    document.Bookmarks["Direcciondos"].Select();
                    application.Selection.TypeText(oFactura.Direcciondos);
                    document.Bookmarks["Direccion_tres"].Select();
                    application.Selection.TypeText(oFactura.Direccion_tres);
                    document.Bookmarks["Direccion_cuatro"].Select();
                    application.Selection.TypeText(oFactura.Direccion_cuatro);
                    document.Bookmarks["RFC_cliente"].Select();
                    application.Selection.TypeText(oFactura.RFC_cliente);
                    document.Bookmarks["Nota_debitoNo"].Select();
                    application.Selection.TypeText(oFactura.Nota_debitoNo);
                    document.Bookmarks["Fecha_factura"].Select();
                    application.Selection.TypeText(Fecha_factura);
                    //document.Bookmarks["concepto1"].Select();
                    //application.Selection.TypeText(concepto1);
                    //document.Bookmarks["conceptodesc1"].Select();
                    //application.Selection.TypeText(conceptodesc1);
                    //document.Bookmarks["importe1"].Select();
                    //application.Selection.TypeText(importe1);
                    document.Bookmarks["ExpNum"].Select();
                    application.Selection.TypeText(oFactura.ExpNum);
                    document.Bookmarks["NuestraRef"].Select();
                    application.Selection.TypeText(oFactura.NuestraRef);
                    document.Bookmarks["referenciacliente"].Select();
                    application.Selection.TypeText(oFactura.referenciacliente);
                    document.Bookmarks["cliente_id"].Select();
                    application.Selection.TypeText(oFactura.cliente_id);
                    document.Bookmarks["usuario"].Select();
                    application.Selection.TypeText(oFactura.usuario);
                    document.Bookmarks["servicio"].Select();
                    application.Selection.TypeText(oFactura.servicio);
                    
                    
                    CultureInfo ci = new CultureInfo("en-us");
                    int count = 1;
                    float cuenta = 0;
                    foreach (var oFacturas in oFactura.conceptos)
                    {
                        document.Bookmarks["concepto" + count].Select();
                        application.Selection.TypeText(oFacturas.concepto1);

                        document.Bookmarks["Honorarios" + count].Select();//conceptodesc1
                        application.Selection.TypeText(oFacturas.conceptohonorarios);

                        document.Bookmarks["derechos" + count].Select();//conceptodesc1
                        application.Selection.TypeText(oFacturas.conceptoderechos);

                        document.Bookmarks["importehonorario" + count].Select();//importehonorario1
                        float fImportenohorarios = float.Parse(oFacturas.simportehono, CultureInfo.InvariantCulture.NumberFormat);
                        application.Selection.TypeText(fImportenohorarios.ToString("C", ci));
                        
                        document.Bookmarks["importederecho" + count].Select();//importehonorario1
                        float fImportederecho = float.Parse(oFacturas.simportederecho, CultureInfo.InvariantCulture.NumberFormat);
                        application.Selection.TypeText(fImportederecho.ToString("C", ci));

                        float iTotalporconcepto = fImportenohorarios + fImportederecho;

                        //String sImportederechos = oFacturas.simportederecho.Replace("$", "").Replace(",", "");
                        ////double iConcepto = Convert.ToDouble(sImporte);
                        //float iConcepto = float.Parse(sImporte, CultureInfo.InvariantCulture.NumberFormat);
                        cuenta += iTotalporconcepto;
                        count++;
                    }

                    /*Calculamos el submonto y total*/
                    double iIva = cuenta * 0.16;
                    double sTotal = iIva + cuenta;
                    document.Bookmarks["Subtotal"].Select();
                    application.Selection.TypeText(cuenta.ToString("C", ci));
                    document.Bookmarks["Iva"].Select();
                    application.Selection.TypeText(iIva.ToString("C", ci));
                    document.Bookmarks["Total"].Select();
                    application.Selection.TypeText(sTotal.ToString("C", ci));

                    /*Decimal to Letra*/
                    decimal total = decimal.Parse(sTotal.ToString());
                    String sLetratotal = total.NumeroALetras();
                    document.Bookmarks["importe_letra_esp"].Select();
                    application.Selection.TypeText(sLetratotal);
                    int inttotal = int.Parse( Math.Round(sTotal, 0).ToString(), CultureInfo.InvariantCulture.NumberFormat);
                    String sNumberwords = NumberToWords(inttotal);
                    document.Bookmarks["importe_letra_eng"].Select();
                    application.Selection.TypeText(sNumberwords);
                    
                    /*Bookmarks en el documento*/
                    /*
                     *Cliente_nombre
                     *Direccionuno
                     *Direcciondos
                     *Direccion_tres
                     *Direccion_cuatro
                     *RFC_cliente
                     *Nota_debitoNo
                     *Fecha_factura
                     *concepto1
                     *conceptodesc1
                     *importe1
                     *disponible hasta N = 10 conceptos en una factura  conceptoN... ,  conceptodescN... , importeN...
                     * ExpNum
                     * NuestraRef
                     * referenciacliente
                     * cliente_id
                     * usuario
                     * servicio
                     * Subtotal
                     * Iva
                     * Total
                     * importe_letra_esp
                     * importe_letra_eng
                     */

                    

                    document.Save();
                    application.Quit();
                    string resultado = CreatePDF(sArchivogenerado, carpeta);
                    Process.Start(resultado);
                    
                
            }
            catch (Exception Ex)
            {
                new filelog("", Ex.Message);

            }
        }
        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }

        //Funcion para converitr a PDF
        public string CreatePDF(string path, string exportDir)
        {
            Application app = new Application();
            app.DisplayAlerts = WdAlertLevel.wdAlertsNone;
            app.Visible = false;

            var objPresSet = app.Documents;
            var objPres = objPresSet.Open(path, true, true, false);

            var pdfFileName = Path.ChangeExtension(path, ".pdf");
            var pdfPath = Path.Combine(exportDir, pdfFileName);

            try
            {
                objPres.ExportAsFixedFormat(
                    pdfPath,
                    WdExportFormat.wdExportFormatPDF,
                    false,
                    WdExportOptimizeFor.wdExportOptimizeForPrint,
                    WdExportRange.wdExportAllDocument
                );
            }
            catch
            {
                pdfPath = null;
            }
            finally
            {
                objPres.Close();
                //objPresSet.Close();
            }
            return pdfPath;
        }
        
    }
    public static class Conversores
{
    public static string NumeroALetras(this decimal numberAsString)
    {
        string dec;            
           
        var entero = Convert.ToInt64(Math.Truncate(numberAsString));
        var decimales = Convert.ToInt32(Math.Round((numberAsString - entero) * 100, 2));
        if (decimales > 0)
        {
            dec = " PESOS CON " + decimales.ToString() + "/100";
            //dec = $" PESOS {decimales:0,0} /100";
        }
        //Código agregado por mí
        else
        {
            dec = " PESOS CON " + decimales.ToString() + "/100";
            //dec = $" PESOS {decimales:0,0} /100";
        }
        var res = NumeroALetras(Convert.ToDouble(entero)) + dec;
        return res;
    }
    [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
    private static string NumeroALetras(double value)
    {
        string num2Text; value = Math.Truncate(value);
        if (value == 0) num2Text = "CERO";
        else if (value == 1) num2Text = "UNO";
        else if (value == 2) num2Text = "DOS";
        else if (value == 3) num2Text = "TRES";
        else if (value == 4) num2Text = "CUATRO";
        else if (value == 5) num2Text = "CINCO";
        else if (value == 6) num2Text = "SEIS";
        else if (value == 7) num2Text = "SIETE";
        else if (value == 8) num2Text = "OCHO";
        else if (value == 9) num2Text = "NUEVE";
        else if (value == 10) num2Text = "DIEZ";
        else if (value == 11) num2Text = "ONCE";
        else if (value == 12) num2Text = "DOCE";
        else if (value == 13) num2Text = "TRECE";
        else if (value == 14) num2Text = "CATORCE";
        else if (value == 15) num2Text = "QUINCE";
        else if (value < 20) num2Text = "DIECI" + NumeroALetras(value - 10);
        else if (value == 20) num2Text = "VEINTE";
        else if (value < 30) num2Text = "VEINTI" + NumeroALetras(value - 20);
        else if (value == 30) num2Text = "TREINTA";
        else if (value == 40) num2Text = "CUARENTA";
        else if (value == 50) num2Text = "CINCUENTA";
        else if (value == 60) num2Text = "SESENTA";
        else if (value == 70) num2Text = "SETENTA";
        else if (value == 80) num2Text = "OCHENTA";
        else if (value == 90) num2Text = "NOVENTA";
        else if (value < 100) num2Text = NumeroALetras(Math.Truncate(value / 10) * 10) + " Y " + NumeroALetras(value % 10);
        else if (value == 100) num2Text = "CIEN";
        else if (value < 200) num2Text = "CIENTO " + NumeroALetras(value - 100);
        else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) num2Text = NumeroALetras(Math.Truncate(value / 100)) + "CIENTOS";
        else if (value == 500) num2Text = "QUINIENTOS";
        else if (value == 700) num2Text = "SETECIENTOS";
        else if (value == 900) num2Text = "NOVECIENTOS";
        else if (value < 1000) num2Text = NumeroALetras(Math.Truncate(value / 100) * 100) + " " + NumeroALetras(value % 100);
        else if (value == 1000) num2Text = "MIL";
        else if (value < 2000) num2Text = "MIL " + NumeroALetras(value % 1000);
        else if (value < 1000000)
        {
            num2Text = NumeroALetras(Math.Truncate(value / 1000)) + " MIL";
            if ((value % 1000) > 0)
            {
                num2Text = num2Text + " " + NumeroALetras(value % 1000);
            }
        }
        else if (value == 1000000)
        {
            num2Text = "UN MILLON";
        }
        else if (value < 2000000)
        {
            num2Text = "UN MILLON " + NumeroALetras(value % 1000000);
        }
        else if (value < 1000000000000)
        {
            num2Text = NumeroALetras(Math.Truncate(value / 1000000)) + " MILLONES ";
            if ((value - Math.Truncate(value / 1000000) * 1000000) > 0)
            {
                num2Text = num2Text + " " + NumeroALetras(value - Math.Truncate(value / 1000000) * 1000000);
            }
        }
        else if (value == 1000000000000) num2Text = "UN BILLON";
        else if (value < 2000000000000) num2Text = "UN BILLON " + NumeroALetras(value - Math.Truncate(value / 1000000000000) * 1000000000000);
        else
        {
            num2Text = NumeroALetras(Math.Truncate(value / 1000000000000)) + " BILLONES";
            if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0)
            {
                num2Text = num2Text + " " + NumeroALetras(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            }
        }
        return num2Text;
    }
}
}
