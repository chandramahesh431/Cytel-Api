using Cytel.Top.Api.Controllers;
using Cytel.Top.Api.Interfaces;
using Cytel.Top.Api.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Cytel.Top.Api.Test
{
    public class StudyControllerTest
    {
        public IEnumerable<Study> exceptedStudyList;
        Mock<IStudyService> serviceMock;
        /// <summary>
        /// Test Case for study controller GET/POST actions
        /// Setup the mocking Objects
        /// </summary>
        [SetUp]
        public void Setup()
        {
            serviceMock = new Mock<IStudyService>();
            exceptedStudyList = new List<Study>() {
                new Study(){ StudyName = "Study10",StudyStartDate = DateTime.Now,EstimatedCompletionDate = DateTime.Now,ProtocolID = "P103",StudyGroup = "Group3",Phase = "Phase3",PrimaryIndication = "PIYes3",SecondaryIndication = "SIYes3"}};
        }
        /// <summary>
        /// Test case for Get EndPoint
        /// </summary>
        [Test]
        public void Get_All_Study_Info()
        {
            var controllerMock = new StudyController(serviceMock.Object);
            serviceMock.Setup(m => m.FindAll()).Returns(exceptedStudyList).Verifiable();
            Assert.AreEqual(exceptedStudyList, controllerMock.Get());
        }
        /// <summary>
        /// Test case for POST EndPoint
        /// 
        /// </summary>
        [Test]
        public void Add_Study_Details()
        {
            var studyInfo = new Study
            {
                StudyName = "Study331",
                StudyStartDate = Convert.ToDateTime("05/06/2020"),
                EstimatedCompletionDate = Convert.ToDateTime("07/07/2020"),
                ProtocolID = "P104",
                StudyGroup = "Group3",
                Phase = "Phase3",
                PrimaryIndication = "PIYes3",
                SecondaryIndication = "SIYes3",
            };
            var controllerMock = new StudyController(serviceMock.Object);
            serviceMock.Setup(m => m.Add(studyInfo));
            Assert.AreEqual(studyInfo, studyInfo);
        }
    }
}