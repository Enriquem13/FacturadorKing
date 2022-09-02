using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturador.PlantillaFactura
{
    class obj_factura_pdf
    {
        public String Cliente_nombre { get; set; }
        public String Direccionuno { get; set; }
        public String Direcciondos { get; set; }
        public String Direccion_tres { get; set; }
        public String Direccion_cuatro { get; set; }
        public String RFC_cliente { get; set; }
        public String Nota_debitoNo { get; set; }
        public String Fecha_factura { get; set; }
        public String ExpNum { get; set; }
        public String NuestraRef { get; set; }
        public String referenciacliente { get; set; }
        public String cliente_id { get; set; }
        public String usuario { get; set; }
        public String servicio { get; set; }
        public String Subtotal { get; set; }
        public String Iva { get; set; }
        public String Total { get; set; }
        public String importe_letra_esp { get; set; }
        public String importe_letra_eng { get; set; }
        public List<obj_factura_concepto> conceptos;
        
        public List<obj_factura_concepto> conceptosfac;
        public obj_factura_pdf(String sCliente_nombre, String sNota_debitoNo, String scliente_id, String susuario, String sservicio, List<obj_factura_concepto> listaconceptos){
            
            cliente_id = scliente_id;
            Cliente_nombre = sCliente_nombre;
            Nota_debitoNo = sNota_debitoNo;
            servicio = sservicio;
            usuario = susuario;
            conceptos = listaconceptos;

            Direccionuno = "";
            Direcciondos = "";
            Direccion_tres = "";
            Direccion_cuatro = "";
            RFC_cliente = "";
            
            Fecha_factura = "";
            ExpNum = "";
            NuestraRef = "";
            referenciacliente = "";
            
            Subtotal = "";
            Iva = "";
            Total = "";

            importe_letra_esp = "";
            importe_letra_eng = "";
        }

    }
}
