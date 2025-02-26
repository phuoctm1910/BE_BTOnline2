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
    public class AllowAccessServices : IAllowAccessServices
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AllowAccessServices(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<AllowAccessResponse> GetAllAllowAccess()
        {
            var allowAccesses = _context.AllowAccesses.ToList();
            return allowAccesses == null ? new List<AllowAccessResponse>() : _mapper.Map<List<AllowAccessResponse>>(allowAccesses);
        }

        public AllowAccessResponse GetAllowAccessById(int id)
        {
            var allowAccess = _context.AllowAccesses.FirstOrDefault(a => a.Id == id);
            return allowAccess == null ? new AllowAccessResponse() : _mapper.Map<AllowAccessResponse>(allowAccess);
        }

        public bool AddAllowAccess(AllowAccessRequest request)
        {
            var newAllowAccess = _mapper.Map<AllowAccess>(request);
            _context.AllowAccesses.Add(newAllowAccess);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateAllowAccess(int id, AllowAccessRequest request)
        {
            var allowAccess = _context.AllowAccesses.FirstOrDefault(a => a.Id == id);
            if (allowAccess == null) return false;

            _mapper.Map(request, allowAccess);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteAllowAccess(int id)
        {
            var allowAccess = _context.AllowAccesses.FirstOrDefault(a => a.Id == id);
            if (allowAccess == null) return false;

            _context.AllowAccesses.Remove(allowAccess);
            _context.SaveChanges();
            return true;
        }
    }
}
