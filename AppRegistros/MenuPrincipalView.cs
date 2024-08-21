using AnvizDemo;
using AppRegistros.Utils;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using static AnvizDemo.AnvizNew;

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

        // BASE DE DATOS
        public void CrearTablaEmpleados()
        {
            command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Empleados (
                IdEmpleado TEXT PRIMARY KEY,
                NombreEmpleado TEXT,
                DNI TEXT,
                AreaTrabajo TEXT
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
        public void InsertarEmpleado(string idEmpleado, string nombreEmpleado, string dni, string areaTrabajo)
        {
            command.CommandText = "SELECT COUNT(*) FROM Empleados WHERE IdEmpleado = @IdEmpleado";
            command.Parameters.AddWithValue("@IdEmpleado", idEmpleado);

            int count = Convert.ToInt32(command.ExecuteScalar());
            command.Parameters.Clear();

            if (count == 0)
            {
                command.CommandText = @"
                INSERT INTO Empleados (IdEmpleado, NombreEmpleado, DNI, AreaTrabajo) 
                VALUES (@IdEmpleado, @NombreEmpleado, @dni, @AreaTrabajo)";
                command.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
                command.Parameters.AddWithValue("@NombreEmpleado", nombreEmpleado);
                command.Parameters.AddWithValue("@dni", dni);
                command.Parameters.AddWithValue("@AreaTrabajo", areaTrabajo);
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


        // DISPOSITIVO
        public async void btnConectarDispositivo_Click(object sender, EventArgs e)
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
                        progressBarCargando.Style = ProgressBarStyle.Marquee;
                        progressBarCargando.Visible = true;
                        btnConectarDispositivo.Enabled = false;
                        groupBoxInicio.Enabled = false;

                        await Task.Delay(500);
                        LoguearDispositivo();
                        ObtenerInfoEmpleado();
                        CargarListViewEmpleados();
                        DownloadAllRecords();
                        groupBoxInicio.Hide();
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
        // leer los nuevos registros del dispositivo
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


        // EMPLEADOS
        private void btnBuscarEmpleadoPorId_Click(object sender, EventArgs e)
        {
            listViewEmpleados.Items.Clear();
            command.Parameters.Clear();

            string idEmpleado = txtIdEmpleado.Text;
            string searchTermWithZeros = idEmpleado.PadLeft(10, '0');

            command.CommandText = "SELECT * FROM Empleados WHERE IdEmpleado = @idEmpleado OR IdEmpleado = @searchTermWithZeros OR NombreEmpleado LIKE @nombre OR DNI = @dni";
            command.Parameters.AddWithValue("@idEmpleado", idEmpleado);
            command.Parameters.AddWithValue("@searchTermWithZeros", searchTermWithZeros);
            command.Parameters.AddWithValue("@nombre", "%" + idEmpleado + "%");
            command.Parameters.AddWithValue("@dni", idEmpleado);

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader["IdEmpleado"].ToString());
                    item.SubItems.Add(reader["NombreEmpleado"].ToString());
                    item.SubItems.Add(reader["DNI"].ToString());
                    item.SubItems.Add(reader["AreaTrabajo"].ToString());

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
                        string dni = string.Empty;
                        string areaTrabajo = string.Empty;

                        dbg_info($"Empleado - ID: {idEmpleado}, Nombre: {nombreEmpleado}");

                        InsertarEmpleado(idEmpleado, nombreEmpleado, dni, areaTrabajo);

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
                    item.SubItems.Add(reader["DNI"].ToString());
                    item.SubItems.Add(reader["AreaTrabajo"].ToString());

                    listViewEmpleados.Items.Add(item);
                }
            }
        }


        // CONFIGURACION
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


        // REGISTROS
        private void btnBuscarRegistros_Click(object sender, EventArgs e)
        {
            ObtenerHorasTrabajadasPorDia();
            //DownloadAllRecords();
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
            double totalHorasTrabajadas = 0.0;

            command.CommandText = @"
                SELECT r.IdEmpleado, e.NombreEmpleado, e.AreaTrabajo, 
                       DATE(r.Fecha) AS Dia, 
                       MIN(r.Fecha) AS Entrada, 
                       MAX(r.Fecha) AS Salida,
                       (strftime('%s', MAX(r.Fecha)) - strftime('%s', MIN(r.Fecha))) / 3600.0 AS HorasTrabajadas
                FROM Registros r
                JOIN Empleados e ON r.IdEmpleado = e.IdEmpleado
                WHERE (r.IdEmpleado = @idEmpleado OR r.IdEmpleado = @searchTermWithZeros OR e.NombreEmpleado LIKE @nombre OR e.DNI LIKE @nombre)
                AND r.Fecha BETWEEN @fechaInicio AND @fechaFin 
                AND r.TipoRegistro IN (0, 1)
                GROUP BY r.IdEmpleado, DATE(r.Fecha)
                HAVING COUNT(*) >= 2
                ORDER BY Dia";
            command.Parameters.AddWithValue("@idEmpleado", idEmpleado);
            command.Parameters.AddWithValue("@searchTermWithZeros", searchTermWithZeros);
            command.Parameters.AddWithValue("@nombre", "%" + idEmpleado + "%");
            command.Parameters.AddWithValue("@fechaInicio", fechaInicio);
            command.Parameters.AddWithValue("@fechaFin", fechaFin);

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem item = new ListViewItem(reader["IdEmpleado"].ToString());
                item.SubItems.Add(reader["NombreEmpleado"].ToString());
                item.SubItems.Add(reader["AreaTrabajo"].ToString());
                item.SubItems.Add(Convert.ToDateTime(reader["Dia"]).ToString("yyyy-MM-dd"));
                item.SubItems.Add(Convert.ToDateTime(reader["Entrada"]).ToString("HH:mm:ss"));
                item.SubItems.Add(Convert.ToDateTime(reader["Salida"]).ToString("HH:mm:ss"));
                double horasTrabajadas = Convert.ToDouble(reader["HorasTrabajadas"]);
                item.SubItems.Add(Convert.ToDouble(reader["HorasTrabajadas"]).ToString("F2"));

                totalHorasTrabajadas += horasTrabajadas;
                listViewRegistros.Items.Add(item);
            }
            reader.Close();
            txtHorasTotales.Text = totalHorasTrabajadas.ToString("F2");
        }
        private void DownloadAllRecords()
        {
            ret = AnvizNew.CChex_DownloadAllRecords(anviz_handle, dev_idx[0]);
            if (ret > 0)
            {
                pBuff = IntPtr.Zero;
                len = 10000;
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
                        while (ret > 0)
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
                            ret = AnvizNew.CChex_Update(anviz_handle, dev_idx, Type, pBuff, len);
                        }
                    }
                    Marshal.FreeHGlobal(pBuff);
                }
            }
            else
            {
                MessageBox.Show("Error al descargar los registros", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        // EDITAR Y ELIMINAR EMPLEADOS
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (listViewEmpleados.SelectedItems.Count > 0)
            {
                ListViewItem itemSeleccionado = listViewEmpleados.SelectedItems[0];
                string idEmpleadoAEditarStr = itemSeleccionado.SubItems[0].Text;

                EditarEmpleadoView editarEmpleadoView = new EditarEmpleadoView(anviz_handle, dev_idx[0], Type[0], idEmpleadoAEditarStr);
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
                    if ((DevTypeFlag[dev_idx[0]] & 0xff) == (int)AnvizNew.CustomType.DEV_TYPE_VER_4_NEWID)
                    {
                        AnvizNew.CCHEX_DEL_EMPLOYEE_INFO_STRU_EXT_INF_ID_VER_4_NEWID delete_item;
                        delete_item.EmployeeId = string_to_my_unicodebyte(28, idEmpleadoAEliminar);
                        delete_item.operation = 0xFF;
                        ret = AnvizNew.CChex_DeletePersonInfo_VER_4_NEWID(anviz_handle, dev_idx[0], ref delete_item);
                        EliminarEmpleadoDelDispositivo(idEmpleadoAEliminar);
                    }
                    else
                    {
                        AnvizNew.CCHEX_DEL_PERSON_INFO_STRU delete_item;
                        delete_item.EmployeeId = new byte[5];
                        string_to_byte(idEmpleadoAEliminar, delete_item.EmployeeId, 5);
                        delete_item.operation = 0xFF;
                        ret = AnvizNew.CChex_DeletePersonInfo(anviz_handle, dev_idx[0], ref delete_item);
                        EliminarEmpleadoDelDispositivo(idEmpleadoAEliminar);
                    }
                    //EliminarEmpleadoDelDispositivo(idEmpleadoAEliminar);
                    //        if (eliminadoDelDispositivo)
                    //        {
                    //            command.CommandText = $"DELETE FROM Empleados WHERE IdEmpleado = @id";
                    //            command.Parameters.AddWithValue("@id", idEmpleadoAEliminar);
                    //            command.ExecuteNonQuery();
                    //            CargarListViewEmpleados();
                    //            MessageBox.Show("El empleado ha sido eliminado correctamente del dispositivo y de la base de datos.");
                    //        }
                    //        else
                    //        {
                    //            MessageBox.Show("Error al eliminar el empleado del dispositivo. No se realizó la eliminación en la base de datos.");
                    //        }
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
                //dbg_info($"idEmpleado: {idEmpleado}");
                //if (string.IsNullOrEmpty(idEmpleado) || idEmpleado.Length != 10)
                //{
                //    dbg_info("Error: El ID del empleado es nulo o no tiene exactamente 10 caracteres.");
                //    return false;
                //}
                //// Toma los últimos 5 caracteres del ID
                //string idEmpleadoCorto = idEmpleado.Substring(idEmpleado.Length - 5);
                //dbg_info($"idEmpleadoCorto: {idEmpleadoCorto}");

                //// Convierte esos 5 caracteres a un array de 5 bytes
                //byte[] employeeIdBytes = Encoding.ASCII.GetBytes(idEmpleadoCorto);


                //CCHEX_DEL_PERSON_INFO_STRU deleteConfig = new CCHEX_DEL_PERSON_INFO_STRU
                //{
                //    EmployeeId = employeeIdBytes,
                //    operation = 0xFF
                //};

                //ret = CChex_DeletePersonInfo(anviz_handle, dev_idx[0], ref deleteConfig);

                pBuff = IntPtr.Zero;
                try
                {
                    len = Marshal.SizeOf(typeof(CCHEX_DEL_PERSON_INFO_STRU)) + 10;
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

                        if(ret > 0)
                        {
                            if ((DevTypeFlag[dev_idx[0]] & 0xff) == (int)AnvizNew.CustomType.DEV_TYPE_VER_4_NEWID)
                            {
                                AnvizNew.CCHEX_RET_DEL_EMPLOYEE_INFO_STRU_VER_4_NEWID result;
                                result = (AnvizNew.CCHEX_RET_DEL_EMPLOYEE_INFO_STRU_VER_4_NEWID)Marshal.PtrToStructure(pBuff, typeof(AnvizNew.CCHEX_RET_DEL_EMPLOYEE_INFO_STRU_VER_4_NEWID));
                                if (result.Result == 0)
                                {
                                    MessageBox.Show("Delete person OK  ID == " + byte_to_unicode_string(result.EmployeeId));
                                }
                                else
                                {
                                    MessageBox.Show("Delete person Fail!");
                                }
                            }
                            else
                            {
                                AnvizNew.CCHEX_RET_DEL_EMPLOYEE_INFO_STRU result;
                                result = (AnvizNew.CCHEX_RET_DEL_EMPLOYEE_INFO_STRU)Marshal.PtrToStructure(pBuff, typeof(AnvizNew.CCHEX_RET_DEL_EMPLOYEE_INFO_STRU));
                                if (result.Result == 0)
                                {
                                    byte[] temp = new byte[8];
                                    int i;
                                    for (i = 0; i < 5; i++)
                                    {
                                        temp[8 - 4 - i] = result.EmployeeId[i];
                                    }
                                    dbg_info(BitConverter.ToInt64(temp, 0).ToString());
                                    MessageBox.Show("Delete person OK  ID == " + BitConverter.ToInt64(temp, 0).ToString());
                                }
                                else
                                {
                                    MessageBox.Show("Delete person Fail!");
                                }

                            }
                            command.CommandText = $"DELETE FROM Empleados WHERE IdEmpleado = @id";
                            command.Parameters.AddWithValue("@id", idEmpleado);
                            command.ExecuteNonQuery();
                            CargarListViewEmpleados();
                            MessageBox.Show("El empleado ha sido eliminado de la base de datos.");
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

        private void ObtenerHorasTrabajadasPorDia()
        {
            command.CommandText = @"
            SELECT 
                IdEmpleado,
                DATE(fecha) AS fecha,
                MIN(fecha) AS horaEntrada,
                MAX(fecha) AS horaSalida,
                (strftime('%s', MAX(fecha)) - strftime('%s', MIN(fecha))) / 3600.0 AS horasTrabajadas
            FROM 
                Registros
            WHERE 
                tiporegistro IN (0, 1)
            GROUP BY 
                idempleado,
                DATE(fecha)
            HAVING 
                COUNT(*) >= 2
            ORDER BY 
                idempleado, fecha;";

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string idEmpleado = reader["IdEmpleado"].ToString();
                    string fecha = reader["Fecha"].ToString();
                    string horaEntrada = reader["horaEntrada"].ToString();
                    string horaSalida = reader["horaSalida"].ToString();
                    double horasTrabajadas = Convert.ToDouble(reader["horasTrabajadas"]);

                    dbg_info($"Empleado: {idEmpleado}, Fecha: {fecha}, Horas Trabajadas: {horasTrabajadas}");
                }
                dbg_info("Horas trabajadas obtenidas correctamente");
            }

        }

        private byte[] string_to_my_unicodebyte(int bytemax, string str)
        {
            if (bytemax > 0)
            {
                byte[] byte_out = new byte[bytemax];// unicode
                int i;                           // 
                byte[] bytestr = Encoding.Unicode.GetBytes(str);
                for (i = 0; i < bytemax; i += 2)
                {
                    if (i < bytestr.Length)
                    {
                        byte_out[i] = bytestr[i + 1];
                        byte_out[i + 1] = bytestr[i];
                        continue;
                    }
                    byte_out[i] = 0;
                }
                return byte_out;
            }
            return null;
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
        private string byte_to_unicode_string(byte[] StrData)
        {
            int i;
            byte[] StringData = new byte[StrData.Length];

            for (i = 0; i + 1 < StringData.Length; i += 2)
            {
                StringData[i] = StrData[i + 1];
                StringData[i + 1] = StrData[i];
            }
            return Encoding.Unicode.GetString(StringData).Replace("\0", "");
            //return Encoding.UTF8.GetString(StringData).Replace("\0", "");
        }
    }
}