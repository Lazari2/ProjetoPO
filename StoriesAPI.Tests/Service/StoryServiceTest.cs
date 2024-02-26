using Microsoft.EntityFrameworkCore;
using StoriesAPI.Infrastruture.Models;
using StoriesAPI.Service.DTO;
using StoriesAPI.Service.Service;

namespace StoriesAPI.Tests.Service
{
    [TestClass]
    public class StoryServiceTests
    {
        private DbContextOptions<StoryContext> _options;

        [TestInitialize]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<StoryContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }

        [TestMethod]
        public async Task PostStory_ReturnsCorrectData()
        {
            var newStory = new StoryDTO
            {
                Title = "NewStory",
                Description = "NewDescription",
                Departament = "NewDepartament"
            };


            using (var context = new StoryContext(_options))
            {
                var storyService = new StoryService(context);
                var result = await storyService.AddStory(newStory);


                Assert.IsNotNull(result);
                Assert.IsTrue(result.Id > 0);
                Assert.AreEqual(newStory.Title, result.Title);
                Assert.AreEqual(newStory.Description, result.Description);
                Assert.AreEqual(newStory.Departament, result.Departament);
            }
        }

        [TestMethod]

        public async Task UpdateStory_ReturnsCorrectData()
        {

            var existingStoryId = 1;
            var existingStory = new Story
            {
                Id = existingStoryId,
                Title = "ExistingTitle",
                Description = "ExistingDescription",
                Departament = "ExistingDepartment"
            };

            var newStory = new StoryDTO
            {
                Id = existingStoryId,
                Title = "UpdatedTitle",
                Description = "UpdatedDescription",
                Departament = "UpdatedDepartment"
            };


            using (var context = new StoryContext(_options))
            {
                await context.Stories.AddAsync(existingStory);
                await context.SaveChangesAsync();

                var storyService = new StoryService(context);
                var result = await storyService.UpdateStory(newStory);

                Assert.IsNotNull(result);
                Assert.AreEqual(newStory.Id, result.Id);
                Assert.AreEqual(newStory.Title, result.Title);
                Assert.AreEqual(newStory.Description, result.Description);
                Assert.AreEqual(newStory.Departament, result.Departament);
            }
        }

        [TestMethod]
        public async Task DeleteStory_ReturnsCorrectData()
        {

            using (var context = new StoryContext(_options))
            {
                context.Stories.Add(new Story
                {
                    Id = 1,
                    Title = "ExistingTitle",
                    Description = "ExistingDescription",
                    Departament = "ExistingDepartment"
                });
                await context.SaveChangesAsync();
            }

            using (var context = new StoryContext(_options))
            {
                var service = new StoryService(context);
                var result = await service.DeleteStory(1);

                Assert.IsNotNull(result);
                Assert.AreEqual(1, result.Id);
                Assert.AreEqual("ExistingTitle", result.Title);
                Assert.AreEqual("ExistingDescription", result.Description);
                Assert.AreEqual("ExistingDepartment", result.Departament);
            }

            using (var context = new StoryContext(_options))
            {
                var deletedStory = await context.Stories.FindAsync(1);
                Assert.IsNull(deletedStory);
            }

        }
        [TestMethod]
        public async Task DeleteStory_ReturnsNullIfStoryNotFound()
        {

            using (var context = new StoryContext(_options))
            {
                var service = new StoryService(context);
                var result = await service.DeleteStory(999);

                Assert.IsNull(result);
            }
        }
        [TestMethod]
        public async Task GetStories_ReturnsCorrectData()
        {

            var expectedStories = new List<Story>
            {
                new Story { Id = 1, Title = "Title1", Description = "Description1", Departament = "Department1" },
                new Story { Id = 2, Title = "Title2", Description = "Description2", Departament = "Department2" },
                new Story { Id = 3, Title = "Title3", Description = "Description3", Departament = "Department3" }
            };

            using (var context = new StoryContext(_options))
            {
                context.Stories.AddRange(expectedStories);
                await context.SaveChangesAsync();
            }

            List<StoryDTO> result;
            using (var context = new StoryContext(_options))
            {
                var service = new StoryService(context);
                result = await service.GetStories();
            }

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedStories.Count, result.Count);

            for (int i = 0; i < expectedStories.Count; i++)
            {
                Assert.AreEqual(expectedStories[i].Id, result[i].Id);
                Assert.AreEqual(expectedStories[i].Title, result[i].Title);
                Assert.AreEqual(expectedStories[i].Description, result[i].Description);
                Assert.AreEqual(expectedStories[i].Departament, result[i].Departament);
            }
        }
        [TestMethod]
        public async Task GetStory_ReturnsCorrectData()
        {
            var expectedStory = new Story { Id = 1, Title = "Title1", Description = "Description1", Departament = "Department1" };

            using (var context = new StoryContext(_options))
            {
                context.Stories.Add(expectedStory);
                await context.SaveChangesAsync();
            }

            StoryDTO result;
            using (var context = new StoryContext(_options))
            {
                var service = new StoryService(context);
                result = await service.GetStory(1);
            }

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedStory.Id, result.Id);
            Assert.AreEqual(expectedStory.Title, result.Title);
            Assert.AreEqual(expectedStory.Description, result.Description);
            Assert.AreEqual(expectedStory.Departament, result.Departament);
        }

        [TestMethod]
        public async Task GetStory_WithNonExistingId_ReturnsNull()
        {

            StoryDTO result;
            using (var context = new StoryContext(_options))
            {
                var service = new StoryService(context);
                result = await service.GetStory(1);
            }

            Assert.IsNull(result);
        }
    }
}
