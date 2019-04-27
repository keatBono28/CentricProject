using CentricProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CentricProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult About()
        {
            string access = "";
            if (AuthorizeLoggedInUser() == 0)
            {
                access = "deny";
            }
            else
            {
                access = "show";
            }
            ViewBag.Message = access;
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Get In Contact with the Admins.";
            return View();
        }

        private int AuthorizeLoggedInUser()
        {
            int userId = 0;
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            if (manager.FindById(User.Identity.GetUserId()) != null)
            {
                var currentUser = manager.FindById(User.Identity.GetUserId());
                userId = currentUser.ProfileDetails.id;
                return userId;
            }
            else
            {
                return userId;
            }
        }
    }
}