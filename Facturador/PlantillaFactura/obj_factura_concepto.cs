using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturador.PlantillaFactura
{
    class obj_factura_concepto
    {

        public String concepto1 { get; set; }
        public String conceptohonorarios { get; set; }
        public String conceptoderechos { get; set; }
        public String simportehono { get; set; }
        public String simportederecho { get; set; }

        public obj_factura_concepto(String concepto, String importehono, String importederecho)
        {
            concepto1 = concepto;
            conceptohonorarios = "Service Charge (Honorarios)";
            conceptoderechos = "Official Fee (Derechos)";
            simportehono = importehono;
            simportederecho = importederecho;
        }

        

    }
}
