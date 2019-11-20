using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Common.Repository;
using Moq;
using SensorService.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Microsoft.AspNetCore.Http;
using System;

namespace SensorServiceTests
{
    public class HomesControllerTest
    {
        [Fact]
        public async Task Get_WhenCalled_ReturnsOkResult()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var homes = new List<Home>
            {
                new Home()
                {
                    Name = "test_home_1",
                    Id = Guid.NewGuid()
                },
                new Home()
                {
                    Name = "test_home_2",
                    Id = Guid.NewGuid()
                }
            };

            mockedRepo.Setup(repo => repo.GetHomes()).ReturnsAsync(homes);

            var controller = new HomesController(mockedRepo.Object);
            var result = await controller.Get();
            var contentResult = (result as OkObjectResult).Value as IEnumerable<Home>;

            Assert.NotNull(contentResult);
            Assert.Equal(contentResult, homes);
        }

        [Fact]
        public async Task GetById_WhenCalled_ReturnsOkResult()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var homes = new List<Home>
            {
                new Home()
                {
                    Name = "test_home_1",
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001")
                },
                new Home()
                {
                    Name = "test_home_2",
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002")
                }
            };

            mockedRepo.Setup(repo => repo.GetHome(Guid.Parse("00000000-0000-0000-0000-000000000001"))).ReturnsAsync(homes[0]);
            mockedRepo.Setup(repo => repo.GetHome(Guid.Parse("00000000-0000-0000-0000-000000000002"))).ReturnsAsync(homes[1]);

            var controller = new HomesController(mockedRepo.Object);
            var result = await controller.Get("00000000-0000-0000-0000-000000000001");
            var contentResult = (result as OkObjectResult).Value as Home;

            Assert.NotNull(contentResult);
            Assert.Equal(contentResult, homes[0]);

            result = await controller.Get("00000000-0000-0000-0000-000000000002");
            contentResult = (result as OkObjectResult).Value as Home;

