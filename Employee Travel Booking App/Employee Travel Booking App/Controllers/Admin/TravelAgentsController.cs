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
    public class TravelAgentsController : Controller
    {
        private Emp_travel_booking_system_Entities db = new Emp_travel_booking_system_Entities();

        // GET: travelagents
        public async Task<ActionResult> Index()
        {
            return View(await db.travelagents.ToListAsync());
        }

        // GET: travelagents/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            travelagent travelagent = await db.travelagents.FindAsync(id);
            if (travelagent == null)
            {
                return HttpNotFound();
            }
            return View(travelagent);
        }

        // GET: travelagents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: travelagents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "agentid,name,email,travel_agent_password")] travelagent travelagent)
        {
            if (ModelState.IsValid)
            {
                db.travelagents.Add(travelagent);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(travelagent);
        }

        // GET: travelagents/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            travelagent travelagent = await db.travelagents.FindAsync(id);
            if (travelagent == null)
            {
                return HttpNotFound();
            }
            return View(travelagent);
        }

        // POST: travelagents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "agentid,name,email,travel_agent_password")] travelagent travelagent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(travelagent).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(travelagent);
        }

        // GET: travelagents/Delete/5

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            travelagent travelagent = await db.travelagents.FindAsync(id);
            if (travelagent == null)
            {
                return HttpNotFound();
            }
            return View(travelagent);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            travelagent travelagent = await db.travelagents.FindAsync(id);
            if (travelagent != null)
            {
                if (travelagent.status == true)
                {
                    // Instead of removing, update the status to indicate soft delete

                    travelagent.status = false; // Assuming false indicates deleted
                    await db.SaveChangesAsync();
                }
                else
                {
                    travelagent.status = true; // Assuming false indicates deleted
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
