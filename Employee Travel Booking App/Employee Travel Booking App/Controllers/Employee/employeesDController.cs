using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Employee_Travel_Booking_App.Controllers.Employee
{
    public class employeesDController : Controller
    {
        private Emp_travel_booking_Entities db = new Emp_travel_booking_Entities();



        // GET: employees/Details/5
        public ActionResult ERDetails()
        {
            var employeeId = (int)Session["EmployeeId"];
            if (employeeId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee employee = db.employees.Find(employeeId);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}