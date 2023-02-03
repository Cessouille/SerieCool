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
    }
}