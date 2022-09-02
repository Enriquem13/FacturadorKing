using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Facturador.Facturador.Modelos;
using Facturador.Facturador.Datos;
using Facturador.Facturador.Utilerias;

namespace Facturador.Facturador.Tarifario
{
    public partial class SeleccionarTarifa : Form
    {
        public SeleccionarTarifa()
        {
            InitializeComponent();
            InicializarEnlaceDeDatos();
            ActivarControles();
            CargarDatosTarifas();
            CargarDatosGrupos();
        }

        private void InicializarEnlaceDeDatos()
        {
            BaseDeDatos = new BaseDeDatos();
            ClienteBinding = new BindingSource();
            TarifasBinding = new BindingSource();
            InteresadoBinding = new BindingSource();
            GruposBinding = new BindingSource();
            CasoBinding = new BindingSource();
        }

        private void ActivarControles()
        {
            if (radioCliente.Checked) {
                ActivarControlesCliente();
            }
            else if (radioInteresado.Checked) {
                ActivarControlesInteresado();
            }
            else if (radioCaso.Checked) {
                ActivarControlesCaso();
            }
        }

        private void ActivarControlesCliente()
        {
            panelCliente.Visible = true;
            panelInteresado.Visible = false;
            panelCaso.Visible = false;
            comboGrupos.Visible = false;
            textNombre.Visible = true;

            labelNombre.Text = "N&ombre cliente:";
            groupDatosTipo.Text = "Datos cliente";
            labelNumero.Text = "&No. Cliente:";
        }

        private void ActivarControlesInteresado()
        {
            panelCliente.Visible = false;
            panelInteresado.Visible = true;
            panelCaso.Visible = false;
            comboGrupos.Visible = false;
            textNombre.Visible = true;

            groupDatosTipo.Text = "Datos interesado";
            labelNumero.Text = "&No. Interesado:";
            labelNombre.Text = "N&ombre Interesado:";
        }

        private void ActivarControlesCaso()
        {
            panelCliente.Visible = false;
            panelInteresado.Visible = false;
            panelCaso.Visible = true;
            comboGrupos.Visible = true;
            textNombre.Visible = false;

            groupDatosTipo.Text = "Datos caso";
            labelNumero.Text = "&No. Caso:";
            labelNombre.Text = "&Grupo:";
        }

        private void CargarDatosTarifas()
        {
            var tarifas = TarifaAdapter.CargarTarifas(BaseDeDatos);
            TarifasBinding.DataSource = tarifas;

            listBoxTarifas.DataSource = TarifasBinding;
            listBoxTarifas.DisplayMember = "Nombre";
        }

        private void CargarDatosGrupos()
        {
            var grupos = GrupoAdapter.CargarGrupos(BaseDeDatos);
            GruposBinding.DataSource = grupos;

            comboGrupos.DataSource = GruposBinding;
            comboGrupos.DisplayMember = "Descripcion";
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (radioCliente.Checked) {
                BuscarCliente();
            }
            else if (radioInteresado.Checked) {
                BuscarInteresado();
            }
            else if (radioCaso.Checked) {
                BuscarCaso();
            }
        }

        private void BuscarCliente()
        {
            var info = ObtenerInformacionBusqueda();
            Modelos.Cliente cliente = null;

            if (!info.IdValido && !info.NombreValido)
            {
                textNumero.SelectAll();
                return;
            }
            if (info.IdValido) {
                cliente = ClienteAdapter.BuscarCliente(info.ID, BaseDeDatos);
            }
            else if (info.NombreValido) {
                cliente = ClienteAdapter.BuscarCliente(info.Nombre, BaseDeDatos);
            }
            if (cliente != null)
            {
                textNumero.Focus();
                textNumero.Clear();
                EnlazarControlesCliente(cliente);
            }
        }

        private InformacionBusqueda ObtenerInformacionBusqueda()
        {
            long id;
            var informacion = new InformacionBusqueda
            {
                IdValido = long.TryParse(textNumero.Text, out id),
                NombreValido = !string.IsNullOrEmpty(textNombre.Text),
                ID = id,
                Nombre = textNombre.Text,
                Grupo = GruposBinding.Current as Grupo
            };
            return informacion;
        }

        private void EnlazarControlesCliente(Modelos.Cliente cliente)
        {
            ClienteBinding.DataSource = cliente;

            if (textNoCliente.DataBindings.Count == 0)
            {
                textNoCliente.DataBindings.Add("Text", ClienteBinding, "ID");
                textNombreCliente.DataBindings.Add("Text", ClienteBinding, "Nombre");
                textMonedaCliente.DataBindings.Add("Text", ClienteBinding, "NombreMoneda");
                textTarifaCliente.DataBindings.Add("Text", ClienteBinding, "NombreTarifa");
            }
            ClienteBinding.ResetBindings(false);
        }

        private void BuscarInteresado()
        {
            //var info = ObtenerInformacionBusqueda();
            //Interesado interesado = null;

            //if (!info.IdValido && !info.NombreValido)
            //{
            //    textNumero.SelectAll();
            //    return;
            //}
            //if (info.IdValido) {
            //    interesado = InteresadoAdapter.BuscarInteresado(info.ID, BaseDeDatos);
            //}
            //else if (info.NombreValido) {
            //    interesado = InteresadoAdapter.BuscarInteresado(info.Nombre, BaseDeDatos);
            //}
            //if (interesado != null)
            //{
            //    textNumero.Focus();
            //    textNumero.Clear();
            //    EnlazarControlesInteresado(interesado);
            //}
        }

