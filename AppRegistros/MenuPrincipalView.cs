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
        IntPtr anviz_handle;
        int[] dev_idx = new int[1];
        int ret = 0;
        int[] Type = new int[1];
        int len = 0;
        IntPtr pBuff;
        private static readonly object lockObject = new object();

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
                IdEmpleado INTEGER PRIMARY KEY,
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
                IdEmpleado INTEGER,
                Fecha TEXT,
                TipoRegistro TEXT,
                FOREIGN KEY(IdEmpleado) REFERENCES Empleados(IdEmpleado)
            );";
            command.ExecuteNonQuery();
        }
        public void InsertarEmpleado(int idEmpleado, string nombreEmpleado)
        {
            command.CommandText = "SELECT COUNT(*) FROM Empleados WHERE IdEmpleado = @IdEmpleado";
            command.Parameters.AddWithValue("@IdEmpleado", idEmpleado);

            int count = Convert.ToInt32(command.ExecuteScalar());
            command.Parameters.Clear();

            if (count == 0)
            {
                command.CommandText = @"
                INSERT INTO Empleados (IdEmpleado, NombreEmpleado) 
                VALUES (@IdEmpleado, @NombreEmpleado)";
                command.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
                command.Parameters.AddWithValue("@NombreEmpleado", nombreEmpleado);
                command.ExecuteNonQuery();
            }
        }
        public void InsertarRegistro(int idEmpleado, string tipoRegistro, string dateStr)
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
                        MessageBox.Show("No se pudo iniciar el programa.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        //ObtenerInfoEmpleado();
                        //CargarListViewEmpleados();
                        DownloadAllRecords();
                        groupBoxInicio.Hide();
                    }
                    else
                    {
                        string errorMsg = "Conexión fallida. RET: " + ret;

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
                }
                else
                {
                    MessageBox.Show("Error en la conexión con el dispositivo. Intente conectarse de nuevo", "Network Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Ingrese un puerto válido. (5010)", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void LoguearDispositivo()
        {
            lock (lockObject)
            {
                len = 25000;
                pBuff = Marshal.AllocHGlobal(len);

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
                        // Informacion sobre configuracion de red = 29
                        case (int)AnvizNew.MsgType.CCHEX_RET_GET_BASIC_CFG_TYPE:
                            dbg_info("Mensaje de tipo 29 recibido");
                            break;
                        // Info de registros = 1
                        case (int)AnvizNew.MsgType.CCHEX_RET_RECORD_INFO_TYPE:
                            {
                                dbg_info("Mensaje de tipo 1 recibido");
                            }
                            break;
                    }
                    ret = AnvizNew.CChex_Update(anviz_handle, dev_idx, Type, pBuff, len);
                }
                Marshal.FreeHGlobal(pBuff);
            }
        }


        // EMPLEADOS
        private void btnBuscarEmpleadoPorId_Click(object sender, EventArgs e)
        {
            listViewEmpleados.Items.Clear();
            command.Parameters.Clear();

            string idEmpleado = txtIdEmpleado.Text;
            string nombreEmpleado = txtIdEmpleado.Text;
            string dni = txtIdEmpleado.Text;

            command.CommandText = "SELECT * FROM Empleados WHERE IdEmpleado = @idEmpleado OR NombreEmpleado LIKE @nombre OR DNI = @dni";
            command.Parameters.AddWithValue("@idEmpleado", idEmpleado);
            command.Parameters.AddWithValue("@nombre", "%" + nombreEmpleado + "%");
            command.Parameters.AddWithValue("@dni", dni);

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
            lock (lockObject)
            {
                pBuff = IntPtr.Zero;
                ret = AnvizNew.CChex_ListPersonInfo(anviz_handle, dev_idx[0]);

                len = Marshal.SizeOf(typeof(CCHEX_RET_PERSON_INFO_STRU)) * 100;
                pBuff = Marshal.AllocHGlobal(len);

                if (ret > 0)
                {
                    Thread.Sleep(100);
                    ret = AnvizNew.CChex_Update(anviz_handle, dev_idx, Type, pBuff, len);
                    dbg_info("CChex_Update returned: " + ret);

                    while (ret > 0 && Type[0] == (int)AnvizNew.MsgType.CCHEX_RET_LIST_PERSON_INFO_TYPE)
                    {
                        CCHEX_RET_PERSON_INFO_STRU infoEmpleado = (CCHEX_RET_PERSON_INFO_STRU)Marshal.PtrToStructure(pBuff, typeof(CCHEX_RET_PERSON_INFO_STRU));

                        // Si el sistema es Little Endian, no necesitas cambiar el orden, pero si es Big Endian, deberías invertir los bytes
                        if (BitConverter.IsLittleEndian)
                        {
                            Array.Reverse(infoEmpleado.EmployeeId);
                        }
                        int idEmpleado = BitConverter.ToInt32(infoEmpleado.EmployeeId, 0);
                        string nombreEmpleado = byte_to_string(infoEmpleado.EmployeeName).TrimEnd('\0');

                        dbg_info($"Empleado - ID: {idEmpleado}, Nombre: {nombreEmpleado}");

                        InsertarEmpleado(idEmpleado, nombreEmpleado);

                        ret = AnvizNew.CChex_Update(anviz_handle, dev_idx, Type, pBuff, len);
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo obtener la informacion de los empleados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Marshal.FreeHGlobal(pBuff);
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
        private void btnCargarEmpleados_Click(object sender, EventArgs e)
        {
            ObtenerInfoEmpleado();
            CargarListViewEmpleados();
        }


        // CONFIGURACION
        private void btnConfigRed_Click(object sender, EventArgs e)
        {
            lock (this.btnConfigRed)
            {
                len = Marshal.SizeOf(typeof(CCHEX_RET_GETNETCFG_STRU)) * 100;
                pBuff = Marshal.AllocHGlobal(len);

                ret = AnvizNew.CChex_GetNetConfig(anviz_handle, dev_idx[0]);
                Thread.Sleep(100);
                ret = AnvizNew.CChex_Update(anviz_handle, dev_idx, Type, pBuff, len);

                dbg_info("CChex_Update returned: " + ret);

                if (ret > 0)
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
                else
                {
                    dbg_info("Datos devueltos invalidos o espacio del buffer insuficiente.");
                }
                Marshal.FreeHGlobal(pBuff);
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
                WHERE (r.IdEmpleado = @idEmpleado OR e.NombreEmpleado LIKE @nombre OR e.DNI LIKE @nombre)
                AND r.Fecha BETWEEN @fechaInicio AND @fechaFin 
                AND r.TipoRegistro IN (0, 1)
                GROUP BY r.IdEmpleado, DATE(r.Fecha)
                HAVING COUNT(*) >= 2
                ORDER BY Dia";
            command.Parameters.AddWithValue("@idEmpleado", idEmpleado);
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
            lock (lockObject)
            {
                ret = AnvizNew.CChex_DownloadAllRecords(anviz_handle, dev_idx[0]);
                if (ret > 0)
                {
                    pBuff = IntPtr.Zero;
                    len = 100000;
                    pBuff = Marshal.AllocHGlobal(len);

                    Thread.Sleep(200);
                    ret = AnvizNew.CChex_Update(anviz_handle, dev_idx, Type, pBuff, len);
                    dbg_info("CChex_Update returned: " + ret);

                    if (ret > 0)
                    {
                        while (ret > 0)
                        {
                            dbg_info("Recibido mensaje de tipo: " + Type[0]);
                            AnvizNew.CCHEX_RET_RECORD_INFO_STRU record_info;
                            record_info = (AnvizNew.CCHEX_RET_RECORD_INFO_STRU)Marshal.PtrToStructure(pBuff, typeof(AnvizNew.CCHEX_RET_RECORD_INFO_STRU));

                            byte[] dateBytes = record_info.Date.Reverse().ToArray();
                            int secondsSince20000102 = BitConverter.ToInt32(dateBytes, 0);

                            // Fecha base: 2 de enero de 2000
                            DateTime baseDate = new DateTime(2000, 1, 2, 0, 0, 0, DateTimeKind.Utc);

                            // Añadir los segundos a la fecha base para obtener la fecha y hora del registro
                            DateTime recordDate = baseDate.AddSeconds(secondsSince20000102);
                            string dateStr = recordDate.ToString("yyyy-MM-dd HH:mm:ss");
                            if (BitConverter.IsLittleEndian)
                            {
                                Array.Reverse(record_info.EmployeeId);
                            }
                            int idEmpleado = BitConverter.ToInt32(record_info.EmployeeId, 0);
                            string tipoRegistro = record_info.RecordType.ToString();

                            InsertarRegistro(idEmpleado, tipoRegistro, dateStr);
                            ret = AnvizNew.CChex_Update(anviz_handle, dev_idx, Type, pBuff, len);
                        }
                    }
                    else
                    {
                        dbg_info("Datos devueltos invalidos o espacio del buffer insuficiente.");
                    }
                    Marshal.FreeHGlobal(pBuff);
                }
                else
                {
                    MessageBox.Show("Error al descargar los registros", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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


        // EDITAR Y ELIMINAR EMPLEADOS
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (listViewEmpleados.SelectedItems.Count > 0)
            {
                ListViewItem itemSeleccionado = listViewEmpleados.SelectedItems[0];
                string idEmpleadoStr = itemSeleccionado.SubItems[0].Text;

                int idEmpleado;
                if (int.TryParse(idEmpleadoStr, out idEmpleado))
                {
                    EditarEmpleadoView editarEmpleadoView = new EditarEmpleadoView(anviz_handle, dev_idx[0], Type[0], idEmpleado);
                    editarEmpleadoView.ShowDialog();
                    CargarListViewEmpleados();
                }
                else
                {
                    MessageBox.Show("El ID del empleado no es un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                string idEmpleadoAEliminarStr = itemSeleccionado.SubItems[0].Text;
                string nombreEmpleado = itemSeleccionado.SubItems[1].Text;

                int idEmpleadoAEliminar = Convert.ToInt32(idEmpleadoAEliminarStr);

                DialogResult respuesta = MessageBox.Show($"Está seguro que quiere eliminar al empleado {nombreEmpleado}?",
                                   "Eliminar empleado",
                                   MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    lock (this.btnEliminar)
                    {
                        byte[] idEmpleadoAEliminarBytesFull = BitConverter.GetBytes(idEmpleadoAEliminar);

                        if (BitConverter.IsLittleEndian)
                        {
                            Array.Reverse(idEmpleadoAEliminarBytesFull);
                        }

                        byte[] idEmpleadoAEliminar5Bytes = new byte[5];
                        Array.Copy(idEmpleadoAEliminarBytesFull, 0, idEmpleadoAEliminar5Bytes, 1, 4);

                        AnvizNew.CCHEX_DEL_PERSON_INFO_STRU delete_item = new CCHEX_DEL_PERSON_INFO_STRU
                        {
                            EmployeeId = idEmpleadoAEliminar5Bytes,
                            operation = 0xFF
                        };
                        ret = AnvizNew.CChex_DeletePersonInfo(anviz_handle, dev_idx[0], ref delete_item);

                        len = Marshal.SizeOf(typeof(CCHEX_DEL_PERSON_INFO_STRU)) * 100;
                        pBuff = Marshal.AllocHGlobal(len);

                        Thread.Sleep(100);
                        ret = AnvizNew.CChex_Update(anviz_handle, dev_idx, Type, pBuff, len);
                        dbg_info("CChex_Update returned: " + ret);

                        if (ret > 0)
                        {
                            AnvizNew.CCHEX_RET_DEL_EMPLOYEE_INFO_STRU result;
                            result = (AnvizNew.CCHEX_RET_DEL_EMPLOYEE_INFO_STRU)Marshal.PtrToStructure(pBuff, typeof(AnvizNew.CCHEX_RET_DEL_EMPLOYEE_INFO_STRU));
                            result.EmployeeId = idEmpleadoAEliminar5Bytes;
                            if (result.Result == 0)
                            {
                                MessageBox.Show("Empleado eliminado correctamente del dispositivo. Employee ID: " + Employee_array_to_srring(result.EmployeeId));

                                // Eliminar de la base de datos
                                command.CommandText = $"DELETE FROM Empleados WHERE IdEmpleado = @id";
                                command.Parameters.AddWithValue("@id", idEmpleadoAEliminar);
                                command.ExecuteNonQuery();
                                MessageBox.Show("Empleado eliminado correctamente. ID: " + Employee_array_to_srring(result.EmployeeId));
                            }
                            else
                            {
                                MessageBox.Show("Error al eliminar el empleado");
                            }
                        }
                        Marshal.FreeHGlobal(pBuff);

                        CargarListViewEmpleados();
                    }
                }
            }
            else
            {
                MessageBox.Show("No se seleccionó ningún empleado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        // UTILIDADES
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