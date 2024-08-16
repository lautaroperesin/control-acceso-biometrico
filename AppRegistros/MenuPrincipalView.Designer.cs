namespace AppRegistros
{
    partial class BusquedaRegistrosView
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnBuscarRegistros = new Button();
            btnConectarDispositivo = new Button();
            textBoxIpDispositivo = new TextBox();
            textBoxPuertoDispositivo = new TextBox();
            label1 = new Label();
            label2 = new Label();
            btnConfigRed = new Button();
            textBoxIp = new TextBox();
            textBoxServIp = new TextBox();
            textBoxGw = new TextBox();
            textBoxMask = new TextBox();
            textBoxPort = new TextBox();
            btnObtenerEmpleados = new Button();
            btnSincHora = new Button();
            txtId = new TextBox();
            dateTimeFechaInicio = new DateTimePicker();
            dateTimeFechaFinal = new DateTimePicker();
            id = new Label();
            label4 = new Label();
            label5 = new Label();
            listViewEmpleados = new ListView();
            columnHeader5 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            columnHeader7 = new ColumnHeader();
            columnHeader8 = new ColumnHeader();
            listViewRegistros = new ListView();
            columnHeader10 = new ColumnHeader();
            columnHeader11 = new ColumnHeader();
            columnHeader12 = new ColumnHeader();
            columnHeader1 = new ColumnHeader();
            btnBuscarEmpleado = new Button();
            btnEditar = new Button();
            dataGridEmpleados = new DataGridView();
            btnEliminar = new Button();
            groupBoxInicio = new GroupBox();
            tabControlEmpleados = new TabControl();
            tabPageEmpleados = new TabPage();
            label3 = new Label();
            txtIdEmpleado = new TextBox();
            tabPageRegistros = new TabPage();
            tabPageConfig = new TabPage();
            ((System.ComponentModel.ISupportInitialize)dataGridEmpleados).BeginInit();
            groupBoxInicio.SuspendLayout();
            tabControlEmpleados.SuspendLayout();
            tabPageEmpleados.SuspendLayout();
            tabPageRegistros.SuspendLayout();
            tabPageConfig.SuspendLayout();
            SuspendLayout();
            // 
            // btnBuscarRegistros
            // 
            btnBuscarRegistros.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnBuscarRegistros.Location = new Point(141, 385);
            btnBuscarRegistros.Name = "btnBuscarRegistros";
            btnBuscarRegistros.Size = new Size(178, 29);
            btnBuscarRegistros.TabIndex = 0;
            btnBuscarRegistros.Text = "Buscar registros";
            btnBuscarRegistros.UseVisualStyleBackColor = true;
            btnBuscarRegistros.Click += btnBuscarRegistros_Click;
            // 
            // btnConectarDispositivo
            // 
            btnConectarDispositivo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnConectarDispositivo.Font = new Font("Segoe UI", 12F);
            btnConectarDispositivo.Location = new Point(434, 273);
            btnConectarDispositivo.Name = "btnConectarDispositivo";
            btnConectarDispositivo.Size = new Size(234, 53);
            btnConectarDispositivo.TabIndex = 2;
            btnConectarDispositivo.Text = "Conectar dispositivo";
            btnConectarDispositivo.UseVisualStyleBackColor = true;
            btnConectarDispositivo.Click += btnConectarDispositivo_Click;
            // 
            // textBoxIpDispositivo
            // 
            textBoxIpDispositivo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxIpDispositivo.Font = new Font("Segoe UI", 12F);
            textBoxIpDispositivo.Location = new Point(526, 137);
            textBoxIpDispositivo.Name = "textBoxIpDispositivo";
            textBoxIpDispositivo.Size = new Size(142, 34);
            textBoxIpDispositivo.TabIndex = 3;
            textBoxIpDispositivo.Text = "192.168.1.218";
            // 
            // textBoxPuertoDispositivo
            // 
            textBoxPuertoDispositivo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxPuertoDispositivo.Font = new Font("Segoe UI", 12F);
            textBoxPuertoDispositivo.Location = new Point(562, 198);
            textBoxPuertoDispositivo.Name = "textBoxPuertoDispositivo";
            textBoxPuertoDispositivo.Size = new Size(106, 34);
            textBoxPuertoDispositivo.TabIndex = 4;
            textBoxPuertoDispositivo.Text = "5010";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(474, 137);
            label1.Name = "label1";
            label1.Size = new Size(32, 28);
            label1.TabIndex = 5;
            label1.Text = "IP:";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(434, 198);
            label2.Name = "label2";
            label2.Size = new Size(74, 28);
            label2.TabIndex = 6;
            label2.Text = "Puerto:";
            // 
            // btnConfigRed
            // 
            btnConfigRed.Location = new Point(109, 226);
            btnConfigRed.Name = "btnConfigRed";
            btnConfigRed.Size = new Size(119, 34);
            btnConfigRed.TabIndex = 9;
            btnConfigRed.Text = "Config. de red";
            btnConfigRed.UseVisualStyleBackColor = true;
            btnConfigRed.Click += btnConfigRed_Click;
            // 
            // textBoxIp
            // 
            textBoxIp.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            textBoxIp.Location = new Point(109, 61);
            textBoxIp.Name = "textBoxIp";
            textBoxIp.Size = new Size(119, 27);
            textBoxIp.TabIndex = 15;
            // 
            // textBoxServIp
            // 
            textBoxServIp.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            textBoxServIp.Location = new Point(109, 160);
            textBoxServIp.Name = "textBoxServIp";
            textBoxServIp.Size = new Size(119, 27);
            textBoxServIp.TabIndex = 16;
            // 
            // textBoxGw
            // 
            textBoxGw.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            textBoxGw.Location = new Point(109, 127);
            textBoxGw.Name = "textBoxGw";
            textBoxGw.Size = new Size(119, 27);
            textBoxGw.TabIndex = 17;
            // 
            // textBoxMask
            // 
            textBoxMask.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            textBoxMask.Location = new Point(109, 94);
            textBoxMask.Name = "textBoxMask";
            textBoxMask.Size = new Size(119, 27);
            textBoxMask.TabIndex = 18;
            // 
            // textBoxPort
            // 
            textBoxPort.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            textBoxPort.Location = new Point(109, 193);
            textBoxPort.Name = "textBoxPort";
            textBoxPort.Size = new Size(119, 27);
            textBoxPort.TabIndex = 19;
            // 
            // btnObtenerEmpleados
            // 
            btnObtenerEmpleados.Anchor = AnchorStyles.None;
            btnObtenerEmpleados.Location = new Point(817, 137);
            btnObtenerEmpleados.Name = "btnObtenerEmpleados";
            btnObtenerEmpleados.Size = new Size(230, 38);
            btnObtenerEmpleados.TabIndex = 20;
            btnObtenerEmpleados.Text = "Obtener empleados";
            btnObtenerEmpleados.UseVisualStyleBackColor = true;
            btnObtenerEmpleados.Click += btnObtenerEmpleados_Click;
            // 
            // btnSincHora
            // 
            btnSincHora.Location = new Point(102, 274);
            btnSincHora.Name = "btnSincHora";
            btnSincHora.Size = new Size(132, 34);
            btnSincHora.TabIndex = 22;
            btnSincHora.Text = "Sincronizar hora";
            btnSincHora.UseVisualStyleBackColor = true;
            btnSincHora.Click += btnSincHora_Click;
            // 
            // txtId
            // 
            txtId.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtId.Location = new Point(193, 280);
            txtId.Name = "txtId";
            txtId.Size = new Size(70, 27);
            txtId.TabIndex = 23;
            // 
            // dateTimeFechaInicio
            // 
            dateTimeFechaInicio.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            dateTimeFechaInicio.Location = new Point(195, 313);
            dateTimeFechaInicio.Name = "dateTimeFechaInicio";
            dateTimeFechaInicio.Size = new Size(122, 27);
            dateTimeFechaInicio.TabIndex = 24;
            // 
            // dateTimeFechaFinal
            // 
            dateTimeFechaFinal.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            dateTimeFechaFinal.Location = new Point(193, 346);
            dateTimeFechaFinal.Name = "dateTimeFechaFinal";
            dateTimeFechaFinal.Size = new Size(124, 27);
            dateTimeFechaFinal.TabIndex = 25;
            // 
            // id
            // 
            id.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            id.AutoSize = true;
            id.Location = new Point(160, 288);
            id.Name = "id";
            id.Size = new Size(27, 20);
            id.TabIndex = 26;
            id.Text = "ID:";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Location = new Point(99, 318);
            label4.Name = "label4";
            label4.Size = new Size(90, 20);
            label4.TabIndex = 27;
            label4.Text = "Fecha inicio:";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label5.AutoSize = true;
            label5.Location = new Point(104, 353);
            label5.Name = "label5";
            label5.Size = new Size(83, 20);
            label5.TabIndex = 28;
            label5.Text = "Fecha final:";
            // 
            // listViewEmpleados
            // 
            listViewEmpleados.Anchor = AnchorStyles.None;
            listViewEmpleados.Columns.AddRange(new ColumnHeader[] { columnHeader5, columnHeader6, columnHeader7, columnHeader8 });
            listViewEmpleados.FullRowSelect = true;
            listViewEmpleados.GridLines = true;
            listViewEmpleados.Location = new Point(198, 36);
            listViewEmpleados.Name = "listViewEmpleados";
            listViewEmpleados.Size = new Size(598, 139);
            listViewEmpleados.TabIndex = 29;
            listViewEmpleados.UseCompatibleStateImageBehavior = false;
            listViewEmpleados.View = View.Details;
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "ID";
            columnHeader5.Width = 110;
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "Nombre";
            columnHeader6.Width = 150;
            // 
            // columnHeader7
            // 
            columnHeader7.Text = "DNI";
            columnHeader7.Width = 150;
            // 
            // columnHeader8
            // 
            columnHeader8.Text = "Área de trabajo";
            columnHeader8.Width = 180;
            // 
            // listViewRegistros
            // 
            listViewRegistros.Anchor = AnchorStyles.None;
            listViewRegistros.BorderStyle = BorderStyle.FixedSingle;
            listViewRegistros.Columns.AddRange(new ColumnHeader[] { columnHeader10, columnHeader11, columnHeader12, columnHeader1 });
            listViewRegistros.FullRowSelect = true;
            listViewRegistros.GridLines = true;
            listViewRegistros.Location = new Point(450, 159);
            listViewRegistros.Name = "listViewRegistros";
            listViewRegistros.Size = new Size(505, 256);
            listViewRegistros.TabIndex = 30;
            listViewRegistros.UseCompatibleStateImageBehavior = false;
            listViewRegistros.View = View.Details;
            // 
            // columnHeader10
            // 
            columnHeader10.Text = "ID";
            columnHeader10.Width = 150;
            // 
            // columnHeader11
            // 
            columnHeader11.Text = "Fecha";
            columnHeader11.Width = 150;
            // 
            // columnHeader12
            // 
            columnHeader12.Text = "Tipo de reg.";
            columnHeader12.Width = 100;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Horas totales";
            columnHeader1.Width = 100;
            // 
            // btnBuscarEmpleado
            // 
            btnBuscarEmpleado.Anchor = AnchorStyles.None;
            btnBuscarEmpleado.Location = new Point(522, 253);
            btnBuscarEmpleado.Name = "btnBuscarEmpleado";
            btnBuscarEmpleado.Size = new Size(142, 33);
            btnBuscarEmpleado.TabIndex = 31;
            btnBuscarEmpleado.Text = "Buscar empelado";
            btnBuscarEmpleado.UseVisualStyleBackColor = true;
            btnBuscarEmpleado.Click += btnBuscarEmpleadoPorId_Click;
            // 
            // btnEditar
            // 
            btnEditar.Anchor = AnchorStyles.None;
            btnEditar.Location = new Point(674, 253);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(122, 33);
            btnEditar.TabIndex = 32;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // dataGridEmpleados
            // 
            dataGridEmpleados.BackgroundColor = SystemColors.ButtonFace;
            dataGridEmpleados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridEmpleados.Location = new Point(23, 292);
            dataGridEmpleados.Name = "dataGridEmpleados";
            dataGridEmpleados.RowHeadersWidth = 51;
            dataGridEmpleados.Size = new Size(416, 198);
            dataGridEmpleados.TabIndex = 33;
            // 
            // btnEliminar
            // 
            btnEliminar.Anchor = AnchorStyles.None;
            btnEliminar.Location = new Point(802, 253);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(118, 33);
            btnEliminar.TabIndex = 34;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // groupBoxInicio
            // 
            groupBoxInicio.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxInicio.Controls.Add(label1);
            groupBoxInicio.Controls.Add(btnConectarDispositivo);
            groupBoxInicio.Controls.Add(textBoxIpDispositivo);
            groupBoxInicio.Controls.Add(textBoxPuertoDispositivo);
            groupBoxInicio.Controls.Add(label2);
            groupBoxInicio.ImeMode = ImeMode.NoControl;
            groupBoxInicio.Location = new Point(-4, -3);
            groupBoxInicio.Name = "groupBoxInicio";
            groupBoxInicio.Size = new Size(1089, 537);
            groupBoxInicio.TabIndex = 35;
            groupBoxInicio.TabStop = false;
            // 
            // tabControlEmpleados
            // 
            tabControlEmpleados.AccessibleName = "";
            tabControlEmpleados.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControlEmpleados.Controls.Add(tabPageEmpleados);
            tabControlEmpleados.Controls.Add(tabPageRegistros);
            tabControlEmpleados.Controls.Add(tabPageConfig);
            tabControlEmpleados.Location = new Point(-4, -3);
            tabControlEmpleados.Multiline = true;
            tabControlEmpleados.Name = "tabControlEmpleados";
            tabControlEmpleados.SelectedIndex = 0;
            tabControlEmpleados.Size = new Size(1089, 537);
            tabControlEmpleados.TabIndex = 36;
            // 
            // tabPageEmpleados
            // 
            tabPageEmpleados.Controls.Add(label3);
            tabPageEmpleados.Controls.Add(txtIdEmpleado);
            tabPageEmpleados.Controls.Add(listViewEmpleados);
            tabPageEmpleados.Controls.Add(btnObtenerEmpleados);
            tabPageEmpleados.Controls.Add(dataGridEmpleados);
            tabPageEmpleados.Controls.Add(btnEliminar);
            tabPageEmpleados.Controls.Add(btnBuscarEmpleado);
            tabPageEmpleados.Controls.Add(btnEditar);
            tabPageEmpleados.Location = new Point(4, 29);
            tabPageEmpleados.Name = "tabPageEmpleados";
            tabPageEmpleados.Padding = new Padding(3);
            tabPageEmpleados.Size = new Size(1081, 504);
            tabPageEmpleados.TabIndex = 0;
            tabPageEmpleados.Text = "Empleados";
            tabPageEmpleados.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.Location = new Point(538, 218);
            label3.Name = "label3";
            label3.Size = new Size(99, 20);
            label3.TabIndex = 36;
            label3.Text = "Nombre o ID:";
            // 
            // txtIdEmpleado
            // 
            txtIdEmpleado.Location = new Point(654, 211);
            txtIdEmpleado.Name = "txtIdEmpleado";
            txtIdEmpleado.Size = new Size(125, 27);
            txtIdEmpleado.TabIndex = 35;
            // 
            // tabPageRegistros
            // 
            tabPageRegistros.Controls.Add(txtId);
            tabPageRegistros.Controls.Add(btnBuscarRegistros);
            tabPageRegistros.Controls.Add(dateTimeFechaInicio);
            tabPageRegistros.Controls.Add(dateTimeFechaFinal);
            tabPageRegistros.Controls.Add(id);
            tabPageRegistros.Controls.Add(label4);
            tabPageRegistros.Controls.Add(listViewRegistros);
            tabPageRegistros.Controls.Add(label5);
            tabPageRegistros.Location = new Point(4, 29);
            tabPageRegistros.Name = "tabPageRegistros";
            tabPageRegistros.Padding = new Padding(3);
            tabPageRegistros.Size = new Size(1081, 504);
            tabPageRegistros.TabIndex = 1;
            tabPageRegistros.Text = "Registros";
            tabPageRegistros.UseVisualStyleBackColor = true;
            // 
            // tabPageConfig
            // 
            tabPageConfig.Controls.Add(textBoxMask);
            tabPageConfig.Controls.Add(btnConfigRed);
            tabPageConfig.Controls.Add(textBoxIp);
            tabPageConfig.Controls.Add(textBoxServIp);
            tabPageConfig.Controls.Add(textBoxGw);
            tabPageConfig.Controls.Add(textBoxPort);
            tabPageConfig.Controls.Add(btnSincHora);
            tabPageConfig.Location = new Point(4, 29);
            tabPageConfig.Name = "tabPageConfig";
            tabPageConfig.Size = new Size(1081, 504);
            tabPageConfig.TabIndex = 2;
            tabPageConfig.Text = "Configuración";
            tabPageConfig.UseVisualStyleBackColor = true;
            // 
            // BusquedaRegistrosView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1086, 535);
            Controls.Add(groupBoxInicio);
            Controls.Add(tabControlEmpleados);
            Name = "BusquedaRegistrosView";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Busqueda de registros";
            ((System.ComponentModel.ISupportInitialize)dataGridEmpleados).EndInit();
            groupBoxInicio.ResumeLayout(false);
            groupBoxInicio.PerformLayout();
            tabControlEmpleados.ResumeLayout(false);
            tabPageEmpleados.ResumeLayout(false);
            tabPageEmpleados.PerformLayout();
            tabPageRegistros.ResumeLayout(false);
            tabPageRegistros.PerformLayout();
            tabPageConfig.ResumeLayout(false);
            tabPageConfig.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btnBuscarRegistros;
        private Button btnConectarDispositivo;
        private TextBox textBoxIpDispositivo;
        private TextBox textBoxPuertoDispositivo;
        private Label label1;
        private Label label2;
        private Button btnConfigRed;
        private TextBox textBoxIp;
        private TextBox textBoxServIp;
        private TextBox textBoxGw;
        private TextBox textBoxMask;
        private TextBox textBoxPort;
        private Button btnObtenerEmpleados;
        private Button btnSincHora;
        private TextBox txtId;
        private DateTimePicker dateTimeFechaInicio;
        private DateTimePicker dateTimeFechaFinal;
        private Label id;
        private Label label4;
        private Label label5;
        private ListView listViewEmpleados;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader8;
        private ListView listViewRegistros;
        private ColumnHeader columnHeader10;
        private ColumnHeader columnHeader11;
        private ColumnHeader columnHeader12;
        private Button btnBuscarEmpleado;
        private Button btnEditar;
        private DataGridView dataGridEmpleados;
        private Button btnEliminar;
        private GroupBox groupBoxInicio;
        private TabControl tabControlEmpleados;
        private TabPage tabPageEmpleados;
        private TabPage tabPageRegistros;
        private TabPage tabPageConfig;
        private Label label3;
        private TextBox txtIdEmpleado;
        private ColumnHeader columnHeader1;
    }
}
