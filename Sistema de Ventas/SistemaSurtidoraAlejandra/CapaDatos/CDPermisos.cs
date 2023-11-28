using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CapaDatos
{
    public class CDPermisos
    {
        public List<Permiso> Listar(int idUsuario)
        {
            List<Permiso> lista = new List<Permiso>();

            using (SqlConnection conection = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder consulta = new StringBuilder();
                    consulta.AppendLine("select p.id_rol,p.NombreMenu from Permiso p");
                    consulta.AppendLine("inner join Rol r on r.id_Rol=p.id_rol");
                    consulta.AppendLine("inner join Usuario u on u.id_Rol=r.id_Rol");
                    consulta.AppendLine("where u.id_Usuario=@idUsuario");

                    SqlCommand terminal = new SqlCommand(consulta.ToString(), conection);
                    terminal.Parameters.AddWithValue("@idUsuario", idUsuario);
                    terminal.CommandType = CommandType.Text;

                    conection.Open();

                    using (SqlDataReader dr = terminal.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Permiso()
                            {
                                objRol = new Rol() { id_Rol = Convert.ToInt32(dr["id_rol"]) },
                                Nombre_Menu = dr["NombreMenu"].ToString(),

                            });
                        }
                    }


                }
                catch (Exception ex)
                {
                    lista = new List<Permiso>();

                }
            }

            return lista;

        }

    }
}
