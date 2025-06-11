using AppUnitTest.Models;
using Microsoft.EntityFrameworkCore;

namespace AppUnitTest.Services
{
    /// <summary>
    /// Interface for user service that defines methods for user management operations.
    /// </summary>
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(Guid userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<int> CreateUserAsync(User user);
        Task<int> UpdateUserAsync(User user);
        Task<int> DeleteUserAsync(Guid userId);
    }

    /// <summary>
    /// Service for managing user operations such as creating, retrieving, updating, and deleting users.
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// Database context service for accessing the user data.
        /// </summary>
        private readonly DbContextService _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class with the specified database context.
        /// </summary>
        /// <param name="dbContext"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public UserService(DbContextService dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <summary>
        /// Retrieves all users from the database asynchronously.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _dbContext.Users.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Retrieves a user by their unique identifier asynchronously.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);

            if (user is null)
                throw new KeyNotFoundException($"User with ID {userId} not found.");

            return user;
        }

        /// <summary>
        /// Creates a new user in the database asynchronously.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<int> CreateUserAsync(User? user)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user));

            await _dbContext.Users.AddAsync(user);

            return await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing user in the database asynchronously.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<int> UpdateUserAsync(User? user)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user));

            if(!(await _dbContext.Users.AsNoTracking().AnyAsync(x => x.Id == user.Id)))
                throw new KeyNotFoundException($"User with ID {user.Id} not found.");

            _dbContext.Users.Update(user);
        
            return await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a user from the database asynchronously by their unique identifier.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<int> DeleteUserAsync(Guid userId)
        {
            var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);
            if (user is null)
                throw new KeyNotFoundException($"User with ID {userId} not found.");

            _dbContext.Users.Remove(user);

            return await _dbContext.SaveChangesAsync(); ;
        }
    }
}
