using System.Runtime.InteropServices;

namespace AnvizDemo
{
    public class AnvizNew
    {
        public enum MsgType : int
        {
            CCHEX_RET_RECORD_INFO_TYPE              = 1,
            CCHEX_RET_DEV_LOGIN_TYPE,
            CCHEX_RET_DEV_LOGOUT_TYPE, 
            CCHEX_RET_DLFINGERPRT_TYPE              = 4,
            CCHEX_RET_ULFINGERPRT_TYPE              = 5,
            CCHEX_RET_ULEMPLOYEE_INFO_TYPE          = 6,
            CCHEX_RET_ULEMPLOYEE2_INFO_TYPE         = 7,
            CCHEX_RET_ULEMPLOYEE2_UNICODE_INFO_TYPE = 8,
            CCHEX_RET_LIST_PERSON_INFO_TYPE         = 9,
            CCHEX_RET_MSGGETBYIDX_INFO_TYPE         = 12,
            CCHEX_RET_MSGGETBYIDX_UNICODE_INFO_TYPE = 13,
            CCHEX_RET_MSGADDNEW_INFO_TYPE           = 14,
            CCHEX_RET_MSGADDNEW_UNICODE_INFO_TYPE   = 15,
            CCHEX_RET_MSGDELBYIDX_INFO_TYPE         = 16,
            CCHEX_RET_MSGGETALLHEAD_INFO_TYPE       = 17,

            CCHEX_RET_REBOOT_TYPE                   = 18,
            CCHEX_RET_DEV_STATUS_TYPE               = 19,
            CCHEX_RET_MSGGETALLHEADUNICODE_INFO_TYPE= 20,
            CCHEX_RET_SETTIME_TYPE                  = 21,
            CCHEX_RET_UPLOADFILE_TYPE               = 22,// = 22
            CCHEX_RET_GETNETCFG_TYPE                = 23,
            CCHEX_RET_SETNETCFG_TYPE                = 24,
            CCHEX_RET_GET_SN_TYPE                   = 25,
            CCHEX_RET_SET_SN_TYPE                   = 26,
            CCHEX_RET_DLEMPLOYEE_3_TYPE             = 27, // 761
            CCHEX_RET_ULEMPLOYEE_3_TYPE             = 28, // 761
            CCHEX_RET_GET_BASIC_CFG_TYPE            = 29,
            CCHEX_RET_SET_BASIC_CFG_TYPE            = 30,
            CCHEX_RET_DEL_PERSON_INFO_TYPE          = 31,
            CCHEX_RET_DEL_RECORD_OR_FLAG_INFO_TYPE  = 33,
            CCHEX_RET_MSGGETBYIDX_UNICODE_S_DATE_INFO_TYPE      = 34,
            CCHEX_RET_MSGADDNEW_UNICODE_S_DATE_INFO_TYPE        = 35,
            CCHEX_RET_MSGGETALLHEADUNICODE_S_DATE_INFO_TYPE     = 36,


            CCHEX_RET_GET_BASIC_CFG2_TYPE           = 37,
            CCHEX_RET_SET_BASIC_CFG2_TYPE           = 38,
            CCHEX_RET_GETTIME_TYPE                  = 39,
            CCHEX_RET_INIT_USER_AREA_TYPE           = 40,
            CCHEX_RET_INIT_SYSTEM_TYPE              = 41,
            CCHEX_RET_GET_PERIOD_TIME_TYPE          = 42,
            CCHEX_RET_SET_PERIOD_TIME_TYPE          = 43,
            CCHEX_RET_GET_TEAM_INFO_TYPE            = 44,
            CCHEX_RET_SET_TEAM_INFO_TYPE            = 45,
            CCHEX_RET_ADD_FINGERPRINT_ONLINE_TYPE   = 46,
            CCHEX_RET_FORCED_UNLOCK_TYPE            = 47,
            CCHEX_RET_UDP_SEARCH_DEV_TYPE           = 48,
            CCHEX_RET_UDP_SET_DEV_CONFIG_TYPE       = 49,


            //

