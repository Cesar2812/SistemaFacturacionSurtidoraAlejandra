using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SistemaSurtidoraAlejandra.Vistas
{
    public partial class MDIfrmMenu : Form
    {
        private int childFormNumber = 0;
        private static Usuario usuarioActual;
        CapaDatos.Conexion conection = new CapaDatos.Conexion();


        public MDIfrmMenu(Usuario objUsuario)
        {
            InitializeComponent();
            usuarioActual = objUsuario;
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Ventana " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void timerHora_Tick(object sender, EventArgs e)
        {
            labelHora.Text = DateTime.Now.ToLongTimeString();
            labelFecha.Text = DateTime.Now.ToLongDateString();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUsuarios user = new frmUsuarios();
            user.MdiParent = this;
            user.Show();
        }

        private void proveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProveedores prove = new frmProveedores();
            prove.MdiParent = this;
            prove.Show();
        }

        private void negocioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDatosSurtidora surtidora = new frmDatosSurtidora();
            surtidora.MdiParent = this;
            surtidora.Show();
        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProductos productos = new frmProductos();
            productos.MdiParent = this;
            productos.Show();
        }

        private void reporteDeComprasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReporteCompra compraRepor = new frmReporteCompra();
            compraRepor.MdiParent = this;
            compraRepor.Show();
        }

        private void verDetalleCompraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetalleCompras dtailCompra = new frmDetalleCompras();
            dtailCompra.MdiParent = this;
            dtailCompra.Show();
        }

        private void reporteDeVentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReporteVenta ventas = new frmReporteVenta();
            ventas.MdiParent = this;
            ventas.Show();

        }

        private void MDIfrmMenu_Load(object sender, EventArgs e)
        {
            List<Permiso> listapermisos = new CNPermiso().Listar(usuarioActual.id_Usuario);
            foreach (ToolStripMenuItem item in menuStrip.Items)
            {
                bool encontrado = listapermisos.Any(n => n.Nombre_Menu == item.Name);
                if (encontrado == false)
                {
                    item.Visible = false;
                }
            }
            lblUsuario.Text = usuarioActual.Nombre1;
        }

        private void mantenedorToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmClientes clientes = new frmClientes();
            clientes.MdiParent = this;
            clientes.Show();
        }

        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCategoria frmcateg = new frmCategoria();
            frmcateg.MdiParent = this;
            frmcateg.Show();
        }
        private void marcasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMarcas frmmarca = new frmMarcas();
            frmmarca.MdiParent = this;
            frmmarca.Show();

        }
        private void realizarCompraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCompras compra = new frmCompras(usuarioActual);
            compra.MdiParent = this;
            compra.Show();
        }
        private void realizarVentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmVentas venta = new frmVentas(usuarioActual);
            venta.MdiParent = this;
            venta.Show();
        }
        private void verDetalleVentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetalleVenta ventasde = new frmDetalleVenta();
            ventasde.MdiParent = this;
            ventasde.Show();
        }
    }
}
