using AnnotationApi.Models;
using AnnotationApi.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using System;

namespace AnnotationApi.Tests
{
    [TestClass]
    public class AnnotationServiceTests
    {
        private IAnnotationDatabaseSettings _settings = new AnnotationDatabaseSettings();
        private string _conStr = "";
        private string _db = "";
        private string _col = "";

        [TestInitialize]
        public void Init()
        {
            _conStr = Config.Get("AnnotationDatabaseSettings:ConnectionString");
            _db = Config.Get("AnnotationDatabaseSettings:DatabaseName");
            _col = Config.Get("AnnotationDatabaseSettings:AnnotationsCollectionName");
            _settings = new AnnotationDatabaseSettings { ConnectionString = _conStr, DatabaseName = _db, AnnotationsCollectionName = _col};
        }

        [TestMethod]
        public void GetAllAnnotations_WhenConnectionStringIncorrect_ShouldReturnMongoAuthenticationException()
        {
            // Arrange
            _settings = new AnnotationDatabaseSettings { ConnectionString = _conStr.Replace("admin", "administrator"), DatabaseName = _db, AnnotationsCollectionName = _col };
            var service = new AnnotationService(_settings);

            // Act
            try
            {
                var result = service.Get();
            }
            catch (Exception exc)
            {
                // Assert
                Assert.IsInstanceOfType(exc, typeof(MongoAuthenticationException));
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void GetAllAnnotations_WhenConnectionStringCorrect_ShouldReturnAnnotations()
        {
            // Arrange
            var service = new AnnotationService(_settings);

            // Act
            var result = service.Get();

            // Assert
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void GetAnnotationById_WhenIdFormatIncorrect_ShouldReturnFormatException()
        {
            // Arrange
            var service = new AnnotationService(_settings);

            // Act
            try
            {
                var result = service.Get("1");
            }
            catch (Exception exc)
            {
                // Assert
                Assert.IsInstanceOfType(exc, typeof(FormatException));
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void GetAnnotationById_WhenAnnotationNotExists_ShouldReturnNull()
        {
            // Arrange
            var service = new AnnotationService(_settings);

            // Act
            var result = service.Get("00d1d2875f6a2210bcd4dfc8");

            // Assert
            Assert.IsTrue(result == null);
        }

        [TestMethod]
        public void GetAnnotationById_WhenAnnotationExists_ShouldReturnAnnotation()
        {
            // Arrange
            var service = new AnnotationService(_settings);

            // Act
            var result = service.Get("60d1d2875f6a2210bcd4dfc8");

            // Assert
            Assert.IsTrue(result != null);
        }
    }
}
