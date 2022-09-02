using System;
using System.Data.Common;
using System.ComponentModel;
using Facturador.Facturador.Modelos;
using Facturador.Facturador.Utilerias;

namespace Facturador.Facturador.Datos
{
    public class CasoAdapter
    {
        public static Caso BuscarCaso(long id, Grupo grupo, BaseDeDatos baseDeDatos)
        {
            var tabla = ObtenerNombreTabla(grupo);
            if (string.IsNullOrEmpty(tabla)) {
                return null;
            }
            var consulta = string.Format(CasoConsulta, tabla);

            baseDeDatos.Conectar();
            baseDeDatos.Preparar(consulta);
            baseDeDatos.AgregarParametro("@CasoNumero", id);

            using (var reader = baseDeDatos.RealizarConsulta())
            {
                while (reader.Read())
                {
                    var caso = ObtenerCaso(reader);
                    caso.Referencia = ReferenciaAdapter.Obtener(reader);
                    caso.Cliente = ClienteAdapter.ObtenerCliente(reader);
                    caso.Cliente.Tarifa = TarifaAdapter.ObtenerTarifa(reader, "ClienteTarifaId", "ClienteTarifaNombre");
                    caso.Cliente.Moneda = MonedaAdapter.ObtenerMoneda(reader, "ClienteMonedaId", "ClienteMonedaDesc",
                        "ClienteMonedaSufijo", "ClienteMonedaSimbolo");
                    //caso.Interesado = InteresadoAdapter.ObtenerInteresado(reader);
                    caso.Interesado.Tarifa = TarifaAdapter.ObtenerTarifa(reader, "InteresadoTarifaId", "InteresadoTarifaNombre");
                    caso.Interesado.Moneda = MonedaAdapter.ObtenerMoneda(reader, "InteresadoMonedaId", "InteresadoMonedaDesc",
                        "InteresadoMonedaSufijo", "InteresadoMonedaSimbolo");
                    caso.Tarifa = TarifaAdapter.ObtenerTarifa(reader);
                    caso.Moneda = MonedaAdapter.ObtenerMoneda(reader);
                    caso.TipoSolicitud = TipoSolicitudAdapter.Obtener(reader);

                    return caso;
                }
            }
            return null;
        }

        private static string ObtenerNombreTabla(Grupo grupo)
        {
            switch(grupo.Descripcion.ToLower())
            {
                case "patentes":
                    return "caso_patente";

                case "marcas":
                    return "caso_marcas";

                default:
                    return string.Empty;
            }
        }

        private static Caso ObtenerCaso(DbDataReader reader)
        {
            var id = BaseDeDatos.ObtenerLongOCero(reader, "CasoId");
            var caso = new Caso(id);

            caso.Titulo = BaseDeDatos.ObtenerCadenaONulo(reader, "CasoTituloespanol");
            caso.TituloIngles = BaseDeDatos.ObtenerCadenaONulo(reader, "CasoTituloingles");
            caso.Numero = BaseDeDatos.ObtenerIntOCero(reader, "CasoNumero");
            caso.NumeroExpediente = BaseDeDatos.ObtenerCadenaONulo(reader, "CasoNumeroExpedienteLargo");

            return caso;
        }

        public static int AsignarTarifa(Caso caso, Tarifa tarifa, BaseDeDatos baseDeDatos)
        {
            baseDeDatos.Conectar();
            baseDeDatos.Preparar(TarifaInsertar);
            baseDeDatos.AgregarParametro("@CasoId", caso.ID);
            baseDeDatos.AgregarParametro("@TipoSolicitud", caso.TipoSolicitud.ID);
            baseDeDatos.AgregarParametro("@TarifaId", tarifa.ID);

            var afectados = baseDeDatos.Ejecutar();
            caso.Tarifa = tarifa;
            baseDeDatos.LimpiarParametros();

            return afectados;
        }

