using GestionMedicoBackend.Models;
using System.Threading.Tasks;
using UserModel = GestionMedicoBackend.Models.User;

namespace GestionMedicoBackend.Services.Auth
{
    public interface IAuthService
    {
        Task<UserModel> Authenticate(string email, string password);
        Task<UserModel> AuthenticateWithFacebook(string accessToken); 
    }
}
