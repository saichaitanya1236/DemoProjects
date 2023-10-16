using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoappAssignment.Models
{
    public class APIResponse
    {
        public Request request { get; set; }
        public Location location { get; set; }
        public Current current { get; set; }
        public bool success { get; set; }
        public Error error { get; set; }
    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Error
    {
        public int code { get; set; }
        public string type { get; set; }
        public string info { get; set; }
    }
    public class Current
    {
        public string observation_time { get; set; }
        public int temperature { get; set; }
        public int weather_code { get; set; }
        public List<string> weather_icons { get; set; }
        public List<string> weather_descriptions { get; set; }
        public string is_day { get; set; }
    }

    public class Location
    {
        public string name { get; set; }
        public string country { get; set; }
        public string region { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public string timezone_id { get; set; }
        public string localtime { get; set; }
        public int localtime_epoch { get; set; }
        public string utc_offset { get; set; }
    }

    public class Request
    {
        public string type { get; set; }
        public string query { get; set; }
        public string language { get; set; }
        public string unit { get; set; }
    }




}