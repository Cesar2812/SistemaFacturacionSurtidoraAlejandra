using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CNRol
    {
        CDRol obj_rol = new CDRol();
        //permisos de roles
        public List<Rol> Listar()
        {
            return obj_rol.Listar();
        }

    }
}
