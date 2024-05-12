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
    public class ManagersController : Controller
    {
        private Emp_travel_booking_system_Entities db = new Emp_travel_booking_system_Entities();

        // GET: managers
        public async Task<ActionResult> Index()
        {
            return View(await db.managers.ToListAsync());
        }

        // GET: managers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            manager manager = await db.managers.FindAsync(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            return View(manager);
        }

        // GET: managers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: managers/Create
   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "managerid,name,department,email,mgr_password")] manager manager)
        {
            if (ModelState.IsValid)
            {
                db.managers.Add(manager);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(manager);
        }

        // GET: managers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            manager manager = await db.managers.FindAsync(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            return View(manager);
        }

        // POST: managers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "managerid,name,department,email,mgr_password")] manager manager)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manager).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(manager);
        }

        // GET: managers/Delete/5

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            manager manager = await db.managers.FindAsync(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            return View(manager);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            manager manager = await db.managers.FindAsync(id);
            if (manager != null)
            {
                if (manager.status == true)
                {
                    // Instead of removing, update the status to indicate soft delete

                    manager.status = false; // Assuming false indicates deleted
                    await db.SaveChangesAsync();
                }
                else
                {
                    manager.status = true; // Assuming false indicates deleted
                    await db.SaveChangesAsync();
                }

            }

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
