using Microsoft.EntityFrameworkCore;
using Moq;
using StoriesAPI.Infrastruture.Models;
using StoriesAPI.Service.Service;

namespace StoriesAPI.Tests.Service
{
    [TestClass]
    public class VoteServiceTests
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
        public async Task VoteStory_ExistingVote_ReturnsTrue()
        {
            
            var userId = 1;
            var storyId = 1;
            var vote = true;

            using (var context = new StoryContext(_options))
            {
                context.Votes.Add(new Vote { UserId = userId, StoryId = storyId, Voted = !vote });
                await context.SaveChangesAsync();
            }

            using (var context = new StoryContext(_options))
            {
                var service = new VoteService(context);
                var result = await service.VoteStory(userId, storyId, vote);

                Assert.IsTrue(result);

                var updatedVote = await context.Votes.FirstOrDefaultAsync(v => v.UserId == userId && v.StoryId == storyId);
                Assert.IsNotNull(updatedVote);
                Assert.AreEqual(vote, updatedVote.Voted);
            }
        }

        [TestMethod]
        public async Task VoteStory_NewVote_ReturnsTrue()
        {

            var userId = 1;
            var storyId = 1;
            var vote = true;

            using (var context = new StoryContext(_options))
            {
                var service = new VoteService(context);
                var result = await service.VoteStory(userId, storyId, vote);

                Assert.IsTrue(result);

                var newVote = await context.Votes.FirstOrDefaultAsync(v => v.UserId == userId && v.StoryId == storyId);
                Assert.IsNotNull(newVote);
                Assert.AreEqual(vote, newVote.Voted);
            }
        }
        [TestMethod]
        public async Task DeleteVote_ExistingVote_ReturnsDeletedVoteDTO()
        {

            var existingVoteId = 1;

            using (var context = new StoryContext(_options))
            {
                var existingVote = new Vote { Id = existingVoteId };
                context.Votes.Add(existingVote);
                await context.SaveChangesAsync();
            }

            using (var context = new StoryContext(_options))
            {
                var service = new VoteService(context);
                var deletedVoteDTO = await service.DeleteVote(existingVoteId);

                Assert.IsNotNull(deletedVoteDTO);
                Assert.AreEqual(existingVoteId, deletedVoteDTO.Id);

                var deletedVote = await context.Votes.FindAsync(existingVoteId);
                Assert.IsNull(deletedVote);
            }
        }
        [TestMethod]
        public async Task DeleteVote_NonExistingVote_ReturnsNull()
        {

            var nonExistingVoteId = 999;

            using (var context = new StoryContext(_options))
            {
                var service = new VoteService(context);
                var deletedVoteDTO = await service.DeleteVote(nonExistingVoteId);

                Assert.IsNull(deletedVoteDTO);
            }
        }


        [TestMethod]
        public async Task AddUser_NewUser_ReturnsUserDTO()
        {
            var userName = "UserUserUser";
            var userService = new VoteService(new StoryContext(_options));

            var result = await userService.AddUser(userName);

            Assert.IsNotNull(result);
            Assert.AreEqual(userName, result.Name);
        }

        [TestMethod]
        public async Task AddUser_NullName_ReturnsNull()
        {

            var userService = new VoteService(new StoryContext(_options));

            var result = await userService.AddUser(null);

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task DeleteUser_ExistingUser_ReturnsDeletedUserDTO()
        {
            var userId = 1;
            var userName = "TestUser";

            using (var context = new StoryContext(_options))
            {
                context.Users.Add(new User { Id = userId, Name = userName });
                await context.SaveChangesAsync();
            }

            using (var context = new StoryContext(_options))
            {
                var userService = new VoteService(context);

                var result = await userService.DeleteUser(userId);

                Assert.IsNotNull(result);
                Assert.AreEqual(userId, result.Id);

                var deletedUser = await context.Users.FindAsync(userId);
                Assert.IsNull(deletedUser);
            }
        }
        [TestMethod]
        public async Task GetUser_ExistingUser_ReturnsUserDTO()
        {
            var userId = 1;
            var userName = "TestUser";

            using (var context = new StoryContext(_options))
            {
                context.Users.Add(new User { Id = userId, Name = userName });
                await context.SaveChangesAsync();
            }

            using (var context = new StoryContext(_options))
            {
                var userService = new VoteService(context);

                var result = await userService.GetUser(userId);

                Assert.IsNotNull(result);
                Assert.AreEqual(userId, result.Id);
                Assert.AreEqual(userName, result.Name);
            }
        }
        [TestMethod]
        public async Task GetUser_NonExistingUser_ReturnsNull()
        {
            var nonExistingUserId = 999;

            using (var context = new StoryContext(_options))
            {
                var userService = new VoteService(context);

                var result = await userService.GetUser(nonExistingUserId);

                Assert.IsNull(result);
            }
        }
        [TestMethod]
        public async Task GetUsers_ReturnsUserDTOList()
        {
            var usersData = new List<User>
            {
                new User { Id = 1, Name = "User1" },
                new User { Id = 2, Name = "User2" },
                new User { Id = 3, Name = "User3" }
            };

            using (var context = new StoryContext(_options))
            {
                await context.Users.AddRangeAsync(usersData);
                await context.SaveChangesAsync();
            }

            using (var context = new StoryContext(_options))
            {
                var userService = new VoteService(context);

                var result = await userService.GetUsers();

                Assert.IsNotNull(result);
                Assert.AreEqual(usersData.Count, result.Count);

                for (int i = 0; i < usersData.Count; i++)
                {
                    Assert.AreEqual(usersData[i].Id, result[i].Id);
                    Assert.AreEqual(usersData[i].Name, result[i].Name);
                }
            }
        }
    }
}
