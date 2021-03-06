using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Security.Cryptography;
using System.Data.OleDb;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Globalization;
using System.Collections;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace Shared.Utility
{
    public class FunctionUtility
    {

        public static string GetMd5Hash(string input)
        {

            MD5 mh = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = mh.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static string EncodePass(string input, string salt)
        {
            var temp = FunctionUtility.GetMd5Hash(input);
            var newpass = FunctionUtility.GetMd5Hash(input + salt);
            return newpass;
        }
        public static void DataTableToExcelAtt(ExcelWorksheet workSheet, DataTable data,string date, string content)
        {
            string a = DefineExcell(data.Columns.Count);
            string range = (data.Rows.Count+5).ToString();
            string colname = DefineExcell(data.Columns.Count);
            workSheet.Cells["A1:C1"].Merge = true;
            workSheet.Cells["A1:C1"].Value = "Bảng điểm danh " + date;
            workSheet.Cells["A2:C2"].Merge = true;
            workSheet.Cells["A2:C2"].Value = "Nội dung: "+content;


            workSheet.Cells["A1:C1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            workSheet.Cells["A1:C1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells["A1:C1"].Style.Font.Size = 16;

            workSheet.Cells["A5:C5"].Style.Font.Bold = true;
            workSheet.Cells["A5:C5"].Style.Font.Size = 13;
            workSheet.Cells["A5:C"+ range].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            workSheet.Cells["A5:C" + range].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            workSheet.Cells["A5:C" + range].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            workSheet.Cells["A5:C" + range].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            //int Index = LoadHeaderExcel(workSheet, logoPath, companyName, companyAddress, reportName, caption);
            workSheet.Cells["A" + 5].LoadFromDataTable(data, true);
            //FormatHeaderExcel(workSheet, "A", Index, colname, Index);
            workSheet.Cells.AutoFitColumns();
            //FormatBodyExcel(workSheet, "A", Index, colname, tbReport.Rows.Count + Index);
        }

        public static void DataTableToExcelThongKe(ExcelWorksheet workSheet, DataTable data,int year,string name,string role)
          {
            //string a = DefineExcell(data.Columns.Count);
            //string range = (data.Rows.Count + 5).ToString();
            //string colname = DefineExcell(data.Columns.Count);
            #region A2:B2
            workSheet.Cells["A2:B2"].Merge = true;
            workSheet.Cells["A2:B2"].Value = "TRƯỜNG ĐẠI HỌC THỦ DẦU MỘT";
            workSheet.Cells["A2:B2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells["A2:B2"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            workSheet.Cells["A2:B2"].Style.Font.Bold = true;
            workSheet.Cells["A2:B2"].Style.Font.Size = 12;
            workSheet.Cells["A2:B2"].Style.Border.Top.Style = ExcelBorderStyle.None;
            workSheet.Cells["A2:B2"].Style.Border.Left.Style = ExcelBorderStyle.None;
            workSheet.Cells["A2:B2"].Style.Border.Right.Style = ExcelBorderStyle.None;
            workSheet.Cells["A2:B2"].Style.Border.Bottom.Style = ExcelBorderStyle.None;
            #endregion
            #region F2:L2
            workSheet.Cells["F2:L2"].Merge = true;
            workSheet.Cells["F2:L2"].Value = "CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM";
            workSheet.Cells["F2:L2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells["F2:L2"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            workSheet.Cells["F2:L2"].Style.Font.Bold = true;
            workSheet.Cells["F2:L2"].Style.Font.Size = 12;
          
            workSheet.Cells["F2:L2"].Style.Border.Top.Style = ExcelBorderStyle.None;
            workSheet.Cells["F2:L2"].Style.Border.Left.Style = ExcelBorderStyle.None;
            workSheet.Cells["F2:L2"].Style.Border.Right.Style = ExcelBorderStyle.None;
            workSheet.Cells["F2:L2"].Style.Border.Bottom.Style = ExcelBorderStyle.None;
            #endregion
            #region A3:B3
            workSheet.Cells["A3:B3"].Merge = true;
            workSheet.Cells["A3:B3"].Value = "ĐƠN VỊ: KHOA KỸ THUẬT CÔNG NGHỆ";
            workSheet.Cells["A3:B3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells["A3:B3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            workSheet.Cells["A3:B3"].Style.Font.Bold = true;
            workSheet.Cells["A3:B3"].Style.Font.Size = 12;
            workSheet.Cells["A3:B3"].Style.Border.Top.Style = ExcelBorderStyle.None;
            workSheet.Cells["A3:B3"].Style.Border.Left.Style = ExcelBorderStyle.None;
            workSheet.Cells["A3:B3"].Style.Border.Right.Style = ExcelBorderStyle.None;
            workSheet.Cells["A3:B3"].Style.Border.Bottom.Style = ExcelBorderStyle.None;
            #endregion
            #region F3:L3
            workSheet.Cells["F3:L3"].Merge = true;
            workSheet.Cells["F3:L3"].Value = "Độc lập - Tự do - Hạnh phúc";
            workSheet.Cells["F3:L3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells["F3:L3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            workSheet.Cells["F3:L3"].Style.Font.Bold = true;
            workSheet.Cells["F3:L3"].Style.Font.Size = 12;
            workSheet.Cells["F3:L3"].Style.Border.Top.Style = ExcelBorderStyle.None;
            workSheet.Cells["F3:L3"].Style.Border.Left.Style = ExcelBorderStyle.None;
            workSheet.Cells["F3:L3"].Style.Border.Right.Style = ExcelBorderStyle.None;
            workSheet.Cells["F3:L3"].Style.Border.Bottom.Style = ExcelBorderStyle.None;
            #endregion
            #region A6:L6
            workSheet.Cells["A6:L6"].Merge = true;
            workSheet.Cells["A6:L6"].Value = "BẢNG TỰ KHAI SỐ GIỜ CÔNG TÁC KHÁC THỰC HIỆN TRONG NĂM HỌC "+year.ToString()+"-"+(year+1).ToString();
            workSheet.Cells["A6:L6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells["A6:L6"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            workSheet.Cells["A6:L6"].Style.Font.Bold = true;
            workSheet.Cells["A6:L6"].Style.Font.Size = 12;
            workSheet.Cells["A6:L6"].Style.Border.Top.Style = ExcelBorderStyle.None;
            workSheet.Cells["A6:L6"].Style.Border.Left.Style = ExcelBorderStyle.None;
            workSheet.Cells["A6:L6"].Style.Border.Right.Style = ExcelBorderStyle.None;
            workSheet.Cells["A6:L6"].Style.Border.Bottom.Style = ExcelBorderStyle.None;
            #endregion
            #region value
            workSheet.Cells["B7"].Value = "Họ và tên giảng viên: ";
            workSheet.Cells["B8"].Value = "Chức vụ: ";
            workSheet.Cells["B9"].Value = "Chức danh: ";
            workSheet.Cells["C7:E7"].Merge = true;
            workSheet.Cells["C8:E8"].Merge = true;
            workSheet.Cells["C9:E9"].Merge = true;
            workSheet.Cells["C7"].Value = name;
            workSheet.Cells["C8"].Value = role;
            workSheet.Cells["C9"].Value = role;
            #endregion
            #region style A7:L10
            workSheet.Cells["A7:L10"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells["A7:L10"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            workSheet.Cells["A7:L10"].Style.Font.Bold = true;
            workSheet.Cells["A7:L10"].Style.Font.Size = 12;
            #endregion
            #region A1:L10 BORDER
            workSheet.Cells["A1:L10"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            workSheet.Cells["A1:L10"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            workSheet.Cells["A1:L10"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            workSheet.Cells["A1:L10"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            workSheet.Cells["A1:L10"].Style.Border.Top.Color.SetColor(Color.White);
            workSheet.Cells["A1:L10"].Style.Border.Left.Color.SetColor(Color.White);
            workSheet.Cells["A1:L10"].Style.Border.Right.Color.SetColor(Color.White);
            workSheet.Cells["A1:L10"].Style.Border.Bottom.Color.SetColor(Color.White);
            workSheet.Cells["A1:L10"].Style.Font.Name = "Times New Roman";
            #endregion
            string range = (data.Rows.Count + 13).ToString();
            workSheet.Cells["A12"].Value = "TT";
            workSheet.Cells["A12:F12"].Style.Font.Bold = true;
            workSheet.Cells["A12:F" + range].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            workSheet.Cells["A12:F" + range].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            workSheet.Cells["A12:F" + range].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            workSheet.Cells["A12:F" + range].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            workSheet.Cells["F13:F" + range].Style.Numberformat.Format = "dd-mm-yyyy";
            //formatRange.NumberFormat = "mm/dd/yyyy hh:mm:ss";
            //workSheet.Cells["A12:F"+ data.Rows.Count+12].Style.Font.Bold = true;
            int tongcong = 0;
            int sl = 0;
            decimal tong = 0;
            for (int i =0;i<data.Rows.Count;i++)
            {
                tongcong= tongcong+Convert.ToInt32(data.Rows[i][2]);
                sl = sl + Convert.ToInt32(data.Rows[i][3]);
                tong = tong + Convert.ToDecimal(data.Rows[i][3]);
                //workSheet.Cells["B"+(i+12).ToString()+":G"+(i + 12).ToString()].Merge = true;
                workSheet.Cells["A"+(i+13).ToString()].Value = (i+1).ToString();

            }
            workSheet.Cells["B" + 12].LoadFromDataTable(data, true);
            string range2 = (data.Rows.Count + 13).ToString();
            workSheet.Cells["B"+ range2].Value = "Tổng cộng";
            workSheet.Cells["B"+ range2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            workSheet.Cells["B" +range2].Style.Font.Bold = true;
            workSheet.Cells["C" +range2].Value = tongcong;
            workSheet.Cells["D" +range2].Value = sl;
            workSheet.Cells["E" + range2].Value = tong;

            string range3 = (data.Rows.Count + 15).ToString();
            workSheet.Cells["B" + range3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            workSheet.Cells["B" + range3].Style.Font.Bold = true;
            workSheet.Cells["B" + range3].Value = "Trưởng khoa";
            workSheet.Cells["C" + range3].Value = "Giám đốc chương trình";
            workSheet.Cells["E" + range3].Value = "Người khai";
            workSheet.Cells["B" + range3].Style.Font.Bold = true;
            workSheet.Cells["C" + range3].Style.Font.Bold = true;
            workSheet.Cells["E" + range3].Style.Font.Bold = true;
            workSheet.Cells.AutoFitColumns();
            workSheet.Column(2).Width = 40;
        }
        public static string DefineExcell(int n)
        {
            string alphabe = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] tempArr = alphabe.ToCharArray();
            string temp2 = string.Empty;
            string temp = string.Empty;
            int x = 0, y = 0, z = 0;
            if (n > 702)
            {
                x = (n - 1) / 702;
                y = (n - x * 702 - 1) / 26;
                z = n - x * 702 - y * 26 - 1;
                temp2 = tempArr[x - 1].ToString() + tempArr[y].ToString() + tempArr[z].ToString();
            }
            else if (n > 26)
            {
                x = (n - 1) / 26;
                y = n - (x * 26) - 1;
                // FormatCellXlsx[] a = new FormatCellXlsx[5];
                temp2 = tempArr[x - 1].ToString() + tempArr[y];
            }
            else
            {
                temp2 = DBConvert.ParseString(tempArr[n - 1]);
            }
            return temp2;
        }
        public static string FotmatDateTime(DateTime date)
        {
            
            var temp = date.ToString().Split('/');
            var day = temp[1].Length < 2 ? "0" + temp[1] : temp[1];
            var month = temp[0].Length < 2 ? "0" + temp[0] : temp[0];
            var year = temp[2].Substring(0, 4);
            var newDate = day + "/"+ month + "/"+ year;
            return newDate;
        }
    }

}
