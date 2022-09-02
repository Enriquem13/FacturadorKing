using System;

namespace Facturador.Facturador.Modelos
{
    public class Grupo : ModeloBase
    {
        public Grupo(long id, string descripcion, Estatus estatus = Estatus.Limpio)
            : base(id, estatus)
        {
            Descripcion = descripcion;
        }

        public string Descripcion { get; set; }
    }
}