            CCHEX_RET_GET_INFOMATION_CODE_TYPE      = 50,
            CCHEX_RET_SET_INFOMATION_CODE_TYPE      = 51,
            CCHEX_RET_GET_BELL_INFO_TYPE            = 52,
            CCHEX_RET_SET_BELL_INFO_TYPE            = 53,
            CCHEX_RET_LIVE_SEND_ATTENDANCE_TYPE     = 54,
            CCHEX_RET_GET_USER_ATTENDANCE_STATUS_TYPE   = 55,
            CCHEX_RET_SET_USER_ATTENDANCE_STATUS_TYPE   = 56,
            CCHEX_RET_CLEAR_ADMINISTRAT_FLAG_TYPE   = 57,
            CCHEX_RET_GET_SPECIAL_STATUS_TYPE       = 58,
            CCHEX_RET_GET_ADMIN_CARD_PWD_TYPE       = 59,
            CCHEX_RET_SET_ADMIN_CARD_PWD_TYPE       = 60,
            CCHEX_RET_GET_DST_PARAM_TYPE            = 61,
            CCHEX_RET_SET_DST_PARAM_TYPE            = 62,
            CCHEX_RET_GET_DEV_EXT_INFO_TYPE         = 63,
            CCHEX_RET_SET_DEV_EXT_INFO_TYPE         = 64,
            CCHEX_RET_GET_BASIC_CFG3_TYPE           = 65,
            CCHEX_RET_SET_BASIC_CFG3_TYPE           = 66,
            CCHEX_RET_CONNECTION_AUTHENTICATION_TYPE    = 67,
            CCHEX_RET_GET_RECORD_NUMBER_TYPE            = 68,
            CCHEX_RET_GET_RECORD_BY_EMPLOYEE_TIME_TYPE  = 69,

            CCHEX_RET_GET_RECORD_INFO_STATUS_TYPE   = 70,
            CCHEX_RET_GET_NEW_RECORD_INFO_TYPE      = 71,

            CCHEX_RET_ULEMPLOYEE2W2_INFO_TYPE       = 72,
            CCHEX_RET_GET_BASIC_CFG5_TYPE           = 73,
            CCHEX_RET_SET_BASIC_CFG5_TYPE           = 74,
            CCHEX_RET_GET_CARD_ID_TYPE              = 75,
            CCHEX_RET_SET_DEV_CURRENT_STATUS_TYPE   = 76,
            CCHEX_RET_GET_URL_TYPE                  = 77,
            CCHEX_RET_SET_URL_TYPE                  = 78,
            CCHEX_RET_GET_STATUS_SWITCH_TYPE        = 79,
            CCHEX_RET_SET_STATUS_SWITCH_TYPE        = 80,
            CCHEX_RET_GET_STATUS_SWITCH_EXT_TYPE    = 81,
            CCHEX_RET_SET_STATUS_SWITCH_EXT_TYPE    = 82,
            CCHEX_RET_UPDATEFILE_STATUS_TYPE        = 83,

            CCHEX_RET_GET_MACHINE_ID_TYPE           = 84,
            CCHEX_RET_SET_MACHINE_ID_TYPE           = 85,

            CCHEX_RET_GET_MACHINE_TYPE_TYPE         = 86,

            CCHEX_RET_UPLOAD_RECORD_TYPE            = 87,

            CCHEX_RET_GET_ONE_EMPLOYEE_INFO_TYPE    = 88,

            CCHEX_RET_ULEMPLOYEE_VER_4_NEWID_TYPE   = 89,

            CCHEX_RET_MANAGE_LOG_RECORD_TYPE        = 90,

            CCHEX_RET_PICTURE_GET_TOTAL_NUMBER_TYPE = 91,
            CCHEX_RET_PICTURE_GET_ALL_HEAD_TYPE = 92,
            CCHEX_RET_PICTURE_GET_DATA_BY_EID_TIME_TYPE = 93,
            CCHEX_RET_PICTURE_DEL_DATA_BY_EID_TIME_TYPE = 94,
            CCHEX_RET_LIVE_SEND_SPECIAL_STATUS_TYPE = 95,

