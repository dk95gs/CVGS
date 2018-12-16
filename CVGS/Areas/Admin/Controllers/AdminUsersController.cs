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
    public class AdminUsersController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AdminUsersController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index( int page = 1, int pageList = 10)
        {
            var aUser = _db.ApplicationUser.OrderBy(a => a.UserName);
            var model = await PagingList.CreateAsync(aUser, pageList, page);
            return View(model);
        }
        //get edit
        public async Task<IActionResult> Edit(string id)
        {
            if(id==null || id.Trim().Length == 0)
            {
                return NotFound();
            }
            var userFromDb = await _db.ApplicationUser.FindAsync(id);
            if(userFromDb == null)
            {
                return NotFound();
            }
            return View(userFromDb);
        }
        //post edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit (string id, ApplicationUser applicationUser)
        {
            if (id != applicationUser.Id)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                ApplicationUser userFromDb = _db.ApplicationUser.Where(u => u.Id == id).FirstOrDefault();
                userFromDb.FirstName = applicationUser.FirstName;
                userFromDb.LastName = applicationUser.LastName;
                userFromDb.Gender = applicationUser.Gender;
                userFromDb.BirthDate = applicationUser.BirthDate;
                userFromDb.Email = applicationUser.Email;

                var x = ModelState;

                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            
            return View(applicationUser);
        }
        //get delete
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || id.Trim().Length == 0)
            {
                return NotFound();
            }
            var userFromDb = await _db.ApplicationUser.FindAsync(id);
            if (userFromDb == null)
            {
                return NotFound();
            }
            return View(userFromDb);
        }
        //post delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(string id)
        {          
            ApplicationUser userFromDb = _db.ApplicationUser.Where(u => u.Id == id).FirstOrDefault();
            _db.Remove(userFromDb);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}