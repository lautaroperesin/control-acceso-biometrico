﻿using AnvizDemo;
using AppRegistros.Utils;
using ClosedXML.Excel;
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
        public void InsertarEmpleado(int idEmpleado, string nombreEmpleado, string dni, string areaTrabajo)
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
            len = 10000;
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
                                        "Dispositivo conectado  ---  ID: " + dev_info.MachineId +
                                        "  ---  Dirección IP y puerto: " + byte_to_string(dev_info.Addr) +
                                        "  ---  Version: " + byte_to_string(dev_info.Version);

                            log_add_string(info_buff);
                        }
                        break;
                    // Estado del dispositivo = 19
                    case (int)AnvizNew.MsgType.CCHEX_RET_DEV_STATUS_TYPE:
                        {
                            AnvizNew.CCHEX_RET_DEV_STATUS_STRU status;
                            status = (AnvizNew.CCHEX_RET_DEV_STATUS_STRU)Marshal.PtrToStructure(pBuff, typeof(AnvizNew.CCHEX_RET_DEV_STATUS_STRU));
                            string info_buff =
                                    "Estado ---  " + "Empleados: " + status.EmployeeNum.ToString()
                                    + "  ---  Huellas: " + status.FingerPrtNum.ToString()
                                    + "  ---  Registros: " + status.TotalRecNum.ToString()
                                    + "  ---  Nuevos Reg: " + status.NewRecNum.ToString();
                            log_add_string(info_buff);
                        }
                        break;
                    // Configuracion basica = 29
                    case (int)AnvizNew.MsgType.CCHEX_RET_GET_BASIC_CFG_TYPE:
                        dbg_info("Mensaje de tipo 29 recibido e ignorado ---------------------------------------------------------------------");
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

                            string info_buff = "Últimos Registros:  ----  Fecha: " + dateStr
                                + "  ---  EmpleadoID: " + Employee_array_to_srring(record_info.EmployeeId)
                                + "  ---  Tipo de registro (0=Entrada / 1=Salida): " + record_info.RecordType.ToString();
                            log_add_string(info_buff);
                        }
                        break;
                }
                ret = AnvizNew.CChex_Update(anviz_handle, dev_idx, Type, pBuff, len);
            }
            Marshal.FreeHGlobal(pBuff);

            // Obtener información de empleados y mostrarlos en el listview
            ObtenerInfoEmpleado();
            CargarListViewEmpleados();
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
            pBuff = IntPtr.Zero;
            ret = AnvizNew.CChex_ListPersonInfo(anviz_handle, dev_idx[0]);

            len = Marshal.SizeOf(typeof(CCHEX_RET_PERSON_INFO_STRU)) * 100;
            pBuff = Marshal.AllocHGlobal(len);

            if (ret > 0)
            {
                Thread.Sleep(800);
                ret = AnvizNew.CChex_Update(anviz_handle, dev_idx, Type, pBuff, len);
                dbg_info("CChex_Update returned: " + ret);

                if (ret > 0)
                {
                    while (ret > 0 && Type[0] == (int)AnvizNew.MsgType.CCHEX_RET_LIST_PERSON_INFO_TYPE)
                    {
                        CCHEX_RET_PERSON_INFO_STRU infoEmpleado = (CCHEX_RET_PERSON_INFO_STRU)Marshal.PtrToStructure(pBuff, typeof(CCHEX_RET_PERSON_INFO_STRU));

                        // Si el sistema es Little Endian, no cambiar el orden, pero si es Big Endian, invertir los bytes
                        if (BitConverter.IsLittleEndian)
                        {
                            Array.Reverse(infoEmpleado.EmployeeId);
                        }
                        int idEmpleado = BitConverter.ToInt32(infoEmpleado.EmployeeId, 0);
                        string nombreEmpleado = byte_to_string(infoEmpleado.EmployeeName).TrimEnd('\0');
                        string dni = string.Empty;
                        string areaTrabajo = string.Empty;

                        dbg_info($"Empleado - ID: {idEmpleado}, Nombre: {nombreEmpleado}");

                        InsertarEmpleado(idEmpleado, nombreEmpleado, dni, areaTrabajo);

                        ret = AnvizNew.CChex_Update(anviz_handle, dev_idx, Type, pBuff, len);
                    }
                    dbg_info("Información de empleados obtenida correctamente -----------------------------------");
                }
                else
                {
                    dbg_info("Datos devueltos invalidos o espacio del buffer insuficiente. Error al obtener los empleados del dispositivo. Fallo el update");
                }
            }
            else
            {
                MessageBox.Show("No se pudo obtener la informacion de los empleados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Marshal.FreeHGlobal(pBuff);
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
            pBuff = IntPtr.Zero;
            len = 1000; //Marshal.SizeOf(typeof(CCHEX_RET_GETNETCFG_STRU)) + 100;
            pBuff = Marshal.AllocHGlobal(len);

            ret = AnvizNew.CChex_GetNetConfig(anviz_handle, dev_idx[0]);

            Thread.Sleep(500);
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
            CargarListViewRegistros();
        }
        private void CargarListViewRegistros()
        {
            listViewRegistros.Items.Clear();
            command.Parameters.Clear();

            string idEmpleado = txtId.Text;
            DateTime fechaInicio = dateTimeFechaInicio.Value.Date;
            DateTime fechaFin = dateTimeFechaFinal.Value.Date.AddDays(1).AddSeconds(-1);
            double totalHorasTrabajadas = 0.0;

            command.CommandText = @"
    SELECT r.IdEmpleado, e.NombreEmpleado, e.AreaTrabajo, 
           DATE(r.Fecha) AS Dia, 
           MIN(r.Fecha) AS Entrada, 
           MAX(r.Fecha) AS Salida,
           (strftime('%s', MAX(r.Fecha)) - strftime('%s', MIN(r.Fecha))) / 3600.0 AS HorasTrabajadas,
           MAX(r.TipoRegistro) AS UltimoRegistro
    FROM Registros r
    JOIN Empleados e ON r.IdEmpleado = e.IdEmpleado
    WHERE (r.IdEmpleado = @idEmpleado OR e.NombreEmpleado LIKE @nombre)
    AND r.Fecha BETWEEN @fechaInicio AND @fechaFin 
    AND r.TipoRegistro IN (0, 1)
    GROUP BY r.IdEmpleado, DATE(r.Fecha)
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
                DateTime entrada = Convert.ToDateTime(reader["Entrada"]);
                DateTime salida = reader["Salida"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["Salida"]);
                double horasTrabajadas = Convert.ToDouble(reader["HorasTrabajadas"]);

                // Si no hay salida registrada o la jornada es mayor a 12 horas
                if (salida == DateTime.MinValue || horasTrabajadas > 12.0)
                {
                    // Marcar la salida automáticamente 12 horas después de la entrada
                    salida = entrada.AddHours(12);

                    // Actualizar en la base de datos
                    command.CommandText = "INSERT INTO Registros (IdEmpleado, Fecha, TipoRegistro) VALUES (@idEmpleado, @fechaSalida, 1)";
                    command.Parameters.AddWithValue("@idEmpleado", reader["IdEmpleado"].ToString());
                    command.Parameters.AddWithValue("@fechaSalida", salida);
                    command.ExecuteNonQuery();

                    // Recalcular horas trabajadas
                    horasTrabajadas = 0;
                }

                item.SubItems.Add(entrada.ToString("HH:mm:ss"));
                item.SubItems.Add(salida.ToString("HH:mm:ss"));
                item.SubItems.Add(horasTrabajadas.ToString("F2"));

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
                len = 100000;
                pBuff = Marshal.AllocHGlobal(len);

                Thread.Sleep(800);
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
                    dbg_info("Registros descargados correctamente ---------------------------------");
                }
                else
                {
                    dbg_info("Datos devueltos invalidos o espacio del buffer insuficiente. Error al descargar los registros. Falló el update");
                }
                Marshal.FreeHGlobal(pBuff);
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
                string idEmpleadoStr = itemSeleccionado.SubItems[0].Text;

                int idEmpleado;
                if (int.TryParse(idEmpleadoStr, out idEmpleado))
                {
                    EditarEmpleadoView editarEmpleadoView = new EditarEmpleadoView(anviz_handle, dev_idx[0], idEmpleado);
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

                    if (ret > 0)
                    {
                        // Eliminar de la base de datos
                        command.CommandText = $"DELETE FROM Empleados WHERE IdEmpleado = @id";
                        command.Parameters.AddWithValue("@id", idEmpleadoAEliminar);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Empleado eliminado correctamente. ID: " + idEmpleadoAEliminar);
                    }
                    CargarListViewEmpleados();
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
        private void log_add_string(string info_buff)
        {
            bool scroll = false;
            if (this.listBoxLog.Items.Count >= 1000)
            {
                this.listBoxLog.Items.Clear();
            }

            if (this.listBoxLog.TopIndex == this.listBoxLog.Items.Count - (int)(this.listBoxLog.Height / this.listBoxLog.ItemHeight))
            {
                scroll = true;
            }
            this.listBoxLog.Items.Add(info_buff);
            if (scroll)
            {
                this.listBoxLog.TopIndex = this.listBoxLog.Items.Count - (int)(this.listBoxLog.Height / this.listBoxLog.ItemHeight);
            }

        }
        private string Employee_array_to_srring(byte[] EmployeeId)
        {
            byte[] temp = new byte[8];
            int i;
            for (i = 0; i < 5; i++)
            {
                temp[8 - 4 - i] = EmployeeId[i];
            }
            return BitConverter.ToInt64(temp, 0).ToString();
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            // Crear un nuevo archivo Excel
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Registros");

                // Añadir encabezados de columna
                worksheet.Cell(1, 1).Value = "IdEmpleado";
                worksheet.Cell(1, 2).Value = "NombreEmpleado";
                worksheet.Cell(1, 3).Value = "AreaTrabajo";
                worksheet.Cell(1, 4).Value = "Fecha";
                worksheet.Cell(1, 5).Value = "Entrada";
                worksheet.Cell(1, 6).Value = "Salida";
                worksheet.Cell(1, 7).Value = "HorasTrabajadas";

                // Recorrer los elementos del ListView y añadir filas al archivo Excel
                for (int i = 0; i < listViewRegistros.Items.Count; i++)
                {
                    var item = listViewRegistros.Items[i];
                    worksheet.Cell(i + 2, 1).Value = item.SubItems[0].Text; // IdEmpleado
                    worksheet.Cell(i + 2, 2).Value = item.SubItems[1].Text; // NombreEmpleado
                    worksheet.Cell(i + 2, 3).Value = item.SubItems[2].Text; // AreaTrabajo
                    worksheet.Cell(i + 2, 4).Value = item.SubItems[3].Text; // Fecha
                    worksheet.Cell(i + 2, 5).Value = item.SubItems[4].Text; // Entrada
                    worksheet.Cell(i + 2, 6).Value = item.SubItems[5].Text; // Salida
                    worksheet.Cell(i + 2, 7).Value = item.SubItems[6].Text; // HorasTrabajadas
                }

                // Guardar el archivo Excel
                using (var saveFileDialog = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx", Title = "Guardar como Excel" })
                {
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        workbook.SaveAs(saveFileDialog.FileName);
                        MessageBox.Show("Archivo exportado exitosamente.", "Exportar a Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}