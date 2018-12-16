using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CVGS.Data;
using CVGS.Models;
using CVGS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CVGS.Areas.Customer.Controllers
{
    [Authorize(Roles = SD.SuperAdminUser + ", " + SD.EmployeeUser + ", " + SD.MemberUser)]
    [Area("Customer")]
    public class OwnedGamesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly HostingEnvironment _hostingEnv;
        public OwnedGamesController(ApplicationDbContext db, HostingEnvironment hostingEnv)
        {
            _db = db;
            _hostingEnv = hostingEnv;
        }
        public async  Task<IActionResult> Index()
        {
            var rec = await _db.OwnedGames.Include(m => m.ApplicationUser).FirstOrDefaultAsync(m => m.ApplicationUser.UserName == User.Identity.Name);
            if(rec == null)
            {
                List<Games> tempList = new List<Games>();
                rec = new OwnedGames()
                {
                    ApplicationUserId = _db.ApplicationUser.FirstOrDefault(m => m.UserName == User.Identity.Name).Id,
                    GameList = JsonConvert.SerializeObject(tempList)
                };
            }
            var gameList = JsonConvert.DeserializeObject <IEnumerable<Games>>(rec.GameList);
            return View(gameList);
        }
        public async Task<IActionResult> Download(string filename)
        {
            string webRootPath = _hostingEnv.WebRootPath;
            byte[] fileBytes = System.IO.File.ReadAllBytes(webRootPath + filename);
            return File(fileBytes, "application/x-msdownload", webRootPath + filename);
        }
    }
}