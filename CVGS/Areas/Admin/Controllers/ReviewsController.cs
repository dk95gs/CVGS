using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVGS.Data;
using CVGS.Models;
using CVGS.Models.ViewModel;
using CVGS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

namespace CVGS.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.SuperAdminUser + ", " + SD.EmployeeUser)]
    [Area("Admin")]
    public class ReviewsController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public ReviewsViewModel ReviewsVM { get; set; }

        public ReviewsController(ApplicationDbContext db)
        {
            _db = db;
            ReviewsVM = new ReviewsViewModel()
            {
                ApplicationUser = _db.ApplicationUser.ToList(),
                Reviews = new Models.Reviews()
            };
        }
        public async Task<IActionResult> Index(int page = 1, int pageList = 2)
        {
            var review = _db.Reviews.Include(a=>a.ApplicationUser).Where(a=>a.isApproved == false).OrderBy(a => a.Title);
            var model = await PagingList.CreateAsync(review, pageList, page);

            return View(model);
        }
        public async Task<IActionResult> ApproveReview(int? id)
        {
            if(!ModelState.IsValid)
            {
                return NotFound();
            }
            var review = await _db.Reviews.FindAsync(id);
            if(review == null)
            {
                return NotFound();
            }
            return View(review);
        }
        public async Task<IActionResult> ConfirmApproveReview(int? id)
        {


            if (ModelState.IsValid)
            {
                var reviewFromDb = _db.Reviews.Where(m => m.Id == id).FirstOrDefault();
                reviewFromDb.isApproved = true;
                await _db.SaveChangesAsync();

            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteReview(int? id)
        {


            if (ModelState.IsValid)
            {
                Reviews reviewFromDb = await _db.Reviews.FindAsync(id);

                _db.Reviews.Remove(reviewFromDb);
                await _db.SaveChangesAsync();

            }
            return RedirectToAction(nameof(Index));
        }

    }
}