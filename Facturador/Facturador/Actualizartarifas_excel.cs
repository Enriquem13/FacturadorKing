using LinqToExcel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.IO;
//using System.Linq;



namespace Facturador.Facturador
{
    public partial class Actualizartarifas_excel : Form
    {
        funcionesdicss obj_dicss = new funcionesdicss();
        public Actualizartarifas_excel()
        {
            InitializeComponent();
            //Consultamos el catálogo de clientes
            ComboboxItem tarifacero = new ComboboxItem();
            tarifacero.Text = "TARIFA BASE";
            tarifacero.Value = 0;
            comboBoxClientes.Items.Add(tarifacero);

            conect conect_clientes = new conect();
            String query2 = "select ClienteNombre, ClienteId from cliente order by cliente.ClienteNombre;";
            MySqlDataReader respuestastringclient = conect_clientes.getdatareader(query2);
            /*agregamos la tarifa Base con idcliente 0 tabla cliente*/
            while (respuestastringclient.Read())
            {
                comboBoxClientes.Items.Add(obj_dicss.validareader("ClienteNombre", "ClienteId", respuestastringclient));
            }
            respuestastringclient.Close();
            conect_clientes.Cerrarconexion();

            //Consultamos el catálogo de interesados
            conect conect_interesados = new conect();
            String query4 = "select InteresadoID, InteresadoNombre from interesado order by InteresadoNombre;";
            MySqlDataReader respuestastringointeresado = conect_interesados.getdatareader(query4);
            while (respuestastringointeresado.Read())
            {
                comboBoxInteresado.Items.Add(obj_dicss.validareader("InteresadoNombre", "InteresadoID", respuestastringointeresado));
            }
            respuestastringointeresado.Close();
            conect_interesados.Cerrarconexion();


            //Cargamos los grupos de casos
            conect conect_grupo = new conect();
            String sQuerygrupo = "select * from grupo;";
            MySqlDataReader respuesta_grupo = conect_grupo.getdatareader(sQuerygrupo);
            while (respuesta_grupo.Read())
            {
                cbGrupocaso.Items.Add(obj_dicss.validareader("GrupoDescripcion", "GrupoId", respuesta_grupo));
            }
            respuesta_grupo.Close();
            conect_grupo.Cerrarconexion();

            /*Llenamos el grid con la información existente*/

            muestratarifas();
            
            //dataGridView_relaciontarifas
            //dataGridView1.Rows.Clear();
            //dataGridView1.Rows.Add(, , ,);
        }

        public void muestratarifas() {
            //Consultamos el catálogo de relacion tarifas cliente interesado caso
            conect conect_tarifas = new conect();
            String query_tarifas = "select * FROM relacion_tarifasexcel_clientes;";
            MySqlDataReader respuestas_con_tarifas = conect_tarifas.getdatareader(query_tarifas);
            dataGridView_relaciontarifas.Rows.Clear();
            while (respuestas_con_tarifas.Read())
            {
                String sidNombre = obj_dicss.validareader("id_baseking", "idrelacion_tarifasexcel_clientes", respuestas_con_tarifas).Text;
                String sTablarelacion = obj_dicss.validareader("tabla_relacion", "idrelacion_tarifasexcel_clientes", respuestas_con_tarifas).Text;
                String sNombretarifado = getNombretarifado(sTablarelacion, sidNombre);
                /*
                   obj_dicss.validareader("id_baseking", "idrelacion_tarifasexcel_clientes", respuestas_con_tarifas).Text,*/
                dataGridView_relaciontarifas.Rows.Add(obj_dicss.validareader("idrelacion_tarifasexcel_clientes", "idrelacion_tarifasexcel_clientes", respuestas_con_tarifas).Text,
                sNombretarifado,
                obj_dicss.validareader("tabla_relacion", "idrelacion_tarifasexcel_clientes", respuestas_con_tarifas).Text,
                obj_dicss.validareader("titulo_excel_pesos", "idrelacion_tarifasexcel_clientes", respuestas_con_tarifas).Text,
                obj_dicss.validareader("titulo_excel_dolares", "idrelacion_tarifasexcel_clientes", respuestas_con_tarifas).Text,
                obj_dicss.validareader("titulo_excel_euros", "idrelacion_tarifasexcel_clientes", respuestas_con_tarifas).Text,
                obj_dicss.validareader("tabla_relacion", "idrelacion_tarifasexcel_clientes", respuestas_con_tarifas).Text//este no debe aparecer
                );
                //comboBoxClientes.Items.Add(obj_dicss.validareader("ClienteNombre", "ClienteId", respuestas_con_tarifas));
            }
            respuestas_con_tarifas.Close();
            conect_tarifas.Cerrarconexion();
        }

