using System;
using System.Linq;
using System.Collections.Generic;


namespace Facturador.Facturador.Modelos.Conversion
{
    public class ConvertidorDivisas
    {
        public ConvertidorDivisas() {
            TiposDeCambio = new List<TipoDeCambio>();
        }

        public decimal Convertir(Moneda de, Moneda a, decimal monto)
        {
            if (de == null || a == null) {
                return 0;
            }
            if (TiposDeCambio.Count == 0) {
                return 0;
            }
            var tipoDeCambio = BuscarTipoDeCambio(de, a);
            if (tipoDeCambio != null) {
                return monto / tipoDeCambio.Razon;
            }
            // Buscar el inverso, y si existe, usarlo.
            tipoDeCambio = BuscarTipoDeCambio(a, de);
            if (tipoDeCambio != null) {
                return monto / (1 / tipoDeCambio.Razon);
            }
            return 0;
        }

        public int ImpuestoDerechos
        {
            get { return IMPUESTO; }
        }

        public Moneda Pesos { get; set; }

        public Moneda Dolares { get; set; }

        public Moneda Euros { get; set; }

        public decimal RazonDolares
        {
            get { return Convertir(Dolares, Pesos, 1); }
        }

        public decimal RazonEuros
        {
            get { return Convertir(Euros, Pesos, 1); }
        }

        public TipoDeCambio BuscarTipoDeCambio(Moneda de, Moneda a)
        {
            var tipoDeCambio = TiposDeCambio.FirstOrDefault(t => t.De.ID == de.ID && t.A.ID == a.ID);

            return tipoDeCambio;
        }

        internal List<TipoDeCambio> TiposDeCambio {get; set;}

        private static readonly int IMPUESTO = 16;
    }
}
