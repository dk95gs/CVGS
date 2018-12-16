using CVGS.Areas.Admin.Controllers;
using CVGS.Data;
using CVGS.Models;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace CVGS.Iteration1.Tests
{
    [TestClass]
    public class GamesControllerTest
    {
        private readonly HostingEnvironment _hostingEnv;

        //Naming convention
        //METHOD_PARAMS_EXPECTED BEHAVIOUR
        [TestMethod]
        public void Index_ApplicationDbContextAndHostingEnvironmentAsParam_ReturnsIndexView()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase("test");
            var _dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var c = new GamesController(_dbContext, _hostingEnv);
            //act
            var result =  c.Index();
            //assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }
        [TestMethod]
        public async Task Create_GameObjectAsParam_ReturnsIndexView()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase("test");
            var _dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var c = new GamesController(_dbContext, _hostingEnv);

            Games game = new Games();
            game.Id = 1;
            game.Title = "test";
            game.Genre = "rock";
            game.Description = "loud stuff";
            game.Publisher = "da";
            game.Developer = "EA";
            game.ESRB_Ratings = "lalaalaa";
            game.ReleaseDate = new System.DateTime();
            game.Price = 2;
            game.GameLink = "dsa";
            game.Available = "dsadas";
            game.EntryDate = new System.DateTime();

            var result = await c.Create(game);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));

        }

        [TestMethod]
        public async Task Edit_IdAsParam_ReturnIndexView()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase("test");
            var _dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var c = new GamesController(_dbContext, _hostingEnv);
            Games game = new Games();
            game.Id = 1;
            game.Title = "test";
            game.Genre = "rock";
            game.Description = "loud stuff";
            game.Publisher = "da";
            game.Developer = "EA";
            game.ESRB_Ratings = "lalaalaa";
            game.ReleaseDate = new System.DateTime();
            game.Price = 2;
            game.GameLink = "dsa";
            game.Available = "dsadas";
            game.EntryDate = new System.DateTime();

            var result = await c.Edit(1);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public async Task Edit_IdAndInvalidGameObjectAsParam_ReturnIndexView()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase("test");
            var _dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var c = new GamesController(_dbContext, _hostingEnv);
            Games game = new Games();
            

            var result = await c.Edit(1);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public async Task Details_IdAsParam_ReturnDetailsIndexView()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase("test");
            var _dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var c = new GamesController(_dbContext, _hostingEnv);

            var result = await c.Details(1);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public async Task Delete_IdAsParam_ReturnConfirmDeleteView()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase("test");
            var _dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var c = new GamesController(_dbContext, _hostingEnv);

            var result = await c.Delete(1);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
