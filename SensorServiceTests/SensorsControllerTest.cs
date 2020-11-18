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
using Microsoft.Extensions.Configuration;
using Common.SensorListenerAPI;

namespace SensorServiceTests
{
    public class SensorsControllerTest
    {
        private readonly ISensorListenerAPI listenerClient;
        private readonly Mock<ISensorListenerAPI> listenerMock;
        public SensorsControllerTest()
        {
            var mockedSensorListenerClient = new Mock<ISensorListenerAPI>();
            mockedSensorListenerClient.Setup(listener => listener.NotifySensorUpdate(It.IsAny<Sensor>())).ReturnsAsync(new Sensor());

            this.listenerMock = mockedSensorListenerClient;
            this.listenerClient = mockedSensorListenerClient.Object;
        }

        [Fact]
        public async Task Put_WhenCalled_ReturnsOkResult()
        {
            var mockedRepo = new Mock<IHomeRepository>();
            var sensor = new Sensor()
            {
                Name = "test_sensor_1"
            };

            var mockedSensorListenerClient = new Mock<ISensorListenerAPI>();
            mockedSensorListenerClient.Setup(listener => listener.NotifySensorUpdate(sensor)).ReturnsAsync(sensor);

            mockedRepo.Setup(repo => repo.EditSensor(sensor)).ReturnsAsync(true);
            var controller = new SensorsController(mockedRepo.Object, mockedSensorListenerClient.Object);
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

            var controller = new SensorsController(mockedRepo.Object, listenerClient);
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

            var controller = new SensorsController(mockedRepo.Object, listenerClient);
            var result = await controller.Get("00000000-0000-0000-0000-000000000003");
            var contentResult = result as NotFoundResult;

            Assert.NotNull(contentResult);
        }

        [Fact]
        public async Task Put_WhenCalled_BadObject_ReturnsBadRequest()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var controller = new SensorsController(mockedRepo.Object, listenerClient);

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
        public async Task GetAll_WhenCalled_ReturnsOk()
        {
            var mockedRepo = new Mock<IHomeRepository>();
            var controller = new SensorsController(mockedRepo.Object, listenerClient);
            var newSensor = new Sensor()
            {
                Name = "test_sensor_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003")
            };
            mockedRepo.Setup(r => r.GetSensors()).ReturnsAsync(new List<Sensor> { newSensor });

            var result = await controller.GetAll();
            var contentResult = (List<Sensor>)(result as OkObjectResult).Value;

            Assert.Single(contentResult);
            Assert.Equal(newSensor, contentResult[0]);
        }

        [Fact]
        public async Task Delete_WhenCalled_ReturnsOk()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var controller = new SensorsController(mockedRepo.Object, listenerClient);

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

            var controller = new SensorsController(mockedRepo.Object, listenerClient);

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

            var controller = new SensorsController(mockedRepo.Object, listenerClient);

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

            var controller = new SensorsController(mockedRepo.Object, listenerClient);

            var newSensor = new Sensor()
            {
                Name = "test_sensor_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                Type = "test_type",
                RoomId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            };

            mockedRepo.Setup(repo => repo.AddSensor(newSensor)).ReturnsAsync(newSensor);

            var result = await controller.Post(newSensor);
            var contentResult = (result as OkObjectResult).Value;

            this.listenerMock.Verify(client => client.NotifySensorUpdate(newSensor), Times.Once);
            
