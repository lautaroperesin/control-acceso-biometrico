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

        public EditarEmpleadoView(int dev_idx, IntPtr anviz_handle, int Type,string idEmpleadoAEditar)
        {
            InitializeComponent();
            cmd.Connection = Helper.CrearConexion();
            this.idEmpleadoAEditar = idEmpleadoAEditar;
            this.anviz_handle = anviz_handle;
            this.dev_idx[0] = dev_idx;
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

            CCHEX_RET_PERSON_INFO_STRU empleado = new CCHEX_RET_PERSON_INFO_STRU();
            empleado.EmployeeId = ConvertToEmployeeIdByteArray(this.idEmpleadoAEditar);
            empleado.EmployeeName = ConvertToEmployeeNameByteArray(nuevoNombre);

            int ret = AnvizNew.CChex_ModifyPersonInfo(anviz_handle, dev_idx[0], ref empleado, 1);
            try
            {
                int len = Marshal.SizeOf(typeof(CCHEX_RET_PERSON_INFO_STRU)) * 50;
                IntPtr pBuff = Marshal.AllocHGlobal(len);

                if (anviz_handle != IntPtr.Zero)
                {
                    ret = AnvizNew.CChex_Update(anviz_handle, dev_idx, Type, pBuff, len);

                    while (ret <= 0)
                    {
                        ret = AnvizNew.CChex_Update(anviz_handle, dev_idx, Type, pBuff, len);
                    }

                    if (ret > 0)
                    {
                        MessageBox.Show("Empleado actualizado correctamente");
                    }
                    else
                    {
                        MessageBox.Show("Error al actualizar el empleado");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el empleado");
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
