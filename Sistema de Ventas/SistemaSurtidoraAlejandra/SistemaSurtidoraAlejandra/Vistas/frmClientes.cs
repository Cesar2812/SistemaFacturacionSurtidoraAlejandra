using CapaEntidad;
using CapaNegocio;
using SistemaSurtidoraAlejandra.Utilidades;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SistemaSurtidoraAlejandra.Vistas
{
    public partial class frmClientes : Form
    {
        public frmClientes()
        {
            InitializeComponent();
        }
        private void frmClientes_Load(object sender, System.EventArgs e)
        {
            comboActivo.Items.Add(new OpcionesDeCombo() { valor = 1, Texto = "Activo" });
            comboActivo.Items.Add(new OpcionesDeCombo() { valor = 0, Texto = "No Activo" });

            comboActivo.DisplayMember = "Texto";
            comboActivo.ValueMember = "Valor";
            comboActivo.SelectedIndex = 0;

            try
            {
                foreach (DataGridViewColumn colum in dataGridClient.Columns)
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
            List<Cliente> liscliente = new CNCliente().Listar();
            foreach (Cliente item in liscliente)
            {
                try
                {
                    dataGridClient.Rows.Add(new object[] {"",item.id_Cliente,item.Cedula,item.Nombre1,item.Nombre2,item.Apellido1,
                                             item.Apellido2,
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
            txtCedula.Text = "";
            txtNombre1.Text = "";
            txtNombre2.Text = "";
            txtApellido1.Text = "";
            txtApellido2.Text = "";
            txtTelefono.Text = "";


            txtNombre1.Select();
        }
        private void butregistrar_Click(object sender, System.EventArgs e)//boton insertar
        {
            string mensaje = string.Empty;

            Cliente objcliente = new Cliente()
            {
                id_Cliente = Convert.ToInt32(txtID.Text),
                Cedula = txtCedula.Text,
                Nombre1 = txtNombre1.Text,
                Nombre2 = txtNombre2.Text,
                Apellido1 = txtApellido1.Text,
                Apellido2 = txtApellido2.Text,
                Telefono = txtTelefono.Text,
                Estado = Convert.ToInt32(((OpcionesDeCombo)comboActivo.SelectedItem).valor) == 1 ? true : false
            };

            if (objcliente.id_Cliente == 0)
            {
                int idcreado = new CNCliente().Registrar(objcliente, out mensaje);

                if (idcreado != 0)
                {
                    try
                    {
                        MessageBox.Show("Cliente Registrado");
                        dataGridClient.Rows.Add(new object[] {"",idcreado,txtCedula.Text,txtNombre1.Text,txtNombre2.Text,txtApellido1.Text,txtApellido2.Text,txtTelefono.Text,
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
                bool resultado = new CNCliente().editar(objcliente, out mensaje);

                if (resultado)
                {
                    DataGridViewRow row = dataGridClient.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["id_Cliente"].Value = txtID.Text;
                    row.Cells["Cedula"].Value = txtCedula.Text;
                    row.Cells["PrimerNombre"].Value = txtNombre1.Text;
                    row.Cells["SegundoNombre"].Value = txtNombre2.Text;
                    row.Cells["PrimerApellido"].Value = txtApellido1.Text;
                    row.Cells["SegundoApellido"].Value = txtApellido2.Text;
                    row.Cells["Telefono"].Value = txtTelefono.Text;
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
                if (MessageBox.Show("¿Desea eliminar al Cliente?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    Cliente obj = new Cliente()
                    {
                        id_Cliente = Convert.ToInt32(txtID.Text)
                    };
                    bool respuesta = new CNCliente().eliminar(obj, out mensaje);

                    if (respuesta)
                    {
                        dataGridClient.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                    }
                }
            }
            Limpiar();
        }
        private void pictureBox3_Click(object sender, EventArgs e)//bton Buscar
        {
            string filtro = ((OpcionesDeCombo)comboBusqueda.SelectedItem).valor.ToString();
            if (dataGridClient.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridClient.Rows)
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
        private void pictureBox2_Click(object sender, EventArgs e)//boton limpiar busqquedas
        {
            textBusquedas.Text = "";
            foreach (DataGridViewRow row in dataGridClient.Rows)
            {
                row.Visible = true;
            }
        }
        private void dataGridClient_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridClient.Columns[e.ColumnIndex].Name == "btnSelec")
            {
                int indice = e.RowIndex;
                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtID.Text = dataGridClient.Rows[indice].Cells["id_Cliente"].Value.ToString();
                    txtCedula.Text = dataGridClient.Rows[indice].Cells["Cedula"].Value.ToString();
                    txtNombre1.Text = dataGridClient.Rows[indice].Cells["PrimerNombre"].Value.ToString();
                    txtNombre2.Text = dataGridClient.Rows[indice].Cells["SegundoNombre"].Value.ToString();
                    txtApellido1.Text = dataGridClient.Rows[indice].Cells["PrimerApellido"].Value.ToString();
                    txtApellido2.Text = dataGridClient.Rows[indice].Cells["SegundoApellido"].Value.ToString();
                    txtTelefono.Text = dataGridClient.Rows[indice].Cells["Telefono"].Value.ToString();


                    foreach (OpcionesDeCombo oc in comboActivo.Items)
                    {
                        if (Convert.ToInt32(oc.valor) == Convert.ToInt32(dataGridClient.Rows[indice].Cells["EstadoValor"].Value))
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
                if (dataGridClient.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dataGridClient.Rows)
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
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
            }

        }
        private void txtNombre1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter ingresado no es una letra ni la tecla de retroceso
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                // Si no es una letra, suprimir el carácter
                e.Handled = true;
            }

        }
        private void txtNombre2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Space && e.KeyChar != (char)Keys.Back)
            {
                // Si no es una letra, suprimir el carácter
                e.Handled = true;
            }

        }
        private void txtApellido1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter ingresado no es una letra ni la tecla de retroceso
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                // Si no es una letra, suprimir el carácter
                e.Handled = true;
            }
        }
        private void txtApellido2_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter ingresado no es una letra ni la tecla de retroceso
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                // Si no es una letra, suprimir el carácter
                e.Handled = true;
            }
        }
        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                // Si no es un número, suprimir el carácter
                e.Handled = true;
            }
        }
    }
}
