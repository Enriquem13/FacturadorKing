using System;
using System.Linq;
using System.Data.Common;
using System.Collections.Generic;
using Facturador.Facturador.Modelos;
using Facturador.Facturador.Utilerias;

namespace Facturador.Facturador.Datos
{
    public class MonedaAdapter
    {
        public static List<Moneda> CargarMonedas(BaseDeDatos baseDeDatos)
        {
            baseDeDatos.Conectar();
            baseDeDatos.Preparar(ConsultaMoneda);

            var monedas = new List<Moneda>();

            using (var reader = baseDeDatos.RealizarConsulta())
            {
                while (reader.Read()) {
                    monedas.Add(ObtenerMoneda(reader));
                }
            }

            return monedas;
        }

        public static Moneda ObtenerMoneda(DbDataReader reader) {
            return ObtenerMoneda(reader, "MonedaId", "MonedaDescripcion", "MonedaSimbolo", "MonedaSufijo");
        }

        internal static Moneda ObtenerMoneda(DbDataReader reader, string columnaId, string columnaDescripcion,
            string columnaSimbolo, string columnaSufijo)
        {
            var id = BaseDeDatos.ObtenerLongOCero(reader, columnaId);
            if (id == 0) {
                return null;
            }
            var descripcion = BaseDeDatos.ObtenerCadenaONulo(reader, columnaDescripcion);
            var simbolo = BaseDeDatos.ObtenerCadenaONulo(reader, columnaSimbolo);
            var sufijo = BaseDeDatos.ObtenerCadenaONulo(reader, columnaSufijo);
            var moneda = new Moneda(id, descripcion, simbolo, sufijo);

            return moneda;
        }

        internal static Moneda ObtenerMonedaPorSufijo(BaseDeDatos baseDeDatos, string sufijo)
        {
            Moneda moneda = null;
            var consulta = string.Format(ConsultaSufijo, ConsultaMoneda, sufijo);

            baseDeDatos.Conectar();
            baseDeDatos.Preparar(consulta);

            using (var reader = baseDeDatos.RealizarConsulta())
            {
                while (reader.Read()) {
                    moneda = ObtenerMoneda(reader);
                }
            }
            return moneda;
        }

        public static Moneda ObtenerMoneda(DbDataReader reader, string columnaId, string columnaDescripcion,
            string columnaSimbolo, string columnaSufijo, List<Moneda> monedas)
        {
            var id = BaseDeDatos.ObtenerLongOCero(reader, columnaId);
            if (id == 0) {
                return null;
            }
            var moneda = monedas.FirstOrDefault(m => m.ID == id);
            if (moneda != null) {
                return moneda;
            }
            moneda = ObtenerMoneda(reader, columnaId, columnaDescripcion, columnaSimbolo, columnaSufijo);
            if (moneda != null) {
                monedas.Add(moneda);
            }
            return moneda;
        }

        private static readonly string ConsultaMoneda = @"SELECT MonedaId AS MonedaId, MonedaDescrip AS MonedaDescripcion,
            MonedaSimbolo AS MonedaSimbolo, MonedaDescripSufijo AS MonedaSufijo FROM moneda";

        private static readonly string ConsultaSufijo = "{0} WHERE MonedaDescripSufijo = '{1}' LIMIT 1";
    }
}
