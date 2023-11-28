using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;


namespace CapaNegocio
{
    public class CNProveedor
    {
        CDProvedores obj_prov = new CDProvedores();
        public List<Proveedor> Listar()
        {
            return obj_prov.Listar();
        }
        //registro de Proveedores
        public int Registrar(Proveedor obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Documento == "")
            {
                Mensaje += "Es necesario el Documento del Proveedor\n";

            }
            if (obj.RazonSocial == "")
            {
                Mensaje += "Es necesaria la Razon social del Proveedor\n";
            }
            if (obj.Nombre == "")
            {
                Mensaje += "Es necesario el Nombre del Proveedor\n";
            }
            if (obj.Correo == "")
            {
                Mensaje += "Es necesario el correo  del Proveedor\n";
            }
            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return obj_prov.Registrar(obj, out Mensaje);

            }
        }
        //edicion de proveedor
        public bool editar(Proveedor obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Documento == "")
            {
                Mensaje += "Es necesario el Documento del Proveedor\n";

            }
            if (obj.RazonSocial == "")
            {
                Mensaje += "Es necesaria la Razon social del Proveedor\n";
            }
            if (obj.Nombre == "")
            {
                Mensaje += "Es neceario el Nombre del Proveedor\n";
            }
            if (obj.Correo == "")
            {
                Mensaje += "Es necesario el correo  del Proveedor\n";
            }
            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return obj_prov.Editar(obj, out Mensaje);

            }
        }
        //eliminacion del Proveedor
        public bool eliminar(Proveedor obj, out string Mensaje)
        {
            return obj_prov.Eliminar(obj, out Mensaje);
        }

    }
}
