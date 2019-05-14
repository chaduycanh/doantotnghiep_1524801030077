using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Shared.Utility;
namespace EntityLibrary
{
    public class LoginServices
    {
        DOANTOTNGHIEPEntities entity = new DOANTOTNGHIEPEntities();
        public bool Login(string user, string pass)
        {
            try
            {
                var model = entity.UserLogins.Where(p => p.Code == user).FirstOrDefault();
                var tempPass= FunctionUtility.GetMd5Hash(pass);
                var password = FunctionUtility.GetMd5Hash(pass+model.SALT);
                if (model.PASSWORD == password)
                {
                    HttpContext.Current.Session["UserRole"] = model.Role;
                    HttpContext.Current.Session["UserName"] = model.FullName;
                    HttpContext.Current.Session["UserCode"] = model.Code;
                    return true;
                }
                else
                {
                    return false;
                }           
            }
            catch (Exception)
            {

                return false;
            }
        }

    }
}
