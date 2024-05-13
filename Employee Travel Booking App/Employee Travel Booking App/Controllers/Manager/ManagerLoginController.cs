using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Employee_Travel_Booking_App.Controllers.Manager
{
    public class ManagerLoginController : Controller
    {
        Emp_travel_booking_Entities db = new Emp_travel_booking_Entities();
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult ManagerLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManagerLogin(string email, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    throw new Exception("Email and password are required.");
                }

                // Retrieve the admin by email
                var manager = db.managers.FirstOrDefault(a => a.email == email);

                // Validate admin existence and password
                if (manager != null)
                {
                    Session["ManagerId"] = manager.managerid;
                    Session["email"] = email;
                    Session["password"] = password;
                    // Redirect to the dashboard controller
                    return RedirectToAction("ManagerDashboard", "Manager");
                }
                else
                {
                    throw new Exception("Invalid email or password.");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
    }
}