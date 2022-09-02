using System;

namespace Facturador.Facturador.Modelos
{
    public abstract class ModeloBase
    {
        public ModeloBase(long id, Estatus estatus)
        {
            ID = id;
            Estatus = estatus;
        }

        public long ID { get; set; }

        public Estatus Estatus { get; set; }
    }
}
