using AppRegistros.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace AppRegistros
{
    public partial class EditarEmpleadoView : Form
    {
        SQLiteCommand cmd = new SQLiteCommand();
        private string? idEmpleadoAEditar;

        public EditarEmpleadoView(string idEmpleadoAEditar)
        {
            InitializeComponent();
            cmd.Connection = Helper.CrearConexion();
            this.idEmpleadoAEditar = idEmpleadoAEditar;
            CargarDatosEnPantalla();
        }

        private void CargarDatosEnPantalla()
        {
            cmd.CommandText = $"SELECT * FROM Empleados WHERE IdEmpleado = @id";
            cmd.Parameters.AddWithValue("@id", this.idEmpleadoAEditar);
            var empleadoReader = cmd.ExecuteReader();
            if (empleadoReader != null)
            {
                if (empleadoReader.Read())
                {
                    txtNombreEmpleado.Text = (string)empleadoReader["NombreEmpleado"];
                    txtDpto.Text = (string)empleadoReader["IdDpto"];
                    txtGrupo.Text = (string)empleadoReader["IdGrupo"];
                }
                else
                {
                    Debug.WriteLine("El reader no se ha leido");
                }
            }
            empleadoReader?.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombreEmpleado.Text;
            string dpto = txtDpto.Text;
            string grupo = txtGrupo.Text;

            cmd.Parameters.Clear();
            using (cmd.Connection)
            {
                string query = $"UPDATE Empleados SET NombreEmpleado = @NombreEmpleado, IdDpto = @IdDpto, IdGrupo = @IdGrupo WHERE IdEmpleado = @id";
                using (var command = new SQLiteCommand(query, cmd.Connection))
                {
                    command.Parameters.AddWithValue("@NombreEmpleado", nombre);
                    command.Parameters.AddWithValue("@IdDpto", dpto);
                    command.Parameters.AddWithValue("@IdGrupo", grupo);
                    command.Parameters.AddWithValue("@id", this.idEmpleadoAEditar);
                    command.ExecuteNonQuery();
                }
            }
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
