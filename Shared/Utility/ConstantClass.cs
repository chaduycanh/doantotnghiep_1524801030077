using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Utility
{
    public class ConstantClass
    {
        public const int timeout = 300; // 5 phút
        public const int MinYear = 1975;
        public const int MaxYear = 2100;
        public const string Payslip = "Payslip";
        public const string daypaid = "1";
        public const string FORMAT_DATETIME = "dd/MM/yyyy";
        public const string FORMAT_FULL_DATETIME = "dd/MM/yyyy HH:mm:ss";
        public const string FORMAT_DATETIME_YYYY_MM_DD = "yyyy-MM-dd";
        public const string FORMAT_DATETIME_MM_YYYY = "MM/yyyy";
        public const string CODE_FORMAT_DATETIME_IN_SQL = "103";
        public const int UserAddmin = 1;
        public const string defaultLang = "vi-VN";
        public const string ROUND_UNIT = "|sheet|pia|pc|pcs|pair|set|roll|";
        public const long GroupAllView = 1088;
        public const string TEAMLEADER = "|2|4|6|10|11|";
        public const string MANAGER = "|13|";
        public const string GroupNameENManager = "MANAGER";
        public const string HubGroupUserLogin = "USERLOGIN";
        public const string InsertEntity = "Insert From Entities";
        public const string UpdatetEntity = "Update From Entities";
        public const string DeletetEntity = "Delete From Entities";

        // table name màn hình kỷ luật
        public const string tableNameHRDEmpDiscipline = "HRDEmpDiscipline";

        // table name màn hình phép
        public const string tableNameHRDEmpLeaveRequest = "HRDEmpLeaveRequest";

        // table name màn hình duyệt chấm công
        public const string tableNameHRDEmployeeWorking = "HRDEmployeeWorking";

        // Duong dan luu file template Word
        public const string templateWORD = "\\WordTemplate\\";

        // Duong dan luu file template Word
        public const string templateExcel = "\\ExcelTemplate\\";

        // Duong dan tam download file WORD sau khi export
        public const string tempDownloadOfficeFile = "\\WordTemplate\\Temp\\";

        // Duong dan tam download file Excel sau khi export
        public const string tempDownloadExcelFile = "\\ExcelTemplate\\Temp\\";

        // Duong dan tam download file CSV sau khi export
        public const string tempDownloadCSVFileEmployee = "\\CSVTemplate\\Employee\\";

        //duong dan image
        public const string imagePathCommon = "\\Image\\Common\\";
        //code master
        public const long GroupMutilanguage = 1087;

        // mã máy chấm công chính
        public const string timerMachineGate = "SHS_GATE";

        // Kích thước màn hình PC
        public const int ScreenWidthPC = 1280;
    }
}
