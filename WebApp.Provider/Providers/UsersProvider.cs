using System.Text;
using WebApp.Contracts.Entities.Common.Users;
using WebApp.Contracts.Entities.Result;
using WebApp.Provider.Providers.Interfaces;
using WebApp.Repository.Repos.Interfaces;

namespace WebApp.Provider.Providers
{
    public class UsersProvider : IUsersProvider
    {
        private readonly IRepository repository;

        public UsersProvider(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<OperationResult<User>> CreateUserAsync(NewUser newUser)
        {
            // Tutaj możliwe użycie FluentValidation zamiast ręcznego sprawdzania
            // Początek walidacji
            StringBuilder errorString = new();

            if (string.IsNullOrEmpty(newUser.Username))
                errorString.AppendLine("Username is null or empty.");

            if (newUser.Username.Length > 150)
                errorString.AppendLine("Length of Username is greater than 150.");

            if (newUser.ExpirationDate < DateTime.UtcNow)
                errorString.AppendLine("ExpirationDate is less than DateTime.UtcNow.");

            if (errorString.Length > 0)
                return new OperationResult<User>(errorString.ToString(), nameof(UsersProvider));
            // Koniec walidacji

            // Użycie warstwy repozytorium
            return await repository.CreateUserAsync(newUser);
        }

        public async Task<BaseOperationResult> DeleteUserAsync(int userId)
        {
            return await repository.DeleteUserAsync(userId);
        }

        public async Task<OperationResult<User>> GetUserAsync(int userId)
        {
            return await repository.GetUserAsync(userId);
        }

        public async Task<OperationResult<User[]>> GetUsersAsync()
        {
            return await repository.GetUsersAsync();
        }

        public async Task<BaseOperationResult> UpdateUserAsync(UpdatedUser updatedUser)
        {
            // Początek walidacji
            StringBuilder errorString = new();

            if (string.IsNullOrEmpty(updatedUser.Username))
                errorString.AppendLine("Username is null or empty.");

            if (updatedUser.Username.Length > 150)
                errorString.AppendLine("Length of Username is greater than 150.");

            if (updatedUser.ExpirationDate < DateTime.UtcNow)
                errorString.AppendLine("ExpirationDate is less than DateTime.UtcNow.");

            if (errorString.Length > 0)
                return new BaseOperationResult(errorString.ToString(), nameof(UsersProvider));
            // Koniec walidacji

            // Wykorzystanie wartstwy repozytorium
            return await repository.UpdateUserAsync(updatedUser);
        }
    }
}
