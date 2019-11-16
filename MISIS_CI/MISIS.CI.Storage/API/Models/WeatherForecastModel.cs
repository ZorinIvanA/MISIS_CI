using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Razor;

namespace MISIS.CI.Storage.API.Models
{
    public class WeatherForecastModel
    {
        [Required]
        public float Lat { get; set; }
        [Required]
        public float Lon { get; set; }
        [Required]
        public float Temperature { get; set; }
        [Required]
        public string City { get; set; }
    }
}
