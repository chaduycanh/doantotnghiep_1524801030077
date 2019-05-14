using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityLibrary;
using OfficeOpenXml;
using Shared.Utility;

namespace DoAnTotNghiep_1524801030077.Controllers
{
    public class StatisticaController : AdminController
    {
        // GET: Statistica
        StatisticaServices _statisticSrv = new StatisticaServices();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Chart()
        {
            return View();
        }
        // GET: ExtratimeActiveTemplate
        public ActionResult GetAll()
        {
            return Json(new { jsData = _statisticSrv.GetAllData() });
        }
        public ActionResult Check(string id, string status)
        {
            try
            {
                var data = _statisticSrv.Check(Convert.ToInt64(id), Convert.ToInt32(status));
                return Json(new { returnData = data, jsData = _statisticSrv.GetAllData() });
            }
            catch (Exception)
            {

                return Json(new { returnData = false });
            }

        }
        class ImagesProff
        {
            public string caption { get; set; }
            public int size { get; set; }
            public string url { get; set; }
            public int key { get; set; }
        }
        public ActionResult GetFile(string Id)
        {
            try
            {
                var data = _statisticSrv.GetDataById(Convert.ToInt64(Id));
                var listProof = data.Proof.Remove(data.Proof.Length - 1).Split(';');
                List<ImagesProff> listImages = new List<ImagesProff>();
                int index = 0;
                foreach (var item in listProof)
                {
                    index = index + 1;
                    ImagesProff temp = new ImagesProff();
                    temp.caption = "Images-" + index;
                    temp.url = "#";
                    temp.key = index;
                    listImages.Add(temp);
                }
                //{ caption: "City-1.jpg", size: 327892, url: "/site/file-delete", key: 1 },
                //        { caption: "City-2.jpg", size: 438828, url: "/site/file-delete", key: 2 },
                return Json(new { returnData = listProof, listImagesDetail = listImages });
            }
            catch (Exception)
            {

                return Json(new { returnData = false });
            }

        }
        public ActionResult PrintThongKe(int year)
        {
            string mgv = Session["UserCode"].ToString();
            string role = Session["UserRole"].ToString();
            string name = Session["UserName"].ToString();
            string fileName = name + "_gio_cong_tac_khac.xlsx";
            DataTable data = _statisticSrv.GetDataTableThongkecanhan(mgv, year);
            if (data == null)
            {
                var streamNull = new System.IO.MemoryStream();
                ExcelPackage pck = new ExcelPackage();
                ExcelWorksheet p = pck.Workbook.Worksheets.Add("Gio Cong Tac Khac");
                pck.SaveAs(streamNull);
                return File(streamNull.ToArray(), "application/octet-stream", fileName);

            }
            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorksheet workSheet = pck.Workbook.Worksheets.Add("Gio Cong Tac Khac");
                FunctionUtility.DataTableToExcelThongKe(workSheet, data, 2018, name, role);
                var ms = new System.IO.MemoryStream();
                pck.SaveAs(ms);
                return File(ms.ToArray(), "application/vnd.ms-excel", fileName);
            }
        }
        public ActionResult PrintThongKeAdmin(int year, string code)
        {
            var modal = _statisticSrv.GetDataUser(code);
            string mgv = modal.Code;
            string role = _statisticSrv.GetRoleName(modal.Role);
            string name = modal.FullName;
            string fileName = name + "_gio_cong_tac_khac.xlsx";
            DataTable data = _statisticSrv.GetDataTableThongkecanhan(mgv, year);
            if (data == null)
            {
                var streamNull = new System.IO.MemoryStream();
                ExcelPackage pck = new ExcelPackage();
                ExcelWorksheet p = pck.Workbook.Worksheets.Add("Gio Cong Tac Khac");
                pck.SaveAs(streamNull);
                return File(streamNull.ToArray(), "application/octet-stream", fileName);

            }
            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorksheet workSheet = pck.Workbook.Worksheets.Add("Gio Cong Tac Khac");
                FunctionUtility.DataTableToExcelThongKe(workSheet, data, 2018, name, role);
                var ms = new System.IO.MemoryStream();
                pck.SaveAs(ms);
                return File(ms.ToArray(), "application/vnd.ms-excel", fileName);
            }
        }
        public ActionResult DataChartDay(string day)
        {
            try
            {
                var t = day.Split('-');
                var a = t[1] + "/" + t[0] + "/" + t[2];
                DateTime dti = Convert.ToDateTime(a);
                var data = _statisticSrv.GetDataChartDay();
                var model = data.Where(p => p.DateActive == dti).ToList();
                int vang = 0;
                int co = 0;
                dynamic[] w = new dynamic[3];
                Array[] q = new Array[2];
                w[0] = "Ngày";
                w[1] = "Có";
                w[2] = "Vắng";
                q[0] = w;             
                DateTime date = new DateTime();
                foreach (var item in model)
                {
                    if (item.Status == 0)
                    {
                        vang = vang + 1;
                    }
                    else if(item.Status==1)
                    {
                        co = co + 1;
                    }
                    date = item.DateActive;
                }
                dynamic[] m = new dynamic[3];
                m[0] = date.ToShortDateString();
                m[1] =co;
                m[2] = vang;
                q[1] = m;
                return Json(new { jsData = q });
            }
            catch (Exception)
            {

                throw;
            }

        }
        public ActionResult DataChartWeek(string fromDate,string toDate)
        {
            try
            {
                //var t = day.Split('-');
                //var a = t[1] + "/" + t[0] + "/" + t[2];
                DateTime from = Convert.ToDateTime(fromDate);
                DateTime to = Convert.ToDateTime(toDate);
                var data = _statisticSrv.GetDataChartDay();
                var model = data.Where(p => p.DateActive >= from && p.DateActive< to).ToList();
          
                dynamic[] w = new dynamic[3];
                Array[] q = new Array[8];
                w[0] = "Tháng";
                w[1] = "Có";
                w[2] = "Vắng";
                q[0] = w;
                DateTime date = new DateTime();
                int index = 1;
                for(DateTime t = from;t<to; t = t.AddDays(1.0))
                {
                    int vang = 0;
                    int co = 0;
                    foreach (var item in model)
                    {
                        if(item.DateActive==t)
                        {
                            if (item.Status == 0)
                            {
                                vang = vang + 1;
                            }
                            else if (item.Status == 1)
                            {
                                co = co + 1;
                            }
                            date = item.DateActive;
                        }

                    }
                    dynamic[] m = new dynamic[3];
                    m[0] = t.ToShortDateString();
                    m[1] = co;
                    m[2] = vang;
                    q[index] = m;
                    index = index + 1;
                }
               
                return Json(new { jsData = q });
            }
            catch (Exception)
            {

                throw;
            }

        }
        public ActionResult DataChartMonth(string datet)
        {
            try
            {
                //var t = day.Split('-');
                //var a = t[1] + "/" + t[0] + "/" + t[2];
                DateTime from = Convert.ToDateTime(datet);
                DateTime to = Convert.ToDateTime(datet).AddMonths(1);
                var data = _statisticSrv.GetDataChartDay();
                var model = data.Where(p => p.DateActive >= from && p.DateActive < to).ToList();

                dynamic[] w = new dynamic[3];
                Array[] q = new Array[32];
                w[0] = "Tháng";
                w[1] = "Có";
                w[2] = "Vắng";
                q[0] = w;
                DateTime date = new DateTime();
                int index = 1;
                for (DateTime t = from; t < to; t = t.AddDays(1.0))
                {
                    int vang = 0;
                    int co = 0;
                    foreach (var item in model)
                    {
                        if (item.DateActive == t)
                        {
                            if (item.Status == 0)
                            {
                                vang = vang + 1;
                            }
                            else if (item.Status == 1)
                            {
                                co = co + 1;
                            }
                            date = item.DateActive;
                        }

                    }
                    dynamic[] m = new dynamic[3];
                    m[0] = t.ToShortDateString();
                    m[1] = co;
                    m[2] = vang;
                    q[index] = m;
                    index = index + 1;
                }

                return Json(new { jsData = q });
            }
            catch (Exception)
            {

                throw;
            }

        }
        public ActionResult DataChartYear(string fromDate, string toDate)
        {
            try
            {
                //var t = day.Split('-');
                //var a = t[1] + "/" + t[0] + "/" + t[2];
                DateTime from = Convert.ToDateTime(fromDate);
                DateTime to = Convert.ToDateTime(toDate);
                var data = _statisticSrv.GetDataChartDay();
                var model = data.Where(p => p.DateActive >= from && p.DateActive < to).ToList();

                dynamic[] w = new dynamic[3];
                Array[] q = new Array[13];
                w[0] = "Tháng";
                w[1] = "Có";
                w[2] = "Vắng";
                q[0] = w;
                DateTime date = new DateTime();
                int index = 1;
                for (int t = 1; t <=12; t++)
                {
                    int vang = 0;
                    int co = 0;
                    foreach (var item in model)
                    {
                        if (item.DateActive.Month == t)
                        {
                            if (item.Status == 0)
                            {
                                vang = vang + 1;
                            }
                            else if (item.Status == 1)
                            {
                                co = co + 1;
                            }
                            date = item.DateActive;
                        }

                    }
                    dynamic[] m = new dynamic[3];
                    m[0] = t;
                    m[1] = co;
                    m[2] = vang;
                    q[index] = m;
                    index = index + 1;
                }

                return Json(new { jsData = q});
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}