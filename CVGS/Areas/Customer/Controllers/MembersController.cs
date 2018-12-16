using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVGS.Data;
using CVGS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;

namespace CVGS.Areas.Customer.Controllers
{
    [Authorize(Roles = SD.SuperAdminUser + ", " + SD.EmployeeUser + ", " + SD.MemberUser)]
    [Area("Customer")]
    public class MembersController : Controller
    {
        private readonly ApplicationDbContext _db;
        public MembersController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index(string userName, int page = SD.page, int pageList = SD.pageSize)
        {
            if(userName != null)
            {
                var members = _db.ApplicationUser.Where(n => n.UserName.Contains(userName)).OrderBy(n => n.UserName);
                var model = await PagingList.CreateAsync(members, pageList, page);

                return View(model);
            }
            var mem = _db.ApplicationUser.OrderBy(n => n.UserName);
            var m = await PagingList.CreateAsync(mem, pageList, page);
            if(HttpContext.Session.GetString("friendMessage") != null)
            {
                ViewBag.friendMessage = HttpContext.Session.GetString("friendMessage");
                HttpContext.Session.Clear();
            }
            return View(m);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _db.ApplicationUser.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            if(HttpContext.Session.GetString("wlMessage") != null)
            {
                ViewBag.wlMessage = HttpContext.Session.GetString("wlMessage");
                HttpContext.Session.Clear();
            }
            return View(user);
        }

    }
}