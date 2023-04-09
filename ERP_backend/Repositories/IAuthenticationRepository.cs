using ERP_backend.Model;

namespace ERP_backend.Repositories
{
    public interface IAuthenticationRepository
    {

        public bool AuthenticateUser(UserAuthenticationDTO userAuthenticationDTO);
        public string GenerateJWT(string? userIdentity);
    }
}
