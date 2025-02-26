using BE_BTOnline2.Models.Requests;
using System.Collections.Generic;

namespace BE_BTOnline2.Services.Interfaces
{
    public interface IInternServices
    {
        List<dynamic> GetAllInterns(int roleId);
        dynamic GetInternById(int id, int roleId);
        bool AddIntern(InternRequest request);
        bool UpdateIntern(int id, InternRequest request);
        bool DeleteIntern(int id);
    }
}
