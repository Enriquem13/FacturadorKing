using System;
using System.Windows.Forms;
using Facturador.Facturador.Datos;
using Facturador.Facturador.Modelos;
using Facturador.Facturador.Modelos.Conversion;
using Facturador.Facturador.Utilerias;


namespace Facturador.Facturador
{
    public partial class Facturador : Form
    {
        public Facturador()
        {
            InitializeComponent();
            InicializarEnlaceDeDatos();
            CargarListadoDeGrupos();
        }

        private void InicializarEnlaceDeDatos()
        {
            BaseDeDatos = new BaseDeDatos();
            CasoBinding = new BindingSource();
            GruposBinding = new BindingSource();
            TarifasBinding = new BindingSource();
            FacturaBinding = new BindingSource();
            
            Convertidor = TipoDeCambioAdapter.CargarTiposDeCambio(BaseDeDatos);
            AgregarEnlaceDeDatosMonedas();
        }

        private void AgregarEnlaceDeDatosMonedas()
        {
            textDolaresPesos.DataBindings.Clear();
            var bindingDolares = textDolaresPesos.DataBindings.Add("Text", Convertidor, "RazonDolares", true);
            bindingDolares.FormatString = "N";

            textEurosPesos.DataBindings.Clear();
            var bindingEuros = textEurosPesos.DataBindings.Add("Text", Convertidor, "RazonEuros", true);
            bindingEuros.FormatString = "N";
        }

        private void CargarListadoDeGrupos()
        {
            GruposBinding.DataSource = GrupoAdapter.CargarGrupos(BaseDeDatos);
            comboGrupos.DataSource = GruposBinding;
            comboGrupos.DisplayMember = "Descripcion";
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            long id;
            var grupo = GruposBinding.Current as Grupo;

            if (long.TryParse(textBuscarCaso.Text, out id)) {
                BuscarCaso(id, grupo);
            }
        }

        private void BuscarCaso(long id, Grupo grupo)
        {
            var caso = CasoAdapter.BuscarCaso(id, grupo, BaseDeDatos);
            if (caso != null)
            {
                CasoBinding.DataSource = caso;

                AgregarDataBindingsControles();
                CargarTarifaCorrecta(caso);
            }
        }

        private void AgregarDataBindingsControles()
        {
            AgregarDataBindingsCliente();
            AgregarDataBindingsInteresado();
            AgregarDataBindingsCaso();
        }

        private void AgregarDataBindingsCliente()
        {
            if (textNoCliente.DataBindings.Count > 0) {
                return;
            }
            textNoCliente.DataBindings.Add("Text", CasoBinding, "Cliente.ID");
            textNombreCliente.DataBindings.Add("Text", CasoBinding, "NombreCliente");
            textTarifaCliente.DataBindings.Add("Text", CasoBinding, "NombreTarifaCliente");
            textMonedaCliente.DataBindings.Add("Text", CasoBinding, "DescripcionMonedaCliente");
        }

        private void AgregarDataBindingsInteresado()
        {
            if (textNoInteresado.DataBindings.Count > 0) {
                return;
            }
            textNoInteresado.DataBindings.Add("Text", CasoBinding, "Interesado.ID");
            textNombreInteresado.DataBindings.Add("Text", CasoBinding, "Interesado.NombreCorto");
            textTarifaInteresado.DataBindings.Add("Text", CasoBinding, "NombreTarifaInteresado");
            textMonedaInteresado.DataBindings.Add("Text", CasoBinding, "DescripcionMonedaInteresado");
        }

        private void AgregarDataBindingsCaso()
        {
            if (textNumeroCaso.DataBindings.Count > 0) {
                return;
            }
            textNumeroCaso.DataBindings.Add("Text", CasoBinding, "Numero");
            textExpedienteCaso.DataBindings.Add("Text", CasoBinding, "NumeroExpediente");
            textReferenciaCaso.DataBindings.Add("Text", CasoBinding, "Referencia.Nombre");
            textTituloCaso.DataBindings.Add("Text", CasoBinding, "Titulo");
            textTituloInglesCaso.DataBindings.Add("Text", CasoBinding, "TituloIngles");
            textTarifaCaso.DataBindings.Add("Text", CasoBinding, "Moneda.Descripcion");
            textMonedaCaso.DataBindings.Add("Text", CasoBinding, "Tarifa.Nombre");
        }

