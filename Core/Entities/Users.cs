using Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Users:BaseEntity, IMustHaveTenant
    {
        public Users()
        {
            HistoricalEvents = new HashSet<HistoricalEvents>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        [Required]
        public string TenantId { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<HistoricalEvents> HistoricalEvents { get; set; }
    }
}