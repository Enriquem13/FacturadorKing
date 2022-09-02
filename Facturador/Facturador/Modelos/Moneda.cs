using System;

namespace Facturador.Facturador.Modelos
{
    public class Moneda : ModeloBase
    {
        public Moneda(long id, string descripcion, string simbolo, string sufijo, Estatus estatus = Estatus.Limpio)
            : base(id, estatus)
        {
            Descripcion = descripcion;
            Simbolo = simbolo;
            Sufijo = sufijo;
        }

        public Moneda(string descripcion, string simbolo, string sufijo)
            : this(0, descripcion, simbolo, sufijo, Estatus.Nuevo)
        {

        }

        public string Descripcion { get; set; }

        public string Simbolo { get; set; }

        public string Sufijo { get; set; }
    }
}
