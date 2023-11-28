using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CapaDatos
{
    public class CDReporteCompras
    {
        public List<ReporteCompra> Listar(string fechaCreacion, string fechaFin, int idproveedor)
        {
            List<ReporteCompra> lista = new List<ReporteCompra>();

            using (SqlConnection conection = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    SqlCommand terminal = new SqlCommand("ReporteCompras", conection);
                    terminal.Parameters.AddWithValue("fechaCreacion", fechaCreacion);
                    terminal.Parameters.AddWithValue("fechaFin", fechaFin);
                    terminal.Parameters.AddWithValue("idproveedor", idproveedor);
                    terminal.CommandType = CommandType.StoredProcedure;


                    conection.Open();
                    using (SqlDataReader dr = terminal.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ReporteCompra()
                            {
                                FechaCreacion = dr["fechaCreacion"].ToString(),
                                TipoDocumento = dr["TipoDocumento"].ToString(),
                                NumeroDcoumento = dr["NumeroDocumento"].ToString(),
                                MontoTotal = dr["Sub_Total"].ToString(),
                                UsuarioRegistro = dr["NombreUsuario"].ToString(),
                                DocumentoProveedor = dr["DocumentoProveedor"].ToString(),
                                RazonSocial = dr["RazonSocial"].ToString(),
                                CodigoProducto = dr["CodigoProducto"].ToString(),
                                NombreProduto = dr["NombreProducto"].ToString(),
                                Marca = dr["Marca"].ToString(),
                                Categoria = dr["Categoria"].ToString(),
                                FechaVencimiento = dr["FechaVencimiento"].ToString(),
                                PrecioDeCompra = dr["PrecioCompra"].ToString(),
                                PrecioDeVenta = dr["PrecioVenta"].ToString(),
                                Cantidad = dr["Cantidad"].ToString(),
                                SubTotal = dr["Total"].ToString()
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    lista = new List<ReporteCompra>();

                }
            }

            return lista;

        }

    }
}
