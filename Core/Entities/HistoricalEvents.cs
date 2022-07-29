using Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class HistoricalEvents : BaseEntity, IMustHaveTenant
    {
        public long Id { get; set; }
        public string DcZaman { get; set; }
        public string DcKategori { get; set; }
        public string DcOlay { get; set; }
        public int? Rate { get; set; }
        public string TenantId { get; set; }
        public long? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(Users.HistoricalEvents))]
        public virtual Users User { get; set; }
    }
}