using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CapaDatos
{
    public class CDCategorias
    {
        public List<Categoria> Listar()
        {
            List<Categoria> lista = new List<Categoria>();

            using (SqlConnection conection = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select id_Categoria,Nombre,Estado from Categoria");

                    SqlCommand terminal = new SqlCommand(query.ToString(), conection);
                    terminal.CommandType = CommandType.Text;

                    conection.Open();
                    using (SqlDataReader dr = terminal.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Categoria()
                            {
                                id_Categoria = Convert.ToInt32(dr["id_Categoria"]),
                                Nombre = dr["Nombre"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            }); ;
                        }
                    }


                }
                catch (Exception ex)
                {
                    lista = new List<Categoria>();

                }
            }

            return lista;

        }
        public int Registrar(Categoria obj, out string Mensaje)
        {
            int idCategoriaGenerada = 0;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection conection = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand terminal = new SqlCommand("Ingresar_Categoria", conection);
                    terminal.Parameters.AddWithValue("@Nombre", obj.Nombre);
                    terminal.Parameters.AddWithValue("@Estado", obj.Estado);
                    terminal.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    terminal.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    terminal.CommandType = CommandType.StoredProcedure;
                    conection.Open();
                    terminal.ExecuteNonQuery();

                    idCategoriaGenerada = Convert.ToInt32(terminal.Parameters["@Resultado"].Value);
                    Mensaje = terminal.Parameters["@Mensaje"].Value.ToString();


                }

            }
            catch (Exception ex)
            {
                idCategoriaGenerada = 0;
                Mensaje = ex.Message;

            }


            return idCategoriaGenerada;
        }
        public bool Editar(Categoria obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection conection = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand terminal = new SqlCommand("Editar_Categoria", conection);
                    terminal.Parameters.AddWithValue("@idCategoria", obj.id_Categoria);
                    terminal.Parameters.AddWithValue("@Nombre", obj.Nombre);
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
        public bool Eliminar(Categoria obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection conection = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand terminal = new SqlCommand("Eliminar_Categoria", conection);
                    terminal.Parameters.AddWithValue("@idCategoria", obj.id_Categoria);
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
