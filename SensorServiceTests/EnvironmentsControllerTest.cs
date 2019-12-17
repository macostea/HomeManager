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
    public class EnvironmentsControllerTest
    {
        [Fact]
        public async Task Put_WhenCalled_ReturnsOkResult()
        {
            var mockedRepo = new Mock<IHomeRepository>();
            var environment = new Environment()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000000")
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
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001")
                },
                new Environment()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002")
                }
            };

            mockedRepo.Setup(repo => repo.GetEnvironment(Guid.Parse("00000000-0000-0000-0000-000000000001"))).ReturnsAsync(envs[0]);
            mockedRepo.Setup(repo => repo.GetEnvironment(Guid.Parse("00000000-0000-0000-0000-000000000002"))).ReturnsAsync(envs[1]);

            var controller = new EnvironmentsController(mockedRepo.Object);
            var result = await controller.Get("00000000-0000-0000-0000-000000000001");
            var contentResult = (result as OkObjectResult).Value as Environment;

            Assert.NotNull(contentResult);
            Assert.Equal(contentResult, envs[0]);

            result = await controller.Get("00000000-0000-0000-0000-000000000002");
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
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001")
                },
                new Environment()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002")
                }
            };

            mockedRepo.Setup(repo => repo.GetEnvironment(Guid.Parse("00000000-0000-0000-0000-000000000001"))).ReturnsAsync(envs[0]);
            mockedRepo.Setup(repo => repo.GetEnvironment(Guid.Parse("00000000-0000-0000-0000-000000000002"))).ReturnsAsync(envs[1]);
            mockedRepo.Setup(repo => repo.GetEnvironment(Guid.Parse("00000000-0000-0000-0000-000000000003"))).ReturnsAsync((Environment)null);

            var controller = new EnvironmentsController(mockedRepo.Object);
            var result = await controller.Get("00000000-0000-0000-0000-000000000003");
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
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003")
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
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003")
            };

            mockedRepo.Setup(repo => repo.GetEnvironment(Guid.Parse("00000000-0000-0000-0000-000000000003"))).ReturnsAsync(newEnv);
            mockedRepo.Setup(repo => repo.DeleteEnvironment(Guid.Parse("00000000-0000-0000-0000-000000000003"))).ReturnsAsync(true);

            var result = await controller.Delete("00000000-0000-0000-0000-000000000003");
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
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003")
            };

            mockedRepo.Setup(repo => repo.GetEnvironment(Guid.Parse("00000000-0000-0000-0000-000000000002"))).ReturnsAsync((Environment)null);

            var result = await controller.Delete("00000000-0000-0000-0000-000000000002");
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
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003")
            };

            mockedRepo.Setup(repo => repo.GetEnvironment(Guid.Parse("00000000-0000-0000-0000-000000000003"))).ReturnsAsync(newEnv);
            mockedRepo.Setup(repo => repo.DeleteEnvironment(Guid.Parse("00000000-0000-0000-0000-000000000003"))).ReturnsAsync(false);

            var result = await controller.Delete("00000000-0000-0000-0000-000000000003");
            var contentResult = result as StatusCodeResult;

            Assert.NotNull(contentResult);
            Assert.Equal(StatusCodes.Status500InternalServerError, contentResult.StatusCode);
        }
    }
}
