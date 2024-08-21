using AnvizDemo;
using AppRegistros.Utils;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using static AnvizDemo.AnvizNew;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppRegistros
{
    public partial class EditarEmpleadoView : Form
    {
        SQLiteCommand cmd = new SQLiteCommand();
        private string? idEmpleadoAEditar;
        IntPtr anviz_handle;
        int[] dev_idx = new int[1];
        int[] Type = new int[1];

        public EditarEmpleadoView(IntPtr anviz_handle, int dev_idx, int Type,string idEmpleadoAEditar)
        {
            InitializeComponent();
            cmd.Connection = Helper.CrearConexion();
            this.anviz_handle = anviz_handle;
            this.dev_idx[0] = dev_idx;
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
            }
            empleadoReader?.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nuevoNombre = txtNombreEmpleado.Text;
            string nuevoDni = txtDocumento.Text;
            string nuevaAreaTrabajo = txtAreaTrabajo.Text;

            // Editar en base de datos
            cmd.Parameters.Clear();
            cmd.CommandText = $"UPDATE Empleados SET NombreEmpleado = @NombreEmpleado, DNI = @dni, AreaTrabajo = @AreaTrabajo WHERE IdEmpleado = @id";
            cmd.Parameters.AddWithValue("@NombreEmpleado", nuevoNombre);
            cmd.Parameters.AddWithValue("@dni", nuevoDni);
            cmd.Parameters.AddWithValue("@AreaTrabajo", nuevaAreaTrabajo);
            cmd.Parameters.AddWithValue("@id", this.idEmpleadoAEditar);
            cmd.ExecuteNonQuery();

            // Editar en dispositivo
            // Convertir el ID de empleado en un array de bytes de 5 bytes
            byte[] employeeIdBytes = Encoding.ASCII.GetBytes(this.idEmpleadoAEditar.PadLeft(5, '0'));

            // Crear la estructura con la nueva información
            CCHEX_RET_PERSON_INFO_STRU personInfo = new CCHEX_RET_PERSON_INFO_STRU
            {
                MachineId = (uint)dev_idx[0], // Usa el DevIdx del dispositivo
                CurIdx = 0, // Indice actual del usuario (lo puedes dejar en 0)
                TotalCnt = 1, // Total de usuarios a modificar (1 en este caso)
                EmployeeId = employeeIdBytes,
                password_len = 0, // Asumimos que no hay cambio en la contraseña
                max_password = 6, // Longitud máxima de contraseña
                password = 0, // No cambiar la contraseña
                max_card_id = 10, // Longitud máxima del ID de tarjeta
                card_id = 0, // No cambiar la tarjeta
                max_EmployeeName = 64, // Longitud máxima del nombre
                EmployeeName = Encoding.ASCII.GetBytes(nuevoNombre),
                DepartmentId = 0, // Si tienes un ID de departamento puedes asignarlo aquí
                GroupId = 0, // Si tienes un ID de grupo puedes asignarlo aquí
                Mode = 0, // Modo de asistencia (ajustar según sea necesario)
                Fp_Status = 0, // Estado de registro biométrico
                Rserved1 = 0, // Reservado
                Rserved2 = 0, // Reservado
                Special = 0, // Información especial (si aplica)
                EmployeeName2 = Encoding.ASCII.GetBytes(string.Empty), // Nombre adicional (si aplica)
                RFC = new byte[13], // RFC (si aplica)
                CURP = new byte[18] // CURP (si aplica)
            };

            int ret = CChex_ModifyPersonInfo(anviz_handle, dev_idx[0], ref personInfo, 1);

            if (ret > 0)
            {
                MessageBox.Show("Empleado actualizado correctamente en el dispositivo.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error al actualizar el empleado en el dispositivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Close();
        }
        private byte[] ConvertToEmployeeIdByteArray(string employeeId)
        {
            return Encoding.ASCII.GetBytes(employeeId.PadLeft(10, '0').Substring(0, 5));
        }

        private byte[] ConvertToEmployeeNameByteArray(string employeeName)
        {
            byte[] nameBytes = new byte[64];
            Encoding.ASCII.GetBytes(employeeName, 0, employeeName.Length, nameBytes, 0);
            return nameBytes;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
