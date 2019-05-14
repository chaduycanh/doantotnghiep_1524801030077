using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityLibrary;

namespace DoAnTotNghiep_1524801030077.Controllers
{
    public class LoginController : Controller
    {
        LoginServices loginServices = new LoginServices();
        EmailServices emailServices = new EmailServices();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
        public ActionResult ResetPassword(string emailaddress)
        {
            emailServices.SendMail(emailaddress);
            return Json(new {error =true });
        }
        public ActionResult Login(string emailaddress, string password)
        {
            if(loginServices.Login(emailaddress, password))
            {
                Session["UserInfo"] = emailaddress;
            
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
       
        }
    }
}