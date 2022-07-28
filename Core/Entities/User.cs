using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class User:BaseEntity
    {
        
        public string Name { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; } 
        public int? TenantId { get; set; }
    }
}
