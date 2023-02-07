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
            var result = seriesController.GetSerie(100000);
            if (result.Result.Value == null)
                seriesController.PostSerie(serie);
            result = seriesController.GetSerie(100000000);
            if (result.Result.Value != null)
                seriesController.DeleteSerie(100000000);
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
            var result = seriesController.GetSeries().Result.Value.Where(s => s.Serieid <= 3).ToList();
            lesSeries.Add(new Serie(1,"Scrubs","J.D. est un jeune médecin qui débute sa carrière dans l'hôpital du Sacré-Coeur. Il vit avec son meilleur ami Turk, qui lui est chirurgien dans le même hôpital. Très vite, Turk tombe amoureux d'une infirmière Carla. Elliot entre dans la bande. C'est une étudiante en médecine quelque peu surprenante. Le service de médecine est dirigé par l'excentrique Docteur Cox alors que l'hôpital est géré par le diabolique Docteur Kelso. A cela viennent s'ajouter plein de personnages hors du commun : Todd le chirurgien obsédé, Ted l'avocat dépressif, le concierge qui trouve toujours un moyen d'embêter JD... Une belle galerie de personnage !",9,184,2001,"ABC (US)"));
            lesSeries.Add(new Serie(2,"James May's 20th Century","The world in 1999 would have been unrecognisable to anyone from 1900. James May takes a look at some of the greatest developments of the 20th century, and reveals how they shaped the times we live in now.",1,6,2007,"BBC Two"));
            lesSeries.Add(new Serie(3,"True Blood","Ayant trouvé un substitut pour se nourrir sans tuer (du sang synthétique), les vampires vivent désormais parmi les humains. Sookie, une serveuse capable de lire dans les esprits, tombe sous le charme de Bill, un mystérieux vampire. Une rencontre qui bouleverse la vie de la jeune femme...",7,81,2008,"HBO"));
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
            var result = seriesController.GetSerie(1);
            Serie serie = new Serie(1, "Scrubs", "J.D. est un jeune médecin qui débute sa carrière dans l'hôpital du Sacré-Coeur. Il vit avec son meilleur ami Turk, qui lui est chirurgien dans le même hôpital. Très vite, Turk tombe amoureux d'une infirmière Carla. Elliot entre dans la bande. C'est une étudiante en médecine quelque peu surprenante. Le service de médecine est dirigé par l'excentrique Docteur Cox alors que l'hôpital est géré par le diabolique Docteur Kelso. A cela viennent s'ajouter plein de personnages hors du commun : Todd le chirurgien obsédé, Ted l'avocat dépressif, le concierge qui trouve toujours un moyen d'embêter JD... Une belle galerie de personnage !", 9, 184, 2001, "ABC (US)");
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
            Serie serie = new Serie(100000000, "Mercredi", "Mercredi Addams est envoyée par ses parents, Gomez et Morticia, au sein de Nevermore Academy, à Jericho dans le Vermont, après avoir été une nouvelle fois expulsée d'un lycée.", 1, 8, 2022, "Netflix");
            var result = seriesController.PostSerie(serie);
            CreatedAtActionResult routeResult = (CreatedAtActionResult)result.Result.Result;
            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Serie>), "Pas un ActionResult");
            Assert.IsInstanceOfType(result.Result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtRouteResult");
            Assert.AreEqual(routeResult.StatusCode, StatusCodes.Status201Created);
            Assert.AreEqual(routeResult.Value, new Serie(100000000, "Mercredi", "Mercredi Addams est envoyée par ses parents, Gomez et Morticia, au sein de Nevermore Academy, à Jericho dans le Vermont, après avoir été une nouvelle fois expulsée d'un lycée.", 1, 8, 2022, "Netflix"));
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

            var result = seriesController.GetSerie(100000);
            seriesController.DeleteSerie(100000);
        }
    }
}