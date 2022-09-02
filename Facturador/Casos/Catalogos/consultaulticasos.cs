using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Facturador
{
    public partial class consultaulticasos : Form
    {
        funcionesdicss obj = new funcionesdicss();
        public consultaulticasos()
        {
            InitializeComponent();
            conect con = new conect();
            String query = "select * from grupo;";
            MySqlDataReader respuestastiposol = con.getdatareader(query);
            while (respuestastiposol.Read())
            {
                cbSelectGrupo.Items.Add(obj.validareader("GrupoDescripcion", "GrupoId", respuestastiposol));
            }

            ComboboxItem combopph = new ComboboxItem();
            combopph.Value = "PPH";
            combopph.Text = "PPH";
            cbSelectGrupo.Items.Add(combopph);

            respuestastiposol.Close();
            con.Cerrarconexion();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try {
                if (cbSelectGrupo.SelectedItem != null)
                {
                    ComboboxItem comGrop = (cbSelectGrupo.SelectedItem as ComboboxItem);
                    String sTiposolicitud = (cbSelectGrupo.SelectedItem as ComboboxItem).Text;
                    int iTiposolicitud = Int32.Parse((cbSelectGrupo.SelectedItem as ComboboxItem).Value.ToString());
                    String sTabla = "";
                    String sQuerytable = "";

                    switch (sTiposolicitud)
                    {
                        case "Todos":
                            {
                                sTabla = "todas";
                            }
                            break;
                        case "Patentes":
                            {
                                sTabla = "todas";
                                sQuerytable = " SELECT  " +
                                            " caso_patente.CasoId as CasoId, " +
                                            " Get_Cliente(CasoId) as cliente_Nombre, " +
                                            " caso_patente.TipoSolicitudId as TipoSolicitudId, " +
                                            " caso_patente.SubTipoSolicitudId as SubTipoSolicitudId, " +
                                            " caso_patente.TipoPctId as TipoPctId, " +
                                            " caso_patente.CasoTituloespanol as CasoTituloespanol, " +
                                            " caso_patente.CasoTituloingles as CasoTituloingles, " +
                                            " caso_patente.IdiomaId as IdiomaId, " +
                                            " caso_patente.CasoFechaConcesion as CasoFechaConcesion, " +
                                            " caso_patente.CasoFechaRecepcion as CasoFechaRecepcion, " +
                                            " caso_patente.CasoFechaVigencia as CasoFechaVigencia, " +
                                            " caso_patente.CasoFechaPublicacionSolicitud as CasoFechaPublicacionSolicitud, " +
                                            " caso_patente.CasoFechaLegal as CasoFechaLegal, " +
                                            " caso_patente.CasoNumConcedida as CasoNumConcedida, " +
                                            " caso_patente.CasoNumeroExpedienteLargo as CasoNumeroExpedienteLargo, " +
                                            " caso_patente.CasoNumero as CasoNumero, " +
                                            " caso_patente.ResponsableId as ResponsableId, " +
                                            " caso_patente.CasoTipoCaptura as CasoTipoCaptura, " +
                                            " caso_patente.CasoTitular as CasoTitular, " +
                                            " caso_patente.EstatusCasoId as EstatusCasoId, " +
                                            " caso_patente.UsuarioId as UsuarioId, " +
                                            " caso_patente.AreaImpiId as AreaImpiId, " +
                                            " caso_patente.CasoFechaInternacional as CasoFechaInternacional, " +
                                            " caso_patente.PaisId as PaisId, " +
                                            " caso_patente.CasoFechaPruebaUsoSig as CasoFechaPruebaUsoSig, " +
                                            " caso_patente.CasoFechaFilingCliente as CasoFechaFilingCliente, " +
                                            " caso_patente.CasoFechaFilingSistema as CasoFechaFilingSistema, " +
                                            " caso_patente.CasoFechaDivulgacionPrevia as CasoFechaDivulgacionPrevia, " +
                                            " caso_patente.CasoFechaCartaCliente as CasoFechaCartaCliente, " +

                                            " Get_prioridad(Casoid) as PrioridadNumero, " +
                                            " Get_interesados(Casoid) as InteresadoNombre, " +
                                            " Get_Tipodesolicitud(Casoid) as TipoSolicitudDescrip, " +
                                            " Get_Referencia(Casoid) as referencia, " +
                                            " Get_Paisclave_patente(Casoid) as PaisClave, " +

                                            " caso_patente.Divicionalid as Divicionalid " +
                                            " FROM " +
                                            " caso_patente" +
                                            " order by CasoId desc limit  " + tbLimitcasos.Text + ";";

                            }
                            break;
                        case "Marcas":
                            {
                                sTabla = "todas";
                                sQuerytable = " SELECT  " +
                                            " Get_prioridad(Casoid) as PrioridadNumero, " +
                                            " Get_interesados(Casoid) as InteresadoNombre, " +
                                            " Get_Tipodesolicitud_marcas(Casoid) as TipoSolicitudDescrip, " +
                                            " Get_Referencia(Casoid) as referencia, " +
                                            " Get_Paisclave_marcas(Casoid) as PaisClave, " +
                                            " Get_Cliente(Casoid) as cliente_Nombre, " +
                                            " Get_Clase_productos(Casoid) as CasoProductosClase, " +
                                            " `caso_marcas`.`CasoId`, " +
                                            " `caso_marcas`.`TipoSolicitudId`, " +
                                            " `caso_marcas`.`SubTipoSolicitudId`, " +
                                            " `caso_marcas`.`CasoTituloingles`, " +
                                            " `caso_marcas`.`CasoTituloespanol`, " +
                                            " `caso_marcas`.`IdiomaId`, " +
                                            " `caso_marcas`.`CasoFechaConcesion`, " +
                                            " `caso_marcas`.`CasoFechaLegal`, " +
                                            " `caso_marcas`.`CasoFechaDivulgacionPrevia`, " +
                                            " `caso_marcas`.`CasoFechaRecepcion`, " +
                                            " `caso_marcas`.`CasoFechaVigencia`, " +
                                            " `caso_marcas`.`CasoNumeroConcedida`, " +
                                            " `caso_marcas`.`CasoNumeroExpedienteLargo`, " +
                                            " `caso_marcas`.`CasoNumero`, " +
                                            " `caso_marcas`.`ResponsableId`, " +
                                            " `caso_marcas`.`TipoMarcaId`, " +
                                            " `caso_marcas`.`CasoLeyendaNoReservable`, " +
                                            " `caso_marcas`.`CasoFechaAlta`, " +
                                            " `caso_marcas`.`CasoTipoCaptura`, " +
                                            " `caso_marcas`.`CasoTitular`, " +
                                            " `caso_marcas`.`CasoFechaFilingSistema`, " +
                                            " `caso_marcas`.`CasoFechaFilingCliente`, " +
                                            " `caso_marcas`.`CasoFechaCartaCliente`, " +
                                            " `caso_marcas`.`EstatusCasoId`, " +
                                            " `caso_marcas`.`UsuarioId`, " +
                                            " `caso_marcas`.`PaisId`, " +
                                            " `caso_marcas`.`CasoFechaPruebaUsoSig`, " +
                                            " `caso_marcas`.`CasoNumConcedida`, " +
                                            " `caso_marcas`.`CasoFechaprobouso`, " +
                                            " `caso_marcas`.`CasoFechainiciouso` " +
                                            " FROM " +
                                            " caso_marcas" +
                                            " order by CasoId desc limit  " + tbLimitcasos.Text + ";";
                            }
                            break;
                        case "Contencioso":
                            {
                                sTabla = "todas";

                            }
                            break;
                        case "Consulta":
                            {
                                sTabla = "todas";
                            }
                            break;
                        case "Oposicion a solicitudes":
                            {
                                sTabla = "todas";
                            }
                            break;
                        case "Variedades vegetales":
                            {
                                sTabla = "todas";
                            }
                            break;
                        case "Derechos de autor":
                            {
                                sTabla = "todas";
                            }
                            break;
                        case "Reserva de derechos":
                            {
                                sTabla = "todas";
                            }
                            break;
                    }
                    /*iniciamos la busqueda*/
                    //borramos el listview 
                    conect con = new conect();
                    //listViewCasos.Items.Clear();
                    int rowcolor = 0;
                    MySqlDataReader respuestastring3;

                    try
                    {
                        String sQuerywhere = "";
                        String sQuerywherecaso = "";
                        String sCampoconsulta = "";
                        String stablaconsulta = "";
                        String sWhereconsulta = "";

                        //Querywhere = sQuerywhere.Substring(4, sQuerywhere.Length - 4);
                        String sQuerybusqueda = "";
                        sQuerybusqueda = sQuerytable;
                        respuestastring3 = con.getdatareader(sQuerybusqueda);
                        dgRowCasos.Rows.Clear();
                        while (respuestastring3.Read())
                        {
                            String sCasoidconsulta = obj.validareader("CasoId", "CasoId", respuestastring3).Text;

                            String sPrioridades = "";
                            int residuo = rowcolor % 2;

                            ListViewItem listaitems = new ListViewItem(obj.validareader("PaisClave", "CasoId", respuestastring3).Text);
                            listaitems.SubItems.Add(sCasoidconsulta);
                            listaitems.SubItems.Add(obj.validareader("CasoNumero", "CasoId", respuestastring3).Text);
                            listaitems.SubItems.Add(obj.validareader("TipoSolicitudDescrip", "CasoId", respuestastring3).Text);
                            listaitems.SubItems.Add(obj.validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text);
                            listaitems.SubItems.Add(obj.validareader("CasoNumConcedida", "CasoId", respuestastring3).Text);
                            //listaitems.SubItems.Add(validareader("InteresadoNombre", "CasoId", respuestastring3).Text);
                            listaitems.SubItems.Add(obj.validareader("InteresadoNombre", "CasoId", respuestastring3).Text);//
                            listaitems.SubItems.Add(obj.validareader("CasoTituloespanol", "CasoId", respuestastring3).Text + "/ " + obj.validareader("CasoTituloingles", "CasoId", respuestastring3).Text);
                            listaitems.SubItems.Add(obj.validareader("cliente_Nombre", "CasoId", respuestastring3).Text);
                            listaitems.SubItems.Add(sPrioridades);
                            listaitems.SubItems.Add(obj.validareader("referencia", "CasoId", respuestastring3).Text);

                            dgRowCasos.Rows.Add(obj.validareader("PaisClave", "CasoId", respuestastring3).Text,
                                                sCasoidconsulta,
                                                obj.validareader("CasoNumero", "CasoId", respuestastring3).Text,
                                                obj.validareader("TipoSolicitudDescrip", "CasoId", respuestastring3).Text,
                                                obj.validareader("CasoNumeroExpedienteLargo", "CasoId", respuestastring3).Text,
                                                obj.validareader("CasoNumConcedida", "CasoId", respuestastring3).Text,
                                                obj.validareader("InteresadoNombre", "CasoId", respuestastring3).Text,
                                                obj.validareader("CasoTituloespanol", "CasoId", respuestastring3).Text + "/ " + obj.validareader("CasoTituloingles", "CasoId", respuestastring3).Text,
                                                obj.validareader("cliente_Nombre", "CasoId", respuestastring3).Text,
                                                sPrioridades,
                                                obj.validareader("referencia", "CasoId", respuestastring3).Text);
                            if (residuo == 0)
                            {
                                listaitems.BackColor = Color.LightGray;
                            }
                            else
                            {
                                listaitems.BackColor = Color.Azure;
                            }
                            //listViewCasos.Items.Add(listaitems);
                            //this.listViewCasos.FullRowSelect = true;
                            rowcolor++;
                        }
                        respuestastring3.Close();
                        con.Cerrarconexion();
                        textBox10.Text = rowcolor + "";

                    }
                    catch (Exception E)
                    {

                        textBox10.Text = rowcolor + "";
                        MessageBox.Show("Se encontraron más de " + rowcolor + " la busqueda debe ser más especifica.");

                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un Tipo de caso");
                }
            }
            catch (Exception ex) { 
            }
            
        }

        private void consultaulticasos_ResizeEnd(object sender, EventArgs e)
        {
            
        }

        private void consultaulticasos_Resize(object sender, EventArgs e)
        {
            //MessageBox.Show("redimencionando");
            dgRowCasos.Location = new Point(this.dgRowCasos.Location.X, 75);
            dgRowCasos.Size = new Size(dgRowCasos.Width, this.Height-120);
            

        }
    }
}
