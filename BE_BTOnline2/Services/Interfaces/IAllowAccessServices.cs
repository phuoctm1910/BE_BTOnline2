using BE_BTOnline2.Models.Requests;
using BE_BTOnline2.Models.Responses;

namespace BE_BTOnline2.Services.Interfaces
{
    public interface IAllowAccessServices
    {
        List<AllowAccessResponse> GetAllAllowAccess();
        AllowAccessResponse GetAllowAccessById(int id);
        bool AddAllowAccess(AllowAccessRequest request);
        bool UpdateAllowAccess(int id, AllowAccessRequest request);
        bool DeleteAllowAccess(int id);
    }
}
