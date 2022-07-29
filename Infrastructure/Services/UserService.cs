using Core.Entities;
using Core.Interfaces;
using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService : GenericRepository<Users>, IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public Users Login(string mail, string password)
        {
            var user = _context.Users.FirstOrDefault(i=>i.Mail== mail && i.Password==password);
            if (user == null)
            {
                return null;
            }
            else return user;
        }
    }
}
