using System.Web;
using System.Web.Mvc;

namespace Employee_Travel_Booking_App
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
