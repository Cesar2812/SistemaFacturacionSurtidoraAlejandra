using CapaEntidad;
using CapaNegocio;
using SistemaSurtidoraAlejandra.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace SistemaSurtidoraAlejandra
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, System.EventArgs e)
        {
            textuser.Select();

        }
        public void fr_closing(object sender, FormClosingEventArgs e)
        {
            textuser.Text = "";
            textpass.Text = "";
            this.Show();
            textuser.Select();

        }
        public static string EncriptarConHash(string textoPlano)//encriptar clave para comprobar
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(textoPlano));

                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    builder.Append(data[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        private void checkBoxPass_CheckedChanged(object sender, System.EventArgs e)//validar que se mire la clave
        {
            try
            {
                if (checkBoxPass.Checked == false)
                {
                    textpass.UseSystemPasswordChar = true;
                }
                else
                {
                    textpass.UseSystemPasswordChar = false;
                }
            }
            catch (Exception ey)
            {
                MessageBox.Show(ey.ToString());
            }

        }
        private void btnIniciar_Click(object sender, EventArgs e)//boton de inicio de sesion
        {
            try
            {
                List<Usuario> usuarios = new CNUsuarios().Listar();
                string usuario = textuser.Text;
                string claveIngresada = textpass.Text;

                //Encriptar la contraseña ingresada para compararla con la almacenada
                string claveEncriptada = EncriptarConHash(claveIngresada);

                Usuario usuarioEncontrado = usuarios.FirstOrDefault(u => u.usuario == usuario && u.Clave == claveEncriptada);

                if (usuarioEncontrado != null)
                {
                    MDIfrmMenu menu = new MDIfrmMenu(usuarioEncontrado);
                    menu.Show();
                    this.Hide();
                    menu.FormClosing += fr_closing;
                }
                else
                {
                    MessageBox.Show("Usuario no encontrado o contraseña incorrecta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}
