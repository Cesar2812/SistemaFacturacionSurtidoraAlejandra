using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CNReportes
    {
        private CDReporteCompras objReporte = new CDReporteCompras();
        private CDReporteVentas objVenta = new CDReporteVentas();

        public List<ReporteCompra> Compra(string fechaCreacion, string fechaFin, int idproveedor)
        {
            return objReporte.Listar(fechaCreacion, fechaFin, idproveedor);
        }

        public List<ReporteVenta> Venta(string fechaCreacion, string fechaFin, int idcliente)
        {
            return objVenta.Listar(fechaCreacion, fechaFin, idcliente);
        }
    }
}
