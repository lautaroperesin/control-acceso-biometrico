using AnvizDemo;
using AppRegistros.Utils;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using static AnvizDemo.AnvizNew;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace AppRegistros
{
    public partial class BusquedaRegistrosView : Form
    {
        Dictionary<int, int> DevTypeFlag = new Dictionary<int, int>();
        IntPtr anviz_handle;
        int[] dev_idx = new int[1];
        int ret = 0;
        int[] Type = new int[1];
        int len;
        IntPtr pBuff;

        public List<CCHEX_RET_RECORD_INFO_STRU> listaDeRegistros = new List<CCHEX_RET_RECORD_INFO_STRU>();
        public List<CCHEX_RET_PERSON_INFO_STRU> listaDeEmpleados = new List<CCHEX_RET_PERSON_INFO_STRU>();

        SQLiteCommand command = new SQLiteCommand();

        public BusquedaRegistrosView()
        {
            InitializeComponent();
            command.Connection = Helper.CrearConexion();
            CrearTablaEmpleados();
            CrearTablaRegistros();
        }

        public void CrearTablaEmpleados()
        {
            command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Empleados (
                IdEmpleado TEXT PRIMARY KEY,
                NombreEmpleado TEXT,
                IdGrupo TEXT,
                IdDpto TEXT
            );";
            command.ExecuteNonQuery();
        }
        public void CrearTablaRegistros()
        {
            command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Registros (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                IdEmpleado TEXT,
                Fecha TEXT,
                TipoRegistro TEXT,
                FOREIGN KEY(IdEmpleado) REFERENCES Empleados(IdEmpleado)
            );";
            command.ExecuteNonQuery();
        }
        public void InsertarEmpleado(string idEmpleado, string nombreEmpleado, string grupoId, string dptoId)   
        {
            command.CommandText = "SELECT COUNT(*) FROM Empleados WHERE IdEmpleado = @IdEmpleado";
            command.Parameters.AddWithValue("@IdEmpleado", idEmpleado);

            int count = Convert.ToInt32(command.ExecuteScalar());
            command.Parameters.Clear();

            if (count == 0)
            {
                command.CommandText = @"
                INSERT INTO Empleados (IdEmpleado, NombreEmpleado, IdGrupo, IdDpto) 
                VALUES (@IdEmpleado, @NombreEmpleado, @IdGrupo, @IdDpto)";
                command.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
                command.Parameters.AddWithValue("@NombreEmpleado", nombreEmpleado);
                command.Parameters.AddWithValue("@IdGrupo", grupoId);
                command.Parameters.AddWithValue("@IdDpto", dptoId);
                command.ExecuteNonQuery();
            }
        }
        public void InsertarRegistro(string idEmpleado, string tipoRegistro, string dateStr)
        {
            command.Parameters.Clear();
            command.CommandText = @"
            SELECT COUNT(*) FROM Registros 
            WHERE IdEmpleado = @IdEmpleado AND Fecha = @Fecha AND TipoRegistro = @TipoRegistro";
            command.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
            command.Parameters.AddWithValue("@Fecha", dateStr);
            command.Parameters.AddWithValue("@TipoRegistro", tipoRegistro);

            int count = Convert.ToInt32(command.ExecuteScalar());
            command.Parameters.Clear();
            if (count == 0)
            {
                command.CommandText = @"
                    INSERT INTO Registros (IdEmpleado, Fecha, TipoRegistro) 
                    VALUES (@IdEmpleado, @Fecha, @TipoRegistro)";
                command.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
                command.Parameters.AddWithValue("@Fecha", dateStr);
                command.Parameters.AddWithValue("@TipoRegistro", tipoRegistro);
                command.ExecuteNonQuery();
            }
        }


        public void btnConectarDispositivo_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar dirección IP y puerto
                if (string.IsNullOrWhiteSpace(textBoxIpDispositivo.Text) || string.IsNullOrWhiteSpace(textBoxPuertoDispositivo.Text))
                {
                    MessageBox.Show("Ingrese un puerto y una dirección IP válida.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string ipAddress = textBoxIpDispositivo.Text.Trim();
                int port = Convert.ToInt32(textBoxPuertoDispositivo.Text.Trim());

                // Verificar conectividad de red mediante Ping
                Ping ping = new Ping();
                PingReply reply = ping.Send(ipAddress);

                if (reply.Status == IPStatus.Success)
                {
                    anviz_handle = AnvizNew.CChex_Start();
                    if (anviz_handle == IntPtr.Zero)
                    {
                        MessageBox.Show("No se pudo iniciar el SDK.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    ret = AnvizNew.CCHex_ClientConnect(anviz_handle, Encoding.ASCII.GetBytes(ipAddress), port);

                    // Verifica el valor de retorno esperado para una conexión exitosa
                    if (ret == 1)
                    {
                        MessageBox.Show("Conexión exitosa", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        groupBoxInicio.Hide();
                        LoguearDispositivo();
                        ObtenerInfoEmpleado();
                        CargarDatosAGrilla();
                    }
                    else
                    {
                        string errorMsg = "Conexión fallida con error de código: " + ret;

                        // Detalle del código de error
                        switch (ret)
                        {
                            case -1:
                                errorMsg += " (Error de conexión)";
                                break;
                            default:
                                errorMsg += " (Error desconocido)";
                                break;
                        }
                        MessageBox.Show(errorMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    System.Diagnostics.Debug.WriteLine("IP Address: " + ipAddress + ", Port: " + port);
                }
                else
                {
                    MessageBox.Show("Ping to the device failed. Please check the network connection.", "Network Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid port number.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void LoguearDispositivo()
        {
            try
            {
                len = 32000;
                pBuff = Marshal.AllocHGlobal(len);

                if (anviz_handle != IntPtr.Zero)
                {
                    ret = AnvizNew.CChex_Update(anviz_handle, dev_idx, Type, pBuff, len);
                    dbg_info("Update returned: " + ret);

                    // Inicia un bucle hasta que Update retorne 0
                    while (ret > 0)
                    {
                        dbg_info("Mensaje de tipo : " + Type[0]);
                        switch (Type[0])
                        {
                            // Dispositivo logueado = 2
                            case (int)AnvizNew.MsgType.CCHEX_RET_DEV_LOGIN_TYPE:
                                {
                                    AnvizNew.CCHEX_RET_DEV_LOGIN_STRU dev_info;
                                    dev_info = (AnvizNew.CCHEX_RET_DEV_LOGIN_STRU)Marshal.PtrToStructure(pBuff, typeof(AnvizNew.CCHEX_RET_DEV_LOGIN_STRU));
                                    string info_buff =
                                                "Dispositivo conectado --- [MachineId:" + dev_info.MachineId +
                                                " Dir. IP:" + byte_to_string(dev_info.Addr) +
                                                " Version:" + byte_to_string(dev_info.Version) + "]";

                                    dbg_info(info_buff);
                                    DevTypeFlag.Add(dev_info.DevIdx, (int)dev_info.DevTypeFlag);
                                }
                                break;
                            // Estado del dispositivo = 19
                            case (int)AnvizNew.MsgType.CCHEX_RET_DEV_STATUS_TYPE:
                                {
                                    AnvizNew.CCHEX_RET_DEV_STATUS_STRU status;
                                    status = (AnvizNew.CCHEX_RET_DEV_STATUS_STRU)Marshal.PtrToStructure(pBuff, typeof(AnvizNew.CCHEX_RET_DEV_STATUS_STRU));
                                    string info_buff =
                                            "Estado --- [MachineId:" + status.MachineId.ToString()
                                            + ", Empleados:" + status.EmployeeNum.ToString()
                                            + ", Huellas:" + status.FingerPrtNum.ToString()
                                            + ", Contraseñas:" + status.PasswdNum.ToString()
                                            + ", Cards:" + status.CardNum.ToString()
                                            + ", Registros:" + status.TotalRecNum.ToString()
                                            + ", Nuevos Reg:" + status.NewRecNum.ToString() + "]";
                                    dbg_info(info_buff);
                                }
                                break;
                            // Informacion sobre configuracion de red = 23
                            case (int)AnvizNew.MsgType.CCHEX_RET_GET_BASIC_CFG_TYPE:
                                System.Diagnostics.Debug.WriteLine("Mensaje de tipo 29 recibido y ignorado");
                                break;
                            // Info de registros = 1
                            case (int)AnvizNew.MsgType.CCHEX_RET_RECORD_INFO_TYPE:
                                {
                                    AnvizNew.CCHEX_RET_RECORD_INFO_STRU record_info;
                                    record_info = (AnvizNew.CCHEX_RET_RECORD_INFO_STRU)Marshal.PtrToStructure(pBuff, typeof(AnvizNew.CCHEX_RET_RECORD_INFO_STRU));

                                    byte[] dateBytes = record_info.Date.Reverse().ToArray();
                                    int secondsSince20000102 = BitConverter.ToInt32(dateBytes, 0);

                                    // Fecha base: 2 de enero de 2000
                                    DateTime baseDate = new DateTime(2000, 1, 2, 0, 0, 0, DateTimeKind.Utc);

                                    // Añadir los segundos a la fecha base para obtener la fecha y hora del registro
                                    DateTime recordDate = baseDate.AddSeconds(secondsSince20000102);
                                    string dateStr = recordDate.ToString("yyyy-MM-dd HH:mm:ss");
                                    string idEmpleado = Employee_array_to_srring(record_info.EmployeeId);
                                    string tipoRegistro = record_info.RecordType.ToString();

                                    InsertarRegistro(idEmpleado, tipoRegistro, dateStr);
                                }
                                break;
                        }
                        ret = AnvizNew.CChex_Update(anviz_handle, dev_idx, Type, pBuff, len);
                    }
                }
            }
            finally
            {
                if (pBuff != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(pBuff);
                }
            }
        }


        private void btnObtenerEmpleados_Click(object sender, EventArgs e)
        {
            CargarListViewEmpleados();
        }
        private void btnBuscarEmpleadoPorId_Click(object sender, EventArgs e)
        {
            listViewEmpleados.Items.Clear();
            command.Parameters.Clear();

            string idEmpleado = txtIdEmpleado.Text;
            string searchTermWithZeros = idEmpleado.PadLeft(10, '0');

            command.CommandText = "SELECT * FROM Empleados WHERE IdEmpleado = @idEmpleado OR IdEmpleado = @searchTermWithZeros OR NombreEmpleado LIKE @nombre";
            command.Parameters.AddWithValue("@idEmpleado", idEmpleado);
            command.Parameters.AddWithValue("@searchTermWithZeros", searchTermWithZeros);
            command.Parameters.AddWithValue("@nombre", "%" + idEmpleado + "%");

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader["IdEmpleado"].ToString());
                    item.SubItems.Add(reader["NombreEmpleado"].ToString());
                    item.SubItems.Add(reader["IdGrupo"].ToString());
                    item.SubItems.Add(reader["IdDpto"].ToString());

                    listViewEmpleados.Items.Add(item);
                }
            }
        }

        private void ObtenerInfoEmpleado()
        {
            ret = AnvizNew.CChex_ListPersonInfo(anviz_handle, dev_idx[0]);
            pBuff = IntPtr.Zero;
            try
            {
                len = Marshal.SizeOf(typeof(CCHEX_RET_PERSON_INFO_STRU)) * 50;
                pBuff = Marshal.AllocHGlobal(len);

                if (anviz_handle != IntPtr.Zero)
                {
                    ret = AnvizNew.CChex_Update(anviz_handle, dev_idx, Type, pBuff, len);
                    dbg_info("CChex_Update returned: " + ret);

                    while (ret <= 0)
                    {
                        ret = AnvizNew.CChex_Update(anviz_handle, dev_idx, Type, pBuff, len);
                        dbg_info("CChex_Update returned: " + ret);
                    }

                    while (ret > 0 && Type[0] == (int)AnvizNew.MsgType.CCHEX_RET_LIST_PERSON_INFO_TYPE)
                    {
                        CCHEX_RET_PERSON_INFO_STRU infoEmpleado = (CCHEX_RET_PERSON_INFO_STRU)Marshal.PtrToStructure(pBuff, typeof(CCHEX_RET_PERSON_INFO_STRU));

                        string idEmpleado = Employee_array_to_srring(infoEmpleado.EmployeeId);
                        string nombreEmpleado = byte_to_string(infoEmpleado.EmployeeName).TrimEnd('\0');
                        string grupoId = infoEmpleado.GroupId.ToString();
                        string dptoId = infoEmpleado.DepartmentId.ToString();

                        dbg_info($"Empleado - ID: {idEmpleado}, Nombre: {nombreEmpleado}");

                        InsertarEmpleado(idEmpleado, nombreEmpleado, grupoId, dptoId);

                        ret = AnvizNew.CChex_Update(anviz_handle, dev_idx, Type, pBuff, len);
                    }
                }
            }
            catch (Exception ex)
            {
                dbg_info("An error occurred: " + ex.Message);
            }
            finally
            {
                if (pBuff != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(pBuff);
                }
            }
        }
        private void CargarListViewEmpleados()
        {
            listViewEmpleados.Items.Clear();
            command.CommandText = "SELECT * FROM Empleados";
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader["IdEmpleado"].ToString());
                    item.SubItems.Add(reader["NombreEmpleado"].ToString());
                    item.SubItems.Add(reader["IdGrupo"].ToString());
                    item.SubItems.Add(reader["IdDpto"].ToString());

                    listViewEmpleados.Items.Add(item);
                }
            }
        }


        private void btnConfigRed_Click(object sender, EventArgs e)
        {
            ret = AnvizNew.CChex_GetNetConfig(anviz_handle, dev_idx[0]);
            ObtenerConfigRed();
        }
        private void ObtenerConfigRed()
        {
            pBuff = IntPtr.Zero;
            try
            {
                len = Marshal.SizeOf(typeof(CCHEX_RET_GETNETCFG_STRU)) + 300;
                pBuff = Marshal.AllocHGlobal(len);

                if (anviz_handle != IntPtr.Zero)
                {
                    ret = AnvizNew.CChex_Update(anviz_handle, dev_idx, Type, pBuff, len);
                    dbg_info("CChex_Update returned: " + ret);

                    while (ret <= 0)
                    {
                        ret = AnvizNew.CChex_Update(anviz_handle, dev_idx, Type, pBuff, len);
                        dbg_info("CChex_Update returned: " + ret);
                    }
                    if (ret <= 0)
                    {
                        dbg_info("Datos devueltos invalidos o espacio del buffer insuficiente.");
                    }
                    else
                    {
                        dbg_info("Recibido mensaje de tipo: " + Type[0]);

                        if (Type[0] == (int)AnvizNew.MsgType.CCHEX_RET_GETNETCFG_TYPE)
                        {
                            AnvizNew.CCHEX_RET_GETNETCFG_STRU dev_net_info;
                            dev_net_info = (AnvizNew.CCHEX_RET_GETNETCFG_STRU)Marshal.PtrToStructure(pBuff, typeof(AnvizNew.CCHEX_RET_GETNETCFG_STRU));
                            this.textBoxIp.Text = dev_net_info.Cfg.IpAddr[0] + "." + dev_net_info.Cfg.IpAddr[1] + "." + dev_net_info.Cfg.IpAddr[2] + "." + dev_net_info.Cfg.IpAddr[3];
                            this.textBoxMask.Text = dev_net_info.Cfg.IpMask[0] + "." + dev_net_info.Cfg.IpMask[1] + "." + dev_net_info.Cfg.IpMask[2] + "." + dev_net_info.Cfg.IpMask[3];
                            this.textBoxGw.Text = dev_net_info.Cfg.GwAddr[0] + "." + dev_net_info.Cfg.GwAddr[1] + "." + dev_net_info.Cfg.GwAddr[2] + "." + dev_net_info.Cfg.GwAddr[3];
                            this.textBoxServIp.Text = dev_net_info.Cfg.ServAddr[0] + "." + dev_net_info.Cfg.ServAddr[1] + "." + dev_net_info.Cfg.ServAddr[2] + "." + dev_net_info.Cfg.ServAddr[3];
                            this.textBoxPort.Text = (dev_net_info.Cfg.Port[0] << 8 | dev_net_info.Cfg.Port[1]).ToString();

                            if (dev_net_info.Result == 0)
                            {
                                dbg_info("Configuracion de red obtenida con éxito");
                            }
                            else
                            {
                                dbg_info("Falló la obtención de la configuracion de red");
                            }
                        }
                        else
                        {
                            dbg_info("Tipo de mensaje no esperado: " + Type[0]);
                        }
                    }
                }
                else
                {
                    dbg_info("El Handle es nulo.");
                }
            }
            catch (Exception ex)
            {
                dbg_info("An error occurred: " + ex.Message);
            }
            finally
            {
                if (pBuff != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(pBuff);
                }
            }
        }


        private void btnBuscarRegistros_Click(object sender, EventArgs e)
        {
            CargarListViewRegistros();
        }
        private void CargarListViewRegistros()
        {
            listViewRegistros.Items.Clear();
            command.Parameters.Clear();

            string idEmpleado = txtId.Text;
            string searchTermWithZeros = idEmpleado.PadLeft(10, '0');
            DateTime fechaInicio = dateTimeFechaInicio.Value.Date;
            DateTime fechaFin = dateTimeFechaFinal.Value.Date;

            command.CommandText = "SELECT * FROM Registros WHERE IdEmpleado = @idEmpleado OR IdEmpleado = @searchTermWithZeros AND Fecha BETWEEN @fechaInicio AND @fechaFin";
            command.Parameters.AddWithValue("@idEmpleado", idEmpleado);
            command.Parameters.AddWithValue("@searchTermWithZeros", searchTermWithZeros);
            command.Parameters.AddWithValue("@fechaInicio", fechaInicio);
            command.Parameters.AddWithValue("@fechaFin", fechaFin);

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader["IdEmpleado"].ToString());
                    item.SubItems.Add(Convert.ToDateTime(reader["Fecha"]).ToString("yyyy-MM-dd HH:mm:ss"));
                    item.SubItems.Add(reader["TipoRegistro"].ToString());

                    listViewRegistros.Items.Add(item);
                }
            }
        }


        private void btnSincHora_Click(object sender, EventArgs e)
        {
            // Obtener la hora actual
            DateTime now = DateTime.Now;

            ret = AnvizNew.CChex_SetTime(anviz_handle, dev_idx[0], now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
            if (ret == 1)
            {
                MessageBox.Show("Hora sincronizada correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error al sincronizar la hora", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (listViewEmpleados.SelectedItems.Count > 0)
            {
                // Editar en ListView
                ListViewItem itemSeleccionado = listViewEmpleados.SelectedItems[0];
                string idEmpleadoAEditarStr = itemSeleccionado.SubItems[0].Text;

                // Editar en DataGridView
                //int idEmpleadoAEditar = Convert.ToInt32(dataGridEmpleados.CurrentRow.Cells[0].Value);
                //string idEmpleadoAEditarStr = (string)dataGridEmpleados.CurrentRow.Cells[0].Value;

                EditarEmpleadoView editarEmpleadoView = new EditarEmpleadoView(idEmpleadoAEditarStr);
                editarEmpleadoView.ShowDialog();
                CargarListViewEmpleados();
            }
            else
            {
                MessageBox.Show("No se seleccionó ningún empleado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (listViewEmpleados.SelectedItems.Count > 0)
            {
                ListViewItem itemSeleccionado = listViewEmpleados.SelectedItems[0];
                string idEmpleadoAEliminar = itemSeleccionado.SubItems[0].Text;
                string nombreEmpleado = itemSeleccionado.SubItems[1].Text;
                command.Parameters.Clear();

                DialogResult respuesta = MessageBox.Show($"Está seguro que quiere eliminar al empleado {nombreEmpleado}?",
                                   "Eliminar empleado",
                                   MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    bool eliminadoDelDispositivo = EliminarEmpleadoDelDispositivo(idEmpleadoAEliminar);
                    if (eliminadoDelDispositivo)
                    {
                        command.CommandText = $"DELETE FROM Empleados WHERE IdEmpleado = @id";
                        command.Parameters.AddWithValue("@id", idEmpleadoAEliminar);
                        command.ExecuteNonQuery();
                        CargarListViewEmpleados();
                        MessageBox.Show("El empleado ha sido eliminado correctamente del dispositivo y de la base de datos.");
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar el empleado del dispositivo. No se realizó la eliminación en la base de datos.");
                    }
                }
            }
            else
            {
                MessageBox.Show("No se seleccionó ningún empleado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private bool EliminarEmpleadoDelDispositivo(string idEmpleado)
        {
            try
            {
                byte[] employeeIdBytes = Encoding.ASCII.GetBytes(idEmpleado.Substring(0, 5));

                CCHEX_DEL_PERSON_INFO_STRU deleteConfig = new CCHEX_DEL_PERSON_INFO_STRU
                {
                    EmployeeId = employeeIdBytes,
                    operation = 0xFF
                };

                ret = CChex_DeletePersonInfo(anviz_handle, dev_idx[0], ref deleteConfig);

                pBuff = IntPtr.Zero;
                try
                {
                    len = Marshal.SizeOf(typeof(CCHEX_DEL_PERSON_INFO_STRU)) + 50;
                    pBuff = Marshal.AllocHGlobal(len);

                    if (anviz_handle != IntPtr.Zero)
                    {
                        ret = AnvizNew.CChex_Update(anviz_handle, dev_idx, Type, pBuff, len);
                        dbg_info("CChex_Update returned: " + ret);

                        while (ret <= 0)
                        {
                            ret = AnvizNew.CChex_Update(anviz_handle, dev_idx, Type, pBuff, len);
                            dbg_info("CChex_Update returned: " + ret);
                        }
                    }
                }
                catch (Exception ex)
                {
                    dbg_info("An error occurred: " + ex.Message);
                }
                finally
                {
                    if (pBuff != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(pBuff);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar el empleado del dispositivo: " + ex.Message);
                return false;
            }
        }


        private void CargarDatosAGrilla()
        {
            command.CommandText = "SELECT * FROM Empleados";
            SQLiteDataReader empleadosReader = command.ExecuteReader();

            DataTable empleadosTable = new DataTable();
            empleadosTable.Load(empleadosReader);
            dataGridEmpleados.DataSource = empleadosTable;
        }

        public void dbg_info(string dbg_string)
        {
            Debug.WriteLine("Time:" + DateTime.Now.ToString("HH-mm-ss") + " ----[  " + dbg_string + "  ]", " SDK_DBG_INFO");
        }
        private string byte_to_string(byte[] StringData)
        {
            return Encoding.Default.GetString(StringData).Replace("\0", "");
        }
        private string Employee_array_to_srring(byte[] EmployeeId)
        {
            return BitConverter.ToString(EmployeeId).Replace("-", "");
        }
    }
}