        private static readonly string CasoConsulta = @"SELECT
            caso.CasoId AS CasoId,
            caso.CasoTituloespanol AS CasoTituloespanol, caso.CasoTituloingles AS CasoTituloingles,
            caso.CasoNumero AS CasoNumero, caso.CasoNumeroExpedienteLargo AS CasoNumeroExpedienteLargo,
            referencia.ReferenciaId AS ReferenciaId, referencia.ReferenciaNombre AS ReferenciaNombre,
            cliente.ClienteId AS ClienteId, cliente.ClienteNombre AS ClienteNombre, 
            tarifa.id AS TarifaId, tarifa.nombre AS TarifaNombre,
            tar_cliente.id AS ClienteTarifaId, tar_cliente.nombre AS ClienteTarifaNombre,
            tar_interesado.id AS InteresadoTarifaId, tar_interesado.nombre AS InteresadoTarifaNombre,
            interesado.InteresadoID AS InteresadoID, interesado.InteresadoNombre AS InteresadoNombre,
            interesado.InteresadoShort AS InteresadoShort, interesado.InteresadoApPaterno AS InteresadoApPaterno,
            interesado.InteresadoApMaterno AS InteresadoApMaterno,
            moneda.MonedaId AS MonedaId, moneda.MonedaDescrip AS MonedaDescripcion, moneda.MonedaDescripSufijo AS MonedaSufijo,
            moneda.MonedaSimbolo AS MonedaSimbolo,
            mon_cliente.MonedaId AS ClienteMonedaId, mon_cliente.MonedaDescrip AS ClienteMonedaDesc,
            mon_cliente.MonedaDescripSufijo AS ClienteMonedaSufijo, mon_cliente.MonedaSimbolo AS ClienteMonedaSimbolo,
            mon_interesado.MonedaId AS InteresadoMonedaId, mon_interesado.MonedaDescrip AS InteresadoMonedaDesc,
            mon_interesado.MonedaDescripSufijo AS InteresadoMonedaSufijo, mon_interesado.MonedaSimbolo AS InteresadoMonedaSimbolo,
            tipo.TipoSolicitudId AS TipoSolicitudId, tipo.TipoSolicitudDescrip AS TipoSolicitudDescrip, tipo.TipoSolicitudDescripI AS TipoSolicitudDescripI,
            tipo.TipoSolicitudVigencia AS TipoSolicitudVigencia,
            grupo.GrupoId AS GrupoId, grupo.GrupoDescripcion AS GrupoDescripcion
            FROM {0} caso
            LEFT JOIN referencia ON (referencia.CasoId = caso.CasoId)
            JOIN casocliente ON (caso.CasoId = casocliente.CasoId)
            JOIN cliente ON (casocliente.ClienteId = cliente.ClienteId)
            LEFT JOIN tarifa_caso ON (caso.CasoId = tarifa_caso.caso_id AND caso.TipoSolicitudId = tarifa_caso.tipo_solicitud_id)
            LEFT JOIN tarifa ON (tarifa_caso.tarifa_id = tarifa.id)
            LEFT JOIN moneda ON (tarifa.moneda_id = moneda.MonedaId)
            LEFT JOIN tarifa tar_cliente ON (cliente.TipoTarifaId = tar_cliente.id)
            LEFT JOIN moneda mon_cliente ON (tar_cliente.moneda_id = mon_cliente.MonedaId)
            JOIN casointeresado ON (caso.CasoId = casointeresado.CasoId)
            JOIN interesado ON (casointeresado.InteresadoId = interesado.InteresadoID)
            LEFT JOIN tarifa_interesado ON (tarifa_interesado.interesado_id = interesado.InteresadoID)
            LEFT JOIN tarifa tar_interesado ON (tarifa_interesado.tarifa_id = tar_interesado.id)
            LEFT JOIN moneda mon_interesado ON (tar_interesado.moneda_id = mon_interesado.MonedaId)
            JOIN tiposolicitud tipo ON (caso.TipoSolicitudId = tipo.TipoSolicitudId)
            JOIN grupo ON (tipo.TipoSolicitudGrupo = grupo.GrupoId)
            WHERE caso.CasoNumero = @CasoNumero LIMIT 1";

        private static readonly string TarifaInsertar = @"INSERT INTO caso_tarifa (caso_id, tipo_solicitud_id, tarifa_id)
            VALUES (@CasoId, @TipoSolicitud, @TarifaId) ON DUPLICATE KEY UPDATE tarifa_id = @TarifaId";

        private static readonly string MonedaInsertar = @"INSERT INTO caso_moneda (caso_id, tipo_solicitud_id, moneda_id)
            VALUES (@CasoId, @TipoSolicitud, @MonedaId) ON DUPLICATE KEY UPDATE moneda_id = @MonedaId";
    }
}
