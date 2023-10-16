using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DemoappAssignment.Models
{
    public class Userdetails:UserModel
    {
        [DisplayName("Temperature(°C)")]
        public string temperature { get; set; }

        [DisplayName("Weather Condition")]
        public string weathersummary { get; set; }
    }
}