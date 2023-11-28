using CapaEntidad;
using CapaNegocio;
using SistemaSurtidoraAlejandra.FormulariosModales;
using SistemaSurtidoraAlejandra.Utilidades;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SistemaSurtidoraAlejandra
{
    public partial class frmCompras : Form
    {
        private static Usuario user;
        public frmCompras(Usuario obuser = null)
        {
            InitializeComponent();
            user = obuser;
        }

        private void frmCompras_Load(object sender, System.EventArgs e)
        {
            //llenando el combo de tipo de documentos
            comboTipoDoc.Items.Add(new OpcionesDeCombo() { valor = "Boleta", Texto = "Boleta" });
            comboTipoDoc.Items.Add(new OpcionesDeCombo() { valor = "Factura", Texto = "Factura" });
            comboTipoDoc.DisplayMember = "Texto";
            comboTipoDoc.ValueMember = "Valor";
            comboTipoDoc.SelectedIndex = 0;
            //para la fecha
            textFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtFcahVen.Text = DateTime.Now.ToString("yyyy/MM/dd");

            textIdProd.Text = "0";
            textIdProv.Text = "0";

        }

        private void btnBuscarProv_Click(object sender, EventArgs e)
        {
            using (var modal = new modalProv())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {

                    textIdProv.Text = modal._proveedor.id_Proveedor.ToString();
                    textDocumento.Text = modal._proveedor.Documento.ToString();
                    textNombre.Text = modal._proveedor.Nombre.ToString();
                }
                else
                {
                    textDocumento.Select();
                }

            }
        }
        private void btnbuscarProducto_Click(object sender, EventArgs e)//boton bucar producto
        {
            using (var modal = new modalProd())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {

                    textIdProd.Text = modal._producto.id_Producto.ToString();
                    texCodProd.Text = modal._producto.Codigo.ToString();
                    textProd.Text = modal._producto.Nombre_Producto.ToString();
                    textCompra.Select();
                }
                else
                {
                    texCodProd.Select();
                }

            }

        }

        private void button1_Click(object sender, EventArgs e)//boton para agrgar products al grid
        {
            decimal precioCompar = 0;
            decimal precioventa = 0;
            DateTime fecga = DateTime.Now;
            bool producto_exist = false;
            if (int.Parse(textIdProd.Text) == 0)
            {
                MessageBox.Show("Debe de selecionar un producto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (!decimal.TryParse(textCompra.Text, out precioCompar))
            {
                MessageBox.Show("Precio Compra-Formato de moneda incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (!decimal.TryParse(textVenta.Text, out precioventa))
            {
                MessageBox.Show("Precio Venta-Formato de moneda incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            foreach (DataGridViewRow fil in dataCompras.Rows)
            {
                if (fil.Cells["idProducto"].Value.ToString() == textIdProd.Text)
                {
                    producto_exist = true;
                    break;
                }
            }
            if (!producto_exist)
            {
                dataCompras.Rows.Add(new object[]
                {
                    textIdProd.Text,
                    textProd.Text,
                    precioCompar.ToString("0.00"),
                    precioventa.ToString("0.00"),
                    Convert.ToDateTime(txtFcahVen.Text),
                    textCantida.Value.ToString(),
                    (textCantida.Value * precioCompar).ToString("0.00")

                });
                CalcularTotal();
                limpiarpro();
                textProd.Select();
            }

        }
        private void limpiarpro()
        {
            textIdProd.Text = "0";
            texCodProd.Text = "";
            textProd.Text = "";
            textCompra.Text = "";
            textVenta.Text = "";
            textCantida.Value = 1;
        }

        private void CalcularTotal()//metodo para calculat total
        {
            decimal total = 0;
            if (dataCompras.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataCompras.Rows)
                {
                    total += Convert.ToDecimal(row.Cells["MontoTotal"].Value.ToString());
                }
            }
            textTotalPagar.Text = total.ToString("0.00");
        }

        private void dataCompras_CellContentClick(object sender, DataGridViewCellEventArgs e)//evento click para eliminar en el datagrid
        {
            if (dataCompras.Columns[e.ColumnIndex].Name == "boton")
            {
                int indice = e.RowIndex;
                if (indice >= 0)
                {
                    dataCompras.Rows.RemoveAt(indice);
                    CalcularTotal();
                }
            }

        }

        private void textCompra_KeyPress(object sender, KeyPressEventArgs e)//validacion
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (textCompra.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
                {
                    e.Handled = true;
                }
                else
                {
                    if (Char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ".")
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
            }

        }
        private void textVenta_KeyPress(object sender, KeyPressEventArgs e)//validacion
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (textVenta.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
                {
                    e.Handled = true;
                }
                else
                {
                    if (Char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ".")
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
            }
        }
        private void butregistrar_Click(object sender, EventArgs e)//registrar la compra en base de dato
        {
            if (Convert.ToInt32(textIdProv.Text) == 0)
            {
                MessageBox.Show("Deebe seleccionar un proveedor", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (dataCompras.Rows.Count < 1)
            {
                MessageBox.Show("Deebe ingresar productos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DataTable detalle_compra = new DataTable();
            detalle_compra.Columns.Add("idProducto", typeof(int));
            detalle_compra.Columns.Add("PrecioDeCompra", typeof(decimal));
            detalle_compra.Columns.Add("PrecioDeVenta", typeof(decimal));
            detalle_compra.Columns.Add("FechaVencimiento", typeof(DateTime));
            detalle_compra.Columns.Add("Cantidad", typeof(int));
            detalle_compra.Columns.Add("Montototal", typeof(decimal));

            foreach (DataGridViewRow row in dataCompras.Rows)
            {
                detalle_compra.Rows.Add(
                    new object[]
                    {
                        Convert.ToInt32(row.Cells["idProducto"].Value.ToString()),
                        row.Cells["PrecioDeCompra"].Value.ToString(),
                        row.Cells["PrecioDeVenta"].Value.ToString(),
                        Convert.ToDateTime(row.Cells["FechaVencimiento"].Value),
                        row.Cells["Cantidad"].Value.ToString(),
                        row.Cells["Montototal"].Value.ToString(),
                    });
            }
            string y = "";
            int correlativo = new CNCompra().obtenerCorrelativo();
            string numerodoc = string.Format("{0:0000}", correlativo);

            Compra oCompra = new Compra()
            {
                od_usuario = new Usuario() { id_Usuario = user.id_Usuario },
                od_Proveedor = new Proveedor() { id_Proveedor = Convert.ToInt32(textIdProv.Text) },
                TipoDeDocumento = ((OpcionesDeCombo)comboTipoDoc.SelectedItem).Texto,
                NumeroDocumento = numerodoc,
                MontoTotal = Convert.ToDecimal(textTotalPagar.Text)
            };
            string v = "";
            string mensaje = string.Empty;
            bool respues = new CNCompra().Registrar(oCompra, detalle_compra, out mensaje);

            if (respues)
            {
                var result = MessageBox.Show("Numero de Compra generado:\n" + numerodoc + "\n\n¿Desea trasladar al portapapeles?",
                    "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                {
                    Clipboard.SetText(numerodoc);
                }
                textIdProv.Text = "0";
                textNombre.Text = "";
                dataCompras.Rows.Clear();
                CalcularTotal();
            }
            else
            {
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        private void texCodProd_KeyDown(object sender, KeyEventArgs e)//metodo para registrar prodcuto con Enter
        {
            if (e.KeyData == Keys.Enter)
            {
                Producto orpro = new CNProducto().Listar().Where(p => p.Codigo == texCodProd.Text && p.Estado == true).FirstOrDefault();
                if (orpro != null)
                {
                    texCodProd.BackColor = System.Drawing.Color.LightGreen;
                    textIdProd.Text = orpro.id_Producto.ToString();
                    textProd.Text = orpro.Nombre_Producto;
                    textCompra.Select();

                }
                else
                {
                    texCodProd.BackColor = System.Drawing.Color.Red;
                    textIdProd.Text = "0";
                    textProd.Text = "";
                    textVenta.Text = "";
                    textCantida.Value = 1;

                }
            }


        }
        private void textNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void textProd_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

        }

        private void textDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void textTotalPagar_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
