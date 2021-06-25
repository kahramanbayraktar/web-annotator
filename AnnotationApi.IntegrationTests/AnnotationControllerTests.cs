using AnnotationApi.Controllers;
using AnnotationApi.Models;
using AnnotationApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace AnnotationApi.IntegrationTests
{
    [TestClass]
    public class AnnotationControllerTests
    {
        private Mock<IAnnotationService> _mockService;
        private Mock<AnnotationController> _mockController;

        [TestInitialize]
        public void Init()
        {
            _mockService = new Mock<IAnnotationService>();
            _mockService.Setup(m => m.Get()).Returns(new List<IAnnotation>());
            _mockController = new Mock<AnnotationController>(_mockService.Object) { CallBase = true };
        }

        [TestMethod]
        public void Get()
        {
            // Arrange

            // Act
            var result = _mockController.Object.Get();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<IAnnotation>>));
            Assert.IsInstanceOfType(result.Value, typeof(List<IAnnotation>));
            Assert.AreEqual(0, result.Value.Count);
        }

        [TestMethod]
        public void Create()
        {
            // Arrange
            var mockAnnotation = new Mock<IAnnotation>();
            mockAnnotation.SetupProperty(m => m.DbId, "3");

            _mockService.Setup(m => m.Create(mockAnnotation.Object)).Returns(mockAnnotation.Object);
            _mockService.Setup(m => m.Update(It.IsAny<string>(), mockAnnotation.Object));

            // Act
            var result = _mockController.Object.Create(mockAnnotation.Object);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<IAnnotation>));
            Assert.AreEqual(result.Value, null);
        }
    }
}
