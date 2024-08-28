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
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            columnHeader11 = new ColumnHeader();
            columnHeader12 = new ColumnHeader();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            btnBuscarEmpleado = new Button();
            btnEditar = new Button();
            btnEliminar = new Button();
            groupBoxInicio = new GroupBox();
            progressBarCargando = new ProgressBar();
            tabControlEmpleados = new TabControl();
            tabPageEmpleados = new TabPage();
            pictureBox5 = new PictureBox();
            pictureBox4 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox2 = new PictureBox();
            label3 = new Label();
            txtIdEmpleado = new TextBox();
            tabPageRegistros = new TabPage();
            pictureBox6 = new PictureBox();
            txtHorasTotales = new TextBox();
            label6 = new Label();
            tabPageConfig = new TabPage();
            pictureBox1 = new PictureBox();
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            listBoxLog = new ListBox();
            groupBoxInicio.SuspendLayout();
            tabControlEmpleados.SuspendLayout();
            tabPageEmpleados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            tabPageRegistros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            tabPageConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btnBuscarRegistros
            // 
            btnBuscarRegistros.Anchor = AnchorStyles.Top;
            btnBuscarRegistros.Location = new Point(709, 42);
            btnBuscarRegistros.Name = "btnBuscarRegistros";
            btnBuscarRegistros.Size = new Size(161, 32);
            btnBuscarRegistros.TabIndex = 0;
            btnBuscarRegistros.Text = "Buscar registros";
            btnBuscarRegistros.UseVisualStyleBackColor = true;
            btnBuscarRegistros.Click += btnBuscarRegistros_Click;
            // 
            // btnConectarDispositivo
            // 
            btnConectarDispositivo.Anchor = AnchorStyles.None;
            btnConectarDispositivo.Font = new Font("Segoe UI", 12F);
            btnConectarDispositivo.Location = new Point(459, 282);
            btnConectarDispositivo.Name = "btnConectarDispositivo";
            btnConectarDispositivo.Size = new Size(225, 56);
            btnConectarDispositivo.TabIndex = 2;
            btnConectarDispositivo.Text = "Conectar dispositivo";
            btnConectarDispositivo.UseVisualStyleBackColor = true;
            btnConectarDispositivo.Click += btnConectarDispositivo_Click;
            // 
            // textBoxIpDispositivo
            // 
            textBoxIpDispositivo.Anchor = AnchorStyles.None;
            textBoxIpDispositivo.Font = new Font("Segoe UI", 12F);
            textBoxIpDispositivo.Location = new Point(551, 146);
            textBoxIpDispositivo.Name = "textBoxIpDispositivo";
            textBoxIpDispositivo.Size = new Size(133, 34);
            textBoxIpDispositivo.TabIndex = 3;
            textBoxIpDispositivo.Text = "192.168.1.218";
            // 
            // textBoxPuertoDispositivo
            // 
            textBoxPuertoDispositivo.Anchor = AnchorStyles.None;
            textBoxPuertoDispositivo.Font = new Font("Segoe UI", 12F);
            textBoxPuertoDispositivo.Location = new Point(587, 207);
            textBoxPuertoDispositivo.Name = "textBoxPuertoDispositivo";
            textBoxPuertoDispositivo.Size = new Size(97, 34);
            textBoxPuertoDispositivo.TabIndex = 4;
            textBoxPuertoDispositivo.Text = "5010";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(499, 146);
            label1.Name = "label1";
            label1.Size = new Size(32, 28);
            label1.TabIndex = 5;
            label1.Text = "IP:";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(459, 207);
            label2.Name = "label2";
            label2.Size = new Size(74, 28);
            label2.TabIndex = 6;
            label2.Text = "Puerto:";
            // 
            // btnConfigRed
            // 
            btnConfigRed.Anchor = AnchorStyles.None;
            btnConfigRed.Location = new Point(30, 335);
            btnConfigRed.Name = "btnConfigRed";
            btnConfigRed.Size = new Size(194, 44);
            btnConfigRed.TabIndex = 9;
            btnConfigRed.Text = "Config. de red";
            btnConfigRed.UseVisualStyleBackColor = true;
            btnConfigRed.Click += btnConfigRed_Click;
            // 
            // textBoxIp
            // 
            textBoxIp.Anchor = AnchorStyles.None;
            textBoxIp.Location = new Point(30, 40);
            textBoxIp.Name = "textBoxIp";
            textBoxIp.ReadOnly = true;
            textBoxIp.Size = new Size(169, 27);
            textBoxIp.TabIndex = 15;
            // 
            // textBoxServIp
            // 
            textBoxServIp.Anchor = AnchorStyles.None;
            textBoxServIp.Location = new Point(30, 230);
            textBoxServIp.Name = "textBoxServIp";
            textBoxServIp.ReadOnly = true;
            textBoxServIp.Size = new Size(169, 27);
            textBoxServIp.TabIndex = 16;
            // 
            // textBoxGw
            // 
            textBoxGw.Anchor = AnchorStyles.None;
            textBoxGw.Location = new Point(30, 167);
            textBoxGw.Name = "textBoxGw";
            textBoxGw.ReadOnly = true;
            textBoxGw.Size = new Size(169, 27);
            textBoxGw.TabIndex = 17;
            // 
            // textBoxMask
            // 
            textBoxMask.Anchor = AnchorStyles.None;
            textBoxMask.Location = new Point(30, 105);
            textBoxMask.Name = "textBoxMask";
            textBoxMask.ReadOnly = true;
            textBoxMask.Size = new Size(169, 27);
            textBoxMask.TabIndex = 18;
            // 
            // textBoxPort
            // 
            textBoxPort.Anchor = AnchorStyles.None;
            textBoxPort.Location = new Point(30, 292);
            textBoxPort.Name = "textBoxPort";
            textBoxPort.ReadOnly = true;
            textBoxPort.Size = new Size(169, 27);
            textBoxPort.TabIndex = 19;
            // 
            // btnSincHora
            // 
            btnSincHora.Anchor = AnchorStyles.None;
            btnSincHora.Location = new Point(30, 452);
            btnSincHora.Name = "btnSincHora";
            btnSincHora.Size = new Size(194, 42);
            btnSincHora.TabIndex = 22;
            btnSincHora.Text = "Sincronizar hora";
            btnSincHora.UseVisualStyleBackColor = true;
            btnSincHora.Click += btnSincHora_Click;
            // 
            // txtId
            // 
            txtId.Anchor = AnchorStyles.Top;
            txtId.Font = new Font("Segoe UI", 10F);
            txtId.Location = new Point(219, 44);
            txtId.Name = "txtId";
            txtId.Size = new Size(70, 30);
            txtId.TabIndex = 23;
            // 
            // dateTimeFechaInicio
            // 
            dateTimeFechaInicio.Anchor = AnchorStyles.Top;
            dateTimeFechaInicio.Font = new Font("Segoe UI", 10F);
            dateTimeFechaInicio.Location = new Point(367, 44);
            dateTimeFechaInicio.Name = "dateTimeFechaInicio";
            dateTimeFechaInicio.Size = new Size(122, 30);
            dateTimeFechaInicio.TabIndex = 24;
            // 
            // dateTimeFechaFinal
            // 
            dateTimeFechaFinal.Anchor = AnchorStyles.Top;
            dateTimeFechaFinal.Font = new Font("Segoe UI", 10F);
            dateTimeFechaFinal.Location = new Point(568, 44);
            dateTimeFechaFinal.Name = "dateTimeFechaFinal";
            dateTimeFechaFinal.Size = new Size(124, 30);
            dateTimeFechaFinal.TabIndex = 25;
            // 
            // id
            // 
            id.Anchor = AnchorStyles.Top;
            id.AutoSize = true;
            id.Font = new Font("Segoe UI", 10F);
            id.Location = new Point(99, 51);
            id.Name = "id";
            id.Size = new Size(111, 23);
            id.TabIndex = 26;
            id.Text = "Nombre / ID:";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10F);
            label4.Location = new Point(300, 51);
            label4.Name = "label4";
            label4.Size = new Size(61, 23);
            label4.TabIndex = 27;
            label4.Text = "Desde:";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top;
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10F);
            label5.Location = new Point(505, 51);
            label5.Name = "label5";
            label5.Size = new Size(57, 23);
            label5.TabIndex = 28;
            label5.Text = "Hasta:";
            // 
            // listViewEmpleados
            // 
            listViewEmpleados.Anchor = AnchorStyles.None;
            listViewEmpleados.Columns.AddRange(new ColumnHeader[] { columnHeader5, columnHeader6, columnHeader7, columnHeader8 });
            listViewEmpleados.FullRowSelect = true;
            listViewEmpleados.GridLines = true;
            listViewEmpleados.Location = new Point(505, 11);
            listViewEmpleados.Name = "listViewEmpleados";
            listViewEmpleados.Size = new Size(601, 505);
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
            listViewRegistros.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            listViewRegistros.BorderStyle = BorderStyle.FixedSingle;
            listViewRegistros.Columns.AddRange(new ColumnHeader[] { columnHeader10, columnHeader3, columnHeader4, columnHeader11, columnHeader12, columnHeader1, columnHeader2 });
            listViewRegistros.FullRowSelect = true;
            listViewRegistros.GridLines = true;
            listViewRegistros.Location = new Point(111, 92);
            listViewRegistros.Name = "listViewRegistros";
            listViewRegistros.Size = new Size(752, 410);
            listViewRegistros.TabIndex = 30;
            listViewRegistros.UseCompatibleStateImageBehavior = false;
            listViewRegistros.View = View.Details;
            // 
            // columnHeader10
            // 
            columnHeader10.Text = "ID";
            columnHeader10.Width = 55;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Nombre";
            columnHeader3.Width = 180;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Área";
            columnHeader4.Width = 150;
            // 
            // columnHeader11
            // 
            columnHeader11.Text = "Dia";
            columnHeader11.Width = 100;
            // 
            // columnHeader12
            // 
            columnHeader12.Text = "Entrada";
            columnHeader12.Width = 100;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Salida";
            columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Horas";
            // 
            // btnBuscarEmpleado
            // 
            btnBuscarEmpleado.Anchor = AnchorStyles.None;
            btnBuscarEmpleado.Font = new Font("Segoe UI", 10F);
            btnBuscarEmpleado.Location = new Point(55, 271);
            btnBuscarEmpleado.Name = "btnBuscarEmpleado";
            btnBuscarEmpleado.Size = new Size(125, 33);
            btnBuscarEmpleado.TabIndex = 31;
            btnBuscarEmpleado.Text = "Buscar";
            btnBuscarEmpleado.UseVisualStyleBackColor = true;
            btnBuscarEmpleado.Click += btnBuscarEmpleadoPorId_Click;
            // 
            // btnEditar
            // 
            btnEditar.Anchor = AnchorStyles.None;
            btnEditar.Font = new Font("Segoe UI", 10F);
            btnEditar.Location = new Point(213, 271);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(122, 33);
            btnEditar.TabIndex = 32;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Anchor = AnchorStyles.None;
            btnEliminar.Font = new Font("Segoe UI", 10F);
            btnEliminar.Location = new Point(363, 271);
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
            groupBoxInicio.Controls.Add(progressBarCargando);
            groupBoxInicio.Controls.Add(label1);
            groupBoxInicio.Controls.Add(btnConectarDispositivo);
            groupBoxInicio.Controls.Add(textBoxIpDispositivo);
            groupBoxInicio.Controls.Add(textBoxPuertoDispositivo);
            groupBoxInicio.Controls.Add(label2);
            groupBoxInicio.ImeMode = ImeMode.NoControl;
            groupBoxInicio.Location = new Point(-4, -3);
            groupBoxInicio.Name = "groupBoxInicio";
            groupBoxInicio.Size = new Size(1131, 559);
            groupBoxInicio.TabIndex = 35;
            groupBoxInicio.TabStop = false;
            // 
            // progressBarCargando
            // 
            progressBarCargando.Location = new Point(459, 429);
            progressBarCargando.Name = "progressBarCargando";
            progressBarCargando.Size = new Size(225, 29);
            progressBarCargando.TabIndex = 7;
            progressBarCargando.Visible = false;
            // 
            // tabControlEmpleados
            // 
            tabControlEmpleados.AccessibleName = "";
            tabControlEmpleados.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControlEmpleados.Controls.Add(tabPageEmpleados);
            tabControlEmpleados.Controls.Add(tabPageRegistros);
            tabControlEmpleados.Controls.Add(tabPageConfig);
            tabControlEmpleados.Font = new Font("Segoe UI", 11F);
            tabControlEmpleados.Location = new Point(-4, -3);
            tabControlEmpleados.Multiline = true;
            tabControlEmpleados.Name = "tabControlEmpleados";
            tabControlEmpleados.Padding = new Point(10, 3);
            tabControlEmpleados.SelectedIndex = 0;
            tabControlEmpleados.Size = new Size(1131, 559);
            tabControlEmpleados.TabIndex = 36;
            // 
            // tabPageEmpleados
            // 
            tabPageEmpleados.BackColor = Color.Transparent;
            tabPageEmpleados.Controls.Add(pictureBox5);
            tabPageEmpleados.Controls.Add(pictureBox4);
            tabPageEmpleados.Controls.Add(pictureBox3);
            tabPageEmpleados.Controls.Add(pictureBox2);
            tabPageEmpleados.Controls.Add(label3);
            tabPageEmpleados.Controls.Add(txtIdEmpleado);
            tabPageEmpleados.Controls.Add(listViewEmpleados);
            tabPageEmpleados.Controls.Add(btnEliminar);
            tabPageEmpleados.Controls.Add(btnBuscarEmpleado);
            tabPageEmpleados.Controls.Add(btnEditar);
            tabPageEmpleados.Font = new Font("Segoe UI", 9F);
            tabPageEmpleados.Location = new Point(4, 34);
            tabPageEmpleados.Name = "tabPageEmpleados";
            tabPageEmpleados.Padding = new Padding(3);
            tabPageEmpleados.Size = new Size(1123, 521);
            tabPageEmpleados.TabIndex = 0;
            tabPageEmpleados.Text = "Empleados";
            // 
            // pictureBox5
            // 
            pictureBox5.Anchor = AnchorStyles.None;
            pictureBox5.Image = Properties.Resources.editar;
            pictureBox5.Location = new Point(213, 320);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(122, 36);
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.TabIndex = 40;
            pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.Anchor = AnchorStyles.None;
            pictureBox4.Image = Properties.Resources.eliminar_usuario;
            pictureBox4.Location = new Point(363, 320);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(118, 36);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 39;
            pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Anchor = AnchorStyles.None;
            pictureBox3.Image = Properties.Resources.buscar;
            pictureBox3.Location = new Point(55, 320);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(125, 36);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 38;
            pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Anchor = AnchorStyles.None;
            pictureBox2.Image = Properties.Resources.user_interface;
            pictureBox2.Location = new Point(213, 68);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(163, 78);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 37;
            pictureBox2.TabStop = false;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10F);
            label3.Location = new Point(57, 200);
            label3.Name = "label3";
            label3.Size = new Size(158, 23);
            label3.TabIndex = 36;
            label3.Text = "Nombre / DNI / ID:";
            // 
            // txtIdEmpleado
            // 
            txtIdEmpleado.Anchor = AnchorStyles.None;
            txtIdEmpleado.Font = new Font("Segoe UI", 10F);
            txtIdEmpleado.Location = new Point(230, 193);
            txtIdEmpleado.Name = "txtIdEmpleado";
            txtIdEmpleado.Size = new Size(125, 30);
            txtIdEmpleado.TabIndex = 35;
            // 
            // tabPageRegistros
            // 
            tabPageRegistros.Controls.Add(pictureBox6);
            tabPageRegistros.Controls.Add(txtHorasTotales);
            tabPageRegistros.Controls.Add(label6);
            tabPageRegistros.Controls.Add(txtId);
            tabPageRegistros.Controls.Add(btnBuscarRegistros);
            tabPageRegistros.Controls.Add(dateTimeFechaInicio);
            tabPageRegistros.Controls.Add(dateTimeFechaFinal);
            tabPageRegistros.Controls.Add(id);
            tabPageRegistros.Controls.Add(label4);
            tabPageRegistros.Controls.Add(listViewRegistros);
            tabPageRegistros.Controls.Add(label5);
            tabPageRegistros.Font = new Font("Segoe UI", 9F);
            tabPageRegistros.Location = new Point(4, 34);
            tabPageRegistros.Name = "tabPageRegistros";
            tabPageRegistros.Padding = new Padding(3);
            tabPageRegistros.Size = new Size(1123, 521);
            tabPageRegistros.TabIndex = 1;
            tabPageRegistros.Text = "Registros";
            tabPageRegistros.UseVisualStyleBackColor = true;
            // 
            // pictureBox6
            // 
            pictureBox6.Anchor = AnchorStyles.None;
            pictureBox6.Image = Properties.Resources.registro_hora;
            pictureBox6.Location = new Point(886, 42);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(206, 165);
            pictureBox6.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox6.TabIndex = 33;
            pictureBox6.TabStop = false;
            // 
            // txtHorasTotales
            // 
            txtHorasTotales.Anchor = AnchorStyles.None;
            txtHorasTotales.Font = new Font("Segoe UI", 12F);
            txtHorasTotales.Location = new Point(953, 272);
            txtHorasTotales.MaxLength = 10;
            txtHorasTotales.Name = "txtHorasTotales";
            txtHorasTotales.ReadOnly = true;
            txtHorasTotales.Size = new Size(72, 34);
            txtHorasTotales.TabIndex = 32;
            txtHorasTotales.TextAlign = HorizontalAlignment.Center;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.None;
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F);
            label6.Location = new Point(869, 230);
            label6.Name = "label6";
            label6.Size = new Size(223, 28);
            label6.TabIndex = 31;
            label6.Text = "Horas totales trabajadas";
            // 
            // tabPageConfig
            // 
            tabPageConfig.Controls.Add(pictureBox1);
            tabPageConfig.Controls.Add(label11);
            tabPageConfig.Controls.Add(label10);
            tabPageConfig.Controls.Add(label9);
            tabPageConfig.Controls.Add(label8);
            tabPageConfig.Controls.Add(label7);
            tabPageConfig.Controls.Add(listBoxLog);
            tabPageConfig.Controls.Add(textBoxMask);
            tabPageConfig.Controls.Add(btnConfigRed);
            tabPageConfig.Controls.Add(textBoxIp);
            tabPageConfig.Controls.Add(textBoxServIp);
            tabPageConfig.Controls.Add(textBoxGw);
            tabPageConfig.Controls.Add(textBoxPort);
            tabPageConfig.Controls.Add(btnSincHora);
            tabPageConfig.Font = new Font("Segoe UI", 9F);
            tabPageConfig.Location = new Point(4, 34);
            tabPageConfig.Name = "tabPageConfig";
            tabPageConfig.Size = new Size(1123, 521);
            tabPageConfig.TabIndex = 2;
            tabPageConfig.Text = "Configuración";
            tabPageConfig.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.None;
            pictureBox1.Image = Properties.Resources.reloj;
            pictureBox1.Location = new Point(73, 410);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(106, 36);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 29;
            pictureBox1.TabStop = false;
            // 
            // label11
            // 
            label11.Anchor = AnchorStyles.None;
            label11.AutoSize = true;
            label11.Location = new Point(30, 269);
            label11.Name = "label11";
            label11.Size = new Size(55, 20);
            label11.TabIndex = 28;
            label11.Text = "Puerto:";
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.None;
            label10.AutoSize = true;
            label10.Location = new Point(30, 207);
            label10.Name = "label10";
            label10.Size = new Size(69, 20);
            label10.TabIndex = 27;
            label10.Text = "Server IP:";
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.None;
            label9.AutoSize = true;
            label9.Location = new Point(30, 144);
            label9.Name = "label9";
            label9.Size = new Size(122, 20);
            label9.TabIndex = 26;
            label9.Text = "Puerta de enlace:";
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.None;
            label8.AutoSize = true;
            label8.Location = new Point(30, 82);
            label8.Name = "label8";
            label8.Size = new Size(137, 20);
            label8.TabIndex = 25;
            label8.Text = "Máscara de subred:";
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.None;
            label7.AutoSize = true;
            label7.Location = new Point(30, 17);
            label7.Name = "label7";
            label7.Size = new Size(91, 20);
            label7.TabIndex = 24;
            label7.Text = "Dirección IP:";
            // 
            // listBoxLog
            // 
            listBoxLog.Anchor = AnchorStyles.None;
            listBoxLog.BorderStyle = BorderStyle.FixedSingle;
            listBoxLog.Font = new Font("Segoe UI", 9F);
            listBoxLog.FormattingEnabled = true;
            listBoxLog.HorizontalScrollbar = true;
            listBoxLog.Location = new Point(249, 7);
            listBoxLog.Name = "listBoxLog";
            listBoxLog.SelectionMode = SelectionMode.None;
            listBoxLog.Size = new Size(867, 502);
            listBoxLog.TabIndex = 23;
            // 
            // BusquedaRegistrosView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1128, 557);
            Controls.Add(groupBoxInicio);
            Controls.Add(tabControlEmpleados);
            Name = "BusquedaRegistrosView";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Busqueda de registros";
            groupBoxInicio.ResumeLayout(false);
            groupBoxInicio.PerformLayout();
            tabControlEmpleados.ResumeLayout(false);
            tabPageEmpleados.ResumeLayout(false);
            tabPageEmpleados.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            tabPageRegistros.ResumeLayout(false);
            tabPageRegistros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            tabPageConfig.ResumeLayout(false);
            tabPageConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
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
        private Button btnEliminar;
        private GroupBox groupBoxInicio;
        private TabControl tabControlEmpleados;
        private TabPage tabPageEmpleados;
        private TabPage tabPageRegistros;
        private TabPage tabPageConfig;
        private Label label3;
        private TextBox txtIdEmpleado;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private TextBox txtHorasTotales;
        private Label label6;
        private ProgressBar progressBarCargando;
        private ListBox listBoxLog;
        private Label label11;
        private Label label10;
        private Label label9;
        private Label label8;
        private Label label7;
        private PictureBox pictureBox1;
        private PictureBox pictureBox3;
        private PictureBox pictureBox2;
        private PictureBox pictureBox5;
        private PictureBox pictureBox4;
        private PictureBox pictureBox6;
    }
}
