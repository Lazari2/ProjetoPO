using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StoriesAPI.Application.Controllers;
using StoriesAPI.Service.DTO;
using StoriesAPI.Service.Service;
using StoriesAPI.ViewModel;

namespace StoriesAPI.Tests.Controllers
{
    [TestClass]
    public class StoriesControllersTest
    {
        [TestMethod]
        public void StoriesController_CanCreateInstance()
        {
            var mockStoryService = new Mock<IStoryService>();

            var controller = new StoriesController(mockStoryService.Object);

            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public async Task AddStory_ReturnsOkResult()
        {
            var newStoryViewModel = new StoryViewModel
            {
                Title = "Test Title",
                Description = "Test Description",
                Departament = "Test Departament"
            };

            var mockStoryService = new Mock<IStoryService>();
            mockStoryService.Setup(service => service.AddStory(It.IsAny<StoryDTO>()))
                            .ReturnsAsync(new StoryDTO { Id = 1,
                                                        Title = "Test Title",
                                                        Description="Test Description",
                                                        Departament ="Test Departament" 
                                                        });

            var controller = new StoriesController(mockStoryService.Object);

            var result = await controller.AddStory(newStoryViewModel) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOfType(result.Value, typeof(StoryDTO));
        }

        [TestMethod]
        public async Task GetStories_ReturnsOkResult()
        {
            var mockStoryService = new Mock<IStoryService>();
            mockStoryService.Setup(service => service.GetStories())
                            .ReturnsAsync(new List<StoryDTO> 
                            {new StoryDTO 
                                 {Id = 1,
                                  Title = "Test Title", Description = "Test Description",
                                  Departament = "Test Departament" } 
                            });

            var controller = new StoriesController(mockStoryService.Object);

            var result = await controller.GetStories() as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOfType(result.Value, typeof(List<StoryDTO>));
        }

        [TestMethod]
        public async Task UpdateStory_ReturnsOkResult()
        {
            int storyId = 1;
            var updatedStoryViewModel = new StoryViewModel
            {
                Id = storyId,
                Title = "Updated Test Title",
                Description = "Updated Test Description",
                Departament = "Updated Test Departament"
            };

            var mockStoryService = new Mock<IStoryService>();
            mockStoryService.Setup(service => service.UpdateStory(It.IsAny<StoryDTO>()))
                            .ReturnsAsync(new StoryDTO
                            {
                                Id = storyId,
                                Title = "Updated Test Title",
                                Description = "Updated Test Description",
                                Departament = "Updated Test Departament"
                            });

            var controller = new StoriesController(mockStoryService.Object);

            var result = await controller.UpdateStory(storyId, updatedStoryViewModel) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.IsInstanceOfType(result.Value, typeof(StoryDTO));
        }

        [TestMethod]
        public async Task DeleteStory_ReturnsOkResult()
        {

            int storyId = 1;
            var deletedStoryDTO = new StoryDTO
            {
                Id = storyId,
                Title = "Deleted Test Title",
                Description = "Deleted Test Description",
                Departament = "Deleted Test Departament"
            };

            var mockStoryService = new Mock<IStoryService>();
            mockStoryService.Setup(service => service.DeleteStory(storyId))
                            .ReturnsAsync(deletedStoryDTO);

            var controller = new StoriesController(mockStoryService.Object);

            var result = await controller.DeleteStory(storyId) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.IsInstanceOfType(result.Value, typeof(StoryDTO));
        }

        [TestMethod]
        public async Task GetStory_ReturnsOkResult()
        {
            int storyId = 1;
            var storyDTO = new StoryDTO
            {
                Id = storyId,
                Title = "Test Title",
                Description = "Test Description",
                Departament = "Test Departament"
            };

            var mockStoryService = new Mock<IStoryService>();
            mockStoryService.Setup(service => service.GetStory(storyId))
                            .ReturnsAsync(storyDTO);

            var controller = new StoriesController(mockStoryService.Object);

            var result = await controller.GetStory(storyId) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.IsInstanceOfType(result.Value, typeof(StoryDTO));
        }

        [TestMethod]
        public async Task GetVoteCount_ReturnsOkResult()
        {
            int storyId = 1;
            int voteCount = 100;

            var mockStoryService = new Mock<IStoryService>();
            mockStoryService.Setup(service => service.GetVoteCount(storyId))
                            .ReturnsAsync(voteCount);

            var controller = new StoriesController(mockStoryService.Object);

            var result = await controller.GetVoteCount(storyId) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.IsInstanceOfType(result.Value, typeof(int));
            Assert.AreEqual(voteCount, (int)result.Value);
        }
    }
}
