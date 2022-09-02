using System;
using System.Data.Common;
using System.ComponentModel;
using Facturador.Facturador.Modelos;
using Facturador.Facturador.Datos;
using Facturador.Facturador.Utilerias;

namespace Facturador.Facturador.Datos
{
    public class ClienteAdapter
    {
        public static Modelos.Cliente BuscarCliente(long id, BaseDeDatos baseDeDatos) {
            return BuscarClienteImpl(baseDeDatos, ConsultaCliente, "cliente.ClienteId = @Cliente", id);
        }

        public static Modelos.Cliente BuscarCliente(string nombre, BaseDeDatos baseDeDatos)
        {
            var parametro = string.Format("{0}{1}{2}", "%", nombre, "%");
            return BuscarClienteImpl(baseDeDatos, ConsultaCliente, "cliente.ClienteNombre LIKE @Cliente", parametro);
        }

        private static Modelos.Cliente BuscarClienteImpl<T>(BaseDeDatos baseDeDatos, string consulta, string comparador, T parametro)
        {
            string consultaFinal = string.Format(consulta, comparador);

            baseDeDatos.Conectar();
            baseDeDatos.Preparar(consultaFinal);
            baseDeDatos.AgregarParametro("@Cliente", parametro);

            using (var reader = baseDeDatos.RealizarConsulta())
            {
                while (reader.Read())
                {
                    var moneda = MonedaAdapter.ObtenerMoneda(reader);
                    var tarifa = TarifaAdapter.ObtenerTarifa(reader);
                    var cliente = ObtenerCliente(reader);

                    cliente.Moneda = moneda;
                    cliente.Tarifa = tarifa;

                    return cliente;
                }
            }
            return null;
        }

        public static Modelos.Cliente ObtenerCliente(DbDataReader reader)
        {
            var id = BaseDeDatos.ObtenerLongOCero(reader, "ClienteId");
            var nombre = BaseDeDatos.ObtenerCadenaONulo(reader, "ClienteNombre");
            var cliente = new Modelos.Cliente(id, nombre);

            return cliente;
        }

        public static int AsignarTarifa(Modelos.Cliente cliente, Tarifa tarifa, BaseDeDatos baseDatos)
        {
            baseDatos.Conectar();
            baseDatos.Preparar("UPDATE cliente SET TipoTarifaId = @TarifaId WHERE ClienteId = @ClienteId");
            baseDatos.AgregarParametro("@TarifaId", tarifa.ID);
            baseDatos.AgregarParametro("@ClienteId", cliente.ID);

            var afectados = baseDatos.Ejecutar();
            cliente.Tarifa = tarifa;

            baseDatos.LimpiarParametros();

            return afectados;
        }

        public static int AsignarMoneda(Modelos.Cliente cliente, Moneda moneda, BaseDeDatos baseDatos)
        {
            baseDatos.Conectar();
            baseDatos.Preparar("UPDATE cliente SET MonedaId = @MonedaId WHERE ClienteId = @ClienteId");
            baseDatos.AgregarParametro("@MonedaId", moneda.ID);
            baseDatos.AgregarParametro("@ClienteId", cliente.ID);

            var afectados = baseDatos.Ejecutar();
            cliente.Moneda = moneda;

            baseDatos.LimpiarParametros();

            return afectados;
        }

        private static readonly string ConsultaCliente = @"SELECT cliente.ClienteId as ClienteId,
            cliente.ClienteNombre as ClienteNombre,  moneda.MonedaId AS MonedaId, moneda.MonedaDescrip AS MonedaDescripcion, 
            moneda.MonedaSimbolo AS MonedaSimbolo, moneda.MonedaDescripSufijo AS MonedaSufijo, tarifa.id AS TarifaId,
            tarifa.nombre AS TarifaNombre FROM cliente
            LEFT JOIN moneda ON (cliente.MonedaId = moneda.MonedaId)
            LEFT JOIN tarifa ON (cliente.TipoTarifaId = tarifa.id) WHERE {0} LIMIT 1";
    }
}
