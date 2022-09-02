using System;
using System.Linq;
using System.Collections.Generic;


namespace Facturador.Facturador.Modelos.Conversion
{
    public class TipoDeCambio : ModeloBase
    {
        public TipoDeCambio(long id, Estatus estatus = Estatus.Limpio) : base(id, estatus)
        {

        }

        public Moneda De { get; set; }

        public Moneda A { get; set; }

        public decimal Razon { get; set; }
    }
}
