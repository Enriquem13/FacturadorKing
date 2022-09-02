using System;
using System.Linq;
using System.Data.Common;
using System.Collections.Generic;
using Facturador.Facturador.Modelos;
using Facturador.Facturador.Modelos.Conversion;
using Facturador.Facturador.Utilerias;

namespace Facturador.Facturador.Datos
{
    public class TipoDeCambioAdapter
    {
        public static ConvertidorDivisas CargarTiposDeCambio(BaseDeDatos baseDeDatos)
        {
            baseDeDatos.Conectar();
            baseDeDatos.Preparar(ConsultaTipos);

            var tiposDeCambio = new List<TipoDeCambio>();
            var monedas = new List<Moneda>();
            var convertidor = new ConvertidorDivisas();

            using (var reader = baseDeDatos.RealizarConsulta())
            {
                while (reader.Read())
                {
                    var monedaDe = MonedaAdapter.ObtenerMoneda(reader, "MonedaDeId", "MonedaDeDescripcion", "MonedaDeSimbolo",
                        "MonedaDeSufijo", monedas);
                    var monedaA = MonedaAdapter.ObtenerMoneda(reader, "MonedaAId", "MonedaADescripcion", "MonedaASimbolo",
                        "MonedaASufijo", monedas);

                    if (monedaDe != null && monedaA != null)
                    {
                        var tipoDeCambio = ObtenerTipoDeCambio(reader, monedaDe, monedaA);
                        tiposDeCambio.Add(tipoDeCambio);

                        AsignarMonedaComun(convertidor, monedaDe, monedaA);
                    }
                }
                convertidor.TiposDeCambio = tiposDeCambio;
                UltimoRecursoAsignarMoneda(baseDeDatos, convertidor);
            }
            return convertidor;
        }

        private static void AsignarMonedaComun(ConvertidorDivisas convertidor, params Moneda[] monedas)
        {
            foreach (var moneda in monedas)
            {
                if (moneda.Sufijo == "MXN" && convertidor.Pesos == null)
                {
                    convertidor.Pesos = moneda;
                }
                if (moneda.Sufijo == "USD" && convertidor.Dolares == null)
                {
                    convertidor.Dolares = moneda;
                }
                if (moneda.Sufijo == "EUR" && convertidor.Euros == null)
                {
                    convertidor.Euros = moneda;
                }
            }
        }

        private static void UltimoRecursoAsignarMoneda(BaseDeDatos baseDeDatos, ConvertidorDivisas convertidor)
        {
            if (convertidor.Pesos == null)
            {
                convertidor.Pesos = MonedaAdapter.ObtenerMonedaPorSufijo(baseDeDatos, "MXN");
            }
            if (convertidor.Dolares == null)
            {
                convertidor.Dolares = MonedaAdapter.ObtenerMonedaPorSufijo(baseDeDatos, "USD");
            }
            if (convertidor.Euros == null)
            {
                convertidor.Euros = MonedaAdapter.ObtenerMonedaPorSufijo(baseDeDatos, "EUR");
            }
        }

        private static TipoDeCambio ObtenerTipoDeCambio(DbDataReader reader, Moneda monedaDe, Moneda monedaA)
        {
            var id = BaseDeDatos.ObtenerLongOCero(reader, "TipoId");
            if (id == 0) {
                return null;
            }
            var tipoDeCambio = new TipoDeCambio(id)
            {
                De = monedaDe,
                A = monedaA,
                Razon = BaseDeDatos.ObtenerLongOCero(reader, "TipoRazon")
            };
            return tipoDeCambio;
        }

        private static readonly string ConsultaTipos = @"SELECT moneda_de.MonedaId AS MonedaDeId,
            moneda_de.MonedaDescrip AS MonedaDeDescripcion, moneda_de.MonedaSimbolo as MonedaDeSimbolo,
            moneda_de.MonedaDescripSufijo AS MonedaDeSufijo, moneda_a.MonedaId AS MonedaAId,
            moneda_a.MonedaDescrip AS MonedaADescripcion, moneda_a.MonedaSimbolo as MonedaASimbolo,
            moneda_a.MonedaDescripSufijo AS MonedaASufijo,
            tipo.id AS TipoId, tipo.razon AS TipoRazon
            FROM tipo_de_cambio AS tipo
            JOIN moneda AS moneda_de ON (tipo.moneda_de = moneda_de.MonedaId)
            JOIN moneda AS moneda_a ON (tipo.moneda_a = moneda_a.MonedaId);";
    }
}
