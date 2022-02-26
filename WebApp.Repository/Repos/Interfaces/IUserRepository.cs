using WebApp.Contracts.Entities.Common.Users;
using WebApp.Contracts.Entities.Result;

namespace WebApp.Repository.Repos.Interfaces
{
    public interface IUserRepository
    {
        Task<OperationResult<User>> GetUserAsync(int userId);
        Task<OperationResult<User[]>> GetUsersAsync();
        Task<OperationResult<User>> CreateUserAsync(NewUser newUser);
        Task<BaseOperationResult> UpdateUserAsync(UpdatedUser updatedUser);
        Task<BaseOperationResult> DeleteUserAsync(int userId);
    }
}
