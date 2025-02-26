using BE_BTOnline2.Models.Requests;
using BE_BTOnline2.Models.Responses;

namespace BE_BTOnline2.Services.Interfaces
{
    public interface IUserServices
    {
        List<UserResponse> GetAllUsers();
        UserResponse GetUserById(int userId);
        bool AddUser(UserRequest request);
        bool UpdateUser(int userId, UserRequest request);
        bool DeleteUser(int userId);
    }
}
