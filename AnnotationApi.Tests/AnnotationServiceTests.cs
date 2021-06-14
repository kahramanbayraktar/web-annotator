using AnnotationApi.Models;
using AnnotationApi.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnnotationApi.Tests
{
    [TestClass]
    public class AnnotationServiceTests
    {
        private IAnnotationDatabaseSettings _settings = new AnnotationDatabaseSettings();

        [TestInitialize]
        public void Init()
        {
            var conStr = Config.Get("AnnotationDatabaseSettings:ConnectionString");
            var db = Config.Get("AnnotationDatabaseSettings:DatabaseName");
            var col = Config.Get("AnnotationDatabaseSettings:AnnotationsCollectionName");
            _settings = new AnnotationDatabaseSettings { ConnectionString = conStr, DatabaseName = db, AnnotationsCollectionName = col};
        }

        [TestMethod]
        public void StandardTest()
        {
            // Arrange
            var service = new AnnotationService(_settings);

            // Act
            var result = service.Search("test");

            // Assert

        }
    }
}
