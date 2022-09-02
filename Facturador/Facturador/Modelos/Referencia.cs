using System;

namespace Facturador.Facturador.Modelos
{
    public class Referencia : ModeloBase
    {
        public Referencia(long id, string nombre, Estatus estatus = Estatus.Limpio) : base(id, estatus)
        {
            Nombre = nombre;
        }

        public string Nombre { get; set; }
    }
}
