using AppRegistros.Utils;
using System.Diagnostics;

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
                    txtDocumento.Text = (string)empleadoReader["DNI"];
                    txtAreaTrabajo.Text = (string)empleadoReader["AreaTrabajo"];
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
            string dni = txtDocumento.Text;
            string areaTrabajo = txtAreaTrabajo.Text;

            cmd.Parameters.Clear();
            cmd.CommandText = $"UPDATE Empleados SET NombreEmpleado = @NombreEmpleado, DNI = @dni, AreaTrabajo = @AreaTrabajo WHERE IdEmpleado = @id";
            cmd.Parameters.AddWithValue("@NombreEmpleado", nombre);
            cmd.Parameters.AddWithValue("@dni", dni);
            cmd.Parameters.AddWithValue("@AreaTrabajo", areaTrabajo);
            cmd.Parameters.AddWithValue("@id", this.idEmpleadoAEditar);
            cmd.ExecuteNonQuery();
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
