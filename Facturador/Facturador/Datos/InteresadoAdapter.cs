using System;
using System.Data.Common;
using System.ComponentModel;
using Facturador.Facturador.Modelos;
using Facturador.Facturador.Utilerias;

namespace Facturador.Facturador.Datos
{
    public class InteresadoAdapter
    {
        //public static Interesado BuscarInteresado(long id, BaseDeDatos baseDeDatos) {
        //    return BuscarInteresado(baseDeDatos, "interesado.InteresadoID = @Interesado", id);
        //}

        //public static Interesado BuscarInteresado(string nombre, BaseDeDatos baseDeDatos)
        //{
        //    var parametro = string.Format("{0}{1}{2}", "%", nombre, "%");
        //    return BuscarInteresado(baseDeDatos, "interesado.InteresadoShort LIKE @Interesado", parametro);
        //}

        //private static Interesado BuscarInteresado<T>(BaseDeDatos baseDeDatos, string comparador, T parametro)
        //{
        //    string consultaFinal = string.Format(InteresadoConsulta, comparador);

        //    baseDeDatos.Conectar();
        //    baseDeDatos.Preparar(consultaFinal);
        //    baseDeDatos.AgregarParametro("@Interesado", parametro);

        //    using (var reader = baseDeDatos.RealizarConsulta())
        //    {
        //        while (reader.Read())
        //        {
        //            var moneda = MonedaAdapter.ObtenerMoneda(reader);
        //            var tarifa = TarifaAdapter.ObtenerTarifa(reader);
        //            var interesado = ObtenerInteresado(reader);

        //            interesado.Moneda = moneda;
        //            interesado.Tarifa = tarifa;

        //            return interesado;
        //        }
        //    }
        //    return null;
        //}

        //public static Interesado ObtenerInteresado(DbDataReader reader)
        //{
        //    var id = BaseDeDatos.ObtenerLongOCero(reader, "InteresadoID");
        //    var nombre = BaseDeDatos.ObtenerCadenaONulo(reader, "InteresadoNombre");
        //    var paterno = BaseDeDatos.ObtenerCadenaONulo(reader, "InteresadoApPaterno");
        //    var materno = BaseDeDatos.ObtenerCadenaONulo(reader, "InteresadoApMaterno");
        //    var corto = BaseDeDatos.ObtenerCadenaONulo(reader, "InteresadoShort");
        //    var interesado = new Interesado(id, nombre);

        //    interesado.ApellidoPaterno = paterno;
        //    interesado.ApellidoMaterno = materno;
        //    interesado.NombreCorto = corto;

        //    return interesado;
        //}

        //public static int AsignarTarifa(Interesado interesado, Tarifa tarifa, BaseDeDatos baseDeDatos)
        //{
        //    baseDeDatos.Conectar();
        //    baseDeDatos.Preparar(TarifaConsulta);
        //    baseDeDatos.AgregarParametro("@InteresadoId", interesado.ID);
        //    baseDeDatos.AgregarParametro("@TarifaId", tarifa.ID);

        //    var afectados = baseDeDatos.Ejecutar();
        //    interesado.Tarifa = tarifa;
        //    baseDeDatos.LimpiarParametros();

        //    return afectados;
        //}

        //public static int AsignarMoneda(Interesado interesado, Moneda moneda, BaseDeDatos baseDeDatos)
        //{
        //    baseDeDatos.Conectar();
        //    baseDeDatos.Preparar(MonedaConsulta);
        //    baseDeDatos.AgregarParametro("@InteresadoId", interesado.ID);
        //    baseDeDatos.AgregarParametro("@MonedaId", moneda.ID);

        //    var afectados = baseDeDatos.Ejecutar();
        //    interesado.Moneda = moneda;
        //    baseDeDatos.LimpiarParametros();

        //    return afectados;
        //}

        //private static readonly string InteresadoConsulta = @"SELECT interesado.InteresadoID AS InteresadoID,
        //    interesado.InteresadoNombre AS InteresadoNombre, interesado.InteresadoApPaterno AS InteresadoApPaterno,
        //    interesado.InteresadoApMaterno AS InteresadoApMaterno, interesado.InteresadoShort AS InteresadoShort,
        //    moneda.MonedaId AS MonedaId, moneda.MonedaDescrip AS MonedaDescrip, 
        //    moneda.MonedaSimbolo AS MonedaSimbolo, moneda.MonedaDescripSufijo AS MonedaDescripSufijo,
        //    tarifa.id AS TarifaId, tarifa.nombre AS TarifaNombre FROM interesado
        //    LEFT JOIN moneda_interesado ON (moneda_interesado.interesado_id = interesado.InteresadoID)
        //    LEFT JOIN moneda ON (moneda.MonedaId = moneda_interesado.moneda_id)
        //    LEFT JOIN tarifa_interesado ON (tarifa_interesado.interesado_id = interesado.InteresadoID)
        //    LEFT JOIN tarifa ON (tarifa_interesado.tarifa_id = tarifa.id)
        //    WHERE {0} LIMIT 1;";

        //private static readonly string TarifaConsulta = @"INSERT INTO tarifa_interesado (tarifa_id, interesado_id)
        //    VALUES (@TarifaId, @InteresadoId) ON DUPLICATE KEY UPDATE tarifa_id = @TarifaId";

        //private static readonly string MonedaConsulta = @"INSERT INTO moneda_interesado (moneda_id, interesado_id)
        //    VALUES (@MonedaId, @InteresadoId) ON DUPLICATE KEY UPDATE moneda_id = @MonedaId";
    }
}
