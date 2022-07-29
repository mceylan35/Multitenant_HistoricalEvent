using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Multitenant.Api.Models
{
    public class TrHistoricalEvent
    {
        [JsonProperty("ID")]
        public long Id { get; set; }

        [JsonProperty("dc_Zaman")]
        public string DcZaman { get; set; }

        [JsonProperty("dc_Kategori")]
        public string DcKategori { get; set; }

        [JsonProperty("dc_Olay")]
        public string DcOlay { get; set; }
    }
}