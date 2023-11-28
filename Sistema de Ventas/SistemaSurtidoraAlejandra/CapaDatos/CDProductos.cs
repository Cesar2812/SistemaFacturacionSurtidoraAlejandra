using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CapaDatos
{
    public class CDProductos
    {

        SqlDataReader leerfilas;
        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();

            using (SqlConnection conection = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("Select id_Producto,Codigo,Nombre_Producto,c.id_Categoria,c.Nombre as Categoria,m.id_Marca,m.Nombre as Marca,p.Descripcion,Stock,PrecioCompra,PrecioVenta,FechaVencimiento,p.Estado");
                    query.AppendLine("from Producto p");
                    query.AppendLine("inner join Categoria c on c.id_Categoria=p.id_categoria");
                    query.AppendLine("inner join Marca m on m.id_Marca=p.id_marca");

                    SqlCommand terminal = new SqlCommand(query.ToString(), conection);
                    terminal.CommandType = CommandType.Text;

                    conection.Open();
                    using (SqlDataReader dr = terminal.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Producto()
                            {
                                id_Producto = Convert.ToInt32(dr["id_Producto"]),
                                Codigo = dr["Codigo"].ToString(),
                                Nombre_Producto = dr["Nombre_Producto"].ToString(),
                                obj_categoria = new Categoria() { id_Categoria = Convert.ToInt32(dr["id_Categoria"]), Nombre = dr["Categoria"].ToString() },
                                obj_marca = new Marca() { id_Marca = Convert.ToInt32(dr["id_Marca"]), NombreMarca = dr["Marca"].ToString() },
                                Descripcion = dr["Descripcion"].ToString(),
                                Stock = Convert.ToInt32(dr["Stock"].ToString()),
                                PrecioCompra = Convert.ToDecimal(dr["PrecioCompra"].ToString()),
                                PrecioVenta = Convert.ToDecimal(dr["PrecioVenta"].ToString()),
                                FechaVencimiento = Convert.ToDateTime(dr["FechaVencimiento"]).ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }


                }
                catch (Exception ex)
                {
                    lista = new List<Producto>();

                }
            }

            return lista;

        }

        public int Registrar(Producto obj, out string Mensaje)
        {
            int idProductoGenerado = 0;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection conection = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand terminal = new SqlCommand("Ingresar_Productos", conection);
                    terminal.Parameters.AddWithValue("@Codigo", obj.Codigo);
                    terminal.Parameters.AddWithValue("@NombreProducto", obj.Nombre_Producto);
                    terminal.Parameters.AddWithValue("@idCategoria", obj.obj_categoria.id_Categoria);
                    terminal.Parameters.AddWithValue("@idMarca", obj.obj_marca.id_Marca);
                    terminal.Parameters.AddWithValue("@Descripcion", obj.Descripcion);
                    terminal.Parameters.AddWithValue("@Estado", obj.Estado);
                    terminal.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    terminal.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    terminal.CommandType = CommandType.StoredProcedure;
                    conection.Open();
                    terminal.ExecuteNonQuery();

                    idProductoGenerado = Convert.ToInt32(terminal.Parameters["@Resultado"].Value);
                    Mensaje = terminal.Parameters["@Mensaje"].Value.ToString();


                }

            }
            catch (Exception ex)
            {
                idProductoGenerado = 0;
                Mensaje = ex.Message;

            }

            return idProductoGenerado;
        }
        public bool Editar(Producto obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection conection = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand terminal = new SqlCommand("Editar_Producto", conection);
                    terminal.Parameters.AddWithValue("@idProducto", obj.id_Producto);
                    terminal.Parameters.AddWithValue("@Codigo", obj.Codigo);
                    terminal.Parameters.AddWithValue("@Nombre", obj.Nombre_Producto);
                    terminal.Parameters.AddWithValue("@idCategoria", obj.obj_categoria.id_Categoria);
                    terminal.Parameters.AddWithValue("@idMarca", obj.obj_marca.id_Marca);
                    terminal.Parameters.AddWithValue("@Descripcion", obj.Descripcion);
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

        public bool Eliminar(Producto obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection conection = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand terminal = new SqlCommand("Eliminar_Producto", conection);
                    terminal.Parameters.AddWithValue("@idProducto", obj.id_Producto);
                    terminal.Parameters.Add("@Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    terminal.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    terminal.CommandType = CommandType.StoredProcedure;
                    conection.Open();
                    terminal.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(terminal.Parameters["@Respuesta"].Value);
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
