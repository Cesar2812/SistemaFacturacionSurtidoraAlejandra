using CapaEntidad;
using CapaNegocio;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SistemaSurtidoraAlejandra.Vistas
{
    public partial class frmDatosSurtidora : Form
    {
        public frmDatosSurtidora()
        {
            InitializeComponent();
        }

        private void pictureBoxLogo_Click(object sender, EventArgs e)
        {

        }
        public Image byteToImage(byte[] imagebytes)
        {
            MemoryStream ms = new MemoryStream();
            ms.Write(imagebytes, 0, imagebytes.Length);
            Image imagen = new Bitmap(ms);

            return imagen;
        }

        private void frmDatosSurtidora_Load(object sender, EventArgs e)
        {
            bool obtenido = true;
            byte[] byteImagen = new CNNegocio().obtenerLogo(out obtenido);
            if (obtenido)
            {
                pictureBoxLogo.Image = byteToImage(byteImagen);
            }
            Negocio datos = new CNNegocio().obtenerdatos();
            txtNombre.Text = datos.Nombre;
            txtRUC.Text = datos.RUC;
            txtDirecccion.Text = datos.Direccion;

        }

        private void btnSubir_Click(object sender, EventArgs e)//subir imagen
        {
            string Mensaje = string.Empty;
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = "Files|*.jpg;*.jpeg;*.png";

            if (openfile.ShowDialog() == DialogResult.OK)
            {
                byte[] byteImage = File.ReadAllBytes(openfile.FileName);
                bool respuesta = new CNNegocio().ActualizarLogo(byteImage, out Mensaje);

                if (respuesta)
                {
                    pictureBoxLogo.Image = byteToImage(byteImage);
                }
                else
                {
                    MessageBox.Show(Mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }

        }

        private void butregistrar_Click(object sender, EventArgs e)//guardar cambios
        {
            string Mensaje = string.Empty;

            Negocio obj = new Negocio()
            {
                Nombre = txtNombre.Text,
                RUC = txtRUC.Text,
                Direccion = txtDirecccion.Text
            };

            bool resp = new CNNegocio().GuardarDatos(obj, out Mensaje);


            if (resp == false)
            {
                MessageBox.Show("Los Datos no se guardaron", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            else
            {
                MessageBox.Show("Los Datos se guardadron correctamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Space && e.KeyChar != (char)Keys.Back)
            {
                // Si no es una letra, suprimir el carácter
                e.Handled = true;
            }
        }
    }
}
