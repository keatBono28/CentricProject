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
            ViewBag.Message = "About Centric";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Get In Contact with the Admins.";

            return View();
        }
    }
}