            CCHEX_RET_TM_ALL_RECORD_INFO_TYPE = 150,              //CCHEX_RET_TM_RECORD_INFO_STRU
            CCHEX_RET_TM_NEW_RECORD_INFO_TYPE = 151,              //CCHEX_RET_TM_RECORD_INFO_STRU
            CCHEX_RET_TM_LIVE_SEND_RECORD_INFO_TYPE = 152,              //CCHEX_RET_TM_LIVE_SEND_RECORD_INFO_STRU
            CCHEX_RET_TM_UPLOAD_RECORD_INFO_TYPE = 153,              //CCHEX_RET_TM_UPLOAD_RECORD_STRU
            CCHEX_RET_TM_RECORD_BY_EMPLOYEE_TIME_TYPE = 154,              //CCHEX_RET_TM_RECORD_INFO_STRU

            CCHEX_RET_GET_T_RECORD_NUMBER_TYPE = 155,              //CCHEX_RET_GET_T_RECORD_NUMBER_STRU
            CCHEX_RET_GET_T_RECORD_TYPE = 156,              //CCHEX_RET_GET_T_RECORD_STRU
            CCHEX_RET_GET_T_PICTURE_BY_RECORD_ID_TYPE = 157,              //CCHEX_RET_GET_PICTURE_BY_RECORD_ID_STRU
            CCHEX_RET_DEL_T_PICTURE_BY_RECORD_ID_TYPE = 158,              //CCHEX_RET_DEL_PICTURE_BY_RECORD_ID_STRU


            CCHEX_RET_CLINECT_CONNECT_FAIL_TYPE     = 200,
            CCHEX_RET_DEV_LOGIN_CHANGE_TYPE         = 201,

            CCHEX_RET_RECORD_INFO_CARD_BYTE7_TYPE = 251,
        };
        public const int FP_LEN = 15360;

