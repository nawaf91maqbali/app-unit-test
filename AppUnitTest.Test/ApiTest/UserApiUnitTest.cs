using AppUnitTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace AppUnitTest.Test.ApiTest
{
    /// <summary>
    /// Unit tests for User API endpoints to ensure correct functionality and response codes.
    /// </summary>
    public class UserApiUnitTest
    {
        /// <summary>
        /// attempt to get users with data seed should return a list of users with OK status code
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetUsers_WithDataSeed_ShouldReturnListOfUsers()
        {
            // Arrange
            var _client = new CustomProgram().CreateClient();

            // Act
            var response = await _client.GetAsync("/api/user/GetUsers");
            response.EnsureSuccessStatusCode();
            var users = await response.Content.ReadFromJsonAsync<List<User>>() ?? new List<User>();

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.Single(users);
        }

        /// <summary>
        /// attempt to get users with no data seed should return an empty list with OK status code
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetUsers_WithNoDataSeed_ShouldReturnEmptyList()
        {
            // Arrange
            var _client = new CustomProgram(false).CreateClient();

            // Act
            var response = await _client.GetAsync("/api/user/GetUsers");
            response.EnsureSuccessStatusCode();
            var users = await response.Content.ReadFromJsonAsync<List<User>>() ?? new List<User>();

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.Empty(users);
        }

        /// <summary>
        /// attempt to get a user by a valid ID should return the user object with OK status code
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetUserById_WithValidId_ShouldReturnUser()
        {
            // Arrange
            var _client = new CustomProgram().CreateClient();
            var userId = User.TestUserId;

            // Act
            var response = await _client.GetAsync($"/api/user/GetUserById/{userId}");
            response.EnsureSuccessStatusCode();
            var user = await response.Content.ReadFromJsonAsync<User>();

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(user);
            Assert.Equal(userId, user?.Id);
        }

        /// <summary>
        /// attempt to get a user by an invalid ID should return NotFound status code
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetUserById_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            var _client = new CustomProgram(false).CreateClient();

            // Act
            var response = await _client.GetAsync($"/api/user/GetUserById/{User.TestUserId}");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        /// <summary>
        /// attempt to create a user with a valid user object should return Created status code
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateUser_WithValidUser_ShouldCreateUser()
        {
            // Arrange
            var _client = new CustomProgram(false).CreateClient();
            var user = User.TestUser();

            // Act
            var response = await _client.PostAsJsonAsync("/api/user/CreateUser", user);
            response.EnsureSuccessStatusCode();
            var createdUser = await response.Content.ReadFromJsonAsync<User>();

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(createdUser);
        }

        /// <summary>
        /// attempt to create a user with a null user object should return BadRequest status code
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateUser_WithNullUser_ShouldReturnBadRequest()
        {
            // Arrange
            var _client = new CustomProgram(false).CreateClient();
            var user = new User(); // Create an empty user object

            // Act
            var response = await _client.PostAsJsonAsync("/api/user/CreateUser", user);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }

        /// <summary>
        /// attempt to update a user with a valid user object should return NoContent status code
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateUser_WithValidUser_ShouldUpdateUser()
        {
            // Arrange
            var _client = new CustomProgram().CreateClient();
            var user = User.TestUser();

            // Act
            var response = await _client.PutAsJsonAsync("/api/user/UpdateUser", user);
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        }

        /// <summary>
        /// attempt to update a user with a null user object should return BadRequest status code
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateUser_WithNullUser_ShouldReturnBadRequest()
        {
            // Arrange
            var _client = new CustomProgram().CreateClient();
            var user = new User(); // Create an empty user object

            // Act
            var response = await _client.PutAsJsonAsync("/api/user/UpdateUser", user);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }

        /// <summary>
        /// attempt to update a user with an invalid user object should return NotFound status code
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateUser_WithInValidUser_ShouldReturnNotFound()
        {
            // Arrange
            var _client = new CustomProgram(false).CreateClient();
            var user = User.TestUser();

            // Act
            var response = await _client.PutAsJsonAsync("/api/user/UpdateUser", user);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        /// <summary>
        /// attempt to delete a user with a valid ID should return NoContent status code
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteUser_WithValidId_ShouldDeleteUser()
        {
            // Arrange
            var _client = new CustomProgram().CreateClient();
            var userId = User.TestUserId;

            // Act
            var response = await _client.DeleteAsync($"/api/user/DeleteUser/{userId}");
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        }

        /// <summary>
        /// attempt to delete a user with an invalid ID should return NotFound status code
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteUser_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            var _client = new CustomProgram(false).CreateClient();

            // Act
            var response = await _client.DeleteAsync($"/api/user/DeleteUser/{User.TestUserId}");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        /// <summary>
        /// use this method to test the delete user endpoint with different seed data scenarios.
        /// </summary>
        /// <param name="seedData"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        //[Theory]
        //[InlineData(true, HttpStatusCode.NoContent)]
        //[InlineData(false, HttpStatusCode.NotFound)]
        //public async Task DeleteUser_ShouldBehaveAccordingToSeed(bool seedData, HttpStatusCode status)
        //{
        //    // Arrange
        //    var _client = new CustomProgram(seedData).CreateClient();

        //    // Act
        //    var response = await _client.DeleteAsync($"/api/user/DeleteUser/{User.TestUserId}");

        //    // Assert
        //    Assert.Equal(status, response.StatusCode);
        //}

    }
}
