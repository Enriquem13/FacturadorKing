using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace Facturador
{
    public partial class Solicituddepatente2019 : Form
    {
        public Form1 loguin;
        public String sgCasooid;
        funcionesdicss dicssdfunctions = new funcionesdicss();
        consultacaso fGtpatentes;
        public bool sbandera = true;
        public Solicituddepatente2019(Form1 login, String sCasoid, consultacaso formpatentes)
        {
            loguin = login;
            sgCasooid = sCasoid;
            fGtpatentes = formpatentes;
            InitializeComponent();
        }

        private void BT_cancelarsolicitud_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void CheckB2anexosolicitud_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckB2anexosolicitud.Checked)
            {
                TB_anexo2.Enabled = true;
            }
            else
            {
                TB_anexo2.Enabled = false;
                TB_anexo2.Text = "";
            }
        }

        private void CheckB3anexosolicitud_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckB3anexosolicitud.Checked)
            {
                
                TB_anexo3.Enabled = true;
            }
            else
            {
                TB_anexo3.Enabled = false;
                TB_anexo3.Text = "";
            }
        }

        private void CheckB4anexosolicitud_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckB4anexosolicitud.Checked)
            {
                
                TB_anexo4.Enabled = true;
            }
            else
            {
                TB_anexo4.Enabled = false;
                TB_anexo4.Text = "";
            }
        }

        private void CheckB5anexosolicitud_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckB5anexosolicitud.Checked)
            {

                TB_anexo5.Enabled = true;
            }
            else
            {
                TB_anexo5.Enabled = false;
                TB_anexo5.Text = "";
            }
        }

        private void CheckB6anexosolicitud_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckB6anexosolicitud.Checked)
            {

                TB_anexo6.Enabled = true;
            }
            else
            {
                TB_anexo6.Enabled = false;
                TB_anexo6.Text = "";
            }
        }

        private void CheckB7anexosolicitud_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckB7anexosolicitud.Checked)
            {

                TB_anexo7.Enabled = true;
            }
            else
            {
                TB_anexo7.Enabled = false;
                TB_anexo7.Text = "";
            }
        }

        private void CheckB8anexosolicitud_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckB8anexosolicitud.Checked)
            {

                TB_anexo8.Enabled = true;
            }
            else
            {
                TB_anexo8.Enabled = false;
                TB_anexo8.Text = "";
            }
        }

        private void CheckB9anexosolicitud_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckB9anexosolicitud.Checked)
            {

                TB_anexo9.Enabled = true;
            }
            else
            {
                TB_anexo9.Enabled = false;
                TB_anexo9.Text = "";
            }
        }

        private void CheckB10anexosolicitud_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckB10anexosolicitud.Checked)
            {

                TB_anexo10.Enabled = true;
            }
            else
            {
                TB_anexo10.Enabled = false;
                TB_anexo10.Text = "";
            }
        }

        private void CheckB11anexosolicitud_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckB11anexosolicitud.Checked)
            {

                TB_anexo11.Enabled = true;
            }
            else
            {
                TB_anexo11.Enabled = false;
                TB_anexo11.Text = "";
            }
        }

        private void CheckB12anexosolicitud_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckB12anexosolicitud.Checked)
            {

                TB_anexo12.Enabled = true;
            }
            else
            {
                TB_anexo12.Enabled = false;
                TB_anexo12.Text = "";
            }
        }

        private void CheckB13anexosolicitud_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckB13anexosolicitud.Checked)
            {

                TB_anexo13.Enabled = true;
            }
            else
            {
                TB_anexo13.Enabled = false;
                TB_anexo13.Text = "";
            }
        }

        private void CheckB14anexosolicitud_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckB14anexosolicitud.Checked)
            {

                TB_anexo14.Enabled = true;
            }
            else
            {
                TB_anexo14.Enabled = false;
                TB_anexo14.Text = "";
            }
        }



    
        private void CheckB17anexosolicitud_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckB17anexosolicitud.Checked)
            {

                TB_anexo17.Enabled = true;
            }
            else
            {
                TB_anexo17.Enabled = false;
                TB_anexo17.Text = "";
            }
        }


        public void generaSolictudpatente() {

            
        }
        
        private void BT_generar_solicitud_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbNoAgregar.Checked || rbAgregar.Checked)
                {
                    bool bAgregarfechadiv = false;
                    if (rbAgregar.Checked && dicssdfunctions.validafechacorectaformato(tbFechaDiculgacion.Text, "dd-MM-yyyy", "yyyy'/'MM'/'dd") == "")
                    {
                        MessageBox.Show("Debe agregar la Fecha de Divulgación válida");
                        return;
                    }
                    else {
                        bAgregarfechadiv = true;
                    }

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
                        String sruta_plantilla = confilepth.sFileupload + @"\formatosconfigurables\nuevasolicitudpatente.docx";
                        String carpeta = "C:\\Formatos_CasosKing";
                        //si no existe la carpeta temporal la creamos
                        if (!(Directory.Exists(carpeta)))
                        {
                            Directory.CreateDirectory(carpeta);
                        }
                        Random r = new Random();
                        int srandonm = r.Next(9, 9999);
                        String sCasonumero = fGtpatentes.lCasoNumero_texbox.Text;
                        String sArchivogenerado = carpeta + "\\Solicitudpatente Numcaso " + sCasonumero + " " + srandonm + ".docx";
                        File.Copy(sruta_plantilla, sArchivogenerado);
                        //abrimos el archivo temporal y lo reemplzamos con los datos
                        Word.Application application = new Word.Application();
                        Word.Document document = application.Documents.Open(sArchivogenerado);
                        //consultamos la cdireccion
                        conect con_3 = new conect();
                        String squerydatoficinas = "select DameNombrePais(oficina.OficinaPaisId) as OficinaPaisId, oficina.*,  apoderado.* from oficina, apoderado limit 1;";
                        MySqlDataReader resp_datofi = con_3.getdatareader(squerydatoficinas);
                        String OficinaCP = "";
                        String OficinaCalle = "";
                        String OficinaNumExt = "";
                        String OficinaNumInt = "";
                        String OficinaColonia = "";
                        String OficinaMunicipio = "";
                        String OficinaEstado = "";
                        String OficinaPaisId = "";
                        String OficinaTelefono = "";
                        String OficinaCorreo = "";
                        String ApoderadoNonbre = "";
                        String ApoderadoApellidoPat = "";
                        String ApoderadoApellidoMat = "";
                        String AutorizadoNombre = "";
                        String AutorizadApellidoPat = "";
                        String AutorizadoApellidoMat = "";
                        while (resp_datofi.Read())
                        {
                            OficinaCP = dicssdfunctions.validareader("OficinaCP", "id_oficina", resp_datofi).Text;
                            OficinaCalle = dicssdfunctions.validareader("OficinaCalle", "id_oficina", resp_datofi).Text;
                            OficinaNumExt = dicssdfunctions.validareader("OficinaNumExt", "id_oficina", resp_datofi).Text;
                            OficinaNumInt = dicssdfunctions.validareader("OficinaNumInt", "id_oficina", resp_datofi).Text;
                            OficinaColonia = dicssdfunctions.validareader("OficinaColonia", "id_oficina", resp_datofi).Text;
                            OficinaMunicipio = dicssdfunctions.validareader("OficinaMunicipio", "id_oficina", resp_datofi).Text;
                            OficinaEstado = dicssdfunctions.validareader("OficinaEstado", "id_oficina", resp_datofi).Text;
                            OficinaPaisId = dicssdfunctions.validareader("OficinaPaisId", "id_oficina", resp_datofi).Text;
                            OficinaTelefono = dicssdfunctions.validareader("OficinaTelefono", "id_oficina", resp_datofi).Text;
                            OficinaCorreo = dicssdfunctions.validareader("OficinaCorreo", "id_oficina", resp_datofi).Text;
                            ApoderadoNonbre = dicssdfunctions.validareader("ApoderadoNonbre", "id_oficina", resp_datofi).Text;
                            ApoderadoApellidoPat = dicssdfunctions.validareader("ApoderadoApellidoPat", "id_oficina", resp_datofi).Text;
                            ApoderadoApellidoMat = dicssdfunctions.validareader("ApoderadoApellidoMat", "id_oficina", resp_datofi).Text;
                            AutorizadoNombre = dicssdfunctions.validareader("AutorizadoNombre", "id_oficina", resp_datofi).Text;
                            AutorizadApellidoPat = dicssdfunctions.validareader("AutorizadApellidoPat", "id_oficina", resp_datofi).Text;
                            AutorizadoApellidoMat = dicssdfunctions.validareader("AutorizadoApellidoMat", "id_oficina", resp_datofi).Text;
                        }
                        resp_datofi.Close();
                        con_3.Cerrarconexion();
                        //agregamos fecha de divulgación si se seleccionó y si se agrego y si es válida
                        if (bAgregarfechadiv && rbAgregar.Checked)
                        {
                            String[] fechadivulgacion = tbFechaDiculgacion.Text.Split('-');
                            document.Bookmarks["dd_fecdivulgprev"].Select();
                            application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fechadivulgacion[0]));

                            document.Bookmarks["mm_fecdivulgprev"].Select();
                            application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fechadivulgacion[1]));

                            document.Bookmarks["yyyy_fecdivulgprev"].Select();
                            application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fechadivulgacion[2]));
                        }

                        //validamos que tipo de solicitud es
                        switch (fGtpatentes.gSTipoSolicitudId)
                        {
                            case "1"://Solicitud de patente
                                {
                                    document.Bookmarks["sol_patinvencion"].Select();
                                    application.Selection.TypeText("X");
                                    if (fGtpatentes.gSSubTipoSolicitudId == "1")
                                    {
                                        document.Bookmarks["sol_patinvencion_pct"].Select();
                                        application.Selection.TypeText("X");
                                    }
                                } break;
                            case "2"://Sol. registro Modelo Utilidad
                                {
                                    document.Bookmarks["sol_modutilidad"].Select();
                                    application.Selection.TypeText("X");
                                    if (fGtpatentes.gSSubTipoSolicitudId == "1")
                                    {
                                        document.Bookmarks["sol_modutilidad_pct"].Select();
                                        application.Selection.TypeText("X");
                                    }
                                } break;
                            case "3"://Sol. registro Dis. industrial (Modelo)
                                {
                                    document.Bookmarks["sol_mod_inductrial"].Select();
                                    application.Selection.TypeText("X");

                                    document.Bookmarks["sol_disind"].Select();
                                    application.Selection.TypeText("X");
                                } break;
                            case "4"://Sol. registro Dis. industrial (Dibujo Ind)
                                {
                                    document.Bookmarks["sol_dibujo_industria"].Select();
                                    application.Selection.TypeText("X");

                                    document.Bookmarks["sol_disind"].Select();
                                    application.Selection.TypeText("X");

                                } break;
                            //case "5"://Solicitud de registro de patente PCT
                            //    {
                            //        document.Bookmarks["Avisocomercial"].Select();
                            //        application.Selection.TypeText("X");
                            //    } break;
                            //case "19"://Esquema trazado de Circuito
                            //    {
                            //        document.Bookmarks["Avisocomercial"].Select();
                            //        application.Selection.TypeText("X");

                            //    } break;
                        }



                        //lvinteresados
                        //validamos si la persona es Persona fisica o persona Moral para guardar los datos

                        //Console.WriteLine("Resultado: "+results);
                        String[] interesadostitulares = Array.FindAll(fGtpatentes.TipoRelacionId, s => s.Equals("1"));
                        String[] interesadostitularesinventor = Array.FindAll(fGtpatentes.TipoRelacionId, s => s.Equals("3"));
                        int numtitularestotales = interesadostitulares.Length + interesadostitularesinventor.Length;
                        int iContadorinventor = 1;
                        bool sBanderaanexointeresados = false;
                        bool bBandera = true;

                        for (int contx = 0; contx < fGtpatentes.TipoRelacionId.Length; contx++)
                        {
                            //1 o 3 significa que son titulares
                            if (fGtpatentes.TipoRelacionId[contx] == "1" || fGtpatentes.TipoRelacionId[contx] == "3")
                            { //escribimos en el documento con los marcadores
                                switch (fGtpatentes.lvinteresados.Items[contx].SubItems[7].Text)
                                {
                                    case "Moral Extranjera":
                                        {
                                            document.Bookmarks["PM_RFC"].Select();
                                            application.Selection.TypeText(fGtpatentes.InteresadoRFC[contx]);

                                            document.Bookmarks["PM_Razonsocial"].Select();
                                            application.Selection.TypeText(fGtpatentes.InteresadoNombre[contx]);

                                            document.Bookmarks["PM_Nacionalidad"].Select();
                                            application.Selection.TypeText(fGtpatentes.nacionalidad[contx]);

                                            document.Bookmarks["PM_telefono"].Select();
                                            application.Selection.TypeText(OficinaTelefono);


                                            document.Bookmarks["PM_correo"].Select();
                                            application.Selection.TypeText(OficinaCorreo);

                                            if (numtitularestotales > 1)
                                            {
                                                document.Bookmarks["PM_anexo"].Select();
                                                application.Selection.TypeText("X");
                                                //generar el anexo de interesados solicitantes
                                                document.Bookmarks["x_anexo_15"].Select();
                                                application.Selection.TypeText("X");
                                                sBanderaanexointeresados = true;

                                            }
                                            //si es persona nacional colocamos en municipio
                                            document.Bookmarks["localidad_1"].Select();
                                            application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionPoblacion[contx]));
                                        } break;
                                    case "Física Extranjera":
                                        {
                                            document.Bookmarks["PF_curp"].Select();
                                            application.Selection.TypeText(fGtpatentes.InteresadoCurp[contx]);

                                            document.Bookmarks["PF_Nombres"].Select();
                                            application.Selection.TypeText(fGtpatentes.InteresadoNombre[contx]);

                                            document.Bookmarks["PF_Primerapellido"].Select();
                                            application.Selection.TypeText(fGtpatentes.InteresadoApPaterno[contx]);

                                            document.Bookmarks["PF_segundoapellido"].Select();
                                            application.Selection.TypeText(fGtpatentes.InteresadoApMaterno[contx]);

                                            document.Bookmarks["PF_Nacionalidad"].Select();
                                            application.Selection.TypeText(fGtpatentes.nacionalidad[contx]);

                                            document.Bookmarks["PFTelefonoladanumext"].Select();
                                            application.Selection.TypeText(OficinaTelefono);

                                            document.Bookmarks["PF_correo"].Select();
                                            application.Selection.TypeText(OficinaCorreo);
                                            //PF_correo

                                            if (fGtpatentes.TipoRelacionId[contx] == "3")
                                            {
                                                document.Bookmarks["PF_solinventor"].Select();
                                                application.Selection.TypeText("X");
                                                //Nos dice que es un inventor y un titular, en los datos de los inventores va el segundo inventor en caso de que lo hubiera si no sólo van en estos campos
                                                //colocamos una bandera que nos ayude a manejar este punto
                                                //generar el anexo de interesados solicitantes

                                            }
                                            if (numtitularestotales > 1)
                                            {
                                                document.Bookmarks["PF_solinventor_anexo"].Select();
                                                application.Selection.TypeText("X");
                                                //generar el anexo de interesados
                                                document.Bookmarks["x_anexo_15"].Select();
                                                application.Selection.TypeText("X");

                                                sBanderaanexointeresados = true;
                                            }
                                            //si es persona nacional colocamos en municipio
                                            document.Bookmarks["localidad_1"].Select();
                                            application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionPoblacion[contx]));

                                        } break;
                                    case "Moral Nacional":
                                        {
                                            document.Bookmarks["PM_RFC"].Select();
                                            application.Selection.TypeText(fGtpatentes.InteresadoRFC[contx]);

                                            document.Bookmarks["PM_Razonsocial"].Select();
                                            application.Selection.TypeText(fGtpatentes.InteresadoNombre[contx]);

                                            document.Bookmarks["PM_Nacionalidad"].Select();
                                            application.Selection.TypeText(fGtpatentes.nacionalidad[contx]);

                                            document.Bookmarks["PM_telefono"].Select();
                                            application.Selection.TypeText(OficinaTelefono);


                                            document.Bookmarks["PM_correo"].Select();
                                            application.Selection.TypeText(OficinaCorreo);
                                            if (numtitularestotales > 1)
                                            {
                                                document.Bookmarks["PM_anexo"].Select();
                                                application.Selection.TypeText("X");
                                                //generar el anexo de interesados
                                                document.Bookmarks["x_anexo_15"].Select();
                                                application.Selection.TypeText("X");
                                                sBanderaanexointeresados = true;
                                            }

                                            //si es persona nacional colocamos en municipio
                                            document.Bookmarks["municipio_1"].Select();
                                            application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionPoblacion[contx]));

                                        } break;
                                    case "Física Nacional":
                                        {
                                            document.Bookmarks["PF_curp"].Select();
                                            application.Selection.TypeText(fGtpatentes.InteresadoCurp[contx]);

                                            document.Bookmarks["PF_Nombres"].Select();
                                            application.Selection.TypeText(fGtpatentes.InteresadoNombre[contx]);

                                            document.Bookmarks["PF_Primerapellido"].Select();
                                            application.Selection.TypeText(fGtpatentes.InteresadoApPaterno[contx]);

                                            document.Bookmarks["PF_segundoapellido"].Select();
                                            application.Selection.TypeText(fGtpatentes.InteresadoApMaterno[contx]);

                                            document.Bookmarks["PF_Nacionalidad"].Select();
                                            application.Selection.TypeText(fGtpatentes.nacionalidad[contx]);

                                            document.Bookmarks["PFTelefonoladanumext"].Select();
                                            application.Selection.TypeText(OficinaTelefono);

                                            document.Bookmarks["PF_correo"].Select();
                                            application.Selection.TypeText(OficinaCorreo);

                                            //PF_correo
                                            if (fGtpatentes.TipoRelacionId[contx] == "3")
                                            {
                                                document.Bookmarks["PF_solinventor"].Select();
                                                application.Selection.TypeText("X");
                                                //Nos dice que es un inventor y un titular, en los datos de los inventores va el segundo inventor en caso de que lo hubiera si no sólo van en estos campos
                                                //colocamos una bandera que nos ayude a manejar este punto
                                            }
                                            if (numtitularestotales > 1)
                                            {
                                                document.Bookmarks["PF_solinventor_anexo"].Select();
                                                application.Selection.TypeText("X");
                                                //generar el anexo de interesados
                                                document.Bookmarks["x_anexo_15"].Select();
                                                application.Selection.TypeText("X");

                                                sBanderaanexointeresados = true;
                                            }
                                            //si es persona nacional colocamos en municipio
                                            document.Bookmarks["municipio_1"].Select();
                                            application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionPoblacion[contx]));
                                        } break;
                                }

                                document.Bookmarks["cp_1"].Select();
                                application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionCP[contx]));

                                document.Bookmarks["calle_1"].Select();
                                application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionCalle[contx]));

                                document.Bookmarks["num_ext_1"].Select();
                                application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionNumExt[contx]));

                                document.Bookmarks["num_int_1"].Select();
                                application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionNumInt[contx]));

                                document.Bookmarks["col_1"].Select();
                                application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionColonia[contx]));

                                //si es extranjero
                                //document.Bookmarks["localidad_1"].Select();
                                //application.Selection.TypeText(fGtpatentes.sgDireccionEstado[contx]);

                                document.Bookmarks["entfed_1"].Select();
                                application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionEstado[contx]));

                                //document.Bookmarks["entrecalles_1"].Select();
                                //application.Selection.TypeText(fGtpatentes.InteresadoRFC[contx]);

                                document.Bookmarks["pais_1"].Select();
                                application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.sgNombrepais[contx]));

                                //document.Bookmarks["callepost_1"].Select();
                                //application.Selection.TypeText(fGtpatentes.InteresadoRFC[contx]);
                                //aqui agregamos la bandera  al titular a los datos del inventor cuando es tipo 3
                                if (fGtpatentes.TipoRelacionId[contx] == "3")
                                {
                                    document.Bookmarks["inventor_cp"].Select();
                                    application.Selection.TypeText(fGtpatentes.InteresadoCurp[contx]);

                                    document.Bookmarks["inventor_nombre"].Select();
                                    application.Selection.TypeText(fGtpatentes.InteresadoNombre[contx]);

                                    document.Bookmarks["inventor_paterno"].Select();
                                    application.Selection.TypeText(fGtpatentes.InteresadoApPaterno[contx]);

                                    document.Bookmarks["inventor_materno"].Select();
                                    application.Selection.TypeText(fGtpatentes.InteresadoApMaterno[contx]);


                                    document.Bookmarks["inventor_nacionalida"].Select();
                                    application.Selection.TypeText(fGtpatentes.nacionalidad[contx]);

                                    document.Bookmarks["inventor_telefono"].Select();
                                    application.Selection.TypeText(OficinaTelefono);

                                    document.Bookmarks["inventor_correo"].Select();
                                    application.Selection.TypeText(OficinaCorreo);


                                    document.Bookmarks["cp_2"].Select();
                                    application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionCP[contx]));

                                    document.Bookmarks["calle_2"].Select();
                                    application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionCalle[contx]));

                                    document.Bookmarks["num_ext_2"].Select();
                                    application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionNumExt[contx]));

                                    document.Bookmarks["num_int_2"].Select();
                                    application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionNumInt[contx]));

                                    document.Bookmarks["col_2"].Select();
                                    application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionColonia[contx]));

                                    document.Bookmarks["entfed_2"].Select();
                                    application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionEstado[contx]));

                                    document.Bookmarks["pais_2"].Select();
                                    application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.sgNombrepais[contx]));

                                    switch (fGtpatentes.lvinteresados.Items[contx].SubItems[7].Text)
                                    {
                                        case "Moral Extranjera":
                                            {
                                                document.Bookmarks["localidad_2"].Select();
                                                application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionPoblacion[contx]));} break;
                                        case "Física Extranjera": {
                                                document.Bookmarks["localidad_2"].Select();
                                                application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionPoblacion[contx]));} break;
                                        case "Moral Nacional": {
                                                document.Bookmarks["municipio_2"].Select();
                                                application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionPoblacion[contx]));} break;
                                        case "Física Nacional": {
                                                document.Bookmarks["municipio_2"].Select();
                                                application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionPoblacion[contx]));} break;
                                    }
                                    bBandera = false;
                                    sexistetitularinventor = 0;
                                    try {
                                        if (numtitularestotales > 0)
                                        {
                                            //generar el anexo de interesados
                                            document.Bookmarks["x_anexo_15"].Select();
                                            application.Selection.TypeText("X");
                                            sBanderaanexointeresados = true;
                                        }
                                    }
                                    catch (Exception E)
                                    {
                                    }
                                }
                                
                                break;
                            }
                        }
                        
                        anexo_titulares_interesados at_nexos_doc = new anexo_titulares_interesados();//neecsitamos crearlo
                        anexo_titulares_interesados at_nexos_inventarios_doc = new anexo_titulares_interesados();//neecsitamos crearlo
                        
                        for (int contx = 0; contx < fGtpatentes.TipoRelacionId.Length; contx++)
                        {
                            if (fGtpatentes.TipoRelacionId[contx] == "2")//si es 2 es que es inventor
                            {
                                switch (iContadorinventor)
                                {
                                    case 1:
                                        {
                                            if (bBandera)
                                            {
                                                document.Bookmarks["inventor_cp"].Select();
                                                application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoCurp[contx]));

                                                document.Bookmarks["inventor_nombre"].Select();
                                                application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoNombre[contx]));

                                                document.Bookmarks["inventor_paterno"].Select();
                                                application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoApPaterno[contx]));

                                                document.Bookmarks["inventor_materno"].Select();
                                                application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoApMaterno[contx]));

                                                document.Bookmarks["inventor_nacionalida"].Select();
                                                application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.nacionalidad[contx]));

                                                document.Bookmarks["inventor_telefono"].Select();
                                                application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(OficinaTelefono));

                                                document.Bookmarks["inventor_correo"].Select();
                                                application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(OficinaCorreo));

                                                document.Bookmarks["cp_2"].Select();
                                                application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionCP[contx]));

                                                document.Bookmarks["calle_2"].Select();
                                                application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionCalle[contx]));

                                                document.Bookmarks["num_ext_2"].Select();
                                                application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionNumExt[contx]));

                                                document.Bookmarks["col_2"].Select();
                                                application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionColonia[contx]));

                                                document.Bookmarks["num_int_2"].Select();
                                                application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionNumInt[contx]));

                                                document.Bookmarks["municipio_2"].Select();
                                                application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionPoblacion[contx]));

                                                document.Bookmarks["entfed_2"].Select();
                                                application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionEstado[contx]));

                                                document.Bookmarks["pais_2"].Select();
                                                application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.sgNombrepais[contx]));
                                            }
                                        } break;
                                    case 2:
                                        {
                                            try { 
                                                document.Bookmarks["inventor_datos_anexo"].Select();
                                                application.Selection.TypeText("X");

                                                document.Bookmarks["x_anexo_15"].Select();
                                                application.Selection.TypeText("X");
                                            }catch(Exception E){
                                            }

                                            sBanderaanexointeresados = true;
                                        } break;
                                    //case 3:
                                    //    {

                                    //    } break;

                                }
                                iContadorinventor++;
                            }
                            int residuo = sContanexos % 2;
                            
                            if (contx > sexistetitularinventor)
                            {
                                if (sbandera)
                                {
                                    try
                                    {
                                        document.Bookmarks["inventor_datos_anexo"].Select();
                                        application.Selection.TypeText("X");
                                    }
                                    catch (Exception E)
                                    {

                                    }
                                    sbandera = false;
                                }
                                 
                                if (residuo==0)
                                {
                                    at_nexos_doc.inicializamos(fGtpatentes.sCasoId, fGtpatentes.TipoRelacionId[contx], fGtpatentes.lCasoNumero_texbox.Text);
                                    parametrosdiftipo(contx, at_nexos_doc, 1, fGtpatentes.TipoRelacionId[contx]);
                                    if (fGtpatentes.TipoRelacionId[contx] == "3")
                                    {
                                        at_nexos_inventarios_doc.inicializamos(fGtpatentes.sCasoId, "2", fGtpatentes.lCasoNumero_texbox.Text);
                                        parametrosdiftipo(contx, at_nexos_inventarios_doc, 1, "2");
                                    }
                                }else{
                                    parametrosdiftipo(contx, at_nexos_doc, 2, fGtpatentes.TipoRelacionId[contx]);
                                    at_nexos_doc.terminardoc();
                                    if (fGtpatentes.TipoRelacionId[contx] == "3")
                                    {
                                        parametrosdiftipo(contx, at_nexos_inventarios_doc, 2, "2");
                                        at_nexos_inventarios_doc.terminardoc();
                                    }
                                }
                                sContanexos++;
                            }
                            
                        }
                        try {
                            at_nexos_doc.terminardoc();
                            at_nexos_inventarios_doc.terminardoc();
                        }catch(Exception E){
                            new filelog(loguin.sId, E.ToString());
                        }

                        document.Bookmarks["apoderado_nombre"].Select();
                        application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(ApoderadoNonbre));

                        document.Bookmarks["apoderado_paterno"].Select();
                        application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(ApoderadoApellidoPat));

                        document.Bookmarks["apoderado_materno"].Select();
                        application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(ApoderadoApellidoMat));

                        document.Bookmarks["apoderado_correo"].Select();
                        application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(OficinaCorreo));

                        document.Bookmarks["apoderado_telefono"].Select();
                        application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(OficinaTelefono));

                        //agregamos la información para escuchar notificaciones
                        document.Bookmarks["cp_3"].Select();
                        application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(OficinaCP));

                        document.Bookmarks["calle_3"].Select();
                        application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(OficinaCalle));

                        document.Bookmarks["num_ext_3"].Select();
                        application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(OficinaNumExt));

                        document.Bookmarks["num_int_3"].Select();
                        application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(OficinaNumInt));

                        document.Bookmarks["col_3"].Select();
                        application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(OficinaColonia));


                        document.Bookmarks["municipio_3"].Select();
                        application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(OficinaMunicipio));

                        document.Bookmarks["entfed_3"].Select();
                        application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(OficinaEstado));

                        document.Bookmarks["pais_3"].Select();
                        application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(OficinaPaisId));


                        //autorizado
                        document.Bookmarks["autorizado_nombre"].Select();
                        application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(AutorizadoNombre));

                        document.Bookmarks["autorizado_paterno"].Select();
                        application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(AutorizadApellidoPat));

                        document.Bookmarks["autorizado_materno"].Select();
                        application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(AutorizadoApellidoMat));

                        document.Bookmarks["autorizado_anexo"].Select();
                        application.Selection.TypeText("X");

                        if (fGtpatentes.rtTitulo.Text != "")
                        {
                            document.Bookmarks["solicitud_denominaci"].Select();
                            application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.rtTitulo.Text));
                        }
                        else
                        {
                            document.Bookmarks["solicitud_denominaci"].Select();
                            application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.rtTitulo.Text));
                        }

                        bool bPctuno = true;
                        bool bSegundaprioridadreclamada = true;
                        int contadorprio = 1;
                        List<anexo_prioridades> lPrioridades = new List<anexo_prioridades>();
                        bool bPitnoanexo = true;
                        anexo_prioridades anPrioridad;
                        for (int z = 0; z < fGtpatentes.lvPrioridades.Items.Count; z++)
                        {
                            
                            
                            switch (contadorprio)
                            {
                                case 1:
                                    {
                                        if (fGtpatentes.lvPrioridades.Items[z].SubItems[5].Text == "PCT" && bPctuno)//es la primera prioridad PCT
                                        {
                                            document.Bookmarks["num_pct"].Select();
                                            application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[1].Text));

                                            if (fGtpatentes.lvPrioridades.Items[z].SubItems[2].Text != "")
                                            {
                                                String[] fechaseparada = fGtpatentes.lvPrioridades.Items[z].SubItems[2].Text.Split('/');
                                                document.Bookmarks["dd_pct"].Select();
                                                application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fechaseparada[0]));

                                                document.Bookmarks["mm_pct"].Select();
                                                application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fechaseparada[1]));

                                                document.Bookmarks["yyyy_pct"].Select();
                                                application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fechaseparada[2]));

                                                //tbFechaDiculgacion.Text
                                            }
                                        }
                                        else
                                        {
                                            document.Bookmarks["paisprioridades"].Select();
                                            application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[4].Text));

                                            if (fGtpatentes.lvPrioridades.Items[z].SubItems[2].Text != "")
                                            {
                                                String[] fechaseparada = fGtpatentes.lvPrioridades.Items[z].SubItems[2].Text.Split('/');
                                                document.Bookmarks["dd_prioridades"].Select();
                                                application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fechaseparada[0]));

                                                document.Bookmarks["mm_prioridades"].Select();
                                                application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fechaseparada[1]));

                                                document.Bookmarks["yyyy_prioridades"].Select();
                                                application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fechaseparada[2]));
                                            }

                                            document.Bookmarks["num_prioridades"].Select();
                                            application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[1].Text));
                                            bSegundaprioridadreclamada = false;
                                        }

                                    } break;
                                case 2:
                                    {
                                        if (bSegundaprioridadreclamada)
                                        {
                                            document.Bookmarks["paisprioridades"].Select();
                                            application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[4].Text));
                                            if (fGtpatentes.lvPrioridades.Items[z].SubItems[2].Text != "")
                                            {
                                                String[] fechaseparada = fGtpatentes.lvPrioridades.Items[z].SubItems[2].Text.Split('/');
                                                document.Bookmarks["dd_prioridades"].Select();
                                                application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fechaseparada[0]));

                                                document.Bookmarks["mm_prioridades"].Select();
                                                application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fechaseparada[1]));

                                                document.Bookmarks["yyyy_prioridades"].Select();
                                                application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fechaseparada[2]));
                                            }
                                            document.Bookmarks["num_prioridades"].Select();
                                            application.Selection.TypeText(dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[1].Text));
                                        }
                                        else
                                        {
                                            anPrioridad = new anexo_prioridades(fGtpatentes.lCasoNumero.Text);
                                            anPrioridad.sPais = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[4].Text);
                                            anPrioridad.sFecha = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[2].Text);
                                            anPrioridad.sNumprioridad = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[1].Text);
                                            lPrioridades.Add(anPrioridad);

                                            //tachamos anexo de prioridades
                                            //x_anexo_18
                                            document.Bookmarks["x_anexo_18"].Select();
                                            application.Selection.TypeText("X");

                                            document.Bookmarks["prioridades_anexo"].Select();
                                            application.Selection.TypeText("X");
                                            bPitnoanexo = false;

                                        }
                                    } break;
                                case 3:
                                    {

                                        if (bPitnoanexo)
                                        {
                                            document.Bookmarks["prioridades_anexo"].Select();
                                            application.Selection.TypeText("X");
                                        }
                                        anPrioridad = new anexo_prioridades(fGtpatentes.lCasoNumero.Text);
                                        anPrioridad.sPais = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[4].Text);
                                        anPrioridad.sFecha = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[2].Text);
                                        anPrioridad.sNumprioridad = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[1].Text);
                                        lPrioridades.Add(anPrioridad);
                                    } break;
                                case 4:
                                    {
                                        anPrioridad = new anexo_prioridades(fGtpatentes.lCasoNumero.Text);
                                        anPrioridad.sPais = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[4].Text);
                                        anPrioridad.sFecha = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[2].Text);
                                        anPrioridad.sNumprioridad = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[1].Text);
                                        lPrioridades.Add(anPrioridad);
                                    } break;
                                case 5:
                                    {
                                        anPrioridad = new anexo_prioridades(fGtpatentes.lCasoNumero.Text);
                                        anPrioridad.sPais = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[4].Text);
                                        anPrioridad.sFecha = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[2].Text);
                                        anPrioridad.sNumprioridad = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[1].Text);
                                        lPrioridades.Add(anPrioridad);
                                    } break;
                                case 6:
                                    {
                                        anPrioridad = new anexo_prioridades(fGtpatentes.lCasoNumero.Text);
                                        anPrioridad.sPais = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[4].Text);
                                        anPrioridad.sFecha = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[2].Text);
                                        anPrioridad.sNumprioridad = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[1].Text);
                                        lPrioridades.Add(anPrioridad);
                                    } break;
                                case 7:
                                    {

                                        anPrioridad = new anexo_prioridades(fGtpatentes.lCasoNumero.Text);
                                        anPrioridad.sPais = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[4].Text);
                                        anPrioridad.sFecha = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[2].Text);
                                        anPrioridad.sNumprioridad = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[1].Text);
                                        lPrioridades.Add(anPrioridad);
                                    } break;
                                case 8:
                                    {
                                        anPrioridad = new anexo_prioridades(fGtpatentes.lCasoNumero.Text);
                                        anPrioridad.sPais = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[4].Text);
                                        anPrioridad.sFecha = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[2].Text);
                                        anPrioridad.sNumprioridad = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[1].Text);
                                        lPrioridades.Add(anPrioridad);
                                    } break;
                                case 9:
                                    {
                                        anPrioridad = new anexo_prioridades(fGtpatentes.lCasoNumero.Text);
                                        anPrioridad.sPais = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[4].Text);
                                        anPrioridad.sFecha = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[2].Text);
                                        anPrioridad.sNumprioridad = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[1].Text);
                                        lPrioridades.Add(anPrioridad);
                                    } break;
                                case 10:
                                    {
                                        anPrioridad = new anexo_prioridades(fGtpatentes.lCasoNumero.Text);
                                        anPrioridad.sPais = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[4].Text);
                                        anPrioridad.sFecha = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[2].Text);
                                        anPrioridad.sNumprioridad = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[1].Text);
                                        lPrioridades.Add(anPrioridad);
                                    } break;
                                case 11:
                                    {
                                        anPrioridad = new anexo_prioridades(fGtpatentes.lCasoNumero.Text);
                                        anPrioridad.sPais = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[4].Text);
                                        anPrioridad.sFecha = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[2].Text);
                                        anPrioridad.sNumprioridad = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[1].Text);
                                        lPrioridades.Add(anPrioridad);
                                    } break;
                                case 12:
                                    {
                                        anPrioridad = new anexo_prioridades(fGtpatentes.lCasoNumero.Text);
                                        anPrioridad.sPais = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[4].Text);
                                        anPrioridad.sFecha = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[2].Text);
                                        anPrioridad.sNumprioridad = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[1].Text);
                                        lPrioridades.Add(anPrioridad);
                                    } break;
                                case 13:
                                    {
                                        anPrioridad = new anexo_prioridades(fGtpatentes.lCasoNumero.Text);
                                        anPrioridad.sPais = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[4].Text);
                                        anPrioridad.sFecha = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[2].Text);
                                        anPrioridad.sNumprioridad = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[1].Text);
                                        lPrioridades.Add(anPrioridad);
                                    } break;
                                case 14:
                                    {
                                        anPrioridad = new anexo_prioridades(fGtpatentes.lCasoNumero.Text);
                                        anPrioridad.sPais = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[4].Text);
                                        anPrioridad.sFecha = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[2].Text);
                                        anPrioridad.sNumprioridad = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[1].Text);
                                        lPrioridades.Add(anPrioridad);
                                    } break;
                                case 15:
                                    {
                                        anPrioridad = new anexo_prioridades(fGtpatentes.lCasoNumero.Text);
                                        anPrioridad.sPais = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[4].Text);
                                        anPrioridad.sFecha = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[2].Text);
                                        anPrioridad.sNumprioridad = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[1].Text);
                                        lPrioridades.Add(anPrioridad);
                                    } break;
                                case 16:
                                    {
                                        anPrioridad = new anexo_prioridades(fGtpatentes.lCasoNumero.Text);
                                        anPrioridad.sPais = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[4].Text);
                                        anPrioridad.sFecha = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[2].Text);
                                        anPrioridad.sNumprioridad = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[1].Text);
                                        lPrioridades.Add(anPrioridad);
                                    } break;
                                case 17:
                                    {
                                        anPrioridad = new anexo_prioridades(fGtpatentes.lCasoNumero.Text);
                                        anPrioridad.sPais = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[4].Text);
                                        anPrioridad.sFecha = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[2].Text);
                                        anPrioridad.sNumprioridad = dicssdfunctions.RemoveLineEndings(fGtpatentes.lvPrioridades.Items[z].SubItems[1].Text);
                                        lPrioridades.Add(anPrioridad);
                                    } break;
                            }
                            contadorprio++;
                        }

                        if (lPrioridades.Count > 0){//Con ésta validación verificamos que se creé el anexo prioridades
                            String sPrioridades =""; ;
                            foreach (anexo_prioridades anexo_prioridades in lPrioridades )
                            {
                                sPrioridades += anexo_prioridades.sPais + "  " + anexo_prioridades.sFecha + "  " + anexo_prioridades.sNumprioridad + '\n';
                            }
                            anexo_prioridades_doc objetoaneco_prioridades = new anexo_prioridades_doc(fGtpatentes.sCasoId, fGtpatentes.lCasoNumero_texbox.Text);
                            objetoaneco_prioridades.addcampo("Prioridades", sPrioridades);
                            objetoaneco_prioridades.terminardoc();
                        }
                        document.Bookmarks["Nomyfirmasolicitante"].Select();
                        application.Selection.TypeText(ApoderadoNonbre + " " + ApoderadoApellidoPat + " " + ApoderadoApellidoMat);
                        //llenamos los anexos configurables
                        if (CheckB1anexosolicitud.Checked)
                        {
                            document.Bookmarks["x_anexo_1"].Select();
                            application.Selection.TypeText("X");
                            document.Bookmarks["x_anexo_text_1"].Select();
                            application.Selection.TypeText(TB_anexo1.Text);
                        }
                        if (CheckB2anexosolicitud.Checked)
                        {
                            document.Bookmarks["x_anexo_2"].Select();
                            application.Selection.TypeText("X");
                            document.Bookmarks["x_anexo_text_2"].Select();
                            application.Selection.TypeText(TB_anexo2.Text);
                        }
                        if (CheckB3anexosolicitud.Checked)
                        {
                            document.Bookmarks["x_anexo_3"].Select();
                            application.Selection.TypeText("X");
                            document.Bookmarks["x_anexo_text_3"].Select();
                            application.Selection.TypeText(TB_anexo3.Text);
                        }
                        if (CheckB4anexosolicitud.Checked)
                        {
                            document.Bookmarks["x_anexo_4"].Select();
                            application.Selection.TypeText("X");
                            document.Bookmarks["x_anexo_text_4"].Select();
                            application.Selection.TypeText(TB_anexo4.Text);
                        }
                        if (CheckB5anexosolicitud.Checked)
                        {
                            document.Bookmarks["x_anexo_5"].Select();
                            application.Selection.TypeText("X");
                            document.Bookmarks["x_anexo_text_5"].Select();
                            application.Selection.TypeText(TB_anexo5.Text);
                        }
                        if (CheckB6anexosolicitud.Checked)
                        {
                            document.Bookmarks["x_anexo_6"].Select();
                            application.Selection.TypeText("X");
                            document.Bookmarks["x_anexo_text_6"].Select();
                            application.Selection.TypeText(TB_anexo6.Text);
                        }
                        if (CheckB7anexosolicitud.Checked)
                        {
                            document.Bookmarks["x_anexo_7"].Select();
                            application.Selection.TypeText("X");
                            document.Bookmarks["x_anexo_text_7"].Select();
                            application.Selection.TypeText(TB_anexo7.Text);
                        }
                        if (CheckB8anexosolicitud.Checked)
                        {
                            document.Bookmarks["x_anexo_8"].Select();
                            application.Selection.TypeText("X");
                            document.Bookmarks["x_anexo_text_8"].Select();
                            application.Selection.TypeText(TB_anexo8.Text);
                        }
                        if (CheckB9anexosolicitud.Checked)
                        {
                            document.Bookmarks["x_anexo_9"].Select();
                            application.Selection.TypeText("X");
                            document.Bookmarks["x_anexo_text_9"].Select();
                            application.Selection.TypeText(TB_anexo9.Text);
                        }
                        if (CheckB10anexosolicitud.Checked)
                        {
                            document.Bookmarks["x_anexo_10"].Select();
                            application.Selection.TypeText("X");
                            document.Bookmarks["x_anexo_text_10"].Select();
                            application.Selection.TypeText(TB_anexo10.Text);
                        }
                        if (CheckB11anexosolicitud.Checked)
                        {
                            document.Bookmarks["x_anexo_11"].Select();
                            application.Selection.TypeText("X");
                            document.Bookmarks["x_anexo_text_11"].Select();
                            application.Selection.TypeText(TB_anexo11.Text);
                        }
                        if (CheckB12anexosolicitud.Checked)
                        {
                            document.Bookmarks["x_anexo_12"].Select();
                            application.Selection.TypeText("X");
                            document.Bookmarks["x_anexo_text_12"].Select();
                            application.Selection.TypeText(TB_anexo12.Text);
                        }
                        if (CheckB13anexosolicitud.Checked)
                        {
                            document.Bookmarks["x_anexo_13"].Select();
                            application.Selection.TypeText("X");
                            document.Bookmarks["x_anexo_text_13"].Select();
                            application.Selection.TypeText(TB_anexo13.Text);
                        }
                        if (CheckB14anexosolicitud.Checked)
                        {
                            document.Bookmarks["x_anexo_14"].Select();
                            application.Selection.TypeText("X");
                            document.Bookmarks["x_anexo_text_14"].Select();
                            application.Selection.TypeText(TB_anexo14.Text);
                        }

                            document.Bookmarks["x_anexo_16"].Select();
                            application.Selection.TypeText("X");
                        if (CheckB17anexosolicitud.Checked)
                        {
                            document.Bookmarks["x_anexo_17"].Select();
                            application.Selection.TypeText("X");
                            document.Bookmarks["x_anexo_text_17"].Select();
                            application.Selection.TypeText(TB_anexo17.Text);
                        }

                        //if(sBanderaanexointeresados){
                        //    generaanexosinteresados(arrayconvalores);
                        //}
                        application.Visible = true;
                        document.Save();
                        //this.Close();
                        //application.Quit();
                        //((Word._Document)application.ActiveDocument).Close();
                    }
                    catch (Exception E)
                    {
                        Console.Write("Error: " + E + "\n");
                        new filelog(loguin.sId, E.ToString());
                    }
                    MessageBox.Show("Documento generado Correctamente.");
                    this.Close();
                    

                }
                else {
                    MessageBox.Show("Debe seleccionar si se agrega la fecha de divulgación");
                }
                
            }
            catch (Exception E)
            {
                new filelog(loguin.sId, E.ToString());
            }
        }

        public void parametrosdiftipo(int iPosicicon, anexo_titulares_interesados obj, int nTitular, String sTiposolitiud)
        {
            try {
                conect con_3 = new conect();
                String squerydatoficinas = "select DameNombrePais(oficina.OficinaPaisId) as OficinaPaisId, oficina.*,  apoderado.* from oficina, apoderado limit 1;";
                MySqlDataReader resp_datofi = con_3.getdatareader(squerydatoficinas);
                String OficinaCP = "";
                String OficinaCalle = "";
                String OficinaNumExt = "";
                String OficinaNumInt = "";
                String OficinaColonia = "";
                String OficinaMunicipio = "";
                String OficinaEstado = "";
                String OficinaPaisId = "";
                String OficinaTelefono = "";
                String OficinaCorreo = "";
                String ApoderadoNonbre = "";
                String ApoderadoApellidoPat = "";
                String ApoderadoApellidoMat = "";
                String AutorizadoNombre = "";
                String AutorizadApellidoPat = "";
                String AutorizadoApellidoMat = "";
                while (resp_datofi.Read())
                {
                    OficinaCP = dicssdfunctions.validareader("OficinaCP", "id_oficina", resp_datofi).Text;
                    OficinaCalle = dicssdfunctions.validareader("OficinaCalle", "id_oficina", resp_datofi).Text;
                    OficinaNumExt = dicssdfunctions.validareader("OficinaNumExt", "id_oficina", resp_datofi).Text;
                    OficinaNumInt = dicssdfunctions.validareader("OficinaNumInt", "id_oficina", resp_datofi).Text;
                    OficinaColonia = dicssdfunctions.validareader("OficinaColonia", "id_oficina", resp_datofi).Text;
                    OficinaMunicipio = dicssdfunctions.validareader("OficinaMunicipio", "id_oficina", resp_datofi).Text;
                    OficinaEstado = dicssdfunctions.validareader("OficinaEstado", "id_oficina", resp_datofi).Text;
                    OficinaPaisId = dicssdfunctions.validareader("OficinaPaisId", "id_oficina", resp_datofi).Text;
                    OficinaTelefono = dicssdfunctions.validareader("OficinaTelefono", "id_oficina", resp_datofi).Text;
                    OficinaCorreo = dicssdfunctions.validareader("OficinaCorreo", "id_oficina", resp_datofi).Text;
                    ApoderadoNonbre = dicssdfunctions.validareader("ApoderadoNonbre", "id_oficina", resp_datofi).Text;
                    ApoderadoApellidoPat = dicssdfunctions.validareader("ApoderadoApellidoPat", "id_oficina", resp_datofi).Text;
                    ApoderadoApellidoMat = dicssdfunctions.validareader("ApoderadoApellidoMat", "id_oficina", resp_datofi).Text;
                    AutorizadoNombre = dicssdfunctions.validareader("AutorizadoNombre", "id_oficina", resp_datofi).Text;
                    AutorizadApellidoPat = dicssdfunctions.validareader("AutorizadApellidoPat", "id_oficina", resp_datofi).Text;
                    AutorizadoApellidoMat = dicssdfunctions.validareader("AutorizadoApellidoMat", "id_oficina", resp_datofi).Text;
                }
                resp_datofi.Close();
                con_3.Cerrarconexion();
                switch (nTitular)
                {
                    case 1:
                        {
                            if (sTiposolitiud == "3" || sTiposolitiud == "1")
                            {
                                obj.addcampo("Solicitante", "X");
                            }
                            else
                            {
                                obj.addcampo("Inventor", "X");
                            }
                            switch (fGtpatentes.lvinteresados.Items[iPosicicon].SubItems[7].Text)
                            {
                                case "Moral Extranjera":
                                    {
                                        obj.addcampo("rfc_pm_1", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoRFC[iPosicicon]));
                                        obj.addcampo("razonsoc_1_pm", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoNombre[iPosicicon]));
                                        obj.addcampo("Nacionalidad_pm_1", dicssdfunctions.RemoveLineEndings(fGtpatentes.nacionalidad[iPosicicon]));
                                        obj.addcampo("telefono_pm_1", dicssdfunctions.RemoveLineEndings(OficinaTelefono));
                                        obj.addcampo("Localidad_1", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionPoblacion[iPosicicon]));
                                    }break;
                                case "Física Extranjera":
                                    {
                                        obj.addcampo("curp_1_pf", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoCurp[iPosicicon]));
                                        obj.addcampo("nombre_pf_1", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoNombre[iPosicicon]));
                                        obj.addcampo("paterno_pf_1", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoApPaterno[iPosicicon]));
                                        obj.addcampo("materno_pf", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoApMaterno[iPosicicon]));
                                        obj.addcampo("Nacionalidad_pf", dicssdfunctions.RemoveLineEndings(fGtpatentes.nacionalidad[iPosicicon]));
                                        obj.addcampo("telefono_pf_1", dicssdfunctions.RemoveLineEndings(OficinaTelefono));
                                        obj.addcampo("Localidad_1", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionPoblacion[iPosicicon]));
                                    }break;
                                case "Moral Nacional":
                                    {
                                        obj.addcampo("rfc_pm_1", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoRFC[iPosicicon]));
                                        obj.addcampo("razonsoc_1_pm", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoNombre[iPosicicon]));
                                        obj.addcampo("Nacionalidad_pm_1", dicssdfunctions.RemoveLineEndings(fGtpatentes.nacionalidad[iPosicicon]));
                                        obj.addcampo("telefono_pm_1", dicssdfunctions.RemoveLineEndings(OficinaTelefono));
                                        obj.addcampo("Muni_1", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionPoblacion[iPosicicon]));
                                    }break;
                                case "Física Nacional":
                                    {
                                        obj.addcampo("curp_1_pf", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoCurp[iPosicicon]));
                                        obj.addcampo("nombre_pf_1", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoNombre[iPosicicon]));
                                        obj.addcampo("paterno_pf_1", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoApPaterno[iPosicicon]));
                                        obj.addcampo("materno_pf", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoApMaterno[iPosicicon]));
                                        obj.addcampo("Nacionalidad_pf", dicssdfunctions.RemoveLineEndings(fGtpatentes.nacionalidad[iPosicicon]));
                                        obj.addcampo("telefono_pf_1", dicssdfunctions.RemoveLineEndings(OficinaTelefono));
                                        obj.addcampo("Muni_1", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionPoblacion[iPosicicon]));
                                    }break;
                            }

                            obj.addcampo("cp_1", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionCP[iPosicicon]));
                            obj.addcampo("calle_1", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionCalle[iPosicicon]));
                            obj.addcampo("numext_1", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionNumExt[iPosicicon]));
                            obj.addcampo("num_int_1", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionNumInt[iPosicicon]));
                            obj.addcampo("col_1", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionColonia[iPosicicon]));
                            obj.addcampo("entfed_1", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionEstado[iPosicicon]));
                            obj.addcampo("pais_1", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgNombrepais[iPosicicon]));
                        } break;
                    case 2:
                        {
                            if (sTiposolitiud == "3" || sTiposolitiud == "1")
                            {
                                obj.addcampo("Solicitante_2", "X");
                            }
                            else
                            {
                                obj.addcampo("inventor_2", "X");
                            }

                            switch (fGtpatentes.lvinteresados.Items[iPosicicon].SubItems[7].Text)
                            {
                                case "Moral Extranjera":
                                    {
                                        obj.addcampo("rfc_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoRFC[iPosicicon]));
                                        obj.addcampo("denominacion_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoNombre[iPosicicon]));
                                        obj.addcampo("pm_2_nacionalidad", dicssdfunctions.RemoveLineEndings(fGtpatentes.nacionalidad[iPosicicon]));
                                        obj.addcampo("telefono_pm_2", dicssdfunctions.RemoveLineEndings(OficinaTelefono));
                                        obj.addcampo("localidad_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionPoblacion[iPosicicon]));
                                        //

                                    } break;
                                case "Física Extranjera":
                                    {
                                        obj.addcampo("curp_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoCurp[iPosicicon]));
                                        obj.addcampo("Nombre_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoNombre[iPosicicon]));
                                        obj.addcampo("paterno_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoApPaterno[iPosicicon]));
                                        obj.addcampo("Materno_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoApMaterno[iPosicicon]));
                                        obj.addcampo("pf_2_nacionalidad", dicssdfunctions.RemoveLineEndings(fGtpatentes.nacionalidad[iPosicicon]));
                                        obj.addcampo("pf_telefono_2", dicssdfunctions.RemoveLineEndings(OficinaTelefono));
                                        obj.addcampo("localidad_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionPoblacion[iPosicicon]));

                                    } break;
                                case "Moral Nacional":
                                    {
                                        obj.addcampo("rfc_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoRFC[iPosicicon]));
                                        obj.addcampo("denominacion_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoNombre[iPosicicon]));
                                        obj.addcampo("pm_2_nacionalidad", dicssdfunctions.RemoveLineEndings(fGtpatentes.nacionalidad[iPosicicon]));
                                        obj.addcampo("telefono_pm_2", dicssdfunctions.RemoveLineEndings(OficinaTelefono));
                                        obj.addcampo("municipio_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionPoblacion[iPosicicon]));

                                    } break;
                                case "Física Nacional":
                                    {
                                        obj.addcampo("curp_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoCurp[iPosicicon]));
                                        obj.addcampo("Nombre_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoNombre[iPosicicon]));
                                        obj.addcampo("paterno_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoApPaterno[iPosicicon]));
                                        obj.addcampo("Materno_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoApMaterno[iPosicicon]));
                                        obj.addcampo("pf_2_nacionalidad", dicssdfunctions.RemoveLineEndings(fGtpatentes.nacionalidad[iPosicicon]));
                                        obj.addcampo("pf_telefono_2", dicssdfunctions.RemoveLineEndings(OficinaTelefono));
                                        obj.addcampo("municipio_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionPoblacion[iPosicicon]));
                                    } break;
                            }
                            obj.addcampo("cp_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionCP[iPosicicon]));
                            obj.addcampo("calle_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionCalle[iPosicicon]));
                            obj.addcampo("num_ext_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionNumExt[iPosicicon]));
                            obj.addcampo("num_int_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionNumInt[iPosicicon]));
                            obj.addcampo("colonia_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionColonia[iPosicicon]));
                            obj.addcampo("entidadfed_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionEstado[iPosicicon]));
                            obj.addcampo("pais_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgNombrepais[iPosicicon]));
                        } break;
                }
            }catch(Exception E){
                new filelog(loguin.sId, E.ToString());
            }
            
        }
        
        public void generaanexo(int iPosicicon) {
            anexo_interesado obj = new anexo_interesado(fGtpatentes.sCasoId);
            //consultamos la cdireccion
            conect con_3 = new conect();
            String squerydatoficinas = "select DameNombrePais(oficina.OficinaPaisId) as OficinaPaisId, oficina.*,  apoderado.* from oficina, apoderado limit 1;";
            MySqlDataReader resp_datofi = con_3.getdatareader(squerydatoficinas);
            String OficinaCP = "";
            String OficinaCalle = "";
            String OficinaNumExt = "";
            String OficinaNumInt = "";
            String OficinaColonia = "";
            String OficinaMunicipio = "";
            String OficinaEstado = "";
            String OficinaPaisId = "";
            String OficinaTelefono = "";
            String OficinaCorreo = "";
            String ApoderadoNonbre = "";
            String ApoderadoApellidoPat = "";
            String ApoderadoApellidoMat = "";
            String AutorizadoNombre = "";
            String AutorizadApellidoPat = "";
            String AutorizadoApellidoMat = "";
            while (resp_datofi.Read())
            {
                OficinaCP = dicssdfunctions.validareader("OficinaCP", "id_oficina", resp_datofi).Text;
                OficinaCalle = dicssdfunctions.validareader("OficinaCalle", "id_oficina", resp_datofi).Text;
                OficinaNumExt = dicssdfunctions.validareader("OficinaNumExt", "id_oficina", resp_datofi).Text;
                OficinaNumInt = dicssdfunctions.validareader("OficinaNumInt", "id_oficina", resp_datofi).Text;
                OficinaColonia = dicssdfunctions.validareader("OficinaColonia", "id_oficina", resp_datofi).Text;
                OficinaMunicipio = dicssdfunctions.validareader("OficinaMunicipio", "id_oficina", resp_datofi).Text;
                OficinaEstado = dicssdfunctions.validareader("OficinaEstado", "id_oficina", resp_datofi).Text;
                OficinaPaisId = dicssdfunctions.validareader("OficinaPaisId", "id_oficina", resp_datofi).Text;
                OficinaTelefono = dicssdfunctions.validareader("OficinaTelefono", "id_oficina", resp_datofi).Text;
                OficinaCorreo = dicssdfunctions.validareader("OficinaCorreo", "id_oficina", resp_datofi).Text;
                ApoderadoNonbre = dicssdfunctions.validareader("ApoderadoNonbre", "id_oficina", resp_datofi).Text;
                ApoderadoApellidoPat = dicssdfunctions.validareader("ApoderadoApellidoPat", "id_oficina", resp_datofi).Text;
                ApoderadoApellidoMat = dicssdfunctions.validareader("ApoderadoApellidoMat", "id_oficina", resp_datofi).Text;
                AutorizadoNombre = dicssdfunctions.validareader("AutorizadoNombre", "id_oficina", resp_datofi).Text;
                AutorizadApellidoPat = dicssdfunctions.validareader("AutorizadApellidoPat", "id_oficina", resp_datofi).Text;
                AutorizadoApellidoMat = dicssdfunctions.validareader("AutorizadoApellidoMat", "id_oficina", resp_datofi).Text;
            }
            resp_datofi.Close();
            con_3.Cerrarconexion();

            if (fGtpatentes.TipoRelacionId[iPosicicon] == "3" || fGtpatentes.TipoRelacionId[iPosicicon] == "1")
            {
                obj.addcampo("Solicitante", "X");
            }
            else {
                obj.addcampo("Inventor", "X");
            }
          
             switch (fGtpatentes.lvinteresados.Items[iPosicicon].SubItems[7].Text)
                                {
                                    case "Moral Extranjera":
                                        {
                                            obj.addcampo("rfc_pm_1", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoRFC[iPosicicon]));
                                            obj.addcampo("razonsoc_1_pm", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoNombre[iPosicicon]));
                                            obj.addcampo("Nacionalidad_pm_1", dicssdfunctions.RemoveLineEndings(fGtpatentes.nacionalidad[iPosicicon]));
                                            obj.addcampo("telefono_pm_1", dicssdfunctions.RemoveLineEndings(OficinaTelefono));
                                            obj.addcampo("Localidad_1", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionPoblacion[iPosicicon]));
                                            //

                                        } break;
                                    case "Física Extranjera":
                                        {
                                            obj.addcampo("curp_1_pf", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoCurp[iPosicicon]));
                                            obj.addcampo("nombre_pf_1", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoNombre[iPosicicon]));
                                            obj.addcampo("paterno_pf_1", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoApPaterno[iPosicicon]));
                                            obj.addcampo("materno_pf", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoApMaterno[iPosicicon]));
                                            obj.addcampo("Nacionalidad_pf", dicssdfunctions.RemoveLineEndings(fGtpatentes.nacionalidad[iPosicicon]));
                                            obj.addcampo("telefono_pf_1", dicssdfunctions.RemoveLineEndings(OficinaTelefono));
                                            obj.addcampo("Localidad_1", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionPoblacion[iPosicicon]));
                                            
                                        } break;
                                    case "Moral Nacional":
                                        {
                                            obj.addcampo("rfc_pm_1", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoRFC[iPosicicon]));
                                            obj.addcampo("razonsoc_1_pm", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoNombre[iPosicicon]));
                                            obj.addcampo("Nacionalidad_pm_1", dicssdfunctions.RemoveLineEndings(fGtpatentes.nacionalidad[iPosicicon]));
                                            obj.addcampo("telefono_pm_1", dicssdfunctions.RemoveLineEndings(OficinaTelefono));
                                            obj.addcampo("Muni_1", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionPoblacion[iPosicicon]));

                                        } break;
                                    case "Física Nacional":
                                        {
                                            obj.addcampo("curp_1_pf", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoCurp[iPosicicon]));
                                            obj.addcampo("nombre_pf_1", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoNombre[iPosicicon]));
                                            obj.addcampo("paterno_pf_1", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoApPaterno[iPosicicon]));
                                            obj.addcampo("materno_pf", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoApMaterno[iPosicicon]));
                                            obj.addcampo("Nacionalidad_pf", dicssdfunctions.RemoveLineEndings(fGtpatentes.nacionalidad[iPosicicon]));
                                            obj.addcampo("telefono_pf_1", dicssdfunctions.RemoveLineEndings(OficinaTelefono));
                                            obj.addcampo("Muni_1", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionPoblacion[iPosicicon]));
                                        } break;
                                }

            obj.addcampo("cp_1", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionCP[iPosicicon]));
            obj.addcampo("calle_1", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionCalle[iPosicicon]));
            obj.addcampo("numext_1", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionNumExt[iPosicicon]));
            obj.addcampo("num_int_1", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionNumInt[iPosicicon]));
            obj.addcampo("col_1", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionColonia[iPosicicon]));
            obj.addcampo("entfed_1", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionEstado[iPosicicon]));
            obj.addcampo("pais_1", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgNombrepais[iPosicicon]));


            try { 
                if (fGtpatentes.TipoRelacionId.Length > iPosicicon + 1)//ya no hay más
                {
                    if (fGtpatentes.TipoRelacionId[iPosicicon + 1] == "3" || fGtpatentes.TipoRelacionId[iPosicicon + 1] == "1")
                    {
                        obj.addcampo("Solicitante_2", "X");
                    }
                    else
                    {
                        obj.addcampo("inventor_2", "X");
                    }

                    switch (fGtpatentes.lvinteresados.Items[iPosicicon + 1].SubItems[7].Text)
                    {
                        case "Moral Extranjera":
                            {
                                obj.addcampo("rfc_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoRFC[iPosicicon + 1]));
                                obj.addcampo("denominacion_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoNombre[iPosicicon + 1]));
                                obj.addcampo("pm_2_nacionalidad", dicssdfunctions.RemoveLineEndings(fGtpatentes.nacionalidad[iPosicicon + 1]));
                                obj.addcampo("telefono_pm_2", dicssdfunctions.RemoveLineEndings(OficinaTelefono));
                                obj.addcampo("localidad_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionPoblacion[iPosicicon + 1]));
                                //

                            } break;
                        case "Física Extranjera":
                            {
                                obj.addcampo("curp_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoCurp[iPosicicon + 1]));
                                obj.addcampo("Nombre_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoNombre[iPosicicon + 1]));
                                obj.addcampo("paterno_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoApPaterno[iPosicicon + 1]));
                                obj.addcampo("Materno_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoApMaterno[iPosicicon + 1]));
                                obj.addcampo("pf_2_nacionalidad", dicssdfunctions.RemoveLineEndings(fGtpatentes.nacionalidad[iPosicicon + 1]));
                                obj.addcampo("pf_telefono_2", dicssdfunctions.RemoveLineEndings(OficinaTelefono));
                                obj.addcampo("localidad_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionPoblacion[iPosicicon + 1]));

                            } break;
                        case "Moral Nacional":
                            {
                                obj.addcampo("rfc_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoRFC[iPosicicon + 1]));
                                obj.addcampo("denominacion_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoNombre[iPosicicon + 1]));
                                obj.addcampo("pm_2_nacionalidad", dicssdfunctions.RemoveLineEndings(fGtpatentes.nacionalidad[iPosicicon + 1]));
                                obj.addcampo("telefono_pm_2", dicssdfunctions.RemoveLineEndings(OficinaTelefono));
                                obj.addcampo("municipio_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionPoblacion[iPosicicon + 1]));

                            } break;
                        case "Física Nacional":
                            {
                                obj.addcampo("curp_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoCurp[iPosicicon + 1]));
                                obj.addcampo("Nombre_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoNombre[iPosicicon + 1]));
                                obj.addcampo("paterno_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoApPaterno[iPosicicon + 1]));
                                obj.addcampo("Materno_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.InteresadoApMaterno[iPosicicon + 1]));
                                obj.addcampo("pf_2_nacionalidad", dicssdfunctions.RemoveLineEndings(fGtpatentes.nacionalidad[iPosicicon + 1]));
                                obj.addcampo("pf_telefono_2", dicssdfunctions.RemoveLineEndings(OficinaTelefono));
                                obj.addcampo("municipio_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionPoblacion[iPosicicon + 1]));
                            } break;
                    }
                    obj.addcampo("cp_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionCP[iPosicicon + 1]));
                    obj.addcampo("calle_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionCalle[iPosicicon + 1]));
                    obj.addcampo("num_ext_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionNumExt[iPosicicon + 1]));
                    obj.addcampo("num_int_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionNumInt[iPosicicon + 1]));
                    obj.addcampo("colonia_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionColonia[iPosicicon + 1]));
                    obj.addcampo("entidadfed_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgDireccionEstado[iPosicicon + 1]));
                    obj.addcampo("pais_2", dicssdfunctions.RemoveLineEndings(fGtpatentes.sgNombrepais[iPosicicon + 1]));
                }
                else { //No hay más valores
                    
                }
            }catch(Exception E){
                MessageBox.Show("Error" + E);
            }
            obj.terminardoc();
        }

        private void tbFechaDiculgacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }


            if (tbFechaDiculgacion.Text.Length == 2)
            {
                tbFechaDiculgacion.Text = tbFechaDiculgacion.Text + "-";
                tbFechaDiculgacion.SelectionStart = tbFechaDiculgacion.Text.Length;

            }
            if (tbFechaDiculgacion.Text.Length == 5)
            {
                tbFechaDiculgacion.Text = tbFechaDiculgacion.Text + "-";
                tbFechaDiculgacion.SelectionStart = tbFechaDiculgacion.Text.Length;
            }
        }

        private void rbAgregar_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAgregar.Checked)
            {
                tbFechaDiculgacion.Enabled = true;
            }
            else {
                tbFechaDiculgacion.Text = "";
                tbFechaDiculgacion.Enabled = false;
            }
        }




    }
}
