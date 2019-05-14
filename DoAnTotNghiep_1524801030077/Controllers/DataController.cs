using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityLibrary;

namespace DoAnTotNghiep_1524801030077.Controllers
{
    public class DataController : AdminController
    {
        // GET: Data
        UserServices _userSrv = new UserServices();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAllUser()
        {
            return Json(new { jsData = _userSrv.GetAllData() });
        }
        public ActionResult GetAllRole()
        {
            return Json(new { jsData = _userSrv.GetAllRole() });
        }
    }
}