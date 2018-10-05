using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Models;
using Common.Repository;
using Moq;
using SensorService.Controllers;
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
    }
}
