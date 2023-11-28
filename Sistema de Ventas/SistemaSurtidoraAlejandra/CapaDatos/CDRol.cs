using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CapaDatos
{
    public class CDRol
    {
        public List<Rol> Listar()
        {
            List<Rol> lista = new List<Rol>();

            using (SqlConnection conection = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder consulta = new StringBuilder();
                    consulta.AppendLine("select id_Rol,Descripcion from Rol");


                    SqlCommand terminal = new SqlCommand(consulta.ToString(), conection);
                    terminal.CommandType = CommandType.Text;

                    conection.Open();

                    using (SqlDataReader dr = terminal.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Rol()
                            {
                                id_Rol = Convert.ToInt32(dr["id_Rol"]),
                                Descripcion = dr["Descripcion"].ToString()

                            }); ;
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Rol>();

                }
            }

            return lista;

        }
    }
}
