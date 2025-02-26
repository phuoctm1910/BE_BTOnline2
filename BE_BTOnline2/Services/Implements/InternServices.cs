using BE_BTOnline2.DB;
using BE_BTOnline2.Models.Requests;
using BE_BTOnline2.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace BE_BTOnline2.Services.Implements
{
    public class InternServices : IInternServices
    {
        private readonly AppDbContext _context;

        public InternServices(AppDbContext context)
        {
            _context = context;
        }

        public List<dynamic> GetAllInterns(int roleId)
        {
            var access = _context.AllowAccesses
                .FirstOrDefault(a => a.RoleId == roleId && a.TableName.ToLower() == "intern");

            if (access == null)
                return new List<dynamic>();

            // ToLower() tất cả các giá trị của allowedColumns
            var allowedColumns = access.AccessProperties.Split(',')
                                                        .Select(col => col.Trim().ToLower())
                                                        .ToList();

            var interns = _context.Interns.ToList();
            var result = new List<dynamic>();

            foreach (var intern in interns)
            {
                dynamic expandoObj = new ExpandoObject();
                var dictionary = (IDictionary<string, object>)expandoObj;

                // Nếu AccessProperties là "all" thì lấy hết tất cả các trường
                if (allowedColumns.Contains("all"))
                {
                    foreach (var property in intern.GetType().GetProperties())
                    {
                        dictionary.Add(property.Name, property.GetValue(intern));
                    }
                }
                else
                {
                    // Ngược lại chỉ lấy các trường được phép
                    foreach (var property in intern.GetType().GetProperties())
                    {
                        // So sánh không phân biệt hoa thường
                        if (allowedColumns.Contains(property.Name.Trim().ToLower()))
                        {
                            dictionary.Add(property.Name, property.GetValue(intern));
                        }
                    }
                }

                result.Add(expandoObj);
            }

            return result;
        }
        public dynamic GetInternById(int id, int roleId)
        {
            var access = _context.AllowAccesses
                .FirstOrDefault(a => a.RoleId == roleId && a.TableName.ToLower() == "intern");

            if (access == null)
                return null;

            // ToLower() tất cả các giá trị của allowedColumns
            var allowedColumns = access.AccessProperties.Split(',')
                                                        .Select(col => col.ToLower())
                                                        .ToList();

            var intern = _context.Interns.FirstOrDefault(i => i.Id == id);
            if (intern == null) return null;

            dynamic expandoObj = new ExpandoObject();
            var dictionary = (IDictionary<string, object>)expandoObj;

            // Nếu AccessProperties là "all" thì lấy hết tất cả các trường
            if (allowedColumns.Contains("all"))
            {
                foreach (var property in intern.GetType().GetProperties())
                {
                    dictionary.Add(property.Name, property.GetValue(intern));
                }
            }
            else
            {
                // Ngược lại chỉ lấy các trường được phép
                foreach (var property in intern.GetType().GetProperties())
                {
                    // So sánh không phân biệt hoa thường
                    if (allowedColumns.Contains(property.Name.ToLower()))
                    {
                        dictionary.Add(property.Name, property.GetValue(intern));
                    }
                }
            }

            return expandoObj;
        }
        public bool AddIntern(InternRequest request)
        {
            var newIntern = new Intern();
            _context.Entry(newIntern).CurrentValues.SetValues(request);
            _context.Interns.Add(newIntern);
            return _context.SaveChanges() > 0;
        }

        public bool UpdateIntern(int id, InternRequest request)
        {
            var intern = _context.Interns.FirstOrDefault(i => i.Id == id);
            if (intern == null) return false;

            _context.Entry(intern).CurrentValues.SetValues(request);
            return _context.SaveChanges() > 0;
        }

        public bool DeleteIntern(int id)
        {
            var intern = _context.Interns.FirstOrDefault(i => i.Id == id);
            if (intern == null) return false;

            _context.Interns.Remove(intern);
            return _context.SaveChanges() > 0;
        }
    }
}
