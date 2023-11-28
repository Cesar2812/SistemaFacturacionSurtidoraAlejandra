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
    public partial class frmVentas : Form
    {
        private static Usuario user;
        public frmVentas(Usuario obuser = null)
        {
            InitializeComponent();
            user = obuser;
        }
        private void frmVentas_Load(object sender, System.EventArgs e)
        {
            //llenando el combo de tipo de documentos
            comboTipoDoc.Items.Add(new OpcionesDeCombo() { valor = "Boleta", Texto = "Boleta" });
            comboTipoDoc.Items.Add(new OpcionesDeCombo() { valor = "Factura", Texto = "Factura" });
            comboTipoDoc.DisplayMember = "Texto";
            comboTipoDoc.ValueMember = "Valor";
            comboTipoDoc.SelectedIndex = 0;

            textFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");

            textIdProd.Text = "0";
            txtIdCliente.Text = "0";
        }

        private void btnBuscarProv_Click(object sender, EventArgs e)//Boton Buscar Clientes
        {
            using (var modal = new modalClient())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {

                    txtIdCliente.Text = modal._Cliente.id_Cliente.ToString();
                    txtCedula.Text = modal._Cliente.Cedula.ToString();
                    textNombre.Text = modal._Cliente.Nombre1.ToString();
                    txtApellido.Text = modal._Cliente.Apellido1.ToString();
                }
                else
                {
                    txtCedula.Select();
                }
            }
        }
        private void btnbuscarProducto_Click(object sender, EventArgs e)//Buscar Productos
        {
            using (var modal = new modalProd())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {

                    textIdProd.Text = modal._producto.id_Producto.ToString();
                    texCodProd.Text = modal._producto.Codigo.ToString();
                    textProd.Text = modal._producto.Nombre_Producto.ToString();
                    textVenta.Text = modal._producto.PrecioVenta.ToString("0.00");
                    texstock.Text = modal._producto.Stock.ToString();
                    textCantida.Select();
                }
                else
                {
                    texCodProd.Select();
                }
            }
        }
        private void texCodProd_KeyDown(object sender, KeyEventArgs e)//Registar producto al tocar enter
        {
            if (e.KeyData == Keys.Enter)
            {
                Producto orpro = new CNProducto().Listar().Where(p => p.Codigo == texCodProd.Text && p.Estado == true).FirstOrDefault();
                if (orpro != null)
                {
                    texCodProd.BackColor = System.Drawing.Color.LightGreen;
                    textIdProd.Text = orpro.id_Producto.ToString();
                    textProd.Text = orpro.Nombre_Producto;
                    textVenta.Text = orpro.PrecioVenta.ToString("0.00");
                    texstock.Text = orpro.Stock.ToString();
                    textCantida.Select();

                }
                else
                {
                    texCodProd.BackColor = System.Drawing.Color.Red;
                    textIdProd.Text = "0";
                    textProd.Text = "";
                    textVenta.Text = "";
                    texstock.Text = "";
                    textCantida.Value = 1;

                }
            }
        }
        private void button1_Click(object sender, EventArgs e)//agregar productos al grid
        {
            decimal precio = 0;
            bool producto_existe = false;

            if (int.Parse(textIdProd.Text) == 0)
            {
                MessageBox.Show("Debe de selecionar un producto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (!decimal.TryParse(textVenta.Text, out precio))
            {
                MessageBox.Show("Precio Venta-Formato de moneda incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textVenta.Select();
                return;
            }
            if (Convert.ToInt32(texstock.Text) < Convert.ToInt32(textCantida.Value.ToString()))
            {
                MessageBox.Show("Lan cantidad para vender no puede ser mayor al Stock", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            foreach (DataGridViewRow fila in dataGridVentas.Rows)
            {
                if (fila.Cells["IdProducto"].Value.ToString() == textIdProd.Text)
                {
                    producto_existe = true;
                    break;
                }
            }
            if (!producto_existe)
            {
                bool respusta = new CNVenta().restarStok(
                    Convert.ToInt32(textIdProd.Text),
                    Convert.ToInt32(textCantida.Value.ToString())
                    );

                if (respusta)
                {
                    dataGridVentas.Rows.Add(new object[]
                    {
                       textIdProd.Text,
                       textProd.Text,
                       precio.ToString(""),
                       textCantida.Value.ToString(),
                       (textCantida.Value*precio).ToString("0.00")
                    });
                    CalcularTotal();
                    limpiarpro();
                    texCodProd.Select();
                }
            }
        }
        private void CalcularTotal()//metodo para calcular total
        {
            decimal total = 0;
            if (dataGridVentas.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridVentas.Rows)
                {
                    total += Convert.ToDecimal(row.Cells["SubTotal"].Value.ToString());
                }
            }
            textTotalPagar.Text = total.ToString("0.00");
        }
        private void limpiarpro()
        {
            textIdProd.Text = "0";
            texCodProd.Text = "";
            textProd.Text = "";
            textVenta.Text = "";
            texstock.Text = "";
            textCantida.Value = 1;
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
            e.Handled = true;
        }
        private void dataGridVentas_CellContentClick(object sender, DataGridViewCellEventArgs e)//evento click para eliminar del grid
        {
            if (dataGridVentas.Columns[e.ColumnIndex].Name == "btnSelec")
            {
                limpiarpro();
                int indice = e.RowIndex;
                if (indice >= 0)
                {
                    bool respuesta = new CNVenta().sumarStock(
                        Convert.ToInt32(dataGridVentas.Rows[indice].Cells["IdProducto"].Value.ToString()),
                        Convert.ToInt32(dataGridVentas.Rows[indice].Cells["Cantidad"].Value.ToString()));
                    if (respuesta)
                    {
                        dataGridVentas.Rows.RemoveAt(indice);
                        CalcularTotal();
                    }

                }
            }
        }
        private void txtPagaCon_KeyPress(object sender, KeyPressEventArgs e)//validacion
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (txtPagaCon.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
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
        private void CalcularVuelto()//calcular cambio
        {
            if (textTotalPagar.Text.Trim() == "")
            {
                MessageBox.Show("No existen Productos en la Venta", "Mnesaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            decimal pagancon;
            decimal total = Convert.ToDecimal(textTotalPagar.Text);
            if (txtPagaCon.Text.Trim() == "")
            {
                txtPagaCon.Text = "0";
            }
            if (decimal.TryParse(txtPagaCon.Text.Trim(), out pagancon))
            {
                if (pagancon < total)
                {
                    txtCambio.Text = "0.00";

                }
                else
                {
                    decimal cambio = pagancon - total;
                    txtCambio.Text = cambio.ToString("0.00");
                }
            }
        }
        private void txtPagaCon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                CalcularVuelto();
            }
        }
        private void butregistrar_Click(object sender, EventArgs e)
        {
            if (txtCedula.Text == "")
            {
                MessageBox.Show("Es necesario el Numero de Cedula del Cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;

            }
            if (textNombre.Text == "")
            {
                MessageBox.Show("Es necesario el Nombre del Cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (dataGridVentas.Rows.Count < 1)
            {
                MessageBox.Show("Es necesario registrar Productos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DataTable detalleVenta = new DataTable();

            detalleVenta.Columns.Add("id_producto", typeof(int));
            detalleVenta.Columns.Add("PrecioVenta", typeof(decimal));
            detalleVenta.Columns.Add("Cantidad", typeof(int));
            detalleVenta.Columns.Add("Sub_Total", typeof(decimal));

            foreach (DataGridViewRow row in dataGridVentas.Rows)
            {
                detalleVenta.Rows.Add(new object[]
                {
                    row.Cells["IdProducto"].Value.ToString(),
                    row.Cells["Precio"].Value.ToString(),
                    row.Cells["Cantidad"].Value.ToString(),
                    row.Cells["SubTotal"].Value.ToString()

                });
            }
            int idCorrelativo = new CNVenta().obtenerCorrelativo();
            string numeroDocumento = string.Format("{0:0000}", idCorrelativo);
            CalcularVuelto();

            Venta oventa = new Venta()
            {
                obj_user = new Usuario() { id_Usuario = user.id_Usuario },
                TipoDocumento = ((OpcionesDeCombo)comboTipoDoc.SelectedItem).Texto,
                NumeroDocumento = numeroDocumento,
                obj_Cliente = new Cliente() { id_Cliente = Convert.ToInt32(txtIdCliente.Text) },
                MontoPago = Convert.ToDecimal(txtPagaCon.Text),
                MontoCambio = Convert.ToDecimal(txtCambio.Text),
                MontoTotal = Convert.ToDecimal(textTotalPagar.Text)
            };

            string mensaje = string.Empty;
            bool respuesta = new CNVenta().Registrar(oventa, detalleVenta, out mensaje);
            if (respuesta)
            {
                var result = MessageBox.Show("Numero de Venta generado:\n" + numeroDocumento + "\n\n¿Desea trasladar al portapapeles?",
                    "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                {
                    Clipboard.SetText(numeroDocumento);
                }
                txtIdCliente.Text = "0";
                textNombre.Text = "";
                txtApellido.Text = "";
                dataGridVentas.Rows.Clear();
                CalcularTotal();
                txtPagaCon.Text = "";
                txtCambio.Text = "";
            }
            else
            {
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void textNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void textProd_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void texstock_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void txtCedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void txtCambio_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void textTotalPagar_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void textCantida_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                // Si no es un número, suprimir el carácter
                e.Handled = true;
            }

        }
    }
}
