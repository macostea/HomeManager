using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entities;
using Common.Repository;
using Moq;
using SensorService.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace SensorServiceTests
{
    public class HumiditySensorReadingsControllerTest
    {
        [Fact]
        public async Task Get_WhenCalled_ReturnsOkResult()
        {
            var mockedSensorRepo = new Mock<IRepository<Sensor>>();
            var mockedHumidityRepo = new Mock<IRepository<HumiditySensorReading>>();

            var humidityReadings = new List<HumiditySensorReading>
            {
                new HumiditySensorReading()
                {
                    Reading = 12.0,
                    SensorReadingId = 1
                },
                new HumiditySensorReading()
                {
                    Reading = 11.0,
                    SensorReadingId = 2
                }
            };

            mockedHumidityRepo.Setup(repo => repo.GetAll(It.IsAny<HumidityReadingWithSensorSpecification>())).ReturnsAsync((HumidityReadingWithSensorSpecification spec) =>
            {
                return spec.ReadingID == 0 ? humidityReadings : null;
            });

            var controller = new HumiditySensorReadingsController(mockedHumidityRepo.Object, mockedSensorRepo.Object);
            var results = await controller.Get();

            Assert.NotEmpty(results);
            Assert.Equal(results, humidityReadings);
        }

        [Fact]
        public async Task GetById_WhenCalled_ReturnsOkResult()
        {
            var mockedSensorRepo = new Mock<IRepository<Sensor>>();
            var mockedHumidityRepo = new Mock<IRepository<HumiditySensorReading>>();

            var humidityReadings = new List<HumiditySensorReading>
            {
                new HumiditySensorReading()
                {
                    Reading = 12.0,
                    SensorReadingId = 1
                }
            };

            var specification = new HumidityReadingWithSensorSpecification(1);

            mockedHumidityRepo.Setup(repo => repo.GetAll(It.IsAny <HumidityReadingWithSensorSpecification>())).ReturnsAsync((HumidityReadingWithSensorSpecification spec) =>
            {
                var isEqual = spec.ReadingID == 1;
                return isEqual ? humidityReadings : null;
            });

            var controller = new HumiditySensorReadingsController(mockedHumidityRepo.Object, mockedSensorRepo.Object);
            var result = await controller.Get(1);
            var contentResult = result as OkObjectResult;

            Assert.NotNull(contentResult);
            Assert.Equal(contentResult.Value, humidityReadings[0]);
        }

        //[Fact]
        //public async Task Post_WhenCalled_ReturnsOkResult()
        //{
        //    var mockedRepo = new Mock<IRepository<Sensor>>();

        //    var sensors = new List<Sensor>
        //    {
        //        new Sensor()
        //        {
        //            Name = "test_sensor_1",
        //            SensorId = 1
        //        },
        //        new Sensor()
        //        {
        //            Name = "test_sensor_2",
        //            SensorId = 2
        //        }
        //    };

        //    mockedRepo.Setup(repo => repo.GetById(1)).ReturnsAsync(sensors[0]);
        //    mockedRepo.Setup(repo => repo.GetById(2)).ReturnsAsync(sensors[1]);

        //    var controller = new SensorsController(mockedRepo.Object);

        //    var newSensor = new Sensor()
        //    {
        //        Name = "test_sensor_1",
        //        SensorId = 3
        //    };

        //    mockedRepo.Setup(repo => repo.Add(newSensor)).ReturnsAsync(true);
        //    mockedRepo.Setup(repo => repo.GetById(3)).ReturnsAsync(newSensor);

        //    var result = await controller.Post(newSensor);
        //    var contentResult = result as CreatedAtRouteResult;

        //    Assert.NotNull(contentResult);
        //    Assert.NotNull(contentResult.Value);
        //    Assert.Equal(contentResult.Value, newSensor);
        //}

        //[Fact]
        //public async Task Post_WhenCalled_BadObject_ReturnsBadRequest()
        //{
        //    var mockedRepo = new Mock<IRepository<Sensor>>();

        //    var controller = new SensorsController(mockedRepo.Object);

        //    var newSensor = new Sensor()
        //    {
        //        Name = "test_sensor_1",
        //        SensorId = 3
        //    };

        //    mockedRepo.Setup(repo => repo.Add(newSensor)).ReturnsAsync(false);

        //    var result = await controller.Post(newSensor);
        //    var contentResult = result as BadRequestResult;

        //    Assert.NotNull(contentResult);
        //}

        //[Fact]
        //public async Task Put_WhenCalled_ReturnsOk()
        //{
        //    var mockedRepo = new Mock<IRepository<Sensor>>();

        //    var controller = new SensorsController(mockedRepo.Object);

        //    var newSensor = new Sensor()
        //    {
        //        Name = "test_sensor_1",
        //        SensorId = 3
        //    };

        //    mockedRepo.Setup(repo => repo.Edit(newSensor)).ReturnsAsync(true);

        //    var result = await controller.Put(newSensor);
        //    var contentResult = result as OkObjectResult;

        //    Assert.NotNull(contentResult);
        //    Assert.NotNull(contentResult.Value);
        //    Assert.Equal(contentResult.Value, newSensor);
        //}

        //[Fact]
        //public async Task Put_WhenCalled_BadObject_ReturnsBadRequest()
        //{
        //    var mockedRepo = new Mock<IRepository<Sensor>>();

        //    var controller = new SensorsController(mockedRepo.Object);

        //    var newSensor = new Sensor()
        //    {
        //        Name = "test_sensor_1",
        //        SensorId = 3
        //    };

        //    mockedRepo.Setup(repo => repo.Edit(newSensor)).ReturnsAsync(false);

        //    var result = await controller.Put(newSensor);
        //    var contentResult = result as BadRequestResult;

        //    Assert.NotNull(contentResult);

        //}

        //[Fact]
        //public async Task Delete_WhenCalled_ReturnsOk()
        //{
        //    var mockedRepo = new Mock<IRepository<Sensor>>();

        //    var controller = new SensorsController(mockedRepo.Object);

        //    var newSensor = new Sensor()
        //    {
        //        Name = "test_sensor_1",
        //        SensorId = 3
        //    };

        //    mockedRepo.Setup(repo => repo.GetById(3)).ReturnsAsync(newSensor);
        //    mockedRepo.Setup(repo => repo.Delete(newSensor)).ReturnsAsync(true);

        //    var result = await controller.Delete(3);
        //    var contentResult = result as OkResult;

        //    Assert.NotNull(contentResult);
        //}

        //[Fact]
        //public async Task Delete_WhenCalled_BadObject_ReturnsBadRequest()
        //{
        //    var mockedRepo = new Mock<IRepository<Sensor>>();

        //    var controller = new SensorsController(mockedRepo.Object);

        //    var newSensor = new Sensor()
        //    {
        //        Name = "test_sensor_1",
        //        SensorId = 3
        //    };

        //    mockedRepo.Setup(repo => repo.GetById(3)).ReturnsAsync(newSensor);
        //    mockedRepo.Setup(repo => repo.Delete(newSensor)).ReturnsAsync(false);

        //    var result = await controller.Delete(3);
        //    var contentResult = result as BadRequestResult;

        //    Assert.NotNull(contentResult);
        //}

        //[Fact]
        //public async Task Delete_WhenCalled_UnknownObject_ReturnsNotFound()
        //{
        //    var mockedRepo = new Mock<IRepository<Sensor>>();

        //    var controller = new SensorsController(mockedRepo.Object);

        //    var newSensor = new Sensor()
        //    {
        //        Name = "test_sensor_1",
        //        SensorId = 3
        //    };

        //    mockedRepo.Setup(repo => repo.GetById(2)).ReturnsAsync((Sensor)null);

        //    var result = await controller.Delete(2);
        //    var contentResult = result as NotFoundResult;

        //    Assert.NotNull(contentResult);
        //}
    }
}
