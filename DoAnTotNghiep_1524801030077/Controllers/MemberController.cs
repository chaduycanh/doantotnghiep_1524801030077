using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using EntityLibrary;
using Shared.Utility;
namespace DoAnTotNghiep_1524801030077.Controllers
{
    public class MemberController : AdminController
    {
        // GET: WorkingHourDefine
        MemberServices _memberSrv = new MemberServices();
        #region cate
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SaveCate(Role data)
        {
            var tempDate = _memberSrv.SaveCate(data);
            return Json(new { returnData = tempDate, jsData = _memberSrv.GetAllCate() });
        }
        public ActionResult GetAllCate()
        {
            return Json(new { returnData = _memberSrv.GetAllCate() });
        }
        public ActionResult EditCate(string Id)
        {
            var data = _memberSrv.GetDataById(Convert.ToInt64(Id));
            return Json(new { returnData = data });
        }
        public ActionResult UpdateCate(Role modal)
        {
            try
            {
                _memberSrv.UpdateCate(modal);
                return Json(new { returnData = true, jsData = _memberSrv.GetAllCate() });
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
                var data = _memberSrv.DeleteCate(Convert.ToInt64(Id));

                return Json(new { returnData = data, jsData = _memberSrv.GetAllCate() });
            }
            catch (Exception)
            {

                return Json(new { returnData = false });
            }

        }
        #endregion
        #region detail
        public ActionResult SaveDetail(UserLogin data)
        {
            Guid g;
            g = Guid.NewGuid();
            data.SALT=g.ToString();
           var temp= FunctionUtility.GetMd5Hash(data.PASSWORD); 
            var newpass= FunctionUtility.GetMd5Hash(data.PASSWORD+ data.SALT);
            data.PASSWORD = newpass;
            //_memberSrv.UpdateDetail(data);
            var tempDate = _memberSrv.SaveDetail(data);
            return Json(new { returnData = tempDate, jsData = _memberSrv.GetAllDetail() });
        }
        public ActionResult GetAllDetail()
        {
            return Json(new { returnData = _memberSrv.GetAllDetail() });
        }
        public ActionResult EditDetail(string Id)
        {
            var data = _memberSrv.GetDataDetailById(Convert.ToInt64(Id));
            return Json(new { returnData = data });
        }
        public ActionResult UpdateDetail(UserLogin modal)
        {
            try
            {
   
                return Json(new { returnData = true, jsData = _memberSrv.GetAllDetail() });
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
                var data = _memberSrv.DeleteDetail(Convert.ToInt64(Id));

                return Json(new { returnData = data, jsData = _memberSrv.GetAllDetail() });
            }
            catch (Exception)
            {

                return Json(new { returnData = false });
            }

        }
        #endregion
    }
}