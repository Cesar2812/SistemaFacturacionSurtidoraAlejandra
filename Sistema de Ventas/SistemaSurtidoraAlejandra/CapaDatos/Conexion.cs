﻿using System.Configuration;

namespace CapaDatos
{
    public class Conexion
    {
        public static string cadena = ConfigurationManager.ConnectionStrings["SistemaSurtidoraAlejandra.Properties.Settings.Conexion"].ToString();

    }
}
