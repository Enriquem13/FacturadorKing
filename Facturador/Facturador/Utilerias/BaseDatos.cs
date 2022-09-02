using System;
using System.IO;
using System.Text;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace Facturador.Facturador.Utilerias
{
    public class BaseDeDatos
    {
        public BaseDeDatos()
        {
            GenerarCadenaConexion();

            _logger = new Logger();
            _conexion = new MySqlConnection(_cadena);
            _comando = new MySqlCommand();
        }

        public void Conectar()
        {
            if (_conexion.State == ConnectionState.Closed) {
                _conexion.Open();
            }
        }

        public void Desconectar() {
            _conexion.Close();
        }

        public void Preparar(string consulta)
        {
            if (string.IsNullOrEmpty(consulta)) {
                throw new ArgumentNullException("La consulta especificada no es válida");
            }
            Conectar();
            _comando.Connection = _conexion;
            _comando.CommandText = consulta;

            LimpiarParametros();
            _comando.Prepare();
        }

        public void AgregarParametro<T>(string parametro, T valor) {
            _comando.Parameters.AddWithValue(parametro, valor);
        }

        public void LimpiarParametros() {
            _comando.Parameters.Clear();
        }

        public int Ejecutar() {
            return _comando.ExecuteNonQuery();
        }

        public long LastInsertedId {
            get { return _comando.LastInsertedId; }
        }

        public DbDataReader RealizarConsulta() {
            return _comando.ExecuteReader();
        }

        public static string ObtenerCadenaONulo(DbDataReader reader, string nombreColumna)
        {
            var ordinal = reader.GetOrdinal(nombreColumna);

            if (!reader.IsDBNull(ordinal)) {
                return reader.GetString(ordinal);
            }
            return string.Empty;
        }

        public static int ObtenerIntOCero(DbDataReader reader, string nombreColumna)
        {
            var ordinal = reader.GetOrdinal(nombreColumna);

            if (!reader.IsDBNull(ordinal)) {
                return reader.GetInt32(ordinal);
            }
            return 0;
        }

        public static int? ObtenerIntONulo(DbDataReader reader, string nombreColumna)
        {
            var numero = ObtenerIntOCero(reader, nombreColumna);
            if (numero > 0) {
                return numero;
            }
            return null;
        }

        public static long ObtenerLongOCero(DbDataReader reader, string nombreColumna)
        {
            var ordinal = reader.GetOrdinal(nombreColumna);

            if (!reader.IsDBNull(ordinal)) {
                return reader.GetInt64(ordinal);
            }
            return 0;
        }

        public static decimal ObtenerDecimalOCero(DbDataReader reader, string nombreColumna)
        {
            var ordinal = reader.GetOrdinal(nombreColumna);

            if (!reader.IsDBNull(ordinal)) {
                return reader.GetDecimal(ordinal);
            }
            return 0;
        }

        private void GenerarCadenaConexion()
        {
            if (string.IsNullOrEmpty(_cadena))
            {
                var builder = new StringBuilder();

                builder.Append(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
                builder.Append(@"\casosking\confacturador.prop");

                var fichero = builder.ToString();
                if (File.Exists(fichero))
                {
                    var contenido = File.ReadAllText(fichero);
                    var lineas = contenido.Split('\n');

                    builder.Clear();
                    builder.Append("server=");
                    builder.Append(lineas[0]);
                    builder.Append(";database=");
                    builder.Append(lineas[1]);
                    builder.Append(";Uid=");
                    builder.Append(lineas[2]);
                    builder.Append(";pwd=");
                    builder.Append(lineas[3]);
                    builder.Append(";");

                    _cadena = builder.ToString();
                }
                else
                {
                    var mensaje = "El archivo de configuración 'confacturador.prop' no fue encontrado o no se pudo abrir";
                    _logger.Error("Class: BaseDatos.", "Error: ", mensaje, " Valores conexión: ", _cadena, "\n");

                    throw new Exception(mensaje);
                }
            }
        }

        private MySqlConnection _conexion;
        private MySqlCommand _comando;
        private static string _cadena;
        private Logger _logger;
    }
}
