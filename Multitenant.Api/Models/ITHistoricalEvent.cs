using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Multitenant.Api.Models
{
    public class ITHistoricalEvent
    {
        [JsonProperty("ID")]
        public long Id { get; set; }

        [JsonProperty("dc_Orario")]
        public string DcOrario { get; set; }

        [JsonProperty("dc_Categoria")]
        public string DcCategoria { get; set; }

        [JsonProperty("dc_Evento")]
        public string DcEvento { get; set; }
    }
}
