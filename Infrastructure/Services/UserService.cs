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
    public class UserService : GenericRepository<User>, IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public User Login(string mail, string password)
        {
            var user = _context.Users.FirstOrDefault(i=>i.Email== mail && i.Password==password);
            if (user == null)
            {
                return null;
            }
            else return user;
        }
    }
}
