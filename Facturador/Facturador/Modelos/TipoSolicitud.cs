using System;

namespace Facturador.Facturador.Modelos
{
    public class TipoSolicitud : ModeloBase
    {
        public TipoSolicitud(long id, string descripcion, Estatus estatus = Estatus.Limpio)
            : base(id, estatus)
        {
            Descripcion = descripcion;
        }

        public string Descripcion { get; set; }

        public string DescripcionIngles { get; set; }

        public int? Vigencia { get; set; }

        public Grupo Grupo { get; set; }

        public string DescripcionGrupo
        {
            get
            {
                if (Grupo == null) {
                    return string.Empty;
                }
                return Grupo.Descripcion;
            }
        }
    }
}
