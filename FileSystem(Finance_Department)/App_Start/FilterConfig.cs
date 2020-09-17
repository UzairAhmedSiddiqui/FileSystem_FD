using System.Web;
using System.Web.Mvc;

namespace FileSystem_Finance_Department_
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
