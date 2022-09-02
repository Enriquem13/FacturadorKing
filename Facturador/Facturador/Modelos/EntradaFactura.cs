using System;
using System.Collections.Generic;

namespace Facturador.Facturador.Modelos
{
    public class EntradaFactura : ModeloBase
    {
        public EntradaFactura(long id, Factura factura, EntradaTarifa tarifa, Estatus estatus = Estatus.Limpio)
            : base(id, estatus)
        {
            Factura = factura;
            EntradaTarifa = tarifa;

            Honorarios = tarifa.Honorarios;
            Derechos = tarifa.Derechos;
            ImpuestoDerechos = tarifa.ImpuestoDerechos;
            TotalDerechos = tarifa.TotalDerechos;
            TotalDerechosMoneda = tarifa.TotalDerechosMoneda;
            TotalDerechosMonedaRedondeado = tarifa.TotalDerechosMonedaRedondeado;
        }

        public decimal Honorarios { get; set; }

        public decimal Derechos { get; set; }

        public decimal ImpuestoDerechos { get; set; }

        public decimal TotalDerechos { get; set; }

        public decimal TotalDerechosMoneda { get; set; }

        public decimal TotalDerechosMonedaRedondeado { get; set; }

        public Factura Factura { get; set; }

        private EntradaTarifa EntradaTarifa { get; set; }
    }
}
