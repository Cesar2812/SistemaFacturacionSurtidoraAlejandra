using System.Collections.Generic;

namespace CapaEntidad
{
    public class Compra
    {
        public int id_Compra { get; set; }
        public Usuario od_usuario { get; set; }
        public Proveedor od_Proveedor { get; set; }
        public string TipoDeDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public decimal MontoTotal { get; set; }
        public List<DetalleCompra> obDetalle { get; set; }
        public string FechaCreacion { get; set; }
    }
}
