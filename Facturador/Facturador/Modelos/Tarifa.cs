using System;
using System.ComponentModel;
using Facturador.Facturador.Modelos.Conversion;

namespace Facturador.Facturador.Modelos
{
    public class Tarifa : ModeloBase
    {
        public Tarifa(long id, string nombre, Estatus estatus = Estatus.Limpio)
            : base(id, estatus)
        {
            Nombre = nombre;
            Entradas = new BindingList<EntradaTarifa>();
            Convertidor = new ConvertidorDivisas();
        }

        public string Nombre { get; set; }

        public Moneda Moneda { get; set; }

        public BindingList<EntradaTarifa> Entradas { get; set; }

        public ConvertidorDivisas Convertidor { get; set; }
    }
}
