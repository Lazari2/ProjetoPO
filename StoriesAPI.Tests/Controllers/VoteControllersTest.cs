using Microsoft.AspNetCore.Mvc;
using System.Net;
using Moq;
using StoriesAPI.Application.Controllers;
using StoriesAPI.Service.Service;

namespace StoriesAPI.Tests.Controllers
{
    [TestClass]
    public class VoteControllersTest
    {
        [TestMethod]
        public void VoteController_CanCreateInstance()
        {
            var mockVoteService = new Mock<IVoteService>();

            var controller = new VoteController(mockVoteService.Object);

            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public async Task VoteStory_ReturnsOkResult()
        {
            int userId = 1;
            int storyId = 1;
            bool vote = true;

            var mockVoteService = new Mock<IVoteService>();
            mockVoteService.Setup(service => service.VoteStory(userId, storyId, vote))
                            .ReturnsAsync(true);

            var controller = new VoteController(mockVoteService.Object);

            var result = await controller.VoteStory(userId, storyId, vote) as OkResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public async Task VoteStory_ReturnsBadRequestResult()
        {
            int userId = 1;
            int storyId = 1;
            bool vote = true;

            var mockVoteService = new Mock<IVoteService>();
            mockVoteService.Setup(service => service.VoteStory(userId, storyId, vote))
                            .ReturnsAsync(false);

            var controller = new VoteController(mockVoteService.Object);

            var result = await controller.VoteStory(userId, storyId, vote) as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
            Assert.AreEqual("Error.", result.Value);
        }

        [TestMethod]
        public async Task CheckVote_ReturnsTrue()
        {
            int userId = 1;
            int storyId = 1;

            var mockVoteService = new Mock<IVoteService>();
            mockVoteService.Setup(service => service.CheckVote(userId, storyId))
                            .ReturnsAsync(true);

            var controller = new VoteController(mockVoteService.Object);

            var result = await controller.CheckVote(userId, storyId);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task CheckVote_ReturnsFalse()
        {
            int userId = 1;
            int storyId = 1;

            var mockVoteService = new Mock<IVoteService>();
            mockVoteService.Setup(service => service.CheckVote(userId, storyId))
                            .ReturnsAsync(false);

            var controller = new VoteController(mockVoteService.Object);

            var result = await controller.CheckVote(userId, storyId);

            Assert.IsFalse(result);
        }

    }
}