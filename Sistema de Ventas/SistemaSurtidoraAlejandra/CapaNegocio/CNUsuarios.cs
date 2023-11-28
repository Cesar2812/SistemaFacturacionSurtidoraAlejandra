using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CNUsuarios
    {
        CDUsuarios obj_users = new CDUsuarios();
        //login
        public List<Usuario> Listar()
        {
            return obj_users.Listar();
        }
        public int Registrar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Nombre1 == "")
            {
                Mensaje += "Es necesario el primer nombre del Usuario\n";

            }
            if (obj.Nombre2 == "")
            {
                Mensaje += "Es necesario el segundo nombre del Usuario\n";
            }
            if (obj.Apellido1 == "")
            {
                Mensaje += "Es necesario el primer apellido del Usuario\n";
            }
            if (obj.Apellido2 == "")
            {
                Mensaje += "Es necesario el segundo apellido del Uusario\n";
            }
            if (obj.usuario == "")
            {
                Mensaje += "Es necesario el Nombre de Usuario\n";
            }
            if (obj.Correo == "")
            {
                Mensaje += "Es necesario el correo del Usuario\n";
            }
            if (obj.Clave == "")
            {
                Mensaje += "Es necesaria la clave del usuario\n";
            }
            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return obj_users.Registrar(obj, out Mensaje);

            }
        }

        public bool editar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Nombre1 == "")
            {
                Mensaje += "Es necesario el primer nombre del Usuario\n";

            }
            if (obj.Nombre2 == "")
            {
                Mensaje += "Es necesario el segundo Nombre del Usuario\n";
            }
            if (obj.Apellido1 == "")
            {
                Mensaje += "Es necesario el primer apellido del usuario\n";
            }
            if (obj.Apellido2 == "")
            {
                Mensaje += "Es ncesario el segundo apellido del usuario\n";
            }
            if (obj.usuario == "")
            {
                Mensaje += "Es necesario el Nombre de Usuario\n";
            }
            if (obj.Correo == "")
            {
                Mensaje += "Es necesario el correo del Usuario\n";
            }
            if (obj.Clave == "")
            {
                Mensaje += "Es necesaria la clave del usuario\n";
            }
            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return obj_users.Editar(obj, out Mensaje);

            }
        }
        public bool eliminar(Usuario obj, out string Mensaje)
        {
            return obj_users.Eliminar(obj, out Mensaje);
        }
    }
}
