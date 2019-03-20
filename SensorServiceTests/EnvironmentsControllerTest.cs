using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Repository;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SensorService.Controllers;
using Xunit;

namespace SensorServiceTests
{
    public class EnvironmentsControllerTest
    {
        [Fact]
        public async Task Put_WhenCalled_ReturnsOkResult()
        {
            var mockedRepo = new Mock<IHomeRepository>();
            var environment = new Environment()
            {
                Id = 0
            };

            mockedRepo.Setup(repo => repo.EditEnvironment(environment)).ReturnsAsync(true);
            var controller = new EnvironmentsController(mockedRepo.Object);
            var result = await controller.Put(environment);
            var contentResult = (result as OkObjectResult).Value;

            Assert.NotNull(contentResult);
            Assert.Equal(contentResult, environment);
        }

        [Fact]
        public async Task GetById_WhenCalled_ReturnsOkResult()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var envs = new List<Environment>
            {
                new Environment()
                {
                    Id = 1
                },
                new Environment()
                {
                    Id = 2
                }
            };

            mockedRepo.Setup(repo => repo.GetEnvironment(1)).ReturnsAsync(envs[0]);
            mockedRepo.Setup(repo => repo.GetEnvironment(2)).ReturnsAsync(envs[1]);

            var controller = new EnvironmentsController(mockedRepo.Object);
            var result = await controller.Get(1);
            var contentResult = (result as OkObjectResult).Value as Environment;

            Assert.NotNull(contentResult);
            Assert.Equal(contentResult, envs[0]);

            result = await controller.Get(2);
            contentResult = (result as OkObjectResult).Value as Environment;

            Assert.NotNull(result);
            Assert.Equal(contentResult, envs[1]);
        }

        [Fact]
        public async Task GetById_WhenCalled_UnknownID_ReturnsNotFoundResult()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var envs = new List<Environment>
            {
                new Environment()
                {
                    Id = 1
                },
                new Environment()
                {
                    Id = 2
                }
            };

            mockedRepo.Setup(repo => repo.GetEnvironment(1)).ReturnsAsync(envs[0]);
            mockedRepo.Setup(repo => repo.GetEnvironment(2)).ReturnsAsync(envs[1]);
            mockedRepo.Setup(repo => repo.GetEnvironment(3)).ReturnsAsync((Environment)null);

            var controller = new EnvironmentsController(mockedRepo.Object);
            var result = await controller.Get(3);
            var contentResult = result as NotFoundResult;

            Assert.NotNull(contentResult);
        }

        [Fact]
        public async Task Put_WhenCalled_BadObject_ReturnsBadRequest()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var controller = new EnvironmentsController(mockedRepo.Object);

            var newEnv = new Environment()
            {
                Id = 3
            };

            mockedRepo.Setup(repo => repo.EditEnvironment(newEnv)).ReturnsAsync(false);

            var result = await controller.Put(newEnv);
            var contentResult = result as BadRequestResult;

            Assert.NotNull(contentResult);
        }

        [Fact]
        public async Task Delete_WhenCalled_ReturnsOk()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var controller = new EnvironmentsController(mockedRepo.Object);

            var newEnv = new Environment()
            {
                Id = 3
            };

            mockedRepo.Setup(repo => repo.GetEnvironment(3)).ReturnsAsync(newEnv);
            mockedRepo.Setup(repo => repo.DeleteEnvironment(3)).ReturnsAsync(true);

            var result = await controller.Delete(3);
            var contentResult = (result as OkObjectResult).Value;

            Assert.NotNull(contentResult);
            Assert.Equal(newEnv, contentResult);
        }

        [Fact]
        public async Task Delete_WhenCalled_UnknownObject_ReturnsNotFound()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var controller = new EnvironmentsController(mockedRepo.Object);

            var newEnv = new Environment()
            {
                Id = 3
            };

            mockedRepo.Setup(repo => repo.GetEnvironment(2)).ReturnsAsync((Environment)null);

            var result = await controller.Delete(2);
            var contentResult = result as NotFoundResult;

            Assert.NotNull(contentResult);
        }

        [Fact]
        public async Task Delete_WhenCalled_DeleteFailed_ReturnsServerError()
        {
            var mockedRepo = new Mock<IHomeRepository>();

            var controller = new EnvironmentsController(mockedRepo.Object);

            var newEnv = new Environment()
            {
                Id = 3
            };

            mockedRepo.Setup(repo => repo.GetEnvironment(3)).ReturnsAsync(newEnv);
            mockedRepo.Setup(repo => repo.DeleteEnvironment(3)).ReturnsAsync(false);

            var result = await controller.Delete(3);
            var contentResult = result as StatusCodeResult;

            Assert.NotNull(contentResult);
            Assert.Equal(StatusCodes.Status500InternalServerError, contentResult.StatusCode);
        }
    }
}
