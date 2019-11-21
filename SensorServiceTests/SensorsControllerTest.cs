using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Common.Repository;
using Moq;
using SensorService.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Microsoft.AspNetCore.Http;

namespace SensorServiceTests
{
    public class SensorsControllerTest
    {
        [Fact]
        public async Task Put_WhenCalled_ReturnsOkResult()
        {
            var mockedRepo = new Mock<IHomeRepository>();
            var sensor = new Sensor()
            {
                Name = "test_sensor_1"
            };

            mockedRepo.Setup(repo => repo.EditSensor(sensor)).ReturnsAsync(true);
            var controller = new SensorsController(mockedRepo.Object);
            var result = await controller.Put(sensor);
            var contentResult = (result as OkObjectResult).Value;

            Assert.NotNull(contentResult);
            Assert.Equal(contentResult, sensor);
        }

        [Fact]
        public async Task GetById_WhenCalled_ReturnsOkResult()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var sensors = new List<Sensor>
            {
                new Sensor()
                {
                    Name = "test_sensor_1",
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001")
                },
                new Sensor()
                {
                    Name = "test_sensor_2",
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002")
                }
            };

            mockedRepo.Setup(repo => repo.GetSensor(Guid.Parse("00000000-0000-0000-0000-000000000001"))).ReturnsAsync(sensors[0]);
            mockedRepo.Setup(repo => repo.GetSensor(Guid.Parse("00000000-0000-0000-0000-000000000002"))).ReturnsAsync(sensors[1]);

            var controller = new SensorsController(mockedRepo.Object);
            var result = await controller.Get("00000000-0000-0000-0000-000000000001");
            var contentResult = (result as OkObjectResult).Value as Sensor;

            Assert.NotNull(contentResult);
            Assert.Equal(contentResult, sensors[0]);

            result = await controller.Get("00000000-0000-0000-0000-000000000002");
            contentResult = (result as OkObjectResult).Value as Sensor;

            Assert.NotNull(result);
            Assert.Equal(contentResult, sensors[1]);
        }

        [Fact]
        public async Task GetById_WhenCalled_UnknownID_ReturnsNotFoundResult()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var sensors = new List<Sensor>
            {
                new Sensor()
                {
                    Name = "test_sensor_1",
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001")
                },
                new Sensor()
                {
                    Name = "test_sensor_2",
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002")
                }
            };

            mockedRepo.Setup(repo => repo.GetSensor(Guid.Parse("00000000-0000-0000-0000-000000000001"))).ReturnsAsync(sensors[0]);
            mockedRepo.Setup(repo => repo.GetSensor(Guid.Parse("00000000-0000-0000-0000-000000000002"))).ReturnsAsync(sensors[1]);

            var controller = new SensorsController(mockedRepo.Object);
            var result = await controller.Get("00000000-0000-0000-0000-000000000003");
            var contentResult = result as NotFoundResult;

            Assert.NotNull(contentResult);
        }

        [Fact]
        public async Task Put_WhenCalled_BadObject_ReturnsBadRequest()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var controller = new SensorsController(mockedRepo.Object);

            var newSensor = new Sensor()
            {
                Name = "test_sensor_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003")
            };

            mockedRepo.Setup(repo => repo.EditSensor(newSensor)).ReturnsAsync(false);

            var result = await controller.Put(newSensor);
            var contentResult = result as BadRequestResult;

            Assert.NotNull(contentResult);
        }

        [Fact]
        public async Task Delete_WhenCalled_ReturnsOk()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var controller = new SensorsController(mockedRepo.Object);

            var newSensor = new Sensor()
            {
                Name = "test_sensor_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003")
            };

            mockedRepo.Setup(repo => repo.GetSensor(Guid.Parse("00000000-0000-0000-0000-000000000003"))).ReturnsAsync(newSensor);
            mockedRepo.Setup(repo => repo.DeleteSensor(Guid.Parse("00000000-0000-0000-0000-000000000003"))).ReturnsAsync(true);

            var result = await controller.Delete("00000000-0000-0000-0000-000000000003");
            var contentResult = (result as OkObjectResult).Value;

            Assert.NotNull(contentResult);
            Assert.Equal(newSensor, contentResult);
        }

        [Fact]
        public async Task Delete_WhenCalled_UnknownObject_ReturnsNotFound()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var controller = new SensorsController(mockedRepo.Object);

            var newSensor = new Sensor()
            {
                Name = "test_sensor_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003")
            };

            mockedRepo.Setup(repo => repo.GetSensor(Guid.Parse("00000000-0000-0000-0000-000000000002"))).ReturnsAsync((Sensor)null);

            var result = await controller.Delete("00000000-0000-0000-0000-000000000002");
            var contentResult = result as NotFoundResult;

            Assert.NotNull(contentResult);
        }

        [Fact]
        public async Task Delete_WhenCalled_DeleteFailed_ReturnsServerError()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var controller = new SensorsController(mockedRepo.Object);

            var newSensor = new Sensor()
            {
                Name = "test_sensor_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003")
            };

            mockedRepo.Setup(repo => repo.GetSensor(Guid.Parse("00000000-0000-0000-0000-000000000003"))).ReturnsAsync(newSensor);
            mockedRepo.Setup(repo => repo.DeleteSensor(Guid.Parse("00000000-0000-0000-0000-000000000003"))).ReturnsAsync(false);

            var result = await controller.Delete("00000000-0000-0000-0000-000000000003");
            var contentResult = result as StatusCodeResult;

            Assert.NotNull(contentResult);
            Assert.Equal(StatusCodes.Status500InternalServerError, contentResult.StatusCode);
        }

        [Fact]
        public async Task Post_WhenCalled_ReturnsOk()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var controller = new SensorsController(mockedRepo.Object);

            var newSensor = new Sensor()
            {
                Name = "test_sensor_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                Type = "test_type"
            };

            mockedRepo.Setup(repo => repo.AddSensor(newSensor)).ReturnsAsync(newSensor);

            var result = await controller.Post(newSensor);
            var contentResult = (result as OkObjectResult).Value;

            Assert.NotNull(contentResult);
            Assert.Equal(newSensor, contentResult);
        }

        [Fact]
        public async Task Post_WhenCalled_AddFaild_ReturnsServerError()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var controller = new SensorsController(mockedRepo.Object);

            var newSensor = new Sensor()
            {
                Name = "test_sensor_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                Type = "test_type"
            };

            mockedRepo.Setup(repo => repo.AddSensor(newSensor)).ReturnsAsync((Sensor)null);

            var result = await controller.Post(newSensor);
            var contentResult = (result as StatusCodeResult).StatusCode;

            Assert.Equal(StatusCodes.Status500InternalServerError, contentResult);
        }
    }
}
