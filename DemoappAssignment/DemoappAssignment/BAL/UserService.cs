using DemoappAssignment.DAL;
using DemoappAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoappAssignment.BAL
{
    public class UserService : Iuser
    {
        private Operations _ops;
        private Logger _logger;
        private WeatherAPIOperations _weatherAPIOperations;
        public UserService(Logger logger)
        {
            _logger = logger;
            _ops = new Operations(_logger);
            _weatherAPIOperations = new WeatherAPIOperations(_logger);
        }
        public ResponseObject AddUser(UserModel user)
        {
            ResponseObject response = new ResponseObject();
           
            try
            {
               DateTime result1;
               if(! DateTime.TryParse(user.Birthdate,out result1 ))
               {
                    response.status = "fail";
                    response.Message = "Invalid date format";
                    return response;
               }
               bool result = _ops.AddUser(user);
                if (result)
                {
                    response.status = "success";
                    response.Message = "User Created Successfully";
                  _logger.LogWrite(string.Format("User {0} {1} added record for {2}",user.FirstName,user.LastName,user.City));
                }
            }
            catch (Exception ex)
            {

                _logger.LogWrite("Exception handled:" + ex.Message);
                if(ex.Message.Contains("Email"))
                {
                   response.status = "fail";
                   response.Message= "User already exists with same email address";
                }
                      
            }
            return response;
        }

        public List<selectclassListmodel> GetcityList(int countryid)
        {
            List<selectclassListmodel> lstcities = new List<selectclassListmodel>();
            try
            {
                //selectclassListmodel objselectpart = new selectclassListmodel() { Text = "--Please select--", Id = 0 };

                lstcities = _ops.GetallCityBasedoncountryid(countryid);
                // lstemails.Insert(0, objselectpart);
            }
            catch (Exception ex)
            {
                _logger.LogWrite("Exception handled in BAL for method GetcityList():" + ex.Message);
                //  throw;
            }
            return lstcities;
        }

        public List<selectclassListmodel> GetCountriesList()
        {
            List<selectclassListmodel> lstemails = new List<selectclassListmodel>();
            try
            {
              //  selectclassListmodel objselectpart = new selectclassListmodel() { Text = "--Please select--", Id = 0 };

                lstemails = _ops.GetallCountries();
               // lstemails.Insert(0, objselectpart);
            }
            catch (Exception ex)
            {
                _logger.LogWrite("Exception handled in BAL for method GetCountriesList():" + ex.Message);
                //  throw;
            }
            return lstemails;
        }

        public List<selectclassListmodel> GetEmailList()
        {
            List<selectclassListmodel> lstemails=new List<selectclassListmodel>();
            try
            {
                selectclassListmodel objselectpart = new selectclassListmodel() { Text = "--Please select--", Id = 0 };

               lstemails=_ops.GetEmailList();
                lstemails.Insert(0, objselectpart);
            }
            catch (Exception ex)
            {
                _logger.LogWrite("Exception handled in BAL for method GetEmailList():" + ex.Message);
              //  throw;
            }
            return lstemails;
           // throw new NotImplementedException();
        }

        public Userdetails GetUserdetails(int id)
        {
            Userdetails objdetails = null;
            try
            {
                objdetails = _ops.GetUserdetailsbyId(id);
                if(objdetails != null)
                {
                    _logger.LogWrite(string.Format("Read record for User {0} {1} in {2}",objdetails.FirstName,objdetails.LastName,objdetails.City));
                    if(!string.IsNullOrEmpty(objdetails.City))
                    {
                        objdetails = _weatherAPIOperations.get_temperature_based_on_city(objdetails.City,objdetails);                       
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("Exception handled in BAL for method GetUserdetails():" + ex.Message);
                objdetails=null;
            }
            return objdetails;

        }
    }
}