            [StructLayout(LayoutKind.Sequential, Size = 27, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_NETCFG_INFO_STRU
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] IpAddr;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] IpMask;   //子网掩码
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] MacAddr;  //MAC地址
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] GwAddr;   //网关地址
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] ServAddr; //服务器ip
            public byte RemoteEnable;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] Port;     //端口
            public byte Mode;       //方式
            public byte DhcpEnable;
        } //27 bytes

        [StructLayout(LayoutKind.Sequential, Size = 8, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_RET_COMMON_STRU
        {
            public uint MachineId;
            public int Result; //0 ok, -1 err
        };

        [StructLayout(LayoutKind.Sequential, Size = 8, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_RET_REBOOT_STRU
        {
            public uint MachineId;
            public int Result;
        }

        //数组网络
        [StructLayout(LayoutKind.Sequential, Size = 8, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_RET_SETNETCFG_STRU
        {
            public uint MachineId;
            public int Result;
        }

        //更新回复
        [StructLayout(LayoutKind.Sequential, Size = 8, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_RET_UPLOADFILE_STRU
        {
            public uint MachineId;
            public int Result; //0 ok, -1 err
            public uint TotalBytes;
            public uint SendBytes;
        }

        //网络配置回复
        [StructLayout(LayoutKind.Sequential, Size = 8, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_RET_GETNETCFG_STRU
        {
            public uint MachineId;
            public int Result; //0 ok, -1 err
            public CCHEX_NETCFG_INFO_STRU Cfg;
        }

        [StructLayout(LayoutKind.Sequential, Size = 14, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_UPLOAD_RECORD_INFO_STRU
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] EmployeeId;    //
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] date;          //日期时间
            public byte back_id;           //备份号
            public byte record_type;       //记录类型
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] work_type;      //工种        (ONLY use 3bytes )
        }
        [StructLayout(LayoutKind.Sequential, Size = 37, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_UPLOAD_RECORD_INFO_STRU_VER_4_NEWID
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 28)]
            public byte[] EmployeeId;    //
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] date;          //日期时间
            public byte back_id;           //备份号
            public byte record_type;       //记录类型
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] work_type;      //工种        (ONLY use 3bytes )
        }

        [StructLayout(LayoutKind.Sequential, Size = 14+4+4, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_RET_LIVE_SEND_ATTENDANCE_STRU
        {
            public uint MachineId;         //机器号
            int Result; //0 ok, -1 err
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] EmployeeId;    //
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Date;          //日期时间
            public byte BackId;           //备份号
            public byte RecordType;       //记录类型
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] WorkType;      //工种        (ONLY use 3bytes )
        }

        [StructLayout(LayoutKind.Sequential, Size = 28, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_RET_RECORD_INFO_STRU
        {
            public uint MachineId;         //机器号
            public byte NewRecordFlag;    //是否是新记录
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] EmployeeId;    //
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Date;          //日期时间
            public byte BackId;           //备份号
            public byte RecordType;       //记录类型
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] WorkType;      //工种        (ONLY use 3bytes )
            public byte Rsv;

            public uint CurIdx;             //add  VER 22
            public uint TotalCnt;           //add  VER 22
        }

        [StructLayout(LayoutKind.Sequential, Size = 51, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_RET_RECORD_INFO_STRU_VER_4_NEWID
        {
            public uint MachineId;         //机器号
            public byte NewRecordFlag;    //是否是新记录
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 28)]
            public byte[] EmployeeId;    //
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Date;          //日期时间
            public byte BackId;           //备份号
            public byte RecordType;       //记录类型
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] WorkType;      //工种        (ONLY use 3bytes )
            public byte Rsv;
            public uint CurIdx;             //add  VER 22
            public uint TotalCnt;           //add  VER 22
        }

        //device login-logout struct
        [StructLayout(LayoutKind.Sequential, Size = 52, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_RET_DEV_LOGIN_STRU
        {
            public int DevIdx;
            public uint MachineId;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] Addr;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Version;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] DevType;
            public uint DevTypeFlag;
        }

        [StructLayout(LayoutKind.Sequential, Size = 52, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_RET_DEV_LOGOUT_STRU
        {
            public int DevIdx;
            public uint MachineId;
            public uint Live;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] Addr;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Version;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] DevType;
        }

        [StructLayout(LayoutKind.Sequential, Size = 11, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_MSGHEAD_INFO_STRU
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] EmployeeId;
            public byte StartYear;
            public byte StartMonth;
            public byte StartDay;

            public byte EndYear;
            public byte EndMonth;
            public byte EndDay;
        }

        [StructLayout(LayoutKind.Sequential, Size = 17, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_MSGHEADUNICODE_INFO_STRU
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] EmployeeId;
            public byte StartYear;
            public byte StartMonth;
            public byte StartDay;
            public byte StartHour;
            public byte StartMin;
            public byte StartSec;

            public byte EndYear;
            public byte EndMonth;
            public byte EndDay;
            public byte EndHour;
            public byte EndMin;
            public byte EndSec;
        }

        [StructLayout(LayoutKind.Sequential, Size = 12, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_RET_MSGGETBYIDX_UNICODE_STRU
        {
            public uint MachineId;
            public int Result;
            public int Len;
        }

        [StructLayout(LayoutKind.Sequential, Size = 12, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_RET_MSGADDNEW_UNICODE_STRU
        {
            public uint MachineId;
            public int Result;
            public int Len;
        }

        [StructLayout(LayoutKind.Sequential, Size = 12, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_RET_MSGGETALLHEAD_UNICODE_STRU
        {
            public uint MachineId;
            public int Result;
            public int Len;
        }

        [StructLayout(LayoutKind.Sequential, Size = 9, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_RET_MSGDELBYIDX_UNICODE_STRU
        {
            public uint MachineId;
            public int Result;
            public byte Idx;
        }

        [StructLayout(LayoutKind.Sequential, Size = 28, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_RET_DEV_STATUS_STRU
        {
            public uint MachineId;
            public uint EmployeeNum;
            public uint FingerPrtNum;
            public uint PasswdNum;
            public uint CardNum;
            public uint TotalRecNum;
            public uint NewRecNum;
        }

        public const int SN_LEN = 16;
        [StructLayout(LayoutKind.Sequential, Size = 24, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_RET_GET_SN_STRU
        {
            public uint MachineId;
            public int Result;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SN_LEN)]
            public byte[] sn;
        }

        // basic config info
        [StructLayout(LayoutKind.Sequential, Size = 20, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_GET_BASIC_CFG_INFO_STRU
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] software_version;
            public uint password;
            public byte delay_for_sleep;
            public byte volume;
            public byte language;
            public byte date_format;
            public byte time_format;
            public byte machine_status;
            public byte modify_language;
            public byte cmd_version;
        } //20 bytes

        [StructLayout(LayoutKind.Sequential, Size = 13, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_SET_BASIC_CFG_INFO_STRU
        {
            public uint password;
            public byte pwd_len;
            public byte delay_for_sleep;
            public byte volume;
            public byte language;
            public byte date_format;
            public byte time_format;
            public byte machine_status;
            public byte modify_language;
            public byte reserved;
        } //13 bytes

        [StructLayout(LayoutKind.Sequential, Size = 8, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_RET_GET_BASIC_CFG_STRU
        {
            public uint MachineId;
            public int Result; //0 ok, -1 err
            public CCHEX_GET_BASIC_CFG_INFO_STRU Cfg;
        }
        [StructLayout(LayoutKind.Sequential, Size = 16, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_RET_DEL_EMPLOYEE_INFO_STRU
        {
            public uint MachineId;
            public int Result; //0 ok, -1 err
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] EmployeeId;  // 5 bytes
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] padding;  // 5 bytes,12 digitals
        }

        // list,modify and delete person
        [StructLayout(LayoutKind.Sequential, Size = 294, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_RET_PERSON_INFO_STRU
        {
            public uint MachineId;
            public int CurIdx;
            public int TotalCnt;

            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] EmployeeId;  // 5 bytes,12 digitals
            public byte password_len;
            public byte max_password;  // now only 6 digitals, do not modify
            public int password;       // do not exceed max_password digitals
            public byte max_card_id;   // 6 for 3 bytes,10 for 4 bytes, do not modify
            public uint card_id;       // do not exceed max_card_id digitals
            public byte max_EmployeeName;  // may be 10 or 20 or 64, do not modify
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64)]
            public byte[] EmployeeName;// do not exceed max_EmployeeName digitals

            public byte DepartmentId;
            public byte GroupId;
            public byte Mode;
            public uint Fp_Status;    // 0~9:fp; 10:face; 11:iris1; 12:iris2
            public byte Rserved1;      // for 22
            public byte Rserved2;      // for 72 and 22
            public byte Special;

            // DR info
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 160)]
            public byte[] EmployeeName2;// do not exceed max_EmployeeName digitals
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 13)]
            public byte[] RFC;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 18)]
            public byte[] CURP;
        }

        [StructLayout(LayoutKind.Sequential, Size = 6, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_DEL_PERSON_INFO_STRU
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] EmployeeId;  // 5 bytes,12 digitals
            public byte operation;
        }

        [StructLayout(LayoutKind.Sequential, Size = 5, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_GET_ONE_EMPLOYEE_INFO_STRU
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] EmployeeId;  // 5 bytes
        }

        // delete record or new flag
        [StructLayout(LayoutKind.Sequential, Size = 5, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_DEL_RECORD_INFO_STRU
        {
            public byte del_type;
            public uint del_count;
        }

        [StructLayout(LayoutKind.Sequential, Size = 12, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_RET_DEL_RECORD_STRU
        {
            public uint MachineId;
            public int Result; //0 ok, -1 err
            public uint deleted_count;
        }

        //get time~~~~~~~~~~~~~~~~~~~~~
        [StructLayout(LayoutKind.Sequential, Size = 24, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_MSG_GETTIME_STRU
        {
            public uint Year;
            public uint Month;
            public uint Day;
            public uint Hour;
            public uint Min;
            public uint Sec;
        } 
        [StructLayout(LayoutKind.Sequential, Size = 32, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_MSG_GETTIME_STRU_EXT_INF
        {
            public uint MachineId;
            public int  Result; //0 ok, -1 err
            public CCHEX_MSG_GETTIME_STRU config;
        } 

        [StructLayout(LayoutKind.Sequential, Size = 16, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_GET_BASIC_CFG_INFO2_STRU_EXT_INF
        {
            public byte compare_level;
            public byte wiegand_range;
            public byte wiegand_type;
            public byte work_code;
            public byte real_time_send;
            public byte auto_update;
            public byte bell_lock;
            public byte lock_delay;
            public uint record_over_alarm;
            public byte re_attendance_delay;
            public byte door_sensor_alarm;
            public byte bell_delay;
            public byte correct_time;
        }
//Period of time
        [StructLayout(LayoutKind.Sequential, Size = 4, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_GET_PERIOD_TIME_ONE_STRU_EXT_INF
        {
            public byte StartHour;
            public byte StartMin;
            public byte EndHour;
            public byte EndMin;
        }

        [StructLayout(LayoutKind.Sequential, Size = 36, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_GET_PERIOD_TIME_STRU_EXT_INF
        {
            public uint MachineId;
            public int  Result; //0 ok, -1 err
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 7)]
            public CCHEX_GET_PERIOD_TIME_ONE_STRU_EXT_INF[] day_week;
        }

        //Add
        [StructLayout(LayoutKind.Sequential, Size = 6, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_ADD_FINGERPRINT_ONLINE_STRU_EXT_INF
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[]   EmployeeId;  // 5 bytes
            public byte     BackupNum;
        }
        [StructLayout(LayoutKind.Sequential, Size = 29, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_ADD_FINGERPRINT_ONLINE_STRU_EXT_INF_ID_VER_4_NEWID
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 28)]
            public byte[] EmployeeId;  // 5 bytes
            public byte BackupNum;
        }

        [StructLayout(LayoutKind.Sequential, Size = 15, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_SET_BASIC_CFG_INFO3_STRU
        {
            public byte     wiegand_type;         //韦根读取方式
            public byte     online_mode;
            public byte     collect_level;
            public byte     pwd_status;           //连接密码状态  =0 网络连接时不需要验证通讯密码 =1网络连接时需要先发送0x04命令 验证通讯密码
            public byte     sensor_status;           //=0 不主动汇报门磁状态  =1主动汇报门磁状态（设备主动发送0x2F命令的应答包)
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[]   reserved;
            public byte     independent_time;
            public byte     m5_t5_status;         //= 0	禁用 = 1	启用，本机状态为出=2	启用，本机状态为入 =4	禁用，本机状态为出 =5	禁用，本机状态为入
        }

        [StructLayout(LayoutKind.Sequential, Size = 12, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_RET_GET_RECORD_NUMBER_STRU
        {
            public uint     MachineId;
            public int      Result;
            public int      record_num;
        }

        [StructLayout(LayoutKind.Sequential, Size = 13, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_GET_RECORD_INFO_BY_TIME
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[]   EmployeeId;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[]   start_date;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[]   end_date;
        }

        [StructLayout(LayoutKind.Sequential, Size = 24+8, CharSet = CharSet.Ansi), Serializable]
        public struct CCHEX_RET_GET_EMPLOYEE_RECORD_BY_TIME_STRU
        {
            public uint     MachineId;
            public int      Result;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[]   EmployeeId;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[]   date;
            public byte     back_id;                      //备份号
            public byte     record_type;                  //记录类型
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[]   work_type; //工种        (ONLY use 3bytes )
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[]   padding;
            public int CurIdx;
            public int TotalCnt;
        }


        //function
        [DllImport("tc-b_new_sdk.dll")]
        public static extern uint CChex_Version();

        [DllImport("tc-b_new_sdk.dll")]
        public static extern void CChex_Init();

        /*****************************************************************************
            return AvzHandle
        *****************************************************************************/
        [DllImport("tc-b_new_sdk.dll")]
        public static extern IntPtr CChex_Start();
        /*****************************************************************************
        return AvzHandle
        IsCloseServer:   0 : not close  ,ServerPort 0:random   other:other
        *****************************************************************************/

        [DllImport("tc-b_new_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CChex_Set_Service_Port(ushort Port);

        [DllImport("tc-b_new_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CChex_Get_Service_Port(IntPtr CchexHandle);


        [DllImport("tc-b_new_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void CChex_Stop(IntPtr CchexHandle);

        //网络
        [DllImport("tc-b_new_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CChex_GetNetConfig(IntPtr CchexHandle, int DevIdx);
        [DllImport("tc-b_new_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CChex_SetNetConfig(IntPtr CchexHandle, int DevIdx, ref CCHEX_NETCFG_INFO_STRU Config);

        /*****************************************************************************
            return 
                >0  ok, return length
                =0, no result.
                <0  return (0 - need len)
        *****************************************************************************/
        [DllImport("tc-b_new_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CChex_Update(IntPtr CchexHandle, int[] DevIdx, int[] Type, IntPtr Buff, int Len);

        /*****************************************************************************

        *****************************************************************************/
        [DllImport("tc-b_new_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CChex_SetTime(IntPtr CchexHandle, int DevIdx, int Year, int Month, int Day, int Hour, int Min, int Sec);

        // download all record
        [DllImport("tc-b_new_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CChex_DownloadAllRecords(IntPtr CchexHandle, int DevIdx);

        // download new record
        [DllImport("tc-b_new_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CChex_DownloadAllNewRecords(IntPtr CchexHandle, int DevIdx);

        // basic config info
        [DllImport("tc-b_new_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CChex_GetBasicConfigInfo(IntPtr CchexHandle, int DevIdx);
        [DllImport("tc-b_new_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CChex_SetBasicConfigInfo(IntPtr CchexHandle, int DevIdx, ref CCHEX_SET_BASIC_CFG_INFO_STRU Config);
        // list, modify and delete person 
        [DllImport("tc-b_new_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CChex_ListPersonInfo(IntPtr CchexHandle, int DevIdx);
        [DllImport("tc-b_new_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CChex_ModifyPersonInfo(IntPtr CchexHandle, int DevIdx, ref CCHEX_RET_PERSON_INFO_STRU personlist, byte person_num);
        [DllImport("tc-b_new_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CChex_DeletePersonInfo(IntPtr CchexHandle, int DevIdx, ref CCHEX_DEL_PERSON_INFO_STRU Config);


        // get, put fp raw data
        [DllImport("tc-b_new_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CChex_DownloadFingerPrint(IntPtr CchexHandle, int DevIdx, byte[] EmployeeId, byte FingerIdx);
        [DllImport("tc-b_new_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CChex_UploadFingerPrint(IntPtr CchexHandle, int DevIdx, byte[] EmployeeId, byte FingerIdx, byte[] FingerData, int DataLen);
        //
        [DllImport("tc-b_new_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CChex_DownloadFingerPrint_VER_4_NEWID(IntPtr CchexHandle, int DevIdx, byte[] EmployeeId, byte FingerIdx);
        [DllImport("tc-b_new_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CChex_UploadFingerPrint_VER_4_NEWID(IntPtr CchexHandle, int DevIdx, byte[] EmployeeId, byte FingerIdx, byte[] FingerData, int DataLen);

        // delete record or new flag
        [DllImport("tc-b_new_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CChex_DeleteRecordInfo(IntPtr CchexHandle, int DevIdx, ref CCHEX_DEL_RECORD_INFO_STRU Config);

        //add~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~2.0
        
        [DllImport("tc-b_new_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CChex_GetTime(IntPtr CchexHandle, int DevIdx);

        [DllImport("tc-b_new_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CChex_InitSystem(IntPtr CchexHandle, int DevIdx);

        [DllImport("tc-b_new_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CChex_GetPeriodTime(IntPtr CchexHandle, int DevIdx, byte SerialNumbe);

        [DllImport("tc-b_new_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CCHex_ClientConnect(IntPtr CchexHandle, byte[] Ip, int Port);

        [DllImport("tc-b_new_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CCHex_ClientDisconnect(IntPtr CchexHandle, int DevIdx);

        [DllImport("tc-b_new_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CChex_GetRecordInfoStatus(IntPtr CchexHandle, int DevIdx);
    }
}