        public String getNombretarifado(String sTablarelacion, String sidNombre)
        {
            try {
                if (sidNombre == "0" && sTablarelacion=="cliente")
                {
                    return "TARIFA BASE";
                }
                String query_tarifado = "";
                switch (sTablarelacion)
                {
                    case "cliente":
                        {
                            query_tarifado = "select * FROM " + sTablarelacion + " where ClienteId = " + sidNombre;
                        } break;
                    case "interesado":
                        {
                            query_tarifado = "select * FROM " + sTablarelacion + " where InteresadoID = " + sidNombre;
                        } break;
                    case "caso_patente":
                        {
                            query_tarifado = "select * FROM " + sTablarelacion + " where casoid = " + sidNombre;
                        } break;
                }
                //Consultamos el catálogo de relacion tarifas cliente interesado caso
                conect conect_Nombretarifado = new conect();
                
                MySqlDataReader respuestas_con_Nombretarifado = conect_Nombretarifado.getdatareader(query_tarifado);
                respuestas_con_Nombretarifado.Read();

                String sNombretarifado = "";// obj_dicss.validareader("tabla_relacion", "idrelacion_tarifasexcel_clientes", respuestas_con_Nombretarifado).Text;
                switch (sTablarelacion)
                {
                    case "cliente":
                        {
                            sNombretarifado = obj_dicss.validareader("ClienteNombre", "ClienteNombre", respuestas_con_Nombretarifado).Text + " " + obj_dicss.validareader("ClienteApellidoPaterno", "ClienteApellidoPaterno", respuestas_con_Nombretarifado).Text + " " + obj_dicss.validareader("ClienteApellidoMaterno", "ClienteApellidoMaterno", respuestas_con_Nombretarifado).Text;
                        } break;
                    case "interesado":
                        {
                            sNombretarifado = obj_dicss.validareader("InteresadoNombre", "InteresadoNombre", respuestas_con_Nombretarifado).Text + " " + obj_dicss.validareader("InteresadoApPaterno", "InteresadoApPaterno", respuestas_con_Nombretarifado).Text + " " + obj_dicss.validareader("InteresadoApMaterno", "InteresadoApMaterno", respuestas_con_Nombretarifado).Text;
                        } break;
                    case "caso_patente":
                        {
                            sNombretarifado = "CasoNumero: " + obj_dicss.validareader("CasoNumero", "CasoNumero", respuestas_con_Nombretarifado).Text;
                        } break;
                }
                respuestas_con_Nombretarifado.Close();
                conect_Nombretarifado.Cerrarconexion();
                return sNombretarifado;
            }catch(Exception Ex){
                return ""+Ex.Message;
            }
            
        }
        List<ExcelTarifas> resultado;
        public String generainsert_por_cliente(List<ExcelTarifas> lista_clientetarifas){
            String sQueryunidos = "";
            int icount = 0;
            foreach (var tarifa in lista_clientetarifas)
            {
                //Meto las propiedades del dto recorrido en el usuario
                if (tarifa.id_concepto != "" && tarifa.id_concepto !="0")
                {
                    sQueryunidos += " INSERT INTO `tarifas_base_king`"+
                                        " (`tarifas_base_king_id`,"+
                                        " `id_concepto`,"+
                                        " `derechos pesos`,"+
                                        " `derechos dolares`,"+
                                        " `derechos euros`,"+
                                        " `honorarios pesos`,"+
                                        " `honorarios dolares`,"+
                                        " `honorarios euros`," +
                                        " `id_baseking`," +
                                        " `tabla_relacion`)" +
                                        " VALUES"+
                                        " ( '" + tarifa.tarifas_base_king_id + "' ," +
                                        " " + tarifa.id_concepto + ","+
                                        " '" + tarifa.derechos_pesos+"',"+
                                        " '" + tarifa.derechos_dolares+"',"+
                                        " '" + tarifa.derechos_euros+"',"+
                                        " '" + tarifa.honorarios_pesos + "',"+
                                        " '" + tarifa.honorarios_dolares + "'," +
                                        " '" + tarifa.honorarios_euros + "'," +
                                        " '" + tarifa.llave_cliente + "'," +
                                        " '" + tarifa.tabla_relacion + "'); ";
                }
                icount++;
            }
            String afectados = "";
            conect con = new conect();
            MySqlDataReader respuestas_rel = con.getdatareader(sQueryunidos);
            //afectados = afectados + respuestas_rel.RecordsAffected;
            //respuestas_rel.Close();
            con.Cerrarconexion();
            return "";
        }


