using System;

namespace Facturador.Facturador.Modelos
{
    public class ConceptoTarifa : ModeloBase
    {
        public ConceptoTarifa(long id, string nombre, string nombreIngles, Estatus estatus = Estatus.Limpio)
            : base(id, estatus)
        {
            Nombre = nombre;
            NombreIngles = nombreIngles;
        }

        public string Nombre { get; set; }

        public string NombreIngles { get; set; }

        public decimal Derechos { get; set; }
    }
}