            Assert.NotNull(result);
            Assert.Equal(contentResult, homes[1]);
        }

        [Fact]
        public async Task GetById_WhenCalled_UnknownID_ReturnsNotFoundResult()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var homes = new List<Home>
            {
                new Home()
                {
                    Name = "test_home_1",
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001")
                },
                new Home()
                {
                    Name = "test_home_2",
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002")
                }
            };

            mockedRepo.Setup(repo => repo.GetHome(Guid.Parse("00000000-0000-0000-0000-000000000001"))).ReturnsAsync(homes[0]);
            mockedRepo.Setup(repo => repo.GetHome(Guid.Parse("00000000-0000-0000-0000-000000000002"))).ReturnsAsync(homes[1]);

            var controller = new HomesController(mockedRepo.Object);
            var result = await controller.Get("00000000-0000-0000-0000-000000000003");
            var contentResult = result as NotFoundResult;

            Assert.NotNull(contentResult);
        }

        [Fact]
        public async Task Put_WhenCalled_ReturnsOkResult()
        {
            var mockedRepo = new Mock<IHomeRepository>();
            var home = new Home()
            {
                Name = "test_home_1"
            };

            mockedRepo.Setup(repo => repo.EditHome(home)).ReturnsAsync(true);
            var controller = new HomesController(mockedRepo.Object);
            var result = await controller.Put(home);
            var contentResult = (result as OkObjectResult).Value;

            Assert.NotNull(contentResult);
            Assert.Equal(contentResult, home);
        }

        [Fact]
        public async Task Put_WhenCalled_BadObject_ReturnsBadRequest()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var controller = new HomesController(mockedRepo.Object);

            var newHome = new Home()
            {
                Name = "test_home_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003")
            };

            mockedRepo.Setup(repo => repo.EditHome(newHome)).ReturnsAsync(false);

            var result = await controller.Put(newHome);
            var contentResult = result as BadRequestResult;

            Assert.NotNull(contentResult);
        }

        [Fact]
        public async Task Delete_WhenCalled_ReturnsOk()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var controller = new HomesController(mockedRepo.Object);

            var newHome = new Home()
            {
                Name = "test_home_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003")
            };

            mockedRepo.Setup(repo => repo.GetHome(Guid.Parse("00000000-0000-0000-0000-000000000003"))).ReturnsAsync(newHome);
            mockedRepo.Setup(repo => repo.DeleteHome(Guid.Parse("00000000-0000-0000-0000-000000000003"))).ReturnsAsync(true);

            var result = await controller.Delete("00000000-0000-0000-0000-000000000003");
            var contentResult = (result as OkObjectResult).Value;

            Assert.NotNull(contentResult);
            Assert.Equal(newHome, contentResult);
        }

        [Fact]
        public async Task Delete_WhenCalled_UnknownObject_ReturnsNotFound()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var controller = new HomesController(mockedRepo.Object);

            var newHome = new Home()
            {
                Name = "test_home_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003")
            };

            mockedRepo.Setup(repo => repo.GetHome(Guid.Parse("00000000-0000-0000-0000-000000000002"))).ReturnsAsync((Home)null);

            var result = await controller.Delete("00000000-0000-0000-0000-000000000002");
            var contentResult = result as NotFoundResult;

            Assert.NotNull(contentResult);
        }

        [Fact]
        public async Task Delete_WhenCalled_DeleteFailed_ReturnsServerError()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var controller = new HomesController(mockedRepo.Object);

            var newHome = new Home()
            {
                Name = "test_home_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003")
            };

            mockedRepo.Setup(repo => repo.GetHome(Guid.Parse("00000000-0000-0000-0000-000000000003"))).ReturnsAsync(newHome);
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

            var controller = new HomesController(mockedRepo.Object);

            var newHome = new Home()
            {
                Name = "test_home_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003")
            };

            mockedRepo.Setup(repo => repo.AddHome(newHome)).ReturnsAsync(newHome);

            var result = await controller.Post(newHome);
            var contentResult = (result as OkObjectResult).Value;

            Assert.NotNull(contentResult);
            Assert.Equal(newHome, contentResult);
        }

        [Fact]
        public async Task Post_WhenCalled_AddFaild_ReturnsServerError()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var controller = new HomesController(mockedRepo.Object);

            var newHome = new Home()
            {
                Name = "test_home_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003")
            };

            mockedRepo.Setup(repo => repo.AddHome(newHome)).ReturnsAsync((Home)null);

            var result = await controller.Post(newHome);
            var contentResult = (result as StatusCodeResult).StatusCode;

            Assert.Equal(StatusCodes.Status500InternalServerError, contentResult);
        }

        [Fact]
        public async Task GetRooms_WhenCalled_ReturnsOk()
        {
            var mockedRepo = new Mock<IHomeRepository>();
            var controller = new HomesController(mockedRepo.Object);

            var newHome = new Home()
            {
                Name = "test_home_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003")
            };

            var rooms = new List<Room>()
            {
                new Room()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000000")
                }
            };

            mockedRepo.Setup(repo => repo.GetHome(Guid.Parse("00000000-0000-0000-0000-000000000003"))).ReturnsAsync(newHome);
            mockedRepo.Setup(repo => repo.GetRooms(newHome)).ReturnsAsync(rooms);

            var result = await controller.GetRooms("00000000-0000-0000-0000-000000000003");
            var contentResult = (result as OkObjectResult).Value;

            Assert.NotNull(contentResult);
            Assert.Equal(rooms, contentResult);
        }

        [Fact]
        public async Task GetRooms_WhenCalled_UnknownHome_ReturnsNotFound()
        {
            var mockedRepo = new Mock<IHomeRepository>();
            var controller = new HomesController(mockedRepo.Object);

            var newHome = new Home()
            {
                Name = "test_home_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003")
            };

            var rooms = new List<Room>()
            {
                new Room()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000000")
                }
            };

            mockedRepo.Setup(repo => repo.GetHome(Guid.Parse("00000000-0000-0000-0000-000000000001"))).ReturnsAsync((Home)null);
            mockedRepo.Setup(repo => repo.GetRooms(newHome)).ReturnsAsync((IEnumerable<Room>)null);

            var result = await controller.GetRooms("00000000-0000-0000-0000-000000000001");
            var contentResult = result as NotFoundResult;

            Assert.NotNull(contentResult);
        }

        [Fact]
        public async Task AddRoom_WhenCalled_ReturnsOk()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var controller = new HomesController(mockedRepo.Object);

            var newHome = new Home()
            {
                Name = "test_home_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003")
            };

            var room = new Room()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000000")
            };

            mockedRepo.Setup(repo => repo.GetHome(Guid.Parse("00000000-0000-0000-0000-000000000003"))).ReturnsAsync(newHome);
            mockedRepo.Setup(repo => repo.AddRoom(Guid.Parse("00000000-0000-0000-0000-000000000003"), room)).ReturnsAsync(room);

            var result = await controller.AddRoom("00000000-0000-0000-0000-000000000003", room);
            var contentResult = (result as OkObjectResult).Value;

            Assert.NotNull(contentResult);
            Assert.Equal(room, contentResult);
        }

        [Fact]
        public async Task AddRoom_WhenCalled_UnknownHome_ReturnsNotFound()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var controller = new HomesController(mockedRepo.Object);

            var newHome = new Home()
            {
                Name = "test_home_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003")
            };

            var room = new Room()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000000")
            };

            mockedRepo.Setup(repo => repo.GetHome(Guid.Parse("00000000-0000-0000-0000-000000000003"))).ReturnsAsync((Home)null);
            mockedRepo.Setup(repo => repo.AddRoom(Guid.Parse("00000000-0000-0000-0000-000000000003"), room)).ReturnsAsync((Room)null);

            var result = await controller.AddRoom("00000000-0000-0000-0000-000000000003", room);
            var contentResult = result as NotFoundResult;

            Assert.NotNull(contentResult);
        }

        [Fact]
        public async Task AddRoom_WhenCalled_AddFaild_ReturnsServerError()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var controller = new HomesController(mockedRepo.Object);

            var newHome = new Home()
            {
                Name = "test_home_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003")
            };

            var room = new Room()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000000")
            };

            mockedRepo.Setup(repo => repo.GetHome(Guid.Parse("00000000-0000-0000-0000-000000000003"))).ReturnsAsync(newHome);
            mockedRepo.Setup(repo => repo.AddRoom(Guid.Parse("00000000-0000-0000-0000-000000000003"), room)).ReturnsAsync((Room)null);

            var result = await controller.AddRoom("00000000-0000-0000-0000-000000000003", room);
            var contentResult = (result as StatusCodeResult).StatusCode;

            Assert.Equal(StatusCodes.Status500InternalServerError, contentResult);
        }

        [Fact]
        public async Task AddWeather_WhenCalled_ReturnsOk()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var controller = new HomesController(mockedRepo.Object);

            var newHome = new Home()
            {
                Name = "test_home_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003")
            };

            var weather = new Weather()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000000")
            };

            mockedRepo.Setup(repo => repo.GetHome(Guid.Parse("00000000-0000-0000-0000-000000000003"))).ReturnsAsync(newHome);
            mockedRepo.Setup(repo => repo.AddWeather(Guid.Parse("00000000-0000-0000-0000-000000000003"), weather)).ReturnsAsync(weather);

            var result = await controller.AddWeather("00000000-0000-0000-0000-000000000003", weather);
            var contentResult = (result as OkObjectResult).Value;

            Assert.NotNull(contentResult);
            Assert.Equal(weather, contentResult);
        }

        [Fact]
        public async Task AddWeather_WhenCalled_UnknownHome_ReturnsOk()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var controller = new HomesController(mockedRepo.Object);

            var newHome = new Home()
            {
                Name = "test_home_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003")
            };

            var weather = new Weather()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000000")
            };

            mockedRepo.Setup(repo => repo.GetHome(Guid.Parse("00000000-0000-0000-0000-000000000003"))).ReturnsAsync((Home)null);
            mockedRepo.Setup(repo => repo.AddWeather(Guid.Parse("00000000-0000-0000-0000-000000000003"), weather)).ReturnsAsync((Weather)null);

            var result = await controller.AddWeather("00000000-0000-0000-0000-000000000003", weather);
            var contentResult = result as NotFoundResult;

            Assert.NotNull(contentResult);
        }

        [Fact]
        public async Task AddWeather_WhenCalled_AddFaild_ReturnsServerError()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var controller = new HomesController(mockedRepo.Object);

            var newHome = new Home()
            {
                Name = "test_home_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003")
            };

            var weather = new Weather()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000000")
            };

            mockedRepo.Setup(repo => repo.GetHome(Guid.Parse("00000000-0000-0000-0000-000000000003"))).ReturnsAsync(newHome);
            mockedRepo.Setup(repo => repo.AddWeather(Guid.Parse("00000000-0000-0000-0000-000000000003"), weather)).ReturnsAsync((Weather)null);

            var result = await controller.AddWeather("00000000-0000-0000-0000-000000000003", weather);
            var contentResult = (result as StatusCodeResult).StatusCode;

            Assert.Equal(StatusCodes.Status500InternalServerError, contentResult);
        }

        [Fact]
        public async Task GetWeather_WhenCalled_ReturnsOk()
        {
            var mockedRepo = new Mock<IHomeRepository>();
            var weather = new List<Weather>()
            {
                new Weather()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000000")
                },
                new Weather()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001")
                }
            };

            var home = new Home()
            {
                Name = "test_home_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001")
            };

            var startDate = DateTime.UtcNow.Subtract(new TimeSpan(1, 0, 0, 0));
            var endDate = DateTime.UtcNow;

            mockedRepo.Setup(repo => repo.GetHome(Guid.Parse("00000000-0000-0000-0000-000000000001"))).ReturnsAsync(home);
            mockedRepo.Setup(repo => repo.GetWeather(Guid.Parse("00000000-0000-0000-0000-000000000001"), startDate, endDate)).ReturnsAsync(weather);

            var controller = new HomesController(mockedRepo.Object);
            var result = await controller.GetWeather("00000000-0000-0000-0000-000000000001", startDate, endDate);
            var contentResult = (result as OkObjectResult).Value;

            Assert.NotNull(contentResult);
            Assert.Equal(weather, contentResult);
        }

        [Fact]
        public async Task GetWeather_WhenCalled_UnknownRoom_ReturnsNotFound()
        {
            var mockedRepo = new Mock<IHomeRepository>();
            var home = new Home()
            {
                Name = "test_home_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001")
            };

            var startDate = DateTime.UtcNow.Subtract(new TimeSpan(1, 0, 0, 0));
            var endDate = DateTime.UtcNow;

            mockedRepo.Setup(repo => repo.GetHome(Guid.Parse("00000000-0000-0000-0000-000000000001"))).ReturnsAsync((Home)null);
            mockedRepo.Setup(repo => repo.GetWeather(Guid.Parse("00000000-0000-0000-0000-000000000001"), startDate, endDate)).ReturnsAsync((IEnumerable<Weather>)null);

            var controller = new HomesController(mockedRepo.Object);
            var result = await controller.GetWeather("00000000-0000-0000-0000-000000000001", startDate, endDate);
            var contentResult = result as NotFoundResult;

            Assert.NotNull(contentResult);
        }

        [Fact]
        public async Task GetWeather_WhenCalled_MissingQueryParameters_ReturnsBadRequest()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var home = new Home()
            {
                Name = "test_home_1",
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001")
            };

            var startDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var endDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            mockedRepo.Setup(repo => repo.GetHome(Guid.Parse("00000000-0000-0000-0000-000000000001"))).ReturnsAsync(home);
            mockedRepo.Setup(repo => repo.GetWeather(Guid.Parse("00000000-0000-0000-0000-000000000001"), startDate, endDate)).ReturnsAsync((List<Weather>)null);

            var controller = new HomesController(mockedRepo.Object);
            var result = await controller.GetWeather("00000000-0000-0000-0000-000000000001", startDate, endDate);
            var contentResult = result as BadRequestResult;

            Assert.NotNull(contentResult);
        }
    }
}
