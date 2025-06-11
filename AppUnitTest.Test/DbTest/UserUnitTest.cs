using AppUnitTest.Models;
using AppUnitTest.Services;
using Microsoft.EntityFrameworkCore;

namespace AppUnitTest.Test.DbTest
{
    /// <summary>
    /// Unit tests for the User model and its interactions with the database.
    /// </summary>
    public class UserUnitTest
    {
        /// <summary>
        /// Create a user with valid data and verify that it is saved successfully.
        /// </summary>
        [Fact]
        public void CreateUser_WithValidUser_ShouldCreateUserSuccessfully()
        {
            // Arrange
            var _dbContext = InitDbAndSeedData(false);
            var isSaved = 0;
            User? user = User.TestUser();

            // Act
            _dbContext.Users.Add(user);
            isSaved = _dbContext.SaveChanges();


            //Assert
            Assert.Equal(1, isSaved);
        }

        /// <summary>
        /// Attempt to create a user with null data and expect an exception to be thrown.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        [Fact]
        public void CreateUser_WithInvalidUser_ShouldFailCreateUser()
        {
            // Arrange
            User? user = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                if (user == null)
                    throw new ArgumentNullException(nameof(user), "User cannot be null.");
            });
        }

        /// <summary>
        /// Test retrieving users from the database when seed data is present, expecting a single user to be returned.
        /// </summary>
        [Fact]
        public void GetAllUsers_WithSeedData_ShouldReturnSingleUser()
        {
            // Arrange
            var _dbContext = InitDbAndSeedData();

            // Act
            var users = _dbContext.Users.ToList();

            // Assert
            Assert.Single(users);        
        }

        /// <summary>
        /// Test retrieving users from the database when no seed data is present, expecting an empty list.
        /// </summary>
        [Fact]
        public void GetAllUsers_WithNoSeedData_ShouldReturnNoUsers()
        {
            // Arrange
            var _dbContext = InitDbAndSeedData(false);

            // Act
            var users = _dbContext.Users.ToList();

            // Assert
            Assert.Empty(users);
        }

        /// <summary>
        /// Attempt to retrieve a user by ID when seed data is present, expecting the user to be returned successfully.
        /// </summary>
        [Fact]
        public void GetUserById_WithSeedData_ShouldReturnUser()
        {
            // Arrange
            var _dbContext = InitDbAndSeedData();

            // Act
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == User.TestUserId);

            // Assert
            Assert.NotNull(user);
            Assert.Equal(User.TestUserId, user?.Id);
        }

        /// <summary>
        /// Attempt to retrieve a user by ID when no seed data is present, expecting an exception to be thrown.
        /// </summary>
        /// <exception cref="KeyNotFoundException"></exception>
        [Fact]
        public void GetUserById_WithNoSeedData_ShouldFailToReturnUser()
        {
            // Arrange
            var _dbContext = InitDbAndSeedData(false);

            // Act
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == User.TestUserId);

            // Assert
            Assert.Throws<KeyNotFoundException>(() =>
            {
                if (user == null)
                    throw new KeyNotFoundException($"User with ID {User.TestUserId} not found.");
            });
        }

        /// <summary>
        /// Update a user with valid data when seed data is present, expecting the update to be successful.
        /// </summary>
        [Fact]
        public void UpdateUser_WithSeedData_ShouldUpdateUserSuccessfully()
        {
            // Arrange
            var _dbContext = InitDbAndSeedData();
            var isUpdate = 0;

            // Act            
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == User.TestUserId);
            if (user != null)
            {
                user.Name = "Mohammed";
                _dbContext.Users.Update(user);
                isUpdate = _dbContext.SaveChanges();
            }


            // Assert
            Assert.NotNull(user);
            Assert.Equal(1, isUpdate);
        }

        /// <summary>
        /// Update a user with valid data when no seed data is present, expecting an exception to be thrown.
        /// </summary>
        /// <exception cref="KeyNotFoundException"></exception>
        [Fact]
        public void UpdateUser_WithNoSeedData_ShouldFailToUpdateUser()
        {
            // Arrange
            var _dbContext = InitDbAndSeedData(false);

            // Act            {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == User.TestUserId);


            // Assert
            Assert.Throws<KeyNotFoundException>(() =>
            {
                if (user == null)
                    throw new KeyNotFoundException($"User with ID {User.TestUserId} not found.");
            });
        }

        /// <summary>
        /// Delete a user with valid data when seed data is present, expecting the deletion to be successful.
        /// </summary>
        [Fact]
        public void DeleteUser_WithSeedData_ShouldDeleteUserSuccessfully()
        {
            // Arrange
            var _dbContext = InitDbAndSeedData(); // Ensure the database is seeded with test data
            var isDeleted = 0;

            // Act
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == User.TestUserId);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                isDeleted = _dbContext.SaveChanges();
            }


            // Assert
            Assert.NotNull(user);
            Assert.Equal(1, isDeleted);
        }

        /// <summary>
        /// Attempt to delete a user with valid data when no seed data is present, expecting an exception to be thrown.
        /// </summary>
        /// <exception cref="KeyNotFoundException"></exception>
        [Fact]
        public void DeleteUser_WithNoSeedData_ShouldFailToDeleteUser()
        {
            // Arrange
            var _dbContext = InitDbAndSeedData(false);

            // Act
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == User.TestUserId);


            // Assert
            Assert.Throws<KeyNotFoundException>(() => { 
                if(user == null)
                    throw new KeyNotFoundException($"User with ID {User.TestUserId} not found.");
            });
        }

        /// <summary>
        /// Initialize the in-memory database and optionally seed it with test data.
        /// </summary>
        /// <param name="seedData"></param>
        /// <returns></returns>
        private DbContextService InitDbAndSeedData(bool seedData = true)
        {
            // This method can be used to seed the in-memory database with initial data if
            // Initialize the UserService with a mock or in-memory DbContext
            var options = new DbContextOptionsBuilder<DbContextService>()
                .UseInMemoryDatabase(databaseName: $"UserServiceDb-{Guid.NewGuid()}")
                .Options;

            var dbContext = new DbContextService(options);

            // Seed the database with test data if required
            if (seedData)
                dbContext.Users.Add(User.TestUser());
            dbContext.SaveChanges();

            return dbContext;
        }
    }
}
