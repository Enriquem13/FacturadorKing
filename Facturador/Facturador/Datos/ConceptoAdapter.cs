using System;
using System.Linq;
using System.Data.Common;
using System.ComponentModel;
using System.Collections.Generic;
using Facturador.Facturador.Modelos;
using Facturador.Facturador.Utilerias;

namespace Facturador.Facturador.Datos
{
    public class ConceptoAdapter
    {
        public static BindingList<ConceptoTarifa> CargarConceptos(BaseDeDatos baseDeDatos)
        {
            baseDeDatos.Conectar();
            baseDeDatos.Preparar("SELECT * FROM concepto_tarifa");

            var conceptos = new BindingList<ConceptoTarifa>();

            using (var reader = baseDeDatos.RealizarConsulta())
            {
                while (reader.Read()) {
                    conceptos.Add(ObtenerConcepto(reader));
                }
            }
            return conceptos;
        }

        private static ConceptoTarifa ObtenerConcepto(DbDataReader reader)
        {
            var id = BaseDeDatos.ObtenerLongOCero(reader, "id");
            var nombre = BaseDeDatos.ObtenerCadenaONulo(reader, "nombre");
            var ingles = BaseDeDatos.ObtenerCadenaONulo(reader, "nombre_ingles");
            var derechos = BaseDeDatos.ObtenerDecimalOCero(reader, "derechos");
            var concepto = new ConceptoTarifa(id, nombre, ingles);
            concepto.Derechos = derechos;

            return concepto;
        }

        public static ConceptoTarifa ObtenerConcepto(DbDataReader reader, string columnaId, string columnaNombre, string columnaIngles, 
            string columnaDerechos, List<ConceptoTarifa> conceptos)
        {
            var id = BaseDeDatos.ObtenerLongOCero(reader, columnaId);
            if (id == 0) {
                return null;
            }
            var concepto = conceptos.FirstOrDefault(c => c.ID == id);
            if (concepto == null)
            {
                var nombre = BaseDeDatos.ObtenerCadenaONulo(reader, columnaNombre);
                var ingles = BaseDeDatos.ObtenerCadenaONulo(reader, columnaIngles);
                var derechos = BaseDeDatos.ObtenerDecimalOCero(reader, columnaDerechos);

                concepto = new ConceptoTarifa(id, nombre, ingles);
                concepto.Derechos = derechos;

                conceptos.Add(concepto);
            }
            return concepto;
        }
    }
}
