using CapaEntidad;
using CapaNegocio;
using SistemaSurtidoraAlejandra.Utilidades;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace SistemaSurtidoraAlejandra.Vistas
{
    public partial class frmUsuarios : Form
    {
        public frmUsuarios()
        {
            InitializeComponent();
        }

        private void frmUsuarios_Load(object sender, System.EventArgs e)
        {
            comboActivo.Items.Add(new OpcionesDeCombo() { valor = 1, Texto = "Activo" });
            comboActivo.Items.Add(new OpcionesDeCombo() { valor = 0, Texto = "No Activo" });

            comboActivo.DisplayMember = "Texto";
            comboActivo.ValueMember = "Valor";
            comboActivo.SelectedIndex = 0;

            List<Rol> listarol = new CNRol().Listar();
            foreach (Rol item in listarol)
            {
                comboRol.Items.Add(new OpcionesDeCombo() { valor = item.id_Rol, Texto = item.Descripcion });


            }
            comboRol.DisplayMember = "Texto";
            comboRol.ValueMember = "Valor";
            comboRol.SelectedIndex = 0;

            try
            {
                foreach (DataGridViewColumn colum in dataGridUsers.Columns)
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

            //mostrar en el gric usuario
            List<Usuario> listausuarios = new CNUsuarios().Listar();
            foreach (Usuario item in listausuarios)
            {
                try
                {
                    dataGridUsers.Rows.Add(new object[] {"",item.id_Usuario,item.Nombre1,item.Nombre2,item.Apellido1,item.Apellido2,item.usuario,
                        item.Correo,item.Clave,
                       item.objRolU.id_Rol,
                       item.objRolU.Descripcion,
                       item.Estado==true ?1 : 0,
                        item.Estado== true ? "Activo" : "No Activo"
                    });
                }
                catch
                {

                }
            }

        }

        private void butregistrar_Click(object sender, System.EventArgs e)//boton Registrar
        {
            if (textConfirm.Text.Equals(textpass.Text))
            {
                string mensaje = string.Empty;

                Usuario objusuario = new Usuario()
                {
                    id_Usuario = Convert.ToInt32(txtID.Text),
                    Nombre1 = textPrimerNombre.Text,
                    Nombre2 = textSegundoNombre.Text,
                    Apellido1 = textApellido1.Text,
                    Apellido2 = textApellido2.Text,
                    usuario = textuser.Text,
                    Correo = textCorreo.Text,
                    Clave = EncriptarConHash(textpass.Text),
                    objRolU = new Rol() { id_Rol = Convert.ToInt32(((OpcionesDeCombo)comboRol.SelectedItem).valor) },
                    Estado = Convert.ToInt32(((OpcionesDeCombo)comboActivo.SelectedItem).valor) == 1 ? true : false
                };

                if (objusuario.id_Usuario == 0)
                {
                    int idusuariocreado = new CNUsuarios().Registrar(objusuario, out mensaje);

                    if (idusuariocreado != 0)
                    {
                        try
                        {
                            MessageBox.Show("Usuario insertado");
                            dataGridUsers.Rows.Add(new object[] {"",idusuariocreado,textPrimerNombre.Text,textSegundoNombre.Text,textApellido1.Text,
                                 textApellido2.Text,textuser.Text,textCorreo.Text,textpass.Text,
                               ((OpcionesDeCombo)comboRol.SelectedItem).valor.ToString(),
                               ((OpcionesDeCombo)comboRol.SelectedItem).Texto.ToString(),
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
                    bool resultado = new CNUsuarios().editar(objusuario, out mensaje);

                    if (resultado)
                    {
                        DataGridViewRow row = dataGridUsers.Rows[Convert.ToInt32(txtIndice.Text)];
                        row.Cells["id_usuario"].Value = txtID.Text;
                        row.Cells["PrimerNombre"].Value = textPrimerNombre.Text;
                        row.Cells["SegundoNombre"].Value = textSegundoNombre.Text;
                        row.Cells["PrimerApellido"].Value = textApellido1.Text;
                        row.Cells["SegundoApellido"].Value = textApellido2.Text;
                        row.Cells["Usuario"].Value = textuser.Text;
                        row.Cells["Correo"].Value = textCorreo.Text;
                        row.Cells["Clave"].Value = textpass.Text;
                        row.Cells["idRol"].Value = ((OpcionesDeCombo)comboRol.SelectedItem).valor.ToString();
                        row.Cells["Rol"].Value = ((OpcionesDeCombo)comboRol.SelectedItem).Texto.ToString();
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
            else
            {
                MessageBox.Show("Las Contraseñas No son Iguales", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public static string EncriptarConHash(string textoPlano)//encriptar la contraseña
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
        public void Limpiar()
        {
            txtIndice.Text = "-1";
            txtID.Text = "0";
            textPrimerNombre.Text = "";
            textApellido1.Text = "";
            textApellido2.Text = "";
            textSegundoNombre.Text = "";
            textuser.Text = "";
            textpass.Text = "";
            textConfirm.Text = "";
            textCorreo.Text = "";

            textPrimerNombre.Select();

        }
        private void dataGridUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridUsers.Columns[e.ColumnIndex].Name == "btnSelec")
            {
                int indice = e.RowIndex;
                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtID.Text = dataGridUsers.Rows[indice].Cells["id_usuario"].Value.ToString();
                    textPrimerNombre.Text = dataGridUsers.Rows[indice].Cells["PrimerNombre"].Value.ToString();
                    textSegundoNombre.Text = dataGridUsers.Rows[indice].Cells["SegundoNombre"].Value.ToString();
                    textApellido1.Text = dataGridUsers.Rows[indice].Cells["PrimerApellido"].Value.ToString();
                    textApellido2.Text = dataGridUsers.Rows[indice].Cells["SegundoApellido"].Value.ToString();
                    textuser.Text = dataGridUsers.Rows[indice].Cells["Usuario"].Value.ToString();
                    textCorreo.Text = dataGridUsers.Rows[indice].Cells["Correo"].Value.ToString();
                    textpass.Text = "";
                    textConfirm.Text = "";

                    foreach (OpcionesDeCombo oc in comboRol.Items)
                    {
                        if (Convert.ToInt32(oc.valor) == Convert.ToInt32(dataGridUsers.Rows[indice].Cells["idRol"].Value))
                        {
                            int indice_combo = comboRol.Items.IndexOf(oc);
                            comboRol.SelectedIndex = indice_combo;
                            break;

                        }
                    }

                    foreach (OpcionesDeCombo oc in comboActivo.Items)
                    {
                        if (Convert.ToInt32(oc.valor) == Convert.ToInt32(dataGridUsers.Rows[indice].Cells["EstadoValor"].Value))
                        {
                            int indice_combo = comboActivo.Items.IndexOf(oc);
                            comboActivo.SelectedIndex = indice_combo;
                            break;

                        }
                    }

                }
            }
        }
        private void button2_Click(object sender, EventArgs e)//boton eliminar
        {
            if (Convert.ToInt32(txtID.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eliminar el usuario?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    Usuario objusuario = new Usuario()
                    {
                        id_Usuario = Convert.ToInt32(txtID.Text)
                    };
                    bool respuesta = new CNUsuarios().eliminar(objusuario, out mensaje);

                    if (respuesta)
                    {
                        dataGridUsers.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                    }
                }
            }
            Limpiar();
        }
        private void pictureBox3_Click(object sender, EventArgs e)//boton Buscar
        {
            string filtro = ((OpcionesDeCombo)comboBusqueda.SelectedItem).valor.ToString();
            if (dataGridUsers.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridUsers.Rows)
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

        private void pictureBox2_Click(object sender, EventArgs e)//bton Limpiar Busqueda
        {
            textBusquedas.Text = "";
            foreach (DataGridViewRow row in dataGridUsers.Rows)
            {
                row.Visible = true;
            }
        }

        private void textBusquedas_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string filtro = ((OpcionesDeCombo)comboBusqueda.SelectedItem).valor.ToString();
                if (dataGridUsers.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dataGridUsers.Rows)
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
            catch (Exception c)
            {
                MessageBox.Show(c.ToString());
            }

        }

        private void textPrimerNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                // Si no es una letra, suprimir el carácter
                e.Handled = true;
            }
        }
        private void textSegundoNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Space && e.KeyChar != (char)Keys.Back)
            {
                // Si no es una letra, suprimir el carácter
                e.Handled = true;
            }
        }
        private void textApellido1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                // Si no es una letra, suprimir el carácter
                e.Handled = true;
            }
        }
        private void textApellido2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                // Si no es una letra, suprimir el carácter
                e.Handled = true;
            }

        }
    }
}
