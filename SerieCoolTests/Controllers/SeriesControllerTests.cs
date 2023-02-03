using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerieCool.Controllers;
using SerieCool.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerieCool.Controllers.Tests
{
    [TestClass()]
    public class SeriesControllerTests
    {
        [TestInitialize()]
        public void Initialiaze()
        {
            var builder = new DbContextOptionsBuilder<SeriesDbContext>().UseNpgsql("Server=localhost;port=5432;Database=SeriesDB; uid=postgres; password=postgres;");
            SeriesDbContext _context = new SeriesDbContext(builder.Options);
            SeriesController seriesController = new SeriesController(_context);

            Serie serie = new Serie(100000, "Mercredi", "Mercredi Addams est envoyée par ses parents, Gomez et Morticia, au sein de Nevermore Academy, à Jericho dans le Vermont, après avoir été une nouvelle fois expulsée d'un lycée.", 1, 8, 2022, "Netflix");
            var result = seriesController.GetSerie(140);
            if (result.Result.Value == null)
                seriesController.PostSerie(serie);
        }
        [TestMethod()]
        public void GetSeriesTest_Ok()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<SeriesDbContext>().UseNpgsql("Server=localhost;port=5432;Database=SeriesDB; uid=postgres; password=postgres;");
            SeriesDbContext _context = new SeriesDbContext(builder.Options);
            SeriesController seriesController = new SeriesController(_context);
            List<Serie> lesSeries = new List<Serie>();
            // Act
            var result = seriesController.GetSeries().Result.Value.Where(s => s.Serieid == 140).ToList();
            lesSeries.Add(new Serie(140,"Mercredi","Mercredi Addams est envoyée par ses parents, Gomez et Morticia, au sein de Nevermore Academy, à Jericho dans le Vermont, après avoir été une nouvelle fois expulsée d'un lycée.",1,8,2022,"Netflix"));
            // Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Serie>), "Pas un IEnumerable");
            CollectionAssert.AreEqual(lesSeries, result);
        }

        [TestMethod()]
        public void GetSerieTest_Ok()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<SeriesDbContext>().UseNpgsql("Server=localhost;port=5432;Database=SeriesDB; uid=postgres; password=postgres;");
            SeriesDbContext _context = new SeriesDbContext(builder.Options);
            SeriesController seriesController = new SeriesController(_context);
            // Act
            var result = seriesController.GetSerie(140);
            Serie serie = new Serie(140, "Mercredi", "Mercredi Addams est envoyée par ses parents, Gomez et Morticia, au sein de Nevermore Academy, à Jericho dans le Vermont, après avoir été une nouvelle fois expulsée d'un lycée.", 1, 8, 2022, "Netflix");
            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Serie>), "Pas un Task<ActionResult<Serie>>");
            Assert.IsInstanceOfType(result.Result.Value, typeof(Serie), "Pas une Serie");
            Assert.AreEqual(serie, result.Result.Value);
        }

        [TestMethod()]
        public void GetSerieTest_NonOk()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<SeriesDbContext>().UseNpgsql("Server=localhost;port=5432;Database=SeriesDB; uid=postgres; password=postgres;");
            SeriesDbContext _context = new SeriesDbContext(builder.Options);
            SeriesController seriesController = new SeriesController(_context);
            // Act
            var result = seriesController.GetSerie(0);
            Serie serie = new Serie(140, "Mercredi", "Mercredi Addams est envoyée par ses parents, Gomez et Morticia, au sein de Nevermore Academy, à Jericho dans le Vermont, après avoir été une nouvelle fois expulsée d'un lycée.", 1, 8, 2022, "Netflix");
            // Assert
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult<Serie>>), "Pas un Task<ActionResult<Serie>>");
            Assert.AreNotEqual(serie, result);
            Assert.IsNull(result.Result.Value);
        }

        [TestMethod()]
        public void DeleteSerieTest_Ok()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<SeriesDbContext>().UseNpgsql("Server=localhost;port=5432;Database=SeriesDB; uid=postgres; password=postgres;");
            SeriesDbContext _context = new SeriesDbContext(builder.Options);
            SeriesController seriesController = new SeriesController(_context);
            // Act
            var result = seriesController.DeleteSerie(140);
            // Assert
            Assert.IsInstanceOfType(result, typeof(Task<IActionResult>), "Pas un ActionResult");
        }

        [TestMethod()]
        public void DeleteSerieTest_NonOk()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<SeriesDbContext>().UseNpgsql("Server=localhost;port=5432;Database=SeriesDB; uid=postgres; password=postgres;");
            SeriesDbContext _context = new SeriesDbContext(builder.Options);
            SeriesController seriesController = new SeriesController(_context);
            // Act
            var result = seriesController.DeleteSerie(0);
            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult), "Pas un ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Pas un NotFoundResult");
        }

        [TestMethod()]
        public void PostSerieTest_Ok()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<SeriesDbContext>().UseNpgsql("Server=localhost;port=5432;Database=SeriesDB; uid=postgres; password=postgres;");
            SeriesDbContext _context = new SeriesDbContext(builder.Options);
            SeriesController seriesController = new SeriesController(_context);
            // Act
            Serie serie = new Serie(140, "Mercredi", "Mercredi Addams est envoyée par ses parents, Gomez et Morticia, au sein de Nevermore Academy, à Jericho dans le Vermont, après avoir été une nouvelle fois expulsée d'un lycée.", 1, 8, 2022, "Netflix");
            var result = seriesController.PostSerie(serie);
            CreatedAtActionResult routeResult = (CreatedAtActionResult)result.Result.Result;
            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Serie>), "Pas un ActionResult");
            Assert.IsInstanceOfType(result.Result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtRouteResult");
            Assert.AreEqual(routeResult.StatusCode, StatusCodes.Status201Created);
            Assert.AreEqual(routeResult.Value, new Serie(140, "Mercredi", "Mercredi Addams est envoyée par ses parents, Gomez et Morticia, au sein de Nevermore Academy, à Jericho dans le Vermont, après avoir été une nouvelle fois expulsée d'un lycée.", 1, 8, 2022, "Netflix"));
        }

        /*[TestMethod()]
        public void PostSerieTest_NonOk()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<SeriesDbContext>().UseNpgsql("Server=localhost;port=5432;Database=SeriesDB; uid=postgres; password=postgres;");
            SeriesDbContext _context = new SeriesDbContext(builder.Options);
            SeriesController seriesController = new SeriesController(_context);
            // Act
            Serie serie = new Serie(140, null, null, 1, 8, 2022, "Netflix");
            var result = seriesController.PostSerie(serie);
            CreatedAtActionResult routeResult = (CreatedAtActionResult)result.Result.Result;
            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Serie>), "Pas un ActionResult");
            Assert.IsInstanceOfType(result.Result.Result, typeof(BadRequestResult), "Pas un BadRequestResult");
            Assert.AreEqual(routeResult.StatusCode, StatusCodes.Status400BadRequest);
            Assert.ThrowsException<AggregateException>(seriesController.PostSerie(serie));
        }*/

        [TestCleanup()]
        public void Cleanup()
        {
            var builder = new DbContextOptionsBuilder<SeriesDbContext>().UseNpgsql("Server=localhost;port=5432;Database=SeriesDB; uid=postgres; password=postgres;");
            SeriesDbContext _context = new SeriesDbContext(builder.Options);
            SeriesController seriesController = new SeriesController(_context);

            var result = seriesController.GetSerie(140);
            seriesController.DeleteSerie(100000);
        }
    }
}