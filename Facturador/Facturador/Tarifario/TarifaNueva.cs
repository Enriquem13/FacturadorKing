using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Facturador.Facturador.Datos;
using Facturador.Facturador.Modelos;
using Facturador.Facturador.Utilerias;

namespace Facturador.Facturador.Tarifario
{
    public partial class TarifaNueva : Form
    {
        public TarifaNueva()
        {
            InitializeComponent();
            InicializarEnlaceDeDatos();
            CargarListadoDeTarifas();
        }

        private void InicializarEnlaceDeDatos()
        {
            BaseDeDatos = new BaseDeDatos();
            Tarifas = new BindingList<Tarifa>();
            TarifasBinding = new BindingSource();

            listBoxTarifas.DataSource = TarifasBinding;
        }

        private void CargarListadoDeTarifas()
        {
            Tarifas = TarifaAdapter.CargarNombresTarifas(BaseDeDatos);
            TarifasBinding.DataSource = Tarifas;
            TarifasBinding.ResetBindings(false);

            listBoxTarifas.DisplayMember = "Nombre";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textNombre.Text)) {
                return;
            }
            var respuesta = MessageBox.Show(this, string.Format("¿Desea agregar la tarifa con nombre '{0}'?", textNombre.Text),
                "Agregar nueva tarifa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes) {
                AgregarNuevaTarifa(textNombre.Text);
            }
        }

        private void AgregarNuevaTarifa(string nombre)
        {
            try
            {
                var tarifa = TarifaAdapter.AgregarNuevaTarifa(nombre, BaseDeDatos);
                if (tarifa != null)
                {
                    Tarifas.Add(tarifa);
                    TarifasBinding.ResetBindings(false);

                    textNombre.Clear();
                    textNombre.Focus();
                }
            }
            catch(Exception ex) {
                MessageBox.Show(this, ex.Message, "Error al agregar la nueva tarifa", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e) {
            Close();
        }

        private BaseDeDatos BaseDeDatos;
        private BindingList<Tarifa> Tarifas { get; set; }
        private BindingSource TarifasBinding { get; set; }
    }
}
