using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Employee_Travel_Booking_App.Controllers.Employee
{
    public class EmployeeLoginController : Controller
    {
       
        private readonly Emp_travel_booking_Entities db; // Replace YourDbContext with your actual DbContext class

        public EmployeeLoginController()
        {
            db = new Emp_travel_booking_Entities(); // Initialize your DbContext
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    throw new Exception("Email and password are required.");
                }

                // Retrieve the admin by email
                var emp = db.employees.FirstOrDefault(a => a.email == email && a.emp_password == password);

                // Validate admin existence and password
                if (emp != null)
                {
                    Session["EmployeeId"] = emp.employeeid;
                    Session["EmployeeName"] = emp.emp_name;
                    Session["EmployeeEmail"] = emp.email;
                    // FormsAuthentication.SetAuthCookie(email, false);
                    // Redirect to the dashboard controller
                    return RedirectToAction("Index", "TravelRequests");
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

        public ActionResult Logout()
        {
            //FormsAuthentication.SignOut();
            return RedirectToAction("Login", "EmployeeLogin");
        }
    }
}