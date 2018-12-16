using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVGS.Data;
using CVGS.Models;
using CVGS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;

namespace CVGS.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.SuperAdminUser + ", " + SD.EmployeeUser)]
    [Area("Admin")]
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public EventsController( ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index( int page = 1, int pageList = 10)
        {
            var e = _db.Events.OrderBy(n => n.Name);
            var model = await PagingList.CreateAsync(e, pageList, page);
            return View(model);
        }
        //get
        public IActionResult Create()
        {
            return View();
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create (Events events)
        {
            if (ModelState.IsValid)
            {
                _db.Add(events);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(events);
        }
        //get
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var e = await _db.Events.FindAsync(id);

            if (e == null)
                return NotFound();

            return View(e);
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Events e)
        {
            if (id != e.Id)
                return NotFound();

            if(ModelState.IsValid)
            {
                var eventFromDb = _db.Events.Where(m => m.Id == e.Id).FirstOrDefault();

                eventFromDb.Name = e.Name;
                eventFromDb.Details = e.Details;
                eventFromDb.StartDate = e.StartDate;
                eventFromDb.StartingTime = e.StartingTime;
                eventFromDb.EndDate = e.EndDate;
                eventFromDb.EndingTime = e.EndingTime;
                eventFromDb.Location = e.Location;
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(e);
        }
        //get
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var e = await _db.Events.FindAsync(id);
            if (e == null)
            {
                return NotFound();
            }
            return View(e);
        }
        //get
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var events = await _db.Events.FindAsync(id);
            if (events == null)
            {
                return NotFound();
            }
            return View(events);
        }
        //post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            Events events = await _db.Events.FindAsync(id);

            if (events == null)
            {
                return NotFound();
            }
            else
            {                
                _db.Events.Remove(events);

                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }
    }
}