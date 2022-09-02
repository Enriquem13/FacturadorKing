using System;
using System.Data.Common;
using System.ComponentModel;
using Facturador.Facturador.Modelos;
using Facturador.Facturador.Utilerias;

namespace Facturador.Facturador.Datos
{
    public class ReferenciaAdapter
    {
        public static Referencia Obtener(DbDataReader reader)
        {
            var id = BaseDeDatos.ObtenerLongOCero(reader, "ReferenciaId");
            if (id == 0) {
                return null;
            }
            var nombre = BaseDeDatos.ObtenerCadenaONulo(reader, "ReferenciaNombre");
            var referencia = new Referencia(id, nombre);

            return referencia;
        }
    }
}
