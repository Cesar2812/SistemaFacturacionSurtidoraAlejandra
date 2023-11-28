using CapaEntidad;
using CapaNegocio;
using SistemaSurtidoraAlejandra.Utilidades;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace SistemaSurtidoraAlejandra
{
    public partial class frmProveedores : Form
    {
        public frmProveedores()
        {
            InitializeComponent();
        }

        private void frmProveedores_Load(object sender, EventArgs e)//load del formulario
        {
            comboActivo.Items.Add(new OpcionesDeCombo() { valor = 1, Texto = "Activo" });
            comboActivo.Items.Add(new OpcionesDeCombo() { valor = 0, Texto = "No Activo" });

            comboActivo.DisplayMember = "Texto";
            comboActivo.ValueMember = "Valor";
            comboActivo.SelectedIndex = 0;

            try
            {
                foreach (DataGridViewColumn colum in dataGridProv.Columns)
                {
                    if (colum.Visible == true && colum.Name != "btnSelec" && colum.Name != "Estado")
                    {
                        comboBusqueda.Items.Add(new OpcionesDeCombo() { valor = colum.Name, Texto = colum.HeaderText });
                    }

                }
                comboBusqueda.DisplayMember = "Texto";
                comboBusqueda.ValueMember = "Valor";
                comboBusqueda.SelectedIndex = 0;
            }
            catch
            {
            }
            //Listar Proveedores en el griv
            List<Proveedor> listProv = new CNProveedor().Listar();
            foreach (Proveedor item in listProv)
            {
                try
                {
                    dataGridProv.Rows.Add(new object[] {"",item.id_Proveedor,item.Documento,item.RazonSocial,item.Nombre,item.Correo,
                                    item.Telefono,
                                          item.Estado==true ?1 : 0,
                                           item.Estado== true ? "Activo" : "No Activo"
                    });
                }
                catch
                {
                }
            }

        }
        //limpiar textBoxs
        public void Limpiar()
        {
            txtIndice.Text = "-1";
            txtID.Text = "0";
            textDocumento.Text = "";
            textRazonSocial.Text = "";
            texttelefono.Text = "";
            textCorreo.Text = "";
            txtNombre.Text = "";

            textDocumento.Select();
        }

        private void butregistrar_Click(object sender, EventArgs e)//boton registrar
        {
            string mensaje = string.Empty;

            Proveedor objprovee = new Proveedor()
            {
                id_Proveedor = Convert.ToInt32(txtID.Text),
                Documento = textDocumento.Text,
                RazonSocial = textRazonSocial.Text,
                Nombre = txtNombre.Text,
                Correo = textCorreo.Text,
                Telefono = texttelefono.Text,
                Estado = Convert.ToInt32(((OpcionesDeCombo)comboActivo.SelectedItem).valor) == 1 ? true : false
            };
            if (objprovee.id_Proveedor == 0)
            {
                int idcreado = new CNProveedor().Registrar(objprovee, out mensaje);

                if (idcreado != 0)
                {
                    try
                    {
                        MessageBox.Show("Proveedor insertado");
                        dataGridProv.Rows.Add(new object[] {"",idcreado,textDocumento.Text,textRazonSocial.Text,txtNombre.Text,textCorreo.Text,texttelefono.Text,
                                                       ((OpcionesDeCombo)comboActivo.SelectedItem).valor.ToString(),
                                                       ((OpcionesDeCombo)comboActivo.SelectedItem).Texto.ToString()
                                        });

                    }
                    catch
                    {
                    }
                    Limpiar();

                }
                else
                {
                    MessageBox.Show(mensaje);
                }

            }
            else
            {
                bool resultado = new CNProveedor().editar(objprovee, out mensaje);

                if (resultado)
                {
                    DataGridViewRow row = dataGridProv.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["id_Proveedor"].Value = txtID.Text;
                    row.Cells["Documento"].Value = textDocumento.Text;
                    row.Cells["RazonSocial"].Value = textRazonSocial.Text;
                    row.Cells["Nombre"].Value = txtNombre.Text;
                    row.Cells["Correo"].Value = textCorreo.Text;
                    row.Cells["Telefono"].Value = texttelefono.Text;
                    row.Cells["EstadoValor"].Value = ((OpcionesDeCombo)comboActivo.SelectedItem).valor.ToString();
                    row.Cells["Estado"].Value = ((OpcionesDeCombo)comboActivo.SelectedItem).Texto.ToString();

                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)//boton eliminar
        {
            if (Convert.ToInt32(txtID.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eliminar al Proveedor?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    Proveedor obj = new Proveedor()
                    {
                        id_Proveedor = Convert.ToInt32(txtID.Text)
                    };
                    bool respuesta = new CNProveedor().eliminar(obj, out mensaje);

                    if (respuesta)
                    {
                        dataGridProv.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                    }
                }
            }
            Limpiar();
        }

        private void pictureBox3_Click(object sender, EventArgs e)//boton Buscar
        {
            string filtro = ((OpcionesDeCombo)comboBusqueda.SelectedItem).valor.ToString();
            if (dataGridProv.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridProv.Rows)
                {
                    if (row.Cells[filtro].Value.ToString().Trim().ToUpper().Contains(textBusquedas.Text.Trim().ToUpper()))
                    {
                        row.Visible = true;
                    }
                    else
                    {
                        row.Visible = false;
                    }

                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)//boton LImpiar
        {
            textBusquedas.Text = "";
            foreach (DataGridViewRow row in dataGridProv.Rows)
            {
                row.Visible = true;
            }
        }

        private void dataGridProv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridProv.Columns[e.ColumnIndex].Name == "btnSelec")
            {
                int indice = e.RowIndex;
                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtID.Text = dataGridProv.Rows[indice].Cells["id_Proveedor"].Value.ToString();
                    textDocumento.Text = dataGridProv.Rows[indice].Cells["Documento"].Value.ToString();
                    textRazonSocial.Text = dataGridProv.Rows[indice].Cells["RazonSocial"].Value.ToString();
                    txtNombre.Text = dataGridProv.Rows[indice].Cells["Nombre"].Value.ToString();
                    textCorreo.Text = dataGridProv.Rows[indice].Cells["Correo"].Value.ToString();
                    texttelefono.Text = dataGridProv.Rows[indice].Cells["Telefono"].Value.ToString();


                    foreach (OpcionesDeCombo oc in comboActivo.Items)
                    {
                        if (Convert.ToInt32(oc.valor) == Convert.ToInt32(dataGridProv.Rows[indice].Cells["EstadoValor"].Value))
                        {
                            int indice_combo = comboActivo.Items.IndexOf(oc);
                            comboActivo.SelectedIndex = indice_combo;
                            break;

                        }
                    }

                }
            }

        }

        private void textBusquedas_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string filtro = ((OpcionesDeCombo)comboBusqueda.SelectedItem).valor.ToString();
                if (dataGridProv.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dataGridProv.Rows)
                    {
                        if (row.Cells[filtro].Value.ToString().Trim().ToUpper().Contains(textBusquedas.Text.Trim().ToUpper()))
                        {
                            row.Visible = true;
                        }
                        else
                        {
                            row.Visible = false;
                        }

                    }
                }
            }
            catch (Exception w)
            {
                MessageBox.Show(w.ToString());
            }

        }
        private void textRazonSocial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Space && e.KeyChar != (char)Keys.Back)
            {
                // Si no es una letra, suprimir el carácter
                e.Handled = true;
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
        private void texttelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != (char)Keys.Back)
            {
                // Si no es un número ni guión, suprimir el carácter
                e.Handled = true;
            }

            // Verificar si hay más de un guión
            if (e.KeyChar == '-' && ((TextBox)sender).Text.IndexOf('-') > -1)
            {
                e.Handled = true;
            }
        }
    }
}
