using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Repository;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SensorService.Controllers;
using Xunit;
using Environment = Domain.Entities.Environment;

namespace SensorServiceTests
{
    public class RoomsControllerTest
    {
        [Fact]
        public async Task GetById_WhenCalled_ReturnsOk()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var rooms = new List<Room>
            {
                new Room()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001")
                }
            };

            mockedRepo.Setup(repo => repo.GetRoom(Guid.Parse("00000000-0000-0000-0000-000000000001"))).ReturnsAsync(rooms[0]);

            var controller = new RoomsController(mockedRepo.Object);
            var result = await controller.Get("00000000-0000-0000-0000-000000000001");
            var contentResult = (result as OkObjectResult).Value as Room;

            Assert.NotNull(contentResult);
            Assert.Equal(contentResult, rooms[0]);
        }

        [Fact]
        public async Task GetById_WhenCalled_UnknownId_ReturnsNotFound()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var rooms = new List<Room>
            {
                new Room()
                {
                    Id = Guid.Empty
                }
            };

            mockedRepo.Setup(repo => repo.GetRoom(Guid.Parse("00000000-0000-0000-0000-000000000001"))).ReturnsAsync(rooms[0]);

            var controller = new RoomsController(mockedRepo.Object);
            var result = await controller.Get("00000000-0000-0000-0000-000000000002");
            var contentResult = result as NotFoundResult;

            Assert.NotNull(contentResult);
        }

        [Fact]
        public async Task Put_WhenCalled_ReturnsOk()
        {
            var mockedRepo = new Mock<IHomeRepository>();
            var room = new Room()
            {
                Name = "test_room_1"
            };

            mockedRepo.Setup(repo => repo.EditRoom(room)).ReturnsAsync(true);
            var controller = new RoomsController(mockedRepo.Object);
            var result = await controller.Put(room);
            var contentResult = (result as OkObjectResult).Value;

            Assert.NotNull(contentResult);
            Assert.Equal(contentResult, room);
        }

        [Fact]
        public async Task Put_WhenCalled_BadObject_ReturnsBadRequest()
        {
            var mockedRepo = new Mock<IHomeRepository>();
            var room = new Room()
            {
                Name = "test_room_1"
            };

            mockedRepo.Setup(repo => repo.EditRoom(room)).ReturnsAsync(false);
            var controller = new RoomsController(mockedRepo.Object);
            var result = await controller.Put(room);
            var contentResult = result as BadRequestResult;

            Assert.NotNull(contentResult);
        }

        [Fact]
        public async Task GetBySensor_WhenCalled_ReturnsOk()
        {
            var mockedRepo = new Mock<IHomeRepository>();
            var room = new Room()
            {
                Name = "test_room_1"
            };
            mockedRepo.Setup(repo => repo.GetRoomBySensorId(Guid.Parse("00000000-0000-0000-0000-000000000001"))).ReturnsAsync(room);

            var controller = new RoomsController(mockedRepo.Object);
            var result = await controller.GetBySensor("00000000-0000-0000-0000-000000000001");
            var contentResult = (result as OkObjectResult).Value;

            Assert.NotNull(contentResult);
            Assert.Equal(contentResult, room);
        }

        [Fact]
        public async Task GetBySensor_WhenCalled_UnknownSensor_ReturnsNotFound()
        {
            var mockedRepo = new Mock<IHomeRepository>();
            var room = new Room()
            {
                Name = "test_room_1"
            };
            mockedRepo.Setup(repo => repo.GetRoomBySensorId(Guid.Parse("00000000-0000-0000-0000-000000000001"))).ReturnsAsync((Room)null);

            var controller = new RoomsController(mockedRepo.Object);
            var result = await controller.GetBySensor("00000000-0000-0000-0000-000000000001");
            var contentResult = result as NotFoundResult;

            Assert.NotNull(contentResult);
        }

        [Fact]
        public async Task Delete_WhenCalled_ReturnsOk()
        {
            var mockedRepo = new Mock<IHomeRepository>();
            var room = new Room()
            {
                Name = "test_room_1"
            };
            mockedRepo.Setup(repo => repo.GetRoom(Guid.Parse("00000000-0000-0000-0000-000000000001"))).ReturnsAsync(room);
            mockedRepo.Setup(repo => repo.DeleteRoom(Guid.Parse("00000000-0000-0000-0000-000000000001"))).ReturnsAsync(true);

            var controller = new RoomsController(mockedRepo.Object);
            var result = await controller.Delete("00000000-0000-0000-0000-000000000001");
            var contentResult = (result as OkObjectResult).Value;

            Assert.NotNull(contentResult);
            Assert.Equal(contentResult, room);
        }

        [Fact]
        public async Task Delete_WhenCalled_UnkownObject_ReturnsNotFound()
        {
            var mockedRepo = new Mock<IHomeRepository>();
            var room = new Room()
            {
                Name = "test_room_1"
            };
            mockedRepo.Setup(repo => repo.GetRoom(Guid.Parse("00000000-0000-0000-0000-000000000001"))).ReturnsAsync((Room)null);
            mockedRepo.Setup(repo => repo.DeleteRoom(Guid.Parse("00000000-0000-0000-0000-000000000001"))).ReturnsAsync(false);

            var controller = new RoomsController(mockedRepo.Object);
            var result = await controller.Delete("00000000-0000-0000-0000-000000000001");
            var contentResult = result as NotFoundResult;

            Assert.NotNull(contentResult);
        }

        [Fact]
        public async Task Delete_WhenCalled_DeleteFailed_ReturnsServerError()
        {
            var mockedRepo = new Mock<IHomeRepository>();
            var room = new Room()
            {
                Name = "test_room_1"
            };
            mockedRepo.Setup(repo => repo.GetRoom(Guid.Parse("00000000-0000-0000-0000-000000000001"))).ReturnsAsync(room);
            mockedRepo.Setup(repo => repo.DeleteRoom(Guid.Parse("00000000-0000-0000-0000-000000000001"))).ReturnsAsync(false);

            var controller = new RoomsController(mockedRepo.Object);
            var result = await controller.Delete("00000000-0000-0000-0000-000000000001");
            var contentResult = (result as StatusCodeResult).StatusCode;

            Assert.Equal(StatusCodes.Status500InternalServerError, contentResult);
        }

        [Fact]
        public async Task GetSensors_WhenCalled_ReturnsOk()
        {
            var mockedRepo = new Mock<IHomeRepository>();
            var sensors = new List<Sensor>()
            {
                new Sensor()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000000")
                },
                new Sensor()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001")
                }
            };
            var room = new Room()
            {
                Name = "test_room_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001")
            };
            mockedRepo.Setup(repo => repo.GetRoom(Guid.Parse("00000000-0000-0000-0000-000000000001"))).ReturnsAsync(room);
            mockedRepo.Setup(repo => repo.GetSensors(room)).ReturnsAsync(sensors);

            var controller = new RoomsController(mockedRepo.Object);
            var result = await controller.GetSensors("00000000-0000-0000-0000-000000000001");
            var contentResult = (result as OkObjectResult).Value;

            Assert.NotNull(contentResult);
            Assert.Equal(sensors, contentResult);
        }

        [Fact]
        public async Task GetSensors_WhenCalled_UnknownRoom_ReturnsNotFound()
        {
            var mockedRepo = new Mock<IHomeRepository>();
            var sensors = new List<Sensor>()
            {
                new Sensor()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000000")
                },
                new Sensor()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001")
                }
            };
            var room = new Room()
            {
                Name = "test_room_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001")
            };
            mockedRepo.Setup(repo => repo.GetRoom(Guid.Parse("00000000-0000-0000-0000-000000000002"))).ReturnsAsync((Room)null);
            mockedRepo.Setup(repo => repo.GetSensors(room)).ReturnsAsync(sensors);

            var controller = new RoomsController(mockedRepo.Object);
            var result = await controller.GetSensors("00000000-0000-0000-0000-000000000002");
            var contentResult = result as NotFoundResult;

            Assert.NotNull(contentResult);
        }

        [Fact]
        public async Task PostSensor_WhenCalled_ReturnsOk()
        {
            var mockedRepo = new Mock<IHomeRepository>();
            var sensor = new Sensor()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000000")
            };

            var room = new Room()
            {
                Name = "test_room_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001")
            };
            mockedRepo.Setup(repo => repo.GetRoom(Guid.Parse("00000000-0000-0000-0000-000000000001"))).ReturnsAsync(room);
            mockedRepo.Setup(repo => repo.AddSensor(Guid.Parse("00000000-0000-0000-0000-000000000001"), sensor)).ReturnsAsync(sensor);

            var controller = new RoomsController(mockedRepo.Object);
            var result = await controller.PostSensor("00000000-0000-0000-0000-000000000001", sensor);
            var contentResult = (result as OkObjectResult).Value;

            Assert.NotNull(contentResult);
            Assert.Equal(sensor, contentResult);
        }

        [Fact]
        public async Task PostSensor_WhenCalled_UnknownRoom_ReturnsNotFound()
        {
            var mockedRepo = new Mock<IHomeRepository>();
            var sensor = new Sensor()
            {
                Id = Guid.Empty
            };

            var room = new Room()
            {
                Name = "test_room_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001")
            };
            mockedRepo.Setup(repo => repo.GetRoom(Guid.Parse("00000000-0000-0000-0000-000000000002"))).ReturnsAsync((Room)null);
            mockedRepo.Setup(repo => repo.AddSensor(Guid.Parse("00000000-0000-0000-0000-000000000002"), sensor)).ReturnsAsync((Sensor)null);

            var controller = new RoomsController(mockedRepo.Object);
            var result = await controller.PostSensor("00000000-0000-0000-0000-000000000002", sensor);
            var contentResult = result as NotFoundResult;

            Assert.NotNull(contentResult);
        }

        [Fact]
        public async Task PostSensor_WhenCalled_InsertFailed_ReturnsServerError()
        {
            var mockedRepo = new Mock<IHomeRepository>();
            var sensor = new Sensor()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000000")
            };

            var room = new Room()
            {
                Name = "test_room_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001")
            };
            mockedRepo.Setup(repo => repo.GetRoom(Guid.Parse("00000000-0000-0000-0000-000000000001"))).ReturnsAsync(room);
            mockedRepo.Setup(repo => repo.AddSensor(Guid.Parse("00000000-0000-0000-0000-000000000001"), sensor)).ReturnsAsync((Sensor)null);

            var controller = new RoomsController(mockedRepo.Object);
            var result = await controller.PostSensor("00000000-0000-0000-0000-000000000001", sensor);
            var contentResult = (result as StatusCodeResult).StatusCode;

            Assert.Equal(StatusCodes.Status500InternalServerError, contentResult);
        }

        [Fact]
        public async Task GetEnvironment_WhenCalled_ReturnsOk()
        {
            var mockedRepo = new Mock<IHomeRepository>();
            var environment = new List<Environment>()
            {
                new Environment()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000000")
                },
                new Environment()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001")
                }
            };

            var room = new Room()
            {
                Name = "test_room_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001")
            };

            var startDate = DateTime.UtcNow.Subtract(new TimeSpan(1, 0, 0, 0));
            var endDate = DateTime.UtcNow;

            mockedRepo.Setup(repo => repo.GetRoom(Guid.Parse("00000000-0000-0000-0000-000000000001"))).ReturnsAsync(room);
            mockedRepo.Setup(repo => repo.GetEnvironmentReadings(Guid.Parse("00000000-0000-0000-0000-000000000001"), startDate, endDate)).ReturnsAsync(environment);

            var controller = new RoomsController(mockedRepo.Object);
            var result = await controller.GetEnvironment("00000000-0000-0000-0000-000000000001", startDate, endDate);
            var contentResult = (result as OkObjectResult).Value;

            Assert.NotNull(contentResult);
            Assert.Equal(environment, contentResult);
        }

        [Fact]
        public async Task GetEnvironment_WhenCalled_UnknownRoom_ReturnsNotFound()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var room = new Room()
            {
                Name = "test_room_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001")
            };

            var startDate = DateTime.UtcNow.Subtract(new TimeSpan(1, 0, 0, 0));
            var endDate = DateTime.UtcNow;

            mockedRepo.Setup(repo => repo.GetRoom(Guid.Parse("00000000-0000-0000-0000-000000000001"))).ReturnsAsync((Room)null);
            mockedRepo.Setup(repo => repo.GetEnvironmentReadings(Guid.Parse("00000000-0000-0000-0000-000000000001"), startDate, endDate)).ReturnsAsync((List<Environment>)null);

            var controller = new RoomsController(mockedRepo.Object);
            var result = await controller.GetEnvironment("00000000-0000-0000-0000-000000000001", startDate, endDate);
            var contentResult = result as NotFoundResult;

            Assert.NotNull(contentResult);
        }

        [Fact]
        public async Task GetEnvironment_WhenCalled_MissingQueryParameters_ReturnsBadRequest()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var room = new Room()
            {
                Name = "test_room_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001")
            };

            var startDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var endDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            mockedRepo.Setup(repo => repo.GetRoom(Guid.Parse("00000000-0000-0000-0000-000000000001"))).ReturnsAsync(room);
            mockedRepo.Setup(repo => repo.GetEnvironmentReadings(Guid.Parse("00000000-0000-0000-0000-000000000001"), startDate, endDate)).ReturnsAsync((List<Environment>)null);

            var controller = new RoomsController(mockedRepo.Object);
            var result = await controller.GetEnvironment("00000000-0000-0000-0000-000000000001", startDate, endDate);
            var contentResult = result as BadRequestResult;

            Assert.NotNull(contentResult);
        }

        [Fact]
        public async Task PostEnvironment_WhenCalled_ReturnsOk()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var room = new Room()
            {
                Name = "test_room_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001")
            };

            var sensor = new Sensor()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002")
            };

            var environment = new Environment()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000000")
            };

            mockedRepo.Setup(repo => repo.GetRoom(Guid.Parse("00000000-0000-0000-0000-000000000001"))).ReturnsAsync(room);
            mockedRepo.Setup(repo => repo.GetSensor(Guid.Parse("00000000-0000-0000-0000-000000000002"))).ReturnsAsync(sensor);
            mockedRepo.Setup(repo => repo.AddEnvironmentReading(Guid.Parse("00000000-0000-0000-0000-000000000001"), Guid.Parse("00000000-0000-0000-0000-000000000002"), environment)).ReturnsAsync(environment);

            var controller = new RoomsController(mockedRepo.Object);
            var result = await controller.PostEnvironment("00000000-0000-0000-0000-000000000001", "00000000-0000-0000-0000-000000000002", environment);
            var contentResult = (result as OkObjectResult).Value;

            Assert.NotNull(contentResult);
            Assert.Equal(environment, contentResult);
        }

        [Fact]
        public async Task PostEnvironment_WhenCalled_UnknownRoom_ReturnsNotFound()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var room = new Room()
            {
                Name = "test_room_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001")
            };

            var sensor = new Sensor()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002")
            };

            var environment = new Environment()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000000")
            };

            mockedRepo.Setup(repo => repo.GetRoom(Guid.Parse("00000000-0000-0000-0000-000000000001"))).ReturnsAsync((Room)null);
            mockedRepo.Setup(repo => repo.GetSensor(Guid.Parse("00000000-0000-0000-0000-000000000002"))).ReturnsAsync(sensor);
            mockedRepo.Setup(repo => repo.AddEnvironmentReading(Guid.Parse("00000000-0000-0000-0000-000000000001"), Guid.Parse("00000000-0000-0000-0000-000000000002"), environment)).ReturnsAsync((Environment)null);

            var controller = new RoomsController(mockedRepo.Object);
            var result = await controller.PostEnvironment("00000000-0000-0000-0000-000000000001", "00000000-0000-0000-0000-000000000002", environment);
            var contentResult = result as NotFoundResult;

            Assert.NotNull(contentResult);
        }

        [Fact]
        public async Task PostEnvironment_WhenCalled_UnknownSensor_ReturnsNotFound()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var room = new Room()
            {
                Name = "test_room_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001")
            };

            var sensor = new Sensor()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002")
            };

            var environment = new Environment()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000000")
            };

            mockedRepo.Setup(repo => repo.GetRoom(Guid.Parse("00000000-0000-0000-0000-000000000001"))).ReturnsAsync(room);
            mockedRepo.Setup(repo => repo.GetSensor(Guid.Parse("00000000-0000-0000-0000-000000000002"))).ReturnsAsync((Sensor)null);
            mockedRepo.Setup(repo => repo.AddEnvironmentReading(Guid.Parse("00000000-0000-0000-0000-000000000001"), Guid.Parse("00000000-0000-0000-0000-000000000002"), environment)).ReturnsAsync((Environment)null);

            var controller = new RoomsController(mockedRepo.Object);
            var result = await controller.PostEnvironment("00000000-0000-0000-0000-000000000001", "00000000-0000-0000-0000-000000000002", environment);
            var contentResult = result as NotFoundResult;

            Assert.NotNull(contentResult);
        }

        [Fact]
        public async Task PostEnvironment_WhenCalled_InsertFailed_ReturnsServerError()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var room = new Room()
            {
                Name = "test_room_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001")
            };

            var sensor = new Sensor()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002")
            };

            var environment = new Environment()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000000")
            };

            mockedRepo.Setup(repo => repo.GetRoom(Guid.Parse("00000000-0000-0000-0000-000000000001"))).ReturnsAsync(room);
            mockedRepo.Setup(repo => repo.GetSensor(Guid.Parse("00000000-0000-0000-0000-000000000002"))).ReturnsAsync(sensor);
            mockedRepo.Setup(repo => repo.AddEnvironmentReading(Guid.Parse("00000000-0000-0000-0000-000000000001"), Guid.Parse("00000000-0000-0000-0000-000000000002"), environment)).ReturnsAsync((Environment)null);

            var controller = new RoomsController(mockedRepo.Object);
            var result = await controller.PostEnvironment("00000000-0000-0000-0000-000000000001", "00000000-0000-0000-0000-000000000002", environment);
            var contentResult = (result as StatusCodeResult).StatusCode;

            Assert.Equal(StatusCodes.Status500InternalServerError, contentResult);
        }
    }
}
