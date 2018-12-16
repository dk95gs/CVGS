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

namespace CVGS.Areas.Customer.Controllers
{
    [Authorize(Roles = SD.SuperAdminUser + ", " + SD.EmployeeUser + ", " + SD.MemberUser)]
    [Area("Customer")]
    public class ShippingMailingAddressesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ShippingMailingAddressesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var rec = await _db.ShippingMailingAddresses.Where(m => m.ApplicationUser.UserName == User.Identity.Name).ToListAsync();
            return View(rec);
        }
        //get
        public IActionResult Create()
        {
            return View();
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ShippingMailingAddresses ShippingAddress)
        {
            if (!ModelState.IsValid)
            {
                return View(ShippingAddress);
            }
            ShippingAddress.ApplicationUserId = _db.ApplicationUser.SingleOrDefault(m => m.UserName == User.Identity.Name).Id;
            _db.Add(ShippingAddress);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //get
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var address = await _db.ShippingMailingAddresses.FindAsync(Id);

            if(address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (int Id, ShippingMailingAddresses SA)
        {
            if (!ModelState.IsValid)
            {
                return View(SA);
            }
            var address = await _db.ShippingMailingAddresses.FindAsync(Id);

            address.StreetName = SA.StreetName;
            address.HouseNumber = SA.HouseNumber;
            address.PostalCode = SA.PostalCode;
            address.Province = SA.Province;
            address.City = SA.City;
            address.Country = SA.Country;
            address.AddressType = SA.AddressType;

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var rec = await _db.ShippingMailingAddresses.FindAsync(Id);

            if (rec == null)
                return NotFound();

            _db.Remove(rec);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}