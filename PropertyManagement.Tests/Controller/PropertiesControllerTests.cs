using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PropertyManagement.Api.Models.Property;
using PropertyManagement.Application.Dtos;
using PropertyManagement.Application.Interfaces;
using PropertyManagement.Controllers;

namespace PropertyManagement.Tests.Controllers
{
    [TestClass]
    public class PropertiesControllerTests
    {
        private Mock<IPropertyService> propertyServiceMock;
        private Mock<IMapper> mapperMock;
        private PropertiesController controller;

        [TestInitialize]
        public void TestInitialize()
        {
            propertyServiceMock = new Mock<IPropertyService>();
            mapperMock = new Mock<IMapper>();
            controller = new PropertiesController(propertyServiceMock.Object, mapperMock.Object);
        }

        [TestMethod]
        public async Task Create_ShouldReturnCreatedResult_WhenPropertyIsCreatedSuccessfully()
        {
            // Setup
            var propertyRequest = new PropertyRequestModel { Name = "Test Property" };
            var propertyDto = new PropertyRequestDTO { Name = "Test Property" };
            var createdPropertyDto = new PropertyResponseDTO { ExternalId = Guid.NewGuid(), Name = "Test Property" };
            var responseModel = new PropertyResponseModel { ExternalId = createdPropertyDto.ExternalId, Name = "Test Property" };

            mapperMock.Setup(m => m.Map<PropertyRequestDTO>(propertyRequest)).Returns(propertyDto);
            propertyServiceMock.Setup(s => s.CreatePropertyAsync(propertyDto)).ReturnsAsync(createdPropertyDto);
            mapperMock.Setup(m => m.Map<PropertyResponseModel>(createdPropertyDto)).Returns(responseModel);

            // Act
            var result = await controller.Create(propertyRequest) as CreatedAtActionResult;

            // Verify
            result.Should().NotBeNull();                       
            result.Value.Should().BeEquivalentTo(responseModel);
        }
       
    }
}