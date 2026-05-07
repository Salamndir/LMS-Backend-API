using StudentApi.Models;

namespace StudentApi.Services
{
    // The Interface acts as a contract for our authentication logic.
    public interface IAuthService
    {
        // Returns a JWT token if credentials are valid, otherwise returns null.
        string Login(LoginRequest request);
    }
}