        public String generainsert_por_cliente_conceptos(List<ExcelTarifas> lista_clientetarifas)
        {
            try {
                conect con_truncate = new conect();
                MySqlDataReader respuestas_rel_truncate = con_truncate.getdatareader("truncate table tarifa_categoria_conceptos; truncate table tarifa_conceptos_king;");
                respuestas_rel_truncate.Close();
                con_truncate.Cerrarconexion();
                String sQueryunidos = "";
                int icount = 0;
                int iIdcategoria = 0;
                String sQuerycategorias = "";
                foreach (var tarifa in lista_clientetarifas)
                {
                    if (tarifa.id_concepto!="")
                    {
                        //Meto las propiedades del dto recorrido en el usuario
                        String sidconcepto = tarifa.id_concepto;
                        if (tarifa.id_concepto == "0")//es una categoria nueva
                        {
                            iIdcategoria++;
                            sQuerycategorias += "INSERT INTO `tarifa_categoria_conceptos` " +
                                                " (`tarifa_categoria_conceptos_is`, " +
                                                " `Nombre_categoria`) " +
                                                " VALUES " +
                                                " ( " + iIdcategoria + ", " +
                                                " '" + tarifa.concepto + "' ); ";

                        }
                        else
                        {
                            sQueryunidos += " INSERT INTO `tarifa_conceptos_king`" +
                                            " (`tarifa_conceptos_kingid`," +
                                            " `id_concepto`," +
                                            " `concepto`," +
                                            " `id_categoria` )" +
                                            " VALUES" +
                                            " ( '" + tarifa.tarifas_base_king_id + "' ," +
                                            " " + tarifa.id_concepto + "," +
                                            " '" + tarifa.concepto + "'," +
                                            " '" + iIdcategoria + "');";
                        }
                    }

                }
                String afectados = "";
                conect con = new conect();
                MySqlDataReader respuestas_rel = con.getdatareader(sQuerycategorias);
                respuestas_rel.Close();
                //afectados = afectados + respuestas_rel.RecordsAffected;
                //respuestas_rel.Close();
                con.Cerrarconexion();
                conect con_2 = new conect();
                MySqlDataReader respuestas_rel_2 = con_2.getdatareader(sQueryunidos);
                respuestas_rel_2.Close();
                con_2.Cerrarconexion();
                return "";
            }catch(Exception ex){
                return "error"+ex;
            }
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            //cargamos el excel segun los titulos de las tarifas
            //Agregamos plugin para consumir el excel como en el robot demailking
            try
            {
                String pathDelFicheroExcel = "";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    /*Lo primero que debemos de hacer es consultar relacion_tarifasexcel_clientes para saber todos los clientes u leer sus tarifas corresponiendiente
                      de cada columna en el excel, una vez leida la procesamos y la insertamos en tarifas_base_king con elclienteid de la consulta
                    */
                    /*Borramos las tarifas para actualizar desde el excel*/
                    conect con_truncate = new conect();
                    String squerytruncate = "truncate table tarifas_base_king;";
                    MySqlDataReader respuestas_truncate = con_truncate.getdatareader(squerytruncate);
                    respuestas_truncate.Close();
                    con_truncate.Cerrarconexion();

                    conect con_truncate_conceptos = new conect();
                    String squerytruncate_conceptos = "truncate table tarifa_conceptos_king;";
                    MySqlDataReader respuestas_truncate_conceptos = con_truncate_conceptos.getdatareader(squerytruncate_conceptos);
                    respuestas_truncate_conceptos.Close();
                    con_truncate_conceptos.Cerrarconexion();
                    /*si hacemos el truncate primero, posteriormente debemos actualizar toda la tabla*/
                    resultado = new List<ExcelTarifas>();
                    conect con = new conect();
                    String query_relaciontarifas = "select * from relacion_tarifasexcel_clientes;";
                    MySqlDataReader respuestas_rel = con.getdatareader(query_relaciontarifas);
                    while (respuestas_rel.Read())
                    {
                        resultado = new List<ExcelTarifas>();
                        String textopesos = obj_dicss.validareader("titulo_excel_pesos", "idrelacion_tarifasexcel_clientes", respuestas_rel).Text;
                        String textodolares = obj_dicss.validareader("titulo_excel_dolares", "idrelacion_tarifasexcel_clientes", respuestas_rel).Text;
                        String textoeuros = obj_dicss.validareader("titulo_excel_euros", "idrelacion_tarifasexcel_clientes", respuestas_rel).Text;
                        String Clienteid = obj_dicss.validareader("id_baseking", "idrelacion_tarifasexcel_clientes", respuestas_rel).Text;
                        String stabla_relacion = obj_dicss.validareader("tabla_relacion", "idrelacion_tarifasexcel_clientes", respuestas_rel).Text;

                        /*pueden existir 6 tipos de convinaciones para leer el excel dependiendo los titulos del excel tarifa*/
                        /*uno cuando los 3 campos tienen un valor para actualizar pesos dolares y euros*/
                        if (textopesos != "" && textodolares != "" && textoeuros!="")
                        {
                            int counttotal = 0;
                            pathDelFicheroExcel = openFileDialog1.FileName;
                            var book = new ExcelQueryFactory(pathDelFicheroExcel);
                            resultado = (from row in book.Worksheet("base pats")//book.Worksheet("Hoja1")
                                             let item = new ExcelTarifas()
                                             {
                                                 tarifas_base_king_id = "",
                                                 id_concepto = row["id_concepto"].Value.ToString(),
                                                 concepto = row["concepto"].Value.ToString(),
                                                 derechos_pesos = row["deroechos pesos redondeados"].Value.ToString(),
                                                 derechos_dolares = row["Derechos USD"].Value.ToString(),
                                                 derechos_euros = row["Derechos Euros"].Value.ToString(),
                                                 honorarios_euros = row[textoeuros].Value.ToString(),
                                                 honorarios_dolares = row[textodolares].Value.ToString(),
                                                 honorarios_pesos = row[textopesos].Value.ToString(),
                                                 llave_cliente = Clienteid,
                                                 tabla_relacion = stabla_relacion,
                                             }
                                             select item).ToList();
                        }
                        /*Cuando tenemos pesos y dolares*/
                        if (textopesos != "" && textodolares != "" && textoeuros == "")
                        {
                            int counttotal = 0;
                            pathDelFicheroExcel = openFileDialog1.FileName;
                            var book = new ExcelQueryFactory(pathDelFicheroExcel);
                            resultado = (from row in book.Worksheet("base pats")//book.Worksheet("Hoja1")
                                             let item = new ExcelTarifas()
                                             {
                                                 tarifas_base_king_id = "",
                                                 id_concepto = row["id_concepto"].Value.ToString(),
                                                 concepto = row["concepto"].Value.ToString(),
                                                 derechos_pesos = row["deroechos pesos redondeados"].Value.ToString(),
                                                 derechos_dolares = row["Derechos USD"].Value.ToString(),
                                                 derechos_euros = row["Derechos Euros"].Value.ToString(),
                                                 honorarios_dolares = row[textodolares].Value.ToString(),
                                                 honorarios_pesos = row[textopesos].Value.ToString(),
                                                 llave_cliente = Clienteid,
                                                 tabla_relacion = stabla_relacion,
                                             }
                                             select item).ToList();
                        }
                        /*cuando tenemos pesos y euros*/
                        if (textopesos != "" && textodolares == "" && textoeuros != "")
                        {
                            int counttotal = 0;
                            pathDelFicheroExcel = openFileDialog1.FileName;
                            var book = new ExcelQueryFactory(pathDelFicheroExcel);
                            resultado = (from row in book.Worksheet("base pats")//book.Worksheet("Hoja1")
                                             let item = new ExcelTarifas()
                                             {
                                                 tarifas_base_king_id = "",
                                                 id_concepto = row["id_concepto"].Value.ToString(),
                                                 concepto = row["concepto"].Value.ToString(),
                                                 derechos_pesos = row["deroechos pesos redondeados"].Value.ToString(),
                                                 derechos_dolares = row["Derechos USD"].Value.ToString(),
                                                 derechos_euros = row["Derechos Euros"].Value.ToString(),
                                                 honorarios_euros = row[textoeuros].Value.ToString(),
                                                 honorarios_pesos = row[textopesos].Value.ToString(),
                                                 llave_cliente = Clienteid,
                                                 tabla_relacion = stabla_relacion,
                                             }
                                             select item).ToList();
                        }
                        /*cuando tenemos dolares y euros*/
                        if (textopesos == "" && textodolares != "" && textoeuros != "")
                        {
                            int counttotal = 0;
                            pathDelFicheroExcel = openFileDialog1.FileName;
                            var book = new ExcelQueryFactory(pathDelFicheroExcel);
                            resultado = (from row in book.Worksheet("base pats")//book.Worksheet("Hoja1")
                                             let item = new ExcelTarifas()
                                             {
                                                 tarifas_base_king_id = "",
                                                 id_concepto = row["id_concepto"].Value.ToString(),
                                                 concepto = row["concepto"].Value.ToString(),
                                                 derechos_pesos = row["deroechos pesos redondeados"].Value.ToString(),
                                                 derechos_dolares = row["Derechos USD"].Value.ToString(),
                                                 derechos_euros = row["Derechos Euros"].Value.ToString(),
                                                 honorarios_dolares = row[textodolares].Value.ToString(),
                                                 honorarios_euros = row[textoeuros].Value.ToString(),
                                                 llave_cliente = Clienteid,
                                                 tabla_relacion = stabla_relacion,
                                             }
                                             select item).ToList();
                        }
                        /*cuando tenemos sólo euros*/
                        if (textopesos == "" && textodolares == "" && textoeuros != "")
                        {
                            int counttotal = 0;
                            pathDelFicheroExcel = openFileDialog1.FileName;
                            var book = new ExcelQueryFactory(pathDelFicheroExcel);
                            resultado = (from row in book.Worksheet("base pats")//book.Worksheet("Hoja1")
                                             let item = new ExcelTarifas()
                                             {
                                                 tarifas_base_king_id = "",
                                                 id_concepto = row["id_concepto"].Value.ToString(),
                                                 concepto = row["concepto"].Value.ToString(),
                                                 derechos_pesos = row["deroechos pesos redondeados"].Value.ToString(),
                                                 derechos_dolares = row["Derechos USD"].Value.ToString(),
                                                 derechos_euros = row["Derechos Euros"].Value.ToString(),
                                                 honorarios_euros = row[textoeuros].Value.ToString(),
                                                 llave_cliente = Clienteid,
                                                 tabla_relacion = stabla_relacion,
                                             }
                                             select item).ToList();
                        }
                        /*cuando tenemos sólo dolares*/
                        if (textopesos == "" && textodolares != "" && textoeuros == "")
                        {
                            int counttotal = 0;
                            pathDelFicheroExcel = openFileDialog1.FileName;
                            var book = new ExcelQueryFactory(pathDelFicheroExcel);
                            resultado = (from row in book.Worksheet("base pats")//book.Worksheet("Hoja1")
                                             let item = new ExcelTarifas()
                                             {
                                                 tarifas_base_king_id = "",
                                                 id_concepto = row["id_concepto"].Value.ToString(),
                                                 concepto = row["concepto"].Value.ToString(),
                                                 derechos_pesos = row["deroechos pesos redondeados"].Value.ToString(),
                                                 derechos_dolares = row["Derechos USD"].Value.ToString(),
                                                 derechos_euros = row["Derechos Euros"].Value.ToString(),
                                                 honorarios_dolares = row[textodolares].Value.ToString(),
                                                 llave_cliente = Clienteid,
                                                 tabla_relacion = stabla_relacion,
                                             }
                                             select item).ToList();
                        }
                        /*cuando tenemos sólo pesos*/
                        if (textopesos != "" && textodolares == "" && textoeuros == "")/*SI SE TIENEN SÓLO PESOS CON EL TIPO DE CAMBIO PODRIAMOS CALCULAR DOLAR Y EURO*/
                        {
                            int counttotal = 0;
                            pathDelFicheroExcel = openFileDialog1.FileName;
                            var book = new ExcelQueryFactory(pathDelFicheroExcel);
                            resultado = (from row in book.Worksheet("base pats")//book.Worksheet("Hoja1")
                                             let item = new ExcelTarifas()
                                             {
                                                 tarifas_base_king_id = "",
                                                 id_concepto = row["id_concepto"].Value.ToString(),
                                                 concepto = row["concepto"].Value.ToString(),
                                                 derechos_pesos = row["deroechos pesos redondeados"].Value.ToString(),
                                                 derechos_dolares = row["Derechos USD"].Value.ToString(),
                                                 derechos_euros = row["Derechos Euros"].Value.ToString(),
                                                 honorarios_pesos = row[textopesos].Value.ToString(),
                                                 llave_cliente = Clienteid,
                                                 tabla_relacion = stabla_relacion,
                                             }
                                             select item).ToList();
                        }

                        /*si están vacios no leer nada, no se puede si no hay una referencia*/
                        if (textopesos == "" && textodolares == "" && textoeuros == "")
                        {
                            MessageBox.Show("Debe agregar una referencia para relacionar con el archivo excel y el cliente.");
                        }
                        String resultadostring = generainsert_por_cliente(resultado);
                        //String resultadostring_conceptos = generainsert_por_cliente_conceptos(resultado);
                    }
                    String resultadostring_conceptos = generainsert_por_cliente_conceptos(resultado);
                    MessageBox.Show("Tarifas actualizadas");
                    respuestas_rel.Close();
                    con.Cerrarconexion();

                    
                    //int counttotal = 0;
                    //pathDelFicheroExcel = openFileDialog1.FileName;
                    //var book = new ExcelQueryFactory(pathDelFicheroExcel);
                    //var resultado = (from row in book.Worksheet("base pats")//book.Worksheet("Hoja1")
                    //                 let item = new ExcelTarifas()
                    //                 {
                    //                     tarifas_base_king_id = "",
                    //                     id_concepto = row["id_concepto"].Value.ToString(),
                    //                     concepto = row["concepto"].Value.ToString(),
                    //                     derechos_pesos = row["deroechos pesos redondeados"].Value.ToString(),
                    //                     derechos_dolares = row["Derechos USD"].Value.ToString(),
                    //                     derechos_euros = row["Derechos Euros"].Value.ToString(),
                    //                     honorarios_euros = row["Tarifa_Honorarios_Murgi Euros"].Value.ToString(),
                    //                     //honorarios_dolares = row[""].Value.ToString()
                    //                     /*concepto = row["pais"].Value.ToString(),
                    //                     derechos = row["idioma"].Value.ToString(),
                    //                     pesos = row["pagina web"].Value.ToString()*/

                    //                     /*
                    //                      public class ExcelTarifas
                    //                    {
                    //                        public string tarifas_base_king_id { get; set; }
                    //                        public string id_concepto { get; set; }
                    //                        public string concepto { get; set; }
                    //                        public string derechos_pesos { get; set; }
                    //                        public string derechos_dolares { get; set; }
                    //                        public string derechos_euros { get; set; }
                    //                        public string honorarios_pesos { get; set; }
                    //                        public string honorarios_dolares { get; set; }
                    //                        public string honorarios_euros { get; set; }
                    //                        public string llave_cliente { get; set; }
                    //                    }*/
                    //                 }
                    //                 select item).ToList();
                    //Action act = () =>
                    //{
                    //    var book = new ExcelQueryFactory(pathDelFicheroExcel);
                    //    var resultado = (from row in book.Worksheet("Hoja1")
                    //                     let item = new ExcelTarifas
                    //                     {
                    //                         tarifas_base_king_id = row["Empresa"].Value.ToString(),
                    //                         id_concepto = row["email"].Value.ToString(),
                    //                         concepto = row["pais"].Value.ToString(),
                    //                         derechos = row["idioma"].Value.ToString(),
                    //                         pesos = row["pagina web"].Value.ToString()
                    //                     }
                    //                     select item).ToList();

                    //    resultado_conultaexcel = resultado;
                    //    book.Dispose();
                    //    counttotal = resultado.Count;
                    //};
                    //using (Waitforsendmail obj = new Waitforsendmail(act))
                    //{
                    //    obj.ShowDialog();
                    //}
                    //tbNumcontactos.Text = counttotal + "";
                }
            }
            catch (Exception Ex)
            {
                new filelog("Cargar excel: ", "verifique la versión de 32 o 64 en office");
                MessageBox.Show("Verifique la version de instalación");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*Prueba para consumir pagina*/
            //var url = "https://dicss.com.mx";
            //var textFromFile = (new WebClient()).DownloadString(url);
            //txtContenidoWeb.Text = textFromFile;
            //MessageBox.Show(textFromFile);
            /*Traemos los valores del combobox de los clientes*/
            String sValorrelacion = "";
            String sTablarelacion = "";
            String sNombretarifado = "";
            try {

                if (rb_cliente.Checked)
                {//Seleccionaron al cliente
                    if (comboBoxClientes.SelectedItem != null)
                    {
                        sNombretarifado = (comboBoxClientes.SelectedItem as ComboboxItem).Text;
                        sValorrelacion = (comboBoxClientes.SelectedItem as ComboboxItem).Value.ToString();
                        sTablarelacion = "cliente";
                    }else {
                        MessageBox.Show("Debe seleccionar un cliente para agregar.");
                        return;
                    }
                }

                if (rb_titular.Checked){//Seleccionaron al titular
                    if (comboBoxInteresado.SelectedItem != null)
                    {
                        sNombretarifado = (comboBoxInteresado.SelectedItem as ComboboxItem).Text;
                        sValorrelacion = (comboBoxInteresado.SelectedItem as ComboboxItem).Value.ToString();
                        sTablarelacion = "interesado";
                    }else{
                        MessageBox.Show("Debe seleccionar un Titular para agregar.");
                        return;
                    }
                    
                }

                if(rb_caso.Checked){//Seleccionaon al caso
                    if (tbCasoid.Text != "")
                    {
                        sNombretarifado = tbCasoid.Text;
                        sValorrelacion = tbCasoid.Text;
                        sTablarelacion = "caso_patente";
                    }else{
                        MessageBox.Show("Debe seleccionar un Caso para agregar.");
                        return;
                    }
                    //buscamos el caso pendiente
                }
                /*Teniendo el dato con el que se va a relacionar los títulos, vamos a validar que por lo menos exista un título para 
                 Relacinarlo con el excel y cargar sus tarifas*/
                if (tb_pesos.Text == "" && tb_dolares.Text == "" && tb_euros.Text==""){
                    MessageBox.Show("Debe llenar por lo menos un título para poder relacionar la tarifa del excel.");
                    return;
                }

                if (sValorrelacion==""){
                    MessageBox.Show("Debe seleccionar por lo menos una opción para relacionar.");
                    return;
                }
                /*generamos el query e insertamos la relacion*/
                conect con_count_tarifa = new conect();
                String sQuerytarifascount = " INSERT INTO `relacion_tarifasexcel_clientes`" +
                                            " (`idrelacion_tarifasexcel_clientes`," +
                                            " `titulo_excel_pesos`," +
                                            " `titulo_excel_dolares`," +
                                            " `titulo_excel_euros`," +
                                            " `tabla_relacion`," +
                                            " `id_baseking`," +
                                            " `fecha_registro`)" +
                                            " VALUES" +
                                            " (NULL," +
                                            " '" + tb_pesos.Text + "'," +
                                            " '" + tb_dolares.Text + "'," +
                                            " '" + tb_euros.Text + "'," +
                                            " '" + sTablarelacion + "'," +
                                            " '" + sValorrelacion + "'," +
                                            " 'now()');";
                MySqlDataReader resp_get_tarifadatoscount = con_count_tarifa.getdatareader(sQuerytarifascount);
                resp_get_tarifadatoscount.Read();
                if (resp_get_tarifadatoscount.RecordsAffected == 1)
                {//se inserto el registro
                    String sCounttarifa = "";
                    conect con_count_insertada = new conect();
                    MySqlDataReader resp_get_tarifaonsertada = con_count_insertada.getdatareader("select * from relacion_tarifasexcel_clientes order by idrelacion_tarifasexcel_clientes desc limit 1;");
                    resp_get_tarifaonsertada.Read();
                    sCounttarifa = obj_dicss.validareader("idrelacion_tarifasexcel_clientes", "idrelacion_tarifasexcel_clientes", resp_get_tarifaonsertada).Text;
                    resp_get_tarifaonsertada.Close();
                    con_count_insertada.Cerrarconexion();

                    dataGridView_relaciontarifas.Rows.Add(sCounttarifa, sNombretarifado, sTablarelacion, tb_pesos.Text, tb_dolares.Text, tb_euros.Text);
                }
                resp_get_tarifadatoscount.Close();
                con_count_tarifa.Cerrarconexion();
            }catch(Exception E){
                
            }
            
        }

