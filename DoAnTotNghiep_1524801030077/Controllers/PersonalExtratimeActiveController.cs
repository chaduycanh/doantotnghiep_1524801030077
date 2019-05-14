using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.Mvc;
using EntityLibrary;

namespace DoAnTotNghiep_1524801030077.Controllers
{
    public class PersonalExtratimeActiveController : AdminController
    {
        PersonalExtratimeActiveServices _pextaSrv = new PersonalExtratimeActiveServices();
        // GET: ExtratimeActiveTemplate
        public ActionResult Index()
        {
            ViewBag.ListData = _pextaSrv.GetAllData();
            return View();
        }
        public ActionResult GetAll()
        {
            return Json(new { jsData = _pextaSrv.GetAllData() });
        }
        public ActionResult Create()
        {
            return PartialView();
        }
        public ActionResult Save(List<HttpPostedFileBase> upload,
                                long KindActive, string Date,
                                string Hour, string Content,
                                string Location)
        {
            try
            {
                var msgv = Session["UserInfo"].ToString();
                string root = "Images/" + msgv;
                var filePath = Path.Combine(Server.MapPath("~/Images/" + msgv));
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
                _pextaSrv.Save(data);

                return Json(new { returnData = true, jsData = _pextaSrv.GetAllData() });
            }
            catch (Exception ex)
            {

                return Json(new { returnData = false });
            }

        }
        public ActionResult Edit(string Id)
        {
            var data = _pextaSrv.GetDataById(Convert.ToInt64(Id));
            return Json(new { returnData = data });
        }
        public ActionResult Delete(string Id)
        {
            try
            {
                var data = _pextaSrv.Delete(Convert.ToInt64(Id));
                return Json(new { returnData = data, jsData = _pextaSrv.GetAllData() });
            }
            catch (Exception)
            {

                return Json(new { returnData = false });
            }

        }
        public ActionResult Update(string Id,string KindActive, string Date,
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

                    return Json(new { returnData = _pextaSrv.Update(data), jsData = _pextaSrv.GetAllData() });
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
                var data = _pextaSrv.GetDataById(Convert.ToInt64(Id));
                var listProof = data.Proof.Remove(data.Proof.Length-1).Split(';');
                List<ImagesProff> listImages = new List<ImagesProff>();
                int index = 0;
                foreach(var item in listProof)
                {
                    index = index + 1;
                    ImagesProff temp = new ImagesProff();
                    temp.caption = "Images-" + index;
                    temp.url = "/PersonalExtratimeActive/DeleteFile?path=" + item+"&id="+ Id;
                    temp.key = index;
                    listImages.Add(temp);
                }
                //{ caption: "City-1.jpg", size: 327892, url: "/site/file-delete", key: 1 },
                //        { caption: "City-2.jpg", size: 438828, url: "/site/file-delete", key: 2 },
                return Json(new { returnData = listProof , listImagesDetail = listImages });
            }
            catch (Exception)
            {

                return Json(new { returnData = false });
            }

        }
        public ActionResult SaveFile(string id)
        {
            try
            {

                var msgv = Session["UserInfo"].ToString();
                string root = "/Images/" + msgv;
                foreach (string item in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[item] as HttpPostedFileBase;
                    //string fileName = file.FileName;
                    //string UploadPath = "~/Images/";
                    var fileName = Path.GetFileName(file.FileName);
                    var ext = Path.GetExtension(file.FileName);

                    string myfile = DateTime.Now.Day.ToString() +
                                    DateTime.Now.Month.ToString() +
                                    DateTime.Now.Year.ToString() +
                                    DateTime.Now.Hour.ToString() +
                                    DateTime.Now.Minute.ToString() +
                                    DateTime.Now.Millisecond +
                                    "_" + msgv + ext;


                    var path = Path.Combine(Server.MapPath("~/Images/" + msgv), myfile);
                    file.SaveAs(path);
                    string url = root + "/" + myfile + ";";
                    var model = new PersonalExtratimeActive();
                    model.Pid = Convert.ToInt64(id);
                    model.Proof = url;
                    var data = _pextaSrv.UpdateFile(model);

                }

                return Json(new { returnData = true });
            }
            catch (Exception)
            {

                return Json(new { returnData = false });
            }

        }
    public ActionResult DeleteFile(string path,string id)
            {
            try
            {
                //Session["UserInfo"] = "1111";
                //var msgv = Session["UserInfo"].ToString();
                //string root = "/Images/" + msgv;
                var fullPath = Server.MapPath("~" + id);

                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                    var model = new PersonalExtratimeActive();
                    model.Pid = Convert.ToInt64(id);
                    model.Proof = path;
                    var data = _pextaSrv.DeleteFile(model);


                    return Json(new { returnData = data });
                }
                else
                {
                    return Json(new { returnData = false });
                }

            }
            catch (Exception)
            {

                return Json(new { returnData = false });
            }

            }
        }
    }