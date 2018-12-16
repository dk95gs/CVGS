using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVGS.Data;
using CVGS.Models;
using CVGS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

namespace CVGS.Areas.Customer.Controllers
{
    [Authorize(Roles = SD.SuperAdminUser + ", " + SD.EmployeeUser + ", " + SD.MemberUser)]
    [Area("Customer")]
    public class EventRegistrationController : Controller
    {
        private readonly ApplicationDbContext _db;

        public EventRegistrationController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index(int? Id)
        {
            var userId = _db.ApplicationUser.SingleOrDefault(m => m.UserName == User.Identity.Name).Id;
            if (Id != null)
            {
                EventRegistration er = await _db.EventRegistration.FindAsync(Id);
                _db.EventRegistration.Remove(er);
                await _db.SaveChangesAsync();
                RouteData.Values.Remove("Id");
                
                var r = _db.EventRegistration.Include(m => m.Events).Include(m => m.ApplicationUser).Where(a => a.ApplicationUserId == userId).OrderBy(m=>m.Events.Name);
                return View(await PagingList.CreateAsync(r, SD.pageSize, SD.page));
            }

            var rec = _db.EventRegistration.Include(m => m.Events).Include(m => m.ApplicationUser).Where(a => a.ApplicationUserId == userId).OrderBy(m => m.Events.Name);
            return View(await PagingList.CreateAsync(rec, SD.pageSize, SD.page));
        }
        public async Task<IActionResult> EventsList(int? eventId)
        {    
            if(eventId == null)
                return View(_db.Events.ToList());

            var userId = _db.ApplicationUser.Where(m => m.UserName == User.Identity.Name).SingleOrDefault().Id;

            var exists = _db.EventRegistration.Where(m => m.EventId == eventId && m.ApplicationUserId == userId).SingleOrDefault();
            if (exists == null)
            {
                EventRegistration er = new EventRegistration()
                {
                    EventId = (int) eventId,
                    ApplicationUserId = userId
                };

                _db.Add(er);
                await _db.SaveChangesAsync();
                ViewBag.registerMsg = "Signed up for event";
                return View(_db.Events.ToList());
            }
            ViewBag.registerMsg = "You are already registered for that event";
            return View(_db.Events.ToList());
        }


    }
}