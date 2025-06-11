using AppUnitTest.Models;
using AppUnitTest.Services;
using Microsoft.EntityFrameworkCore;

namespace AppUnitTest.Test.ServiceTest
{
    /// <summary>
    /// Unit tests for the UserService class.
    /// </summary>
    public class UserServiceUnitTest
    {
        /// <summary>
        /// Test Add User Method With Valid User
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateUserAsync_WithValidUser_ShouldCreateUserSuccessfully()
        {
            // Arrange
            var userService = await InitDbAndSeedData();
            var user = User.TestUser();
            user.Id = Guid.NewGuid(); // Ensure a new ID for the test user

            // Act
            var isSaved = await userService.CreateUserAsync(user);


            //Assert
            Assert.Equal(1, isSaved);
        }

        /// <summary>
        /// Test Add User Method With Invalid User
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateUserAsync_WithInvalidUser_ShouldThrowArgumentNullException()
        {
            // Arrange
            var userService = await InitDbAndSeedData();

            // Act Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await userService.CreateUserAsync(default));
        }

        /// <summary>
        /// Test Get All Users Method With Data Seed
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetAllUsersAsync_WithDataSeed_ShouldReturnSingleUser()
        {
            // Arrange
            var userService = await InitDbAndSeedData();

            // Act
            var users = await userService.GetAllUsersAsync();

            // Assert
            Assert.Single(users);
        }

        /// <summary>
        /// Test Get All Users Method With No Data Seed
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetAllUsersAsync_WithNoDataSeed_ShouldReturnEmptyList()
        {
            // Arrange
            var userService = await InitDbAndSeedData(false);

            // Act
            var users = await userService.GetAllUsersAsync();

            // Assert
            Assert.Empty(users);
        }

        /// <summary>
        /// Test Get User By Id Method With Data Seed
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetUserByIdAsync_WithDataSeed_ShouldReturnSeededUser()
        {
            // Arrange
            var userService = await InitDbAndSeedData();

            // Act
            var user = await userService.GetUserByIdAsync(User.TestUserId);

            // Assert
            Assert.Equal(User.TestUserId, user?.Id);
        }

        /// <summary>
        /// Test Get User By Id Method With No Seeded Data
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetUserByIdAsync_WithNoDataSeed_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var userService = await InitDbAndSeedData(false);

            // Act Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(async () => await userService.GetUserByIdAsync(User.TestUserId));
        }

        /// <summary>
        /// Test Update User Method With Valid User
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateUserAsync_WithValidUser_ShouldUpdateUserSuccessfully()
        {
            // Arrange
            var userService = await InitDbAndSeedData();
            var user = User.TestUser();

            // Act
            user.Name = "Mohammed";
            var isUpdate = await userService.UpdateUserAsync(user);

            // Assert
            Assert.Equal(1, isUpdate);
        }

        /// <summary>
        /// Test Update User Method With Null User
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateUserAsync_WithNullUser_ShouldThrowArgumentNullException()
        {
            // Arrange
            var userService = await InitDbAndSeedData(false);

            // Act Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await userService.UpdateUserAsync(default));
        }

        /// <summary>
        /// Test Update User Method With No Data Seed
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateUserAsync_WithNoDataSeed_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var userService = await InitDbAndSeedData(false);
            var user = User.TestUser();

            // Act
            user.Name = "Mohammed";

            // Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(async () => await userService.UpdateUserAsync(user));
        }

        /// <summary>
        /// Test Delete User Method With Data Seed
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteUserAsync_WithDataSeed_ShouldDeleteUserSuccessfully()
        {
            // Arrange
            var userService = await InitDbAndSeedData();

            // Act
            var isDeleted = await userService.DeleteUserAsync(User.TestUserId);

            // Assert
            Assert.Equal(1, isDeleted);
        }

        /// <summary>
        /// Test Delete User Method With No Data Seed
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteUserAsync_WithNoDataSeed_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var userService = await InitDbAndSeedData(false);

            // Act Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(async () => await userService.DeleteUserAsync(User.TestUserId));
        }

        /// <summary>
        /// Initialize the database and seed it with test data.
        /// </summary>
        /// <returns></returns>
        private async Task<UserService> InitDbAndSeedData(bool seedData = true)
        {
            // This method can be used to seed the in-memory database with initial data if
            // Initialize the UserService with a mock or in-memory DbContext
            var options = new DbContextOptionsBuilder<DbContextService>()
                .UseInMemoryDatabase(databaseName: $"UserServiceDb-{Guid.NewGuid()}")
                .Options;

            var dbContext = new DbContextService(options);
            var userService = new UserService(dbContext);

            // Seed the database with test data if required
            if (seedData)
                await userService.CreateUserAsync(User.TestUser());

            // Optionally, you can add more test users here if needed

            return userService;
        }
    }
}
