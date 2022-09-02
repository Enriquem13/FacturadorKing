using System;

namespace Facturador.Facturador.Modelos
{
    public class Caso : ModeloBase
    {
        public Caso(long id, Estatus estatus = Estatus.Limpio) : base(id, estatus)
        {

        }

        public string Titulo { get; set; }

        public string TituloIngles { get; set; }

        public int Numero { get; set; }

        public string NumeroExpediente { get; set; }

        public Tarifa Tarifa { get; set; }

        public Moneda Moneda { get; set; }

        public Referencia Referencia { get; set; }

        public Cliente Cliente { get; set; }

        public Interesado Interesado { get; set; }

        public TipoSolicitud TipoSolicitud { get; set; }

        public long NumeroCliente
        {
            get
            {
                if (Cliente == null) {
                    return 0;
                }
                return Cliente.ID;
            }
        }

        public string NombreCliente
        {
            get
            {
                if (Cliente == null)
                {
                    return string.Empty;
                }
                return Cliente.Nombre;
            }
        }

        public string NombreReferencia
        {
            get
            {
                if (Referencia == null)
                {
                    return string.Empty;
                }
                return Referencia.Nombre;
            }
        }

        public string NombreTarifa
        {
            get
            {
                if (Tarifa == null)
                {
                    return string.Empty;
                }
                return Tarifa.Nombre;
            }
        }

        public string DescripcionMoneda
        {
            get
            {
                if (Moneda == null)
                {
                    return string.Empty;
                }
                return Moneda.Descripcion;
            }
        }

        public string NombreTarifaCliente
        {
            get
            {
                if (Cliente == null)
                {
                    return string.Empty;
                }
                return Cliente.NombreTarifa;
            }
        }

        public string DescripcionMonedaCliente
        {
            get
            {
                if (Cliente == null) {
                    return string.Empty;
                }
                return Cliente.NombreMoneda;
            }
        }

        public string NombreTarifaInteresado
        {
            get
            {
                if (Interesado == null) {
                    return string.Empty;
                }
                return Interesado.NombreTarifa;
            }
        }

        public string DescripcionMonedaInteresado
        {
            get
            {
                if (Interesado == null) {
                    return string.Empty;
                }
                return Interesado.NombreMoneda;
            }
        }
    }
}
