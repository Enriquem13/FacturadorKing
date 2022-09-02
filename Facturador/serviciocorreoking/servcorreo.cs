using MailBee.SmtpMail;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Facturador.serviciocorreoking
{
    public partial class servcorreo : Form
    {
        int imarcatotal = 0;
        int ipattotal = 0;
        int imarcatotal_accion = 0;
        int ipattotal_accion = 0;
        bool bBanderadia = true;
        public bool bBanderahabilitalog { get; set; }
        public servcorreo()
        {
            InitializeComponent();
        }

        private void servcorreo_Load(object sender, EventArgs e)
        {

        }

        public void enviaremail()
        {
            try
            {
                String sEncabezado = " <p>Estimado Equipo de Vadillo & King, " +
                                " <br> " +
                                " Es indispensable que se atiendan estos casos hoy mismo.</p> <br><br> ";
                //Acción Oficial
                int iCountmailsend = 0;
                if (cb_Aoficial_marcas.Checked)
                {
                    try
                    {
                        String sMarcaspendientesAccion = generabodymailmarcasAccion();//agregar validacion de si es diferente de vacios
                        iCountmailsend += SendEmail("eduarximo@gmail.com", "Urgente atender estos plazos de Marcas (Acción Oficial) ", sEncabezado + "<label><strong>Marcas (" + (imarcatotal_accion - 1) + ")</strong></label>" + sMarcaspendientesAccion, "eduardor@dicss.com.mx");
                    }
                    catch (Exception Ex)
                    {
                        new filelog("Acción oficial- Marcas:", Ex.Message);
                    }
                }


                if (cb_Aoficial_patentes.Checked)
                {
                    try
                    {
                        String sPatentespendientesAccion = generabodymailpatentesAccion();//validar que no sea vacio
                        iCountmailsend += SendEmail("eduarximo@gmail.com", "Urgente atender estos plazos de Patentes (Acción Oficial) ", sEncabezado + "<label><strong>Patentes (" + (ipattotal_accion - 1) + ")</strong></label>" + sPatentespendientesAccion, "eduardor@dicss.com.mx");
                    }
                    catch (Exception Ex)
                    {
                        new filelog("Acción oficial- Patentes:", Ex.Message);
                    }

                }


                if (cb_sol_marcas.Checked)
                {
                    try
                    {
                        String sMarcaspendientes = generabodymailmarcas();
                        iCountmailsend += SendEmail("eduarximo@gmail.com", "Urgente presentar estas solicitudes de Marca", sEncabezado + "<label><strong>Marcas (" + (imarcatotal - 1) + ")</strong></label>" + sMarcaspendientes, "eduardor@dicss.com.mx");
                    }
                    catch (Exception Ex)
                    {
                        new filelog("Solicitudes- Marca:", Ex.Message);
                    }
                }


                if (cb_sol_patentes.Checked)
                {
                    try
                    {
                        String sPatentespendientes = generabodymailpatentes();
                        iCountmailsend += SendEmail("eduarximo@gmail.com", "Urgente presentar estas solicitudes de Patente", sEncabezado + "<label><strong>Patentes (" + (ipattotal - 1) + ")</strong></label>" + sPatentespendientes, "eduardor@dicss.com.mx");
                    }
                    catch (Exception Ex)
                    {
                        new filelog("Solicitudes- Patentes:", Ex.Message);
                    }
                }
                new filelog("Sendmail", "Correos enviados: " + iCountmailsend);
                MessageBox.Show("Correos enviados: " + iCountmailsend   );
                //Accionoficial oficial
            }
            catch (Exception Ex)
            {
                new filelog("linea: 151", "Error: " + Ex.Message);
            }

        }
        public String generabodymailmarcas()
        {
            try
            {
                String sCasospendientes = "";
                String encabezado = " <table border='1'> " +
                                    "     <thead> " +
                                    "         <tr> " +
                                    "             <th>Número</th> " +
                                    "             <th>Tipo de Solicitud</th> " +
                                    "             <th>Tipo de Plazo</th> " +
                                    "             <th>Marca</th> " +
                                    "             <th>Clase</th> " +
                                    "             <th>Caso Número</th> " +
                                    "             <th>CasoId</th> " +
                                    "             <th>Plazo Fecha</th> " +
                                    "             <th>Mes</th> " +
                                    "             <th>Titular</th> " +
                                    "             <th>Cliente</th> " +
                                    "             <th>País registro</th> " +
                                    "         </tr> " +
                                    "     </thead> ";
                //" </table> ";
                conect con1 = new conect();
                //String query = " select  " +
                //                    " TipoSolicitud.TipoSolicitudDescrip as tipodesolicitud, " +
                //                    " tipoplazo.TipoPlazoDescrip as tipoplazo, " +
                //                    " estatusplazo.EstatusPlazoDescrip as estatus, " +
                //                    " caso.CasoNumero as Referencia_King, " +
                //                    " caso.CasoDenominacion as tituloesp, " +
                //                    " caso.CasoTitulo as tituloingles, " +
                //                    " fn_interesados(caso.casoid) as interesados, " +
                //                    " Get_Cliente(caso.casoid) as cliente, " +
                //                    " fn_clase(caso.casoid) as clase, " +
                //                    " caso.Casoid as Casoid, " +
                //                    " estatuscaso.EstatusCasoDescrip as EstatusCasoDescrip, " +
                //                    " plazo.PlazoFecha as PlazoFecha," +
                //                    " Get_paisregistro(caso.PaisId) as paisregistro " +
                //                    " from  " +
                //                    " plazo,  " +
                //                    " tiposolicitud, " +
                //                    " caso,  " +
                //                    " estatusplazo, " +
                //                    " estatuscaso, " +
                //                    " tipoplazo " +
                //                    " where  " +
                //                    " plazo.CasoId = caso.CasoId " +
                //                    " AND caso.TipoSolicitudId = tiposolicitud.TipoSolicitudId " +
                //                    " AND caso.EstatusCasoId = estatuscaso.EstatusCasoId " +
                //                    " AND plazo.EstatusPlazoId = estatusplazo.EstatusPlazoId " +
                //                    " AND plazo.TipoPlazoId = tipoplazo.TipoPlazoId " +
                //                    " AND plazo.TipoPlazoId in(2) " +
                //                    " AND tiposolicitud.TipoSolicitudGrupo = 2" +
                //                    " AND plazo.EstatusPlazoId = 1 " +
                //                    " AND estatuscaso.estautscasoindseg = 1" +
                //                    " AND caso.EstatusCasoId = 1" +
                //                    " order by plazo.PlazoFecha; ";
                String sQuerycasos = " select  " +
                                    " TipoSolicitud.TipoSolicitudDescrip as tipodesolicitud, " +
                                    " tipoplazo.TipoPlazoDescrip as tipoplazo, " +
                                    " estatusplazo.EstatusPlazoDescrip as estatus, " +
                                    " caso_marcas.CasoNumero as Referencia_King, " +
                                    " caso_marcas.CasoTituloespanol as tituloesp, " +
                                    " caso_marcas.CasoTituloingles as tituloingles, " +
                                    " Get_Interesados_tiposol(caso_marcas.casoid, caso_marcas.TipoSolicitudId) as interesados, " +
                                    " Get_Cliente_tiposol(caso_marcas.casoid, caso_marcas.TipoSolicitudId) as cliente, " +
                                    " Damelaclase(caso_marcas.casoid) as clase, " +
                                    " caso_marcas.Casoid as Casoid, " +
                                    " estatuscaso.EstatusCasoDescrip as EstatusCasoDescrip, " +
                                    " plazos_detalle.Fecha_Vencimiento as PlazoFecha," +
                                    " DameNombrePais(caso_marcas.PaisId) as paisregistro " +
                                    " from  " +
                                    " plazos,  " +
                                    " plazos_detalle," +
                                    " tiposolicitud, " +
                                    " caso_marcas,  " +
                                    " estatusplazo, " +
                                    " estatuscaso, " +
                                    " tipoplazo " +
                                    " where  " +
                                    " plazos.CasoId = caso_marcas.CasoId " +
                                    " AND plazos.TipoSolicitudId = caso_marcas.TipoSolicitudId" +
                                    " AND plazos.Plazosid = plazos_detalle.Plazosid" +
                                    " AND caso_marcas.TipoSolicitudId = tiposolicitud.TipoSolicitudId " +
                                    " AND caso_marcas.EstatusCasoId = estatuscaso.EstatusCasoId " +
                                    " AND plazos_detalle.Estatus_plazoid = estatusplazo.EstatusPlazoId " +
                                    " AND plazos_detalle.Tipo_plazoid = tipoplazo.TipoPlazoId " +
                                    " AND plazos_detalle.Tipo_plazoid in(2, 51) " +
                                    " AND tiposolicitud.TipoSolicitudGrupo = 2" +
                                    " AND plazos_detalle.Estatus_plazoid = 1 " +
                                    " AND estatuscaso.estautscasoindseg = 1" +
                                    " AND caso_marcas.EstatusCasoId = 1" +
                                    " order by plazos_detalle.Fecha_Vencimiento;";

                //MySqlDataReader consultacasosconplazospendientes = con1.getdatareader(query); //king
                MySqlDataReader consultacasosconplazospendientes = con1.getdatareader(sQuerycasos);

                /*inicio de documentosdeprioridad*/
                conect con1_doc = new conect();
                //String query_doc = " select  " +
                //                    " TipoSolicitud.TipoSolicitudDescrip as tipodesolicitud, " +
                //                    " tipoplazo.TipoPlazoDescrip as tipoplazo, " +
                //                    " estatusplazo.EstatusPlazoDescrip as estatus, " +
                //                    " caso.CasoNumero as Referencia_King, " +
                //                    " caso.Casoid as Casoid, " +
                //                    " caso.CasoDenominacion as tituloesp, " +
                //                    " caso.CasoTitulo as tituloingles, " +
                //                    " fn_interesados(caso.casoid) as interesados, " +
                //                    " Get_Cliente(caso.casoid) as cliente, " +
                //                    " fn_clase(caso.casoid) as clase, " +
                //                    " estatuscaso.EstatusCasoDescrip as EstatusCasoDescrip, " +
                //                    " plazo.PlazoFecha as PlazoFecha, " +
                //                    " Get_paisregistro(caso.PaisId) as paisregistro " +
                //                    " from  " +
                //                    " plazo,  " +
                //                    " tiposolicitud, " +
                //                    " caso,  " +
                //                    " estatusplazo, " +
                //                    " estatuscaso, " +
                //                    " tipoplazo " +
                //                    " where  " +
                //                    " plazo.CasoId = caso.CasoId " +
                //                    " AND caso.TipoSolicitudId = tiposolicitud.TipoSolicitudId " +
                //                    " AND caso.EstatusCasoId = estatuscaso.EstatusCasoId " +
                //                    " AND plazo.EstatusPlazoId = estatusplazo.EstatusPlazoId " +
                //                    " AND plazo.TipoPlazoId = tipoplazo.TipoPlazoId " +
                //                    " AND plazo.TipoPlazoId in(21, 24) " +
                //                    " AND tiposolicitud.TipoSolicitudGrupo = 2" +
                //                    " AND plazo.EstatusPlazoId = 1 " +
                //                    " AND estatuscaso.estautscasoindseg = 1" +
                //                    " order by plazo.PlazoFecha; ";
                String sQuerydocs = " select  " +
                                    " TipoSolicitud.TipoSolicitudDescrip as tipodesolicitud, " +
                                    " tipoplazo.TipoPlazoDescrip as tipoplazo, " +
                                    " estatusplazo.EstatusPlazoDescrip as estatus, " +
                                    " caso_marcas.CasoNumero as Referencia_King, " +
                                    " caso_marcas.Casoid as Casoid, " +
                                    " caso_marcas.CasoTituloespanol as tituloesp, " +
                                    " caso_marcas.CasoTituloingles as tituloingles, " +
                                    " Get_Interesados_tiposol(caso_marcas.casoid, caso_marcas.TipoSolicitudId) as interesados, " +
                                    " Get_Cliente_tiposol(caso_marcas.casoid, caso_marcas.TipoSolicitudId) as cliente, " +
                                    " Damelaclase(caso_marcas.casoid) as clase, " +
                                    " estatuscaso.EstatusCasoDescrip as EstatusCasoDescrip, " +
                                    " plazos_detalle.Fecha_Vencimiento as PlazoFecha, " +
                                    " DameNombrePais(caso_marcas.PaisId) as paisregistro " +
                                    " from  " +
                                    " plazos,  " +
                                    " plazos_detalle," +
                                    " tiposolicitud, " +
                                    " caso_marcas,  " +
                                    " estatusplazo, " +
                                    " estatuscaso, " +
                                    " tipoplazo " +
                                    " where  " +
                                    " plazos.CasoId = caso_marcas.CasoId " +
                                    " AND plazos.TipoSolicitudId = caso_marcas.TipoSolicitudId" +
                                    " AND plazos.Plazosid = plazos_detalle.Plazosid" +
                                    " AND caso_marcas.TipoSolicitudId = tiposolicitud.TipoSolicitudId " +
                                    " AND caso_marcas.EstatusCasoId = estatuscaso.EstatusCasoId " +
                                    " AND plazos_detalle.Estatus_plazoid = estatusplazo.EstatusPlazoId " +
                                    " AND plazos_detalle.Tipo_plazoid = tipoplazo.TipoPlazoId " +
                                    " AND plazos_detalle.Tipo_plazoid in(21, 24) " +
                                    " AND tiposolicitud.TipoSolicitudGrupo = 2" +
                                    " AND plazos_detalle.Estatus_plazoid = 1 " +
                                    " AND estatuscaso.estautscasoindseg = 1" +
                                    " order by plazos_detalle.Fecha_Vencimiento;";
                //MySqlDataReader consultacasosconplazospendientes_doc = con1_doc.getdatareader(query_doc);//king   
                MySqlDataReader consultacasosconplazospendientes_doc = con1_doc.getdatareader(sQuerydocs);

                /* Fin inicio de documentosdeprioridad*/

                String sBodytable = "";
                int iNum = 1;
                while (consultacasosconplazospendientes.Read())
                {
                    String sFecha = validareader("PlazoFecha", "CasoId", consultacasosconplazospendientes);
                    String sMes = "";
                    if (sFecha != "")
                    {
                        sFecha = sFecha.Substring(0, 10);
                        DateTime fecha = Convert.ToDateTime(sFecha);
                        sMes = MonthName(fecha.Month);
                    }
                    String titulodelainvencion = "";
                    if (validareader("tituloesp", "CasoId", consultacasosconplazospendientes) != "")
                    {
                        titulodelainvencion = validareader("tituloesp", "CasoId", consultacasosconplazospendientes);
                    }
                    else
                    {
                        titulodelainvencion = validareader("tituloingles", "CasoId", consultacasosconplazospendientes);
                    }
                    String titular, cliente;
                    titular = analizatitular(validareader("interesados", "CasoId", consultacasosconplazospendientes));
                    cliente = validareader("cliente", "CasoId", consultacasosconplazospendientes);
                    //if (titular != "" && titular.Length > 24)
                    //{
                    //    titular = titular.Substring(9, 15) + "..";
                    //}
                    //else
                    //{
                    //    if (titular != "")
                    //    {
                    //        titular = titular.Substring(9, titular.Length - 1) + "..";
                    //    }
                    //}
                    if (cliente != "" && cliente.Length > 15)
                    {
                        cliente = cliente.Substring(0, 15) + "..";
                    }

                    int result = iNum % 2;
                    String sEstilo = "style='background: #dedede;'";
                    if (result == 0)
                    {
                        sEstilo = "";
                    }

                    sCasospendientes += "<tr " + sEstilo + ">";
                    sCasospendientes += "<td><center>" + iNum + "</center></td> ";
                    sCasospendientes += "<td>" + validareader("tipodesolicitud", "CasoId", consultacasosconplazospendientes) + "</td> ";
                    sCasospendientes += "<td>" + validareader("tipoplazo", "CasoId", consultacasosconplazospendientes) + "</td> ";
                    sCasospendientes += "<td>" + titulodelainvencion + "</td> ";
                    sCasospendientes += "<td>" + validareader("clase", "CasoId", consultacasosconplazospendientes) + "</td> ";
                    sCasospendientes += "<td><center>" + validareader("Referencia_King", "CasoId", consultacasosconplazospendientes) + "</center></td> ";
                    sCasospendientes += "<td><center>" + validareader("Casoid", "CasoId", consultacasosconplazospendientes) + "</center></td> ";
                    sCasospendientes += "<td>" + validareader("PlazoFecha", "CasoId", consultacasosconplazospendientes).Substring(0, 10) + "</td> ";
                    sCasospendientes += "<td>" + sMes + "</td> ";
                    sCasospendientes += "<td>" + titular + "</td> ";
                    sCasospendientes += "<td>" + cliente + "</td> ";

                    sCasospendientes += "<td>" + validareader("paisregistro", "CasoId", consultacasosconplazospendientes) + "</td> ";

                    sCasospendientes += "</tr>";
                    iNum++;
                }

                while (consultacasosconplazospendientes_doc.Read())
                {
                    String sFecha = validareader("PlazoFecha", "CasoId", consultacasosconplazospendientes_doc);
                    String sMes = "";
                    if (sFecha != "")
                    {
                        sFecha = sFecha.Substring(0, 10);
                        DateTime fecha = Convert.ToDateTime(sFecha);
                        sMes = MonthName(fecha.Month);
                    }
                    String titulodelainvencion = "";
                    if (validareader("tituloesp", "CasoId", consultacasosconplazospendientes_doc) != "")
                    {
                        titulodelainvencion = validareader("tituloesp", "CasoId", consultacasosconplazospendientes_doc);
                    }
                    else
                    {
                        titulodelainvencion = validareader("tituloingles", "CasoId", consultacasosconplazospendientes_doc);
                    }
                    String titular, cliente;
                    titular = analizatitular(validareader("interesados", "CasoId", consultacasosconplazospendientes_doc));
                    cliente = validareader("cliente", "CasoId", consultacasosconplazospendientes_doc);
                    //if (titular != "" && titular.Length > 24)
                    //{
                    //    titular = titular.Substring(9, 15) + "..";
                    //}
                    //else
                    //{
                    //    if (titular != "")
                    //    {
                    //        titular = titular.Substring(9, titular.Length - 1) + "..";
                    //    }
                    //}

                    if (cliente != "" && cliente.Length > 15)
                    {
                        cliente = cliente.Substring(0, 15) + "..";
                    }

                    int result = iNum % 2;
                    String sEstilo = "style='background: #dedede;'";
                    if (result == 0)
                    {
                        sEstilo = "";
                    }
                    sCasospendientes += "<tr " + sEstilo + ">";
                    sCasospendientes += "<td><center>" + iNum + "</center></td> ";
                    sCasospendientes += "<td>" + validareader("tipodesolicitud", "CasoId", consultacasosconplazospendientes_doc) + "</td> ";
                    sCasospendientes += "<td>" + validareader("tipoplazo", "CasoId", consultacasosconplazospendientes_doc) + "</td> ";
                    sCasospendientes += "<td>" + titulodelainvencion + "</td> ";
                    sCasospendientes += "<td>" + validareader("clase", "CasoId", consultacasosconplazospendientes_doc) + "</td> ";
                    sCasospendientes += "<td><center>" + validareader("Referencia_King", "CasoId", consultacasosconplazospendientes_doc) + "</center></td> ";
                    sCasospendientes += "<td><center>" + validareader("Casoid", "CasoId", consultacasosconplazospendientes_doc) + "</center></td> ";
                    sCasospendientes += "<td>" + validareader("PlazoFecha", "CasoId", consultacasosconplazospendientes_doc).Substring(0, 10) + "</td> ";
                    sCasospendientes += "<td>" + sMes + "</td> ";
                    sCasospendientes += "<td>" + titular + "</td> ";
                    sCasospendientes += "<td>" + cliente + "</td> ";

                    sCasospendientes += "<td>" + validareader("paisregistro", "CasoId", consultacasosconplazospendientes_doc).Substring(0, 10) + "</td> ";

                    sCasospendientes += "</tr>";
                    iNum++;
                }
                imarcatotal = iNum;
                String tCuerpodelcorreo = "<tbody>" + sCasospendientes + "</tbody></table>";
                return encabezado + tCuerpodelcorreo;
            }
            catch (Exception Ex)
            {
                new filelog("Excepcion 714:", Ex.Message);
                return "";
            }
        }
        public String generabodymailpatentes()
        {
            try
            {
                String encabezado = " <table border='1'> " +
                                    "     <thead> " +
                                    "         <tr> " +
                                    "             <th>Número</th> " +
                                    "             <th>Tipo de Solicitud</th> " +
                                    "             <th>Tipo de Plazo</th> " +
                                    "             <th>Caso Número</th> " +
                                    "             <th>CasoId</th> " +
                                    "             <th>Plazo Fecha</th> " +
                                    "             <th>Mes</th> " +
                                    "             <th>Titular</th> " +
                                    "             <th>Cliente</th> " +
                                    "             <th>Título</th> " +
                                    "             <th>País de Registro</th> " +
                                    "         </tr> " +
                                    "     </thead> ";
                //" </table> ";
                //consultamos las solicitudes
                String sCasospendientes = "";
                conect con1 = new conect();
                //String query = " select  " +
                //                    " TipoSolicitud.TipoSolicitudDescrip as tipodesolicitud, " +
                //                    " tipoplazo.TipoPlazoDescrip as tipoplazo, " +
                //                    " estatusplazo.EstatusPlazoDescrip as estatus, " +
                //                    " caso.CasoNumero as Referencia_King, " +
                //                    " caso.Casoid as Casoid, " +
                //                    " caso.CasoDenominacion as tituloesp, " +
                //                    " caso.CasoTitulo as tituloingles, " +
                //                    " fn_interesados(caso.casoid) as interesados, " +
                //                    " Dameelclientetipo(caso.casoid) as cliente, " +
                //                    " estatuscaso.EstatusCasoDescrip as EstatusCasoDescrip, " +
                //                    " plazo.PlazoFecha as PlazoFecha, " +
                //                    " Get_paisregistro(caso.PaisId) as paisregistro " +
                //                    " from  " +
                //                    " plazo,  " +
                //                    " tiposolicitud, " +
                //                    " caso,  " +
                //                    " estatusplazo, " +
                //                    " estatuscaso, " +
                //                    " tipoplazo " +
                //                    " where  " +
                //                    " plazo.CasoId = caso.CasoId " +
                //                    " AND caso.TipoSolicitudId = tiposolicitud.TipoSolicitudId " +
                //                    " AND caso.EstatusCasoId = estatuscaso.EstatusCasoId " +
                //                    " AND plazo.EstatusPlazoId = estatusplazo.EstatusPlazoId " +
                //                    " AND plazo.TipoPlazoId = tipoplazo.TipoPlazoId " +
                //                    " AND plazo.TipoPlazoId in(2) " +
                //                    " AND tiposolicitud.TipoSolicitudGrupo = 1" +
                //                    " AND plazo.EstatusPlazoId = 1" +
                //                    " AND estatuscaso.estautscasoindseg = 1" +
                //                    " AND estatuscaso.EstatusCasoId = 1" +
                //                    " order by plazo.PlazoFecha; ";
                String sQuerycasosking = " select  " +
                                    " TipoSolicitud.TipoSolicitudDescrip as tipodesolicitud, " +
                                    " tipoplazo.TipoPlazoDescrip as tipoplazo, " +
                                    " estatusplazo.EstatusPlazoDescrip as estatus, " +
                                    " caso_patente.CasoNumero as Referencia_King, " +
                                    " caso_patente.Casoid as Casoid, " +
                                    " caso_patente.CasoTituloespanol as tituloesp, " +
                                    " caso_patente.CasoTituloingles as tituloingles, " +
                                    " Get_Interesados_tiposol(caso_patente.casoid, caso_patente.TipoSolicitudId) as interesados, " +
                                    " Get_Cliente_tiposol(caso_patente.casoid, caso_patente.TipoSolicitudId) as cliente, " +
                                    " estatuscaso.EstatusCasoDescrip as EstatusCasoDescrip, " +
                                    " plazos_detalle.Fecha_Vencimiento as PlazoFecha, " +
                                    " DameNombrePais(caso_patente.PaisId) as paisregistro " +
                                    " from  " +
                                    " plazos,  " +
                                    " plazos_detalle," +
                                    " tiposolicitud, " +
                                    " caso_patente,  " +
                                    " estatusplazo, " +
                                    " estatuscaso, " +
                                    " tipoplazo " +
                                    " where  " +
                                    " plazos.CasoId = caso_patente.CasoId " +
                                    " AND plazos.TipoSolicitudId = caso_patente.TipoSolicitudId" +
                                    " AND plazos.Plazosid = plazos_detalle.Plazosid" +
                                    " AND caso_patente.TipoSolicitudId = tiposolicitud.TipoSolicitudId " +
                                    " AND caso_patente.EstatusCasoId = estatuscaso.EstatusCasoId " +
                                    " AND plazos_detalle.Estatus_plazoid = estatusplazo.EstatusPlazoId " +
                                    " AND plazos_detalle.Tipo_plazoid = tipoplazo.TipoPlazoId " +
                                    " AND plazos_detalle.Tipo_plazoid in(2) " +
                                    " AND tiposolicitud.TipoSolicitudGrupo = 1" +
                                    " AND plazos_detalle.Estatus_plazoid = 1" +
                                    " AND estatuscaso.estautscasoindseg = 1" +
                                    " AND estatuscaso.EstatusCasoId = 1" +
                                    " order by plazos_detalle.Fecha_Vencimiento;";
                //MySqlDataReader consultacasosconplazospendientes = con1.getdatareader(query);//king
                MySqlDataReader consultacasosconplazospendientes = con1.getdatareader(sQuerycasosking);//casos_king
                //Fin consultamos las solicitudes

                //inicio de docuemntos de prioridad
                conect con1_doc = new conect();
                //String query_doc = " select  " +
                //                    " TipoSolicitud.TipoSolicitudDescrip as tipodesolicitud, " +
                //                    " tipoplazo.TipoPlazoDescrip as tipoplazo, " +
                //                    " estatusplazo.EstatusPlazoDescrip as estatus, " +
                //                    " caso.CasoNumero as Referencia_King, " +
                //                    " caso.Casoid as Casoid, " +
                //                    " caso.CasoDenominacion as tituloesp, " +
                //                    " caso.CasoTitulo as tituloingles, " +
                //                    " fn_interesados(caso.casoid) as interesados, " +
                //                    " Get_Cliente(caso.casoid) as cliente, " +
                //                    " estatuscaso.EstatusCasoDescrip as EstatusCasoDescrip, " +
                //                    " plazo.PlazoFecha as PlazoFecha, " +
                //                    " Get_paisregistro(caso.PaisId) as paisregistro " +
                //                    " from  " +
                //                    " plazo,  " +
                //                    " tiposolicitud, " +
                //                    " caso,  " +
                //                    " estatusplazo, " +
                //                    " estatuscaso, " +
                //                    " tipoplazo " +
                //                    " where  " +
                //                    " plazo.CasoId = caso.CasoId " +
                //                    " AND caso.TipoSolicitudId = tiposolicitud.TipoSolicitudId " +
                //                    " AND caso.EstatusCasoId = estatuscaso.EstatusCasoId " +
                //                    " AND plazo.EstatusPlazoId = estatusplazo.EstatusPlazoId " +
                //                    " AND plazo.TipoPlazoId = tipoplazo.TipoPlazoId " +
                //                    " AND plazo.TipoPlazoId in(21, 24) " +
                //                    " AND tiposolicitud.TipoSolicitudGrupo = 1" +
                //                    " AND plazo.EstatusPlazoId = 1" +
                //                    " AND estatuscaso.estautscasoindseg = 1" +
                //                    " order by plazo.PlazoFecha; ";
                String squerydoc = " select  " +
                                 " TipoSolicitud.TipoSolicitudDescrip as tipodesolicitud, " +
                                 " tipoplazo.TipoPlazoDescrip as tipoplazo, " +
                                 " estatusplazo.EstatusPlazoDescrip as estatus, " +
                                 " caso_patente.CasoNumero as Referencia_King, " +
                                 " caso_patente.Casoid as Casoid, " +
                                 " caso_patente.CasoTituloespanol as tituloesp, " +
                                 " caso_patente.CasoTituloingles as tituloingles, " +
                                 " Get_Interesados_tiposol(caso_patente.casoid, caso_patente.TipoSolicitudId) as interesados, " +
                                 " Get_Cliente_tiposol(caso_patente.casoid, caso_patente.TipoSolicitudId) as cliente, " +
                                 " estatuscaso.EstatusCasoDescrip as EstatusCasoDescrip, " +
                                 " plazos_detalle.Fecha_Vencimiento as PlazoFecha, " +
                                 " DameNombrePais(caso_patente.PaisId) as paisregistro " +
                                 " from  " +
                                 " plazos,  " +
                                 " plazos_detalle," +
                                 " tiposolicitud, " +
                                 " caso_patente,  " +
                                 " estatusplazo, " +
                                 " estatuscaso, " +
                                 " tipoplazo " +
                                 " where  " +
                                 " plazos.CasoId = caso_patente.CasoId " +
                                 " AND plazos.TipoSolicitudId = caso_patente.TipoSolicitudId" +
                                 " AND plazos.Plazosid = plazos_detalle.Plazosid" +
                                 " AND caso_patente.TipoSolicitudId = tiposolicitud.TipoSolicitudId " +
                                 " AND caso_patente.EstatusCasoId = estatuscaso.EstatusCasoId " +
                                 " AND plazos_detalle.Estatus_plazoid = estatusplazo.EstatusPlazoId " +
                                 " AND plazos_detalle.Tipo_plazoid = tipoplazo.TipoPlazoId " +
                                 " AND plazos_detalle.Tipo_plazoid in(21, 24) " +
                                 " AND tiposolicitud.TipoSolicitudGrupo = 1" +
                                 " AND plazos_detalle.Estatus_plazoid = 1" +
                                 " AND estatuscaso.estautscasoindseg = 1" +
                                 " order by plazos_detalle.Fecha_Vencimiento;";
                //MySqlDataReader consultacasosconplazospendientes_doc = con1_doc.getdatareader(query_doc);
                MySqlDataReader consultacasosconplazospendientes_doc = con1_doc.getdatareader(squerydoc);
                //fin documentos de prioridad

                String sBodytable = "";
                int iNum = 1;
                while (consultacasosconplazospendientes.Read())
                {
                    String sFecha = validareader("PlazoFecha", "CasoId", consultacasosconplazospendientes);
                    String sMes = "";
                    if (sFecha != "")
                    {
                        sFecha = sFecha.Substring(0, 10);
                        DateTime fecha = Convert.ToDateTime(sFecha);
                        sMes = MonthName(fecha.Month);
                    }

                    String titulodelainvencion = "";
                    if (validareader("tituloesp", "CasoId", consultacasosconplazospendientes) != "")
                    {
                        titulodelainvencion = validareader("tituloesp", "CasoId", consultacasosconplazospendientes);
                    }
                    else
                    {
                        titulodelainvencion = validareader("tituloingles", "CasoId", consultacasosconplazospendientes);
                    }

                    String titular, cliente;
                    titular = analizatitular(validareader("interesados", "CasoId", consultacasosconplazospendientes));
                    cliente = validareader("cliente", "CasoId", consultacasosconplazospendientes);
                    //if (titular != "" && titular.Length > 24)
                    //{
                    //    titular = titular.Substring(9, 15) + "..";
                    //}
                    //else
                    //{
                    //    if (titular != "")
                    //    {
                    //        titular = titular.Substring(9, titular.Length - 1) + "..";
                    //    }
                    //}
                    String spaisregistro = validareader("paisregistro", "CasoId", consultacasosconplazospendientes);

                    if (cliente != "" && cliente.Length > 15)
                    {
                        cliente = cliente.Substring(0, 15) + "..";
                    }

                    int result = iNum % 2;
                    String sEstilo = "style='background: #dedede;'";
                    if (result == 0)
                    {
                        sEstilo = "";
                    }
                    sCasospendientes += "<tr " + sEstilo + ">";
                    sCasospendientes += "<td><center>" + iNum + "</center></td> ";
                    sCasospendientes += "<td>" + validareader("tipodesolicitud", "CasoId", consultacasosconplazospendientes) + "</td> ";
                    sCasospendientes += "<td>" + validareader("tipoplazo", "CasoId", consultacasosconplazospendientes) + "</td> ";
                    sCasospendientes += "<td><center>" + validareader("Referencia_King", "CasoId", consultacasosconplazospendientes) + "</center></td> ";
                    sCasospendientes += "<td><center>" + validareader("Casoid", "CasoId", consultacasosconplazospendientes) + "</center></td> ";
                    sCasospendientes += "<td>" + validareader("PlazoFecha", "CasoId", consultacasosconplazospendientes).Substring(0, 10) + "</td> ";
                    sCasospendientes += "<td>" + sMes + "</td> ";
                    sCasospendientes += "<td>" + titular + "</td> ";
                    sCasospendientes += "<td>" + cliente + "</td> ";
                    sCasospendientes += "<td>" + titulodelainvencion + "</td> ";

                    sCasospendientes += "<td>" + spaisregistro + "</td> ";

                    sCasospendientes += "</tr>";
                    iNum++;
                }
                consultacasosconplazospendientes.Close();
                con1.Cerrarconexion();
                while (consultacasosconplazospendientes_doc.Read())
                {
                    String sFecha = validareader("PlazoFecha", "CasoId", consultacasosconplazospendientes_doc);
                    String sMes = "";
                    if (sFecha != "")
                    {
                        sFecha = sFecha.Substring(0, 10);
                        DateTime fecha = Convert.ToDateTime(sFecha);
                        sMes = MonthName(fecha.Month);
                    }
                    String titulodelainvencion = "";
                    if (validareader("tituloesp", "CasoId", consultacasosconplazospendientes_doc) != "")
                    {
                        titulodelainvencion = validareader("tituloesp", "CasoId", consultacasosconplazospendientes_doc);
                    }
                    else
                    {
                        titulodelainvencion = validareader("tituloingles", "CasoId", consultacasosconplazospendientes_doc);
                    }
                    String titular, cliente;
                    titular = analizatitular(validareader("interesados", "CasoId", consultacasosconplazospendientes_doc));
                    cliente = validareader("cliente", "CasoId", consultacasosconplazospendientes_doc);
                    //if (titular != "" && titular.Length > 24)
                    //{
                    //    titular = titular.Substring(9, 15) + "..";
                    //}
                    //else
                    //{
                    //    if (titular != "")
                    //    {
                    //        titular = titular.Substring(9, titular.Length - 1) + "..";
                    //    }

                    //}

                    if (cliente != "" && cliente.Length > 15)
                    {
                        cliente = cliente.Substring(0, 15) + "..";
                    }

                    int result = iNum % 2;
                    String sEstilo = "style='background: #dedede;'";
                    if (result == 0)
                    {
                        sEstilo = "";
                    }
                    sCasospendientes += "<tr " + sEstilo + ">";
                    sCasospendientes += "<td><center>" + iNum + "</center></td> ";
                    sCasospendientes += "<td>" + validareader("tipodesolicitud", "CasoId", consultacasosconplazospendientes_doc) + "</td> ";
                    sCasospendientes += "<td>" + validareader("tipoplazo", "CasoId", consultacasosconplazospendientes_doc) + "</td> ";
                    sCasospendientes += "<td><center>" + validareader("Referencia_King", "CasoId", consultacasosconplazospendientes_doc) + "</center></td> ";
                    sCasospendientes += "<td><center>" + validareader("Casoid", "CasoId", consultacasosconplazospendientes_doc) + "</center></td> ";
                    sCasospendientes += "<td>" + validareader("PlazoFecha", "CasoId", consultacasosconplazospendientes_doc).Substring(0, 10) + "</td> ";
                    sCasospendientes += "<td>" + sMes + "</td> ";
                    sCasospendientes += "<td>" + titular + "</td> ";
                    sCasospendientes += "<td>" + cliente + "</td> ";
                    sCasospendientes += "<td>" + titulodelainvencion + "</td> ";
                    sCasospendientes += "<td>" + validareader("paisregistro", "CasoId", consultacasosconplazospendientes_doc) + "</td> ";
                    //paisregistro
                    sCasospendientes += "</tr>";
                    iNum++;
                }
                consultacasosconplazospendientes_doc.Close();
                con1_doc.Cerrarconexion();



                ipattotal = iNum;
                String tCuerpodelcorreo = "<tbody>" + sCasospendientes + "</tbody></table>";
                return encabezado + tCuerpodelcorreo;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Ocurrió un error en la consulta");
                return "Ocurrió un error en la consulta";

            }

        }
        public String generabodymailmarcasAccion()
        {
            String sCasospendientes = "";
            conect con1 = new conect();
            //String query = " select  " +
            //                " TipoSolicitud.TipoSolicitudDescrip as tipodesolicitud, " +
            //                " tipoplazo.TipoPlazoDescrip as tipoplazo, " +
            //                " estatusplazo.EstatusPlazoDescrip as estatus, " +
            //                " caso.CasoNumero as Referencia_King, " +

            //                " caso.CasoDenominacion as CasoDenominacion, " +
            //                " fn_clase(caso.Casoid) as clases, " +

            //                " caso.Casoid as Casoid, " +
            //                " fn_interesados(caso.casoid) as interesados, " +
            //                " Get_Cliente(caso.casoid) as cliente, " +
            //                " estatuscaso.EstatusCasoDescrip as EstatusCasoDescrip, " +
            //                " subtipodocumento.SubTipoDocumentoDescrip as SubTipoDocumentoDescrip," +
            //                " plazo.PlazoFecha as PlazoFecha, " +

            //                " Get_paisregistro(caso.PaisId) as paisregistro " +

            //                " from  " +
            //                " plazo,  " +
            //                " tiposolicitud, " +
            //                " caso,  " +
            //                " estatusplazo, " +
            //                " estatuscaso, " +
            //                " documento," +
            //                " subtipodocumento," +
            //                " tipoplazo " +
            //                " where  " +
            //                " plazo.CasoId = caso.CasoId " +
            //                " AND caso.TipoSolicitudId = tiposolicitud.TipoSolicitudId " +
            //                " AND caso.EstatusCasoId = estatuscaso.EstatusCasoId " +
            //                " AND plazo.EstatusPlazoId = estatusplazo.EstatusPlazoId " +
            //                " AND plazo.TipoPlazoId = tipoplazo.TipoPlazoId " +
            //                " AND plazo.DocumentoId = documento.DocumentoId" +
            //                " AND documento.SubTipoDocumentoId = subtipodocumento.SubTipoDocumentoId" +
            //                " AND plazo.TipoPlazoId in(4) " +
            //                " AND tiposolicitud.TipoSolicitudGrupo = 2" +
            //                " AND plazo.EstatusPlazoId = 1" +
            //                " AND estatuscaso.estautscasoindseg = 1" +
            //                " order by plazo.PlazoFecha;";
            String sQuerycasosking = " Select   " +
                                 " TipoSolicitud.TipoSolicitudDescrip as tipodesolicitud,  " +
                                 " tipoplazo.TipoPlazoDescrip as tipoplazo,  " +
                                 " estatusplazo.EstatusPlazoDescrip as estatus,  " +
                                 " caso_marcas.CasoNumero as Referencia_King,  " +
                                 " caso_marcas.CasoTituloingles as CasoDenominacion,  " +
                                 " Damelaclase(caso_marcas.Casoid) as clases,  " +
                                 " caso_marcas.Casoid as Casoid,  " +
                                 " Get_Interesados_tiposol(caso_marcas.casoid, caso_marcas.TipoSolicitudId) as interesados, " +
                                 " Get_Cliente_tiposol(caso_marcas.casoid, caso_marcas.TipoSolicitudId) as cliente,  " +
                                 " estatuscaso.EstatusCasoDescrip as EstatusCasoDescrip,  " +
                                 " subtipodocumento.SubTipoDocumentoDescrip as SubTipoDocumentoDescrip, " +
                                 " plazos_detalle.Fecha_Vencimiento as PlazoFecha, " +
                                 " DameNombrePais(caso_marcas.PaisId) as paisregistro  " +
                                 " from   " +
                                 " plazos,   " +
                                 " plazos_detalle, " +
                                 " tiposolicitud,  " +
                                 " caso_marcas,   " +
                                 " estatusplazo,  " +
                                 " estatuscaso,  " +
                                 " documento, " +
                                 " subtipodocumento, " +
                                 " tipoplazo  " +
                                 " where   " +
                                 " plazos.CasoId = caso_marcas.CasoId  " +
                                 " AND plazos.TipoSolicitudId = caso_marcas.TipoSolicitudId " +
                                 " AND plazos.Plazosid = plazos_detalle.Plazosid " +
                                 " AND caso_marcas.TipoSolicitudId = tiposolicitud.TipoSolicitudId  " +
                                 " AND caso_marcas.EstatusCasoId = estatuscaso.EstatusCasoId  " +
                                 " AND plazos_detalle.Estatus_plazoid = estatusplazo.EstatusPlazoId  " +
                                 " AND plazos_detalle.Tipo_plazoid = tipoplazo.TipoPlazoId  " +
                                 " AND plazos_detalle.DocumentoId = documento.DocumentoId " +
                                 " AND documento.SubTipoDocumentoId = subtipodocumento.SubTipoDocumentoId " +
                                 " AND plazos_detalle.Tipo_plazoid in(4, 46)  " +
                                 " AND tiposolicitud.TipoSolicitudGrupo = 2 " +
                                 " AND plazos_detalle.Estatus_plazoid = 1 " +
                                 " AND estatuscaso.estautscasoindseg = 1 " +
                                 " order by plazos_detalle.Fecha_Vencimiento; ";
            //new filelog("863", "Query: "+query);
            //MySqlDataReader consultacasosconplazospendientes = con1.getdatareader(query);//king
            MySqlDataReader consultacasosconplazospendientes = con1.getdatareader(sQuerycasosking);//casosking
            String encabezado = " <table border='1'> " +
                                "     <thead> " +
                                "         <tr> " +
                                "             <th>Número</th> " +
                                "             <th>Tipo de Solicitud</th> " +
                                "             <th>Requisito a contestar:</th> " +
                                "             <th>Caso Número</th> " +
                                "             <th>Denominación de la marca</th> " +
                                "             <th>Clase</th> " +
                                "             <th>CasoId</th> " +
                                "             <th>Vencimineto 2 meses</th> " +
                                "             <th>Mes</th> " +
                                "             <th>Titular</th> " +
                                "             <th>Cliente</th> " +
                                "             <th>País de registro</th> " +
                                "         </tr> " +
                                "     </thead> ";
            //" </table> ";
            String sBodytable = "";
            int iNum = 1;
            while (consultacasosconplazospendientes.Read())
            {
                String sFecha = validareader("PlazoFecha", "CasoId", consultacasosconplazospendientes);
                String sMes = "";
                try
                {
                    if (sFecha != "")
                    {
                        sFecha = sFecha.Substring(0, 10);
                        DateTime fecha = Convert.ToDateTime(sFecha);
                        sMes = MonthName(fecha.Month);
                    }
                }
                catch (Exception Ex)
                {
                    new filelog("sFecha Error 943", " :" + Ex.Message);
                }


                String titular, cliente;
                titular = analizatitular(validareader("interesados", "CasoId", consultacasosconplazospendientes));
                cliente = validareader("cliente", "CasoId", consultacasosconplazospendientes);
                //if (titular != "" && titular.Length > 24)
                //{
                //    titular = titular.Substring(9, 15) + "..";
                //}
                //else
                //{
                //    if (titular != "")
                //    {
                //        titular = titular.Substring(9, titular.Length - 1) + "..";
                //    }
                //}
                String sEstilo = "style='background: #dedede;'";
                try
                {
                    if (cliente != "" && cliente.Length > 15)
                    {
                        cliente = cliente.Substring(0, 15) + "..";
                    }

                    int result = iNum % 2;

                    if (result == 0)
                    {
                        sEstilo = "";
                    }
                }
                catch (Exception Ex)
                {
                    new filelog("931", " Error al procesar cliente: " + Ex.Message);
                }


                sCasospendientes += "<tr " + sEstilo + ">";
                sCasospendientes += "<td><center>" + iNum + "</center></td> ";
                sCasospendientes += "<td>" + validareader("tipodesolicitud", "CasoId", consultacasosconplazospendientes) + "</td> ";
                sCasospendientes += "<td>" + validareader("SubTipoDocumentoDescrip", "CasoId", consultacasosconplazospendientes) + "</td> ";
                sCasospendientes += "<td><center>" + validareader("Referencia_King", "CasoId", consultacasosconplazospendientes) + "</center></td> ";

                sCasospendientes += "<td><center>" + validareader("CasoDenominacion", "CasoId", consultacasosconplazospendientes) + "</center></td> ";
                sCasospendientes += "<td><center>" + validareader("clases", "CasoId", consultacasosconplazospendientes) + "</center></td> ";

                sCasospendientes += "<td><center>" + validareader("Casoid", "CasoId", consultacasosconplazospendientes) + "</center></td> ";
                sCasospendientes += "<td>" + /*validareader("PlazoFecha", "CasoId", consultacasosconplazospendientes).Substring(0, 10)*/ sFecha + "</td> ";
                sCasospendientes += "<td>" + sMes + "</td> ";
                sCasospendientes += "<td>" + titular + "</td> ";
                sCasospendientes += "<td>" + cliente + "</td> ";

                sCasospendientes += "<td>" + validareader("paisregistro", "CasoId", consultacasosconplazospendientes) + "</td> ";
                sCasospendientes += "</tr>";
                iNum++;
            }
            imarcatotal_accion = iNum;
            String tCuerpodelcorreo = "<tbody>" + sCasospendientes + "</tbody></table>";
            return encabezado + tCuerpodelcorreo;
        }
        private int SendEmail(string To, string Subject, string body, string SmtpCC)
        {
            string SmtpServer = "smtp.gmail.com";
            string SmtpUser = "mail@vadillo-king.mx";
            string SmtpPwd = "ONEFA2019";
            string SmtpFrom = "smtp.gmail.com";
            //string SmtpCC = "eduardor@dicss.com.mx";
            int SmtpPort = 465;
            string SmtpDisplay = "Vadillo &  King";
            string rstl = "";
            string[] lineasdos;
            try
            {
                String fechalog = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                //StringBuilder sb_log = new StringBuilder();
                //sb_log.Append(fechalog + ":configuracionfilesinicio:" + " Error:");
                String strRutaArchivo = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string fichero = strRutaArchivo + "\\mailking\\conemailking.properties";
                string contenido = String.Empty;
                if (File.Exists(fichero))
                {
                    contenido = File.ReadAllText(fichero);
                    lineasdos = contenido.Split('\n');

                    SmtpServer = lineasdos[4];
                    SmtpUser = lineasdos[5];
                    SmtpPwd = lineasdos[6];
                    SmtpFrom = lineasdos[7];
                    To = "patricio.king@vadillo-king.mx";//lineasdos[10];
                    SmtpCC = lineasdos[10];
                }
                else
                {
                    MessageBox.Show("No existe el archivo de configuración. Aplication_Data\\mailking\\conemailking.properties");
                    new filelog("robot_servicio", "No existe el archivo de configuración. Aplication_Data\\mailking\\conemailking.properties");
                    return 0;
                }
                if (tbCorreodepruebas.Text != "")
                {
                    To = tbCorreodepruebas.Text;
                    SmtpCC = "";
                    //MessageBox.Show("Se enviará correo de prueba sólo a " + tbCorreodepruebas.Text);
                }
                MailBee.Global.LicenseKey = "MN110-8B8932A44B8239779277420FE843-E158";
                Smtp mailer = new Smtp();
                SmtpServer server = new SmtpServer(SmtpServer, SmtpUser, SmtpPwd);
                server.AccountDomain = SmtpServer;
                server.Port = SmtpPort;
                server.AccountName = SmtpUser;
                server.Password = SmtpPwd;

                mailer.SmtpServers.Add(server);
                mailer.Connect();
                mailer.Hello();
                mailer.Login();
                //Get Data From Send Email
                mailer.Message.From.Email = SmtpFrom;
                mailer.Message.From.DisplayName = SmtpDisplay;// "News Letter <newsletter@vadillo-king.mx>";
                mailer.Message.To.AsString = To;
                mailer.Message.Cc.AsString = SmtpCC;
                mailer.Message.Subject = Subject;
                //Embed image (add as attachment with Content-ID value set) of stream
                //Stream Logo = Get_Logo();
                //mailer.Message.Attachments.Add(Logo, "KingEmail.jpg", "<12s4a8a8778c$5664i1b1$ir671781@tlffmdqjobxj>", "image/gif", null, NewAttachmentOptions.None, MailTransferEncoding.Base64);
                //Get Body of Email
                mailer.BodyHtmlText = body;
                //Sender
                bool bBandera = mailer.Send();
                mailer.Dispose();
                //if (bBanderahabilitalog)
                //{
                new filelog("Enviando mail", " A:" + To + " cco:" + SmtpCC + " Asunto:" + Subject);
                //}
                if (bBandera) {
                    return 1;
                } else {
                    return 0;
                }
                
            }
            catch (Exception e)
            {
                new filelog("serivicio", "Error:" + e.Message);
                return 0;
            }
        }
        public String generabodymailpatentesAccion()
        {
            try
            {
                String sCasospendientes = "";
                conect con1 = new conect();
                String sQuerypatentes = " select" +
                                         " TipoSolicitud.TipoSolicitudDescrip as tipodesolicitud, " +
                                         " tipoplazo.TipoPlazoDescrip as tipoplazo, " +
                                         " estatusplazo.EstatusPlazoDescrip as estatus, " +
                                         " caso_patente.CasoNumero as Referencia_King, " +
                                         " caso_patente.Casoid as Casoid, " +
                                         " Get_Interesados_tiposol(caso_patente.casoid, caso_patente.TipoSolicitudId) as interesados, " +
                                         " Get_Cliente_tiposol(caso_patente.casoid, caso_patente.TipoSolicitudId) as cliente, " +
                                         " estatuscaso.EstatusCasoDescrip as EstatusCasoDescrip, " +
                                         " subtipodocumento.SubTipoDocumentoDescrip as SubTipoDocumentoDescrip," +
                                         " plazos_detalle.Fecha_Vencimiento as PlazoFecha, " +
                                         " DameNombrePais(caso_patente.PaisId) as paisregistro " +
                                         " from  " +
                                         " plazos,  " +
                                         " plazos_detalle," +
                                         " tiposolicitud, " +
                                         " caso_patente,  " +
                                         " estatusplazo, " +
                                         " estatuscaso, " +
                                         " documento," +
                                         " subtipodocumento," +
                                         " tipoplazo " +
                                         " where  " +
                                         " plazos.CasoId = caso_patente.CasoId " +
                                         " AND plazos.TipoSolicitudId = caso_patente.TipoSolicitudId" +
                                         " AND plazos.Plazosid = plazos_detalle.Plazosid" +
                                         " AND caso_patente.TipoSolicitudId = tiposolicitud.TipoSolicitudId " +
                                         " AND caso_patente.EstatusCasoId = estatuscaso.EstatusCasoId " +
                                         " AND plazos_detalle.Estatus_plazoid = estatusplazo.EstatusPlazoId " +
                                         " AND plazos_detalle.Tipo_plazoid = tipoplazo.TipoPlazoId " +
                                         " AND plazos_detalle.DocumentoId = documento.DocumentoId" +
                                         " AND documento.SubTipoDocumentoId = subtipodocumento.SubTipoDocumentoId" +
                                         " AND plazos_detalle.Tipo_plazoid in(4) " +
                                         " AND tiposolicitud.TipoSolicitudGrupo = 1" +
                                         " AND plazos_detalle.Estatus_plazoid = 1" +
                                         " AND estatuscaso.estautscasoindseg = 1" +
                                         " order by plazos_detalle.Fecha_Vencimiento;";
                //MySqlDataReader consultacasosconplazospendientes = con1.getdatareader(query);//king
                MySqlDataReader consultacasosconplazospendientes = con1.getdatareader(sQuerypatentes);
                //aplicar los comentarios en marcas de agregar el mes y la fecha de notificacion
                String encabezado = " <table border='1'> " +
                                    "     <thead> " +
                                    "         <tr> " +
                                    "             <th>Número</th> " +
                                    "             <th>Tipo de Solicitud</th> " +//agregar fecha notificacion
                                    "             <th>Requisito a contestar:</th> " +
                                    "             <th>Caso Número</th> " +
                                    "             <th>CasoId</th> " +
                                    "             <th>Vencimineto 2 meses</th> " +//agregar mes numero que se llame mes corriendo
                                    "             <th>Mes</th> " +
                                    "             <th>Titular</th> " +
                                    "             <th>Cliente</th> " +
                                    "             <th>País de Registro</th> " +
                                    "         </tr> " +
                                    "     </thead> ";
                //" </table> ";
                String sBodytable = "";
                int iNum = 1;
                while (consultacasosconplazospendientes.Read())
                {
                    String sFecha = validareader("PlazoFecha", "CasoId", consultacasosconplazospendientes);
                    String sMes = "";
                    if (sFecha != "")
                    {
                        sFecha = sFecha.Substring(0, 10);
                        DateTime fecha = Convert.ToDateTime(sFecha);
                        sMes = MonthName(fecha.Month);
                    }

                    String titular, cliente;
                    titular = analizatitular(validareader("interesados", "CasoId", consultacasosconplazospendientes));
                    cliente = validareader("cliente", "CasoId", consultacasosconplazospendientes);
                    //if (titular != "" && titular.Length > 24)
                    //{
                    //    titular = titular.Substring(9, 15) + "..";
                    //}
                    //else
                    //{
                    //    if (titular != "")
                    //    {
                    //        titular = titular.Substring(9, titular.Length - 1) + "..";
                    //    }
                    //}

                    if (cliente != "" && cliente.Length > 15)
                    {
                        cliente = cliente.Substring(0, 15) + "..";
                    }

                    int result = iNum % 2;
                    String sEstilo = "style='background: #dedede;'";
                    if (result == 0)
                    {
                        sEstilo = "";
                    }
                    String sPlazoFecha = validareader("PlazoFecha", "CasoId", consultacasosconplazospendientes);
                    if (sPlazoFecha != "")
                    {
                        sPlazoFecha = validareader("PlazoFecha", "CasoId", consultacasosconplazospendientes).Substring(0, 10);
                    }
                    sCasospendientes += "<tr " + sEstilo + ">";
                    sCasospendientes += "<td><center>" + iNum + "</center></td> ";
                    sCasospendientes += "<td>" + validareader("tipodesolicitud", "CasoId", consultacasosconplazospendientes) + "</td> ";
                    sCasospendientes += "<td>" + validareader("SubTipoDocumentoDescrip", "CasoId", consultacasosconplazospendientes) + "</td> ";
                    sCasospendientes += "<td><center>" + validareader("Referencia_King", "CasoId", consultacasosconplazospendientes) + "</center></td> ";
                    sCasospendientes += "<td><center>" + validareader("Casoid", "CasoId", consultacasosconplazospendientes) + "</center></td> ";
                    sCasospendientes += "<td>" + sPlazoFecha + "</td> ";
                    sCasospendientes += "<td>" + sMes + "</td> ";
                    sCasospendientes += "<td>" + titular + "</td> ";
                    sCasospendientes += "<td>" + cliente + "</td> ";
                    sCasospendientes += "<td>" + validareader("paisregistro", "CasoId", consultacasosconplazospendientes) + "</td> ";
                    sCasospendientes += "</tr>";
                    iNum++;
                }
                ipattotal_accion = iNum;
                String tCuerpodelcorreo = "<tbody>" + sCasospendientes + "</tbody></table>";
                return encabezado + tCuerpodelcorreo;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public String analizatitular(String titular)
        {
            try
            {
                if (titular != "" && titular.Length > 24)
                {
                    titular = titular.Substring(0, 15) + "..";
                }
                else
                {
                    if (titular != "")
                    {
                        titular = titular.Substring(0, titular.Length - 10) + "..";
                    }
                }
                return titular.Trim();
            }
            catch (Exception Ex)
            {
                new filelog("Titular: ", "Valor: " + titular);
            }
            return "";
        }
        public String validareader(String campoText, String campoValue, MySqlDataReader mresultado)
        {
            try
            {
                String cItemresult = "";
                if (!mresultado.IsDBNull(mresultado.GetOrdinal(campoText)))
                {
                    cItemresult = mresultado.GetString(mresultado.GetOrdinal(campoText));
                }
                return cItemresult;
            }
            catch (Exception E)
            {
                return "";
            }
        }
        public string MonthName(int month)
        {
            DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;
            return dtinfo.GetMonthName(month);
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbCorreodepruebas.Text != "")
                {
                    enviaremail();
                }
                else
                {
                    MessageBox.Show("Para enviar prueba es necesario agregar el correo de prueba.");
                    tbCorreodepruebas.Focus();
                }
            }
            catch (Exception Ex)
            {
                new filelog("enviar prueba email", "Error: " + Ex.Message);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try {
                enviaremail();
                //MessageBox.Show("Correos enviados: " + iCountmailsend);
            }
            catch (Exception exs) { 
            }
            
        }
    }
}
