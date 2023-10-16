using DemoappAssignment.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace DemoappAssignment.DAL
{
    public class Operations
    {
        static string Constring = string.Empty;//ConfigurationManager.ConnectionStrings[1].ConnectionString;
        SQLiteConnection conn;
        SQLiteCommand cmd;
        SQLiteDataAdapter adapter;
        SQLiteDataReader reader;
        private Logger _logger;
        public Operations(Logger logger)
        {
            _logger = logger;
            Constring = ConfigurationManager.ConnectionStrings[1].ConnectionString;
        }
        internal bool AddUser(UserModel user)
        {
            bool result = false;
            try
            {
                _logger.LogWrite("Adding User");
                using (conn = new SQLiteConnection(Constring))
                {
                    conn.Open();
                    cmd = new SQLiteCommand(conn);
                    cmd.CommandText = "insert into UserMaster(FirstName,LastName,Email,BirthDate,MobileNumber,City,Country)values(@fname,@lname,@email,@dob,@phone,@city,@country)";
                    cmd.Parameters.AddWithValue("@fname", user.FirstName);
                    cmd.Parameters.AddWithValue("@lname", user.LastName);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@dob", user.Birthdate);
                    cmd.Parameters.AddWithValue("@phone", user.MobileNumber);
                    cmd.Parameters.AddWithValue("@city", user.City);
                    cmd.Parameters.AddWithValue("@country", user.Country);
                    int effortedrows = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (effortedrows > 0)
                    {
                        result = true;

                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("Exception handled in DAL for method AddUser:" + ex.Message);
                throw ex;

            }
            return result;
        }
        internal List<selectclassListmodel> GetEmailList()
        {
            List<selectclassListmodel> lstitems = new List<selectclassListmodel>();
            try
            {
                using (conn = new SQLiteConnection(Constring))
                {
                    conn.Open();
                    cmd = new SQLiteCommand(conn);
                    cmd.CommandText = "SELECT Email as 'Text',Id as 'value' from UserMaster";
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            lstitems.Add(new selectclassListmodel() { Text = reader["Text"].ToString(), Id = Convert.ToInt32(reader["value"]) });
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("Exception handled in DAL for method GetEmailList:" + ex.Message);
                throw ex;
            }
            return lstitems;
        }

        internal Userdetails GetUserdetailsbyId(int Id)
        {
            Userdetails objuser=null;
            try
            {
                using (conn = new SQLiteConnection(Constring))
                {

                    cmd = new SQLiteCommand(conn);
                    cmd.CommandText = "SELECT FirstName,LastName,BirthDate,Email,MobileNumber,City,Country FROM UserMaster WHERE Id=" + Id;
                    DataSet dataSet = new DataSet();
                    adapter = new SQLiteDataAdapter(cmd);
                    adapter.Fill(dataSet);
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        objuser = new Userdetails()
                        {
                            FirstName = dataSet.Tables[0].Rows[0]["FirstName"].ToString(),
                            LastName = dataSet.Tables[0].Rows[0]["LastName"].ToString(),
                            Birthdate = dataSet.Tables[0].Rows[0]["BirthDate"].ToString(),
                            Email = dataSet.Tables[0].Rows[0]["Email"].ToString(),
                            MobileNumber = dataSet.Tables[0].Rows[0]["MobileNumber"].ToString(),
                            Country = dataSet.Tables[0].Rows[0]["Country"].ToString(),
                            City = dataSet.Tables[0].Rows[0]["City"].ToString()
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("Exception handled in DAL for method GetUserdetailsbyId:"+Id+" " + ex.Message);
                objuser = null;
                throw ex;
            }
            return objuser;
        }

        internal List<selectclassListmodel> GetallCountries()
        {
            List<selectclassListmodel> list = new List<selectclassListmodel>();
            try
            {
                using (conn = new SQLiteConnection(Constring))
                {
                    conn.Open();
                    cmd = new SQLiteCommand(conn);
                    cmd.CommandText = "SELECT CountryName as 'Text',Id as 'value' from Country_Master";
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            list.Add(new selectclassListmodel() { Text = reader["Text"].ToString(), Id = Convert.ToInt32(reader["value"]) });
                        }
                    }
                    conn.Close();
                }
            }
            catch ( Exception ex)
            {

                //throw;
                _logger.LogWrite("Exception handled in DAL for method GetallCountries:" + ex.Message);
            }
            return list;
        }
        internal List<selectclassListmodel> GetallCityBasedoncountryid(int countryid)
        {
            List<selectclassListmodel> list = new List<selectclassListmodel>();
            try
            {
                using (conn = new SQLiteConnection(Constring))
                {
                    conn.Open();
                    cmd = new SQLiteCommand(conn);
                    cmd.CommandText = "SELECT CityName as 'Text',Id as 'value' from City_Master WHERE CountryId="+countryid;
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            list.Add(new selectclassListmodel() { Text = reader["Text"].ToString(), Id = Convert.ToInt32(reader["value"]) });
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

                //throw;
                _logger.LogWrite("Exception handled in DAL for method GetallCityBasedoncountryid("+countryid+"):" + ex.Message);
            }
            return list;
        }
    }
}