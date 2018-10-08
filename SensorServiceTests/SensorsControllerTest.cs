using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Models;
using Common.Repository;
using Moq;
using SensorService.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace SensorServiceTests
{
    public class SensorsControllerTest
    {
        [Fact]
        public async Task Get_WhenCalled_ReturnsOkResult()
        {
            var mockedRepo = new Mock<IRepository<Sensor>>();

            var sensors = new List<Sensor>
            {
                new Sensor()
                {
                    Name = "test_sensor_1"
                },
                new Sensor()
                {
                    Name = "test_sensor_2"
                }
            };

            mockedRepo.Setup(repo => repo.GetAll()).ReturnsAsync(sensors);

            var controller = new SensorsController(mockedRepo.Object);
            var results = await controller.Get();

            Assert.NotEmpty(results);
            Assert.Equal(results, sensors);
        }

        [Fact]
        public async Task GetById_WhenCalled_ReturnsOkResult()
        {
            var mockedRepo = new Mock<IRepository<Sensor>>();

            var sensors = new List<Sensor>
            {
                new Sensor()
                {
                    Name = "test_sensor_1",
                    SensorId = 1
                },
                new Sensor()
                {
                    Name = "test_sensor_2",
                    SensorId = 2
                }
            };

            mockedRepo.Setup(repo => repo.GetById(1)).ReturnsAsync(sensors[0]);
            mockedRepo.Setup(repo => repo.GetById(2)).ReturnsAsync(sensors[1]);

            var controller = new SensorsController(mockedRepo.Object);
            var result = await controller.Get(1);

            Assert.NotNull(result);
            Assert.Equal(result, sensors[0]);

            result = await controller.Get(2);

            Assert.NotNull(result);
            Assert.Equal(result, sensors[1]);
        }

        [Fact]
        public async Task Post_WhenCalled_ReturnsOkResult()
        {
            var mockedRepo = new Mock<IRepository<Sensor>>();

            var sensors = new List<Sensor>
            {
                new Sensor()
                {
                    Name = "test_sensor_1",
                    SensorId = 1
                },
                new Sensor()
                {
                    Name = "test_sensor_2",
                    SensorId = 2
                }
            };

            mockedRepo.Setup(repo => repo.GetById(1)).ReturnsAsync(sensors[0]);
            mockedRepo.Setup(repo => repo.GetById(2)).ReturnsAsync(sensors[1]);

            var controller = new SensorsController(mockedRepo.Object);

            var newSensor = new Sensor()
            {
                Name = "test_sensor_1",
                SensorId = 3
            };

            mockedRepo.Setup(repo => repo.Add(newSensor)).ReturnsAsync(true);
            mockedRepo.Setup(repo => repo.GetById(3)).ReturnsAsync(newSensor);

            var result = await controller.Post(newSensor);
            var contentResult = result as CreatedAtRouteResult;

            Assert.NotNull(contentResult);
            Assert.NotNull(contentResult.Value);
            Assert.Equal(contentResult.Value, newSensor);
        }

        [Fact]
        public async Task Post_WhenCalled_BadObject_ReturnsBadRequest()
        {
            var mockedRepo = new Mock<IRepository<Sensor>>();

            var controller = new SensorsController(mockedRepo.Object);

            var newSensor = new Sensor()
            {
                Name = "test_sensor_1",
                SensorId = 3
            };

            mockedRepo.Setup(repo => repo.Add(newSensor)).ReturnsAsync(false);

            var result = await controller.Post(newSensor);
            var contentResult = result as BadRequestResult;

            Assert.NotNull(contentResult);
        }

        [Fact]
        public async Task Put_WhenCalled_ReturnsOk()
        {
            var mockedRepo = new Mock<IRepository<Sensor>>();

            var controller = new SensorsController(mockedRepo.Object);

            var newSensor = new Sensor()
            {
                Name = "test_sensor_1",
                SensorId = 3
            };

            mockedRepo.Setup(repo => repo.Edit(newSensor)).ReturnsAsync(true);

            var result = await controller.Put(newSensor);
            var contentResult = result as OkObjectResult;

            Assert.NotNull(contentResult);
            Assert.NotNull(contentResult.Value);
            Assert.Equal(contentResult.Value, newSensor);
        }

        [Fact]
        public async Task Put_WhenCalled_BadObject_ReturnsBadRequest()
        {
            var mockedRepo = new Mock<IRepository<Sensor>>();

            var controller = new SensorsController(mockedRepo.Object);

            var newSensor = new Sensor()
            {
                Name = "test_sensor_1",
                SensorId = 3
            };

            mockedRepo.Setup(repo => repo.Edit(newSensor)).ReturnsAsync(false);

            var result = await controller.Put(newSensor);
            var contentResult = result as BadRequestResult;

            Assert.NotNull(contentResult);

        }
    }
}
