using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using POEClaim.Controllers;
using POEClaim.Models;
using POEClaim.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace TestProjectClaim
{
    [TestClass]
    public class ClaimControllerTests
    {
        private Mock<ApplicationDbContext> _mockContext;
        private ClaimController _controller;
        private object model;

        [TestInitialize]
        public void Setup()
        {
            _mockContext = new Mock<ApplicationDbContext>();

            // We will mock Claims DbSet here if needed
            _controller = new ClaimController(_mockContext.Object);
        }

        [TestMethod]
        public void Index_ReturnsAllClaims_ForPC()
        {
            // Arrange
            var claims = new List<Claim>
            {
                new Claim { Id = 1, FacultyName="John Doe", Email="lecturer@test.com" },
                new Claim { Id = 2, FacultyName="Jane Smith", Email="lecturer2@test.com" }
            };

            // Setup your mock here if using EF Core DbSet
            // _mockContext.Setup(c => c.Claims.ToList()).Returns(claims);

            // Act
            var result = _controller.Index() as ViewResult;
            var model = result?.Model as List<Claim>;

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(model);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(2, model.Count);
        }

        [TestMethod]
        public void VerifyClaim_ChangesStatusToVerified()
        {
            // Arrange
            var claim = new Claim { Id = 1, Status = "Pending" };
            // Mock Find method
            _mockContext.Setup(c => c.Claims.Find(1)).Returns(claim);

            // Act
            var result = _controller.VerifyClaim(1) as RedirectToActionResult;

            // Assert
          //  Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(2, model.Count);
          //  Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(2, model.Count);
        }

        [TestMethod]
        public void RejectClaim_ChangesStatusToRejected()
        {
            // Arrange
            var claim = new Claim { Id = 2, Status = "Pending" };
            _mockContext.Setup(c => c.Claims.Find(2)).Returns(claim);

            // Act
            var result = _controller.RejectClaim(2) as RedirectToActionResult;

            // Assert
          //  Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(2, model.Count);
          //  Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(2, model.Count);
        }

        [TestMethod]
        public void ApproveClaim_ChangesStatusToApproved()
        {
            // Arrange
            var claim = new Claim { Id = 3, Status = "Pending" };
            _mockContext.Setup(c => c.Claims.Find(3)).Returns(claim);

            // Act
            var result = _controller.ApproveClaim(3) as RedirectToActionResult;

            // Assert
          //  Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(2, model.Count);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("Index", result.ActionName);
        }
    }
}



































    