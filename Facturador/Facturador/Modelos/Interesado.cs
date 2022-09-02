using System;

namespace Facturador.Facturador.Modelos
{
    public class Interesado : PersonaBase
    {
        public Interesado(long id, string nombre, Estatus estatus = Estatus.Limpio)
            : base(id, nombre, estatus)
        {

        }

        public string NombreCorto { get; set; }
    }
}
