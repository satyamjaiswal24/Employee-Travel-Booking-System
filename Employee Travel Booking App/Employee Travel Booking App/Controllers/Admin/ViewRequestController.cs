using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Employee_Travel_Booking_App.Controllers.Admin
{
    public class ViewRequestController : Controller
    {
        private readonly Emp_travel_booking_system_Entities db;

        public ViewRequestController()
        {
            db = new Emp_travel_booking_system_Entities();
        }
        // GET: ViewRequest
        [HttpGet]
        public ActionResult GetViewRequest()
        {
            var travelRequests = db.travelrequests.ToList();
            return View(travelRequests);
        }
    }
}