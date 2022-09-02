using System;
using System.Data.Common;
using System.ComponentModel;
using Facturador.Facturador.Modelos;
using Facturador.Facturador.Utilerias;

namespace Facturador.Facturador.Datos
{
    public class TipoSolicitudAdapter
    {
        public static TipoSolicitud Obtener(DbDataReader reader)
        {
            var id = BaseDeDatos.ObtenerLongOCero(reader, "TipoSolicitudId");
            if (id == 0) {
                return null;
            }
            var descripcion = BaseDeDatos.ObtenerCadenaONulo(reader, "TipoSolicitudDescrip");
            var tipo = new TipoSolicitud(id, descripcion);

            tipo.DescripcionIngles = BaseDeDatos.ObtenerCadenaONulo(reader, "TipoSolicitudDescripI");
            tipo.Vigencia = BaseDeDatos.ObtenerIntONulo(reader, "TipoSolicitudVigencia");
            tipo.Grupo = GrupoAdapter.ObtenerGrupo(reader);

            return tipo;
        }
    }
}
