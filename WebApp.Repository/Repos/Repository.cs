using Microsoft.EntityFrameworkCore;
using WebApp.Contracts.Entities.Common.Users;
using WebApp.Contracts.Entities.Result;
using WebApp.Repository.Entities;
using WebApp.Repository.Repos.Interfaces;

namespace WebApp.Repository.Repos
{
    public partial class Repository : IRepository
    {
        private readonly WebAppDbContext dbContext;

        public Repository(WebAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<OperationResult<User>> CreateUserAsync(NewUser newUser)
        {
            if (await dbContext.Users.AnyAsync(x => x.Username == newUser.Username))
                return new OperationResult<User>
                    ($"User with username: {newUser.Username} already exists.");

            var dbUser = new DbUser
            {
                Username = newUser.Username,
                Email = newUser.Email,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                ExpirationDate = newUser.ExpirationDate,
                IsEnabled = newUser.IsEnabled,
                Password = newUser.Password,
                Telephone = newUser.Telephone,
                InsertDate = DateTime.UtcNow,
                ModifyDate = DateTime.UtcNow,
                LastLoginDate = null
            };

            dbContext.Users.Add(dbUser);
            await dbContext.SaveChangesAsync();

            var addedUser = new User
            {
                UserId = dbUser.UserId,
                Username = dbUser.Username,
                Password = dbUser.Password,
                Email = dbUser.Email,
                ExpirationDate = dbUser.ExpirationDate,
                FirstName = dbUser.FirstName,
                LastName = dbUser.LastName,
                Telephone = dbUser.Telephone,
                IsEnabled = dbUser.IsEnabled
            };

            return new OperationResult<User>(addedUser);
        }

        public async Task<BaseOperationResult> DeleteUserAsync(int userId)
        {
            var dbUser = await dbContext.Users
                .SingleOrDefaultAsync(x => x.UserId == userId);

            if (dbUser == null)
                return new BaseOperationResult("Item not found");

            dbContext.Users.Remove(dbUser);
            await dbContext.SaveChangesAsync();
            return BaseOperationResult.SuccessfulOperation;
        }

        public async Task<OperationResult<User>> GetUserAsync(int userId)
        {
            var dbUser = await dbContext.Users
                .SingleOrDefaultAsync(x => x.UserId == userId);

            if (dbUser == null)
                return new OperationResult<User>((User)null);

            return new OperationResult<User>(new User
            {
                UserId = dbUser.UserId,
                Username = dbUser.Username,
                Password = dbUser.Password,
                Email = dbUser.Email,
                ExpirationDate = dbUser.ExpirationDate,
                FirstName = dbUser.FirstName,
                LastName = dbUser.LastName,
                Telephone = dbUser.Telephone,
                IsEnabled = dbUser.IsEnabled
            });
        }

        public async Task<OperationResult<User[]>> GetUsersAsync()
        {
            var dbUsers = await dbContext.Users
                .ToArrayAsync();

            if (dbUsers.Length == 0)
                return new OperationResult<User[]>(Array.Empty<User>());

            var users = new List<User>();
            foreach(var dbUser in dbUsers)
            {
                users.Add(new User
                {
                    UserId = dbUser.UserId,
                    Username = dbUser.Username,
                    Password = dbUser.Password,
                    Email = dbUser.Email,
                    ExpirationDate = dbUser.ExpirationDate,
                    FirstName = dbUser.FirstName,
                    LastName = dbUser.LastName,
                    Telephone = dbUser.Telephone,
                    IsEnabled = dbUser.IsEnabled
                });
            }

            return new OperationResult<User[]>(users.ToArray());
        }

        public async Task<BaseOperationResult> UpdateUserAsync(UpdatedUser updatedUser)
        {
            var dbUser = await dbContext.Users
                .SingleOrDefaultAsync(x => x.UserId == updatedUser.UserId);

            if (dbUser == null)
                return new BaseOperationResult("Item not found");

            if (await dbContext.Users.AnyAsync(x => x.Username == updatedUser.Username
                && x.UserId != updatedUser.UserId))
                return new BaseOperationResult
                    ($"User with username: {updatedUser.Username} already exists.");

            dbContext.Entry(dbUser).CurrentValues.SetValues(updatedUser);
            dbUser.ModifyDate = DateTime.UtcNow;
            await dbContext.SaveChangesAsync();

            return BaseOperationResult.SuccessfulOperation;
        }
    }
}
