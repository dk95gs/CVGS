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
using PagedList.Core;
using Rotativa.AspNetCore;

namespace CVGS.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.SuperAdminUser + ", " + SD.EmployeeUser)]
    [Area("Admin")]
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ReportsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MemberListIndex(int page = 1, int pageSize = 10)
        {
            PagedList<ApplicationUser> model = new PagedList<ApplicationUser>(_db.ApplicationUser, page, pageSize); ;
            return View("MemberListIndex",model);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MemberListIndex(string gender)
        {
            int page = 1;
            int pageSize = 10;
            ViewBag.gender = gender;
            var item = _db.ApplicationUser.Where(a=>a.Gender == gender);
            PagedList<ApplicationUser> model = new PagedList<ApplicationUser>(item, page, pageSize); ;
            return View("MemberListIndex", model);
        }


        public IActionResult MemberListReport(string gender)
        {
            IQueryable<ApplicationUser> item;
            if(gender == null)
            {
                item = _db.ApplicationUser;
            }
            else
            {
                item = _db.ApplicationUser.Where(a => a.Gender == gender);
            }
            //return View(item.ToList());
            return new ViewAsPdf(item.ToList());
        }

        public IActionResult MemberDetailsIndex(int page = 1, int pageSize = 10)
        {
            PagedList<ApplicationUser> model = new PagedList<ApplicationUser>(_db.ApplicationUser, page, pageSize); ;
            return View("MemberDetailsIndex", model);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MemberDetailsIndex(string userName)
        {
            int page = 1;
            int pageSize = 10;
            ViewBag.userName = userName;
            var item = _db.ApplicationUser.Where(a => a.UserName == userName);
            PagedList<ApplicationUser> model = new PagedList<ApplicationUser>(item, page, pageSize); ;
            return View("MemberDetailsIndex", model);
        }


        public IActionResult MemberDetailsReport(string userName)
        {
            IQueryable<ApplicationUser> item;
            if (userName == null)
            {
                item = _db.ApplicationUser;
            }
            else
            {
                item = _db.ApplicationUser.Where(a => a.UserName == userName);
            }
            //return View(item.ToList());
            return new ViewAsPdf(item.ToList());
        }
        public IActionResult GameListIndex(int page = 1, int pageSize = 10)
        {
            PagedList<Games> model = new PagedList<Games>(_db.Games, page, pageSize); ;
            return View("GameListIndex", model);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GameListIndex(string esrbRating)
        {
            int page = 1;
            int pageSize = 10;
            ViewBag.esrbRating = esrbRating;
            var item = _db.Games.Where(a => a.ESRB_Ratings == esrbRating);
            PagedList<Games> model = new PagedList<Games>(item, page, pageSize); ;
            return View("GameListIndex", model);
        }


        public IActionResult GameListReport(string esrbRating)
        {
            IQueryable<Games> item;
            if (esrbRating == null)
            {
                item = _db.Games;
            }
            else
            {
                item = _db.Games.Where(a => a.ESRB_Ratings == esrbRating);
            }
            //return View(item.ToList());
            return new ViewAsPdf(item.ToList());
        }

        public IActionResult GameDetailsIndex(int page = 1, int pageSize = 10)
        {
            PagedList<Games> model = new PagedList<Games>(_db.Games, page, pageSize); ;
            return View("GameDetailsIndex", model);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GameDetailsIndex(string title)
        {
            int page = 1;
            int pageSize = 10;
            ViewBag.gameTitle = title;
            var item = _db.Games.Where(a => a.Title == title);
            PagedList<Games> model = new PagedList<Games>(item, page, pageSize); ;
            return View("GameDetailsIndex", model);
        }


        public IActionResult GameDetailsReport(string title)
        {
            if (title == null)
            {
                return RedirectToAction(nameof(GameDetailsIndex));
            }
            else
            {
               
                return new ViewAsPdf(_db.Games.Where(a => a.Title == title).ToList());

            }
            //return new ViewAsPdf(item.ToList());
        }

        public IActionResult WishListsIndex(int page = 1, int pageSize = 10)
        {
            //PagedList<WishLists> model = new PagedList<WishLists>(_db.WishLists, page, pageSize);
            var rec = _db.WishLists.Include(m => m.ApplicationUser).Include(m => m.Games).ToList();
            return View(rec);
        }

        public IActionResult WishListsReport()
        {
                return new ViewAsPdf(_db.WishLists.Include(m=>m.ApplicationUser).Include(m=>m.Games).ToList());
     
            //return new ViewAsPdf(item.ToList());
        }

        public IActionResult SalesIndex()
        {
            //PagedList<WishLists> model = new PagedList<WishLists>(_db.WishLists, page, pageSize);
            return View(_db.Orders.Include(m=>m.ApplicationUser).ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SalesIndex(DateTime date)
        {
            var rec = _db.Orders.Include(m => m.ApplicationUser).Where(m => m.CreationDate < date).ToList();
            ViewBag.searchDate = date;
            return View(rec);
        }

        public IActionResult SalesReport(DateTime date)
        {
            return new ViewAsPdf(_db.Orders.Include(m => m.ApplicationUser).Where(m => m.CreationDate < date).ToList());

            //return new ViewAsPdf(item.ToList());
        }
    }
}