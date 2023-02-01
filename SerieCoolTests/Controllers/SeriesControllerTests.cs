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
        private static SeriesController seriesController;

        [ClassInitialize()]
        public void SeriesControllerTest()
        {
            var builder = new DbContextOptionsBuilder<SeriesDbContext>().UseNpgsql("Server=localhost;port=5432;Database=SeriesDB; uid=postgres; password=postgres;");
            SeriesDbContext _context = new SeriesDbContext(builder.Options);
            seriesController = new SeriesController(_context);
    }

        [TestMethod()]
        public void GetSeriesTest()
        {
            // Arrange
            // Act
            var result = seriesController.GetSeries();
            // Assert
            Assert.AreNotEqual(result, 0);
        }
    }
}