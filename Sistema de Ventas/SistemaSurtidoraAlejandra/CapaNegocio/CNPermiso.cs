using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CNPermiso
    {
        CDPermisos obj_permisos = new CDPermisos();
        //permisos de roles
        public List<Permiso> Listar(int id_user)
        {
            return obj_permisos.Listar(id_user);
        }

    }
}
