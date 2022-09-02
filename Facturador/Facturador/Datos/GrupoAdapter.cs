using System;
using System.Data.Common;
using System.ComponentModel;
using System.Collections.Generic;
using Facturador.Facturador.Modelos;
using Facturador.Facturador.Utilerias;

namespace Facturador.Facturador.Datos
{
    public class GrupoAdapter
    {
        public static BindingList<Grupo> CargarGrupos(BaseDeDatos baseDeDatos)
        {
            baseDeDatos.Conectar();
            baseDeDatos.Preparar("SELECT * FROM grupo");

            var grupos = new BindingList<Grupo>();

            using (var reader = baseDeDatos.RealizarConsulta())
            {
                while (reader.Read()) {
                    grupos.Add(ObtenerGrupo(reader));
                }
            }

            return grupos;
        }

        public static Grupo ObtenerGrupo(DbDataReader reader)
        {
            var id = BaseDeDatos.ObtenerLongOCero(reader, "GrupoId");
            if (id == 0) {
                return null;
            }
            var descripcion = BaseDeDatos.ObtenerCadenaONulo(reader, "GrupoDescripcion");
            var grupo = new Grupo(id, descripcion);

            return grupo;
        }
    }
}