        private void button2_Click(object sender, EventArgs e)/*buscamos el caso*/
        {

        }

        private void btn_cargarconceptos_Click(object sender, EventArgs e)
        {
            try
            {
                String pathDelFicheroExcel = "";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    int counttotal = 0;
                    pathDelFicheroExcel = openFileDialog1.FileName;
                    var book = new ExcelQueryFactory(pathDelFicheroExcel);
                    resultado = (from row in book.Worksheet("base pats")//book.Worksheet("Hoja1")
                                 let item = new ExcelTarifas()
                                 {
                                     tarifas_base_king_id = "",
                                     id_concepto = row["id_concepto"].Value.ToString(),
                                     concepto = row["concepto"].Value.ToString(),
                                 }
                                 select item).ToList();
                    
                    generainsert_por_cliente_conceptos(resultado);
                }
            }
            catch (Exception Ex)
            {
                new filelog("Cargar excel: ", "verifique la versión de 32 o 64 en office");
                MessageBox.Show("Verifique la version de instalación");
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (cbGrupocaso.SelectedItem != null)
            {

                int isGrop = Int32.Parse((cbGrupocaso.SelectedItem as ComboboxItem).Value.ToString());
                buscaCaso bCaso = new buscaCaso(isGrop);
                if (bCaso.ShowDialog() == DialogResult.OK)
                {
                    String sQueryconulta = "";
                    try
                    {
                        tbCasoid.Text = bCaso.sCasoid;
                    }
                    catch (Exception ex)
                    {

                    }

                }
            }
            else {
                MessageBox.Show("Debe seleccionar un grupo");
            }
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            try {
                String sids = "";
                int x=0;
                if (dataGridView_relaciontarifas.SelectedRows.Count > 0)
                {
                    for (x = 0; x < dataGridView_relaciontarifas.SelectedRows.Count; x++)
                    {
                        sids += dataGridView_relaciontarifas.SelectedRows[x].Cells[0].Value.ToString() + ",";
                    }
                    sids = sids.Substring(0, sids.Length - 1);

                    var confirmResult = MessageBox.Show("Seguro que desea eliminar " + x + " registro(s) seleccionado(s)",
                                         "Confirmación de eliminar registros!!",
                                         MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        String queryeliminarrelacion = "Delete from relacion_tarifasexcel_clientes where idrelacion_tarifasexcel_clientes in(" + sids + ");";
                        conect con = new conect();
                        MySqlDataReader respuestas_rel = con.getdatareader(queryeliminarrelacion);
                        String afectados = "" + respuestas_rel.RecordsAffected;
                        MessageBox.Show(afectados + " Registro(s) afectado(s)");
                        if (respuestas_rel.RecordsAffected>0)
                        {
                            muestratarifas();
                        }
                        respuestas_rel.Close();
                        con.Cerrarconexion();
                    }
                }
                else {
                    MessageBox.Show("Debe selecionar un registro para poder eliminar.");
                }
            }catch(Exception Ex){

            }
        }

