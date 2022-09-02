using System;

namespace Facturador.Facturador.Modelos
{
    public abstract class PersonaBase : ModeloBase
    {
        public PersonaBase(long id, string nombre, Estatus estatus = Estatus.Limpio)
            : base(id, estatus)
        {
            Nombre = nombre;
        }

        public string Nombre { get; set; }

        public string ApellidoPaterno { get; set; }

        public string ApellidoMaterno { get; set; }

        public Moneda Moneda { get; set; }

        public Tarifa Tarifa { get; set; }

        public string NombreMoneda
        {
            get
            {
                if (Moneda == null) {
                    return string.Empty;
                }
                return Moneda.Descripcion;
            }
        }

        public string NombreTarifa
        {
            get
            {
                if (Tarifa == null) {
                    return string.Empty;
                }
                return Tarifa.Nombre;
            }
        }
    }
}
