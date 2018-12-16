using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVGS.Data;
using CVGS.Models;
using CVGS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CVGS.Areas.Customer.Controllers
{
    [Authorize(Roles = SD.SuperAdminUser + ", " + SD.EmployeeUser + ", " + SD.MemberUser)]
    [Area("Customer")]
    public class CreditCardsController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _db;

        public CreditCardsController(ApplicationDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }
        public IActionResult Index(int? id = null)
        {
            var userId = _db.ApplicationUser.SingleOrDefault(m => m.UserName == User.Identity.Name).Id;
            var cards = _db.CreditCards.Where(m => m.ApplicationUserId == userId).ToList();
            if (id!= null)
            {
                var card = _db.CreditCards.SingleOrDefault(w => w.Id == id);
                _db.Remove(card);
                _db.SaveChanges();
                cards = _db.CreditCards.Where(m => m.ApplicationUserId == userId).ToList();
                return View(cards);
            }
            
            ViewBag.test = _config["ConnectionStrings:HashKey"];
            return View(cards);
        }
        public IActionResult Create()
        {
            return View();
        }
        //post
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CreditCards creditCard)
        {
            var userId = _db.ApplicationUser.SingleOrDefault(m => m.UserName == User.Identity.Name).Id;
            if (ModelState.IsValid)
            {
                creditCard.Last4Digits = creditCard.CreditCardNumberHash.Substring(creditCard.CreditCardNumberHash.Length - 4);
                creditCard.CreditCardNumberHash = EncryptDecrypt.Encrypt(creditCard.CreditCardNumberHash, SD.hashKey);
                creditCard.CVVHash = EncryptDecrypt.Encrypt(creditCard.CVVHash, SD.hashKey);
                creditCard.ApplicationUserId = userId;
                _db.Add(creditCard);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(creditCard);
        }
    }
}