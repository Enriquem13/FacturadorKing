using System;

namespace Facturador.Facturador.Modelos
{
    public class Cliente : PersonaBase
    {
        public Cliente(long id, string nombre, Estatus estatus = Estatus.Limpio)
            : base(id, nombre, estatus)
        {

        }
    }
}
