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
    public class FriendsFamilyListsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public FriendsFamilyListsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
           
            var rec = await _db.FriendsFamilyLists.Include(m => m.ChildApplicationUser).Where(m => m.ParentUserName == User.Identity.Name).ToListAsync();            
            return View(rec);
        }
        public async Task<IActionResult> AwaitingApproval()
        {            
            var rec = await _db.FriendsAwaitingApprovals.Include(m => m.ChildApplicationUser).Where(m => m.ParentUserName == User.Identity.Name).ToListAsync();
            return View(rec);
        }
        public async Task<IActionResult> ApproveFriend(string ParentName, string ChildId)
        {
            var rec = await _db.FriendsAwaitingApprovals.Include(m=>m.ChildApplicationUser).Where(m => m.ParentUserName == ParentName && m.ChildUserId == ChildId).FirstOrDefaultAsync();

            var parentId = _db.ApplicationUser.Where(m => m.UserName == ParentName).SingleOrDefault().Id;

            FriendsFamilyLists ffList = new FriendsFamilyLists()
            {
                ParentUserName = rec.ChildApplicationUser.UserName,
                ChildUserId = parentId
            };
            FriendsFamilyLists currentUserFFList = new FriendsFamilyLists()
            {
                ParentUserName = rec.ParentUserName,
                ChildUserId = rec.ChildUserId
            };
            _db.Add(ffList);
            _db.Add(currentUserFFList);
            _db.Remove(rec);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(AwaitingApproval));
        }
        public async Task<IActionResult> Add(string Id)
        {   
            var parentName = _db.ApplicationUser.Where(m => m.Id == Id).SingleOrDefault().UserName;
            var childId = _db.ApplicationUser.Where(m => m.UserName == User.Identity.Name).SingleOrDefault().Id;

            var friendRec = _db.FriendsFamilyLists.Where(m => m.ChildUserId == childId && m.ParentUserName == parentName);
            if(friendRec.Count() > 0)
            {
                HttpContext.Session.SetString("friendMessage", "You are already friends with this member");
                return RedirectToAction("Index", "Members", null);
            }
            var awaitingRec = _db.FriendsAwaitingApprovals.Where(m => m.ChildUserId == childId && m.ParentUserName == parentName);
            if (awaitingRec.Count() > 0)
            {
                HttpContext.Session.SetString("friendMessage","You have already sent this member a friends request");
                return RedirectToAction("Index", "Members", null);
            }
            FriendsAwaitingApproval ffList = new FriendsAwaitingApproval()
            {
                ParentUserName = parentName,
                ChildUserId = childId
                
            };
            _db.Add(ffList);
            await _db.SaveChangesAsync();
            HttpContext.Session.SetString("friendMessage",$"Sent {parentName} a friend request");
            return RedirectToAction("Index", "Members", null);
        }
        public async Task<IActionResult> Delete(string Id, string from, string userName)
        {
            
            if ( from == "Approval")
            {
                var record = _db.FriendsAwaitingApprovals.Where(m => m.ChildUserId == Id).FirstOrDefault();
                _db.Remove(record);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(AwaitingApproval));
            }
            var userOne = _db.FriendsFamilyLists.Where(m => m.ChildUserId == Id).FirstOrDefault();
            var userTwo = _db.FriendsFamilyLists.Include(m=>m.ChildApplicationUser).Where(m => m.ParentUserName == userName && m.ChildApplicationUser.UserName == User.Identity.Name).FirstOrDefault();
            _db.Remove(userOne);
            _db.Remove(userTwo);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}