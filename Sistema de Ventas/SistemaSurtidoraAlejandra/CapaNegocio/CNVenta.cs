using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;
using System.Data;

namespace CapaNegocio
{
    public class CNVenta
    {
        private CDVenta objventa = new CDVenta();

        public int obtenerCorrelativo()
        {
            return objventa.obtnerCorrerlativo();
        }
        public bool Registrar(Venta obj, DataTable DetalleVenta, out string Mensaje)
        {
            return objventa.RegistrarVenta(obj, DetalleVenta, out Mensaje);
        }
        public bool restarStok(int idproducto, int cantidad)
        {
            return objventa.RestarStocK(idproducto, cantidad);
        }
        public bool sumarStock(int idproducto, int cantidad)
        {
            return objventa.SumarStocK(idproducto, cantidad);
        }
        public Venta obtnerVenta(string numero)
        {
            Venta oventa = objventa.obtenerVenta(numero);

            if (oventa.id_Venta != 0)
            {
                List<DetalleVenta> oDetalle = objventa.obtenerDetail(oventa.id_Venta);

                oventa.obDetalle = oDetalle;
            }
            return oventa;
        }
    }
}
