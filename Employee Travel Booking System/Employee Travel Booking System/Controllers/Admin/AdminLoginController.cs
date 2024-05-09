    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Employee_Travel_Booking_System.Controllers.Admin
{
    public class AdminLoginController : Controller
    {
        private readonly Emp_travel_booking_systemEntities db; // Replace YourDbContext with your actual DbContext class

        public AdminLoginController()
        {
            db = new Emp_travel_booking_systemEntities(); // Initialize your DbContext
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
                var admin = db.admins.FirstOrDefault(a => a.email == email);

                // Validate admin existence and password
                if (admin != null && VerifyPassword(password, admin.admin_password))
                {
                    // Redirect to the dashboard controller
                    return RedirectToAction("Index", "AdminDashboard");
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

        // You can use a more secure hashing algorithm like BCrypt
        private bool VerifyPassword(string enteredPassword, string hashedPassword)
        {
            // Compare entered password with hashed password
            return enteredPassword == hashedPassword;
        }


    }
}