        private void CargarTarifaCorrecta(Caso caso)
        {
            var tarifa = caso.Tarifa;
            if (tarifa == null) {
                tarifa = caso.Interesado.Tarifa;
            }
            if (tarifa == null) {
                tarifa = caso.Cliente.Tarifa;
            }
            if (tarifa == null) {
                return;
            }
            tarifa = TarifaAdapter.CargarEntradasTarifa(tarifa, BaseDeDatos);
            tarifa.Convertidor = Convertidor;

            EnlazarControlesTarifa(tarifa);
            AgregarEnlacesFactura();
        }

        private void EnlazarControlesTarifa(Tarifa tarifa)
        {
            comboConceptos.Enabled = true;
            textDerechos.Enabled = true;
            btnAgregar.Enabled = true;

            TarifasBinding.DataSource = tarifa.Entradas;
            comboConceptos.DataSource = TarifasBinding;
            comboConceptos.DisplayMember = "ConceptoNombre";

            textTarifa.DataBindings.Clear();
            var bindingTarifa = textTarifa.DataBindings.Add("Text", TarifasBinding, "Honorarios", true);
            bindingTarifa.FormatString = "N";

            labelDenominacionHonorarios.DataBindings.Clear();
            labelDenominacionHonorarios.DataBindings.Add("Text", TarifasBinding, "Tarifa.Moneda.Sufijo", true);

            textDerechos.DataBindings.Clear();
            var bindingDerechos = textDerechos.DataBindings.Add("Text", TarifasBinding, "Derechos", true);
            bindingDerechos.FormatString = "N";

            textImpuesto.DataBindings.Clear();
            var bindingImpuesto = textImpuesto.DataBindings.Add("Text", TarifasBinding, "ImpuestoDerechos", true);
            bindingImpuesto.FormatString = "N";

            textTotalDerechosPesos.DataBindings.Clear();
            var bindingTotal = textTotalDerechosPesos.DataBindings.Add("Text", TarifasBinding, "TotalDerechos", true);
            bindingTotal.FormatString = "N";

            textDerechosMoneda.DataBindings.Clear();
            var bindingMoneda = textDerechosMoneda.DataBindings.Add("Text", TarifasBinding, "TotalDerechosMonedaRedondeado", true);
            bindingMoneda.FormatString = "N";

            labelDenominacionDerechos.DataBindings.Clear();
            labelDenominacionDerechos.DataBindings.Add("Text", TarifasBinding, "Tarifa.Moneda.Sufijo", true);
        }

        private void AgregarEnlacesFactura()
        {
            FacturaBinding.DataSource = new Factura();
            dataGridFactura.DataBindings.Clear();
            dataGridFactura.DataBindings.Add("DataSource", FacturaBinding, "Entradas", true);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var entrada = TarifasBinding.Current as EntradaTarifa;
            if (entrada == null)
            {
                throw new Exception("No se pudo obtener la tarifa para agregarla a la factura");
            }
            var factura = FacturaBinding.Current as Factura;
            if (factura == null)
            {
                throw new Exception("Error al agregar la entrada a la factura");
            }
            factura.Agregar(entrada);
            comboConceptos.SelectedIndex = -1;
        }

        private void ConceptosSelectedIndexChanged(object sender, EventArgs e)
        {
            var combo = sender as ComboBox;
            if (combo == null) {
                return;
            }
            if (combo.SelectedIndex == -1)
            {
                TarifasBinding.SuspendBinding();
            }
            else
            {
                TarifasBinding.ResumeBinding();
            }
        }

        private BaseDeDatos BaseDeDatos { get; set; }
        private BindingSource CasoBinding { get; set; }
        private BindingSource GruposBinding { get; set; }
        private BindingSource TarifasBinding { get; set; }
        private ConvertidorDivisas Convertidor { get; set;}
        private BindingSource FacturaBinding { get; set; }
    }
}
