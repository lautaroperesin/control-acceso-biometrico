namespace AppRegistros
{
    partial class EditarEmpleadoView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditarEmpleadoView));
            txtNombreEmpleado = new TextBox();
            txtDocumento = new TextBox();
            txtAreaTrabajo = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            btnGuardar = new Button();
            btnCancelar = new Button();
            SuspendLayout();
            // 
            // txtNombreEmpleado
            // 
            txtNombreEmpleado.Anchor = AnchorStyles.None;
            txtNombreEmpleado.Font = new Font("Microsoft Sans Serif", 12F);
            txtNombreEmpleado.Location = new Point(307, 106);
            txtNombreEmpleado.Margin = new Padding(3, 2, 3, 2);
            txtNombreEmpleado.Name = "txtNombreEmpleado";
            txtNombreEmpleado.Size = new Size(130, 26);
            txtNombreEmpleado.TabIndex = 0;
            // 
            // txtDocumento
            // 
            txtDocumento.Anchor = AnchorStyles.None;
            txtDocumento.Font = new Font("Microsoft Sans Serif", 12F);
            txtDocumento.Location = new Point(307, 159);
            txtDocumento.Margin = new Padding(3, 2, 3, 2);
            txtDocumento.Name = "txtDocumento";
            txtDocumento.Size = new Size(130, 26);
            txtDocumento.TabIndex = 1;
            // 
            // txtAreaTrabajo
            // 
            txtAreaTrabajo.Anchor = AnchorStyles.None;
            txtAreaTrabajo.Font = new Font("Microsoft Sans Serif", 12F);
            txtAreaTrabajo.Location = new Point(307, 214);
            txtAreaTrabajo.Margin = new Padding(3, 2, 3, 2);
            txtAreaTrabajo.Name = "txtAreaTrabajo";
            txtAreaTrabajo.Size = new Size(130, 26);
            txtAreaTrabajo.TabIndex = 2;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(213, 106);
            label1.Name = "label1";
            label1.Size = new Size(71, 21);
            label1.TabIndex = 3;
            label1.Text = "Nombre:";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(247, 160);
            label2.Name = "label2";
            label2.Size = new Size(40, 21);
            label2.TabIndex = 4;
            label2.Text = "DNI:";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(158, 214);
            label3.Name = "label3";
            label3.Size = new Size(119, 21);
            label3.TabIndex = 5;
            label3.Text = "Área de trabajo:";
            // 
            // btnGuardar
            // 
            btnGuardar.Anchor = AnchorStyles.None;
            btnGuardar.Location = new Point(190, 284);
            btnGuardar.Margin = new Padding(3, 2, 3, 2);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(111, 38);
            btnGuardar.TabIndex = 6;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Anchor = AnchorStyles.None;
            btnCancelar.Location = new Point(346, 284);
            btnCancelar.Margin = new Padding(3, 2, 3, 2);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(113, 38);
            btnCancelar.TabIndex = 7;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // EditarEmpleadoView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoValidate = AutoValidate.EnablePreventFocusChange;
            ClientSize = new Size(601, 366);
            Controls.Add(btnCancelar);
            Controls.Add(btnGuardar);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtAreaTrabajo);
            Controls.Add(txtDocumento);
            Controls.Add(txtNombreEmpleado);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            Name = "EditarEmpleadoView";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Editar empleado";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtNombreEmpleado;
        private TextBox txtDocumento;
        private TextBox txtAreaTrabajo;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button btnGuardar;
        private Button btnCancelar;
    }
}