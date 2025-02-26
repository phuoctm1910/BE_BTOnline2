using BE_BTOnline2.Models.Requests;
using BE_BTOnline2.Models.Responses;

namespace BE_BTOnline2.Services.Interfaces
{
    public interface IAuthServices
    {
        AuthResponse Authenticate(AuthRequest request);
    }
}
