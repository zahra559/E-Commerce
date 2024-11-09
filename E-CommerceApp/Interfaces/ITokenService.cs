using E_CommerceApp.Models;

namespace E_CommerceApp.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user , IList<string> userRoles);
    }
}