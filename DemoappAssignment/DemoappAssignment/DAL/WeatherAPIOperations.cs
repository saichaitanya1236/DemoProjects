using DemoappAssignment.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace DemoappAssignment.DAL
{
    public class WeatherAPIOperations
    {
        private Logger _logger;
        
        public WeatherAPIOperations(Logger logger)
        {
            _logger = logger;
        }

        public Userdetails get_temperature_based_on_city(string city,Userdetails objuserdetails)
        {
            Userdetails userdet = objuserdetails;
            try
            {
               
                StringBuilder sbApibuilder = new StringBuilder();
                sbApibuilder.Append(Convert.ToString(ConfigurationManager.AppSettings["WeatherAPIURL"]));
                sbApibuilder.Replace("$accesskey$", Convert.ToString(ConfigurationManager.AppSettings["Weather_access_Key"]).Replace("$cityname$", city));
                sbApibuilder.Replace("$cityname$", city);
                WebClient webClient = new WebClient();
                webClient.Headers.Add("Content-type", "application/json");
                var json = webClient.DownloadString(sbApibuilder.ToString());
                APIResponse aPIResponse = JsonConvert.DeserializeObject<APIResponse>(json);
                if (aPIResponse != null)
                {
                    if (aPIResponse.error == null)
                    {
                      userdet.temperature = Convert.ToString(aPIResponse.current.temperature);
                        if (aPIResponse.current.weather_descriptions.Count > 0) {
                            userdet.weathersummary = aPIResponse.current.weather_descriptions[0].Trim();
                                }
                    }
                    else
                    {
                        userdet.temperature = "Temperature unavaliable for this city";
                        userdet.weathersummary = "Weather description not available";
                    }
                }
                else
                {
                    userdet.temperature = "Temperature unavaliable for this city";
                    userdet.weathersummary = "Weather description not available";
                }
            }
            catch (Exception ex)
            {
                userdet.temperature = "Temperature unavaliable for this city";
                userdet.weathersummary = "Weather description not available";
                _logger.LogWrite("Exception handled in DAL for method get_temperature_based_on_city:" + ex.Message);
            }
            
            return userdet;
        }
    }
}