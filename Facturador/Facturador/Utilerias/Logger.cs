using System;
using System.IO;
using System.Text;

namespace Facturador.Facturador.Utilerias
{
    public class Logger
    {
        public Logger() {
            CrearDirectorioParaLog();
        }

        public void Error(params string[] lista)
        {
            if (string.IsNullOrEmpty(_ruta)) {
                return;
            }
            if (lista.Length == 0) {
                return;
            }
            try
            {
                var builder = new StringBuilder();
                builder.Append(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

                foreach(var info in lista) {
                    builder.Append(info);
                }
                File.AppendAllText(_ruta + "casosking.log", builder.ToString());
            }
            catch
            {
                // Si llegamos aquí, ya no se puede hacer nada :(
            }
        }

        private void CrearDirectorioParaLog()
        {
            try
            {
                if (string.IsNullOrEmpty(_ruta))
                {
                    _ruta = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + @"\casosking";

                    if (!Directory.Exists(_ruta)) {
                        Directory.CreateDirectory(_ruta);
                    }
                }
            }
            catch
            {
                // Si llegamos aquí, ya no se puede hacer nada :(
            }
        }

        private static string _ruta;
    }
}
