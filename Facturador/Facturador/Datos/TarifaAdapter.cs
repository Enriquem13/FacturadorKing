using System;
using System.Linq;
using System.Data.Common;
using System.ComponentModel;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Facturador.Facturador.Modelos;
using Facturador.Facturador.Utilerias;


namespace Facturador.Facturador.Datos
{
    public class TarifaAdapter
    {
        public static BindingList<Tarifa> CargarTarifas(BaseDeDatos baseDeDatos)
        {
            baseDeDatos.Conectar();
            baseDeDatos.Preparar(TarifaConsulta);

            var tarifas = new BindingList<Tarifa>();
            var conceptos = new List<ConceptoTarifa>();
            var monedas = new List<Moneda>();

            using (var reader = baseDeDatos.RealizarConsulta())
            {
                while (reader.Read())
                {
                    var tarifa = ObtenerTarifa(reader, tarifas);
                    var concepto = ObtenerConcepto(reader, conceptos);
                    var moneda = ObtenerMoneda(reader, monedas);

                    if (tarifa!= null && concepto != null && moneda != null)
                    {
                        var entrada = ObtenerEntradaTarifa(reader, concepto);
                        entrada.Tarifa = tarifa;

                        tarifa.Entradas.Add(entrada);
                    }
                }
            }
            return tarifas;
        }

        private static Tarifa ObtenerTarifa(DbDataReader reader, BindingList<Tarifa> tarifas)
        {
            var id = BaseDeDatos.ObtenerLongOCero(reader, "TarifaId");
            if (id == 0) {
                return null;
            }
            var tarifa = tarifas.FirstOrDefault(t => t.ID == id);
            if (tarifa == null)
            {
                var nombre = BaseDeDatos.ObtenerCadenaONulo(reader, "TarifaNombre");
                tarifa = new Tarifa(id, nombre);

                tarifas.Add(tarifa);
            }
            return tarifa;
        }

        private static ConceptoTarifa ObtenerConcepto(DbDataReader reader, List<ConceptoTarifa> conceptos)
        {
            return ConceptoAdapter.ObtenerConcepto(reader, "ConceptoId", "ConceptoNombre", "ConceptoIngles",
                "ConceptoDerechos", conceptos);
        }

        private static Moneda ObtenerMoneda(DbDataReader reader, List<Moneda> monedas)
        {
            return MonedaAdapter.ObtenerMoneda(reader, "MonedaId", "MonedaDescripcion", "MonedaSimbolo",
                "MonedaSufijo", monedas);
        }

        private static EntradaTarifa ObtenerEntradaTarifa(DbDataReader reader, ConceptoTarifa concepto)
        {
            var id = BaseDeDatos.ObtenerLongOCero(reader, "EntradaId");
            var monto = BaseDeDatos.ObtenerDecimalOCero(reader, "EntradaMonto");
            var entrada = new EntradaTarifa(id, monto, concepto);

            return entrada;
        }

        public static BindingList<Tarifa> CargarNombresTarifas(BaseDeDatos baseDeDatos)
        {
            baseDeDatos.Conectar();
            baseDeDatos.Preparar("SELECT id AS TarifaId, nombre AS TarifaNombre FROM tarifa");

            var tarifas = new BindingList<Tarifa>();
            using (var reader = baseDeDatos.RealizarConsulta())
            {
                while (reader.Read())
                {
                    var tarifa = ObtenerTarifa(reader);
                    tarifas.Add(tarifa);
                }
            }
            return tarifas;
        }

        public static Tarifa ObtenerTarifa(DbDataReader reader)
        {
            return ObtenerTarifa(reader, "TarifaId", "TarifaNombre");
        }

        public static Tarifa ObtenerTarifa(DbDataReader reader, string columnaId, string columnaNombre)
        {
            var id = BaseDeDatos.ObtenerLongOCero(reader, columnaId);
            if (id == 0)
            {
                return null;
            }
            var nombre = BaseDeDatos.ObtenerCadenaONulo(reader, columnaNombre);
            var tarifa = new Tarifa(id, nombre);

            return tarifa;
        }

        public static Tarifa AgregarNuevaTarifa(string nombre, BaseDeDatos baseDeDatos)
        {
            try
            {
                baseDeDatos.Conectar();
                baseDeDatos.Preparar("INSERT INTO tarifa (nombre) VALUES (@nombre)");
                baseDeDatos.AgregarParametro("@nombre", nombre);
                baseDeDatos.Ejecutar();

                var id = baseDeDatos.LastInsertedId;
                var tarifa = new Tarifa(id, nombre);

                return tarifa;
            }
            catch (MySqlException ex)
            {
                if (ex.Number == (int)MySqlErrorCode.DuplicateKeyEntry)
                {
                    throw new Exception(string.Format("El nombre de tarifa '{0}' está duplicado", nombre), ex);
                }
                throw new Exception(string.Format("Error al agregar la nueva tarifa {0}: {1}", nombre, ex.Message), ex);
            }
        }

        public static Tarifa CargarEntradasTarifa(Tarifa tarifa, BaseDeDatos baseDeDatos)
        {
            if (tarifa.Entradas.Count > 0) {
                return tarifa;
            }
            var consulta = string.Format("{0} WHERE entrada_tarifa.tarifa_id = @TarifaId", TarifaConsulta);
            var conceptos = new List<ConceptoTarifa>();
            var monedas = new List<Moneda>();

            baseDeDatos.Conectar();
            baseDeDatos.Preparar(consulta);
            baseDeDatos.AgregarParametro("@TarifaId", tarifa.ID);

            using (var reader = baseDeDatos.RealizarConsulta())
            {
                while (reader.Read())
                {
                    var concepto = ObtenerConcepto(reader, conceptos);
                    var moneda = ObtenerMoneda(reader, monedas);

                    if (concepto != null && moneda != null)
                    {
                        if (tarifa.Moneda == null) {
                            tarifa.Moneda = moneda;
                        }
                        var entrada = ObtenerEntradaTarifa(reader, concepto);
                        entrada.Tarifa = tarifa;

                        tarifa.Entradas.Add(entrada);
                    }
                }
            }
            return tarifa;
        }

        private static readonly string TarifaConsulta = @"SELECT entrada_tarifa.id AS EntradaId, entrada_tarifa.monto AS EntradaMonto, 
	        concepto_tarifa.id AS ConceptoId, concepto_tarifa.nombre AS ConceptoNombre, concepto_tarifa.nombre_ingles AS ConceptoIngles,
	        concepto_tarifa.derechos AS ConceptoDerechos,
	        tarifa.id AS TarifaId, tarifa.nombre AS TarifaNombre,
	        moneda.MonedaId AS MonedaId, moneda.MonedaDescrip AS MonedaDescripcion, moneda.MonedaSimbolo AS MonedaSimbolo,
	        moneda.MonedaDescripSufijo AS MonedaSufijo
	        FROM tarifa
	        LEFT JOIN entrada_tarifa ON (tarifa.id = entrada_tarifa.tarifa_id)
	        LEFT JOIN concepto_tarifa ON (entrada_tarifa.concepto_id = concepto_tarifa.id)
	        LEFT JOIN moneda ON (moneda.MonedaId = tarifa.moneda_id)";

        private static readonly string TarifaInsertar = @"INSERT INTO entrada_tarifa (monto, concepto_id, tarifa_id, moneda_id)
            VALUES (@monto,  @conceptoId, @tarifaId, @monedaId)";
    }
}
