using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CapaDatos
{
    public class CDMarcas
    {
        public List<Marca> Listar()
        {
            List<Marca> listamarca = new List<Marca>();

            using (SqlConnection conection = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select p.id_Marca,p.Nombre as NombreMarca,c.id_Categoria,c.Nombre as NombreCategoria,p.Estado from Marca p");
                    query.AppendLine("inner join Categoria c on c.id_Categoria=p.id_categoria");

                    SqlCommand terminal = new SqlCommand(query.ToString(), conection);
                    terminal.CommandType = CommandType.Text;

                    conection.Open();
                    using (SqlDataReader dr = terminal.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listamarca.Add(new Marca()
                            {
                                id_Marca = Convert.ToInt32(dr["id_Marca"]),
                                NombreMarca = dr["NombreMarca"].ToString(),
                                obj_categoria = new Categoria() { id_Categoria = Convert.ToInt32(dr["id_Categoria"]), Nombre = dr["NombreCategoria"].ToString() },
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    listamarca = new List<Marca>();

                }
            }
            return listamarca;

        }
        public int Registrar(Marca obj, out string Mensaje)
        {
            int idmarcaGenerada = 0;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection conection = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand terminal = new SqlCommand("Ingresar_Marca", conection);
                    terminal.Parameters.AddWithValue("@Nombre", obj.NombreMarca);
                    terminal.Parameters.AddWithValue("@idCateg", obj.obj_categoria.id_Categoria);
                    terminal.Parameters.AddWithValue("@Estado", obj.Estado);
                    terminal.Parameters.Add("@Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    terminal.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    terminal.CommandType = CommandType.StoredProcedure;
                    conection.Open();
                    terminal.ExecuteNonQuery();
                    idmarcaGenerada = Convert.ToInt32(terminal.Parameters["@Respuesta"].Value);
                    Mensaje = terminal.Parameters["@Mensaje"].Value.ToString();

                }

            }
            catch (Exception ex)
            {
                idmarcaGenerada = 0;
                Mensaje = ex.Message;

            }
            return idmarcaGenerada;
        }
        public bool Editar(Marca obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection conection = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand terminal = new SqlCommand("Editar_Marca", conection);
                    terminal.Parameters.AddWithValue("@idMarca", obj.id_Marca);
                    terminal.Parameters.AddWithValue("@Nombre", obj.NombreMarca);
                    terminal.Parameters.AddWithValue("@idCateg", obj.obj_categoria.id_Categoria);
                    terminal.Parameters.AddWithValue("@Estado", obj.Estado);
                    terminal.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    terminal.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    terminal.CommandType = CommandType.StoredProcedure;
                    conection.Open();
                    terminal.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(terminal.Parameters["@Resultado"].Value);
                    Mensaje = terminal.Parameters["@Mensaje"].Value.ToString();


                }

            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;

            }
            return respuesta;
        }

        public bool Eliminar(Marca obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection conection = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand terminal = new SqlCommand("Eliminar_Marca", conection);
                    terminal.Parameters.AddWithValue("@idMarca", obj.id_Marca);
                    terminal.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    terminal.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    terminal.CommandType = CommandType.StoredProcedure;
                    conection.Open();
                    terminal.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(terminal.Parameters["@Resultado"].Value);
                }

            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }
            return respuesta;

        }
    }
}
