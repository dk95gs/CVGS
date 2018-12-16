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
using ReflectionIT.Mvc.Paging;

namespace CVGS.Areas.Admin.Controllers
{
    [Authorize( Roles = SD.SuperAdminUser + ", " + SD.EmployeeUser)]
    [Area("Admin")]
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly HostingEnvironment _hostingEnv;


        public GamesController(ApplicationDbContext db, HostingEnvironment hostingEnv)
        {
            _db = db;
            _hostingEnv = hostingEnv;
        }
        public async Task<IActionResult> Index(int page = 1, int pageList = 10)
        {
            
            var game = _db.Games.OrderBy(a => a.Title);
            var model = await PagingList.CreateAsync(game, pageList, page);
            return View(model);
        }
        //get 
        public IActionResult Create()
        {
            return View();
        }
        // post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Games games)
        {
            if (!ModelState.IsValid)
            {
                return View(games);
            }
            if (games.isFree)
            {
                games.Price = 0;
                games.isDownloadable = true;
            }
            _db.Add(games);
            await _db.SaveChangesAsync();

            if (_hostingEnv != null)
            {
                string webRootPath = _hostingEnv.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                var gamesFromDb = _db.Games.Find(games.Id);

                var gamePicFile = files.FirstOrDefault(m => m.Name == SD.GamePic);
                var gameExeFile = files.FirstOrDefault(m => m.Name == SD.GameExe);

                if (files.Count != 0)
                {
                    if (gamePicFile != null)
                    {
                        var imgUploads = Path.Combine(webRootPath, SD.ImageFolder);
                        var imgExtension = Path.GetExtension(gamePicFile.FileName);

                        using (var filestream = new FileStream(Path.Combine(imgUploads, games.Id + imgExtension), FileMode.Create))
                        {
                            gamePicFile.CopyTo(filestream);
                        }
                        gamesFromDb.GameLink = @"\" + SD.ImageFolder + @"\" + games.Id + imgExtension;
                    }
                    else
                    {
                        var uploads = Path.Combine(webRootPath, SD.ImageFolder + @"\" + SD.DefaultGameImage);
                        System.IO.File.Copy(uploads, webRootPath + @"\" + SD.ImageFolder + @"\" + games.Id + ".png");
                        gamesFromDb.GameLink = @"\" + SD.ImageFolder + @"\" + games.Id + ".png";
                    }
                    if (gameExeFile != null)
                    {
                        var exeUploads = Path.Combine(webRootPath, SD.ExeFolder);
                        var exeExtension = Path.GetExtension(gameExeFile.FileName);

                        using (var filestream = new FileStream(Path.Combine(exeUploads, games.Id + exeExtension), FileMode.Create))
                        {
                            gameExeFile.CopyTo(filestream);
                        }
                        gamesFromDb.ExeLink = @"\" + SD.ExeFolder + @"\" + games.Id + exeExtension;
                    }
                    else
                    {
                        var exeUploads = Path.Combine(webRootPath, SD.ExeFolder + @"\" + SD.DefaultGameExe);
                        System.IO.File.Copy(exeUploads, webRootPath + @"\" + SD.ExeFolder + @"\" + games.Id + ".png");
                        gamesFromDb.ExeLink = @"\" + SD.ExeFolder + @"\" + games.Id + ".png";
                    }
                }
                else
                {
                    var uploads = Path.Combine(webRootPath, SD.ImageFolder + @"\" + SD.DefaultGameImage);
                    System.IO.File.Copy(uploads, webRootPath + @"\" + SD.ImageFolder + @"\" + games.Id + ".png");
                    gamesFromDb.GameLink = @"\" + SD.ImageFolder + @"\" + games.Id + ".png";

                    var exeUploads = Path.Combine(webRootPath, SD.ExeFolder + @"\" + SD.DefaultGameExe);
                    System.IO.File.Copy(exeUploads, webRootPath + @"\" + SD.ExeFolder + @"\" + games.Id + ".png");
                    gamesFromDb.ExeLink = @"\" + SD.ExeFolder + @"\" + games.Id + ".png";
                }
            }
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //get
        public async Task<IActionResult> Edit (int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var games = await _db.Games.FindAsync(id);
            if(games == null)
            {
                return NotFound();
            }
            return View(games);
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (int id, Games games)
        {
            if (!ModelState.IsValid)
            {
                return View(games);
            }
            if (_hostingEnv != null)
            {
                string webRootPath = _hostingEnv.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                var gameFromDb = _db.Games.Where(m => m.Id == games.Id).FirstOrDefault();

                var gamePicFile = files.FirstOrDefault(m => m.Name == SD.GamePic);
                var gameExeFile = files.FirstOrDefault(m => m.Name == SD.GameExe);

                if (files.Count != 0)
                {
                    if(gamePicFile != null)
                    {
                        var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                        var extension_new = Path.GetExtension(gamePicFile.FileName);
                        var extension_old = Path.GetExtension(gameFromDb.GameLink);

                        if (System.IO.File.Exists(Path.Combine(uploads, games.Id + extension_old)))
                        {
                            System.IO.File.Delete((Path.Combine(uploads, games.Id + extension_old)));
                        }

                        using (var filestream = new FileStream(Path.Combine(uploads, games.Id + extension_new), FileMode.Create))
                        {
                            gamePicFile.CopyTo(filestream);
                        }
                        gameFromDb.GameLink = @"\" + SD.ImageFolder + @"\" + games.Id + extension_new;
                    }
                    if (gameExeFile != null)
                    {
                        var uploads = Path.Combine(webRootPath, SD.ExeFolder);
                        var extension_new = Path.GetExtension(gameExeFile.FileName);
                        var extension_old = Path.GetExtension(gameFromDb.ExeLink);

                        if (System.IO.File.Exists(Path.Combine(uploads, games.Id + extension_old)))
                        {
                            System.IO.File.Delete((Path.Combine(uploads, games.Id + extension_old)));
                        }

                        using (var filestream = new FileStream(Path.Combine(uploads, games.Id + extension_new), FileMode.Create))
                        {
                            gameExeFile.CopyTo(filestream);
                        }
                        gameFromDb.ExeLink = @"\" + SD.ExeFolder + @"\" + games.Id + extension_new;
                    }
                }

                if (games.GameLink != null)
                {
                    gameFromDb.GameLink = games.GameLink;
                }
                if(games.ExeLink != null)
                {
                    gameFromDb.ExeLink = games.ExeLink;
                }
                gameFromDb.Title = games.Title;
                gameFromDb.Genre = games.Genre;
                gameFromDb.Description = games.Description;
                gameFromDb.Developer = games.Developer;
                gameFromDb.Publisher = games.Publisher;
                gameFromDb.ESRB_Ratings = games.ESRB_Ratings;
                gameFromDb.ReleaseDate = games.ReleaseDate;
                gameFromDb.Price = games.Price;
                gameFromDb.Available = games.Available;
                gameFromDb.isDownloadable = games.isDownloadable;
                gameFromDb.isFree = games.isFree;
                if (games.isFree)
                {
                    gameFromDb.Price = 0;
                    gameFromDb.isDownloadable = true;
                }

                await _db.SaveChangesAsync();   
            }
            return RedirectToAction(nameof(Index));

        }
        //get
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var games = await _db.Games.FindAsync(id);
            if (games == null)
            {
                return NotFound();
            }
            return View(games);
        }
        //get
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var games = await _db.Games.FindAsync(id);
            if (games == null)
            {
                return NotFound();
            }
            return View(games);
        }
        //post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string webRootPath = _hostingEnv.WebRootPath;

            Games game = await _db.Games.FindAsync(id);

            if (game == null)
            {
                return NotFound();
            }
            else
            {
                var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                var extension = Path.GetExtension(game.GameLink);

                if (System.IO.File.Exists(Path.Combine(uploads, game.Id + extension)))
                {
                    System.IO.File.Delete(Path.Combine(uploads, game.Id + extension));
                }
                _db.Games.Remove(game);

                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }
    }
}