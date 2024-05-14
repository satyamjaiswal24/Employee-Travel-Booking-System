using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Employee_Travel_Booking_App.Controllers.Travel_Agent
{
    public class AgentLoginController : Controller
    {
        // GET: AgentLogin
        private readonly Emp_travel_booking_Entities db; // Replace YourDbContext with your actual DbContext class

        public AgentLoginController()
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
                var agent = db.travelagents.FirstOrDefault(a => a.email == email && a.travel_agent_password == password);

                // Validate admin existence and password
                if (agent != null)
                {
                    Session["email"] = email;
                    Session["password"] = password;
                    FormsAuthentication.SetAuthCookie(email, false);
                    // Redirect to the dashboard controller
                    return RedirectToAction("AgentIndex", "AgentDashboard");
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
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "AgentLogin");
        }

    }
}