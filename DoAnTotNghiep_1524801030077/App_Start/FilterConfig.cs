using System.Web;
using System.Web.Mvc;

namespace DoAnTotNghiep_1524801030077
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
