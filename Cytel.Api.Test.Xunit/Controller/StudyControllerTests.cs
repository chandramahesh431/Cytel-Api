using Cytel.Top.Api.Controllers;
using Cytel.Top.Api.Interfaces;
using Cytel.Top.Api.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

public class StudyControllerTests
{
    private readonly Mock<IStudyService> _mockService;
    private readonly StudyController _controller;

    public StudyControllerTests()
    {
        // Arrange
        _mockService = new Mock<IStudyService>();
        _controller = new StudyController(_mockService.Object);
    }

    [Fact]
    public void StudyController_GetMethod()
    {
        List<Study> lst = new List<Study>();
        lst.Add(new Study { StudyName = "Study1", ProtocolID = "ProtocolID1" });
        // Arrange
        _mockService.Setup(repo => repo.FindAll())
            .Returns((lst));
        var controller = new StudyController(_mockService.Object);

        // Act
        var result = controller.Get();

       //Assert
        Assert.Equal(lst, result);

    }
}