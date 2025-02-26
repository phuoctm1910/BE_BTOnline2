using AutoMapper;
using BE_BTOnline2.DB;
using BE_BTOnline2.Models.Requests;
using BE_BTOnline2.Models.Responses;
using BE_BTOnline2.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BE_BTOnline2.Services.Implements
{
    public class RoleServices : IRoleServices
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public RoleServices(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<RoleResponse> GetAllRoles()
        {
            var roles = _context.Roles.ToList();
            return roles == null ? new List<RoleResponse>() : _mapper.Map<List<RoleResponse>>(roles);
        }

        public RoleResponse GetRoleById(int roleId)
        {
            var role = _context.Roles.FirstOrDefault(r => r.RoleId == roleId);
            return role == null ? new RoleResponse() : _mapper.Map<RoleResponse>(role);
        }

        public bool AddRole(RoleRequest request)
        {
            var newRole = _mapper.Map<Role>(request);
            _context.Roles.Add(newRole);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateRole(int roleId, RoleRequest request)
        {
            var role = _context.Roles.FirstOrDefault(r => r.RoleId == roleId);
            if (role == null) return false;

            _mapper.Map(request, role);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteRole(int roleId)
        {
            var role = _context.Roles.FirstOrDefault(r => r.RoleId == roleId);
            if (role == null) return false;

            _context.Roles.Remove(role);
            _context.SaveChanges();
            return true;
        }
    }
}
