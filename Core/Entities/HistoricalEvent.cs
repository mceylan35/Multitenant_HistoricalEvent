using Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class HistoricalEvent:BaseEntity,IMustHaveTenant
    {

        public HistoricalEvent(string DcZaman, string DcKategori, string DcOlay)
        {
            this.DcKategori = DcKategori;
            this.DcZaman = DcZaman;
            this.DcOlay = DcOlay;
        }
        public string DcZaman { get; set; }

         
        public string DcKategori { get; set; }

        
        public string DcOlay { get; set; }
        public int Rate { get; private set; }
        public string TenantId { get; set; }
    }
}
