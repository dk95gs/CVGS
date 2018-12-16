using CVGS.Areas.Admin.Controllers;
using CVGS.Controllers;
using CVGS.Data;
using CVGS.Models;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace CVGS.Iteration2.Tests
{
    [TestClass]
    public class ControllerTest
    {
        private readonly HostingEnvironment _hostingEnv;

        //Naming convention
        //METHOD_PARAMS_EXPECTED BEHAVIOUR
        [TestMethod]
        public async Task RateVideoGame_RatingHigherThenAllowedValueAndGameId_ReturnsNotFoundResult()
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

            await c.Create(game);


            var sf = new StoreFrontController(_dbContext);

            var result = await sf.RateGame(9, 2);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
