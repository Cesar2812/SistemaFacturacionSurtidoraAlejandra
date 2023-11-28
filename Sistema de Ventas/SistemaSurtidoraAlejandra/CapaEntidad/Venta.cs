using System.Collections.Generic;

namespace CapaEntidad
{
    public class Venta
    {
        public int id_Venta { get; set; }
        public Usuario obj_user { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public Cliente obj_Cliente { get; set; }
        public decimal MontoPago { get; set; }
        public decimal MontoCambio { get; set; }
        public decimal MontoTotal { get; set; }
        public List<DetalleVenta> obDetalle { get; set; }
        public string FechaRegistro { get; set; }
    }
}
