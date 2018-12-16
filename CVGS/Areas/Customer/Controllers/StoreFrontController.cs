using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CVGS.Models;
using CVGS.Data;
using CVGS.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using ReflectionIT.Mvc.Paging;
using CVGS.Utility;

namespace CVGS.Controllers
{
    [Area("Customer")]
    public class StoreFrontController : Controller
    {
        private readonly ApplicationDbContext _db;

        public StoreFrontController(ApplicationDbContext db)
        {
            _db = db;                     
        }

        public async Task<IActionResult> Index(string title, int page = SD.page, int pageList = SD.pageSize)
        {
         

            if (title != null)
            {
                var reviews = _db.Games.Where(n => n.Title.Contains(title)).OrderBy(n=>n.Title);
                var model = await PagingList.CreateAsync(reviews, pageList, page);
                return View(model);
            }
            var r = _db.Games.OrderBy(n => n.Title);
            var m = await PagingList.CreateAsync(r, pageList, page);
            return View(m);
        }
        public async Task<IActionResult> Reviews(int gameId, string gameName)
        {
            ViewBag.gameName = gameName;
            var rec = _db.Reviews.Include(m=>m.ApplicationUser).Where(m=>m.GameId == gameId && m.isApproved == true);
            return View(await rec.ToListAsync());
        }
        public IActionResult CreateReview(string userName, int gameId, string gameName)
        {
            ApplicationUser ap = _db.ApplicationUser.FirstOrDefault(m => m.UserName == userName);
            HttpContext.Session.SetString("gameName", gameName);
            ViewBag.userId = ap.Id;
            ViewBag.gameId = gameId;
            return View();
        }

        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> CreateReview(Reviews review, int gameId)
        {
            var x = gameId;
            if (ModelState.IsValid)
            {
                _db.Add(review);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                
            }
            
            return View(review);
        }
        public async Task<IActionResult> EditReview(int? id)
        {
            if (id == null)
                return NotFound();

            var review = await _db.Reviews.FindAsync(id);

            if (review == null)
                return NotFound();

            return View(review);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> EditReview(int id, Reviews review)
        {

            if (id != review.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var reviewFromDb = _db.Reviews.Where(m => m.Id == review.Id).FirstOrDefault();

                reviewFromDb.Title = review.Title;
                reviewFromDb.Body = review.Body;
                reviewFromDb.GameId = review.GameId;
                reviewFromDb.ApplicationUserId = review.ApplicationUserId;
                reviewFromDb.isApproved = review.isApproved;
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(review);
        }
        public async Task<IActionResult> RateGame(float Rating, int GameId)
        {
            if (Rating >= 6)
            {
                return NotFound();
            }
            Ratings ratingRec = new Ratings();
            var userId = _db.ApplicationUser.Where(m=>m.UserName == User.Identity.Name).SingleOrDefault().Id;

            var exists = _db.Ratings.SingleOrDefault(m => m.ApplicationUserId == userId && m.GameId == GameId);
            if(exists != null)
            {
                
                exists.Rating = Rating;
                await _db.SaveChangesAsync();
                var gRating = _db.Ratings.Where(m => m.GameId == GameId).ToList();                
                var game = await _db.Games.FindAsync(GameId);
                game.Rating = SD.CalculateRating(gRating);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ratingRec.GameId = GameId;
            ratingRec.ApplicationUserId = userId;
            ratingRec.Rating = Rating;
            _db.Add(ratingRec);
            await _db.SaveChangesAsync();

            var gameRatings = _db.Ratings.Where(m => m.GameId == GameId).ToList();
           
            var gameToUpdate = await _db.Games.FindAsync(GameId);
            gameToUpdate.Rating = SD.CalculateRating(gameRatings);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult GameDetails(int Id)
        {           
            return View(_db.Games.SingleOrDefault(m=>m.Id == Id));
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
