using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CNProducto
    {
        CDProductos obj_prod = new CDProductos();
        //login
        public List<Producto> Listar()
        {
            return obj_prod.Listar();
        }

        public int Registrar(Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Codigo == "")
            {
                Mensaje += "Es necesario el Codigo del Producto\n";

            }
            if (obj.Nombre_Producto == "")
            {
                Mensaje += "Es necesario el nombre del Producto\n";
            }
            if (obj.Descripcion == "")
            {
                Mensaje += "Es necesaria la descripcion del Producto\n";
            }
            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return obj_prod.Registrar(obj, out Mensaje);

            }
        }

        public bool editar(Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Nombre_Producto == "")
            {
                Mensaje += "Es necesario el Nombre del Producto\n";

            }
            if (obj.Codigo == "")
            {
                Mensaje += "Es necesario el Codigo del Producto\n";
            }
            if (obj.Descripcion == "")
            {
                Mensaje += "Es necesaria la descripcion del Producto\n";
            }
            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return obj_prod.Editar(obj, out Mensaje);

            }
        }
        public bool eliminar(Producto obj, out string Mensaje)
        {
            return obj_prod.Eliminar(obj, out Mensaje);
        }
    }
}
