using BE_BTOnline2.Models.Requests;
using BE_BTOnline2.Models.Responses;

namespace BE_BTOnline2.Services.Interfaces
{
    public interface IRoleServices
    {
        List<RoleResponse> GetAllRoles();
        RoleResponse GetRoleById(int roleId);
        bool AddRole(RoleRequest request);
        bool UpdateRole(int roleId, RoleRequest request);
        bool DeleteRole(int roleId);
    }
}
