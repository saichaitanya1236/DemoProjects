using DemoappAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoappAssignment.BAL
{
    internal interface Iuser
    {
        ResponseObject AddUser(UserModel user);
        List<selectclassListmodel> GetEmailList();
        Userdetails GetUserdetails(int id);
        List<selectclassListmodel> GetCountriesList();
        List<selectclassListmodel> GetcityList(int countryid);
    }
}
