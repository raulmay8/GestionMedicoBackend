using GestionMedicoBackend.Models;
using UserModel = GestionMedicoBackend.Models.User;

namespace GestionMedicoBackend.Services.Auth
{
    public interface IAuthService
    {
        Task<UserModel> Authenticate(string email, string password);
    }
}