            Assert.NotNull(contentResult);
            Assert.Equal(newSensor, contentResult);
        }

        [Fact]
        public async Task Post_WhenCalled_AddFaild_ReturnsServerError()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var controller = new SensorsController(mockedRepo.Object, listenerClient);

            var newSensor = new Sensor()
            {
                Name = "test_sensor_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                Type = "test_type"
            };

            mockedRepo.Setup(repo => repo.AddSensor(newSensor)).ReturnsAsync((Sensor)null);

            var result = await controller.Post(newSensor);
            var contentResult = (result as StatusCodeResult).StatusCode;

            this.listenerMock.VerifyNoOtherCalls();
            Assert.Equal(StatusCodes.Status500InternalServerError, contentResult);
        }

        [Fact]
        public async Task GetHomeyMapping_WhenCalled_ReturnsOk()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var controller = new SensorsController(mockedRepo.Object, listenerClient);

            var newSensor = new Sensor()
            {
                Name = "test_sensor_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                Type = "test_type"
            };

            mockedRepo.Setup(repo => repo.GetSensor(newSensor.Id)).ReturnsAsync(newSensor);

            var newMapping = new HomeyMapping()
            {
                HumTopic = "humTopic",
                TempTopic = "tempTopic",
                MotionTopic = "motionTopic"
            };

            mockedRepo.Setup(repo => repo.GetHomeyMapping(newSensor)).ReturnsAsync(newMapping);

            var result = await controller.GetHomeyMapping(newSensor.Id.ToString());
            var contentResult = (result as OkObjectResult).Value;

            Assert.Equal(newMapping, contentResult);
        }

        [Fact]
        public async Task GetHomeyMapping_WhenCalled_WrongSensor_ReturnsNotFound()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var controller = new SensorsController(mockedRepo.Object, listenerClient);
            var sensorId = Guid.Parse("00000000-0000-0000-0000-000000000003");

            mockedRepo.Setup(repo => repo.GetSensor(sensorId)).ReturnsAsync((Sensor)null);

            var result = await controller.GetHomeyMapping(sensorId.ToString());
            var contentResult = result as NotFoundResult;

            Assert.NotNull(contentResult);
            mockedRepo.Verify(repo => repo.GetSensor(sensorId));
            mockedRepo.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task PostHomeyMapping_WhenCalled_ReturnsOk()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var controller = new SensorsController(mockedRepo.Object, listenerClient);

            var newSensor = new Sensor()
            {
                Name = "test_sensor_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                Type = "test_type"
            };

            var newMapping = new HomeyMapping()
            {
                HumTopic = "humTopic",
                TempTopic = "tempTopic",
                MotionTopic = "motionTopic"
            };

            mockedRepo.Setup(repo => repo.GetSensor(newSensor.Id)).ReturnsAsync(newSensor);
            mockedRepo.Setup(repo => repo.AddHomeyMapping(newSensor.Id, newMapping)).ReturnsAsync(newMapping);

            var result = await controller.PostHomeyMapping(newSensor.Id.ToString(), newMapping);
            var contentResult = (result as OkObjectResult).Value;

            Assert.Equal(newMapping, contentResult);
        }

        [Fact]
        public async Task PostHomeyMapping_WhenCalled_WrongSensor_ReturnsNotFound()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var controller = new SensorsController(mockedRepo.Object, listenerClient);

            var sensorId = Guid.Parse("00000000-0000-0000-0000-000000000003");

            var newMapping = new HomeyMapping()
            {
                HumTopic = "humTopic",
                TempTopic = "tempTopic",
                MotionTopic = "motionTopic"
            };

            mockedRepo.Setup(repo => repo.GetSensor(sensorId)).ReturnsAsync((Sensor)null);

            var result = await controller.PostHomeyMapping(sensorId.ToString(), newMapping);
            var contentResult = (result as NotFoundResult);

            Assert.NotNull(contentResult);
            mockedRepo.Verify(repo => repo.GetSensor(sensorId));
            mockedRepo.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task PostHomeyMapping_WhenCalled_AddFailed_Returns500()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var controller = new SensorsController(mockedRepo.Object, listenerClient);

            var newSensor = new Sensor()
            {
                Name = "test_sensor_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                Type = "test_type"
            };

            var newMapping = new HomeyMapping()
            {
                HumTopic = "humTopic",
                TempTopic = "tempTopic",
                MotionTopic = "motionTopic"
            };

            mockedRepo.Setup(repo => repo.GetSensor(newSensor.Id)).ReturnsAsync(newSensor);
            mockedRepo.Setup(repo => repo.AddHomeyMapping(newSensor.Id, newMapping)).ReturnsAsync((HomeyMapping)null);

            var result = await controller.PostHomeyMapping(newSensor.Id.ToString(), newMapping);
            var contentResult = (result as StatusCodeResult);

            Assert.Equal(StatusCodes.Status500InternalServerError, contentResult.StatusCode);
        }
    }
}
