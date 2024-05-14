using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Employee_Travel_Booking_App.Controllers.Employee
{
    public class TravelRequestsController : Controller
    {
        private Emp_travel_booking_Entities db = new Emp_travel_booking_Entities();

        // GET: travelrequests
        public ActionResult Index()
        {
            // Check if EmployeeId is stored in the session
            if (Session["EmployeeId"] != null)
            {
                var employeeId = (int)Session["EmployeeId"];

                // Filter travel requests by EmployeeId
                var travelrequests = db.travelrequests
                    .Where(t => t.employeeid == employeeId)
                    .Include(t => t.employee)
                    .ToList();

                return View(travelrequests);
            }
            else
            {
                // Handle case where EmployeeId is not stored in session
                // Redirect to login page or display an error message
                return RedirectToAction("Login", "EmployeeLogin");
            }
        }


        // GET: travelrequests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            travelrequest travelrequest = db.travelrequests.Find(id);
            if (travelrequest == null)
            {
                return HttpNotFound();
            }
            return View(travelrequest);
        }



        // GET: travelrequests/Create
        public ActionResult Create()
        {
            ViewBag.employeeid = new SelectList(db.employees, "employeeid", "emp_name");
            return View();
        }

        // POST: travelrequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "reasonfortravel,flightneeded,hotelneeded,departurecity,arrivalcity,departuredate,departuretime,additionalinformation")] travelrequest travelrequest)
        {

            if (ModelState.IsValid)
            {
                // Check if the EmployeeId session variable exists and is of the correct type
                if (Session["EmployeeId"] != null && Session["EmployeeId"] is int employeeId)
                {
                    var employeeName = (string)Session["EmployeeName"];
                    var employeeEmail = (string)Session["EmployeeEmail"];

                    travelrequest.employeeid = employeeId;
                    travelrequest.employeeemail = employeeEmail;
                    travelrequest.employeename = employeeName;

                    travelrequest.approvalstatus = "Pending";
                    travelrequest.bookingstatus = "Pending";
                    db.travelrequests.Add(travelrequest);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    throw new Exception("EmployeeId session variable is missing or invalid.");
                }
            }

            ViewBag.employeeid = new SelectList(db.employees, "employeeid", "emp_name", travelrequest.employeeid);
            return View(travelrequest);
        }

        // GET: travelrequests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            travelrequest travelrequest = db.travelrequests.Find(id);
            if (travelrequest == null)
            {
                return HttpNotFound();

            }
            ViewBag.employeeid = new SelectList(db.employees, "employeeid", "emp_name", travelrequest.employeeid);
            return View(travelrequest);
        }

        // POST: travelrequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string approvalstatus)
        {
            travelrequest travelrequest = db.travelrequests.Find(id);
            if (travelrequest == null)
            {
                return HttpNotFound();
            }

            // Update only the approval status
            travelrequest.approvalstatus = approvalstatus;

            // Save changes
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}