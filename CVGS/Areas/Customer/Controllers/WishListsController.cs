using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVGS.Data;
using CVGS.Models;
using CVGS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CVGS.Areas.Customer.Controllers
{
    [Authorize(Roles = SD.SuperAdminUser + ", " + SD.EmployeeUser + ", " + SD.MemberUser)]
    [Area("Customer")]
    public class WishListsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public WishListsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            double sumOfWishlist = 0;
            var userName = User.Identity.Name;
            var rec = await _db.WishLists.Include(m => m.ApplicationUser).Include(m => m.Games).Where(m => m.ApplicationUser.UserName == userName).ToListAsync();

            foreach (var item in rec)
            {
                sumOfWishlist = sumOfWishlist + item.Games.Price;
            }
            ViewBag.totalWishList = sumOfWishlist;
            return View(rec);
        }
        public async Task<IActionResult> GetUserWishList(string Id)
        {
            var friendCheck = _db.FriendsFamilyLists.Where(m => m.ChildUserId == Id && m.ParentUserName == User.Identity.Name).ToList();

            if(friendCheck.Count != 0)
            {          
                double sumOfWishlist = 0;
                var rec = await _db.WishLists.Include(m => m.ApplicationUser).Include(m => m.Games).Where(m => m.ApplicationUserId == Id).ToListAsync();
                var userName = _db.ApplicationUser.Where(m => m.Id == Id).SingleOrDefault().UserName;          

                foreach (var item in rec)
                {
                    sumOfWishlist = sumOfWishlist + item.Games.Price;
                }
                ViewBag.totalWishList = sumOfWishlist;
                ViewBag.otherMemberName = userName;
                return View("Index",rec);
            }
            HttpContext.Session.SetString("wlMessage", "Must be friends with member to view their wishlist");
            return RedirectToAction("Details", "Members", new { id = Id });
        }
        public async Task<IActionResult> Add(int Id)
        {
          
            var userId = _db.ApplicationUser.Where(m => m.UserName == User.Identity.Name).SingleOrDefault().Id;
            var game = await _db.Games.Where(m => m.Id == Id).SingleOrDefaultAsync();
            
            WishLists wishList = new WishLists() {
                GameId = Id,
                ApplicationUserId = userId
            };

            _db.Add(wishList);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index", "StoreFront", null);
        }
        public async Task<IActionResult> Delete(int? Id)
        {
            var rec = _db.WishLists.Where(m => m.Id == Id).FirstOrDefault();

            _db.Remove(rec);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}