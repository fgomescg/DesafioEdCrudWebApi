using Entities.Models;

namespace Contracts
{
    public interface IidentityService
    {
        string BuildJWTToken();
        bool Authenticate(Login login);
    }
}
