using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MISIS.CI.Gateway.BusinessLogic.Dto
{
    public class ForecastDto
    {
        public float Lat { get; set; }
        public float Lon { get; set; }
        [JsonProperty("currently.temperature")]
        public float Temperature { get; set; }
        [JsonProperty("timezone")]
        public string City { get; set; }
    }
}
