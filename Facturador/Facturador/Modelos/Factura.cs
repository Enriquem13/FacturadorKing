using System;
using System.ComponentModel;
using System.Collections.Generic;
using Facturador.Facturador.Modelos.Conversion;


namespace Facturador.Facturador.Modelos
{
    public class Factura : ModeloBase
    {
        public Factura(long id, Estatus estatus = Estatus.Limpio) : base(id, estatus)
        {
            Entradas = new BindingList<EntradaFactura>();
        }

        public Factura() : this(0, Estatus.Nuevo)
        {
        }

        public EntradaFactura Agregar(EntradaTarifa entradaTarifa)
        {
            var entradaFactura = new EntradaFactura(0, this, entradaTarifa);
            Entradas.Add(entradaFactura);
            
            return entradaFactura;
        }

        public BindingList<EntradaFactura> Entradas { get; set; }

        public DateTime Fecha { get; set; }
    }
}
