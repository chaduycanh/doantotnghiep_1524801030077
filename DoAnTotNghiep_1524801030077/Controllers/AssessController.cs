using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityLibrary;
namespace DoAnTotNghiep_1524801030077.Controllers
{
    public class AssessController : AdminController
    {
        AssessServices _assessSrv = new AssessServices();
        public ActionResult Index()
        {
            return View();
        }

        // GET: ExtratimeActiveTemplate
        public ActionResult GetAll()
        {
            return Json(new { jsData = _assessSrv.GetAllData() });
        }
        public ActionResult Check(string id, string status)
        {
            try
            {
                var data = _assessSrv.Check(Convert.ToInt64(id), Convert.ToInt32(status));
                return Json(new { returnData = data, jsData = _assessSrv.GetAllData() });
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
                var data = _assessSrv.GetDataById(Convert.ToInt64(Id));
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
    }
}