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
    public class AttendancetController : AdminController
    {
        // GET: Attendancet
        AttendanceServices _attdSrv = new AttendanceServices();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAll()
        {
            return Json(new { jsData = _attdSrv.GetAllData() });
        }
        public ActionResult Search(string id)
        {
            if (id == "")
            {
                id = "0";
            }
            return Json(new { jsData = _attdSrv.SearchActiveToAttById(Convert.ToInt64(id)) });
        }
        public ActionResult GetActive(string date)
        {
            var detailDate = date.Split('-');
            var newDate = detailDate[1] + "/" + detailDate[0] + "/" + detailDate[2];
            DateTime d = Convert.ToDateTime(newDate);
            return Json(new { jsData = _attdSrv.SearchActiveToAttByDate(d) });
        }
        public ActionResult DiemDanh(string id, string status)
        {
            var model = new Attendance();
            model.Pid = Convert.ToInt64(id);
            model.Status = Convert.ToInt32(status);
            var data = _attdSrv.DiemDanh(model);
            return Json(new { returnData = data, jsData = _attdSrv.GetAllData() });
        }
        public ActionResult Create()
        {
            return PartialView();
        }
        public ActionResult Save(List<HttpPostedFileBase> upload,
                                string KindActive, string Date,
                                string Hour, string Content,
                                string Location)
        {
            try
            {
                var msgv = Session["UserInfo"].ToString();
                string root = "Images/" + msgv;
                string filePath = HttpContext.Server.MapPath(root);
                // If directory does not exist, create it. 
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);

                }
                string url = string.Empty;
                if (upload != null)
                {
                    foreach (var item in upload)
                    {

                        var fileName = Path.GetFileName(item.FileName);
                        var ext = Path.GetExtension(item.FileName);

                        //string name = Path.GetFileNameWithoutExtension(fileName); 
                        string myfile = DateTime.Now.Day.ToString() +
                                        DateTime.Now.Month.ToString() +
                                        DateTime.Now.Year.ToString() +
                                        DateTime.Now.Hour.ToString() +
                                        DateTime.Now.Minute.ToString() +
                                        DateTime.Now.Millisecond +
                                        "_" + msgv + ext;


                        var path = Path.Combine(Server.MapPath("~/Images/" + msgv), myfile);
                        item.SaveAs(path);
                        url = url + root + "/" + myfile + ";";
                        //item.SaveAs(filePath);
                    }
                }




                PersonalExtratimeActive data = new PersonalExtratimeActive();
                data.Date = Convert.ToDateTime(Date);
                data.Time = Hour;
                data.Content = Content;
                data.Location = Location;
                data.TeacherCode = msgv;
                data.KindActive = Convert.ToInt64(KindActive);
                data.Proof = url;
                data.Status = 0;
                //string a = data["Date"].ToString();
                _attdSrv.Save(data);

                return Json(new { returnData = true, jsData = _attdSrv.GetAllData() });
            }
            catch (Exception ex)
            {

                return Json(new { returnData = false });
            }

        }
        public ActionResult Edit(string Id)
        {
            // var data = _attdSrv.GetDataById(Convert.ToInt64(Id));
            return Json(new { returnData = true });
        }
        public ActionResult Delete(string Id)
        {
            try
            {
                var data = _attdSrv.Delete(Convert.ToInt64(Id));
                return Json(new { returnData = data, jsData = _attdSrv.GetAllData() });
            }
            catch (Exception)
            {

                return Json(new { returnData = false });
            }

        }
        public ActionResult Update(string Id, string KindActive, string Date,
                                string Hour, string Content,
                                string Location)
        {
            try
            {
                var msgv = Session["UserInfo"].ToString();
                PersonalExtratimeActive data = new PersonalExtratimeActive();
                data.Pid = Convert.ToInt64(Id);
                data.Date = Convert.ToDateTime(Date);
                data.Time = Hour;
                data.Content = Content;
                data.Location = Location;
                data.TeacherCode = msgv;
                data.KindActive = Convert.ToInt64(KindActive);
                //string a = data["Date"].ToString();
                // _pextaSrv.Update(data);

                return Json(new { returnData = true, jsData = _attdSrv.GetAllData() });
            }
            catch (Exception)
            {

                return Json(new { returnData = false });
            }

        }
        public ActionResult PrintAtt(string code,string date) 
        {
            string fileName = "Bang_diem_danh_"+date+".xlsx";
            string content = _attdSrv.GetDataActice(Convert.ToInt64(code)).Content;
            DataTable data = _attdSrv.GetDataTableToPrint(Convert.ToInt64(code)); 
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
                string caption = "";
                ExcelWorksheet workSheet = pck.Workbook.Worksheets.Add("Gio Cong Tac Khac");
                FunctionUtility.DataTableToExcelAtt(workSheet, data,date, content);
                var ms = new System.IO.MemoryStream();
                pck.SaveAs(ms);
                return File(ms.ToArray(), "application/vnd.ms-excel", fileName);
            }
        }
    }
}