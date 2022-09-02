using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Facturador
{
    class funcionesdicss
    {

        public String sFehacss { get; set; }
        public String carpeta { get; set; }
        public funcionesdicss() {
            try
            {
                carpeta = "C:\\Formatos_CasosKing";
                //si no existe la carpeta temporal la creamos
                if (!(Directory.Exists(carpeta)))
                {
                    Directory.CreateDirectory(carpeta);
                }
            } catch (Exception E) {

            }
        }
        public String sGetfechehhmmss() {
            DateTime fecha = DateTime.Now;
            sFehacss = fecha.ToString("HHmmss");
            return sFehacss;
        }

        public void lipiafechas_busquedas(DataGridView dtaPagos)
        {
            try
            {
                for (int fila = 0; fila < dtaPagos.Rows.Count - 1; fila++)
                {
                    for (int col = 0; col < dtaPagos.Rows[fila].Cells.Count; col++)
                    {
                        string valor = dtaPagos.Rows[fila].Cells[col].Value.ToString();
                        if (valor == "01/01/0001 12:00:00 a. m." || valor == "01/01/0001")
                        {
                            dtaPagos.Rows[fila].Cells[col].Style.ForeColor = Color.LightGray;
                            //dRows.Cells[7].Style.ForeColor = Color.Black;
                        }
                        //MessageBox.Show(valor);
                    }
                }
            }
            catch (Exception exs)
            {
                new filelog("", "" + exs.Message);
            }

        }
        public ComboboxItem validareader(String campoText, String campoValue, MySqlDataReader mresultado)
        {
            try
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
            catch (Exception E)
            {
                ComboboxItem cItemresult = new ComboboxItem();
                cItemresult.Text = "";
                cItemresult.Value = "";
                return cItemresult;
            }
        }
        public ComboboxItem validareader(String campoText, String campoValue, MySqlDataReader mresultado, Boolean bFecha)
        {
            try
            {
                ComboboxItem cItemresult = new ComboboxItem();
                if (!mresultado.IsDBNull(mresultado.GetOrdinal(campoText)))
                {
                    cItemresult.Text = validafechacorectaformato(mresultado.GetString(mresultado.GetOrdinal(campoText)), "dd-MM-yyyy", "dd'/'MM'/'yyyy");
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
            catch (Exception E)
            {
                ComboboxItem cItemresult = new ComboboxItem();
                cItemresult.Text = "";
                cItemresult.Value = "";
                return cItemresult;
            }
        }
        public string RemoveLineEndings(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            string lineSeparator = ((char)0x2028).ToString();
            string paragraphSeparator = ((char)0x2029).ToString();
            return value.Replace("\r\n", string.Empty).Replace("\n", string.Empty).Replace("\r", string.Empty).Replace(lineSeparator, string.Empty).Replace(paragraphSeparator, string.Empty);
        }

        public String validafechacorectaformato(String Fechaentrada, String sFormatoentrada, String sFechaSalida)
        {//verificamos que es una fecha valida y la convertimos a un formato date mysql
            String sFechasalida = "";
            try
            {
                sFechasalida = DateTime.ParseExact(Fechaentrada, sFormatoentrada, CultureInfo.InvariantCulture).ToString(sFechaSalida); //tbDocumentofecharecepcion.Text;
            }
            catch (Exception E)
            {
                sFechasalida = "";
            }
            return sFechasalida;
        }

        public String cambiodeformatofecha_yyyyMMdd(String sValorentrada) {
            String sReturnvalue = "";
            try {
                DateTime d;

                if (DateTime.TryParseExact(sValorentrada, "dd/MM/yyyy H:mm:ss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out d))
                {
                    sReturnvalue = d.ToString("yyyy-MM-dd");
                }
                return sReturnvalue;
            } catch (Exception Ex) {
                return "";
            }

        }
        public ComboboxItem getitembycomobobox(ComboBox combobox, Object value)
        {
            ComboboxItem cbitem = new ComboboxItem();
            cbitem.Text = " - ";
            cbitem.Value = 0;
            try
            {
                for (int x = 0; x < combobox.Items.Count; x++)
                {
                    if ((combobox.Items[x] as ComboboxItem).Value.Equals(value))
                    {
                        cbitem.Text = (combobox.Items[x] as ComboboxItem).Text;
                        cbitem.Value = (combobox.Items[x] as ComboboxItem).Value;
                    }
                }
            }
            catch (Exception ex)
            {
                cbitem.Text = " - ";
                cbitem.Value = 0;
            }
            return cbitem;
        }

        public bool generaplazo(String sTipoplazo, String sCasoid, String DocumentoId, String AnualidadId, String ClienteId, String PlazoMotivoCancelacion, String PlazoFecha, String PlazoFechaProrroga, String UsuarioId, String PlazoFechaAtencion, String EstatusPlazoId, String UsuarioIdCancelo, String PlazoDescripcion, String PlazoIdRef, String usuarioIdAtendio)
        {//requerimos para generar un plazo un 
            bool bVaribalereturn = false;
            try {
                if (sCasoid == "")
                {
                    return false;
                }
                conect con = new conect();
                String sInsert = "INSERT INTO `plazo` " +
                                "(`PlazoId`, " +
                                "`TipoPlazoId`, " +
                                "`CasoId`, " +
                                "`DocumentoId`, " +
                                "`AnualidadId`, " +
                                "`ClienteId`, " +
                                "`PlazoMotivoCancelacion`, " +
                                "`PlazoFecha`, " +
                                "`PlazoFechaProrroga`, " +
                                "`UsuarioId`, " +
                                "`PlazoFechaAtencion`, " +
                                "`EstatusPlazoId`, " +
                                "`UsuarioIdCancelo`, " +
                                "`PlazoDescripcion`, " +
                                "`PlazoIdRef`, " +
                                "`usuarioIdAtendio`) " +
                                "VALUES " +
                                "(NULL, " +
                                "'" + sTipoplazo + "' ," +
                                "'" + sCasoid + "' ," +
                                "'" + DocumentoId + "', " +
                                "'" + AnualidadId + "', " +
                                "'" + ClienteId + "', " +
                                "'" + PlazoMotivoCancelacion + "', " +
                                "'" + PlazoFecha + "', " +
                                "'" + PlazoFechaProrroga + "', " +
                                "'" + UsuarioId + "', " +
                                "'" + PlazoFechaAtencion + "', " +
                                "'" + EstatusPlazoId + "', " +
                                "'" + UsuarioIdCancelo + "', " +
                                "'" + PlazoDescripcion + "', " +
                                "'" + PlazoIdRef + "', " +
                                "'" + usuarioIdAtendio + "'); ";
                MySqlDataReader respuestastringcasonum = con.getdatareader(sInsert);
                if (respuestastringcasonum.RecordsAffected == 1)
                {
                    bVaribalereturn = true;
                } else {
                    bVaribalereturn = false;
                }
            } catch (Exception Ex) {
                bVaribalereturn = false;
            }
            return bVaribalereturn;
        }

        public void validafecha(TextBox itemTexbox) {
            try
            {
                if (itemTexbox.Text != "") {
                    DateTime dDate;
                    if (DateTime.TryParse(itemTexbox.Text, out dDate))
                    {
                        DateTime datemenor = new DateTime(1900, 1, 1, 0, 0, 0);//fecha menor 01/01/1900
                        DateTime datemayor = new DateTime(2060, 12, 1, 12, 0, 0);//fecha mayor 01/12/2060

                        int result = DateTime.Compare(datemenor, dDate);
                        int resultdos = DateTime.Compare(datemayor, dDate);


                        string relationship;

                        if (result < 0 && resultdos > 0)
                        {//validacion correcta

                            relationship = datemenor.ToString("dd-MM-yyyy") + " es menor than " + dDate.ToString("dd-MM-yyyy");
                        }
                        else if (result == 0)
                        {

                            itemTexbox.Text = "";
                            itemTexbox.Focus();
                            MessageBox.Show("Fecha incorrecta");
                        }
                        else
                        {

                            itemTexbox.Text = "";
                            itemTexbox.Focus();
                            MessageBox.Show("Fecha incorrecta");
                        }
                    }
                    else
                    {
                        itemTexbox.Text = "";
                        itemTexbox.Focus();
                        MessageBox.Show("Fecha incorrecta");
                    }
                }

            }
            catch (Exception ex)
            {
                itemTexbox.Text = "";
                MessageBox.Show("Fecha incorrecta");
            }



        }

        public String validafechacorecta(String Fechaentrada, String sFormatoentrada, String sFechaSalida)
        {//verificamos que es una fecha valida y la convertimos a un formato date mysql
            String sFechasalida = "";
            try
            {
                sFechasalida = DateTime.ParseExact(Fechaentrada, sFormatoentrada, CultureInfo.InvariantCulture).ToString(sFechaSalida); //tbDocumentofecharecepcion.Text;
            }
            catch (Exception E)
            {
                sFechasalida = "";
            }
            return sFechasalida;
        }

        public void validafechnomayoralaactual(TextBox itemTexbox) {
            try
            {
                if (itemTexbox.Text != "")
                {
                    DateTime dDate;
                    if (DateTime.TryParse(itemTexbox.Text, out dDate))
                    {
                        DateTime datemenor = new DateTime(1900, 1, 1, 0, 0, 0);//fecha menor 01/01/1900
                        DateTime datemayor = DateTime.Now;//fecha actual

                        int result = DateTime.Compare(datemenor, dDate);
                        int resultdos = DateTime.Compare(datemayor, dDate);


                        string relationship;

                        if (resultdos > 0)
                        {//validacion correcta

                            relationship = datemenor.ToString("dd-MM-yyyy") + " es menor than " + dDate.ToString("dd-MM-yyyy");
                        } else {

                            itemTexbox.Text = "";
                            MessageBox.Show("Fecha incorrecta");
                        }
                    }
                    else
                    {
                        itemTexbox.Text = "";
                        MessageBox.Show("Fecha incorrecta");
                    }
                }

            }
            catch (Exception ex)
            {
                itemTexbox.Text = "";
                MessageBox.Show("Fecha incorrecta");
            }
        }

        public String validafechavacia(String fecha)
        {
            if (fecha != "")
            {
                if (fecha.Contains("0000"))
                {
                    return fecha = "";
                }
                return fecha.Substring(0, 10);
            }
            else
            {
                return "";
            }
        }

        public void activaaviso(TextBox tbboxavisoprueba) {

            try {
                string[] lineas = null;
                String strRutaArchivo = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string fichero = strRutaArchivo + "\\casosking\\confacturador.prop";
                string contenido = String.Empty;
                if (File.Exists(fichero))
                {
                    contenido = File.ReadAllText(fichero);
                    lineas = contenido.Split('\n');
                }
                String sNombrebase = lineas[1];
                bool containsSearchResult = sNombrebase.Contains("prueba");
                if (containsSearchResult)
                {
                    tbboxavisoprueba.Visible = true;
                }
                else
                {
                    tbboxavisoprueba.Visible = false;
                }
            }
            catch (Exception exs) {
                new filelog("Mensaje: ", exs.Message);
            }
        }


    }
    class Patente { //definimos el contenido de lo que puede contener una patente
        public String CasoId { get; set; }
        public String TipoSolicitudId { get; set; }
        public String SubTipoSolicitudId { get; set; }
        public String TipoPctId { get; set; }
        public String CasoTituloespanol { get; set; }
        public String CasoTituloingles { get; set; }
        public String IdiomaId { get; set; }
        public String CasoFechaConcesion { get; set; }
        public String CasoFechaRecepcion { get; set; }
        public String CasoFechaVigencia { get; set; }
        public String CasoFechaPublicacionSolicitud { get; set; }
        public String CasoFechaLegal { get; set; }
        public String CasoNumConcedida { get; set; }
        public String CasoNumeroExpedienteLargo { get; set; }
        public String CasoNumero { get; set; }
        public String ResponsableId { get; set; }
        public String CasoTipoCaptura { get; set; }
        public String CasoTitular { get; set; }
        public String EstatusCasoId { get; set; }
        public String UsuarioId { get; set; }
        public String AreaImpiId { get; set; }
        public String CasoFechaInternacional { get; set; }
        public String PaisId { get; set; }
        public String CasoFechaPruebaUsoSig { get; set; }
        public String CasoFechaFilingCliente { get; set; }
        public String CasoFechaFilingSistema { get; set; }
        public String CasoFechaDivulgacionPrevia { get; set; }
        public String CasoFechaCartaCliente { get; set; }
        public String Divicionalid { get; set; }
        public String CasoDisenoClasificacion { get; set; }
        public String caso_patentecol { get; set; }
        public List<Interesado> interesado { get; set; }
        public List<Documento> Documentos { get; set; }
        public List<Prioridad> Prioridades { get; set; }
        public List<Plazo> Plazos { get; set; }
        public List<Anualidad_old> Anualidades_old { get; set; }
        public List<Anualidad_act> Anualidades_act { get; set; }

    }

    class caso_marcas {
        public String CasoId { get; set; }
        public String TipoSolicitudId { get; set; }
        public String SubTipoSolicitudId { get; set; }
        public String CasoTituloingles { get; set; }
        public String CasoTituloespanol { get; set; }
        public String IdiomaId { get; set; }
        public String CasoFechaConcesion { get; set; }
        public String CasoFechaLegal { get; set; }
        public String CasoFechaDivulgacionPrevia { get; set; }
        public String CasoFechaRecepcion { get; set; }
        public String CasoFechaVigencia { get; set; }
        public String CasoNumeroConcedida { get; set; }
        public String CasoNumeroExpedienteLargo { get; set; }
        public String CasoNumero { get; set; }
        public String ResponsableId { get; set; }
        public String TipoMarcaId { get; set; }
        public String CasoLeyendaNoReservable { get; set; }
        public String CasoFechaAlta { get; set; }
        public String CasoTipoCaptura { get; set; }
        public String CasoTitular { get; set; }
        public String CasoFechaFilingSistema { get; set; }
        public String CasoFechaFilingCliente { get; set; }
        public String CasoFechaCartaCliente { get; set; }
        public String EstatusCasoId { get; set; }
        public String UsuarioId { get; set; }
        public String PaisId { get; set; }
        public String CasoFechaPruebaUsoSig { get; set; }
        public String CasoNumConcedida { get; set; }
        public String CasoFechaprobouso { get; set; }
        public String CasoFechainiciouso { get; set; }
        public List<Interesado> interesado { get; set; }
        public List<Documento> Documentos { get; set; }
        public List<Prioridad> Prioridades { get; set; }
        public List<Plazo> Plazos { get; set; }
        public funcionesdicss objfunciones = new funcionesdicss();

        //datos marcas
        public String Clientenombre { get; set; }
        public String InteresadoNombre { get; set; }
        public String PaisClave { get; set; }
        public String referencia { get; set; }

        //datos de cartas marcas
        public String FechaReporteEspanol { get; set; }
        public String CLIENTENOMBRE = "";
        public String TitularNombre = "";
        public String AsuntoE = "";
        public String Titulo = "";
        public String CLASE = "";
        public String ReferenciaCliente = "";
        public String FechaNotificacionOficioE = "";
        public String Iniciales = "";
        public caso_marcas(String sCasoid, String sTiposolicitudid) {

            try {
                //Consultamos caso_registrodeobra
                conect con = new conect();//
                String sQuery = "Select * from caso_marcas_vw where CasoId = " + sCasoid + " and TipoSolicitudId = " + sTiposolicitudid + ";";
                MySqlDataReader respuesta = con.getdatareader(sQuery);
                while (respuesta.Read())
                {
                    CasoId = objfunciones.validareader("CasoId", "CasoId", respuesta).Text;
                    TipoSolicitudId = objfunciones.validareader("TipoSolicitudId", "TipoSolicitudId", respuesta).Text;
                    SubTipoSolicitudId = objfunciones.validareader("SubTipoSolicitudId", "SubTipoSolicitudId", respuesta).Text;
                    CasoTituloingles = objfunciones.validareader("CasoTituloingles", "CasoTituloingles", respuesta).Text;
                    CasoTituloespanol = objfunciones.validareader("CasoTituloespanol", "CasoTituloespanol", respuesta).Text;
                    IdiomaId = objfunciones.validareader("IdiomaId", "IdiomaId", respuesta).Text;
                    CasoFechaConcesion = objfunciones.validareader("CasoFechaConcesion", "CasoFechaConcesion", respuesta).Text;
                    CasoFechaLegal = objfunciones.validareader("CasoFechaLegal", "CasoFechaLegal", respuesta).Text;
                    CasoFechaDivulgacionPrevia = objfunciones.validareader("CasoFechaDivulgacionPrevia", "CasoFechaDivulgacionPrevia", respuesta).Text;
                    CasoFechaRecepcion = objfunciones.validareader("CasoFechaRecepcion", "CasoFechaRecepcion", respuesta).Text;
                    CasoFechaVigencia = objfunciones.validareader("CasoFechaVigencia", "CasoFechaVigencia", respuesta).Text;
                    CasoNumeroConcedida = objfunciones.validareader("CasoNumeroConcedida", "CasoNumeroConcedida", respuesta).Text;
                    CasoNumeroExpedienteLargo = objfunciones.validareader("CasoNumeroExpedienteLargo", "CasoNumeroExpedienteLargo", respuesta).Text;
                    CasoNumero = objfunciones.validareader("CasoNumero", "CasoNumero", respuesta).Text;
                    ResponsableId = objfunciones.validareader("ResponsableId", "ResponsableId", respuesta).Text;
                    TipoMarcaId = objfunciones.validareader("TipoMarcaId", "TipoMarcaId", respuesta).Text;
                    CasoLeyendaNoReservable = objfunciones.validareader("CasoLeyendaNoReservable", "CasoLeyendaNoReservable", respuesta).Text;
                    CasoFechaAlta = objfunciones.validareader("CasoFechaAlta", "CasoFechaAlta", respuesta).Text;
                    CasoTipoCaptura = objfunciones.validareader("CasoTipoCaptura", "CasoTipoCaptura", respuesta).Text;
                    CasoTitular = objfunciones.validareader("CasoTitular", "CasoTitular", respuesta).Text;
                    CasoFechaFilingSistema = objfunciones.validareader("CasoFechaFilingSistema", "CasoFechaFilingSistema", respuesta).Text;
                    CasoFechaFilingCliente = objfunciones.validareader("CasoFechaFilingCliente", "CasoFechaFilingCliente", respuesta).Text;
                    CasoFechaCartaCliente = objfunciones.validareader("CasoFechaCartaCliente", "CasoFechaCartaCliente", respuesta).Text;
                    EstatusCasoId = objfunciones.validareader("EstatusCasoId", "EstatusCasoId", respuesta).Text;
                    UsuarioId = objfunciones.validareader("UsuarioId", "UsuarioId", respuesta).Text;
                    PaisId = objfunciones.validareader("PaisId", "PaisId", respuesta).Text;
                    CasoFechaPruebaUsoSig = objfunciones.validareader("CasoFechaPruebaUsoSig", "CasoFechaPruebaUsoSig", respuesta).Text;
                    CasoNumConcedida = objfunciones.validareader("CasoNumConcedida", "CasoNumConcedida", respuesta).Text;
                    CasoFechaprobouso = objfunciones.validareader("CasoFechaprobouso", "CasoFechaprobouso", respuesta).Text;
                    CasoFechainiciouso = objfunciones.validareader("CasoFechainiciouso", "CasoFechainiciouso", respuesta).Text;

                    Clientenombre = objfunciones.validareader("Clientenombre", "Clientenombre", respuesta).Text;
                    InteresadoNombre = objfunciones.validareader("InteresadoNombre", "InteresadoNombre", respuesta).Text;
                    PaisClave = objfunciones.validareader("PaisClave", "PaisClave", respuesta).Text;
                    referencia = objfunciones.validareader("referencia", "referencia", respuesta).Text;
                }
                respuesta.Close();
                con.Cerrarconexion();
                DateTime dDatetime = DateTime.Now;
                FechaReporteEspanol = dDatetime.ToString("dd-MM-yyyy");
                //CLIENTENOMBRE = "";
                //TitularNombre = "";
                //AsuntoE = "";
                //Titulo = "";
                //CLASE = "";
                //ReferenciaCliente = "";
                //FechaNotificacionOficioE = "";
                //Iniciales = "";
            }
            catch (Exception Ex) {
                new filelog("caso_cartas", "" + Ex);
            }

        }



        public String getAtributo(String scampo)
        {
            String sValor = "";
            try {

                switch (scampo)
                {
                    case "CasoId":
                        { sValor = CasoId; }
                        break;
                    case "TipoSolicitudId":
                        { sValor = TipoSolicitudId; }
                        break;
                    case "SubTipoSolicitudId":
                        { sValor = SubTipoSolicitudId; }
                        break;
                    case "CasoTituloingles":
                        { sValor = CasoTituloingles; }
                        break;
                    case "CasoTituloespanol":
                        { sValor = CasoTituloespanol; }
                        break;
                    case "IdiomaId":
                        { sValor = IdiomaId; }
                        break;
                    case "CasoFechaConcesion":
                        { sValor = CasoFechaConcesion; }
                        break;
                    case "CasoFechaLegal":
                        { sValor = CasoFechaLegal; }
                        break;
                    case "CasoFechaDivulgacionPrevia":
                        { sValor = CasoFechaDivulgacionPrevia; }
                        break;
                    case "CasoFechaRecepcion":
                        { sValor = CasoFechaRecepcion; }
                        break;
                    case "CasoFechaVigencia":
                        { sValor = CasoFechaVigencia; }
                        break;
                    case "CasoNumeroConcedida":
                        { sValor = CasoNumeroConcedida; }
                        break;
                    case "CasoNumeroExpedienteLargo":
                        { sValor = CasoNumeroExpedienteLargo; }
                        break;
                    case "CasoNumero":
                        { sValor = CasoNumero; }
                        break;
                    case "ResponsableId":
                        { sValor = ResponsableId; }
                        break;
                    case "TipoMarcaId":
                        { sValor = TipoMarcaId; }
                        break;
                    case "CasoLeyendaNoReservable":
                        { sValor = CasoLeyendaNoReservable; }
                        break;
                    case "CasoFechaAlta":
                        { sValor = CasoFechaAlta; }
                        break;
                    case "CasoTipoCaptura":
                        { sValor = CasoTipoCaptura; }
                        break;
                    case "CasoTitular":
                        { sValor = CasoTitular; }
                        break;
                    case "CasoFechaFilingSistema":
                        { sValor = CasoFechaFilingSistema; }
                        break;
                    case "CasoFechaFilingCliente":
                        { sValor = CasoFechaFilingCliente; }
                        break;
                    case "CasoFechaCartaCliente":
                        { sValor = CasoFechaCartaCliente; }
                        break;
                    case "EstatusCasoId":
                        { sValor = EstatusCasoId; }
                        break;
                    case "UsuarioId":
                        { sValor = UsuarioId; }
                        break;
                    case "PaisId":
                        { sValor = PaisId; }
                        break;
                    case "CasoFechaPruebaUsoSig":
                        { sValor = CasoFechaPruebaUsoSig; }
                        break;
                    case "CasoNumConcedida":
                        { sValor = CasoNumConcedida; }
                        break;
                    case "CasoFechaprobouso":
                        { sValor = CasoFechaprobouso; }
                        break;
                    case "CasoFechainiciouso":
                        { sValor = CasoFechainiciouso; }
                        break;
                    case "Clientenombre":
                        { sValor = Clientenombre; }
                        break;
                    case "InteresadoNombre":
                        { sValor = InteresadoNombre; }
                        break;
                    case "PaisClave":
                        { sValor = PaisClave; }
                        break;
                    case "referencia":
                        { sValor = referencia; }
                        break;
                    case "FechaReporteEspanol":
                        { sValor = FechaReporteEspanol; }
                        break;
                        //FechaReporteEspanol
                }
            } catch (Exception ex) {
                new filelog("getcampomarcas", "" + ex.Message);
            }

            return sValor;
        }
    }
    class view_caso_marcas {
        //public String[] sCamposmarcas;
        public funcionesdicss objfunciones = new funcionesdicss();
        public List<String[]> sValorescampos = new List<String[]>();
        public view_caso_marcas(String sCasoid, String sTiposolicitudid, String sIdidioma, String sDocumentoid = "")
        {
            try {
                List<string> sCampos = new List<string>();
                int iContadornumcampo = 0;

                conect con_campos = new conect();//
                String sQuery_campos = "show columns from caso_marcas_vw;";
                MySqlDataReader respuesta_campo = con_campos.getdatareader(sQuery_campos);
                while (respuesta_campo.Read())
                {
                    sCampos.Add(objfunciones.validareader("Field", "Field", respuesta_campo).Text);
                }
                respuesta_campo.Close();
                con_campos.Cerrarconexion();


                //Agregamos los campos de la Vista
                conect con = new conect();
                String sQuery = "";
                if (sIdidioma == "" || sIdidioma=="2" || sIdidioma == "0"){
                    sQuery = "SET lc_time_names = 'es_ES';Select * from caso_marcas_vw where CasoId = " + sCasoid + " and TipoSolicitudId = " + sTiposolicitudid + ";";
                }else {
                    sQuery = "SET lc_time_names = 'en_US';Select * from caso_marcas_vw_en where CasoId = " + sCasoid + " and TipoSolicitudId = " + sTiposolicitudid + ";";
                }
                
                MySqlDataReader respuesta = con.getdatareader(sQuery);
                while (respuesta.Read())
                {
                    for (int x=0; x<sCampos.Count; x++) {
                        String[] asCampo= new String[2];
                        asCampo[0] = sCampos[x];
                        asCampo[1] = objfunciones.validareader(sCampos[x], sCampos[x], respuesta).Text;
                        sValorescampos.Add(asCampo);
                    }
                }
                respuesta.Close();
                con.Cerrarconexion();

                iContadornumcampo = sCampos.Count;
                //Consultamos campos de documento
                if (sDocumentoid != "")
                {
                    conect con_doc_campos = new conect();//
                    String sQuery_doc_campos = "show columns from documento_vw;";
                    MySqlDataReader respuesta_doc_campos = con_doc_campos.getdatareader(sQuery_doc_campos);
                    while (respuesta_doc_campos.Read())
                    {
                        sCampos.Add(objfunciones.validareader("Field", "Field", respuesta_doc_campos).Text);
                    }
                    respuesta_doc_campos.Close();
                    con_doc_campos.Cerrarconexion();


                    //Agregamos los campos del documento a la lista después de la última posición
                    //Agregamos los campos de la Vista
                    conect con_doc = new conect();
                    String sQuery_doc = "";
                    if (sIdidioma == "" || sIdidioma == "2" || sIdidioma == "0")
                    {
                        sQuery_doc = "SET lc_time_names = 'es_ES';Select * from documento_vw where documentoid = " + sDocumentoid + ";";
                    }
                    else
                    {
                        sQuery_doc = "SET lc_time_names = 'en_US';Select * from documento_vw_en where documentoid = " + sDocumentoid + ";";
                    }

                    MySqlDataReader respuesta_doc = con_doc.getdatareader(sQuery_doc);
                    while (respuesta_doc.Read())
                    {
                        for (int y = iContadornumcampo; y < sCampos.Count; y++)
                        {
                            String[] asCampo = new String[2];
                            asCampo[0] = sCampos[y];
                            asCampo[1] = objfunciones.validareader(sCampos[y], sCampos[y], respuesta_doc).Text;
                            sValorescampos.Add(asCampo);
                        }
                    }
                    respuesta_doc.Close();
                    con_doc.Cerrarconexion();
                }

                //Agregamos los datos de los documentos 
            }
            catch (Exception ex) {
                new filelog("getcampomarcas", "" + ex.Message);
            }
        }

        
        public String getAtributo(String sDato) {
            try {
                for (int x=0; x< sValorescampos.Count; x++ ) {
                    String [] sValoracomparar = sValorescampos[x];
                    if (sValoracomparar[0]==sDato) {
                        return sValoracomparar[1];
                    }
                }
            }
            catch (Exception ex) {
                new filelog("getcampomarcas", "" + ex.Message);
            }
            return "";
        }
    }

    class view_caso_patentes
    {
        //public String[] sCamposmarcas;
        public funcionesdicss objfunciones = new funcionesdicss();
        public List<String[]> sValorescampos = new List<String[]>();
        public view_caso_patentes(String sCasoid, String sTiposolicitudid, String sIdidioma, String sDocumentoid = "")
        {
            try
            {
                List<string> sCampos = new List<string>();
                int iContadornumcampo = 0;

                conect con_campos = new conect();//
                String sQuery_campos = "show columns from caso_patentes_vw;";
                MySqlDataReader respuesta_campo = con_campos.getdatareader(sQuery_campos);
                while (respuesta_campo.Read())
                {
                    sCampos.Add(objfunciones.validareader("Field", "Field", respuesta_campo).Text);
                }
                respuesta_campo.Close();
                con_campos.Cerrarconexion();


                //Agregamos los campos de la Vista
                conect con = new conect();
                String sQuery = "";
                if (sIdidioma == "" || sIdidioma == "2" || sIdidioma == "0")
                {
                    sQuery = "SET lc_time_names = 'es_ES';Select * from caso_patentes_vw where CasoId = " + sCasoid + " and TipoSolicitudId = " + sTiposolicitudid + ";";
                }
                else
                {
                    sQuery = "SET lc_time_names = 'en_US';Select * from caso_patentes_vw_en where CasoId = " + sCasoid + " and TipoSolicitudId = " + sTiposolicitudid + ";";
                }

                MySqlDataReader respuesta = con.getdatareader(sQuery);
                while (respuesta.Read())
                {
                    for (int x = 0; x < sCampos.Count; x++)
                    {
                        String[] asCampo = new String[2];
                        asCampo[0] = sCampos[x];
                        asCampo[1] = objfunciones.validareader(sCampos[x], sCampos[x], respuesta).Text;
                        sValorescampos.Add(asCampo);
                    }
                }
                respuesta.Close();
                con.Cerrarconexion();

                iContadornumcampo = sCampos.Count;
                //Consultamos campos de documento
                if (sDocumentoid != "")
                {
                    conect con_doc_campos = new conect();//
                    String sQuery_doc_campos = "show columns from documento_vw;";
                    MySqlDataReader respuesta_doc_campos = con_doc_campos.getdatareader(sQuery_doc_campos);
                    while (respuesta_doc_campos.Read())
                    {
                        sCampos.Add(objfunciones.validareader("Field", "Field", respuesta_doc_campos).Text);
                    }
                    respuesta_doc_campos.Close();
                    con_doc_campos.Cerrarconexion();


                    //Agregamos los campos del documento a la lista después de la última posición
                    //Agregamos los campos de la Vista
                    conect con_doc = new conect();
                    String sQuery_doc = "";
                    if (sIdidioma == "" || sIdidioma == "2" || sIdidioma == "0")
                    {
                        sQuery_doc = "SET lc_time_names = 'es_ES';Select * from documento_vw where documentoid = " + sDocumentoid + ";";
                    }
                    else
                    {
                        sQuery_doc = "SET lc_time_names = 'en_US';Select * from documento_vw_en where documentoid = " + sDocumentoid + ";";
                    }

                    MySqlDataReader respuesta_doc = con_doc.getdatareader(sQuery_doc);
                    while (respuesta_doc.Read())
                    {
                        for (int y = iContadornumcampo; y < sCampos.Count; y++)
                        {
                            String[] asCampo = new String[2];
                            asCampo[0] = sCampos[y];
                            asCampo[1] = objfunciones.validareader(sCampos[y], sCampos[y], respuesta_doc).Text;
                            sValorescampos.Add(asCampo);
                        }
                    }
                    respuesta_doc.Close();
                    con_doc.Cerrarconexion();
                }

                //Agregamos los datos de los documentos 
            }
            catch (Exception ex)
            {
                new filelog("getcampopatente", "" + ex.Message);
            }
        }


        public String getAtributo(String sDato)
        {
            try
            {
                for (int x = 0; x < sValorescampos.Count; x++)
                {
                    String[] sValoracomparar = sValorescampos[x];
                    if (sValoracomparar[0] == sDato)
                    {
                        return sValoracomparar[1];
                    }
                }
            }
            catch (Exception ex)
            {
                new filelog("getcampopatente", "" + ex.Message);
            }
            return "";
        }
    }
    //class view_caso_marcas_en
    //{
    //    //public String[] sCamposmarcas;
    //    public funcionesdicss objfunciones = new funcionesdicss();
    //    public List<String[]> sValorescampos = new List<String[]>();
    //    public view_caso_marcas_en(String sCasoid, String sTiposolicitudid)
    //    {
    //        try
    //        {
    //            List<string> sCampos = new List<string>();

    //            conect con_campos = new conect();//
    //            String sQuery_campos = "show columns from caso_marcas_vw_en;";
    //            MySqlDataReader respuesta_campo = con_campos.getdatareader(sQuery_campos);
    //            while (respuesta_campo.Read())
    //            {
    //                sCampos.Add(objfunciones.validareader("Field", "Field", respuesta_campo).Text);
    //            }
    //            respuesta_campo.Close();
    //            con_campos.Cerrarconexion();
    //            //sCampos.Add("FechaReporteEspanol");


    //            conect con = new conect();//
    //            String sQuery = "Select * from caso_marcas_vw_en where CasoId = " + sCasoid + " and TipoSolicitudId = " + sTiposolicitudid + ";";
    //            MySqlDataReader respuesta = con.getdatareader(sQuery);
    //            while (respuesta.Read())
    //            {
    //                for (int x = 0; x < sCampos.Count; x++)
    //                {
    //                    String[] asCampo = new String[2];
    //                    asCampo[0] = sCampos[x];
    //                    asCampo[1] = objfunciones.validareader(sCampos[x], sCampos[x], respuesta).Text;
    //                    sValorescampos.Add(asCampo);
    //                }
    //            }
    //            respuesta.Close();
    //            con.Cerrarconexion();
    //        }
    //        catch (Exception ex)
    //        {
    //            new filelog("getcampomarcas", "" + ex.Message);
    //        }
    //    }
    //    public String getAtributo(String sDato)
    //    {
    //        try
    //        {
    //            for (int x = 0; x < sValorescampos.Count; x++)
    //            {
    //                String[] sValoracomparar = sValorescampos[x];
    //                if (sValoracomparar[0] == sDato)
    //                {
    //                    return sValoracomparar[1];
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            new filelog("getcampomarcas", "" + ex.Message);
    //        }
    //        return "";
    //    }
    //}


    class caso_derechoautor {
        public String CasoId { get; set; }
        public String TipoSolicitudId { get; set; }
        public String CasoTituloespanol { get; set; }
        public String CasoTituloingles { get; set; }
        public String IdiomaId { get; set; }
        public String CasoFechaLegal { get; set; }
        public String CasoFechaRecepcion { get; set; }
        public String CasoFechaConcesion { get; set; }
        public String CasoNumConcedida { get; set; }
        public String CasoNumeroExpedienteLargo { get; set; }
        public String CasoNumero { get; set; }
        public String ResponsableId { get; set; }
        public String tipo_obraid { get; set; }
        public String CasoTipoCaptura { get; set; }
        public String CasoTitular { get; set; }
        public String EstatusCasoId { get; set; }
        public String PaisId { get; set; }
        public String UsuarioId { get; set; }
        public String CasoFechaCartaCliente { get; set; }
        public String CasoFechaVigencia { get; set; }
        public String UsuarioCliente { get; set; }
        public List<Interesado> interesado { get; set; }
        public List<Documento> Documentos { get; set; }
        public List<Prioridad> Prioridades { get; set; }
        public List<Plazo> Plazos { get; set; }
        public funcionesdicss objfunciones = new funcionesdicss();

        public caso_derechoautor(String sCasoid, String sTiposolicitud){
            try
            {
                //Consultamos caso_registrodeobra
                conect con = new conect();//
                String sQuery = "Select * from caso_registroobra_view where CasoId = " + sCasoid + " and TipoSolicitudId = " + sTiposolicitud + ";";
                MySqlDataReader respuesta = con.getdatareader(sQuery);
                while (respuesta.Read())
                {
                    CasoId = objfunciones.validareader("CasoId", "CasoId", respuesta).Text;
                    TipoSolicitudId = objfunciones.validareader("TipoSolicitudId", "CasoId", respuesta).Text;
                    CasoTituloespanol = objfunciones.validareader("CasoTituloespanol", "CasoId", respuesta).Text;
                    CasoTituloingles = objfunciones.validareader("CasoTituloingles", "CasoId", respuesta).Text;
                    IdiomaId = objfunciones.validareader("IdiomaId", "CasoId", respuesta).Text;
                    CasoFechaLegal = objfunciones.validareader("CasoFechaLegal", "CasoId", respuesta).Text;
                    CasoFechaRecepcion = objfunciones.validareader("CasoFechaRecepcion", "CasoId", respuesta).Text;
                    CasoFechaConcesion = objfunciones.validareader("CasoFechaConcesion", "CasoId", respuesta).Text;
                    CasoNumConcedida = objfunciones.validareader("CasoNumConcedida", "CasoId", respuesta).Text;
                    CasoNumeroExpedienteLargo = objfunciones.validareader("CasoNumeroExpedienteLargo", "CasoId", respuesta).Text;
                    CasoNumero = objfunciones.validareader("CasoNumero", "CasoId", respuesta).Text;
                    ResponsableId = objfunciones.validareader("ResponsableId", "CasoId", respuesta).Text;
                    tipo_obraid = objfunciones.validareader("tipo_obraid", "CasoId", respuesta).Text;
                    CasoTipoCaptura = objfunciones.validareader("CasoTipoCaptura", "CasoId", respuesta).Text;
                    CasoTitular = objfunciones.validareader("CasoTitular", "CasoId", respuesta).Text;
                    EstatusCasoId = objfunciones.validareader("EstatusCasoId", "CasoId", respuesta).Text;
                    PaisId = objfunciones.validareader("PaisId", "CasoId", respuesta).Text;
                    UsuarioId = objfunciones.validareader("UsuarioId", "CasoId", respuesta).Text;
                    CasoFechaCartaCliente = objfunciones.validareader("CasoFechaCartaCliente", "CasoId", respuesta).Text;
                    CasoFechaVigencia = objfunciones.validareader("CasoFechaVigencia", "CasoId", respuesta).Text;
                    UsuarioCliente = objfunciones.validareader("UsuarioCliente", "CasoId", respuesta).Text;
                    
                }
                respuesta.Close();
                con.Cerrarconexion();

                //Consultamos documentos
                conect con_doc = new conect();//
                String sQuery_doc = "Select * from caso_registroobra_view where CasoId = " + sCasoid + " and TipoSolicitudId = " + sTiposolicitud + ";";
                MySqlDataReader respuesta_doc = con_doc.getdatareader(sQuery_doc);
                while (respuesta_doc.Read())
                {
                    
                }
                respuesta_doc.Close();
                con_doc.Cerrarconexion();
            }
            catch (Exception ex)
            {

            }
        }
    }

    class caso_reservaderecho {
        public String CasoId { get; set; }
        public String TipoSolicitudId { get; set; }
        public String id_tipo_reservaespecie { get; set; }
        public String CasoTituloespanol { get; set; }
        public String CasoTituloingles { get; set; }
        public String CasoFechaLegal { get; set; }
        public String CasoFechaRecepcion { get; set; }
        public String CasoFechaVigencia { get; set; }
        public String CasoFechaConcesion { get; set; }
        public String CasoNumConcedida { get; set; }
        public String CasoNumeroExpedienteLargo { get; set; }
        public String CasoNumero { get; set; }
        public String ResponsableId { get; set; }
        public String TipoMarcaId { get; set; }
        public String CasoTipoCaptura { get; set; }
        public String CasoTitular { get; set; }
        public String EstatusCasoId { get; set; }
        public String PaisId { get; set; }
        public String UsuarioId { get; set; }
        public String TipoReservaId { get; set; }
        public String SubTipoReservaId { get; set; }
        public List<Interesado> interesado { get; set; }
        public List<Documento> Documentos { get; set; }
        public List<Prioridad> Prioridades { get; set; }
        public List<Plazo> Plazos { get; set; }
    }

    class caso_contencioso {
        public String CasoId { get; set; }
        public String TipoSolicitudId { get; set; }
        public String SubTipoSolicitudId { get; set; }
        public String CasoTituloespanol { get; set; }
        public String CasoTituloingles { get; set; }
        public String IdiomaId { get; set; }
        public String CasoFechaConcesion { get; set; }
        public String CasoFechaLegal { get; set; }
        public String CasoFechaPresentacion { get; set; }
        public String CasoFechaDivulgacionPrevia { get; set; }
        public String CasoFechaRecepcion { get; set; }
        public String CasoFechaVigencia { get; set; }
        public String CasoNumConcedida { get; set; }
        public String CasoNumeroExpedienteLargo { get; set; }
        public String CasoNumero { get; set; }
        public String ResponsableId { get; set; }
        public String TipoMarcaId { get; set; }
        public String CasoFechaAlta { get; set; }
        public String CasoTipoCaptura { get; set; }
        public String CasoTitular { get; set; }
        public String CasoFechaFilingSistema { get; set; }
        public String CasoFechaFilingCliente { get; set; }
        public String CasoFechaCartaCliente { get; set; }
        public String EstatusCasoId { get; set; }
        public String UsuarioId { get; set; }
        public String PaisId { get; set; }
        public String CasoFechaPruebaUsoSig { get; set; }
        public String TipoReservaId { get; set; }
        public String ParteRepresentadaId { get; set; }
        public String SentidoResolucionId { get; set; }
        public String CasoFechaResolucion { get; set; }
        public String CasoEncargadoExterno { get; set; }
        public String caso_contenciosocol { get; set; }
        public List<Interesado> interesado { get; set; }
        public List<Documento> Documentos { get; set; }
        public List<Prioridad> Prioridades { get; set; }
        public List<Plazo> Plazos { get; set; }
    }

    class Anualidad_act{
        public String Anialidades_MD_nuevosid { get; set; }
        public String casoid { get; set; }
        public String TipoSolicitudId { get; set; }
        public String secuencia { get; set; }
        public String periodo { get; set; }
        public String estatusanualidad { get; set; }
        public String fechalimite { get; set; }
        public String fecha_pago { get; set; }

    }
    class Anualidad_old {
        public String AnualidadId { get; set; }
        public String AnualidadSecuencia { get; set; }
        public String AnualidadIndExe { get; set; }
        public String AnualidadAno { get; set; }
        public String AnualidadMes { get; set; }
        public String AnualidadQuinquenio { get; set; }
        public String EstatusAnualidadId { get; set; }
        public String CasoId { get; set; }
        public String TipoSolicitudId { get; set; }
        public String AnualidadFechaPago { get; set; }
        public String AnualidadFechaLimitePago { get; set; }
        public String AnualidadFechaFinVigencia { get; set; }
        public String AnualidadTipo { get; set; }
    }
    class Plazo{
        public String casoid { get; set; }
        public String Plazos_detalleid { get; set; }
        public String TipoSolicitudId { get; set; }
        public String Plazosid { get; set; }
        public String Capturo { get; set; }
        public String Documento { get; set; }
        public String Tipo_plazo_IMPI { get; set; }
        public String Estatus_plazo_impi { get; set; }
        public String Fecha_notificacion_impi { get; set; }
        public String Fecha_Vencimiento_regular_impi { get; set; }
        public String Fecha_vencimiento_3m_impi { get; set; }
        public String Fecha_vencimiento_4m_impi { get; set; }
        public String Fecha_atendio_plazo_impi { get; set; }
        public String Fecha_atendio_plazo_impi_sistema { get; set; }
        public String atendio_plazoimpi { get; set; }
        public String Motivo_cancelacion_plazo_impi { get; set; }
        public String Fecha_cancelacion_plazo_impi { get; set; }
        public String Usuariocancelo { get; set; }
        public String Doc_atendio { get; set; }
    }
    class Prioridad
    {
        public String PrioridadId { get; set; }
        public String CasoId { get; set; }
        public String TipoSolicitudId { get; set; }
        public String PrioridadNumero { get; set; }
        public String PaisID { get; set; }
        public String PrioridadFecha { get; set; }
        public String TipoPrioridadId { get; set; }
    }

    class Documento {
        public String DocumentoId { get; set; }
        public String DocumentoCodigoBarras { get; set; }
        public String SubTipoDocumentoId { get; set; }
        public String DocumentoFecha { get; set; }
        public String DocumentoFolio { get; set; }
        public String DocumentoFechaRecepcion { get; set; }
        public String DocumentoFechaVencimiento { get; set; }
        public String DocumentoFechaCaptura { get; set; }
        public String DocumentoFechaEscaneo { get; set; }
        public String DocumentoObservacion { get; set; }
        public String DocumentoIdRef { get; set; }
        public String UsuarioId { get; set; }
        public String CompaniaMensajeriaId { get; set; }
        public String DocumentoFechaEnvio { get; set; }
        public String DocumentoNumeroGuia { get; set; }
        public String DocumentoFechaEntrega { get; set; }
        public String usuarioIdPreparo { get; set; }
    }

    class Interesado {
        public String InteresadoID { get; set; }
        public String InteresadoTipoPersonaSAT { get; set; }
        public String InteresadoNombre { get; set; }
        public String InteresadoApPaterno { get; set; }
        public String InteresadoApMaterno { get; set; }
        public String InteresadoRFC { get; set; }
        public String SociedadID { get; set; }
        public String InteresadoRGP { get; set; }
        public String InteresadoFechaAlta { get; set; }
        public String PaisId { get; set; }
        public String InteresadoIndAct { get; set; }
        public String InteresadoShort { get; set; }
        public String InteresadoPoder { get; set; }
        public String InteresadoCurp { get; set; }
        public String InteresadoMail { get; set; }
        public String InteresadoTelefono { get; set; }
        public String holderid { get; set; }
    }

    class view_caso_contencioso
    {
        //public String[] sCamposmarcas;
        public funcionesdicss objfunciones = new funcionesdicss();
        public List<String[]> sValorescampos = new List<String[]>();
        public view_caso_contencioso(String sCasoid, String sTiposolicitudid, String sIdidioma, String sDocumentoid = "")
        {
            try
            {
                List<string> sCampos = new List<string>();
                int iContadornumcampo = 0;

                conect con_campos = new conect();//
                String sQuery_campos = "show columns from caso_contencioso_vw;";
                MySqlDataReader respuesta_campo = con_campos.getdatareader(sQuery_campos);
                while (respuesta_campo.Read())
                {
                    sCampos.Add(objfunciones.validareader("Field", "Field", respuesta_campo).Text);
                }
                respuesta_campo.Close();
                con_campos.Cerrarconexion();


                //Agregamos los campos de la Vista
                conect con = new conect();
                String sQuery = "";
                if (sIdidioma == "" || sIdidioma == "2" || sIdidioma == "0")
                {
                    sQuery = "SET lc_time_names = 'es_ES';Select * from caso_contencioso_vw where CasoId = " + sCasoid + " and TipoSolicitudId = " + sTiposolicitudid + ";";
                }
                else
                {
                    sQuery = "SET lc_time_names = 'en_US';Select * from caso_contencioso_vw_en where CasoId = " + sCasoid + " and TipoSolicitudId = " + sTiposolicitudid + ";";
                }

                MySqlDataReader respuesta = con.getdatareader(sQuery);
                while (respuesta.Read())
                {
                    for (int x = 0; x < sCampos.Count; x++)
                    {
                        String[] asCampo = new String[2];
                        asCampo[0] = sCampos[x];
                        asCampo[1] = objfunciones.validareader(sCampos[x], sCampos[x], respuesta).Text;
                        sValorescampos.Add(asCampo);
                    }
                }
                respuesta.Close();
                con.Cerrarconexion();

                iContadornumcampo = sCampos.Count;
                //Consultamos campos de documento
                if (sDocumentoid != "")
                {
                    conect con_doc_campos = new conect();//
                    String sQuery_doc_campos = "show columns from documento_vw;";
                    MySqlDataReader respuesta_doc_campos = con_doc_campos.getdatareader(sQuery_doc_campos);
                    while (respuesta_doc_campos.Read())
                    {
                        sCampos.Add(objfunciones.validareader("Field", "Field", respuesta_doc_campos).Text);
                    }
                    respuesta_doc_campos.Close();
                    con_doc_campos.Cerrarconexion();


                    //Agregamos los campos del documento a la lista después de la última posición
                    //Agregamos los campos de la Vista
                    conect con_doc = new conect();
                    String sQuery_doc = "";
                    if (sIdidioma == "" || sIdidioma == "2" || sIdidioma == "0")
                    {
                        sQuery_doc = "SET lc_time_names = 'es_ES';Select * from documento_vw where documentoid = " + sDocumentoid + ";";
                    }
                    else
                    {
                        sQuery_doc = "SET lc_time_names = 'en_US';Select * from documento_vw_en where documentoid = " + sDocumentoid + ";";
                    }

                    MySqlDataReader respuesta_doc = con_doc.getdatareader(sQuery_doc);
                    while (respuesta_doc.Read())
                    {
                        for (int y = iContadornumcampo; y < sCampos.Count; y++)
                        {
                            String[] asCampo = new String[2];
                            asCampo[0] = sCampos[y];
                            asCampo[1] = objfunciones.validareader(sCampos[y], sCampos[y], respuesta_doc).Text;
                            sValorescampos.Add(asCampo);
                        }
                    }
                    respuesta_doc.Close();
                    con_doc.Cerrarconexion();
                }

                //Agregamos los datos de los documentos 
            }
            catch (Exception ex)
            {
                new filelog("getcampocontencioso", "" + ex.Message);
            }
        }


        public String getAtributo(String sDato)
        {
            try
            {
                for (int x = 0; x < sValorescampos.Count; x++)
                {
                    String[] sValoracomparar = sValorescampos[x];
                    if (sValoracomparar[0] == sDato)
                    {
                        return sValoracomparar[1];
                    }
                }
            }
            catch (Exception ex)
            {
                new filelog("getcampomarcas", "" + ex.Message);
            }
            return "";
        }
    }
}
