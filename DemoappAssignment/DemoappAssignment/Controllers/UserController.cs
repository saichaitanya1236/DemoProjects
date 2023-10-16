using DemoappAssignment.BAL;
using DemoappAssignment.DAL;
using DemoappAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace DemoappAssignment.Controllers
{
    public class UserController : Controller
    {
        private Iuser _userservice;
        private Logger _logger;
        public UserController()
        {
            _logger = new Logger();
            _userservice = new UserService(_logger);

        }
        // GET: User
        public ActionResult Index()
        {
            var lstcountries = _userservice.GetCountriesList();
            Session["Countryist"] = new SelectList(lstcountries, "Id", "Text");
            return View();
        }

        // GET: User/Details/5
        public ActionResult Details()
        {
            var lstemailids = _userservice.GetEmailList();
            Session["Emaillist"] = new SelectList(lstemailids, "Id", "Text");

            return View();

        }
        [HttpPost]
        public JsonResult Details(int id)
        {
            //var id = Convert.ToInt32(collection["ddlEmails"]);

            if (id > 0)
            {
                var userdetails = _userservice.GetUserdetails(id);

                return Json(userdetails);
            }
            else
            {
                return Json("");
            }


        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Index(FormCollection user)
        {

            if (ModelState.IsValid)
            {
                UserModel userModel = new UserModel();
                userModel.FirstName = user[1];
                userModel.LastName = user[2];
                userModel.Birthdate = user[3];
                userModel.Email= user[4];
                userModel.MobileNumber = user[5];
                userModel.Country = user[6];
                userModel.City = user[7];
                SelectList selectLists =(SelectList)Session["Countryist"];
                userModel.Country = selectLists.Where(i=>i.Value== user[6]).Select(i=>i.Text).FirstOrDefault();
                var cityselectLists =(List<selectclassListmodel>) Session["citylist"];
                userModel.City = cityselectLists.Where(i => i.Id.ToString() == user[7]).Select(i => i.Text).FirstOrDefault();
                var result = _userservice.AddUser(userModel);
                if (result.status == "success")
                {
                    ModelState.Clear();
                    ViewBag.Message = String.Format("User {0} {1} created Successfully!", userModel.FirstName, userModel.LastName);
                }
                else
                {
                    ViewBag.ErrorMessage = result.Message;
                }
            }
            return View();

        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        [HttpGet]
        public JsonResult Getcitylist(int countryid)
        {
            if (countryid > 0)
            {
                var citylist = _userservice.GetcityList(countryid);
                Session["citylist"]= citylist;
                return Json(citylist, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

       
    }
}
