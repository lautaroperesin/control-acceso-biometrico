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
            txtNombreEmpleado = new TextBox();
            txtDpto = new TextBox();
            txtGrupo = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            btnGuardar = new Button();
            btnCancelar = new Button();
            SuspendLayout();
            // 
            // txtNombreEmpleado
            // 
            txtNombreEmpleado.Font = new Font("Microsoft Sans Serif", 12F);
            txtNombreEmpleado.Location = new Point(351, 141);
            txtNombreEmpleado.Name = "txtNombreEmpleado";
            txtNombreEmpleado.Size = new Size(148, 30);
            txtNombreEmpleado.TabIndex = 0;
            // 
            // txtDpto
            // 
            txtDpto.Font = new Font("Microsoft Sans Serif", 12F);
            txtDpto.Location = new Point(351, 212);
            txtDpto.Name = "txtDpto";
            txtDpto.Size = new Size(148, 30);
            txtDpto.TabIndex = 1;
            // 
            // txtGrupo
            // 
            txtGrupo.Font = new Font("Microsoft Sans Serif", 12F);
            txtGrupo.Location = new Point(351, 286);
            txtGrupo.Name = "txtGrupo";
            txtGrupo.Size = new Size(148, 30);
            txtGrupo.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(243, 141);
            label1.Name = "label1";
            label1.Size = new Size(89, 28);
            label1.TabIndex = 3;
            label1.Text = "Nombre:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(201, 210);
            label2.Name = "label2";
            label2.Size = new Size(143, 28);
            label2.TabIndex = 4;
            label2.Text = "Departamento:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(260, 286);
            label3.Name = "label3";
            label3.Size = new Size(72, 28);
            label3.TabIndex = 5;
            label3.Text = "Grupo:";
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(217, 379);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(127, 50);
            btnGuardar.TabIndex = 6;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(395, 379);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(129, 50);
            btnCancelar.TabIndex = 7;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // EditarEmpleadoView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoValidate = AutoValidate.EnablePreventFocusChange;
            ClientSize = new Size(687, 488);
            Controls.Add(btnCancelar);
            Controls.Add(btnGuardar);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtGrupo);
            Controls.Add(txtDpto);
            Controls.Add(txtNombreEmpleado);
            Name = "EditarEmpleadoView";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Editar empleado";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtNombreEmpleado;
        private TextBox txtDpto;
        private TextBox txtGrupo;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button btnGuardar;
        private Button btnCancelar;
    }
}