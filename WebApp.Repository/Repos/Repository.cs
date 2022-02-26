using Microsoft.EntityFrameworkCore;
using WebApp.Contracts.Entities.Common.Users;
using WebApp.Contracts.Entities.Result;
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

        public Task<OperationResult<User>> CreateUserAsync(NewUser newUser)
        {
            throw new NotImplementedException();
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

        public Task<BaseOperationResult> UpdateUserAsync(UpdatedUser updatedUser)
        {
            throw new NotImplementedException();
        }
    }
}
