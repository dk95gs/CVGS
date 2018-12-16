using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVGS.Data;
using CVGS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CVGS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ShipmentsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ShipmentsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public  IActionResult Index()
        {
            var rec = _db.Shipments.Include(m => m.ApplicationUser).Where(m => m.isApproved == false && m.isRejected != true);
            return View(rec);
        }
        public async Task<IActionResult> StartProcess(int Id)
        {
            var recToProcess = await _db.Shipments.FindAsync(Id);
            recToProcess.isProccessing = true;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Approve(int Id)
        {
            var recToApprove = await _db.Shipments.FindAsync(Id);
            recToApprove.isApproved = true;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Reject(int Id)
        {
            var recToReject = await _db.Shipments.FindAsync(Id);
            recToReject.isRejected = true;
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> OrderItems(int Id)
        {
            var rec = await _db.Shipments.FirstOrDefaultAsync(m => m.Id == Id);
            var orders = JsonConvert.DeserializeObject<Orders>(rec.Order);
            var orderItems = JsonConvert.DeserializeObject<List<Games>>(orders.MyCartItems);
            var additionalItems = JsonConvert.DeserializeObject<Dictionary<string, IEnumerable<Games>>>(orders.FriendsCartItems);

            foreach (var items in additionalItems)
            {
                foreach (var order in items.Value)
                {
                    orderItems.Add(order);
                }
            }

            return View(orderItems);
        }
    }
}