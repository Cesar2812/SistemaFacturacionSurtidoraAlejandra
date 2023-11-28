namespace SistemaSurtidoraAlejandra
{
    partial class frmCompras
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCompras));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textNombre = new System.Windows.Forms.TextBox();
            this.textDocumento = new System.Windows.Forms.TextBox();
            this.textIdProv = new System.Windows.Forms.TextBox();
            this.btnBuscarProv = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboTipoDoc = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textFecha = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtFcahVen = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textCantida = new System.Windows.Forms.NumericUpDown();
            this.textIdProd = new System.Windows.Forms.TextBox();
            this.textVenta = new System.Windows.Forms.TextBox();
            this.textCompra = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textProd = new System.Windows.Forms.TextBox();
            this.btnbuscarProducto = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.texCodProd = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataCompras = new System.Windows.Forms.DataGridView();
            this.idProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrecioDeCompra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrecioDeVenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaVencimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MontoTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.boton = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label12 = new System.Windows.Forms.Label();
            this.textTotalPagar = new System.Windows.Forms.TextBox();
            this.butregistrar = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnBuscarProv)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textCantida)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnbuscarProducto)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataCompras)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textNombre);
            this.groupBox2.Controls.Add(this.textDocumento);
            this.groupBox2.Controls.Add(this.textIdProv);
            this.groupBox2.Controls.Add(this.btnBuscarProv);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(365, 47);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(475, 91);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Información Del Proveedor";
            // 
            // textNombre
            // 
            this.textNombre.Location = new System.Drawing.Point(196, 42);
            this.textNombre.Name = "textNombre";
            this.textNombre.Size = new System.Drawing.Size(254, 22);
            this.textNombre.TabIndex = 63;
            this.textNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textNombre_KeyPress);
            // 
            // textDocumento
            // 
            this.textDocumento.Location = new System.Drawing.Point(4, 42);
            this.textDocumento.Name = "textDocumento";
            this.textDocumento.Size = new System.Drawing.Size(136, 22);
            this.textDocumento.TabIndex = 62;
            this.textDocumento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textDocumento_KeyPress);
            // 
            // textIdProv
            // 
            this.textIdProv.Location = new System.Drawing.Point(405, 15);
            this.textIdProv.Name = "textIdProv";
            this.textIdProv.Size = new System.Drawing.Size(36, 22);
            this.textIdProv.TabIndex = 61;
            this.textIdProv.Visible = false;
            // 
            // btnBuscarProv
            // 
            this.btnBuscarProv.BackColor = System.Drawing.SystemColors.Control;
            this.btnBuscarProv.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarProv.Image")));
            this.btnBuscarProv.Location = new System.Drawing.Point(146, 40);
            this.btnBuscarProv.Name = "btnBuscarProv";
            this.btnBuscarProv.Size = new System.Drawing.Size(40, 24);
            this.btnBuscarProv.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnBuscarProv.TabIndex = 59;
            this.btnBuscarProv.TabStop = false;
            this.btnBuscarProv.Click += new System.EventHandler(this.btnBuscarProv_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(201, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Nombre";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 16);
            this.label5.TabIndex = 2;
            this.label5.Text = "N° Documento";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboTipoDoc);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textFecha);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(14, 46);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(345, 91);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información Compra";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(134, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Documento";
            // 
            // comboTipoDoc
            // 
            this.comboTipoDoc.FormattingEnabled = true;
            this.comboTipoDoc.Location = new System.Drawing.Point(137, 42);
            this.comboTipoDoc.Name = "comboTipoDoc";
            this.comboTipoDoc.Size = new System.Drawing.Size(192, 24);
            this.comboTipoDoc.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fecha";
            // 
            // textFecha
            // 
            this.textFecha.Location = new System.Drawing.Point(6, 42);
            this.textFecha.Name = "textFecha";
            this.textFecha.Size = new System.Drawing.Size(125, 22);
            this.textFecha.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(320, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(220, 24);
            this.label4.TabIndex = 7;
            this.label4.Text = "REGISTRAR COMPRA";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(848, 149);
            this.panel1.TabIndex = 9;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.txtFcahVen);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.textCantida);
            this.groupBox3.Controls.Add(this.textIdProd);
            this.groupBox3.Controls.Add(this.textVenta);
            this.groupBox3.Controls.Add(this.textCompra);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.textProd);
            this.groupBox3.Controls.Add(this.btnbuscarProducto);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.texCodProd);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(14, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(826, 91);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Información De Producto";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(642, 19);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(122, 16);
            this.label11.TabIndex = 86;
            this.label11.Text = "Fecha Vencimiento";
            // 
            // txtFcahVen
            // 
            this.txtFcahVen.Location = new System.Drawing.Point(644, 40);
            this.txtFcahVen.Name = "txtFcahVen";
            this.txtFcahVen.Size = new System.Drawing.Size(106, 22);
            this.txtFcahVen.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(769, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(51, 63);
            this.button1.TabIndex = 85;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textCantida
            // 
            this.textCantida.Location = new System.Drawing.Point(584, 41);
            this.textCantida.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.textCantida.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.textCantida.Name = "textCantida";
            this.textCantida.Size = new System.Drawing.Size(46, 22);
            this.textCantida.TabIndex = 69;
            this.textCantida.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // textIdProd
            // 
            this.textIdProd.Location = new System.Drawing.Point(120, 16);
            this.textIdProd.Name = "textIdProd";
            this.textIdProd.Size = new System.Drawing.Size(30, 22);
            this.textIdProd.TabIndex = 68;
            this.textIdProd.Visible = false;
            // 
            // textVenta
            // 
            this.textVenta.Location = new System.Drawing.Point(494, 41);
            this.textVenta.Name = "textVenta";
            this.textVenta.Size = new System.Drawing.Size(82, 22);
            this.textVenta.TabIndex = 67;
            this.textVenta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textVenta_KeyPress);
            // 
            // textCompra
            // 
            this.textCompra.Location = new System.Drawing.Point(391, 41);
            this.textCompra.Name = "textCompra";
            this.textCompra.Size = new System.Drawing.Size(89, 22);
            this.textCompra.TabIndex = 66;
            this.textCompra.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textCompra_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(579, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 16);
            this.label10.TabIndex = 65;
            this.label10.Text = "Cantidad";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(491, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(84, 16);
            this.label9.TabIndex = 64;
            this.label9.Text = "Precio Venta";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(388, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 16);
            this.label8.TabIndex = 63;
            this.label8.Text = "Precio Compra";
            // 
            // textProd
            // 
            this.textProd.Location = new System.Drawing.Point(183, 41);
            this.textProd.Name = "textProd";
            this.textProd.Size = new System.Drawing.Size(198, 22);
            this.textProd.TabIndex = 62;
            this.textProd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textProd_KeyPress);
            // 
            // btnbuscarProducto
            // 
            this.btnbuscarProducto.BackColor = System.Drawing.SystemColors.Control;
            this.btnbuscarProducto.Image = ((System.Drawing.Image)(resources.GetObject("btnbuscarProducto.Image")));
            this.btnbuscarProducto.Location = new System.Drawing.Point(137, 40);
            this.btnbuscarProducto.Name = "btnbuscarProducto";
            this.btnbuscarProducto.Size = new System.Drawing.Size(40, 23);
            this.btnbuscarProducto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnbuscarProducto.TabIndex = 62;
            this.btnbuscarProducto.TabStop = false;
            this.btnbuscarProducto.Click += new System.EventHandler(this.btnbuscarProducto_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(186, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 16);
            this.label6.TabIndex = 4;
            this.label6.Text = "Producto";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 16);
            this.label7.TabIndex = 2;
            this.label7.Text = "Codigo Producto";
            // 
            // texCodProd
            // 
            this.texCodProd.Location = new System.Drawing.Point(6, 41);
            this.texCodProd.Name = "texCodProd";
            this.texCodProd.Size = new System.Drawing.Size(125, 22);
            this.texCodProd.TabIndex = 2;
            this.texCodProd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.texCodProd_KeyDown);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Location = new System.Drawing.Point(12, 167);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(850, 121);
            this.panel2.TabIndex = 11;
            // 
            // dataCompras
            // 
            this.dataCompras.AllowUserToAddRows = false;
            this.dataCompras.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataCompras.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataCompras.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataCompras.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idProducto,
            this.Producto,
            this.PrecioDeCompra,
            this.PrecioDeVenta,
            this.FechaVencimiento,
            this.Cantidad,
            this.MontoTotal,
            this.boton});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataCompras.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataCompras.Location = new System.Drawing.Point(12, 296);
            this.dataCompras.Name = "dataCompras";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataCompras.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataCompras.Size = new System.Drawing.Size(735, 276);
            this.dataCompras.TabIndex = 64;
            this.dataCompras.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataCompras_CellContentClick);
            // 
            // idProducto
            // 
            this.idProducto.HeaderText = "idProducto";
            this.idProducto.Name = "idProducto";
            this.idProducto.Visible = false;
            // 
            // Producto
            // 
            this.Producto.HeaderText = "Producto";
            this.Producto.Name = "Producto";
            this.Producto.Width = 150;
            // 
            // PrecioDeCompra
            // 
            this.PrecioDeCompra.HeaderText = "PrecioCompra";
            this.PrecioDeCompra.Name = "PrecioDeCompra";
            // 
            // PrecioDeVenta
            // 
            this.PrecioDeVenta.HeaderText = "PrecioVenta";
            this.PrecioDeVenta.Name = "PrecioDeVenta";
            this.PrecioDeVenta.Visible = false;
            // 
            // FechaVencimiento
            // 
            this.FechaVencimiento.HeaderText = "FechaVencimiento";
            this.FechaVencimiento.Name = "FechaVencimiento";
            this.FechaVencimiento.Visible = false;
            // 
            // Cantidad
            // 
            this.Cantidad.HeaderText = "Cantidad";
            this.Cantidad.Name = "Cantidad";
            // 
            // MontoTotal
            // 
            this.MontoTotal.HeaderText = "SubTotal";
            this.MontoTotal.Name = "MontoTotal";
            // 
            // boton
            // 
            this.boton.HeaderText = "";
            this.boton.Name = "boton";
            this.boton.Width = 35;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(753, 475);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(89, 16);
            this.label12.TabIndex = 73;
            this.label12.Text = "Total a Pagar";
            // 
            // textTotalPagar
            // 
            this.textTotalPagar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textTotalPagar.Location = new System.Drawing.Point(752, 497);
            this.textTotalPagar.Name = "textTotalPagar";
            this.textTotalPagar.Size = new System.Drawing.Size(132, 22);
            this.textTotalPagar.TabIndex = 72;
            this.textTotalPagar.Text = "0";
            this.textTotalPagar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textTotalPagar_KeyPress);
            // 
            // butregistrar
            // 
            this.butregistrar.BackColor = System.Drawing.Color.SeaGreen;
            this.butregistrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butregistrar.FlatAppearance.BorderSize = 0;
            this.butregistrar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.butregistrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butregistrar.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butregistrar.ForeColor = System.Drawing.Color.White;
            this.butregistrar.Image = ((System.Drawing.Image)(resources.GetObject("butregistrar.Image")));
            this.butregistrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butregistrar.Location = new System.Drawing.Point(752, 522);
            this.butregistrar.Margin = new System.Windows.Forms.Padding(2);
            this.butregistrar.Name = "butregistrar";
            this.butregistrar.Size = new System.Drawing.Size(132, 50);
            this.butregistrar.TabIndex = 74;
            this.butregistrar.Text = "      Guardar";
            this.butregistrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.butregistrar.UseVisualStyleBackColor = false;
            this.butregistrar.Click += new System.EventHandler(this.butregistrar_Click);
            // 
            // frmCompras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 599);
            this.Controls.Add(this.butregistrar);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.textTotalPagar);
            this.Controls.Add(this.dataCompras);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmCompras";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GESTION DE COMPRA";
            this.Load += new System.EventHandler(this.frmCompras_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnBuscarProv)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textCantida)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnbuscarProducto)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataCompras)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textNombre;
        private System.Windows.Forms.TextBox textDocumento;
        private System.Windows.Forms.TextBox textIdProv;
        private System.Windows.Forms.PictureBox btnBuscarProv;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboTipoDoc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textFecha;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown textCantida;
        private System.Windows.Forms.TextBox textIdProd;
        private System.Windows.Forms.TextBox textVenta;
        private System.Windows.Forms.TextBox textCompra;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textProd;
        private System.Windows.Forms.PictureBox btnbuscarProducto;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox texCodProd;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataCompras;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textTotalPagar;
        private System.Windows.Forms.Button butregistrar;
        private System.Windows.Forms.TextBox txtFcahVen;
        private System.Windows.Forms.DataGridViewTextBoxColumn idProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Producto;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioDeCompra;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioDeVenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaVencimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn MontoTotal;
        private System.Windows.Forms.DataGridViewButtonColumn boton;
        private System.Windows.Forms.Label label11;
    }
}