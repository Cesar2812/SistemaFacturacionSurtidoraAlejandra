using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;
using System.Data;

namespace CapaNegocio
{
    public class CNCompra
    {
        private CDCompra objCompras = new CDCompra();
        public int obtenerCorrelativo()
        {
            return objCompras.obtnerCorrerlativo();
        }
        public bool Registrar(Compra obj, DataTable DetalleCompra, out string Mensaje)
        {
            return objCompras.Registrar(obj, DetalleCompra, out Mensaje);
        }
        public Compra obtnerCompra(string numero)
        {
            Compra oCompra = objCompras.ObtnerCompra(numero);

            if (oCompra.id_Compra != 0)
            {
                List<DetalleCompra> oDetalle = objCompras.obtenerDetail(oCompra.id_Compra);

                oCompra.obDetalle = oDetalle;
            }
            return oCompra;
        }


    }
}
