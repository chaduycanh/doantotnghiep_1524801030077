using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityLibrary;

namespace DoAnTotNghiep_1524801030077.Controllers
{
    public class WorkingHourDefineController : Controller
    {
        // GET: WorkingHourDefine
        WokingHourDefineServices _wrkhSrv = new WokingHourDefineServices();
        #region cate
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SaveCate(WorkingDefineCate data)
        {
            var tempDate = _wrkhSrv.SaveCate(data);
            return Json(new { returnData = tempDate, jsData = _wrkhSrv.GetAllCate()});
        }
        public ActionResult GetAllCate()
        {
            return Json(new { returnData = _wrkhSrv.GetAllCate() });
        }
        public ActionResult EditCate(string Id)
        {
            var data = _wrkhSrv.GetDataById(Convert.ToInt64(Id));
            return Json(new { returnData = data });
        }
        public ActionResult UpdateCate(WorkingDefineCate modal)
        {
            try
            {
                _wrkhSrv.UpdateCate(modal);
                return Json(new { returnData = true, jsData = _wrkhSrv.GetAllCate() });
            }
            catch (Exception)
            {

                return Json(new { returnData = false });
            }

        }
        public ActionResult DeleteCate(string Id)
        {
            try
            {
                var data = _wrkhSrv.DeleteCate(Convert.ToInt64(Id));
                
                return Json(new { returnData = data, jsData = _wrkhSrv.GetAllCate() });
            }
            catch (Exception)
            {

                return Json(new { returnData = false });
            }

        }
        #endregion
        #region detail
        public ActionResult SaveDetail(WorkingDefine data)
        {
            var tempDate = _wrkhSrv.SaveDetail(data);
            return Json(new { returnData = tempDate, jsData = _wrkhSrv.GetAllDetail() });
        }
        public ActionResult GetAllDetail()
        {
            return Json(new { returnData = _wrkhSrv.GetAllDetail() });
        }
        public ActionResult EditDetail(string Id)
        {
            var data = _wrkhSrv.GetDataDetailById(Convert.ToInt64(Id));
            return Json(new { returnData = data });
        }
        public ActionResult UpdateDetail(WorkingDefine modal)
        {
            try
            {
                _wrkhSrv.UpdateDetail(modal);
                return Json(new { returnData = true, jsData = _wrkhSrv.GetAllDetail() });
            }
            catch (Exception)
            {

                return Json(new { returnData = false });
            }

        }
        public ActionResult DeleteDetail(string Id)
        {
            try
            {
                var data = _wrkhSrv.DeleteDetail(Convert.ToInt64(Id));

                return Json(new { returnData = data, jsData = _wrkhSrv.GetAllDetail() });
            }
            catch (Exception)
            {

                return Json(new { returnData = false });
            }

        }
        #endregion
    }
}