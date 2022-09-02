using System;
using System.ComponentModel;

namespace Facturador.Facturador.Modelos
{
    public class EntradaTarifa : ModeloBase
    {
        public EntradaTarifa(long id, decimal monto, ConceptoTarifa concepto, Estatus estatus = Estatus.Limpio)
            : base(id, estatus)
        {
            Honorarios = monto;
            Concepto = concepto;
        }

        public EntradaTarifa(decimal monto, ConceptoTarifa concepto)
            : this(0, monto, concepto, Estatus.Nuevo)
        {
        }

        public decimal Honorarios { get; set; }

        public decimal Derechos
        {
            get { return Concepto.Derechos; }
            set { Concepto.Derechos = value; }
        }

        public decimal ImpuestoDerechos
        {
            get { return (Tarifa.Convertidor.ImpuestoDerechos / 100m) * Derechos; }
        }

        public decimal TotalDerechos
        {
            get { return Derechos + ImpuestoDerechos; }
        }

        public decimal TotalDerechosMoneda
        {
            get { return Tarifa.Convertidor.Convertir(Tarifa.Convertidor.Pesos, Tarifa.Moneda, TotalDerechos); }
        }

        public decimal TotalDerechosMonedaRedondeado
        {
            get { return Math.Ceiling(TotalDerechosMoneda); }
        }

        public ConceptoTarifa Concepto { get; set; }

        public Tarifa Tarifa { get; set; }

        public string ConceptoNombre {
            get { return Concepto.Nombre; }
        }

        public string ConceptoNombreIngles {
            get { return Concepto.NombreIngles; }
        }
    }
}
