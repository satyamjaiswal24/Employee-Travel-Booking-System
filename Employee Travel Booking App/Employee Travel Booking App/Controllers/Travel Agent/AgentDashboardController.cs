using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Employee_Travel_Booking_App.Controllers.Travel_Agent
{
    public class AgentDashboardController : Controller
    {
        private readonly Emp_travel_booking_Entities db;

        public AgentDashboardController()
        {
            db = new Emp_travel_booking_Entities();
        }


        // GET: AgentDash
        public ActionResult AgentIndex()
        {

            // Retrieve pending travel requests and pass them to the view
            var pendingRequests = db.travelrequests;
            return View(pendingRequests);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateStatus(int requestId, string status)
        {
            try
            {
                // Check if the user is logged in
               

                // Retrieve the travel request by ID
                var travelRequest = db.travelrequests.FirstOrDefault(tr => tr.requestid == requestId);

                if (travelRequest == null)
                {
                    throw new Exception("Travel request not found.");
                }

                // Update the booking status based on availability
                travelRequest.bookingstatus = status;

                db.SaveChanges();

                TempData["SuccessMessage"] = "Booking status updated successfully.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction("AgentIndex");
        }
    }
}