        private void EnlazarControlesInteresado(Interesado interesado)
        {
            InteresadoBinding.DataSource = interesado;

            if (textNoInteresado.DataBindings.Count == 0)
            {
                textNoInteresado.DataBindings.Add("Text", InteresadoBinding, "ID");
                textNombreInteresado.DataBindings.Add("Text", InteresadoBinding, "Nombre");
                textNombreCorto.DataBindings.Add("Text", InteresadoBinding, "NombreCorto");
                textMonedaInteresado.DataBindings.Add("Text", InteresadoBinding, "NombreMoneda");
                textTarifaInteresado.DataBindings.Add("Text", InteresadoBinding, "NombreTarifa");
            }
            InteresadoBinding.ResetBindings(false);
        }

        private void BuscarCaso()
        {
            var info = ObtenerInformacionBusqueda();
            Caso caso = null;

            if (!info.IdValido)
            {
                textNumero.SelectAll();
                return;
            }
            if (info.IdValido) {
                caso = CasoAdapter.BuscarCaso(info.ID, info.Grupo, BaseDeDatos);
            }
            if (caso != null)
            {
                textNumero.Focus();
                textNumero.Clear();
                EnlazarControlesCaso(caso);
            }
        }

        private void EnlazarControlesCaso(Caso caso)
        {
            CasoBinding.DataSource = caso;

            if (textNoCaso.DataBindings.Count == 0)
            {
                textNoCaso.DataBindings.Add("Text", CasoBinding, "Numero");
                textTituloCaso.DataBindings.Add("Text", CasoBinding, "Titulo");
                textReferencia.DataBindings.Add("Text", CasoBinding, "NombreReferencia");
                textMonedaCaso.DataBindings.Add("Text", CasoBinding, "NombreTarifa");
                textTarifaCaso.DataBindings.Add("Text", CasoBinding, "DescripcionMoneda");
            }
            InteresadoBinding.ResetBindings(false);
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            if (radioCliente.Checked) {
                AsignarTarifaCliente();
            }
            else if (radioInteresado.Checked) {
                AsignarTarifaInteresado();
            }
            else if (radioCaso.Checked) {
                AsignarTarifaCaso();
            }
        }

        private void AsignarTarifaCliente()
        {
            var cliente = ClienteBinding.Current as Modelos.Cliente;
            var tarifa = TarifasBinding.Current as Tarifa;

            if (ModelosNulos(cliente, tarifa))
            {
                MostrarErrorAsignacion("tarifa", "cliente");
                return;
            }
            var respuesta = ConfirmarAsignacionTarifa("tarifa", tarifa.Nombre, "cliente", cliente.Nombre);
            if (respuesta == DialogResult.Yes)
            {
                ClienteAdapter.AsignarTarifa(cliente, tarifa, BaseDeDatos);
                ClienteBinding.ResetBindings(false);
            }
        }

        private bool ModelosNulos<T, U>(T modelo1, U modelo2)
            where T: ModeloBase
            where U: ModeloBase
        {
            return modelo1 == null || modelo2 == null;
        }

        private void MostrarErrorAsignacion(string tipo, string persona)
        {
            MessageBox.Show(this, string.Format("Error al asignar la {0} al {1}", tipo, persona), "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
        }

        private DialogResult ConfirmarAsignacionTarifa(string tipo, string nombreTipo, string persona, string nombrePersona)
        {
            var respuesta = MessageBox.Show(this,
                string.Format("¿Desea asignar la {0} '{1}' al {2} '{3}'?", tipo, nombreTipo, persona, nombrePersona),
                string.Format("Asignación de {0}", tipo), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            return respuesta;
        }

        private void AsignarTarifaInteresado()
        {
            //var interesado = InteresadoBinding.Current as Interesado;
            //var tarifa = TarifasBinding.Current as Tarifa;

            //if (ModelosNulos(interesado, tarifa))
            //{
            //    MostrarErrorAsignacion("tarifa", "interesado");
            //    return;
            //}
            //var respuesta = ConfirmarAsignacionTarifa("tarifa", tarifa.Nombre, "interesado", interesado.NombreCorto);
            //if (respuesta == DialogResult.Yes)
            //{
            //    InteresadoAdapter.AsignarTarifa(interesado, tarifa, BaseDeDatos);
            //    InteresadoBinding.ResetBindings(false);
            //}
        }

        private void AsignarTarifaCaso()
        {
            var tarifa = TarifasBinding.Current as Tarifa;
            var caso = CasoBinding.Current as Caso;

            if (ModelosNulos(caso, tarifa))
            {
                MostrarErrorAsignacion("tarifa", "caso");
                return;
            }
            var respuesta = ConfirmarAsignacionTarifa("tarifa", tarifa.Nombre, "caso", caso.Numero.ToString());
            if (respuesta == DialogResult.Yes)
            {
                CasoAdapter.AsignarTarifa(caso, tarifa, BaseDeDatos);
                CasoBinding.ResetBindings(false);
            }
        }

        private void radioTipo_CheckedChanged(object sender, EventArgs e) {
            ActivarControles();
        }

        private BindingSource ClienteBinding {get; set;}
        private BindingSource TarifasBinding { get; set; }
        private BindingSource InteresadoBinding { get; set; }
        private BindingSource GruposBinding { get; set; }
        private BindingSource CasoBinding { get; set; }
        private BaseDeDatos BaseDeDatos { get; set; }
    }

    class InformacionBusqueda
    {
        internal long ID { get; set; }

        internal string Nombre { get; set; }

        internal bool IdValido { get; set; }

        internal bool NombreValido { get; set; }

        internal Grupo Grupo { get; set; }
    }
}
