using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Employee_Travel_Booking_App.Controllers.Manager
{
    [Authorize]
    public class ManagerController : Controller
    {

        Emp_travel_booking_Entities db = new Emp_travel_booking_Entities();

       
            // GET: Manager
        public ActionResult ManagerDashboard()
        {
            return View();
        }
        public ActionResult Requests()
        {
            // Get the manager's login ID from the session
            int? managerId = Session["ManagerId"] as int?;

            // Retrieve travel requests of employees reporting to this manager
            var pendingRequests = db.travelrequests.Where(r => r.approvalstatus == "pending" && r.employee.managerid == managerId).ToList();

            return View(pendingRequests);

        }

        // GET: Manager/Details/5
        public ActionResult Details(int id)
        {
            // Retrieve the details of a specific travel request
            var request = db.travelrequests.FirstOrDefault(x => x.requestid == id);
            return View(request);
        }

        // POST: Manager/Approve/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Approve(int id)
        {
            // Update the status of the travel request to "Approved"
            var request = db.travelrequests.Find(id);
            request.approvalstatus = "Approved";
            db.SaveChanges();
            return RedirectToAction("Requests");
        }

        // POST: Manager/Reject/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reject(int id)
        {
            // Update the status of the travel request to "Rejected"
            var request = db.travelrequests.Find(id);
            request.approvalstatus = "Rejected";
            db.SaveChanges();
            return RedirectToAction("Requests");
        }

        public ActionResult RequestHistory()
        {
            // Get the manager's login ID from the session
            int? managerId = Session["ManagerId"] as int?;

            // Retrieve the history of travel requests made by reporting employees
            var requestHistory = db.travelrequests.Where(r => r.employeeid == r.employeeid && r.employee.managerid == managerId).ToList();
            return View(requestHistory);
        }
    }
}