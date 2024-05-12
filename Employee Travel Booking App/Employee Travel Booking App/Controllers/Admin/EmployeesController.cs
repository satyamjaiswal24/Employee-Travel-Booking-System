using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Employee_Travel_Booking_App;

namespace Employee_Travel_Booking_App.Controllers.Admin
{
    public class EmployeesController : Controller
    {
        private Emp_travel_booking_system_Entities db = new Emp_travel_booking_system_Entities();

        // GET: Employees
        public async Task<ActionResult> Index()
        {
            var employees = db.employees.Include(e => e.manager);
            return View(await employees.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee employee = await db.employees.FindAsync(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.managerid = new SelectList(db.managers, "managerid", "name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "employeeid,emp_name,email,emp_password,department,position,hiredate,phonenumber,address,managerid")] employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.status = true;
                db.employees.Add(employee);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.managerid = new SelectList(db.managers, "managerid", "name", employee.managerid);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee employee = await db.employees.FindAsync(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.managerid = new SelectList(db.managers, "managerid", "name", employee.managerid);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "employeeid,emp_name,email,emp_password,department,position,hiredate,phonenumber,address,managerid")] employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.managerid = new SelectList(db.managers, "managerid", "name", employee.managerid);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public class Employee
        {
            // Your existing properties
            public int EmployeeId { get; set; }
            public string Emp_Name { get; set; }
            public string Email { get; set; }
            // Add status property
            public bool Status { get; set; }
            // Other properties
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee employee = await db.employees.FindAsync(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            employee employee = await db.employees.FindAsync(id);
            if (employee != null)
            {
                if(employee.status == true)
                {
                    // Instead of removing, update the status to indicate soft delete

                    employee.status = false; // Assuming false indicates deleted
                    await db.SaveChangesAsync();
                }
                else{
                    employee.status = true; // Assuming false indicates deleted
                    await db.SaveChangesAsync();
                }
                
            }
            
            return RedirectToAction("Index");
        }

        // GET: Employees/AssignManager/5
        public async Task<ActionResult> ChangeManager(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            employee employee = await db.employees.FindAsync(id);

            if (employee == null)
            {
                return HttpNotFound();
            }

            ViewBag.ManagerId = new SelectList(db.managers, "managerid", "name");
            return View(employee);
        }

        // POST: Employees/AssignManager/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeManager(int id, int managerId)
        {
            employee employee = await db.employees.FindAsync(id);

            if (employee == null)
            {
                return HttpNotFound();
            }

            employee.managerid = managerId;
            await db.SaveChangesAsync();

            return RedirectToAction("Index");
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