        //public void insertconceptosrtarifas(List<ExcelTarifas> ecxelconceptos){
        //    try {
        //        String sQueryinsert = "";
        //        String sQueryunidos = "";
        //        int icount = 1;
        //        int icountcat = 2;
        //        foreach (var tarifa in ecxelconceptos)
        //        {
        //            //Meto las propiedades del dto recorrido en el usuario
        //            if (icount > 0)
        //            {
        //                sQueryinsert += " INSERT INTO `tarifa_categoria_conceptos` " +
        //                                " (`tarifa_categoria_conceptos_is`, " +
        //                                " `Nombre_categoria`) " +
        //                                " VALUES " +
        //                                " (sfrwd, " +
        //                                " sfrwd); ";

        //                sQueryunidos += " INSERT INTO `tarifa_conceptos_king`" +
        //                                    " (`tarifa_conceptos_kingid`," +
        //                                    " `id_concepto`," +
        //                                    " `concepto`," +
        //                                    " `id_categoria`" +
        //                                    " )" +
        //                                    " VALUES" +
        //                                    " ( '" + icount + "' ," +
        //                                    " " + tarifa.id_concepto + "," +
        //                                    " '" + tarifa.derechos_pesos + "'," +
        //                                    " '" + tarifa.derechos_dolares + "'," +
        //                                    " '" + tarifa.derechos_euros + "'," +
        //                                    " '" + tarifa.honorarios_pesos + "'," +
        //                                    " '" + tarifa.honorarios_dolares + "'," +
        //                                    " '" + tarifa.honorarios_euros + "'," +
        //                                    " " + tarifa.llave_cliente + "); ";
        //            }
        //            icount++;
        //        }
        //        String afectados = "";
        //        conect con = new conect();
        //        MySqlDataReader respuestas_rel = con.getdatareader(sQueryunidos);
        //        //afectados = afectados + respuestas_rel.RecordsAffected;
        //        //respuestas_rel.Close();
        //        con.Cerrarconexion();
        //    }catch(Exception Ex){
                
        //    }
        //}
    }
    
    
    public class ExcelTarifas
    {
        public string tarifas_base_king_id { get; set; }
        public string id_concepto { get; set; }
        public string concepto { get; set; }
        public string derechos_pesos { get; set; }
        public string derechos_dolares { get; set; }
        public string derechos_euros { get; set; }
        public string honorarios_pesos { get; set; }
        public string honorarios_dolares { get; set; }
        public string honorarios_euros { get; set; }
        public string llave_cliente { get; set; }
        public string tabla_relacion { get; set; }
        public ExcelTarifas()
        {//constructor
            tarifas_base_king_id = "";
            id_concepto = "";
            concepto = "";
            derechos_pesos = "";
            derechos_dolares = "";
            derechos_euros = "";
            honorarios_pesos = "";
            honorarios_dolares = "";
            honorarios_euros = "";
            llave_cliente = "";
            tabla_relacion = "";
        }
    }

}
