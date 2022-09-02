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

namespace Facturador
{
    public partial class Fusuarios : Form
    {
        public Form1 oFormlogin;
        public captura capFormcap;
        public String sIdusuario_g="";
        public Fusuarios(Form1 form, captura Formcap)
        {
            try
            {
                oFormlogin = form;
                capFormcap = Formcap;
                InitializeComponent();
                btGuardar.Enabled = false;
                //documentosimpi
                actualizatabla();

                //cargamos combobox de sexo
                //ComboboxItem cItemresult = new ComboboxItem();
                //cItemresult.Text = "Hombre";
                //cItemresult.Value = 1;
                //ComboboxItem cItemresult_2 = new ComboboxItem();
                //cItemresult_2.Text = "Mujer";
                //cItemresult_2.Value = 2;

                //CB_sexousuario.Items.Add(cItemresult);
                //CB_sexousuario.Items.Add(cItemresult_2);

                ComboboxItem cItemresult_act = new ComboboxItem();
                cItemresult_act.Text = "Activo";
                cItemresult_act.Value = 1;
                ComboboxItem cItemresult_inact = new ComboboxItem();
                cItemresult_inact.Text = "Inactivo";
                cItemresult_inact.Value = 2;

                cbActivo.Items.Add(cItemresult_act);
                cbActivo.Items.Add(cItemresult_inact);

                //Cargamos los tiposdeusuarios Perfil
                conect con = new conect();
                String sQuerytipoSol = "select distinct(PerfilNombre), PerfilId from Perfil;";//gtipode grupo
                MySqlDataReader respuestastringtoiposl = con.getdatareader(sQuerytipoSol);
                while (respuestastringtoiposl.Read())
                {
                    cbPerfil.Items.Add(validareader("PerfilNombre", "PerfilId", respuestastringtoiposl));
                }
                respuestastringtoiposl.Close();
                con.Cerrarconexion();
            }catch(Exception E){
                String ruta_log = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\casosking\\";
                if (!Directory.Exists(ruta_log))
                {
                    System.IO.Directory.CreateDirectory(ruta_log);
                }
                String fechalog = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                StringBuilder sb_log = new StringBuilder();
                sb_log.Append(fechalog + ":Usrid:" + oFormlogin.sId + " Error:" + E + "\n");
                System.IO.File.AppendAllText(ruta_log + "sistema_casosking.log", sb_log.ToString());
                sb_log.Clear();
            }
        }
        public void actualizatabla() {
            String squeryadocumentos = "";
            try { 
                lvUsuarios.Items.Clear();
                String sIdusuario;
                conect con2 = new conect();
                squeryadocumentos = "select Get_Perfil(UsuarioId) as perfildesc," +
                                           "UsuarioId, " +
                                           "UsuarioIndActivo, " +
                                           "UsuarioNombre, " +
                                           "UsuarioPaterno, " +
                                           "UsuarioMaterno, " +
                                           "UsuarioClave, " +
                                           "UsuarioPassword, " +
                                           "UsuarioSexo, " +
                                           "UsuarioEmail, " +
                                           "UsuarioClaveEInvoice, " +
                                           "UsuarioArea, " +
                                           "UsuarioRFC, " +
                                           "UsuarioIndAdmin " +
                                           " from usuario";
                MySqlDataReader resp_docimpi = con2.getdatareader(squeryadocumentos);
                int rowcolor = 0;
                while (resp_docimpi.Read())
                {
                    //lvdocumentosimpi 
                    sIdusuario = validareader("UsuarioId", "UsuarioId", resp_docimpi).Text;
                    String sActivo = validareader("UsuarioIndActivo", "UsuarioId", resp_docimpi).Text;
                    if (sActivo == "1")
                    {
                        sActivo = "Activo";
                    }
                    else
                    {
                        sActivo = "Inactivo";
                    }
                    ListViewItem items = new ListViewItem(sIdusuario);
                    items.SubItems.Add(validareader("UsuarioNombre", "UsuarioId", resp_docimpi).Text);
                    items.SubItems.Add(validareader("UsuarioPaterno", "UsuarioId", resp_docimpi).Text);
                    items.SubItems.Add(validareader("UsuarioMaterno", "UsuarioId", resp_docimpi).Text);
                    items.SubItems.Add(sActivo);
                    items.SubItems.Add(validareader("UsuarioClave", "UsuarioId", resp_docimpi).Text);
                    items.SubItems.Add(validareader("UsuarioPassword", "UsuarioId", resp_docimpi).Text);
                    //items.SubItems.Add(validareader("UsuarioSexo", "UsuarioId", resp_docimpi).Text);

                    items.SubItems.Add(validareader("UsuarioEmail", "UsuarioId", resp_docimpi).Text);
                    items.SubItems.Add(validareader("UsuarioClaveEInvoice", "UsuarioId", resp_docimpi).Text);

                    items.SubItems.Add(validareader("UsuarioArea", "UsuarioId", resp_docimpi).Text);
                    items.SubItems.Add(validareader("UsuarioRFC", "UsuarioId", resp_docimpi).Text);
                    items.SubItems.Add(validareader("perfildesc", "UsuarioId", resp_docimpi).Text);
                    int residuo = rowcolor % 2;
                    if (residuo == 0)
                    {
                        items.BackColor = Color.LightGray;
                    }
                    else
                    {
                        items.BackColor = Color.Azure;
                    }
                    lvUsuarios.Items.Add(items);
                    lvUsuarios.FullRowSelect = true;
                    rowcolor++;
                }
                resp_docimpi.Close();
                con2.Cerrarconexion();
            }catch(Exception E){
                String ruta_log = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\casosking\\";
                if (!Directory.Exists(ruta_log))
                {
                    System.IO.Directory.CreateDirectory(ruta_log);
                }
                String fechalog = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                StringBuilder sb_log = new StringBuilder();
                sb_log.Append(fechalog + ":Usrid:" + oFormlogin.sId + " Error:" + E + " Query:" + squeryadocumentos + "\n");
                System.IO.File.AppendAllText(ruta_log + "sistema_casosking.log", sb_log.ToString());
                sb_log.Clear();
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

        private void BT_agregar_Click(object sender, EventArgs e)
        {
            if(!TB_apellidomusuarios.Text.Equals("") && !TB_apellidopusuario.Text.Equals("") && !TB_areausuario.Text.Equals("") && !TB_contraseñausuario.Text.Equals("")
                && !TB_correousuario.Text.Equals("") && !TB_nombre_usuario.Text.Equals("") && !TB_rfcusuario.Text.Equals("") && !TB_usuariousuario.Text.Equals("") && cbActivo.SelectedItem != null && cbPerfil.SelectedItem != null)
            {

                   try
                   {
                       //conect coninsert = new conect();
                       DateTime dateTime = DateTime.UtcNow.Date;
                       String sFechacambio = dateTime.ToString("yyyy/MM/dd hh:mm:sss");
                       conect con1 = new conect();
                       String queryinsert = "INSERT INTO `usuario` "+
                           " (`UsuarioId`, "+
                           "`UsuarioNombre`, "+
                           "`UsuarioPaterno`, "+
                           "`UsuarioMaterno`, "+
                           " `UsuarioName`, "+
                           "`UsuarioFENombre`, "+
                           "`UsuarioClave`, "+
                           "`UsuarioPassword`, "+
                           "`UsuarioEmail`, "+
                           "`UsuarioIndActivo`,"+
                           " `UsuarioFechaCambio`, "+
                           "`UsuarioSexo`, "+
                           "`UsuarioArea`, "+
                           "`UsuarioClaveEInvoice`, "+
                           "`UsuarioRFC`, "+
                           "`UsuarioCodigo`,"+
                           " `UsuarioImagenFirma`," +
                           "`UsuarioTitulo`, "+
                           "`UsuarioCargoEspanol`, "+
                           "`UsuarioCargoIngles`, "+
                           "`UsuarioNombreFirma`)"
                           + "VALUES (NULL,'" 
                           + TB_nombre_usuario.Text + "', '" 
                           +TB_apellidopusuario.Text +"', '" 
                           +TB_apellidomusuarios.Text + "', '" 
                           + TB_usuariousuario.Text + "',"+
                           " NULL,'" 
                           + TB_usuariousuario.Text + "', '" 
                           + TB_contraseñausuario.Text + "', '" 
                           + TB_correousuario.Text + "',"+
                           " "+(cbActivo.SelectedItem as ComboboxItem).Value+", "+
                           " '" + sFechacambio + "'," +
                           "'', "+
                           "'" 
                           + TB_areausuario.Text + "'," 
                           +" '"+tbCorreocontrasena.Text+"','" 
                           + TB_rfcusuario.Text + "', " +
                           " NULL, "+
                           "NULL, "+
                           "NULL, "+
                           "NULL, "+
                           "NULL, "+
                           "NULL);";
                       MySqlDataReader respuestastringinsert = con1.getdatareader(queryinsert);
                       if (respuestastringinsert == null)
                       {
                           MessageBox.Show("Error al intentar insertar el usuario, verifique los campos.");
                       }
                       else
                       {
                           respuestastringinsert.Close();
                           con1.Cerrarconexion();

                           conect con2 = new conect();
                           String getidusuario = "select * from usuario order by UsuarioId desc limit 1";
                           MySqlDataReader respuestasidusuario = con2.getdatareader(getidusuario);
                           respuestasidusuario.Read();
                           String sIdusuario = validareader("UsuarioId", "UsuarioId", respuestasidusuario).Text;
                           respuestasidusuario.Close();
                           con2.Cerrarconexion();


                           conect con3 = new conect();
                           String insertperfilusuario = "INSERT INTO `perfilusuario`(`PerfilId`,`UsuarioId`)VALUES('" + (cbPerfil.SelectedItem as ComboboxItem).Value + "', '" + sIdusuario + "');";
                           MySqlDataReader respuestainsertperfil = con3.getdatareader(insertperfilusuario);
                           if (respuestainsertperfil!=null)
                           {
                               sIdusuario_g = "";
                               TB_nombre_usuario.Text = "";
                               TB_apellidopusuario.Text = "";
                               TB_apellidomusuarios.Text = "";
                               TB_usuariousuario.Text = "";
                               TB_areausuario.Text = "";
                               TB_correousuario.Text = "";
                               TB_contraseñausuario.Text = "";
                               TB_rfcusuario.Text = "";
                               tbCorreocontrasena.Text = "";
                               cbActivo.Text = "";
                               cbPerfil.Text = "";
                               actualizatabla();
                               MessageBox.Show("Usuario agregado correctamente.");
                           }else{
                               MessageBox.Show("Húbo un error al insertar el perfil, modifique el perfil de éste usuario agregado.");
                           }
                           respuestainsertperfil.Close();
                           con3.Cerrarconexion();
                           // limpiarcasillas();
                       }
                   }
                   catch (Exception E)
                   {
                       //escribimos en log
                       Console.WriteLine("{0} Exception caught.", E);
                       MessageBox.Show("Error al insertar Usuario Nuevo excepción: " + E);
                   }
            }else{
                MessageBox.Show("Debe llenar todos los campos");
            }
        }

        private void BT_modificar_Click(object sender, EventArgs e)
        {
            try {
                String Usuarioid = lvUsuarios.SelectedItems[0].SubItems[0].Text;//id
                String sNombre = lvUsuarios.SelectedItems[0].SubItems[1].Text;//Nombre
                String sPaterno = lvUsuarios.SelectedItems[0].SubItems[2].Text;//Paterno
                String sMaterno = lvUsuarios.SelectedItems[0].SubItems[3].Text;//Materno
                String sActivo = lvUsuarios.SelectedItems[0].SubItems[4].Text;//Activo
                String sUsuario = lvUsuarios.SelectedItems[0].SubItems[5].Text;//Usuario
                String sContraseña = lvUsuarios.SelectedItems[0].SubItems[6].Text;//Contraseña
                String sCorreo = lvUsuarios.SelectedItems[0].SubItems[7].Text;//Correo
                String sContrasenacorreo = lvUsuarios.SelectedItems[0].SubItems[8].Text;//Contrasenacorreo
                String sArea = lvUsuarios.SelectedItems[0].SubItems[9].Text;//Area
                String sRFC = lvUsuarios.SelectedItems[0].SubItems[10].Text;//RFC
                String sPerfil = lvUsuarios.SelectedItems[0].SubItems[11].Text;//Perfil
                sIdusuario_g = Usuarioid;
                TB_nombre_usuario.Text = sNombre;
                TB_apellidopusuario.Text = sPaterno;
                TB_apellidomusuarios.Text = sMaterno;
                TB_usuariousuario.Text = sUsuario;
                TB_areausuario.Text = sArea;
                TB_correousuario.Text = sCorreo;
                TB_contraseñausuario.Text = sContraseña;
                TB_rfcusuario.Text = sRFC;
                tbCorreocontrasena.Text = sContrasenacorreo;
                cbActivo.Text = sActivo;
                cbPerfil.Text = sPerfil;
                BT_agregar.Enabled = false;
                btGuardar.Enabled = true;

            }catch(Exception E){
                MessageBox.Show("Error al intentar cargar el usuario seleccionado.");
            }
        }

        private void BT_eliminar_Click(object sender, EventArgs e)
        {
            if (lvUsuarios.SelectedItems.Count > 0)
            {
                String Usuarioid = lvUsuarios.SelectedItems[0].SubItems[0].Text;//id
                String sNombre = lvUsuarios.SelectedItems[0].SubItems[1].Text;//Nombre
                if (Usuarioid != "")
                {
                    var confirmResult = MessageBox.Show("¿Seguro que desea borrar el usuario " + sNombre + " con id: " + Usuarioid + " ?.", "Eliminar Usuario", MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {

                        String sDelete = " delete from usuario where UsuarioId =  '" + Usuarioid + "';";
                        conect con6 = new conect();
                        MySqlDataReader respuesta_delete = con6.getdatareader(sDelete);
                        if (respuesta_delete == null)
                        {
                            MessageBox.Show("No se puede eliminar al usuario, puede existir actividad en el sistema");
                        }
                        else
                        {
                            respuesta_delete.Close();
                            con6.Cerrarconexion();
                            MessageBox.Show("Usuario borrado correctamente");
                            actualizatabla();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Error al seleccionar al usuario");
                }
            }
            else {
                MessageBox.Show("Debe seleccionar un usuario para poder eliminarlo.");
            }
        }

        private void BT_menusuarios_Click(object sender, EventArgs e)
        {
            capFormcap.Show();
            this.Close();
        }

        private void BT_saliruusarios_Click(object sender, EventArgs e)
        {
            capFormcap.Close();
            oFormlogin.Close();
            this.Close();
        }

        private void btGuardar_Click(object sender, EventArgs e)
        {
            Boolean bCambios = false;
            if (sIdusuario_g != "")
            {
                DateTime dateTime = DateTime.UtcNow.Date;
                String sFechacambio = dateTime.ToString("yyyy/MM/dd hh:mm:sss");
                String sNombre = TB_nombre_usuario.Text;
                String sPaterno = TB_apellidopusuario.Text;
                String sMaterno = TB_apellidomusuarios.Text;
                String sUsuario = TB_usuariousuario.Text;
                String sArea = TB_areausuario.Text;
                String sCorreo = TB_correousuario.Text;
                String sContraseña = TB_contraseñausuario.Text;
                String sRFC = TB_rfcusuario.Text;
                String sContrasenacorreo = tbCorreocontrasena.Text;
                String sActivo = "";
                if ((cbActivo.SelectedItem as ComboboxItem) != null)
                {
                    sActivo = (cbActivo.SelectedItem as ComboboxItem).Value.ToString();
                }
                else {
                    MessageBox.Show("Debe seleccionar si el usuario está activo o descactivado antes de guardar los cambios.");
                    return;
                }
                
                String sPerfil="";
                if ((cbPerfil.SelectedItem as ComboboxItem) != null)
                {
                    sPerfil = (cbPerfil.SelectedItem as ComboboxItem).Value.ToString();
                }else {
                    MessageBox.Show("Debe seleccionar un Perfil antes de guardar los cambios");
                    return;
                }
                
                String sUpdateusr = " UPDATE usuario  SET " +
                                    " `UsuarioNombre` = '" + sNombre + "', " +
                                    " `UsuarioPaterno` = '" + sPaterno + "', " +
                                    " `UsuarioMaterno` = '" + sMaterno + "', " +

                                    " `UsuarioClave` = '" + sUsuario + "', " +
                                    " `UsuarioPassword` = '" + sContraseña + "', " +
                                    " `UsuarioEmail` =  '" + sCorreo + "', " +
                                    " `UsuarioIndActivo` = " + sActivo + ", " +
                                    " `UsuarioFechaCambio` = '" + sFechacambio + "', " +

                                    " `UsuarioArea` = '" + sArea + "', " +
                                    " `UsuarioClaveEInvoice` = '" + sContrasenacorreo + "', " +
                                    " `UsuarioRFC` = '" + sRFC + "' " +
                                    " WHERE `UsuarioId` = '" + sIdusuario_g + "';";
                conect con2 = new conect();
                MySqlDataReader respuestastringupdate = con2.getdatareader(sUpdateusr);
                if (respuestastringupdate == null)
                {
                    bCambios = false;
                }else{
                    con2.Cerrarconexion();
                    respuestastringupdate.Close();

                    String sInsertperfil = "INSERT INTO `perfilusuario` (`PerfilId`, `UsuarioId`) VALUES ('" + sPerfil + "', '" + sIdusuario_g + "');";
                    conect con3 = new conect();
                    MySqlDataReader respuestastringupdate_perfil = con3.getdatareader(sInsertperfil);
                    if (respuestastringupdate_perfil == null)
                    {
                        con3.Cerrarconexion();
                        //respuestastringupdate_perfil.Close();
                        String sUpdateperfil = "UPDATE `perfilusuario` SET `PerfilId` = '" + sPerfil + "' WHERE  `UsuarioId`='" + sIdusuario_g + "';";
                        conect con4 = new conect();
                        MySqlDataReader respuestaUpdate_perfil = con4.getdatareader(sUpdateperfil);
                        if (respuestaUpdate_perfil != null)
                        {
                            bCambios = true;
                            respuestaUpdate_perfil.Close();
                            con4.Cerrarconexion();
                        }
                        else {
                            bCambios = false;
                        }
                    }
                    else { 
                        //CORRECTO   
                        bCambios = true;
                        respuestastringupdate_perfil.Close();
                        con3.Cerrarconexion();
                    }
                }
                
            }
            else {
                MessageBox.Show("Debe seleccionar un usuario de la lista y dar click en el botón modificar para guardar los cambios");
            }
            if (bCambios)
            {
                sIdusuario_g = "";
                TB_nombre_usuario.Text = "";
                TB_apellidopusuario.Text = "";
                TB_apellidomusuarios.Text = "";
                TB_usuariousuario.Text = "";
                TB_areausuario.Text = "";
                TB_correousuario.Text = "";
                TB_contraseñausuario.Text = "";
                TB_rfcusuario.Text = "";
                tbCorreocontrasena.Text = "";
                cbActivo.Text = "";
                cbPerfil.Text = "";
                BT_agregar.Enabled = true;
                btGuardar.Enabled = false;
                actualizatabla();
                MessageBox.Show("Cambios Realizados correctamente.");

            }
            else {
                if (sIdusuario_g!="")
                {
                    MessageBox.Show("Revise que todos los campos sean correctos");
                }
                
            }
            
        }

        private void lvUsuarios_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                String Usuarioid = lvUsuarios.SelectedItems[0].SubItems[0].Text;//id
                String sNombre = lvUsuarios.SelectedItems[0].SubItems[1].Text;//Nombre
                String sPaterno = lvUsuarios.SelectedItems[0].SubItems[2].Text;//Paterno
                String sMaterno = lvUsuarios.SelectedItems[0].SubItems[3].Text;//Materno
                String sActivo = lvUsuarios.SelectedItems[0].SubItems[4].Text;//Activo
                String sUsuario = lvUsuarios.SelectedItems[0].SubItems[5].Text;//Usuario
                String sContraseña = lvUsuarios.SelectedItems[0].SubItems[6].Text;//Contraseña
                String sCorreo = lvUsuarios.SelectedItems[0].SubItems[7].Text;//Correo
                String sContrasenacorreo = lvUsuarios.SelectedItems[0].SubItems[8].Text;//Contrasenacorreo
                String sArea = lvUsuarios.SelectedItems[0].SubItems[9].Text;//Area
                String sRFC = lvUsuarios.SelectedItems[0].SubItems[10].Text;//RFC
                String sPerfil = lvUsuarios.SelectedItems[0].SubItems[11].Text;//Perfil
                sIdusuario_g = Usuarioid;
                TB_nombre_usuario.Text = sNombre;
                TB_apellidopusuario.Text = sPaterno;
                TB_apellidomusuarios.Text = sMaterno;
                TB_usuariousuario.Text = sUsuario;
                TB_areausuario.Text = sArea;
                TB_correousuario.Text = sCorreo;
                TB_contraseñausuario.Text = sContraseña;
                TB_rfcusuario.Text = sRFC;
                tbCorreocontrasena.Text = sContrasenacorreo;
                cbActivo.Text = sActivo;
                cbPerfil.Text = sPerfil;
                BT_agregar.Enabled = false;
                btGuardar.Enabled = true;

            }
            catch (Exception E)
            {
                MessageBox.Show("Error al intentar cargar el usuario seleccionado.");
            }
        }


    }
}
