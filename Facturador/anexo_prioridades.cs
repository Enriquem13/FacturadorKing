using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturador
{
    class anexo_prioridades
    {
        public String sReferencia { get; set; }
        public String sPais { get; set; }
        public String sNumprioridad { get; set; }
        public String sFecha { get; set; }
        public anexo_prioridades(String referencia) {
            sReferencia = referencia;
        }
    }
}
