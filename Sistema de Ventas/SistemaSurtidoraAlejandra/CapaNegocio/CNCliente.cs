using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CNCliente
    {
        CDCliente obj_cliente = new CDCliente();
        public List<Cliente> Listar()
        {
            return obj_cliente.Listar();
        }
        //registro de clientes
        public int Registrar(Cliente obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Cedula == "")
            {
                Mensaje += "Es necesario el numero de cedula del Cliente\n";

            }
            if (obj.Nombre1 == "")
            {
                Mensaje += "Es necesaria el primer nombre del Cliente\n";
            }
            if (obj.Nombre2 == "")
            {
                Mensaje += "Es necesario el segundo nombre del Cliente\n";
            }
            if (obj.Apellido1 == "")
            {
                Mensaje += "Es necesario el primer apellido del Cliente\n";
            }
            if (obj.Apellido2 == "")
            {
                Mensaje += "Es necesario el segundo apellido del Cliente\n";
            }
            if (obj.Telefono == "")
            {
                Mensaje += "Es necesario el numero de telefono del Cliente\n";
            }
            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return obj_cliente.Registrar(obj, out Mensaje);

            }
        }

        //edicion de proveedor
        public bool editar(Cliente obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Cedula == "")
            {
                Mensaje += "Es necesario el numero de cedula del cliente\n";

            }
            if (obj.Nombre1 == "")
            {
                Mensaje += "Es necesario el primer nombre del Cliente\n";
            }
            if (obj.Nombre2 == "")
            {
                Mensaje += "Es neceario el el segundo nombre del Cliente\n";
            }
            if (obj.Apellido1 == "")
            {
                Mensaje += "Es necesario el primer Apellido del Cliente\n";
            }
            if (obj.Apellido2 == "")
            {
                Mensaje += "Es necesario el segundo apellido del Cliente\n";
            }
            if (obj.Telefono == "")
            {
                Mensaje += "Es necesario el telefono del cliente\n";
            }
            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return obj_cliente.Editar(obj, out Mensaje);

            }
        }

        //eliminacion del cliente
        public bool eliminar(Cliente obj, out string Mensaje)
        {
            return obj_cliente.Eliminar(obj, out Mensaje);
        }

    }
}
