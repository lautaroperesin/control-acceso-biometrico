using AnvizDemo;
using AppRegistros.Utils;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Reflection.PortableExecutable;
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

            // Editar en dispositivo
            //string idEmpleadoAEditarStr = this.idEmpleadoAEditar.PadLeft(5, '0').Substring(this.idEmpleadoAEditar.Length > 5 ? this.idEmpleadoAEditar.Length - 5 : 0);
            //byte[] idEmpleadoAEditarBytes = Encoding.ASCII.GetBytes(idEmpleadoAEditarStr);

            ulong idEmpleadoNumerico = ulong.Parse(idEmpleadoAEditar);
            byte[] idEmpleadoAEditarBytes = BitConverter.GetBytes(idEmpleadoNumerico);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(idEmpleadoAEditarBytes);
            }

            // BitConverter produce un array de 4 bytes, hay que extenderlo a 5 bytes
            byte[] idEmpleadoAEditar5Bytes = new byte[5];
            Array.Copy(idEmpleadoAEditarBytes, 3, idEmpleadoAEditar5Bytes, 0, 5);

            // Crear la estructura con la nueva información
            AnvizNew.CCHEX_RET_PERSON_INFO_STRU personInfo = new AnvizNew.CCHEX_RET_PERSON_INFO_STRU
            {
                EmployeeId = idEmpleadoAEditar5Bytes
            };

            personInfo.EmployeeName = new byte[64];
            byte[] name = Encoding.Default.GetBytes(nuevoNombre);
            Array.Copy(name, personInfo.EmployeeName, Math.Min(name.Length, 64));

            int ret = CChex_ModifyPersonInfo(anviz_handle, dev_idx[0], ref personInfo, 1);

            IntPtr pBuff;
            int len = 10000;
            pBuff = Marshal.AllocHGlobal(len);

            if (anviz_handle != IntPtr.Zero)
            {
                ret = AnvizNew.CChex_Update(anviz_handle, dev_idx, Type, pBuff, len);
                Debug.WriteLine("CChex_Update returned: " + ret);

                while (ret <= 0)
                {
                    ret = AnvizNew.CChex_Update(anviz_handle, dev_idx, Type, pBuff, len);
                    Debug.WriteLine("CChex_Update returned: " + ret);
                }
                if (ret > 0)
                {
                    if (Type[0] == (int)AnvizNew.MsgType.CCHEX_RET_ULEMPLOYEE2_UNICODE_INFO_TYPE)
                    {
                        AnvizNew.CCHEX_RET_DEL_EMPLOYEE_INFO_STRU result;
                        result = (AnvizNew.CCHEX_RET_DEL_EMPLOYEE_INFO_STRU)Marshal.PtrToStructure(pBuff, typeof(AnvizNew.CCHEX_RET_DEL_EMPLOYEE_INFO_STRU));
                        if (result.Result == 0)
                        {
                            MessageBox.Show("Empleado modificado correctamente. ID: " + Employee_array_to_srring(result.EmployeeId));
                        }
                        else
                        {
                            MessageBox.Show("Error al modificar el empleado. ID: " + Employee_array_to_srring(result.EmployeeId));
                        }
                    }
                }
                else
                {
                        Debug.WriteLine("Datos devueltos invalidos o espacio del buffer insuficiente.");
                }
                Marshal.FreeHGlobal(pBuff);
            }

            // Editar en base de datos
            cmd.Parameters.Clear();
            cmd.CommandText = $"UPDATE Empleados SET NombreEmpleado = @NombreEmpleado, DNI = @dni, AreaTrabajo = @AreaTrabajo WHERE IdEmpleado = @id";
            cmd.Parameters.AddWithValue("@NombreEmpleado", nuevoNombre);
            cmd.Parameters.AddWithValue("@dni", nuevoDni);
            cmd.Parameters.AddWithValue("@AreaTrabajo", nuevaAreaTrabajo);
            cmd.Parameters.AddWithValue("@id", this.idEmpleadoAEditar);
            cmd.ExecuteNonQuery();
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
        private void string_to_byte(string str, byte[] value, byte len)
        {
            int i;
            ulong ul_value = Convert.ToUInt64(str);
            for (i = 0; i < len; i++)
            {
                value[i] = (byte)((ul_value & ((ulong)0xFF << (8 * (len - i - 1)))) >> (8 * (len - i - 1)));
            }
        }
        private string Employee_array_to_srring(byte[] EmployeeId)
        {
            return BitConverter.ToString(EmployeeId).Replace("-", "");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
