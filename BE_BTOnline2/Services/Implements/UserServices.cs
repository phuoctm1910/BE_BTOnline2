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
    public class UserServices : IUserServices
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserServices(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<UserResponse> GetAllUsers()
        {
            var users = _context.Users.Include(u => u.Role).ToList();
            return users == null ? new List<UserResponse>() : _mapper.Map<List<UserResponse>>(users);
        }

        public UserResponse GetUserById(int userId)
        {
            var user = _context.Users.Include(u => u.Role).FirstOrDefault(u => u.UserId == userId);
            return user == null ? new UserResponse() : _mapper.Map<UserResponse>(user);
        }

        public bool AddUser(UserRequest request)
        {
            var newUser = _mapper.Map<User>(request);
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateUser(int userId, UserRequest request)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null) return false;

            _mapper.Map(request, user);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null) return false;

            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }
    }
}
