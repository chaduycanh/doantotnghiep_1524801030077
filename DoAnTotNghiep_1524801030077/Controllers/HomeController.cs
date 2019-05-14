using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityLibrary;

namespace DoAnTotNghiep_1524801030077.Controllers
{
    public class HomeController : AdminController
    {
        HomeServices _homeSrv = new HomeServices();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult GetAllCalander()
        {
            var msgv = Session["UserInfo"].ToString();

            var data = _homeSrv.GetAllDataCalender(msgv);
            List<Calender> tempData = new List<Calender>();
           // 2018 - 11 - 09T16: 00:00
            foreach (var item in data)
            {
                var a = item.Date.ToString().Split('/');
                if(a[0].Length<2)
                {
                    a[0] = "0" + a[0];
                }
                if (a[1].Length < 2)
                {
                    a[1] = "0" + a[1];
                }
                var date = a[2].Substring(0,4) + "-" + a[0] + "-" + a[1] + "T" + item.Hour + ":00";
                Calender temp = new Calender();
                temp.title = item.Content;
                temp.start = date;
                temp.description = item.Location;
                tempData.Add(temp);
            }

            return Json(new { returnData = tempData });
        }
        public class Calender
        {
            public string title { get; set; }
            public string description { get; set; }
            public string start { get; set; }
        }
    }
}