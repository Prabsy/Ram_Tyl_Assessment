using Refit;
using System.Threading.Tasks;

public interface IUserApi
{
    [Get("/api/users/{id}")]
    Task<TestUserModel> GetUserByIdAsync(int id);

    [Post("/api/users")]
    Task<ApiResponse<RegisterUserModel>> CreateUserAsync([Body] RegisterUserModel user);
}
